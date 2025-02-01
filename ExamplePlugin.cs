using System;
using PluginFramework;

namespace ExamplePlugin
{
    public class ExamplePlugin : IPlugin
    {
        public string Name => "Example Plugin";
        public string Version => "1.0.0";

        public void OnLoad()
        {
            Logger.Log($"✅ [{Name}] تم التحميل بنجاح!");
        }

        public void OnUnload()
        {
            Logger.Log($"🛑 [{Name}] تم إلغاء تحميل الإضافة.");
        }
    }
}