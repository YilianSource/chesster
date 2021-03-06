﻿using System;

namespace Chesster.Logging
{
    /// <summary>
    /// Represents a logger that outputs the messages to the console.
    /// </summary>
    public class ConsoleLogOutput : LogOutput
    {
        /// <summary>
        /// Logs the specified message to the console, using the given logger settings.
        /// </summary>
        public override void Log(LogMessage message, LoggerSettings settings)
        {
            if (settings.IncludeDate)
                Console.Write($"{Date.ToString(settings.DateFormat)} ");
            if (settings.IncludeTime)
                Console.Write($"{Date.ToString(settings.TimeFormat)} ");
            if (settings.IncludeDate || settings.IncludeTime)
                Console.Write(' ');

            WriteLogLevel(message.Level);
            Console.Write("  ");

            if (message.SenderType != null)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{FormatTypeName(message.SenderType)}: ");
                Console.ResetColor();
            }

            int left = Console.CursorLeft;
            Console.WriteLine(message.Message);

            if (!string.IsNullOrEmpty(message.AdditionalInfo))
            {
                foreach (string line in message.AdditionalInfo.Split('\n'))
                {
                    Console.CursorLeft = left;
                    Console.WriteLine(line);
                }
            }
        }

        private void WriteLogLevel(LogLevel level)
        {
            switch(level)
            {
                case LogLevel.Fatal:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Warn:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }

            Console.Write(level.ToString().PadRight(5));
            Console.ResetColor();
        }
    }
}
