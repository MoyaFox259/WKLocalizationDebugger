using System;
using Imui.Core;

namespace WKLocalizationDebugger.IMGUI.Tabs
{
    public interface ITab
    {
        public string TabName { get; set; }

        public void Draw(ImGui gui)
        {
        }
    }
}

