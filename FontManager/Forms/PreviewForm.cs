using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FontManager.Models;

namespace FontManager.Forms
{
    public partial class PreviewForm : Form
    {
        private readonly FontFile _fontFile;
        private PrivateFontCollection _privateFonts;
        private Font _previewFont;

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        public PreviewForm(FontFile fontFile)
        {
            InitializeComponent();
            _fontFile = fontFile;
        }

        private void PreviewForm_Load(object sender, EventArgs e)
        {
            LoadFont();
            UpdatePreview();
            lblFontInfo.Text = $"{_fontFile.FontFamilyName} - {_fontFile.Style} (版本: {_fontFile.Version})";
        }

        private void LoadFont()
        {
            try
            {
                _privateFonts = new PrivateFontCollection();

                if (File.Exists(_fontFile.FilePath))
                {
                    _privateFonts.AddFontFile(_fontFile.FilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载字体失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePreview()
        {
            try
            {
                if (_privateFonts != null && _privateFonts.Families.Length > 0)
                {
                    _previewFont?.Dispose();
                    _previewFont = new Font(_privateFonts.Families[0], trkFontSize.Value);
                    txtPreview.Font = _previewFont;
                    lblFontSize.Text = $"字号: {trkFontSize.Value}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"应用字体失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trkFontSize_ValueChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _previewFont?.Dispose();
            _privateFonts?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
