using System;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger.Scanners
{
    public class LevelScanner : TranslationScanner
    {
        public LevelScanner(TranslationCatalog catalog) : base(catalog)
        {
        }

        public override void Scan()
        {
            var database = GetFullAssetDatabase();
            database?.regionAssets?.ForEach(i => HandleRegion(i));
            database?.subRegionAssets?.ForEach(i => HandleSubregion(i));
            database?.levelAssets?.ForEach(i => HandleLevelAssetHolder(i));
            Scan<UT_ZoneTitler>(o => HandleZoneTitler(o));
        }

        public void HandleRegion(M_Region region)
        {
            if (region is null) return;
            RegisterOriginalText(
                "LocationNames",
                "RegionIntroTexts",
                region.introText
            );
        }

        public void HandleSubregion(M_Subregion subregion)
        {
            if (subregion is null) return;
            RegisterOriginalText(
                "LocationNames",
                "SubregionIntroTexts",
                subregion.introText
            );
        }

        public void HandleLevelAssetHolder(
            M_Level.LevelAssetHolder levelAssetHolder
        )
        {
            if (levelAssetHolder is null) return;
            HandleLevel(levelAssetHolder.level);
        }

        public void HandleLevel(M_Level level)
        {
            if (level is null) return;
            RegisterOriginalText(
                "LocationNames",
                "LevelIntroTexts",
                level.introText
            );
            RegisterOriginalText(
                "LocationNames",
                "LevelSaveNames",
                level.saveName
            );
        }

        public void HandleZoneTitler(UT_ZoneTitler zoneTitler)
        {
            if (zoneTitler is null) return;
            RegisterOriginalText(
                "LocationNames",
                "RegionIntroTexts",
                zoneTitler.region
            );
            RegisterOriginalText(
                "LocationNames",
                "SubregionIntroTexts",
                zoneTitler.subRegion
            );
        }
    }
}

