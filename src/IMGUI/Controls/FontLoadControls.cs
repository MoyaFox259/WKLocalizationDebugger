using System;
using System.IO;
using Imui.Core;
using Imui.Controls;
using Imui.Rendering;
using UnityEngine;

namespace WKLocalizationDebugger.IMGUI.Controls
{
    public static class FontLoadControls
    {
        public static string UnloadFontLabel = "Unload Font";
        public static string LoadFontLabel = "Load Font";
        public static float LeftSpacingScale = 0.4f;

        public static void Draw(ImGui gui)
        {
            gui.AddSpacing();
            gui.BeginHorizontal();
            var rowWidth = gui.GetLayoutWidth();
            var rowHeight = gui.GetRowHeight();
            var leftSpacing = rowWidth * LeftSpacingScale;
            gui.AddSpacing(leftSpacing);
            var buttonSpacing = gui.Style.Layout.Spacing;
            var buttonWidth = (rowWidth - leftSpacing - buttonSpacing) / 2;
            var buttonRect = gui.AddLayoutRect(buttonWidth, rowHeight);
            if (gui.Button(UnloadFontLabel, buttonRect))
            {
                UnloadFont(gui);
            }
            gui.AddSpacing(buttonSpacing);
            buttonRect = gui.AddLayoutRect(buttonWidth, rowHeight);
            if (gui.Button(LoadFontLabel, buttonRect))
            {
                LoadFont(gui);
            }
            gui.EndHorizontal();
        }

        public static void LoadFont(ImGui gui)
        {
            var filePath = ConfigManager.FontFilePath?.Value;
            if (
                string.IsNullOrWhiteSpace(filePath)
                || !File.Exists(filePath)
            )
            {
                return;
            }
            var sampleSize = ConfigManager.FontSampleSize?.Value ?? 16;
            var font = new Font(filePath);
            var imGlyphRenderMode = ImGlyphRenderMode.Sdf;
            gui.TextDrawer.LoadFont(font, sampleSize, imGlyphRenderMode);
        }

        public static void UnloadFont(ImGui gui) => gui.LoadDefaultFont();
    }
}

