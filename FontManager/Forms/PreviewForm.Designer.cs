namespace FontManager.Forms
{
    partial class PreviewForm
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
            this.txtPreview = new System.Windows.Forms.TextBox();
            this.trkFontSize = new System.Windows.Forms.TrackBar();
            this.lblFontSize = new System.Windows.Forms.Label();
            this.lblFontInfo = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trkFontSize)).BeginInit();
            this.SuspendLayout();
            
            this.txtPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPreview.Location = new System.Drawing.Point(12, 70);
            this.txtPreview.Multiline = true;
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPreview.Size = new System.Drawing.Size(460, 280);
            this.txtPreview.TabIndex = 0;
            this.txtPreview.Text = "霞鹜文楷\r\nLxgwWenKai\r\n\r\n永 The quick brown fox jumps over the lazy dog 永\r\n\r\n" +
                "ABCDEFGHIJKLMabcdefghijklm 1234567890\r\n" +
                "编程代码: var x = 123; // 注释\r\n" +
                "中文排版：春眠不觉晓，处处闻啼鸟。";
            
            this.trkFontSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trkFontSize.Location = new System.Drawing.Point(75, 38);
            this.trkFontSize.Maximum = 72;
            this.trkFontSize.Minimum = 8;
            this.trkFontSize.Name = "trkFontSize";
            this.trkFontSize.Size = new System.Drawing.Size(397, 45);
            this.trkFontSize.TabIndex = 1;
            this.trkFontSize.TickFrequency = 4;
            this.trkFontSize.Value = 16;
            this.trkFontSize.ValueChanged += new System.EventHandler(this.trkFontSize_ValueChanged);
            
            this.lblFontSize.AutoSize = true;
            this.lblFontSize.Location = new System.Drawing.Point(12, 42);
            this.lblFontSize.Name = "lblFontSize";
            this.lblFontSize.Size = new System.Drawing.Size(55, 13);
            this.lblFontSize.TabIndex = 2;
            this.lblFontSize.Text = "字号: 16";
            
            this.lblFontInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFontInfo.Location = new System.Drawing.Point(12, 9);
            this.lblFontInfo.Name = "lblFontInfo";
            this.lblFontInfo.Size = new System.Drawing.Size(460, 23);
            this.lblFontInfo.TabIndex = 3;
            this.lblFontInfo.Text = "字体信息";
            
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(397, 356);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(484, 391);
            this.Controls.Add(this.txtPreview);
            this.Controls.Add(this.trkFontSize);
            this.Controls.Add(this.lblFontSize);
            this.Controls.Add(this.lblFontInfo);
            this.Controls.Add(this.btnClose);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "PreviewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "字体预览";
            this.Load += new System.EventHandler(this.PreviewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trkFontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtPreview;
        private System.Windows.Forms.TrackBar trkFontSize;
        private System.Windows.Forms.Label lblFontSize;
        private System.Windows.Forms.Label lblFontInfo;
        private System.Windows.Forms.Button btnClose;
    }
}
