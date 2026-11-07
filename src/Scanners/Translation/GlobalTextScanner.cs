using System;
using UnityEngine.UI;
using TMPro;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger.Scanners
{
    public class GlobalTextScanner : TranslationScanner
    {
        public GlobalTextScanner(TranslationCatalog catalog) : base(catalog)
        {
        }

        public override void Scan()
        {
            Scan<Text>(o => HandleText(o));
            Scan<TextMeshPro>(o => HandleTextMeshPro(o));
            Scan<TextMeshProUGUI>(o => HandleTextMeshProUGUI(o));
        }

        public void HandleText(Text text)
        {
            if (text is null) return;
            RegisterOriginalText(
                "GlobalTexts(Unfiltered)",
                "From'Text'",
                text.text
            );
        }

        public void HandleTextMeshPro(TextMeshPro text)
        {
            if (text is null) return;
            RegisterOriginalText(
                "GlobalTexts(Unfiltered)",
                "From'TextMeshPro'",
                text.text
            );
        }

        public void HandleTextMeshProUGUI(TextMeshProUGUI text)
        {
            if (text is null) return;
            RegisterOriginalText(
                "GlobalTexts(Unfiltered)",
                "From'TextMeshProUGUI'",
                text.text
            );
        }
    }
}

