using System;
using System.Linq;
using System.Collections.Generic;

namespace WKLocalizationDebugger.Localization
{
    public class TranslationCatalog
    {
        public List<TranslationGroup> TranslationGroups;

        public TranslationGroup CreateOrGetTranslationGroup(string groupName)
        {
            if (
                !TryGetTranslationGroup(
                    groupName,
                    out TranslationGroup translationGroup
                )
            )
            {
                translationGroup = new TranslationGroup(groupName);
                TranslationGroups ??= [];
                TranslationGroups.Add(translationGroup);
            }
            return translationGroup;
        }

        public bool TryGetTranslationGroup(
            string groupName,
            out TranslationGroup translationGroup
        )
        {
            translationGroup = null;
            if (TranslationGroups is null || TranslationGroups.Count == 0)
            {
                return false;
            }
            var groupIndex = TranslationGroups
                .FindIndex(g => g != null && g.GroupName == groupName);
            if (groupIndex < 0)
            {
                return false;
            }
            translationGroup = TranslationGroups[groupIndex];
            return true;
        }

        public void RegisterOriginalText(
            string groupName,
            string subgroupName,
            string key,
            string originalText
        )
        {
            var translationGroup = CreateOrGetTranslationGroup(groupName);
            translationGroup.RegisterOriginalText(
                subgroupName,
                key,
                originalText
            );
        }

        public void RegisterTranslatedText(
            string groupName,
            string subgroupName,
            string key,
            string translatedText
        )
        {
            var translationGroup = CreateOrGetTranslationGroup(groupName);
            translationGroup.RegisterTranslatedText(
                subgroupName,
                key,
                translatedText
            );
        }

        public void RegisterTranslation(
            string groupName,
            string subgroupName,
            Translation translation
        )
        {
            if (translation is null) return;
            var translationGroup = CreateOrGetTranslationGroup(groupName);
            translationGroup.RegisterTranslation(subgroupName, translation);
        }
    }
}

