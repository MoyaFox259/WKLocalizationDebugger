using System;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger.Scanners
{
    public class ObjectiveScanner : TranslationScanner
    {
        public ObjectiveScanner(TranslationCatalog catalog) : base(catalog)
        {
        }

        public override void Scan()
        {
            Scan<CH_RoachCollector>(o => HandleRoachCollector(o));
            Scan<CH_ChallengeCounter>(o => HandleChallengeCounter(o));
            Scan<UT_ObjectiveController>(o => HandleObjectiveController(o));
        }

        public void HandleRoachCollector(CH_RoachCollector roachCollector)
        {
            if (roachCollector is null) return;
            RegisterOriginalText(
                "Objectives",
                "ObjectiveTitleTemplates",
                $"BANK {roachCollector.bankTarget} ROACHES"
            );
            RegisterOriginalText(
                "Objectives",
                "ObjectiveSuccessHeaders",
                roachCollector.successText
            );
        }

        public void HandleChallengeCounter(
            CH_ChallengeCounter challengeCounter
        )
        {
            if (challengeCounter is null) return;
            challengeCounter.objectives
                ?.ForEach(i => HandleObjectiveCounter(i));
        }

        public void HandleObjectiveCounter(
            CH_ChallengeCounter.ObjectiveCounter objectiveCounter
        )
        {
            if (objectiveCounter is null) return;
            RegisterOriginalText(
                "Objectives",
                "ObjectiveTitleTemplates",
                objectiveCounter.objectiveTitle
            );
            RegisterOriginalText(
                "Objectives",
                "ObjectiveDescriptionTemplates",
                objectiveCounter.objectiveDesc
            );
            RegisterOriginalText(
                "Objectives",
                "ObjectiveProgressHeaderTemplates",
                objectiveCounter.progressHeaderDesc
            );
            RegisterOriginalText(
                "Objectives",
                "ObjectiveSuccessHeaders",
                objectiveCounter.finishedHeaderDesc
            );
        }

        public void HandleObjectiveController(
            UT_ObjectiveController objectiveController
        )
        {
            if (objectiveController is null) return;
            RegisterOriginalText(
                "Objectives",
                "ObjectiveTitleTemplates",
                objectiveController.objectiveTitle
            );
            RegisterOriginalText(
                "Objectives",
                "ObjectiveDescriptionTemplates",
                objectiveController.objectiveDesc
            );
        }
    }
}

