using System;
using Newtonsoft.Json;

namespace FontManager.Models
{
    public class GitHubAsset
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("download_count")]
        public long DownloadCount { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("browser_download_url")]
        public string BrowserDownloadUrl { get; set; }

        public bool IsFontFile => Name != null && (Name.EndsWith(".ttf", StringComparison.OrdinalIgnoreCase) ||
                                                     Name.EndsWith(".otf", StringComparison.OrdinalIgnoreCase));

        public string Style
        {
            get
            {
                if (Name == null) return "Unknown";
                if (Name.Contains("Light")) return "Light";
                if (Name.Contains("Medium")) return "Medium";
                if (Name.Contains("Bold")) return "Bold";
                if (Name.Contains("Mono")) return "Mono";
                return "Regular";
            }
        }

        public string SizeFormatted
        {
            get
            {
                string[] sizes = { "B", "KB", "MB", "GB" };
                double len = Size;
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
}
