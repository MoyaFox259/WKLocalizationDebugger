using System;
using Imui.Core;
using Imui.Style;
using Imui.Controls;
using Imui.Rendering;
using WKLib.Core.UI;

namespace WKLocalizationDebugger.IMGUI.Controls
{
    public static class TextSizeSlider
    {
        public static string TextSizeLabel = "TextSize";
        public static string ResetLabel = "R";
        public static string ValueFormat = "0.0";
        public static float LabelWidthScale = 0.4f;
        public static float ValueWidthScale = 0.2f;
        public static float DefaultTextSize = 16f;
        public static float CurrentTextSize = DefaultTextSize;
        public static float MinTextSize = 4f;
        public static float MaxTextSize = 48f;
        public static float ValueStep = 0.1f;

        public static void Draw(ImGui gui)
        {
            var resetTextSize = false;
            var updateTextSize = false;
            gui.AddSpacing();
            gui.BeginHorizontal();
            var rowWidth = gui.GetLayoutWidth();
            var rowHeight = gui.GetRowHeight();
            var labelWidth = rowWidth * LabelWidthScale;
            var labelRect = gui.AddLayoutRect(labelWidth, rowHeight);
            gui.Text(
                TextSizeLabel,
                labelRect,
                false,
                ImTextOverflow.Ellipsis
            );
            var resetButtonRect = gui.AddLayoutRect(rowHeight, rowHeight);
            resetTextSize |= gui.Button(ResetLabel, resetButtonRect);
            var valueWidth = rowWidth * ValueWidthScale;
            var sliderWidth = rowWidth - labelWidth - rowHeight - valueWidth;
            var sliderRect = gui.AddLayoutRect(sliderWidth, rowHeight);
            updateTextSize |= gui.Slider(
                ref CurrentTextSize,
                MinTextSize,
                MaxTextSize,
                sliderRect,
                ValueStep
            );
            var valueRect = gui.AddLayoutRect(valueWidth, rowHeight);
            updateTextSize |= ImNumericEdit.NumericEdit(
                gui,
                ref CurrentTextSize,
                valueRect,
                ValueFormat,
                ValueStep,
                MinTextSize,
                MaxTextSize
            );
            gui.EndHorizontal();
            if (resetTextSize)
            {
                ResetTextSize(gui);
                return;
            }
            if (updateTextSize)
            {
                UpdateTextSize(gui);
            }
        }

        public static void UpdateTextSize(ImGui gui)
        {
            if (RootPanel.instance?.ThemeController is null) return;
            var theme = RootPanel.instance.ThemeController.BaseTheme;
            theme.TextSize = CurrentTextSize;
            gui.SetTheme(in theme);
        }

        public static void ResetTextSize(ImGui gui)
        {
            CurrentTextSize = DefaultTextSize;
            if (RootPanel.instance?.ThemeController is null) return;
            var theme = RootPanel.instance.ThemeController.BaseTheme;
            theme.TextSize = DefaultTextSize;
            gui.SetTheme(in theme);
        }
    }
}

