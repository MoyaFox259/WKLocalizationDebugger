using System;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger.Scanners
{
    public class CosmeticScanner : TranslationScanner
    {
        public CosmeticScanner(TranslationCatalog catalog) : base(catalog)
        {
        }

        public override void Scan()
        {
            CL_CosmeticManager.cosmeticHands
                ?.ForEach(i => HandleHandCosmetic(i));
            CL_CosmeticManager.cosmeticItems
                ?.ForEach(i => HandleItemCosmetic(i));
        }

        public void HandleHandCosmetic(Cosmetic_HandItem handCosmetic)
        {
            if (handCosmetic is null) return;
            HandleCosmeticInfo(handCosmetic.cosmeticInfo);
            handCosmetic.cosmeticData?.palettes?
                .ForEach(i => HandleHandCosmeticPalette(i));
        }

        public void HandleItemCosmetic(Cosmetic_Item itemCosmetic)
        {
            if (itemCosmetic is null) return;
            HandleCosmeticInfo(itemCosmetic.cosmeticInfo);
        }

        public void HandleCosmeticInfo(Cosmetic_Info cosmeticInfo)
        {
            if (cosmeticInfo is null) return;
            RegisterOriginalText(
                "Cosmetics",
                "CosmeticDescriptions",
                cosmeticInfo.description
            );
        }

        public void HandleHandCosmeticPalette(
            Cosmetic_HandItem.Cosmetic_HandItem_Data.ColorPalette handPalette
        )
        {
            if (handPalette is null) return;
            RegisterOriginalText(
                "Cosmetics",
                "PaletteNames",
                handPalette.title
            );
        }
    }
}

