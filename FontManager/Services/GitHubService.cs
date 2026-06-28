using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using FontManager.Models;
using FontManager.Utils;
using Newtonsoft.Json;

namespace FontManager.Services
{
    public class GitHubService
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        private const string ApiBase = "https://api.github.com";

        static GitHubService()
        {
            HttpClient.DefaultRequestHeaders.Add("User-Agent", "FontManager/1.0");
            HttpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
        }

        private static void UpdateAuthToken()
        {
            string token = ConfigManager.Settings.GitHubToken;
            if (!string.IsNullOrEmpty(token))
            {
                if (HttpClient.DefaultRequestHeaders.Authorization == null || 
                    HttpClient.DefaultRequestHeaders.Authorization.Parameter != token)
                {
                    HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
        }

        public static List<FontFamily> GetSupportedFontFamilies()
        {
            return new List<FontFamily>
            {
                new FontFamily { Name = "霞鹜文楷", RepoOwner = "lxgw", RepoName = "LxgwWenKai", Description = "霞鹜文楷主版本" },
                new FontFamily { Name = "霞鹜文楷轻便版", RepoOwner = "lxgw", RepoName = "LxgwWenKai-Lite", Description = "精简版，去除部分汉字" },
                new FontFamily { Name = "霞鹜文楷GB", RepoOwner = "lxgw", RepoName = "LxgwWenkaiGB", Description = "GB标准版" },
                new FontFamily { Name = "霞鹜文楷TC", RepoOwner = "lxgw", RepoName = "LxgwWenkaiTC", Description = "繁体中文版" },
                new FontFamily { Name = "霞鹜文楷屏幕阅读版", RepoOwner = "lxgw", RepoName = "LxgwWenKai-Screen", Description = "屏幕阅读优化版" },
                new FontFamily { Name = "霞鹜文楷韩文版", RepoOwner = "lxgw", RepoName = "LxgwWenkaiKR", Description = "韩文版" },
                new FontFamily { Name = "霞鹜文楷GB轻便版", RepoOwner = "lxgw", RepoName = "LxgwWenkaiGB-Lite", Description = "GB精简版" }
            };
        }

        public async Task<GitHubRelease> GetLatestReleaseAsync(string owner, string repo)
        {
            try
            {
                UpdateAuthToken();
                string url = $"{ApiBase}/repos/{owner}/{repo}/releases/latest";
                var response = await HttpClient.GetStringAsync(url);
                return JsonConvert.DeserializeObject<GitHubRelease>(response);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to get latest release for {owner}/{repo}", ex);
                throw;
            }
        }

        public async Task<List<GitHubRelease>> GetReleasesAsync(string owner, string repo, int perPage = 10)
        {
            try
            {
                string url = $"{ApiBase}/repos/{owner}/{repo}/releases?per_page={perPage}";
                var response = await HttpClient.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<GitHubRelease>>(response);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to get releases for {owner}/{repo}", ex);
                return new List<GitHubRelease>();
            }
        }

        public async Task<byte[]> DownloadAssetAsync(GitHubAsset asset)
        {
            try
            {
                return await HttpClient.GetByteArrayAsync(asset.BrowserDownloadUrl);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to download asset {asset.Name}", ex);
                return null;
            }
        }

        public async Task DownloadAssetToFileAsync(GitHubAsset asset, string filePath)
        {
            try
            {
                UpdateAuthToken();
                using (var response = await HttpClient.GetAsync(asset.BrowserDownloadUrl, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                    {
                        await stream.CopyToAsync(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to download asset {asset.Name} to file", ex);
                throw;
            }
        }

        public async Task<List<string>> DownloadAssetsParallelAsync(List<GitHubAsset> assets, string directory, 
            int maxConcurrency = 3, IProgress<int> progress = null)
        {
            var downloadedFiles = new List<string>();
            var semaphore = new SemaphoreSlim(maxConcurrency);
            var completedCount = 0;

            var tasks = assets.Select(async asset =>
            {
                await semaphore.WaitAsync();
                try
                {
                    string filePath = Path.Combine(directory, asset.Name);
                    await DownloadAssetToFileAsync(asset, filePath);
                    downloadedFiles.Add(filePath);
                    
                    int current = Interlocked.Increment(ref completedCount);
                    progress?.Report((int)((current / (double)assets.Count) * 100));
                }
                catch (Exception ex)
                {
                    Logger.Error($"Failed to download {asset.Name}", ex);
                }
                finally
                {
                    semaphore.Release();
                }
            });

            await Task.WhenAll(tasks);
            return downloadedFiles;
        }
    }
}
