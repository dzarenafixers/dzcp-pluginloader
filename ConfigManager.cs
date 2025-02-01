using System;
using System.IO;

namespace PluginFramework
{
    public class ConfigManager
    {
        private readonly string ConfigFilePath;

        public ConfigManager(string configFileName)
        {
            ConfigFilePath = Path.Combine("Configs", configFileName);
            InitializeConfig();
        }

        private void InitializeConfig()
        {
            if (!File.Exists(ConfigFilePath))
            {
                // إنشاء ملف إعدادات افتراضي
                File.WriteAllText(ConfigFilePath, "{\n  \"Setting1\": \"DefaultValue\",\n  \"Setting2\": \"DefaultValue\"\n}");
                Console.WriteLine($"[ConfigManager] تم إنشاء ملف الإعدادات: {ConfigFilePath}");
            }
        }
    }
}