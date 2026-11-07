using System;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger.Scanners
{
    public class TrinketScanner : TranslationScanner
    {
        public TrinketScanner(TranslationCatalog catalog) : base(catalog)
        {
        }

        public override void Scan()
        {
            Scan<Trinket>(o => HandleTrinket(o));
            // var database = GetFullAssetDatabase();
            // database?.trinketAssets?.ForEach(i => HandleTrinket(i));
        }

        public void HandleTrinket(Trinket trinket)
        {
            if (trinket is null) return;
            RegisterOriginalText(
                "Trinkets",
                "TrinketTitles",
                trinket.title
            );
            RegisterOriginalText(
                "Trinkets",
                "TrinketDescriptions",
                trinket.description
            );
            RegisterOriginalText(
                "Trinkets",
                "TrinketFlavorTexts",
                trinket.flavorText
            );
        }
    }
}

