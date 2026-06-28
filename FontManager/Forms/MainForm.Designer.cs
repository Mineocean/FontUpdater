namespace FontManager.Forms
{
    partial class MainForm
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
            this.dgvFonts = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInstalled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLatest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnCheckUpdates = new System.Windows.Forms.Button();
            this.btnUpdateAll = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFonts)).BeginInit();
            this.panelTop.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            
            this.dgvFonts.AllowUserToAddRows = false;
            this.dgvFonts.AllowUserToDeleteRows = false;
            this.dgvFonts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFonts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFonts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colName,
                this.colInstalled,
                this.colVersion,
                this.colLatest,
                this.colStatus
            });
            this.dgvFonts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFonts.Location = new System.Drawing.Point(0, 60);
            this.dgvFonts.MultiSelect = false;
            this.dgvFonts.Name = "dgvFonts";
            this.dgvFonts.ReadOnly = true;
            this.dgvFonts.RowHeadersVisible = false;
            this.dgvFonts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFonts.Size = new System.Drawing.Size(784, 418);
            this.dgvFonts.TabIndex = 0;
            
            this.colName.HeaderText = "字体名称";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            
            this.colInstalled.HeaderText = "已安装";
            this.colInstalled.Name = "colInstalled";
            this.colInstalled.ReadOnly = true;
            this.colInstalled.Width = 60;
            
            this.colVersion.HeaderText = "当前版本";
            this.colVersion.Name = "colVersion";
            this.colVersion.ReadOnly = true;
            
            this.colLatest.HeaderText = "最新版本";
            this.colLatest.Name = "colLatest";
            this.colLatest.ReadOnly = true;
            
            this.colStatus.HeaderText = "状态";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            
            this.panelTop.Controls.Add(this.btnCheckUpdates);
            this.panelTop.Controls.Add(this.btnUpdateAll);
            this.panelTop.Controls.Add(this.btnDownload);
            this.panelTop.Controls.Add(this.btnSettings);
            this.panelTop.Controls.Add(this.btnPreview);
            this.panelTop.Controls.Add(this.btnBackup);
            this.panelTop.Controls.Add(this.progressBar);
            this.panelTop.Controls.Add(this.lblStatus);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(784, 60);
            this.panelTop.TabIndex = 1;
            
            this.btnCheckUpdates.Location = new System.Drawing.Point(12, 15);
            this.btnCheckUpdates.Name = "btnCheckUpdates";
            this.btnCheckUpdates.Size = new System.Drawing.Size(100, 30);
            this.btnCheckUpdates.TabIndex = 0;
            this.btnCheckUpdates.Text = "检查更新";
            this.btnCheckUpdates.UseVisualStyleBackColor = true;
            this.btnCheckUpdates.Click += new System.EventHandler(this.btnCheckUpdates_Click);
            
            this.btnUpdateAll.Location = new System.Drawing.Point(118, 15);
            this.btnUpdateAll.Name = "btnUpdateAll";
            this.btnUpdateAll.Size = new System.Drawing.Size(100, 30);
            this.btnUpdateAll.TabIndex = 1;
            this.btnUpdateAll.Text = "全部更新";
            this.btnUpdateAll.UseVisualStyleBackColor = true;
            this.btnUpdateAll.Click += new System.EventHandler(this.btnUpdateAll_Click);
            
            this.btnDownload.Location = new System.Drawing.Point(224, 15);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 30);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "下载";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            
            this.btnSettings.Location = new System.Drawing.Point(305, 15);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 30);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.Text = "设置";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            
            this.btnPreview.Location = new System.Drawing.Point(386, 15);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 30);
            this.btnPreview.TabIndex = 4;
            this.btnPreview.Text = "预览";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            
            this.btnBackup.Location = new System.Drawing.Point(467, 15);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(75, 30);
            this.btnBackup.TabIndex = 5;
            this.btnBackup.Text = "备份";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            
            this.progressBar.Location = new System.Drawing.Point(560, 20);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(200, 20);
            this.progressBar.TabIndex = 6;
            
            this.lblStatus.Location = new System.Drawing.Point(560, 5);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(200, 15);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "";
            
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.toolStripStatusLabel
            });
            this.statusStrip.Location = new System.Drawing.Point(0, 478);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 2;
            
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(769, 17);
            this.toolStripStatusLabel.Spring = true;
            this.toolStripStatusLabel.Text = "就绪";
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 500);
            this.Controls.Add(this.dgvFonts);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.statusStrip);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "霞鹜文楷字体管理器";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFonts)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvFonts;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colInstalled;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLatest;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnCheckUpdates;
        private System.Windows.Forms.Button btnUpdateAll;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}
