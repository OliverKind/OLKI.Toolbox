namespace OLKI.Toolbox.UpdateApp
{
    partial class UpdateForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtChangeLog = new System.Windows.Forms.TextBox();
            this.lblCurrentVersion = new System.Windows.Forms.Label();
            this.lblNewVersion = new System.Windows.Forms.Label();
            this.btnInstallUpdate = new System.Windows.Forms.Button();
            this.expDownload = new OLKI.Toolbox.Widgets.ExtProgressBar();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 426);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(710, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtChangeLog
            // 
            this.txtChangeLog.Location = new System.Drawing.Point(12, 48);
            this.txtChangeLog.Multiline = true;
            this.txtChangeLog.Name = "txtChangeLog";
            this.txtChangeLog.ReadOnly = true;
            this.txtChangeLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtChangeLog.Size = new System.Drawing.Size(710, 314);
            this.txtChangeLog.TabIndex = 5;
            // 
            // lblCurrentVersion
            // 
            this.lblCurrentVersion.AutoSize = true;
            this.lblCurrentVersion.Location = new System.Drawing.Point(12, 9);
            this.lblCurrentVersion.Name = "lblCurrentVersion";
            this.lblCurrentVersion.Size = new System.Drawing.Size(83, 13);
            this.lblCurrentVersion.TabIndex = 3;
            this.lblCurrentVersion.Text = "Ihre Version: {0}";
            // 
            // lblNewVersion
            // 
            this.lblNewVersion.AutoSize = true;
            this.lblNewVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewVersion.Location = new System.Drawing.Point(12, 32);
            this.lblNewVersion.Name = "lblNewVersion";
            this.lblNewVersion.Size = new System.Drawing.Size(108, 13);
            this.lblNewVersion.TabIndex = 4;
            this.lblNewVersion.Text = "Neue Version: {0}";
            // 
            // btnInstallUpdate
            // 
            this.btnInstallUpdate.Location = new System.Drawing.Point(12, 368);
            this.btnInstallUpdate.Name = "btnInstallUpdate";
            this.btnInstallUpdate.Size = new System.Drawing.Size(710, 23);
            this.btnInstallUpdate.TabIndex = 0;
            this.btnInstallUpdate.Text = "Neue Version herunterladen und installieren";
            this.btnInstallUpdate.UseVisualStyleBackColor = true;
            this.btnInstallUpdate.Click += new System.EventHandler(this.btnInstallUpdate_Click);
            // 
            // expDownload
            // 
            this.expDownload.ByteDimension = OLKI.Toolbox.DirectoryAndFile.FileSize.Dimension.NoDimension;
            this.expDownload.Location = new System.Drawing.Point(12, 397);
            this.expDownload.MaxValue = ((long)(0));
            this.expDownload.MinimumSize = new System.Drawing.Size(300, 23);
            this.expDownload.Name = "expDownload";
            this.expDownload.ShowDescriptionText = false;
            this.expDownload.Size = new System.Drawing.Size(710, 23);
            this.expDownload.TabIndex = 1;
            this.expDownload.Value = ((long)(-1));
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(734, 461);
            this.Controls.Add(this.btnInstallUpdate);
            this.Controls.Add(this.expDownload);
            this.Controls.Add(this.lblNewVersion);
            this.Controls.Add(this.lblCurrentVersion);
            this.Controls.Add(this.txtChangeLog);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateForm";
            this.ShowInTaskbar = false;
            this.Text = "Eine neue Programmversion ist verfügbar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtChangeLog;
        private System.Windows.Forms.Label lblCurrentVersion;
        private System.Windows.Forms.Label lblNewVersion;
        private Widgets.ExtProgressBar expDownload;
        private System.Windows.Forms.Button btnInstallUpdate;
    }
}