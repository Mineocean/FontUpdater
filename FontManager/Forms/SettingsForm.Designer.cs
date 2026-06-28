namespace FontManager.Forms
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.chkAutoCheck = new System.Windows.Forms.CheckBox();
            this.chkAutoInstall = new System.Windows.Forms.CheckBox();
            this.chkBackupBeforeUpdate = new System.Windows.Forms.CheckBox();
            this.lblDownloadDir = new System.Windows.Forms.Label();
            this.txtDownloadDir = new System.Windows.Forms.TextBox();
            this.btnBrowseDownload = new System.Windows.Forms.Button();
            this.lblBackupDir = new System.Windows.Forms.Label();
            this.txtBackupDir = new System.Windows.Forms.TextBox();
            this.btnBrowseBackup = new System.Windows.Forms.Button();
            this.lblGitHubToken = new System.Windows.Forms.Label();
            this.txtGitHubToken = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBoxGeneral = new System.Windows.Forms.GroupBox();
            this.groupBoxPaths = new System.Windows.Forms.GroupBox();
            this.groupBoxGitHub = new System.Windows.Forms.GroupBox();
            this.groupBoxGeneral.SuspendLayout();
            this.groupBoxPaths.SuspendLayout();
            this.groupBoxGitHub.SuspendLayout();
            this.SuspendLayout();
            
            this.chkAutoCheck.AutoSize = true;
            this.chkAutoCheck.Location = new System.Drawing.Point(15, 25);
            this.chkAutoCheck.Name = "chkAutoCheck";
            this.chkAutoCheck.Size = new System.Drawing.Size(130, 17);
            this.chkAutoCheck.TabIndex = 0;
            this.chkAutoCheck.Text = "启动时自动检查更新";
            this.chkAutoCheck.UseVisualStyleBackColor = true;
            
            this.chkAutoInstall.AutoSize = true;
            this.chkAutoInstall.Location = new System.Drawing.Point(15, 50);
            this.chkAutoInstall.Name = "chkAutoInstall";
            this.chkAutoInstall.Size = new System.Drawing.Size(130, 17);
            this.chkAutoInstall.TabIndex = 1;
            this.chkAutoInstall.Text = "下载后自动安装字体";
            this.chkAutoInstall.UseVisualStyleBackColor = true;
            
            this.chkBackupBeforeUpdate.AutoSize = true;
            this.chkBackupBeforeUpdate.Location = new System.Drawing.Point(15, 75);
            this.chkBackupBeforeUpdate.Name = "chkBackupBeforeUpdate";
            this.chkBackupBeforeUpdate.Size = new System.Drawing.Size(130, 17);
            this.chkBackupBeforeUpdate.TabIndex = 2;
            this.chkBackupBeforeUpdate.Text = "更新前自动备份旧版本";
            this.chkBackupBeforeUpdate.UseVisualStyleBackColor = true;
            
            this.lblDownloadDir.AutoSize = true;
            this.lblDownloadDir.Location = new System.Drawing.Point(15, 25);
            this.lblDownloadDir.Name = "lblDownloadDir";
            this.lblDownloadDir.Size = new System.Drawing.Size(79, 13);
            this.lblDownloadDir.TabIndex = 3;
            this.lblDownloadDir.Text = "下载目录:";
            
            this.txtDownloadDir.Location = new System.Drawing.Point(100, 22);
            this.txtDownloadDir.Name = "txtDownloadDir";
            this.txtDownloadDir.Size = new System.Drawing.Size(280, 20);
            this.txtDownloadDir.TabIndex = 4;
            
            this.btnBrowseDownload.Location = new System.Drawing.Point(386, 20);
            this.btnBrowseDownload.Name = "btnBrowseDownload";
            this.btnBrowseDownload.Size = new System.Drawing.Size(30, 23);
            this.btnBrowseDownload.TabIndex = 5;
            this.btnBrowseDownload.Text = "...";
            this.btnBrowseDownload.UseVisualStyleBackColor = true;
            this.btnBrowseDownload.Click += new System.EventHandler(this.btnBrowseDownload_Click);
            
            this.lblBackupDir.AutoSize = true;
            this.lblBackupDir.Location = new System.Drawing.Point(15, 55);
            this.lblBackupDir.Name = "lblBackupDir";
            this.lblBackupDir.Size = new System.Drawing.Size(79, 13);
            this.lblBackupDir.TabIndex = 6;
            this.lblBackupDir.Text = "备份目录:";
            
            this.txtBackupDir.Location = new System.Drawing.Point(100, 52);
            this.txtBackupDir.Name = "txtBackupDir";
            this.txtBackupDir.Size = new System.Drawing.Size(280, 20);
            this.txtBackupDir.TabIndex = 7;
            
            this.btnBrowseBackup.Location = new System.Drawing.Point(386, 50);
            this.btnBrowseBackup.Name = "btnBrowseBackup";
            this.btnBrowseBackup.Size = new System.Drawing.Size(30, 23);
            this.btnBrowseBackup.TabIndex = 8;
            this.btnBrowseBackup.Text = "...";
            this.btnBrowseBackup.UseVisualStyleBackColor = true;
            this.btnBrowseBackup.Click += new System.EventHandler(this.btnBrowseBackup_Click);
            
            this.lblGitHubToken.AutoSize = true;
            this.lblGitHubToken.Location = new System.Drawing.Point(15, 25);
            this.lblGitHubToken.Name = "lblGitHubToken";
            this.lblGitHubToken.Size = new System.Drawing.Size(79, 13);
            this.lblGitHubToken.TabIndex = 0;
            this.lblGitHubToken.Text = "Token:";
            
            this.txtGitHubToken.Location = new System.Drawing.Point(100, 22);
            this.txtGitHubToken.Name = "txtGitHubToken";
            this.txtGitHubToken.Size = new System.Drawing.Size(280, 20);
            this.txtGitHubToken.TabIndex = 1;
            this.txtGitHubToken.UseSystemPasswordChar = true;
            
            this.btnSave.Location = new System.Drawing.Point(260, 250);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(341, 250);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            
            this.btnReset.Location = new System.Drawing.Point(12, 250);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 25);
            this.btnReset.TabIndex = 11;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            
            this.groupBoxGeneral.Controls.Add(this.chkAutoCheck);
            this.groupBoxGeneral.Controls.Add(this.chkAutoInstall);
            this.groupBoxGeneral.Controls.Add(this.chkBackupBeforeUpdate);
            this.groupBoxGeneral.Location = new System.Drawing.Point(12, 12);
            this.groupBoxGeneral.Name = "groupBoxGeneral";
            this.groupBoxGeneral.Size = new System.Drawing.Size(420, 100);
            this.groupBoxGeneral.TabIndex = 12;
            this.groupBoxGeneral.TabStop = false;
            this.groupBoxGeneral.Text = "常规设置";
            
            this.groupBoxPaths.Controls.Add(this.lblDownloadDir);
            this.groupBoxPaths.Controls.Add(this.txtDownloadDir);
            this.groupBoxPaths.Controls.Add(this.btnBrowseDownload);
            this.groupBoxPaths.Controls.Add(this.lblBackupDir);
            this.groupBoxPaths.Controls.Add(this.txtBackupDir);
            this.groupBoxPaths.Controls.Add(this.btnBrowseBackup);
            this.groupBoxPaths.Location = new System.Drawing.Point(12, 118);
            this.groupBoxPaths.Name = "groupBoxPaths";
            this.groupBoxPaths.Size = new System.Drawing.Size(420, 76);
            this.groupBoxPaths.TabIndex = 13;
            this.groupBoxPaths.TabStop = false;
            this.groupBoxPaths.Text = "路径设置";
            
            this.groupBoxGitHub.Controls.Add(this.lblGitHubToken);
            this.groupBoxGitHub.Controls.Add(this.txtGitHubToken);
            this.groupBoxGitHub.Location = new System.Drawing.Point(12, 200);
            this.groupBoxGitHub.Name = "groupBoxGitHub";
            this.groupBoxGitHub.Size = new System.Drawing.Size(420, 50);
            this.groupBoxGitHub.TabIndex = 14;
            this.groupBoxGitHub.TabStop = false;
            this.groupBoxGitHub.Text = "GitHub设置 (可选，用于解决API限制)";
            
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(444, 290);
            this.Controls.Add(this.groupBoxGeneral);
            this.Controls.Add(this.groupBoxPaths);
            this.Controls.Add(this.groupBoxGitHub);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReset);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBoxGeneral.ResumeLayout(false);
            this.groupBoxGeneral.PerformLayout();
            this.groupBoxPaths.ResumeLayout(false);
            this.groupBoxPaths.PerformLayout();
            this.groupBoxGitHub.ResumeLayout(false);
            this.groupBoxGitHub.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.CheckBox chkAutoCheck;
        private System.Windows.Forms.CheckBox chkAutoInstall;
        private System.Windows.Forms.CheckBox chkBackupBeforeUpdate;
        private System.Windows.Forms.Label lblDownloadDir;
        private System.Windows.Forms.TextBox txtDownloadDir;
        private System.Windows.Forms.Button btnBrowseDownload;
        private System.Windows.Forms.Label lblBackupDir;
        private System.Windows.Forms.TextBox txtBackupDir;
        private System.Windows.Forms.Button btnBrowseBackup;
        private System.Windows.Forms.Label lblGitHubToken;
        private System.Windows.Forms.TextBox txtGitHubToken;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox groupBoxGeneral;
        private System.Windows.Forms.GroupBox groupBoxPaths;
        private System.Windows.Forms.GroupBox groupBoxGitHub;
    }
}
