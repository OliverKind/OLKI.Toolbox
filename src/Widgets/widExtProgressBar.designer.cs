
namespace OLKI.Toolbox.Widgets
{
    partial class ExtProgressBar
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.cboByteDime = new System.Windows.Forms.ComboBox();
            this.txtBytesNum = new System.Windows.Forms.TextBox();
            this.txtBytesPer = new System.Windows.Forms.TextBox();
            this.pbaProgress = new System.Windows.Forms.ProgressBar();
            this.txtDescript = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cboByteDime
            // 
            this.cboByteDime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboByteDime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboByteDime.DropDownWidth = 65;
            this.cboByteDime.FormattingEnabled = true;
            this.cboByteDime.Location = new System.Drawing.Point(668, 28);
            this.cboByteDime.Name = "cboByteDime";
            this.cboByteDime.Size = new System.Drawing.Size(57, 21);
            this.cboByteDime.TabIndex = 25;
            this.cboByteDime.SelectedIndexChanged += new System.EventHandler(this.cboByteDimension_SelectedIndexChanged);
            this.cboByteDime.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cboByteDime_MouseDown);
            // 
            // txtBytesNum
            // 
            this.txtBytesNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBytesNum.Location = new System.Drawing.Point(547, 29);
            this.txtBytesNum.Name = "txtBytesNum";
            this.txtBytesNum.ReadOnly = true;
            this.txtBytesNum.Size = new System.Drawing.Size(115, 20);
            this.txtBytesNum.TabIndex = 24;
            // 
            // txtBytesPer
            // 
            this.txtBytesPer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBytesPer.Location = new System.Drawing.Point(506, 29);
            this.txtBytesPer.Name = "txtBytesPer";
            this.txtBytesPer.ReadOnly = true;
            this.txtBytesPer.Size = new System.Drawing.Size(35, 20);
            this.txtBytesPer.TabIndex = 23;
            // 
            // pbaProgress
            // 
            this.pbaProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbaProgress.Location = new System.Drawing.Point(0, 26);
            this.pbaProgress.Name = "pbaProgress";
            this.pbaProgress.Size = new System.Drawing.Size(500, 23);
            this.pbaProgress.Step = 1;
            this.pbaProgress.TabIndex = 22;
            // 
            // txtDescript
            // 
            this.txtDescript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescript.Location = new System.Drawing.Point(0, 0);
            this.txtDescript.Name = "txtDescript";
            this.txtDescript.ReadOnly = true;
            this.txtDescript.Size = new System.Drawing.Size(725, 20);
            this.txtDescript.TabIndex = 26;
            // 
            // ExtProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtDescript);
            this.Controls.Add(this.cboByteDime);
            this.Controls.Add(this.txtBytesNum);
            this.Controls.Add(this.txtBytesPer);
            this.Controls.Add(this.pbaProgress);
            this.MinimumSize = new System.Drawing.Size(300, 23);
            this.Name = "ExtProgressBar";
            this.Size = new System.Drawing.Size(725, 49);
            this.Resize += new System.EventHandler(this.ProgressbarEx_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ComboBox cboByteDime;
        internal System.Windows.Forms.TextBox txtBytesNum;
        internal System.Windows.Forms.TextBox txtBytesPer;
        internal System.Windows.Forms.ProgressBar pbaProgress;
        private System.Windows.Forms.TextBox txtDescript;
    }
}
