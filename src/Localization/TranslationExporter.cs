using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WKLocalizationDebugger.Localization
{
    public class TranslationExporter
    {
        public JsonSerializerSettings JsonSettings;

        public TranslationExporter()
        {
            JsonSettings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new DefaultNamingStrategy()
                },
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.None,
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        public void ExportTranslationCatalog(
            TranslationCatalog translationCatalog,
            string exportFolder
        )
        {
            if (
                translationCatalog is null
                || string.IsNullOrWhiteSpace(exportFolder)
                || !Directory.Exists(exportFolder)
            )
            {
                return;
            }
            var translationGroups = translationCatalog.TranslationGroups;
            if (translationGroups is null || translationGroups.Count == 0)
            {
                return;
            }
            foreach (var translationGroup in translationGroups)
            {
                ExportTranslationGroup(translationGroup, exportFolder);
            }
        }

        public void ExportTranslationGroup(
            TranslationGroup translationGroup,
            string exportFolder
        )
        {
            if (
                translationGroup is null
                || string.IsNullOrWhiteSpace(exportFolder)
                || !Directory.Exists(exportFolder)
                || JsonSettings is null
            )
            {
                return;
            }
            var groupData = DumpTranslationGroup(translationGroup);
            if (groupData is null) return;
            var jsonText =
                JsonConvert.SerializeObject(groupData, JsonSettings);
            var fileName = translationGroup.GroupName + ".json";
            var filePath = Path.Combine(exportFolder, fileName);
            File.WriteAllText(filePath, jsonText);
        }

        public Dictionary<string, object> DumpTranslationGroup(
            TranslationGroup translationGroup
        )
        {
            if (translationGroup is null) return null;
            var translationSubgroups = translationGroup.TranslationSubgroups;
            if (
                translationSubgroups is null
                || translationSubgroups.Count == 0
            )
            {
                return null;
            }
            var groupData = new Dictionary<string, object>();
            foreach (var translationSubgroup in translationSubgroups)
            {
                var subgroupName = translationSubgroup?.SubgroupName;
                if (subgroupName is null) continue;
                var subgroupData =
                    DumpTranslationSubgroup(translationSubgroup);
                if (subgroupData is null) continue;
                groupData[subgroupName] = subgroupData;
            }
            return groupData.Count > 0 ? groupData : null;
        }

        public object DumpTranslationSubgroup(
            TranslationSubgroup translationSubgroup
        )
        {
            if (translationSubgroup is null) return null;
            var translations = translationSubgroup.Translations;
            if (translations is null || translations.Count == 0) return null;
            if (translations.Count == 1)
            {
                var translation = translations[0];
                if (translation != null && translation.Key is null)
                {
                    return DumpTranslation(translation);
                }
            }
            var subgroupData = new Dictionary<string, string>();
            foreach (var translation in translations)
            {
                var translationKey = translation?.Key;
                if (translationKey is null) continue;
                var translationData = DumpTranslation(translation);
                if (translationData is null) continue;
                subgroupData[translationKey] = translationData;
            }
            return subgroupData.Count > 0 ? subgroupData : null;
        }

        public string DumpTranslation(Translation translation) =>
            translation?.TranslatedText ?? translation?.OriginalText;
    }
}

