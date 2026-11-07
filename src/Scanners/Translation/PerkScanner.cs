using System;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger.Scanners
{
    public class PerkScanner : TranslationScanner
    {
        public PerkScanner(TranslationCatalog catalog) : base(catalog)
        {
        }

        public override void Scan()
        {
            var database = GetFullAssetDatabase();
            database?.perkAssets?.ForEach(i => HandlePerk(i));
        }

        public void HandlePerk(Perk perk)
        {
            if (perk is null) return;
            RegisterOriginalText(
                "Perks",
                "PerkTitles",
                perk.title
            );
            RegisterOriginalText(
                "Perks",
                "PerkDescriptions",
                perk.description
            );
            RegisterOriginalText(
                "Perks",
                "PerkFlavorTexts",
                perk.flavorText
            );
            perk.modules?.ForEach(i => HandlePerkModule(i));
        }

        public void HandlePerkModule(PerkModule perkModule)
        {
            if (perkModule is PerkModule_MassProximityEffect mp)
            {
                RegisterOriginalText(
                    "TextScrawls",
                    "ScrawlTextTemplates",
                    mp.enterProximityText
                );
            }
        }
    }
}

