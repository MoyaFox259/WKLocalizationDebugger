using System;
using Imui.Core;
using Imui.Controls;
using WKLib.API.UI;
using WKLocalizationDebugger.IMGUI.Controls;

namespace WKLocalizationDebugger.IMGUI.Tabs
{
    public class SettingsTab : ITab
    {
        public string TabName { get; set; } = "Settings";

        public void Draw(ImGui gui)
        {
            DrawSectionSeparator(gui, "UI.Window");
            UIUtility.DrawConfigEntry(gui, ConfigManager.ToggleWindowKey);
            DrawSectionSeparator(gui, "UI.TextSize");
            TextSizeSlider.Draw(gui);
            DrawSectionSeparator(gui, "UI.Font");
            UIUtility.DrawConfigEntry(gui, ConfigManager.FontFilePath);
            UIUtility.DrawConfigEntry(gui, ConfigManager.FontSampleSize);
            FontLoadControls.Draw(gui);
            DrawSectionSeparator(gui, "Translation.Export");
            UIUtility.DrawConfigEntry(gui, ConfigManager.ExportFolderPath);
            DrawSectionSeparator(gui, "Translation.Preview");
            UIUtility.DrawConfigEntry(gui, ConfigManager.PreviewPreviousKey);
            UIUtility.DrawConfigEntry(gui, ConfigManager.PreviewNextKey);
            UIUtility.DrawConfigEntry(gui, ConfigManager.ToggleTranslationKey);
        }

        public void DrawSectionSeparator(ImGui gui, string section)
        {
            if (string.IsNullOrWhiteSpace(section)) return;
            gui.AddSpacing();
            gui.Separator(section);
        }
    }
}

