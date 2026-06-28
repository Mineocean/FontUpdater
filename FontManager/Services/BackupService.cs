using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FontManager.Utils;

namespace FontManager.Services
{
    public class BackupInfo
    {
        public string FileName { get; set; }
        public string BackupPath { get; set; }
        public string OriginalPath { get; set; }
        public DateTime BackupDate { get; set; }
        public long FileSize { get; set; }

        public string FileSizeFormatted
        {
            get
            {
                string[] sizes = { "B", "KB", "MB", "GB" };
                double len = FileSize;
                int order = 0;
                while (len >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    len /= 1024;
                }
                return $"{len:0.##} {sizes[order]}";
            }
        }
    }

    public class BackupService
    {
        private static readonly string BackupDirectory = ConfigManager.Settings.BackupDirectory;

        public BackupService()
        {
            if (!Directory.Exists(BackupDirectory))
            {
                Directory.CreateDirectory(BackupDirectory);
            }
        }

        public string BackupFont(string fontFilePath)
        {
            try
            {
                if (!File.Exists(fontFilePath))
                {
                    Logger.Error($"Font file not found for backup: {fontFilePath}");
                    return null;
                }

                string fileName = Path.GetFileName(fontFilePath);
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string backupFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{timestamp}{Path.GetExtension(fileName)}";
                string backupPath = Path.Combine(BackupDirectory, backupFileName);

                File.Copy(fontFilePath, backupPath);

                Logger.Info($"Backed up font: {fileName} to {backupPath}");
                return backupPath;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to backup font {fontFilePath}", ex);
                return null;
            }
        }

        public List<BackupInfo> GetBackups()
        {
            var backups = new List<BackupInfo>();

            try
            {
                if (!Directory.Exists(BackupDirectory))
                {
                    return backups;
                }

                foreach (var file in Directory.GetFiles(BackupDirectory, "*.ttf"))
                {
                    var fileInfo = new FileInfo(file);
                    backups.Add(new BackupInfo
                    {
                        FileName = fileInfo.Name,
                        BackupPath = file,
                        OriginalPath = ExtractOriginalPath(fileInfo.Name),
                        BackupDate = fileInfo.LastWriteTime,
                        FileSize = fileInfo.Length
                    });
                }

                backups = backups.OrderByDescending(b => b.BackupDate).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to get backups", ex);
            }

            return backups;
        }

        public bool RestoreFont(string backupPath)
        {
            try
            {
                if (!File.Exists(backupPath))
                {
                    Logger.Error($"Backup file not found: {backupPath}");
                    return false;
                }

                string fileName = Path.GetFileName(backupPath);
                string originalPath = ExtractOriginalPath(fileName);

                if (string.IsNullOrEmpty(originalPath))
                {
                    string userFontDir = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "Microsoft", "Windows", "Fonts");
                    originalPath = Path.Combine(userFontDir, ExtractOriginalFileName(fileName));
                }

                string originalDir = Path.GetDirectoryName(originalPath);
                if (!Directory.Exists(originalDir))
                {
                    Directory.CreateDirectory(originalDir);
                }

                File.Copy(backupPath, originalPath, true);

                Logger.Info($"Restored font from {backupPath} to {originalPath}");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to restore font from {backupPath}", ex);
                return false;
            }
        }

        public bool DeleteBackup(string backupPath)
        {
            try
            {
                if (File.Exists(backupPath))
                {
                    File.Delete(backupPath);
                    Logger.Info($"Deleted backup: {backupPath}");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to delete backup {backupPath}", ex);
                return false;
            }
        }

        public void CleanupOldBackups(int daysToKeep = 30)
        {
            try
            {
                if (!Directory.Exists(BackupDirectory))
                {
                    return;
                }

                var cutoffDate = DateTime.Now.AddDays(-daysToKeep);
                var files = Directory.GetFiles(BackupDirectory, "*.ttf");

                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.LastWriteTime < cutoffDate)
                    {
                        fileInfo.Delete();
                        Logger.Info($"Deleted old backup: {fileInfo.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to cleanup old backups", ex);
            }
        }

        private string ExtractOriginalPath(string backupFileName)
        {
            return null;
        }

        private string ExtractOriginalFileName(string backupFileName)
        {
            int lastUnderscore = backupFileName.LastIndexOf('_');
            if (lastUnderscore > 0)
            {
                string withoutTimestamp = backupFileName.Substring(0, lastUnderscore);
                string extension = Path.GetExtension(backupFileName);
                return withoutTimestamp + extension;
            }
            return backupFileName;
        }
    }
}
