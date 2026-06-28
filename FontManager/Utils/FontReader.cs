using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace FontManager.Utils
{
    public static class FontReader
    {
        public static string GetFontVersion(string fontPath)
        {
            try
            {
                if (!File.Exists(fontPath))
                    return null;

                byte[] fileBytes = File.ReadAllBytes(fontPath);
                
                if (fileBytes.Length < 12)
                    return null;

                uint numTables = (uint)((fileBytes[4] << 8) | fileBytes[5]);
                
                for (int i = 0; i < numTables; i++)
                {
                    int offset = 12 + (i * 16);
                    if (offset + 16 > fileBytes.Length)
                        break;

                    string tag = System.Text.Encoding.ASCII.GetString(fileBytes, offset, 4);
                    
                    if (tag == "name")
                    {
                        uint tableOffset = (uint)((fileBytes[offset + 8] << 24) | 
                                                  (fileBytes[offset + 9] << 16) | 
                                                  (fileBytes[offset + 10] << 8) | 
                                                  fileBytes[offset + 11]);
                        
                        string version = ParseNameTable(fileBytes, tableOffset);
                        if (!string.IsNullOrEmpty(version))
                        {
                            return ExtractVersionNumber(version);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Debug($"Failed to read font version from {fontPath}: {ex.Message}");
            }

            return ExtractVersionFromFileName(fontPath);
        }

        private static string ParseNameTable(byte[] data, uint tableOffset)
        {
            try
            {
                if (tableOffset + 6 > data.Length)
                    return null;

                uint count = (uint)((data[tableOffset + 2] << 8) | data[tableOffset + 3]);
                uint stringOffset = (uint)((data[tableOffset + 4] << 8) | data[tableOffset + 5]);

                for (int i = 0; i < count && i < 100; i++)
                {
                    uint recordOffset = tableOffset + 6 + (uint)(i * 12);
                    if (recordOffset + 12 > data.Length)
                        break;

                    uint platformID = (uint)((data[recordOffset] << 8) | data[recordOffset + 1]);
                    uint encodingID = (uint)((data[recordOffset + 2] << 8) | data[recordOffset + 3]);
                    uint nameID = (uint)((data[recordOffset + 6] << 8) | data[recordOffset + 7]);
                    uint length = (uint)((data[recordOffset + 8] << 8) | data[recordOffset + 9]);
                    uint strOffset = (uint)((data[recordOffset + 10] << 8) | data[recordOffset + 11]);

                    if (nameID == 5)
                    {
                        uint absoluteOffset = tableOffset + stringOffset + strOffset;
                        if (absoluteOffset + length > data.Length || length == 0)
                            continue;

                        try
                        {
                            string versionStr = null;
                            if (platformID == 3 && encodingID == 1)
                            {
                                versionStr = System.Text.Encoding.BigEndianUnicode.GetString(data, (int)absoluteOffset, (int)length);
                            }
                            else if (platformID == 1 && encodingID == 0)
                            {
                                versionStr = System.Text.Encoding.ASCII.GetString(data, (int)absoluteOffset, (int)length);
                            }

                            if (!string.IsNullOrEmpty(versionStr) && versionStr.Contains("Version"))
                            {
                                return versionStr;
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }

            return null;
        }

        private static string ExtractVersionNumber(string versionString)
        {
            if (string.IsNullOrEmpty(versionString))
                return null;

            var match = Regex.Match(versionString, @"Version\s+(\d+\.?\d*)");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            match = Regex.Match(versionString, @"(\d+\.\d+)");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return versionString.Trim();
        }

        private static string ExtractVersionFromFileName(string fontPath)
        {
            string fileName = Path.GetFileNameWithoutExtension(fontPath);
            var match = Regex.Match(fileName, @"v?(\d+\.\d+)");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return null;
        }

        public static string GetFontFamilyName(string fontPath)
        {
            try
            {
                using (var privateFonts = new PrivateFontCollection())
                {
                    privateFonts.AddFontFile(fontPath);
                    if (privateFonts.Families.Length > 0)
                    {
                        return privateFonts.Families[0].Name;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Debug($"Failed to read font family name from {fontPath}: {ex.Message}");
            }

            return Path.GetFileNameWithoutExtension(fontPath);
        }
    }
}
