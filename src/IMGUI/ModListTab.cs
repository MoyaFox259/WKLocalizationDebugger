using System;
using Imui.Core;
using Imui.Controls;
using WKLib.API.UI;

namespace WKLocalizationDebugger.IMGUI
{
    public class ModListTab : ModTab
    {
        public string PluginName;
        public string OpenWindowLabel;
        public string CloseWindowLabel;
        public WKLibWindow Window;

        public ModListTab(string pluginName, WKLibWindow window)
        {
            PluginName = pluginName;
            OpenWindowLabel = "Open window";
            CloseWindowLabel = "Close window";
            Window = window;
        }

        public override string DisplayName => PluginName;

        public override void DrawSubMenu(ImGui gui)
        {
            if (Window is null) return;
            var buttonText = Window.isOpen
                ? CloseWindowLabel
                : OpenWindowLabel;
            if (gui.Button(buttonText))
            {
                Window.isOpen = !Window.isOpen;
            }
        }
    }
}

