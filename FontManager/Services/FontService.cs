using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using FontManager.Models;
using FontManager.Utils;

namespace FontManager.Services
{
    public class FontService
    {
        private static readonly string SystemFontDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
        private static readonly string UserFontDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Microsoft", "Windows", "Fonts");

        [DllImport("gdi32.dll")]
        private static extern int AddFontResource(string lpFileName);

        [DllImport("gdi32.dll")]
        private static extern int RemoveFontResource(string lpFileName);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const uint WM_FONTCHANGE = 0x001D;
        private static readonly IntPtr HWND_BROADCAST = new IntPtr(0xFFFF);

        public List<FontFile> GetInstalledFonts()
        {
            var fonts = new List<FontFile>();

            try
            {
                if (Directory.Exists(UserFontDirectory))
                {
                    foreach (var file in Directory.GetFiles(UserFontDirectory, "*.ttf"))
                    {
                        var fontFile = CreateFontFileFromFile(file);
                        if (fontFile != null)
                        {
                            fonts.Add(fontFile);
                        }
                    }
                }

                using (var installedFonts = new InstalledFontCollection())
                {
                    foreach (var fontFamily in installedFonts.Families)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to get installed fonts", ex);
            }

            return fonts;
        }

        public List<FontFile> GetInstalledFontsByFamily(string familyName)
        {
            return GetInstalledFonts()
                .Where(f => f.FontFamilyName.Contains(familyName) || familyName.Contains(f.FontFamilyName))
                .ToList();
        }

        private FontFile CreateFontFileFromFile(string filePath)
        {
            try
            {
                var fileInfo = new FileInfo(filePath);
                string version = FontReader.GetFontVersion(filePath);
                string familyName = FontReader.GetFontFamilyName(filePath);
                string style = DetermineStyle(filePath, familyName);

                return new FontFile
                {
                    FileName = fileInfo.Name,
                    FilePath = filePath,
                    Version = version,
                    Style = style,
                    FileSize = fileInfo.Length,
                    InstallDate = fileInfo.LastWriteTime,
                    FontFamilyName = familyName
                };
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to create FontFile for {filePath}", ex);
                return null;
            }
        }

        private string DetermineStyle(string filePath, string familyName)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath).ToLower();
            
            if (fileName.Contains("light")) return "Light";
            if (fileName.Contains("medium")) return "Medium";
            if (fileName.Contains("bold")) return "Bold";
            if (fileName.Contains("mono")) return "Mono";
            if (fileName.Contains("italic")) return "Italic";
            if (fileName.Contains("clear")) return "Clear";
            if (fileName.Contains("screen")) return "Screen";
            
            return "Regular";
        }

        public bool InstallFont(string fontFilePath)
        {
            try
            {
                if (!File.Exists(fontFilePath))
                {
                    Logger.Error($"Font file not found: {fontFilePath}");
                    return false;
                }

                if (!Directory.Exists(UserFontDirectory))
                {
                    Directory.CreateDirectory(UserFontDirectory);
                }

                string fileName = Path.GetFileName(fontFilePath);
                string destPath = Path.Combine(UserFontDirectory, fileName);

                if (File.Exists(destPath))
                {
                    File.Delete(destPath);
                }

                File.Copy(fontFilePath, destPath);

                int result = AddFontResource(destPath);
                SendMessage(HWND_BROADCAST, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);

                Logger.Info($"Font installed: {fileName}");
                return result > 0;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to install font {fontFilePath}", ex);
                return false;
            }
        }

        public bool UninstallFont(string fontFilePath)
        {
            try
            {
                if (!File.Exists(fontFilePath))
                {
                    return false;
                }

                int result = RemoveFontResource(fontFilePath);
                SendMessage(HWND_BROADCAST, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);

                File.Delete(fontFilePath);

                Logger.Info($"Font uninstalled: {Path.GetFileName(fontFilePath)}");
                return result > 0;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to uninstall font {fontFilePath}", ex);
                return false;
            }
        }

        public bool InstallFontsFromDirectory(string directory)
        {
            try
            {
                if (!Directory.Exists(directory))
                {
                    Logger.Error($"Directory not found: {directory}");
                    return false;
                }

                int installedCount = 0;
                var fontFiles = Directory.GetFiles(directory, "*.ttf");

                foreach (var fontFile in fontFiles)
                {
                    if (InstallFont(fontFile))
                    {
                        installedCount++;
                    }
                }

                Logger.Info($"Installed {installedCount} fonts from {directory}");
                return installedCount > 0;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to install fonts from {directory}", ex);
                return false;
            }
        }

        public FontFile GetFontInfo(string fontFilePath)
        {
            return CreateFontFileFromFile(fontFilePath);
        }

        public bool IsFontInstalled(string fontFileName)
        {
            string userFontPath = Path.Combine(UserFontDirectory, fontFileName);
            return File.Exists(userFontPath);
        }
    }
}
