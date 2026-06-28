using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace FontManager.Utils
{
    public class AppSettings
    {
        public string DownloadDirectory { get; set; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "FontManager", "Downloads");
        
        public string BackupDirectory { get; set; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "FontManager", "Backups");
        
        public bool AutoCheckUpdates { get; set; } = false;
        public bool AutoInstallFonts { get; set; } = true;
        public bool CreateBackupBeforeUpdate { get; set; } = true;
        public List<string> EnabledFontFamilies { get; set; } = new List<string>();
        public int MaxConcurrentDownloads { get; set; } = 3;
        public bool MinimizeToTray { get; set; } = false;
        public string ProxyAddress { get; set; } = "";
        public int ProxyPort { get; set; } = 0;
        public DateTime LastUpdateCheck { get; set; } = DateTime.MinValue;
    }

    public static class ConfigManager
    {
        private static readonly string ConfigDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "FontManager");
        
        private static readonly string ConfigFile = Path.Combine(ConfigDirectory, "settings.json");
        
        private static AppSettings _settings;
        private static readonly object Lock = new object();

        public static AppSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    Load();
                }
                return _settings;
            }
        }

        public static void Load()
        {
            try
            {
                lock (Lock)
                {
                    if (File.Exists(ConfigFile))
                    {
                        string json = File.ReadAllText(ConfigFile);
                        _settings = JsonConvert.DeserializeObject<AppSettings>(json);
                    }
                    else
                    {
                        _settings = new AppSettings();
                        Save();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to load configuration", ex);
                _settings = new AppSettings();
            }
        }

        public static void Save()
        {
            try
            {
                lock (Lock)
                {
                    if (!Directory.Exists(ConfigDirectory))
                    {
                        Directory.CreateDirectory(ConfigDirectory);
                    }

                    string json = JsonConvert.SerializeObject(_settings, Formatting.Indented);
                    File.WriteAllText(ConfigFile, json);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to save configuration", ex);
            }
        }

        public static void Reset()
        {
            _settings = new AppSettings();
            Save();
        }
    }
}
