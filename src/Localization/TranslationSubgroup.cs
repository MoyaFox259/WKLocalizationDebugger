using System;
using System.Linq;
using System.Collections.Generic;

namespace WKLocalizationDebugger.Localization
{
    public class TranslationSubgroup
    {
        public string SubgroupName;
        public List<Translation> Translations;
        public Dictionary<string, Translation> TranslationLookupTable;

        public TranslationSubgroup(string subgroupName)
        {
            SubgroupName = subgroupName;
            Translations = [];
        }

        public Translation CreateOrGetTranslation(string key)
        {
            if (!TryGetTranslation(key, out Translation translation))
            {
                translation = new Translation(key);
                Translations ??= [];
                Translations.Add(translation);
                if (key != null)
                {
                    TranslationLookupTable ??= [];
                    TranslationLookupTable[key] = translation;
                }
            }
            return translation;
        }

        public bool TryGetTranslation(string key, out Translation translation)
        {
            translation = null;
            if (
                key != null
                && TranslationLookupTable != null
                && TranslationLookupTable.TryGetValue(key, out translation)
                && translation != null
            )
            {
                return true;
            }
            if (Translations != null && Translations.Count > 0)
            {
                var translationIndex = Translations
                    .FindIndex(t => t != null && t.Key == key);
                if (translationIndex >= 0)
                {
                    translation = Translations[translationIndex];
                    return true;
                }
            }
            return false;
        }

        public void RebuildLookupTable()
        {
            if (Translations is null || Translations.Count == 0) return;
            TranslationLookupTable = Translations
                .Where(t => t?.Key != null)
                .ToDictionary(t => t.Key, t => t);
        }

        public void RegisterOriginalText(string key, string originalText)
        {
            var translation = CreateOrGetTranslation(key);
            translation.OriginalText = originalText;
        }

        public void RegisterTranslatedText(string key, string translatedText)
        {
            var translation = CreateOrGetTranslation(key);
            translation.TranslatedText = translatedText;
        }

        public void RegisterTranslation(Translation translation)
        {
            if (translation is null) return;
            var key = translation.Key;
            if (TryGetTranslation(key, out Translation oldTranslation))
            {
                oldTranslation.OriginalText = translation.OriginalText;
                oldTranslation.TranslatedText = translation.TranslatedText;
                return;
            }
            Translations ??= [];
            Translations.Add(translation);
            if (key is null) return;
            TranslationLookupTable ??= [];
            TranslationLookupTable[key] = translation;
        }
    }
}

