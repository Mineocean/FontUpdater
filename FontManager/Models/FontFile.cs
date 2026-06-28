using System;

namespace FontManager.Models
{
    public class FontFile
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Version { get; set; }
        public string Style { get; set; }
        public long FileSize { get; set; }
        public DateTime InstallDate { get; set; }
        public string FontFamilyName { get; set; }

        public string DisplayName => $"{FontFamilyName} - {Style}";
        
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
}
