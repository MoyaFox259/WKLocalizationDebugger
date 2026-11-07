using System;
using System.Collections.Generic;
using Imui.Core;
using Imui.Controls;
using UnityEngine;

namespace WKLocalizationDebugger.IMGUI.Controls
{
    public static class CustomTabBar
    {
        public static Color32 ActiveTabNameColor = ColorPalette.White;

        public static bool Draw(
            ImGui gui,
            List<string> tabNames,
            ref int activeTabIndex,
            float buttonWidth = -1f
        )
        {
            if (tabNames is null || tabNames.Count == 0) return false;
            var activeTabIndexChanged = false;
            var tabCount = tabNames.Count;
            var rowWidth = gui.GetLayoutWidth();
            var rowHeight = gui.GetRowHeight();
            var textSettings =
                ImButton.CreateTextSettings(gui, gui.Style.Button);
            gui.BeginHorizontal();
            for (var tabIndex = 0; tabIndex < tabCount; tabIndex++)
            {
                var tabName = tabNames[tabIndex];
                var finalButtonWidth = buttonWidth < 0
                    ? GetDefaultTabButtonWidth(gui, tabName, textSettings)
                    : GetFixedTabButtonWidth(rowWidth, tabCount, buttonWidth);
                var buttonRect =
                    gui.AddLayoutRect(finalButtonWidth, rowHeight);
                var updateActiveTabIndex = tabIndex == activeTabIndex
                    ? ActiveTabButton(
                        gui,
                        tabName,
                        textSettings,
                        ActiveTabNameColor,
                        buttonRect
                    )
                    : InactiveTabButton(gui, tabName, buttonRect);
                if (updateActiveTabIndex)
                {
                    activeTabIndex = tabIndex;
                    activeTabIndexChanged = true;
                }
            }
            gui.EndHorizontal();
            return activeTabIndexChanged;
        }

        public static bool ActiveTabButton(
            ImGui gui,
            string tabName,
            ImTextSettings textSettings,
            Color32 tabNameColor,
            ImRect buttonRect
        )
        {
            gui.Text(tabName, textSettings, tabNameColor, buttonRect);
            return gui.InvisibleButton(buttonRect);
        }

        public static bool InactiveTabButton(
            ImGui gui,
            string tabName,
            ImRect buttonRect
        )
            => gui.Button(tabName, buttonRect);

        public static float GetDefaultTabButtonWidth(
            ImGui gui,
            string tabName,
            ImTextSettings textSettings
        )
        {
            var textSize =
                gui.MeasureTextSize(tabName, in textSettings);
            return textSize.x + gui.Style.Layout.Indent * 2;
        }

        public static float GetFixedTabButtonWidth(
            float rowWidth,
            int tabCount,
            float buttonWidth
        )
            => tabCount > 0
                ? Math.Min(buttonWidth, rowWidth / tabCount)
                : 0f;
    }
}

