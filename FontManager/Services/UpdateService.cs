using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FontManager.Models;
using FontManager.Utils;

namespace FontManager.Services
{
    public class UpdateInfo
    {
        public FontFamily FontFamily { get; set; }
        public GitHubRelease LatestRelease { get; set; }
        public string CurrentVersion { get; set; }
        public string LatestVersion { get; set; }
        public bool HasUpdate { get; set; }
        public List<GitHubAsset> FontAssets { get; set; } = new List<GitHubAsset>();
    }

    public class UpdateProgressEventArgs : EventArgs
    {
        public string FontFamilyName { get; set; }
        public int ProgressPercentage { get; set; }
        public string StatusMessage { get; set; }
    }

    public class UpdateService
    {
        private readonly GitHubService _gitHubService;
        private readonly FontService _fontService;
        private readonly BackupService _backupService;

        public event EventHandler<UpdateProgressEventArgs> ProgressChanged;

        public UpdateService()
        {
            _gitHubService = new GitHubService();
            _fontService = new FontService();
            _backupService = new BackupService();
        }

        public async Task<List<UpdateInfo>> CheckForUpdatesAsync(List<FontFamily> fontFamilies)
        {
            var updates = new List<UpdateInfo>();
            var errors = new List<string>();

            foreach (var fontFamily in fontFamilies)
            {
                try
                {
                    var latestRelease = await _gitHubService.GetLatestReleaseAsync(fontFamily.RepoOwner, fontFamily.RepoName);
                    if (latestRelease == null)
                    {
                        errors.Add($"{fontFamily.Name}: 无法获取Release信息");
                        continue;
                    }

                    var installedFonts = _fontService.GetInstalledFontsByFamily(fontFamily.Name);
                    string currentVersion = installedFonts.FirstOrDefault()?.Version ?? "未安装";

                    var fontAssets = latestRelease.Assets.Where(a => a.IsFontFile).ToList();
                    
                    if (fontAssets.Count == 0)
                    {
                        errors.Add($"{fontFamily.Name}: Release中没有字体文件");
                    }

                    bool hasUpdate = CompareVersions(latestRelease.Version, currentVersion) > 0;

                    updates.Add(new UpdateInfo
                    {
                        FontFamily = fontFamily,
                        LatestRelease = latestRelease,
                        CurrentVersion = currentVersion,
                        LatestVersion = latestRelease.Version,
                        HasUpdate = hasUpdate,
                        FontAssets = fontAssets
                    });
                }
                catch (Exception ex)
                {
                    errors.Add($"{fontFamily.Name}: {ex.Message}");
                    Logger.Error($"Failed to check updates for {fontFamily.Name}", ex);
                }
            }

            if (errors.Count > 0)
            {
                Logger.Warning($"Update check errors:\n{string.Join("\n", errors)}");
            }

            return updates;
        }

        public async Task<bool> UpdateFontFamilyAsync(UpdateInfo updateInfo)
        {
            try
            {
                ReportProgress(updateInfo.FontFamily.Name, 0, "开始更新...");

                if (ConfigManager.Settings.CreateBackupBeforeUpdate)
                {
                    ReportProgress(updateInfo.FontFamily.Name, 10, "创建备份...");
                    var installedFonts = _fontService.GetInstalledFontsByFamily(updateInfo.FontFamily.Name);
                    foreach (var font in installedFonts)
                    {
                        _backupService.BackupFont(font.FilePath);
                    }
                }

                string downloadDir = Path.Combine(ConfigManager.Settings.DownloadDirectory, updateInfo.FontFamily.Name);
                if (!Directory.Exists(downloadDir))
                {
                    Directory.CreateDirectory(downloadDir);
                }

                int totalAssets = updateInfo.FontAssets.Count;
                int completedAssets = 0;

                foreach (var asset in updateInfo.FontAssets)
                {
                    ReportProgress(updateInfo.FontFamily.Name, 
                        20 + (int)((completedAssets / (double)totalAssets) * 60),
                        $"下载 {asset.Name}...");

                    string filePath = Path.Combine(downloadDir, asset.Name);
                    await _gitHubService.DownloadAssetToFileAsync(asset, filePath);

                    completedAssets++;
                }

                ReportProgress(updateInfo.FontFamily.Name, 80, "安装字体...");

                if (ConfigManager.Settings.AutoInstallFonts)
                {
                    _fontService.InstallFontsFromDirectory(downloadDir);
                }

                ReportProgress(updateInfo.FontFamily.Name, 100, "更新完成!");

                Logger.Info($"Updated {updateInfo.FontFamily.Name} to version {updateInfo.LatestVersion}");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to update {updateInfo.FontFamily.Name}", ex);
                ReportProgress(updateInfo.FontFamily.Name, 0, $"更新失败: {ex.Message}");
                return false;
            }
        }

        public async Task UpdateAllAsync(List<UpdateInfo> updates)
        {
            foreach (var update in updates.Where(u => u.HasUpdate))
            {
                await UpdateFontFamilyAsync(update);
            }
        }

        private int CompareVersions(string version1, string version2)
        {
            if (string.IsNullOrEmpty(version1) || version1 == "未安装")
                return string.IsNullOrEmpty(version2) || version2 == "未安装" ? 0 : -1;
            if (string.IsNullOrEmpty(version2) || version2 == "未安装")
                return 1;

            try
            {
                string v1 = version1.TrimStart('v', 'V').Trim();
                string v2 = version2.TrimStart('v', 'V').Trim();

                var v1Parts = v2.Split('.').Select(s => { int.TryParse(s, out int n); return n; }).ToArray();
                var v2Parts = v2.Split('.').Select(s => { int.TryParse(s, out int n); return n; }).ToArray();

                v1Parts = v1.Split('.').Select(s => { int.TryParse(s, out int n); return n; }).ToArray();
                v2Parts = v2.Split('.').Select(s => { int.TryParse(s, out int n); return n; }).ToArray();

                int maxLength = Math.Max(v1Parts.Length, v2Parts.Length);
                for (int i = 0; i < maxLength; i++)
                {
                    int a = i < v1Parts.Length ? v1Parts[i] : 0;
                    int b = i < v2Parts.Length ? v2Parts[i] : 0;

                    if (a > b) return 1;
                    if (a < b) return -1;
                }

                return 0;
            }
            catch
            {
                return string.Compare(version1, version2, StringComparison.OrdinalIgnoreCase);
            }
        }

        private void ReportProgress(string fontFamilyName, int percentage, string message)
        {
            ProgressChanged?.Invoke(this, new UpdateProgressEventArgs
            {
                FontFamilyName = fontFamilyName,
                ProgressPercentage = percentage,
                StatusMessage = message
            });
        }
    }
}
