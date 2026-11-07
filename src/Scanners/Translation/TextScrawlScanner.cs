using System;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger.Scanners
{
    public class TextScrawlScanner : TranslationScanner
    {
        public TextScrawlScanner(TranslationCatalog catalog) : base(catalog)
        {
        }

        public override void Scan()
        {
            Scan<UT_ScrawlEffect>(o => HandleScrawlEffect(o));
            Scan<DEN_EngravedDoor>(o => HandleEngravedDoor(o));
            // Scan<UT_EventZone>(o => HandleEventZone(o));
            var database = GetFullAssetDatabase();
            database?.sessionEvents?.ForEach(i => HandleSessionEvent(i));
        }

        public void HandleScrawlEffect(UT_ScrawlEffect scrawlEffect)
        {
            if (scrawlEffect is null) return;
            RegisterOriginalText(
                "TextScrawls",
                "ScrawlTextTemplates",
                scrawlEffect.playScrawl
            );
        }

        public void HandleEngravedDoor(DEN_EngravedDoor engravedDoor)
        {
            if (engravedDoor is null) return;
            var randomTexts = engravedDoor.headerStrings;
            if (randomTexts is null || randomTexts.Count == 0) return;
            foreach (var randomText in randomTexts)
            {
                RegisterOriginalText(
                    "TextScrawls",
                    "ScrawlTextTemplates",
                    "<color=red>" + randomText + "</color>"
                );
            }
        }

        // public void HandleEventZone(UT_EventZone eventZone)
        // {
        //     if (eventZone is null) return;
        //     RegisterOriginalText(
        //         "TextScrawls",
        //         "ScrawlTextTemplates",
        //         eventZone.enterScrawl
        //     );
        //     RegisterOriginalText(
        //         "TextScrawls",
        //         "ScrawlTextTemplates",
        //         eventZone.exitScrawl
        //     );
        // }

        public void HandleSessionEvent(SessionEvent sessionEvent)
        {
            if (sessionEvent is null) return;
            sessionEvent.modules?.ForEach(i => HandleSessionEventModule(i));
        }

        public void HandleSessionEventModule(
            SessionEventModule sessionEventModule
        )
        {
            if (sessionEventModule is SessionEventModule_TextEvent te)
            {
                if (!te.randomLines)
                {
                    RegisterOriginalText(
                        "TextScrawls",
                        "ScrawlTextTemplates",
                        te.playScrawl
                    );
                    return;
                }
                var randomTexts = te.lines;
                if (randomTexts is null || randomTexts.Count == 0) return;
                foreach (var randomText in randomTexts)
                {
                    RegisterOriginalText(
                        "TextScrawls",
                        "ScrawlTextTemplates",
                        randomText
                    );
                }
                return;
            }
            if (sessionEventModule is SessionEventModule_ModuleSequencer ms)
            {
                ms.sequenceEvents
                    ?.ForEach(i => HandleSessionEventModuleSequence(i));
                return;
            }
            if (
                sessionEventModule is
                    SessionEventModule_AddModuleAfterTrigger am
            )
            {
                am.modules?.ForEach(i => HandleSessionEventModule(i));
                return;
            }
            if (
                sessionEventModule is
                    SessionEventModule_CameraDirectionCheck cc
            )
            {
                cc.modulesOnCheck?.ForEach(i => HandleSessionEventModule(i));
                return;
            }
        }

        public void HandleSessionEventModuleSequence(
            SessionEventModule_ModuleSequencer.SequenceEvent moduleSequence
        )
        {
            if (moduleSequence is null) return;
            moduleSequence.modules?.ForEach(i => HandleSessionEventModule(i));
        }
    }
}

