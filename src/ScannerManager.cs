using System;
using System.Collections.Generic;
using WKLocalizationDebugger.Scanners;

namespace WKLocalizationDebugger
{
    public static class ScannerManager
    {
        public static List<IScanner> Scanners { get; internal set; } = [];

        public static void Initialize()
        {
            var catalog = TranslationManager.Catalog;
            Scanners = [
                new VanillaLocalizationScanner(catalog),
                new GamemodeScanner(catalog),
                new LevelScanner(catalog),
                new CosmeticScanner(catalog),
                new TrinketScanner(catalog),
                new PerkScanner(catalog),
                new FacilityUpgradeScanner(catalog),
                new ProgressionUnlockScanner(catalog),
                new AchievementScanner(catalog),
                new ObjectiveScanner(catalog),
                new ItemScanner(catalog),
                new QuietOSScanner(catalog),
                new TextScrawlScanner(catalog),
                new UIScanner(catalog),
                new GlobalTextScanner(catalog),
                new CustomLocalizationScanner(catalog)
            ];
        }

        public static void Scan<TScanner>() where TScanner : IScanner
        {
            if (Scanners is null || Scanners.Count == 0) return;
            foreach (var scanner in Scanners)
            {
                if (scanner is TScanner)
                {
                    scanner.Scan();
                }
            }
        }

        public static void ScanAllTranslations()
        {
            Scan<TranslationScanner>();
        }
    }
}

