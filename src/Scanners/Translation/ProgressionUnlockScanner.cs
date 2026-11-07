using System;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger.Scanners
{
    public class ProgressionUnlockScanner : TranslationScanner
    {
        public ProgressionUnlockScanner(TranslationCatalog catalog)
            : base(catalog)
        {
        }

        public override void Scan()
        {
            Scan<ProgressionUnlock>(o => HandleProgressionUnlock(o));
        }

        public void HandleProgressionUnlock(ProgressionUnlock unlock)
        {
            if (unlock is null) return;
            RegisterOriginalText(
                "ProgressionUnlocks",
                "LogbookUnlockTitles",
                unlock.unlockLogDescription
            );
            RegisterOriginalText(
                "ProgressionUnlocks",
                "UnlockTitles",
                unlock.unlockTitle
            );
            RegisterOriginalText(
                "ProgressionUnlocks",
                "UnlockDescriptions",
                unlock.unlockDescription
            );
            RegisterOriginalText(
                "ProgressionUnlocks",
                "UnlockHints",
                unlock.unlockHint
            );
            if (!unlock.showProgression) return;
            RegisterOriginalText(
                "ProgressionUnlocks",
                "UnlockProgressTextTemplates",
                unlock.id,
                "Progress: {current}/{required} " + unlock.progressionString
            );
        }
    }
}

