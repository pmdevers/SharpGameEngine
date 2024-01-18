using System;
using System.Collections.Generic;
using System.Text;

namespace SharpEngine.Core
{
    public static class LoggerExtensions
    {

        public static void Assert(this ILogger log, bool assert, string message)
        {
#if DEBUG
            if (assert)
            {
                log.Warn(message);
            }
#endif
        }

        public static void Trace(this ILogger log, params object[] args)
        {
#if DEBUG
            log.Log(LogLevel.Info, args);
#endif
        }

        //public static void Info(this ILogger log, params object[] args)
        //{
        //    log.Log(LogLevel.Info, args);
        //}

        public static void Info(this ILogger log, string message, params object[] args)
        {
            log.Log(LogLevel.Info, string.Format(message, args));
        }

        public static void Warn(this ILogger log, params object[] args)
        {
            log.Log(LogLevel.Warn, args);
        }

        public static void Error(this ILogger log, params object[] args)
        {
            log.Log(LogLevel.Error, args);
        }

        public static void Fatal(this ILogger log, params object[] args)
        {
            log.Log(LogLevel.Fatal, args);
        }

        public static void Debug(this ILogger log, params object[] args)
        {
            log.Log(LogLevel.Debug, args);
        }
    }

    public enum LogLevel
    {
        Info,
        Debug,
        Warn,
        Error,
        Fatal
    }
    public interface ILogger
    {
        void Log(LogLevel level, params object[] args);
    }
    public class Logger : ILogger
    {
        private readonly string name;

        public Logger(string name)
        {
            this.name = name;
        }

        public void Log(LogLevel level, params object[] args)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var arg in args)
            {
                builder.Append(arg.ToString());
            }

            Log(level, builder.ToString());
        }

        private void Log(LogLevel level, string message)
        {
            var consoleColors = GetLogLevelColors(level);
            var m = FormatMessage(level, message);
            WriteWithColor(m, true, consoleColors.Background, consoleColors.Forground);
        }

        private string FormatMessage(LogLevel level, string message)
        {
            var abbr = GetLogLevelString(level);
            return $"[{DateTime.Now}] {name} {abbr}: {message}";

        }

        private string GetLogLevelString(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Info:
                    return "INFO";
                case LogLevel.Debug:
                    return "DBUG";
                case LogLevel.Warn:
                    return "WARN";
                case LogLevel.Error:
                    return "ERRR";
                case LogLevel.Fatal:
                    return "FATL";
                default:
                    return "";
            }
        }

        private void WriteWithColor(string message, bool newline, ConsoleColor backgroundColor, ConsoleColor foregroundColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            if (newline)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.Write(message, backgroundColor, foregroundColor);
            }
            Console.ResetColor();
        }

        private ConsoleColors GetLogLevelColors(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Info:
                    return new ConsoleColors(ConsoleColor.Black, ConsoleColor.Blue);
                case LogLevel.Debug:
                    return new ConsoleColors(ConsoleColor.Black, ConsoleColor.White);
                case LogLevel.Warn:
                    return new ConsoleColors(ConsoleColor.Black, ConsoleColor.Yellow);
                case LogLevel.Error:
                    return new ConsoleColors(ConsoleColor.Black, ConsoleColor.Red);
                case LogLevel.Fatal:
                    return new ConsoleColors(ConsoleColor.Red, ConsoleColor.White);
                default:
                    return new ConsoleColors(ConsoleColor.Black, ConsoleColor.White);
            }
        }




        public struct ConsoleColors
        {
            public ConsoleColors(ConsoleColor background, ConsoleColor foreground)
            {
                Background = background;
                Forground = foreground;
            }
            public ConsoleColor Background { get; }
            public ConsoleColor Forground { get; }
        }
    }
}
