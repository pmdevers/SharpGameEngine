using SharpEngine.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpEngine.Core
{


    public static class EntryPoint
    {
        public static ILogger CoreLogger { get; } = new Logger("CORE");
        public static ILogger ClientLogger { get; } = new Logger("CLIENT");

        public static void Entry(IApplication application, string[] args)
        {
            CoreLogger.Info("Initiallize SharpEngine");
            ClientLogger.Warn("Initialize Client");

            application.Run();
        }

    }
}
