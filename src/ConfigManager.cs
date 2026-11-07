using System;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace WKLocalizationDebugger
{
    public static class ConfigManager
    {
        public static ConfigEntry<KeyCode> ToggleWindowKey
            { get; internal set; }
        public static ConfigEntry<string> FontFilePath
            { get; internal set; }
        public static ConfigEntry<int> FontSampleSize
            { get; internal set; }
        public static ConfigEntry<string> ExportFolderPath
            { get; internal set; }
        public static ConfigEntry<KeyCode> PreviewPreviousKey
            { get; internal set; }
        public static ConfigEntry<KeyCode> PreviewNextKey
            { get; internal set; }
        public static ConfigEntry<KeyCode> ToggleTranslationKey
            { get; internal set; }

        public static void Initialize(BaseUnityPlugin plugin)
        {
            if (plugin is null) return;
            var config = plugin.Config;
            ToggleWindowKey = config.Bind<KeyCode>(
                "UI.Window",
                "ToggleWindowKey",
                KeyCode.None,
                ""
            );
            FontFilePath = config.Bind<string>(
                "UI.Font",
                "FontFilePath",
                "",
                ""
            );
            FontSampleSize = config.Bind<int>(
                "UI.Font",
                "FontSampleSize",
                16,
                ""
            );
            ExportFolderPath = config.Bind<string>(
                "Translation.Export",
                "ExportFolderPath",
                "",
                ""
            );
            PreviewPreviousKey = config.Bind<KeyCode>(
                "Translation.Preview",
                "PreviewPreviousKey",
                KeyCode.PageUp,
                ""
            );
            PreviewNextKey = config.Bind<KeyCode>(
                "Translation.Preview",
                "PreviewNextKey",
                KeyCode.PageDown,
                ""
            );
            ToggleTranslationKey = config.Bind<KeyCode>(
                "Translation.Preview",
                "ToggleTranslationKey",
                KeyCode.None,
                ""
            );
        }
    }
}

