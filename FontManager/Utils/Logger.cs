using System;
using System.IO;

namespace FontManager.Utils
{
    public static class Logger
    {
        private static readonly string LogDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "FontManager", "Logs");
        
        private static readonly string LogFile = Path.Combine(LogDirectory, $"log_{DateTime.Now:yyyyMMdd}.txt");
        private static readonly object Lock = new object();

        static Logger()
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }
        }

        public static void Info(string message)
        {
            WriteLog("INFO", message);
        }

        public static void Warning(string message)
        {
            WriteLog("WARN", message);
        }

        public static void Error(string message, Exception ex = null)
        {
            string logMessage = message;
            if (ex != null)
            {
                logMessage += $"\n{ex.GetType().Name}: {ex.Message}\n{ex.StackTrace}";
            }
            WriteLog("ERROR", logMessage);
        }

        public static void Debug(string message)
        {
            WriteLog("DEBUG", message);
        }

        private static void WriteLog(string level, string message)
        {
            try
            {
                lock (Lock)
                {
                    string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
                    File.AppendAllText(LogFile, logEntry + Environment.NewLine);
                }
            }
            catch
            {
            }
        }

        public static string[] GetRecentLogs(int lines = 100)
        {
            try
            {
                if (File.Exists(LogFile))
                {
                    var allLines = File.ReadAllLines(LogFile);
                    int startIndex = Math.Max(0, allLines.Length - lines);
                    string[] recentLines = new string[allLines.Length - startIndex];
                    Array.Copy(allLines, startIndex, recentLines, 0, recentLines.Length);
                    return recentLines;
                }
            }
            catch
            {
            }
            return new string[0];
        }

        public static void ClearOldLogs(int daysToKeep = 30)
        {
            try
            {
                if (Directory.Exists(LogDirectory))
                {
                    var files = Directory.GetFiles(LogDirectory, "log_*.txt");
                    foreach (var file in files)
                    {
                        var fileInfo = new FileInfo(file);
                        if (fileInfo.LastWriteTime < DateTime.Now.AddDays(-daysToKeep))
                        {
                            fileInfo.Delete();
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}
