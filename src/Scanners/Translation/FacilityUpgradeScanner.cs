using System;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger.Scanners
{
    public class FacilityUpgradeScanner : TranslationScanner
    {
        public FacilityUpgradeScanner(TranslationCatalog catalog)
            : base(catalog)
        {
        }

        public override void Scan()
        {
            Scan<FacilityUpgrade>(o => HandleFacilityUpgrade(o));
            // var database = GetFullAssetDatabase();
            // database?.facilityAssets?.ForEach(i => HandleFacility(i));
        }

        // public void HandleFacility(Facility facility)
        // {
        //     if (facility is null) return;
        //     facility.GetUpgrades().ForEach(i => HandleFacilityUpgrade(i));
        // }

        public void HandleFacilityUpgrade(FacilityUpgrade facilityUpgrade)
        {
            if (facilityUpgrade is null) return;
            RegisterOriginalText(
                "FacilityUpgrades",
                "UpgradeTitles",
                facilityUpgrade.cardName
            );
            RegisterOriginalText(
                "FacilityUpgrades",
                "UpgradeDescriptions",
                facilityUpgrade.description
            );
            RegisterOriginalText(
                "FacilityUpgrades",
                "UpgradeUnlockDescriptions",
                facilityUpgrade.unlockDesc
            );
        }
    }
}

