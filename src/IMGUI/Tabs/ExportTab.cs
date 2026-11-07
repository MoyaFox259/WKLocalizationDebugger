using System;
using Imui.Core;
using Imui.Controls;
using WKLib.API.UI;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger.IMGUI.Tabs
{
    public class ExportTab : ITab
    {
        public string TabName { get; set; } = "Export";

        public void Draw(ImGui gui)
        {
            UIUtility.DrawConfigEntry(gui, ConfigManager.ExportFolderPath);
            gui.AddSpacing();
            if (gui.Button("Export All"))
            {
                ExportAllTranslations();
            }
        }

        public void ExportAllTranslations()
        {
            var exportFolder = ConfigManager.ExportFolderPath?.Value;
            TranslationManager.ExportAllTranslations(exportFolder);
        }
    }
}

