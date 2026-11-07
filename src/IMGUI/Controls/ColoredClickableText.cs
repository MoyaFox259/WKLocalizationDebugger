using System;
using Imui.Core;
using Imui.Controls;
using Imui.Rendering;
using UnityEngine;

namespace WKLocalizationDebugger.IMGUI.Controls
{
    public static class ColoredClickableText
    {
        public static bool ClickableText(
            this ImGui gui,
            string text,
            Color32 color,
            bool clipClickableAreaWidth = true,
            bool clipClickableAreaHeight = true
        )
        {
            var textSettings = ImText.GetTextSettings(
                gui,
                false,
                ImTextOverflow.Overflow
            );
            return ClickableText(
                gui,
                text,
                color,
                clipClickableAreaWidth,
                clipClickableAreaHeight
            );
        }

        public static bool ClickableText(
            this ImGui gui,
            string text,
            ImTextSettings textSettings,
            Color32 color,
            bool clipClickableAreaWidth = true,
            bool clipClickableAreaHeight = true
        )
        {
            var textRectSize = gui.Layout.GetAvailableSize().Max(1f, 1f);
            var textRect = gui.Layout.GetRect(textRectSize);
            return ClickableText(
                gui,
                text,
                textSettings,
                color,
                textRect,
                clipClickableAreaWidth,
                clipClickableAreaHeight
            );
        }

        public static bool ClickableText(
            this ImGui gui,
            string text,
            ImTextSettings textSettings,
            Color32 color,
            ImRect textRect,
            bool clipClickableAreaWidth = true,
            bool clipClickableAreaHeight = true
        )
        {
            gui.Canvas.Text(
                text,
                color,
                textRect,
                in textSettings,
                out ImRect finalTextRect
            );
            var buttonRect = new ImRect(textRect);
            if (clipClickableAreaWidth)
            {
                buttonRect.X = finalTextRect.X;
                buttonRect.W = finalTextRect.W;
            }
            if (clipClickableAreaHeight)
            {
                buttonRect.Y = finalTextRect.Y;
                buttonRect.H = finalTextRect.H;
            }
            gui.Layout.AddRect(buttonRect);
            return gui.InvisibleButton(buttonRect);
        }
    }
}

