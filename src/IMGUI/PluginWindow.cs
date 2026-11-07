using System;
using System.Linq;
using System.Collections.Generic;
using Imui.Core;
using Imui.Controls;
using UnityEngine;
using WKLib.API.UI;
using WKLib.API.Input;
using WKLocalizationDebugger.IMGUI.Tabs;
using WKLocalizationDebugger.IMGUI.Controls;

namespace WKLocalizationDebugger.IMGUI
{
    public class PluginWindow : WKLibWindow
    {
        public string WindowName;
        public float WindowWidth = 800f;
        public float WindowHeight = 600f;
        public int ActiveTabIndex = 0;
        public List<ITab> Tabs;
        public List<string> TabNames;

        public PluginWindow()
        {
            isOpen = false;
            WindowName = Plugin.PLUGIN_NAME;
            Tabs = [
                new ScanTab(),
                new ExportTab(),
                new SettingsTab()
            ];
            TabNames = Tabs.Select(t => t.TabName).ToList();
            SetActiveTab("Settings");
        }

        public override void HandleInput(ImGui gui)
        {
            // if (
            //     ConfigManager.PreviewPreviousKey != null
            //     && InputUtility
            //         .GetKeyDown(ConfigManager.PreviewPreviousKey.Value)
            // )
            // {
            //     NotePreviewControls.PreviewPrevious();
            // }
            // if (
            //     ConfigManager.PreviewNextKey != null
            //     && InputUtility.GetKeyDown(ConfigManager.PreviewNextKey.Value)
            // )
            // {
            //     NotePreviewControls.PreviewNext();
            // }
            // if (
            //     ConfigManager.ToggleTranslationKey != null
            //     && InputUtility
            //         .GetKeyDown(ConfigManager.ToggleTranslationKey.Value)
            // )
            // {
            //     NotePreviewControls.IsOriginal = !NotePreviewControls.IsOriginal;
            //     NotePreviewControls.Preview();
            // }
            if (
                ConfigManager.ToggleWindowKey != null
                && InputUtility.GetKeyDown(ConfigManager.ToggleWindowKey.Value)
            )
            {
                ToggleOpen();
            }
            // if (isOpen)
            // {
            //     Cursor.lockState = CursorLockMode.None;
            // }
            // else if (!CL_GameManager.isDead() && !CL_GameManager.gMan.isPaused)
            // {
            //     Cursor.lockState = CursorLockMode.Locked;
            // }
            // CL_GameManager.gMan.lockPlayerInput = isOpen;
        }

        public override void Draw(ImGui gui, bool isRootPanelOpen)
        {
            if (
                !isRootPanelOpen
                || !gui.BeginWindow(
                    WindowName,
                    ref isOpen,
                    new ImSize(WindowWidth, WindowHeight),
                    ImWindowFlag.None
                )
            )
            {
                return;
            }
            DrawTabBar(gui);
            DrawTabContent(gui);
            gui.EndWindow();
        }

        public void DrawTabBar(ImGui gui)
        {
            CustomTabBar.Draw(gui, TabNames, ref ActiveTabIndex);
        }

        public void DrawTabContent(ImGui gui)
        {
            if (
                Tabs is null
                || ActiveTabIndex < 0
                || ActiveTabIndex > Tabs.Count
            )
            {
                return;
            }
            Tabs[ActiveTabIndex]?.Draw(gui);
        }

        public void ToggleOpen()
        {
            isOpen = !isOpen;
        }

        public void SetActiveTab(string tabName)
        {
            if (Tabs is null) return;
            var tabIndex = Tabs.FindIndex(t => t.TabName == tabName);
            if (tabIndex < 0) return;
            SetActiveTab(tabIndex);
        }

        public void SetActiveTab(int tabIndex)
        {
            ActiveTabIndex = tabIndex;
        }
    }
}

