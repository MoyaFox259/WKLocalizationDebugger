using System;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger.Scanners
{
    public class AchievementScanner : TranslationScanner
    {
        public AchievementScanner(TranslationCatalog catalog) : base(catalog)
        {
        }

        public override void Scan()
        {
            Scan<AchievementCollection>(o => HandleAchievementCollection(o));
        }

        public void HandleAchievementCollection(
            AchievementCollection achievementCollection
        )
        {
            if (achievementCollection is null) return;
            achievementCollection.achievements
                ?.ForEach(i => HandleAchievement(i));
        }

        public void HandleAchievement(
            CL_AchievementManager.GameAchievement achievement
        )
        {
            if (achievement is null || !achievement.announce) return;
            RegisterOriginalText(
                "Achievements",
                "AchievementTitles",
                achievement.name
            );
            RegisterOriginalText(
                "Achievements",
                "AchievementDescriptions",
                achievement.announceText
            );
        }
    }
}

