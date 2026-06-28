using System;
using System.Collections.Generic;

namespace FontManager.Models
{
    public class FontFamily
    {
        public string Name { get; set; }
        public string RepoOwner { get; set; }
        public string RepoName { get; set; }
        public string Description { get; set; }
        public List<FontFile> InstalledFonts { get; set; } = new List<FontFile>();
        public string LatestVersion { get; set; }
        public bool IsInstalled => InstalledFonts.Count > 0;

        public string FullName => $"{RepoOwner}/{RepoName}";
        
        public string DownloadBaseUrl => $"https://github.com/{RepoOwner}/{RepoName}/releases/download";
    }
}
