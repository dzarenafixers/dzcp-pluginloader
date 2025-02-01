using System;

namespace PluginFramework
{
    public static class ConsoleBanner
    {
        public static void Show()
        {
            Console.WriteLine(@"
 ██████╗ ██╗      ██████╗  ██████╗ ███████╗
██╔═══██╗██║      ██╔══██╗██╔═══██╗██╔════╝
██║   ██║██║      ██████╔╝██║   ██║███████╗
██║   ██║██║      ██╔══██╗██║   ██║╚════██║
╚██████╔╝███████╗██║  ██║╚██████╔╝███████║
 ╚═════╝ ╚══════╝╚═╝  ╚═╝ ╚═════╝ ╚══════╝
        ");
            Console.WriteLine("⚡ [PluginFramework] تم بدء تشغيل محمل الإضافات!");
        }
    }
}