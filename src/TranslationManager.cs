using System;
using System.Linq;
using System.Collections.Generic;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger
{
    public static class TranslationManager
    {
        public static TranslationCatalog Catalog { get; internal set; }
        public static TranslationExporter Exporter { get; internal set; }

        public static void Initialize()
        {
            Catalog = new();
            Exporter = new();
        }

        public static bool TryGetTranslationGroup(
            string groupName,
            out TranslationGroup translationGroup
        )
        {
            translationGroup = null;
            return (
                Catalog != null
                && Catalog.TryGetTranslationGroup(
                    groupName,
                    out translationGroup
                )
            );
        }

        public static bool TryGetTranslationSubgroup(
            string groupName,
            string subgroupName,
            out TranslationSubgroup translationSubgroup
        )
        {
            translationSubgroup = null;
            return (
                TryGetTranslationGroup(
                    groupName,
                    out TranslationGroup translationGroup
                )
                && translationGroup.TryGetTranslationSubgroup(
                    subgroupName,
                    out translationSubgroup
                )
            );
        }

        public static bool TryGetTranslations(
            string groupName,
            string subgroupName,
            out List<Translation> translations
        )
        {
            translations = null;
            if (
                TryGetTranslationSubgroup(
                    groupName,
                    subgroupName,
                    out TranslationSubgroup translationSubgroup
                )
            )
            {
                translations = translationSubgroup.Translations;
            }
            return translations != null;
        }

        public static void ExportAllTranslations(string exportFolder) =>
            Exporter?.ExportTranslationCatalog(Catalog, exportFolder);

        public static void ExportTranslationGroup(
            string groupName,
            string exportFolder
        )
        {
            if (
                Exporter != null
                && TryGetTranslationGroup(
                    groupName,
                    out TranslationGroup translationGroup
                )
            )
            {
                Exporter.ExportTranslationGroup(
                    translationGroup,
                    exportFolder
                );
            }
        }
    }
}

