using System;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger.Scanners
{
    public class UIScanner : TranslationScanner
    {
        public UIScanner(TranslationCatalog catalog) : base(catalog)
        {
        }

        public override void Scan()
        {
            Scan<UI_Page>(o => HandlePage(o));
            Scan<OS_Tooltip>(o => HandleTooltip(o));
            Scan<UI_GamemodeText>(o => HandleGamemodeText(o));
            Scan<UT_StatText>(o => HandleStatText(o));
            Scan<UT_RoachTextCounter>(o => HandleRoachTextCounter(o));
            Scan<UI_ObjectiveViewer>(o => HandleObjectiveViewer(o));
            Scan<UT_BadgeGiver>(o => HandleBadgeGiver(o));
            Scan<UT_HoverInfo>(o => HandleHoverInfo(o));
            Scan<TipList>(o => HandleTipList(o));
        }

        public void HandlePage(UI_Page page)
        {
            if (page is null) return;
            RegisterOriginalText(
                "MainMenu",
                "PageTitles",
                page.title
            );
        }

        public void HandleTooltip(OS_Tooltip tooltip)
        {
            if (tooltip is null) return;
            RegisterOriginalText(
                "QuietOS",
                "Tooltips",
                tooltip.tip
            );
        }

        public void HandleGamemodeText(UI_GamemodeText gamemodeText)
        {
            if (gamemodeText is null) return;
            RegisterOriginalText(
                "Gamemodes",
                "GamemodeTextPrefixes",
                gamemodeText.append
            );
        }

        public void HandleStatText(UT_StatText statText)
        {
            if (statText is null) return;
            RegisterOriginalText(
                "ScoreScreen",
                "StatDefaultTexts",
                statText.defaultText
            );
            RegisterOriginalText(
                "ScoreScreen",
                "StatTextTemplates",
                statText.textPrefix + "{value}"
            );
        }

        public void HandleRoachTextCounter(
            UT_RoachTextCounter roachTextCounter
        )
        {
            if (roachTextCounter is null) return;
            RegisterOriginalText(
                "GameplayTexts",
                "RoachCounterTemplates",
                roachTextCounter.textFormat
            );
        }

        public void HandleObjectiveViewer(UI_ObjectiveViewer objectiveViewer)
        {
            if (objectiveViewer is null) return;
            RegisterOriginalText(
                "Objectives",
                "ObjectiveViewerTitleTemplates",
                objectiveViewer.objectiveViewerTitle?.text
            );
        }

        public void HandleBadgeGiver(UT_BadgeGiver badgeGiver)
        {
            if (badgeGiver is null) return;
            RegisterOriginalText(
                "GameplayTexts",
                "BadgeTitles",
                badgeGiver.badgeText
            );
        }

        public void HandleHoverInfo(UT_HoverInfo hoverInfo)
        {
            if (hoverInfo is null) return;
            RegisterOriginalText(
                "GameplayTexts",
                "InteractionHints",
                hoverInfo.infoText
            );
        }

        public void HandleTipList(TipList tipList)
        {
            if (tipList is null) return;
            tipList.tips?.ForEach(
                i => RegisterOriginalText("DeathTexts", "DeathTips", i)
            );
        }
    }
}

