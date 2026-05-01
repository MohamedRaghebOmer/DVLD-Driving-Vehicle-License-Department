using DVLD.Core.Helpers;
using System;
using System.IO;

namespace DVLD.Core.Logging
{
    public static class AppLogger
    {
        private static readonly string logFilePath = Path.Combine((PathHelper.LoggingFolderPath), "log.txt");

        public static void LogError(string message, Exception ex = null)
        {
            try
            {
                if (!Directory.Exists(logFilePath))
                {
                    File.Create(logFilePath);
                }

                using (var writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now} | ERROR | {message}");

                    if (ex != null)
                    {
                        writer.WriteLine($"Exception: {ex.GetType().Name}: {ex.Message}");
                        writer.WriteLine($"StackTrace: {ex.StackTrace}");
                        if (ex.InnerException != null)
                            writer.WriteLine($"InnerException: {ex.InnerException.Message}");
                    }
                }
            }
            catch { } // Silent catch, Logger must NOT crach the program
        }
    }
}

