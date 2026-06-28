using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FontManager.Models;
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

        public static List<FontFamily> GetSupportedFontFamilies()
        {
            return new List<FontFamily>
            {
                new FontFamily { Name = "霞鹜文楷", RepoOwner = "lxgw", RepoName = "LxgwWenKai", Description = "霞鹜文楷主版本" },
                new FontFamily { Name = "霞鹜文楷轻便版", RepoOwner = "lxgw", RepoName = "LxgwWenKai-Lite", Description = "精简版，去除部分汉字" },
                new FontFamily { Name = "霞鹜文楷GB", RepoOwner = "lxgw", RepoName = "LxgwWenkaiGB", Description = "GB标准版" },
                new FontFamily { Name = "霞鹜文楷TC", RepoOwner = "lxgw", RepoName = "LxgwWenkaiTC", Description = "繁体中文版" },
                new FontFamily { Name = "霞鹜文楷屏幕阅读版", RepoOwner = "lxgw", RepoName = "LxgwWenKaiScreen", Description = "屏幕阅读优化版" },
                new FontFamily { Name = "霞鹜文楷Mono", RepoOwner = "lxgw", RepoName = "LxgwWenKaiMono", Description = "等宽版" },
                new FontFamily { Name = "霞鹜文楷Clear", RepoOwner = "lxgw", RepoName = "LxgwWenKaiClear", Description = "清晰版" },
                new FontFamily { Name = "霞鹜文楷Italic", RepoOwner = "lxgw", RepoName = "LxgwWenKaiItalic", Description = "斜体版" },
                new FontFamily { Name = "霞鹜文楷2nd", RepoOwner = "lxgw", RepoName = "LxgwWenKai-2nd", Description = "第二版" },
                new FontFamily { Name = "霞鹜文楷3rd", RepoOwner = "lxgw", RepoName = "LxgwWenKai-3rd", Description = "第三版" },
                new FontFamily { Name = "霞鹜文楷4th", RepoOwner = "lxgw", RepoName = "LxgwWenKai-4th", Description = "第四版" },
                new FontFamily { Name = "霞鹜文楷5th", RepoOwner = "lxgw", RepoName = "LxgwWenKai-5th", Description = "第五版" },
                new FontFamily { Name = "霞鹜文楷6th", RepoOwner = "lxgw", RepoName = "LxgwWenKai-6th", Description = "第六版" }
            };
        }

        public async Task<GitHubRelease> GetLatestReleaseAsync(string owner, string repo)
        {
            try
            {
                string url = $"{ApiBase}/repos/{owner}/{repo}/releases/latest";
                var response = await HttpClient.GetStringAsync(url);
                return JsonConvert.DeserializeObject<GitHubRelease>(response);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to get latest release for {owner}/{repo}", ex);
                return null;
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
                var data = await HttpClient.GetByteArrayAsync(asset.BrowserDownloadUrl);
                System.IO.File.WriteAllBytes(filePath, data);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to download asset {asset.Name} to file", ex);
                throw;
            }
        }
    }
}
