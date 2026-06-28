using System;
using System.Diagnostics;
using System.Windows.Forms;
using FontManager.Utils;

namespace FontManager.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            var settings = ConfigManager.Settings;
            chkAutoCheck.Checked = settings.AutoCheckUpdates;
            chkAutoInstall.Checked = settings.AutoInstallFonts;
            chkBackupBeforeUpdate.Checked = settings.CreateBackupBeforeUpdate;
            txtDownloadDir.Text = settings.DownloadDirectory;
            txtBackupDir.Text = settings.BackupDirectory;
            txtGitHubToken.Text = settings.GitHubToken;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var settings = ConfigManager.Settings;
            settings.AutoCheckUpdates = chkAutoCheck.Checked;
            settings.AutoInstallFonts = chkAutoInstall.Checked;
            settings.CreateBackupBeforeUpdate = chkBackupBeforeUpdate.Checked;
            settings.DownloadDirectory = txtDownloadDir.Text;
            settings.BackupDirectory = txtBackupDir.Text;
            settings.GitHubToken = txtGitHubToken.Text;
            ConfigManager.Save();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("确定要重置所有设置吗？", "确认",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ConfigManager.Reset();
                LoadSettings();
            }
        }

        private void btnBrowseDownload_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = txtDownloadDir.Text;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtDownloadDir.Text = dialog.SelectedPath;
                }
            }
        }

        private void btnBrowseBackup_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = txtBackupDir.Text;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtBackupDir.Text = dialog.SelectedPath;
                }
            }
        }

        private void lnkGetToken_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://github.com/settings/tokens/new?scopes=&description=FontManager",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"无法打开浏览器: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
