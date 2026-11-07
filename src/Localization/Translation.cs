using System;

namespace WKLocalizationDebugger.Localization
{
    public class Translation
    {
        public string Key;
        public string OriginalText;
        public string TranslatedText;

        public Translation(
            string key,
            string originalText = null,
            string translatedText = null
        )
        {
            Key = key;
            OriginalText = originalText;
            TranslatedText = translatedText;
        }
    }
}

