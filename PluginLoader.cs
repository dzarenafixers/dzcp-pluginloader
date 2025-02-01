using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Loader;

namespace PluginFramework
{
    public class PluginLoader
    {
        private static readonly string PluginsDirectory = "Plugins";
        private static readonly string LogsDirectory = "Logs";
        private static readonly Dictionary<string, IPlugin> LoadedPlugins = new Dictionary<string, IPlugin>();

        [PluginEntryPoint("PluginLoader", "1.0.0", "محمل إضافات مخصص لـ SCP:SL", "YourName")]
        public void Init()
        {
            try
            {
                Console.WriteLine("⚡ [PluginLoader] تم تحميل محمل الإضافات داخل SCPSL Server!");
                Logger.Log("🚀 [PluginLoader] تم بدء تشغيل محمل الإضافات!");

                InitializeDirectories();
                LoadPlugins();

                // ✅ تسجيل مستمع للحدث
                string eventName = "PluginLoaded";
                if (!string.IsNullOrEmpty(eventName))
                {
                    EventManager.RegisterListener(eventName, new PluginLoadedListener());
                    Logger.Log($"✅ [PluginLoader] تم تسجيل الحدث: {eventName}");
                }
                else
                {
                    Logger.Log("❌ [PluginLoader] لم يتم تسجيل الحدث لأن الاسم غير صالح.");
                }

                // ✅ تحفيز حدث عند تحميل `PluginLoader`
                EventManager.TriggerEvent("PluginLoaded", this);
            }
            catch (Exception ex)
            {
                Logger.Log($"❌ [PluginLoader] خطأ أثناء التشغيل: {ex.Message}");
            }
        }

        private static void InitializeDirectories()
        {
            CreateDirectory(PluginsDirectory);
            CreateDirectory(LogsDirectory);
        }

        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Logger.Log($"📂 [PluginLoader] تم إنشاء المجلد: {path}");
            }
        }

        public static void LoadPlugins()
        {
            Logger.Log("[PluginLoader] جاري البحث عن الإضافات...");
            Console.WriteLine("[PluginLoader] جاري البحث عن الإضافات...");

            var pluginFiles = Directory.GetFiles(PluginsDirectory, "*.dll");

            if (!pluginFiles.Any())
            {
                Logger.Log("[PluginLoader] لم يتم العثور على إضافات.");
                Console.WriteLine("[PluginLoader] لم يتم العثور على إضافات.");
                return;
            }

            foreach (var pluginFile in pluginFiles)
            {
                LoadPlugin(pluginFile);
            }
        }

        private static void LoadPlugin(string pluginFile)
        {
            try
            {
                Logger.Log($"🔄 [PluginLoader] جارٍ تحميل الإضافة: {pluginFile}");
                Console.WriteLine($"🔄 [PluginLoader] جارٍ تحميل الإضافة: {pluginFile}");

                var assembly = Assembly.LoadFrom(pluginFile);
                var pluginTypes = assembly.GetTypes()
                    .Where(type => typeof(IPlugin).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

                foreach (var type in pluginTypes)
                {
                    var pluginInstance = (IPlugin)Activator.CreateInstance(type);
                    pluginInstance.OnLoad();
                    LoadedPlugins[pluginInstance.Name] = pluginInstance;

                    Logger.Log($"✅ [PluginLoader] تم تحميل الإضافة: {pluginInstance.Name} - الإصدار {pluginInstance.Version}");
                    Console.WriteLine($"✅ [PluginLoader] تم تحميل الإضافة: {pluginInstance.Name} - الإصدار {pluginInstance.Version}");
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"❌ [PluginLoader] خطأ أثناء تحميل {pluginFile}: {ex.Message}");
                Console.WriteLine($"❌ [PluginLoader] خطأ أثناء تحميل {pluginFile}: {ex.Message}");
            }
        }
    }
}
