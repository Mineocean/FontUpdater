using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace FontManager.Utils
{
    public static class FontReader
    {
        [DllImport("gdi32.dll")]
        private static extern int GetFontData(IntPtr hdc, uint dwTable, uint dwOffset, byte[] lpBuffer, int cbData);

        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        public static string GetFontVersion(string fontPath)
        {
            try
            {
                if (!File.Exists(fontPath))
                    return null;

                using (var privateFonts = new PrivateFontCollection())
                {
                    privateFonts.AddFontFile(fontPath);
                    if (privateFonts.Families.Length == 0)
                        return null;

                    var font = privateFonts.Families[0];
                    using (var bmp = new Bitmap(1, 1))
                    using (var g = Graphics.FromImage(bmp))
                    {
                        IntPtr hdc = g.GetHdc();
                        using (var fontObj = new Font(font, 12))
                        {
                            IntPtr hFont = fontObj.ToHfont();
                            IntPtr oldFont = SelectObject(hdc, hFont);

                            byte[] versionData = new byte[256];
                            int result = GetFontData(hdc, 0x00000000, 0, versionData, versionData.Length);

                            SelectObject(hdc, oldFont);
                            DeleteObject(hFont);
                            g.ReleaseHdc(hdc);

                            if (result > 0)
                            {
                                return ParseVersionFromNameTable(fontPath);
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            return ParseVersionFromNameTable(fontPath);
        }

        private static string ParseVersionFromNameTable(string fontPath)
        {
            try
            {
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
                        
                        return ParseNameTable(fileBytes, tableOffset);
                    }
                }
            }
            catch
            {
            }

            return null;
        }

        private static string ParseNameTable(byte[] data, uint tableOffset)
        {
            try
            {
                if (tableOffset + 6 > data.Length)
                    return null;

                uint format = (uint)((data[tableOffset] << 8) | data[tableOffset + 1]);
                uint count = (uint)((data[tableOffset + 2] << 8) | data[tableOffset + 3]);
                uint stringOffset = (uint)((data[tableOffset + 4] << 8) | data[tableOffset + 5]);

                for (int i = 0; i < count; i++)
                {
                    uint recordOffset = tableOffset + 6 + (uint)(i * 12);
                    if (recordOffset + 12 > data.Length)
                        break;

                    uint platformID = (uint)((data[recordOffset] << 8) | data[recordOffset + 1]);
                    uint encodingID = (uint)((data[recordOffset + 2] << 8) | data[recordOffset + 3]);
                    uint languageID = (uint)((data[recordOffset + 4] << 8) | data[recordOffset + 5]);
                    uint nameID = (uint)((data[recordOffset + 6] << 8) | data[recordOffset + 7]);
                    uint length = (uint)((data[recordOffset + 8] << 8) | data[recordOffset + 9]);
                    uint offset = (uint)((data[recordOffset + 10] << 8) | data[recordOffset + 11]);

                    if (nameID == 5)
                    {
                        uint absoluteOffset = tableOffset + stringOffset + offset;
                        if (absoluteOffset + length > data.Length)
                            break;

                        if (platformID == 3)
                        {
                            return System.Text.Encoding.BigEndianUnicode.GetString(data, (int)absoluteOffset, (int)length);
                        }
                        else if (platformID == 1 && encodingID == 0)
                        {
                            return System.Text.Encoding.ASCII.GetString(data, (int)absoluteOffset, (int)length);
                        }
                    }
                }
            }
            catch
            {
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
            catch
            {
            }

            return Path.GetFileNameWithoutExtension(fontPath);
        }
    }
}
