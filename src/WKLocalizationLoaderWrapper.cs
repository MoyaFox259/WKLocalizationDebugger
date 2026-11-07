using System;
using System.Reflection;
using BepInEx;
using BepInEx.Bootstrap;

namespace WKLocalizationDebugger
{
    public static class WKLocalizationLoaderWrapper
    {
        public const string PLUGIN_GUID =
            "mimimi-turret.wk-localization-loader";
        public static Assembly PluginAssembly;

        public static bool TryGetAssembly(out Assembly assembly)
        {
            if (PluginAssembly != null)
            {
                assembly = PluginAssembly;
                return true;
            }
            if (
                !Chainloader.PluginInfos.TryGetValue(
                    PLUGIN_GUID,
                    out PluginInfo pluginInfo
                )
            )
            {
                assembly = null;
                return false;
            }
            assembly = pluginInfo?.Instance?.GetType()?.Assembly;
            return assembly != null;
        }

        public static bool TryGetModuleClass(
            string moduleName,
            out Type moduleClass
        )
        {
            moduleClass = null;
            if (TryGetAssembly(out Assembly assembly))
            {
                moduleClass = assembly.GetType(
                    "WKLocalizationLoader.Modules." + moduleName,
                    throwOnError: false
                );
            }
            return moduleClass != null;
        }

        public static void ToggleModule(string moduleName)
        {
            if (TryGetModuleClass(moduleName, out Type moduleClass))
            {
                var fieldFlags = BindingFlags.Public
                    | BindingFlags.Static
                    | BindingFlags.FlattenHierarchy;
                var isEnabledField =
                    moduleClass.GetField("IsEnabled", fieldFlags);
                if (
                    isEnabledField != null
                    && isEnabledField.FieldType == typeof(bool)
                )
                {
                    var isEnabled = (bool)isEnabledField.GetValue(null);
                    isEnabledField.SetValue(null, !isEnabled);
                }
            }
        }
    }
}

