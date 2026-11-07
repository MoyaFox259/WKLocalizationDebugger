using System;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger.Scanners
{
    public class CustomLocalizationScanner : TranslationScanner
    {
        public CustomLocalizationScanner(TranslationCatalog catalog)
            : base(catalog)
        {
        }

        public override void Scan()
        {
            ScanModule("AchievementPatch", "Achievements");
            ScanModule("AnnouncementSubtitlePatch", "AnnouncementSubtitles");
            ScanModule("ClosedCaptionSubtitlePatch", "ClosedCaptionSubtitles");
            ScanModule("CosmeticPatch", "Cosmetics");
            ScanModule("DeathTextPatch", "DeathTexts");
            ScanModule("DocumentPatch", "Documents");
            ScanModule("FacilityUpgradePatch", "FacilityUpgrades");
            ScanModule("GamemodePatch", "Gamemodes");
            ScanModule("GameplayTextPatch", "GameplayTexts");
            ScanModule("ItemDescriptionPatch", "ItemDescriptions");
            ScanModule("LocationNamePatch", "LocationNames");
            ScanModule("MainMenuPatch", "MainMenu");
            ScanModule("MotherSubtitlePatch", "MotherSubtitles");
            ScanModule("NotePatch", "Notes");
            ScanModule("ObjectivePatch", "Objectives");
            ScanModule("PerkPatch", "Perks");
            ScanModule("ProgressionUnlockPatch", "ProgressionUnlocks");
            ScanModule("QuietOSPatch", "QuietOS");
            ScanModule("RecordingSubtitlePatch", "RecordingSubtitles");
            ScanModule("RoachTraderSubtitlePatch", "RoachTraderSubtitles");
            ScanModule("ScoreScreenPatch", "ScoreScreen");
            ScanModule("StaticTextPatch", "StaticTexts");
            ScanModule("TextScrawlPatch", "TextScrawls");
            ScanModule("TrinketPatch", "Trinkets");
        }

        public void ScanModule(string moduleName, string groupName)
        {
            if (
                WKLocalizationLoaderWrapper.TryGetModuleClass(
                    moduleName,
                    out Type moduleClass
                )
            )
            {
                ScanType(moduleClass, groupName);
            }
        }

        public void ScanType(Type type, string groupName)
        {
            if (type is null) return;
            var fieldFlags = BindingFlags.Public
                | BindingFlags.Static
                | BindingFlags.FlattenHierarchy;
            var staticFields = type.GetFields(fieldFlags);
            if (staticFields is null) return;
            Array.ForEach(staticFields, i => HandleStaticField(i, groupName));
        }

        public void HandleStaticField(
            FieldInfo fieldInfo,
            string groupName
        )
        {
            if (fieldInfo is null) return;
            var jsonIgnoreAttribute =
                fieldInfo.GetCustomAttribute<JsonIgnoreAttribute>();
            if (jsonIgnoreAttribute != null) return;
            var jsonPropertyAttribute =
                fieldInfo.GetCustomAttribute<JsonPropertyAttribute>();
            if (jsonPropertyAttribute is null) return;
            if (fieldInfo.FieldType == typeof(string))
            {
                HandleStaticStringField(fieldInfo, groupName);
                return;
            }
            if (fieldInfo.FieldType == typeof(Dictionary<string, string>))
            {
                HandleStaticDictionaryField(fieldInfo, groupName);
            }
        }

        public void HandleStaticStringField(
            FieldInfo fieldInfo,
            string groupName
        )
        {
            if (fieldInfo?.FieldType != typeof(string)) return;
            var translatedText = fieldInfo.GetValue(null) as string;
            if (translatedText is null) return;
            RegisterTranslatedText(
                groupName,
                fieldInfo.Name,
                null,
                translatedText
            );
        }

        public void HandleStaticDictionaryField(
            FieldInfo fieldInfo,
            string groupName
        )
        {
            if (fieldInfo?.FieldType != typeof(Dictionary<string, string>))
            {
                return;
            }
            var translations =
                fieldInfo.GetValue(null) as Dictionary<string, string>;
            if (translations is null || translations.Count == 0) return;
            foreach (var translation in translations)
            {
                RegisterTranslatedText(
                    groupName,
                    fieldInfo.Name,
                    translation.Key,
                    translation.Value
                );
            }
        }
    }
}

