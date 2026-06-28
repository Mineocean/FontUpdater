using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontManager.Models;
using FontManager.Services;
using FontManager.Utils;

namespace FontManager.Forms
{
    public partial class MainForm : Form
    {
        private readonly UpdateService _updateService;
        private readonly FontService _fontService;
        private readonly BackupService _backupService;
        private List<FontFamily> _fontFamilies;
        private List<UpdateInfo> _updates;

        public MainForm()
        {
            InitializeComponent();
            _updateService = new UpdateService();
            _fontService = new FontService();
            _backupService = new BackupService();
            _fontFamilies = GitHubService.GetSupportedFontFamilies();

            _updateService.ProgressChanged += UpdateService_ProgressChanged;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadFontList();
        }

        private void LoadFontList()
        {
            dgvFonts.Rows.Clear();

            foreach (var family in _fontFamilies)
            {
                var installedFonts = _fontService.GetInstalledFontsByFamily(family.Name);
                bool isInstalled = installedFonts.Count > 0;
                string currentVersion = installedFonts.FirstOrDefault()?.Version ?? "未安装";

                int rowIndex = dgvFonts.Rows.Add(
                    family.Name,
                    isInstalled,
                    currentVersion,
                    family.LatestVersion ?? "未知",
                    "就绪"
                );

                dgvFonts.Rows[rowIndex].Tag = family;
            }
        }

        private async Task CheckForUpdatesAsync()
        {
            btnCheckUpdates.Enabled = false;
            toolStripStatusLabel.Text = "正在检查更新...";
            progressBar.Style = ProgressBarStyle.Marquee;

            try
            {
                _updates = await _updateService.CheckForUpdatesAsync(_fontFamilies);

                foreach (DataGridViewRow row in dgvFonts.Rows)
                {
                    var family = (FontFamily)row.Tag;
                    var update = _updates.FirstOrDefault(u => u.FontFamily.Name == family.Name);

                    if (update != null)
                    {
                        row.Cells["colLatest"].Value = update.LatestVersion;
                        row.Cells["colStatus"].Value = update.HasUpdate ? "有更新" : "已是最新";
                        
                        if (update.HasUpdate)
                        {
                            row.DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
                        }
                    }
                }

                int updateCount = _updates.Count(u => u.HasUpdate);
                toolStripStatusLabel.Text = $"检查完成，发现 {updateCount} 个更新";
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to check for updates", ex);
                toolStripStatusLabel.Text = "检查更新失败";
                MessageBox.Show($"检查更新失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCheckUpdates.Enabled = true;
                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Value = 0;
            }
        }

        private async void btnCheckUpdates_Click(object sender, EventArgs e)
        {
            await CheckForUpdatesAsync();
        }

        private async void btnUpdateAll_Click(object sender, EventArgs e)
        {
            if (_updates == null || !_updates.Any(u => u.HasUpdate))
            {
                MessageBox.Show("没有可用的更新", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("确定要更新所有字体吗？", "确认", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            btnUpdateAll.Enabled = false;
            btnCheckUpdates.Enabled = false;

            try
            {
                await _updateService.UpdateAllAsync(_updates);
                MessageBox.Show("所有字体更新完成！", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadFontList();
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to update all fonts", ex);
                MessageBox.Show($"更新失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnUpdateAll.Enabled = true;
                btnCheckUpdates.Enabled = true;
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var settingsForm = new SettingsForm())
            {
                settingsForm.ShowDialog(this);
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (dgvFonts.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择一个字体", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var family = (FontFamily)dgvFonts.SelectedRows[0].Tag;
            var installedFonts = _fontService.GetInstalledFontsByFamily(family.Name);

            if (installedFonts.Count == 0)
            {
                MessageBox.Show("该字体尚未安装，无法预览", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var previewForm = new PreviewForm(installedFonts.First()))
            {
                previewForm.ShowDialog(this);
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            var backups = _backupService.GetBackups();
            
            if (backups.Count == 0)
            {
                MessageBox.Show("没有备份记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string message = $"共有 {backups.Count} 个备份:\n\n";
            foreach (var backup in backups.Take(10))
            {
                message += $"- {backup.FileName} ({backup.FileSizeFormatted}) - {backup.BackupDate:yyyy-MM-dd HH:mm}\n";
            }

            if (backups.Count > 10)
            {
                message += $"\n...还有 {backups.Count - 10} 个备份";
            }

            MessageBox.Show(message, "备份列表", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateService_ProgressChanged(object sender, UpdateProgressEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateService_ProgressChanged(sender, e)));
                return;
            }

            progressBar.Value = e.ProgressPercentage;
            lblStatus.Text = $"{e.FontFamilyName}: {e.StatusMessage}";
            toolStripStatusLabel.Text = $"{e.FontFamilyName}: {e.StatusMessage}";
        }
    }
}
