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
            this.btnClose = new System.Windows.Forms.Button();
            this.lblProductName = new System.Windows.Forms.Label();
            this.tblLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pbxLogoFile = new System.Windows.Forms.PictureBox();
            this.txtCredits = new System.Windows.Forms.TextBox();
            this.pbxLogoApplication = new System.Windows.Forms.PictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnGoToLicenses = new System.Windows.Forms.Button();
            this.tblLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogoFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogoApplication)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(508, 441);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 20);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "&OK";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblProductName
            // 
            this.tblLayoutPanel.SetColumnSpan(this.lblProductName, 2);
            this.lblProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductName.Location = new System.Drawing.Point(137, 0);
            this.lblProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lblProductName.MaximumSize = new System.Drawing.Size(0, 17);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(446, 17);
            this.lblProductName.TabIndex = 0;
            this.lblProductName.Text = "Progamm: {0}";
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tblLayoutPanel
            // 
            this.tblLayoutPanel.ColumnCount = 3;
            this.tblLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 131F));
            this.tblLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tblLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayoutPanel.Controls.Add(this.pbxLogoFile, 0, 5);
            this.tblLayoutPanel.Controls.Add(this.txtCredits, 1, 5);
            this.tblLayoutPanel.Controls.Add(this.pbxLogoApplication, 0, 0);
            this.tblLayoutPanel.Controls.Add(this.lblProductName, 1, 0);
            this.tblLayoutPanel.Controls.Add(this.lblVersion, 1, 1);
            this.tblLayoutPanel.Controls.Add(this.lblCopyright, 1, 2);
            this.tblLayoutPanel.Controls.Add(this.lblCompanyName, 1, 3);
            this.tblLayoutPanel.Controls.Add(this.txtDescription, 1, 4);
            this.tblLayoutPanel.Controls.Add(this.btnClose, 2, 6);
            this.tblLayoutPanel.Controls.Add(this.btnGoToLicenses, 1, 6);
            this.tblLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutPanel.Location = new System.Drawing.Point(9, 9);
            this.tblLayoutPanel.Name = "tblLayoutPanel";
            this.tblLayoutPanel.RowCount = 7;
            this.tblLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tblLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblLayoutPanel.Size = new System.Drawing.Size(586, 464);
            this.tblLayoutPanel.TabIndex = 0;
            // 
            // pbxLogoFile
            // 
            this.pbxLogoFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxLogoFile.Location = new System.Drawing.Point(3, 239);
            this.pbxLogoFile.Name = "pbxLogoFile";
            this.pbxLogoFile.Size = new System.Drawing.Size(125, 194);
            this.pbxLogoFile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxLogoFile.TabIndex = 13;
            this.pbxLogoFile.TabStop = false;
            // 
            // txtCredits
            // 
            this.tblLayoutPanel.SetColumnSpan(this.txtCredits, 2);
            this.txtCredits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCredits.Location = new System.Drawing.Point(137, 239);
            this.txtCredits.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.txtCredits.Multiline = true;
            this.txtCredits.Name = "txtCredits";
            this.txtCredits.ReadOnly = true;
            this.txtCredits.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCredits.Size = new System.Drawing.Size(446, 194);
            this.txtCredits.TabIndex = 5;
            this.txtCredits.TabStop = false;
            this.txtCredits.Text = resources.GetString("txtCredits.Text");
            this.txtCredits.WordWrap = false;
            // 
            // pbxLogoApplication
            // 
            this.pbxLogoApplication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxLogoApplication.Location = new System.Drawing.Point(3, 3);
            this.pbxLogoApplication.Name = "pbxLogoApplication";
            this.tblLayoutPanel.SetRowSpan(this.pbxLogoApplication, 5);
            this.pbxLogoApplication.Size = new System.Drawing.Size(125, 230);
            this.pbxLogoApplication.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxLogoApplication.TabIndex = 12;
            this.pbxLogoApplication.TabStop = false;
            // 
            // lblVersion
            // 
            this.tblLayoutPanel.SetColumnSpan(this.lblVersion, 2);
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVersion.Location = new System.Drawing.Point(137, 26);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lblVersion.MaximumSize = new System.Drawing.Size(0, 17);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(446, 17);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "Version: {0} ";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCopyright
            // 
            this.tblLayoutPanel.SetColumnSpan(this.lblCopyright, 2);
            this.lblCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCopyright.Location = new System.Drawing.Point(137, 52);
            this.lblCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lblCopyright.MaximumSize = new System.Drawing.Size(0, 17);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(446, 17);
            this.lblCopyright.TabIndex = 2;
            this.lblCopyright.Text = "Copyright: {0}";
            this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCompanyName
            // 
            this.tblLayoutPanel.SetColumnSpan(this.lblCompanyName, 2);
            this.lblCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCompanyName.Location = new System.Drawing.Point(137, 78);
            this.lblCompanyName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lblCompanyName.MaximumSize = new System.Drawing.Size(0, 17);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(446, 17);
            this.lblCompanyName.TabIndex = 3;
            this.lblCompanyName.Text = "Entwickelt durch: {0}";
            this.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDescription
            // 
            this.tblLayoutPanel.SetColumnSpan(this.txtDescription, 2);
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(137, 107);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDescription.Size = new System.Drawing.Size(446, 126);
            this.txtDescription.TabIndex = 4;
            this.txtDescription.TabStop = false;
            this.txtDescription.Text = "Beschreibung";
            this.txtDescription.WordWrap = false;
            // 
            // btnGoToLicenses
            // 
            this.btnGoToLicenses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGoToLicenses.Location = new System.Drawing.Point(134, 441);
            this.btnGoToLicenses.Name = "btnGoToLicenses";
            this.btnGoToLicenses.Size = new System.Drawing.Size(75, 20);
            this.btnGoToLicenses.TabIndex = 14;
            this.btnGoToLicenses.Text = "&Lizenzen";
            this.btnGoToLicenses.Click += new System.EventHandler(this.btnGoToLicenses_Click);
            // 
            // AboutForm
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(604, 482);
            this.Controls.Add(this.tblLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Info über {0}";
            this.tblLayoutPanel.ResumeLayout(false);
            this.tblLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogoFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogoApplication)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.TableLayoutPanel tblLayoutPanel;
        private System.Windows.Forms.TextBox txtCredits;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.PictureBox pbxLogoApplication;
        private System.Windows.Forms.PictureBox pbxLogoFile;
        private System.Windows.Forms.Button btnGoToLicenses;

    }
}
