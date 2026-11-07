using System;
using BepInEx;
using BepInEx.Logging;
using ImuiBepInEx;
using WKLib;
using WKLib.API;
using WKLib.API.UI;
using WKLocalizationDebugger.IMGUI;

namespace WKLocalizationDebugger
{
    [BepInDependency(
        ImuiBepInEx.Plugin.GUID,
        BepInDependency.DependencyFlags.HardDependency
    )]
    [BepInDependency(
        WKLibPlugin.GUID,
        BepInDependency.DependencyFlags.HardDependency
    )]
    [BepInDependency(
        WKLocalizationLoaderWrapper.PLUGIN_GUID,
        BepInDependency.DependencyFlags.SoftDependency
    )]
    [BepInPlugin(
        PLUGIN_GUID,
        PLUGIN_NAME,
        PLUGIN_VERSION
    )]
    public class Plugin : BaseUnityPlugin
    {
        public const string PLUGIN_GUID =
            "mimimi-turret.wk-localization-debugger";
        public const string PLUGIN_NAME = "WKLocalizationDebugger";
        public const string PLUGIN_VERSION = "0.1.0";

        public static WKLibAPI WKLibAPI =
            WKLibAPI.Create(PLUGIN_NAME, PLUGIN_GUID);
        public static WKLibWindow Window = new PluginWindow();
        public static ModTab Tab = new ModListTab(PLUGIN_NAME, Window);
        public static new ManualLogSource Logger;

        private void Awake()
        {
            Logger = base.Logger;
            ConfigManager.Initialize(this);
            TranslationManager.Initialize();
            ScannerManager.Initialize();
            WKLibAPI.AddWindow(Window);
            WKLibAPI.AddToModList(Tab);
        }
    }
}

