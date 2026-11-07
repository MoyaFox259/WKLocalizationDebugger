using System;
using System.Linq;
using System.Collections.Generic;
using Imui.Core;
using Imui.Controls;
using Imui.Rendering;
using UnityEngine;

namespace WKLocalizationDebugger.IMGUI.Controls
{
    public static class ColoredTextsTooltip
    {
        public static void TooltipAtLastControl(
            this ImGui gui,
            List<(string Text, Color32 Color)> coloredTexts,
            ImTooltipShow show = ImTooltipShow.OnHover
        )
        {
            if (!ShouldShowTooltipAtLastControl(gui, show)) return;
            Tooltip(gui, coloredTexts);
        }

        public static void TooltipAtControl(
            this ImGui gui,
            uint control,
            List<(string Text, Color32 Color)> coloredTexts,
            ImTooltipShow show = ImTooltipShow.OnHover
        )
        {
            if (!ShouldShowTooltip(gui, control, show)) return;
            Tooltip(gui, coloredTexts);
        }

        public static void Tooltip(
            this ImGui gui,
            List<(string Text, Color32 Color)> coloredTexts
        )
        {
            if (coloredTexts is null || coloredTexts.Count == 0) return;
            var textSettings = new ImTextSettings(
                gui.Style.Layout.TextSize,
                0f,
                0f,
                false,
                ImTextOverflow.Overflow
            );
            var boxRect = GetTooltipBoxRect(
                gui,
                coloredTexts.Select(t => t.Text).ToList(),
                textSettings,
                out List<ImRect> textRects
            );
            gui.BeginPopup();
            gui.Box(boxRect, gui.Style.Tooltip.Box);
            for (
                var textIndex = 0;
                textIndex < coloredTexts.Count;
                textIndex++
            )
            {
                var text = coloredTexts[textIndex].Text;
                var color = coloredTexts[textIndex].Color;
                var textRect = textRects[textIndex];
                gui.Text(text, textSettings, color, textRect);
            }
            gui.EndPopup();
        }

        public static bool ShouldShowTooltipAtLastControl(
            this ImGui gui,
            ImTooltipShow show = ImTooltipShow.OnHover
        )
            => ShouldShowTooltip(gui, gui.LastControl, show);

        public static bool ShouldShowTooltip(
            this ImGui gui,
            uint control,
            ImTooltipShow show = ImTooltipShow.OnHover
        )
            => (
                (show & ImTooltipShow.OnHover) != 0
                && gui.IsControlHovered(control)
            )
            ||(
                (show & ImTooltipShow.OnActive) != 0
                && gui.IsControlActive(control)
            );

        public static ImRect GetTooltipBoxRect(
            ImGui gui,
            List<string> texts,
            ImTextSettings textSettings,
            out List<ImRect> textRects
        )
        {
            var calculateTexts = texts != null && texts.Count > 0;
            var basePosition = gui.Input.MousePosition
                + gui.Style.Tooltip.OffsetPixels / gui.Canvas.ScreenScale;
            var boxHorizontalPadding = gui.Style.Tooltip.Padding.Horizontal;
            var boxVerticalPadding = gui.Style.Tooltip.Padding.Vertical;
            var boxWidth = boxHorizontalPadding;
            var boxHeight = boxVerticalPadding;
            var textRectSizes = new List<Vector2>();
            if (calculateTexts)
            {
                textRectSizes = texts
                    .Select(t => gui.MeasureTextSize(t, textSettings))
                    .ToList();
                boxWidth += textRectSizes.Max(v => v.x);
                boxHeight += textRectSizes.Sum(v => v.y);
            }
            var boxX = basePosition.x;
            var boxY = gui.Style.Tooltip.AboveCursor
                ? basePosition.y
                : basePosition.y - boxHeight;
            textRects = new List<ImRect>();
            if (calculateTexts)
            {
                var textX = boxX + boxHorizontalPadding / 2;
                var textY = boxY + boxHeight - boxVerticalPadding / 2;
                textRects = CalculateTooltipTextRects(
                    textX,
                    textY,
                    textRectSizes
                );
            }
            return new ImRect(boxX, boxY, boxWidth, boxHeight);
        }

        public static List<ImRect> CalculateTooltipTextRects(
            float textX,
            float textY,
            List<Vector2> textRectSizes
        )
        {
            var textRects = new List<ImRect>();
            if (textRectSizes != null && textRectSizes.Count > 0)
            {
                for (
                    var sizeIndex = 0;
                    sizeIndex < textRectSizes.Count;
                    sizeIndex++
                )
                {
                    var textRectSize = textRectSizes[sizeIndex];
                    textY -= textRectSize.y;
                    var textRect = new ImRect(
                        textX,
                        textY,
                        textRectSize.x,
                        textRectSize.y
                    );
                    textRects.Add(textRect);
                }
            }
            return textRects;
        }
    }
}

