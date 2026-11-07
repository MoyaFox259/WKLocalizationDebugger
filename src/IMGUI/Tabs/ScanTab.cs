using System;
using Imui.Core;
using Imui.Controls;

namespace WKLocalizationDebugger.IMGUI.Tabs
{
    public class ScanTab : ITab
    {
        public string TabName { get; set; } = "Scan";

        public void Draw(ImGui gui)
        {
            if (gui.Button("Scan All"))
            {
                ScannerManager.ScanAllTranslations();
            }
        }
    }
}

