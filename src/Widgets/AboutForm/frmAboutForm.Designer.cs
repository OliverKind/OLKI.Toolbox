namespace OLKI.Toolbox.Widgets.AboutForm
{
    partial class AboutForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.pbxLogoApplication = new System.Windows.Forms.PictureBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtCredits = new System.Windows.Forms.TextBox();
            this.pbxLogoFile = new System.Windows.Forms.PictureBox();
            this.btnGoToLicenses = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.chkCheckUpdatesOnStartup = new System.Windows.Forms.CheckBox();
            this.lblCheckUpdates = new System.Windows.Forms.LinkLabel();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogoApplication)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogoFile)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxLogoApplication
            // 
            this.pbxLogoApplication.Location = new System.Drawing.Point(12, 12);
            this.pbxLogoApplication.Name = "pbxLogoApplication";
            this.pbxLogoApplication.Size = new System.Drawing.Size(125, 230);
            this.pbxLogoApplication.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxLogoApplication.TabIndex = 13;
            this.pbxLogoApplication.TabStop = false;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(146, 116);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDescription.Size = new System.Drawing.Size(446, 126);
            this.txtDescription.TabIndex = 6;
            this.txtDescription.TabStop = false;
            this.txtDescription.Text = "Beschreibung";
            this.txtDescription.WordWrap = false;
            // 
            // txtCredits
            // 
            this.txtCredits.Location = new System.Drawing.Point(146, 248);
            this.txtCredits.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.txtCredits.Multiline = true;
            this.txtCredits.Name = "txtCredits";
            this.txtCredits.ReadOnly = true;
            this.txtCredits.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCredits.Size = new System.Drawing.Size(446, 194);
            this.txtCredits.TabIndex = 7;
            this.txtCredits.TabStop = false;
            this.txtCredits.Text = resources.GetString("txtCredits.Text");
            this.txtCredits.WordWrap = false;
            // 
            // pbxLogoFile
            // 
            this.pbxLogoFile.Location = new System.Drawing.Point(12, 248);
            this.pbxLogoFile.Name = "pbxLogoFile";
            this.pbxLogoFile.Size = new System.Drawing.Size(125, 194);
            this.pbxLogoFile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxLogoFile.TabIndex = 16;
            this.pbxLogoFile.TabStop = false;
            // 
            // btnGoToLicenses
            // 
            this.btnGoToLicenses.Location = new System.Drawing.Point(146, 448);
            this.btnGoToLicenses.Name = "btnGoToLicenses";
            this.btnGoToLicenses.Size = new System.Drawing.Size(75, 20);
            this.btnGoToLicenses.TabIndex = 8;
            this.btnGoToLicenses.Text = "&Lizenzen";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(517, 448);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 20);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "&OK";
            // 
            // chkCheckUpdatesOnStartup
            // 
            this.chkCheckUpdatesOnStartup.AutoSize = true;
            this.chkCheckUpdatesOnStartup.Location = new System.Drawing.Point(485, 38);
            this.chkCheckUpdatesOnStartup.Name = "chkCheckUpdatesOnStartup";
            this.chkCheckUpdatesOnStartup.Size = new System.Drawing.Size(107, 17);
            this.chkCheckUpdatesOnStartup.TabIndex = 3;
            this.chkCheckUpdatesOnStartup.Text = "Beim Start prüfen";
            this.chkCheckUpdatesOnStartup.UseVisualStyleBackColor = true;
            this.chkCheckUpdatesOnStartup.Visible = false;
            // 
            // lblCheckUpdates
            // 
            this.lblCheckUpdates.AutoSize = true;
            this.lblCheckUpdates.Location = new System.Drawing.Point(394, 38);
            this.lblCheckUpdates.Name = "lblCheckUpdates";
            this.lblCheckUpdates.Size = new System.Drawing.Size(85, 13);
            this.lblCheckUpdates.TabIndex = 2;
            this.lblCheckUpdates.TabStop = true;
            this.lblCheckUpdates.Text = "Updates suchen";
            this.lblCheckUpdates.Visible = false;
            // 
            // lblProductName
            // 
            this.lblProductName.Location = new System.Drawing.Point(146, 12);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(446, 17);
            this.lblProductName.TabIndex = 0;
            this.lblProductName.Text = "Progamm: {0}";
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(146, 38);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(242, 17);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "Version: {0} ";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCopyright
            // 
            this.lblCopyright.Location = new System.Drawing.Point(146, 64);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(446, 17);
            this.lblCopyright.TabIndex = 4;
            this.lblCopyright.Text = "Copyright: {0}";
            this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.Location = new System.Drawing.Point(146, 90);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(446, 17);
            this.lblCompanyName.TabIndex = 5;
            this.lblCompanyName.Text = "Entwickelt durch: {0}";
            this.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 480);
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.lblCheckUpdates);
            this.Controls.Add(this.chkCheckUpdatesOnStartup);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnGoToLicenses);
            this.Controls.Add(this.pbxLogoFile);
            this.Controls.Add(this.txtCredits);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.pbxLogoApplication);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Info über {0}";
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogoApplication)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogoFile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbxLogoApplication;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtCredits;
        private System.Windows.Forms.PictureBox pbxLogoFile;
        private System.Windows.Forms.Button btnGoToLicenses;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkCheckUpdatesOnStartup;
        private System.Windows.Forms.LinkLabel lblCheckUpdates;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblCompanyName;
    }
}
