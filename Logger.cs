using System;
using System.IO;

namespace PluginFramework
{
    public static class Logger
    {
        private static readonly string LogFilePath = "Logs/plugin_loader.log";

        static Logger()
        {
            if (!Directory.Exists("Logs"))
            {
                Directory.CreateDirectory("Logs");
            }
        }

        public static void Log(string message)
        {
            string logMessage = $"[{DateTime.Now}] {message}";
            Console.WriteLine(logMessage);
            File.AppendAllText(LogFilePath, logMessage + Environment.NewLine);
        }
    }
}