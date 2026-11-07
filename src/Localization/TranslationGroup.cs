using System;
using System.Linq;
using System.Collections.Generic;

namespace WKLocalizationDebugger.Localization
{
    public class TranslationGroup
    {
        public string GroupName;
        public List<TranslationSubgroup> TranslationSubgroups;

        public TranslationGroup(string groupName)
        {
            GroupName = groupName;
            TranslationSubgroups = [];
        }

        public TranslationSubgroup CreateOrGetTranslationSubgroup(
            string subgroupName
        )
        {
            if (
                !TryGetTranslationSubgroup(
                    subgroupName,
                    out TranslationSubgroup translationSubgroup
                )
            )
            {
                translationSubgroup = new TranslationSubgroup(subgroupName);
                TranslationSubgroups ??= [];
                TranslationSubgroups.Add(translationSubgroup);
            }
            return translationSubgroup;
        }

        public bool TryGetTranslationSubgroup(
            string subgroupName,
            out TranslationSubgroup translationSubgroup
        )
        {
            translationSubgroup = null;
            if (
                TranslationSubgroups is null
                || TranslationSubgroups.Count == 0
            )
            {
                return false;
            }
            var subgroupIndex = TranslationSubgroups
                .FindIndex(s => s != null && s.SubgroupName == subgroupName);
            if (subgroupIndex < 0)
            {
                return false;
            }
            translationSubgroup = TranslationSubgroups[subgroupIndex];
            return true;
        }

        public void RegisterOriginalText(
            string subgroupName,
            string key,
            string originalText
        )
        {
            var translationSubgroup =
                CreateOrGetTranslationSubgroup(subgroupName);
            translationSubgroup.RegisterOriginalText(key, originalText);
        }

        public void RegisterTranslatedText(
            string subgroupName,
            string key,
            string translatedText
        )
        {
            var translationSubgroup =
                CreateOrGetTranslationSubgroup(subgroupName);
            translationSubgroup.RegisterTranslatedText(key, translatedText);
        }

        public void RegisterTranslation(
            string subgroupName,
            Translation translation
        )
        {
            if (translation is null) return;
            var translationSubgroup =
                CreateOrGetTranslationSubgroup(subgroupName);
            translationSubgroup.RegisterTranslation(translation);
        }
    }
}

