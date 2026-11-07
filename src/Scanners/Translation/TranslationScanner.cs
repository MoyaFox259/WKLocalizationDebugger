using System;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger.Scanners
{
    public abstract class TranslationScanner : ObjectScanner
    {
        public TranslationCatalog Catalog;

        public TranslationScanner(TranslationCatalog catalog)
        {
            Catalog = catalog;
        }

        public virtual void RegisterOriginalText(
            string groupName,
            string subgroupName,
            string originalText
        )
        {
            if (originalText is null) return;
            RegisterOriginalText(
                groupName,
                subgroupName,
                originalText,
                originalText
            );
        }

        public virtual void RegisterOriginalText(
            string groupName,
            string subgroupName,
            string key,
            string originalText
        )
        {
            if (Catalog is null) return;
            Catalog.RegisterOriginalText(
                groupName,
                subgroupName,
                key,
                originalText
            );
        }

        public virtual void RegisterTranslatedText(
            string groupName,
            string subgroupName,
            string key,
            string translatedText
        )
        {
            if (Catalog is null) return;
            Catalog.RegisterTranslatedText(
                groupName,
                subgroupName,
                key,
                translatedText
            );
        }

        public virtual void RegisterTranslation(
            string groupName,
            string subgroupName,
            string key,
            string originalText,
            string translatedText
        )
        {
            if (Catalog is null) return;
            var translation =
                new Translation(key, originalText, translatedText);
            Catalog.RegisterTranslation(groupName, subgroupName, translation);
        }
    }
}

