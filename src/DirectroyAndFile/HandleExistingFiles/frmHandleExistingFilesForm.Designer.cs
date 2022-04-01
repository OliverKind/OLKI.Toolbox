namespace OLKI.Toolbox.DirectoryAndFile
{
    partial class HandleExistingFilesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HandleExistingFilesForm));
            this.lblActionDoubleFile = new System.Windows.Forms.Label();
            this.rabAction_OverwriteAll = new System.Windows.Forms.RadioButton();
            this.grbAction = new System.Windows.Forms.GroupBox();
            this.rabAction_OverwriteIfDifferentChecksum = new System.Windows.Forms.RadioButton();
            this.cboAction_AddText_Template = new System.Windows.Forms.ComboBox();
            this.btnAction_AddText_Transfair = new System.Windows.Forms.Button();
            this.rabAction_AskAnyTime = new System.Windows.Forms.RadioButton();
            this.rabAction_Skipp = new System.Windows.Forms.RadioButton();
            this.txtAction_AddText_Text = new System.Windows.Forms.TextBox();
            this.rabAction_AddText = new System.Windows.Forms.RadioButton();
            this.rabAction_OverwriteIfDifferentLengthOrNewer = new System.Windows.Forms.RadioButton();
            this.rabAction_OverwriteIfDifferentLength = new System.Windows.Forms.RadioButton();
            this.rabAction_OverwriteIfNewer = new System.Windows.Forms.RadioButton();
            this.chkForAllFollowing = new System.Windows.Forms.CheckBox();
            this.prgSourceProperty = new OLKI.Toolbox.Widgets.ReadOnlyPropertyGrid();
            this.grbSourceProperty = new System.Windows.Forms.GroupBox();
            this.grbDestinationProperty = new System.Windows.Forms.GroupBox();
            this.prgDestinationProperty = new OLKI.Toolbox.Widgets.ReadOnlyPropertyGrid();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblActionDefaultSettings = new System.Windows.Forms.Label();
            this.grbAction.SuspendLayout();
            this.grbSourceProperty.SuspendLayout();
            this.grbDestinationProperty.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblActionDoubleFile
            // 
            this.lblActionDoubleFile.AutoSize = true;
            this.lblActionDoubleFile.Location = new System.Drawing.Point(12, 9);
            this.lblActionDoubleFile.Name = "lblActionDoubleFile";
            this.lblActionDoubleFile.Size = new System.Drawing.Size(276, 13);
            this.lblActionDoubleFile.TabIndex = 0;
            this.lblActionDoubleFile.Text = "Die Datei existiert bereits. Wie soll vorgegangen werden?";
            // 
            // rabAction_OverwriteAll
            // 
            this.rabAction_OverwriteAll.AutoSize = true;
            this.rabAction_OverwriteAll.Location = new System.Drawing.Point(6, 19);
            this.rabAction_OverwriteAll.Name = "rabAction_OverwriteAll";
            this.rabAction_OverwriteAll.Size = new System.Drawing.Size(94, 17);
            this.rabAction_OverwriteAll.TabIndex = 0;
            this.rabAction_OverwriteAll.Text = "Überschreiben";
            this.rabAction_OverwriteAll.UseVisualStyleBackColor = true;
            this.rabAction_OverwriteAll.CheckedChanged += new System.EventHandler(this.rabActionXXX_CheckedChanged);
            // 
            // grbAction
            // 
            this.grbAction.Controls.Add(this.rabAction_OverwriteIfDifferentChecksum);
            this.grbAction.Controls.Add(this.cboAction_AddText_Template);
            this.grbAction.Controls.Add(this.btnAction_AddText_Transfair);
            this.grbAction.Controls.Add(this.rabAction_AskAnyTime);
            this.grbAction.Controls.Add(this.rabAction_Skipp);
            this.grbAction.Controls.Add(this.txtAction_AddText_Text);
            this.grbAction.Controls.Add(this.rabAction_AddText);
            this.grbAction.Controls.Add(this.rabAction_OverwriteIfDifferentLengthOrNewer);
            this.grbAction.Controls.Add(this.rabAction_OverwriteIfDifferentLength);
            this.grbAction.Controls.Add(this.rabAction_OverwriteIfNewer);
            this.grbAction.Controls.Add(this.rabAction_OverwriteAll);
            this.grbAction.Location = new System.Drawing.Point(538, 25);
            this.grbAction.Name = "grbAction";
            this.grbAction.Size = new System.Drawing.Size(514, 203);
            this.grbAction.TabIndex = 4;
            this.grbAction.TabStop = false;
            this.grbAction.Text = "Aktion:";
            // 
            // rabAction_OverwriteIfDifferentChecksum
            // 
            this.rabAction_OverwriteIfDifferentChecksum.AutoSize = true;
            this.rabAction_OverwriteIfDifferentChecksum.Location = new System.Drawing.Point(6, 111);
            this.rabAction_OverwriteIfDifferentChecksum.Name = "rabAction_OverwriteIfDifferentChecksum";
            this.rabAction_OverwriteIfDifferentChecksum.Size = new System.Drawing.Size(272, 17);
            this.rabAction_OverwriteIfDifferentChecksum.TabIndex = 8;
            this.rabAction_OverwriteIfDifferentChecksum.Text = "Überschreiben falls Datei Prüfsumme unterschiedlich";
            this.rabAction_OverwriteIfDifferentChecksum.UseVisualStyleBackColor = true;
            this.rabAction_OverwriteIfDifferentChecksum.CheckedChanged += new System.EventHandler(this.rabActionXXX_CheckedChanged);
            // 
            // cboAction_AddText_Template
            // 
            this.cboAction_AddText_Template.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAction_AddText_Template.FormattingEnabled = true;
            this.cboAction_AddText_Template.Items.AddRange(new object[] {
            "Datei Datum und Uhrzeit der Erstellung - {{$CreationTime}}",
            "Datei Datum und Uhrzeit der letzten Änderung - {{$LastWriteTime}}",
            "Datei Datum und Uhrzeit des letzten Zugriffs - {{$LastAccessTime}}",
            "Aktuelles Datum und Uhrzeit - {{$Now}}"});
            this.cboAction_AddText_Template.Location = new System.Drawing.Point(432, 134);
            this.cboAction_AddText_Template.Name = "cboAction_AddText_Template";
            this.cboAction_AddText_Template.Size = new System.Drawing.Size(76, 21);
            this.cboAction_AddText_Template.TabIndex = 7;
            // 
            // btnAction_AddText_Transfair
            // 
            this.btnAction_AddText_Transfair.Location = new System.Drawing.Point(396, 135);
            this.btnAction_AddText_Transfair.Name = "btnAction_AddText_Transfair";
            this.btnAction_AddText_Transfair.Size = new System.Drawing.Size(30, 20);
            this.btnAction_AddText_Transfair.TabIndex = 6;
            this.btnAction_AddText_Transfair.Text = "<<";
            this.btnAction_AddText_Transfair.UseVisualStyleBackColor = true;
            this.btnAction_AddText_Transfair.Click += new System.EventHandler(this.btnAction_AddText_Transfair_Click);
            // 
            // rabAction_AskAnyTime
            // 
            this.rabAction_AskAnyTime.AutoSize = true;
            this.rabAction_AskAnyTime.Location = new System.Drawing.Point(6, 180);
            this.rabAction_AskAnyTime.Name = "rabAction_AskAnyTime";
            this.rabAction_AskAnyTime.Size = new System.Drawing.Size(81, 17);
            this.rabAction_AskAnyTime.TabIndex = 9;
            this.rabAction_AskAnyTime.Text = "Nachfragen";
            this.rabAction_AskAnyTime.UseVisualStyleBackColor = true;
            this.rabAction_AskAnyTime.CheckedChanged += new System.EventHandler(this.rabActionXXX_CheckedChanged);
            // 
            // rabAction_Skipp
            // 
            this.rabAction_Skipp.AutoSize = true;
            this.rabAction_Skipp.Location = new System.Drawing.Point(6, 157);
            this.rabAction_Skipp.Name = "rabAction_Skipp";
            this.rabAction_Skipp.Size = new System.Drawing.Size(88, 17);
            this.rabAction_Skipp.TabIndex = 8;
            this.rabAction_Skipp.Text = "Überspringen";
            this.rabAction_Skipp.UseVisualStyleBackColor = true;
            this.rabAction_Skipp.CheckedChanged += new System.EventHandler(this.rabActionXXX_CheckedChanged);
            // 
            // txtAction_AddText_Text
            // 
            this.txtAction_AddText_Text.Location = new System.Drawing.Point(213, 135);
            this.txtAction_AddText_Text.Name = "txtAction_AddText_Text";
            this.txtAction_AddText_Text.Size = new System.Drawing.Size(176, 20);
            this.txtAction_AddText_Text.TabIndex = 5;
            this.txtAction_AddText_Text.TextChanged += new System.EventHandler(this.txtAction_AddText_Text_TextChanged);
            // 
            // rabAction_AddText
            // 
            this.rabAction_AddText.AutoSize = true;
            this.rabAction_AddText.Location = new System.Drawing.Point(6, 134);
            this.rabAction_AddText.Name = "rabAction_AddText";
            this.rabAction_AddText.Size = new System.Drawing.Size(201, 17);
            this.rabAction_AddText.TabIndex = 4;
            this.rabAction_AddText.Text = "An vorhandene Datei Text anhängen";
            this.rabAction_AddText.UseVisualStyleBackColor = true;
            this.rabAction_AddText.CheckedChanged += new System.EventHandler(this.rabActionXXX_CheckedChanged);
            // 
            // rabAction_OverwriteIfDifferentLengthOrNewer
            // 
            this.rabAction_OverwriteIfDifferentLengthOrNewer.AutoSize = true;
            this.rabAction_OverwriteIfDifferentLengthOrNewer.Location = new System.Drawing.Point(6, 88);
            this.rabAction_OverwriteIfDifferentLengthOrNewer.Name = "rabAction_OverwriteIfDifferentLengthOrNewer";
            this.rabAction_OverwriteIfDifferentLengthOrNewer.Size = new System.Drawing.Size(338, 17);
            this.rabAction_OverwriteIfDifferentLengthOrNewer.TabIndex = 3;
            this.rabAction_OverwriteIfDifferentLengthOrNewer.Text = "Überschreiben falls Größe unterschiedlich oder Quelldatei aktueller";
            this.rabAction_OverwriteIfDifferentLengthOrNewer.UseVisualStyleBackColor = true;
            this.rabAction_OverwriteIfDifferentLengthOrNewer.CheckedChanged += new System.EventHandler(this.rabActionXXX_CheckedChanged);
            // 
            // rabAction_OverwriteIfDifferentLength
            // 
            this.rabAction_OverwriteIfDifferentLength.AutoSize = true;
            this.rabAction_OverwriteIfDifferentLength.Location = new System.Drawing.Point(6, 65);
            this.rabAction_OverwriteIfDifferentLength.Name = "rabAction_OverwriteIfDifferentLength";
            this.rabAction_OverwriteIfDifferentLength.Size = new System.Drawing.Size(221, 17);
            this.rabAction_OverwriteIfDifferentLength.TabIndex = 2;
            this.rabAction_OverwriteIfDifferentLength.Text = "Überschreiben falls Größe unterschiedlich";
            this.rabAction_OverwriteIfDifferentLength.UseVisualStyleBackColor = true;
            this.rabAction_OverwriteIfDifferentLength.CheckedChanged += new System.EventHandler(this.rabActionXXX_CheckedChanged);
            // 
            // rabAction_OverwriteIfNewer
            // 
            this.rabAction_OverwriteIfNewer.AutoSize = true;
            this.rabAction_OverwriteIfNewer.Location = new System.Drawing.Point(6, 42);
            this.rabAction_OverwriteIfNewer.Name = "rabAction_OverwriteIfNewer";
            this.rabAction_OverwriteIfNewer.Size = new System.Drawing.Size(208, 17);
            this.rabAction_OverwriteIfNewer.TabIndex = 1;
            this.rabAction_OverwriteIfNewer.Text = "Überschreiben falls Quelldatei aktueller";
            this.rabAction_OverwriteIfNewer.UseVisualStyleBackColor = true;
            this.rabAction_OverwriteIfNewer.CheckedChanged += new System.EventHandler(this.rabActionXXX_CheckedChanged);
            // 
            // chkForAllFollowing
            // 
            this.chkForAllFollowing.AutoSize = true;
            this.chkForAllFollowing.Location = new System.Drawing.Point(538, 234);
            this.chkForAllFollowing.Name = "chkForAllFollowing";
            this.chkForAllFollowing.Size = new System.Drawing.Size(255, 17);
            this.chkForAllFollowing.TabIndex = 5;
            this.chkForAllFollowing.Text = "Diese Aktion für zukünftige Dateien übernehmen";
            this.chkForAllFollowing.UseVisualStyleBackColor = true;
            // 
            // prgSourceProperty
            // 
            this.prgSourceProperty.HelpVisible = false;
            this.prgSourceProperty.LineColor = System.Drawing.SystemColors.ControlDark;
            this.prgSourceProperty.Location = new System.Drawing.Point(6, 19);
            this.prgSourceProperty.Name = "prgSourceProperty";
            this.prgSourceProperty.ReadOnly = true;
            this.prgSourceProperty.Size = new System.Drawing.Size(245, 295);
            this.prgSourceProperty.TabIndex = 0;
            this.prgSourceProperty.ToolbarVisible = false;
            // 
            // grbSourceProperty
            // 
            this.grbSourceProperty.Controls.Add(this.prgSourceProperty);
            this.grbSourceProperty.Location = new System.Drawing.Point(12, 25);
            this.grbSourceProperty.Name = "grbSourceProperty";
            this.grbSourceProperty.Size = new System.Drawing.Size(257, 320);
            this.grbSourceProperty.TabIndex = 2;
            this.grbSourceProperty.TabStop = false;
            this.grbSourceProperty.Text = "Quelldatei";
            // 
            // grbDestinationProperty
            // 
            this.grbDestinationProperty.Controls.Add(this.prgDestinationProperty);
            this.grbDestinationProperty.Location = new System.Drawing.Point(275, 25);
            this.grbDestinationProperty.Name = "grbDestinationProperty";
            this.grbDestinationProperty.Size = new System.Drawing.Size(257, 320);
            this.grbDestinationProperty.TabIndex = 3;
            this.grbDestinationProperty.TabStop = false;
            this.grbDestinationProperty.Text = "Zieldatei";
            // 
            // prgDestinationProperty
            // 
            this.prgDestinationProperty.HelpVisible = false;
            this.prgDestinationProperty.LineColor = System.Drawing.SystemColors.ControlDark;
            this.prgDestinationProperty.Location = new System.Drawing.Point(6, 19);
            this.prgDestinationProperty.Name = "prgDestinationProperty";
            this.prgDestinationProperty.ReadOnly = true;
            this.prgDestinationProperty.Size = new System.Drawing.Size(245, 295);
            this.prgDestinationProperty.TabIndex = 0;
            this.prgDestinationProperty.ToolbarVisible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(942, 322);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(538, 322);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(110, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblActionDefaultSettings
            // 
            this.lblActionDefaultSettings.AutoSize = true;
            this.lblActionDefaultSettings.Location = new System.Drawing.Point(535, 9);
            this.lblActionDefaultSettings.Name = "lblActionDefaultSettings";
            this.lblActionDefaultSettings.Size = new System.Drawing.Size(353, 13);
            this.lblActionDefaultSettings.TabIndex = 1;
            this.lblActionDefaultSettings.Text = "Welche Aktion soll angewendet werden wenn eine Datei bereits existiert?";
            // 
            // HandleExistingFilesForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1064, 357);
            this.Controls.Add(this.lblActionDefaultSettings);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grbDestinationProperty);
            this.Controls.Add(this.grbSourceProperty);
            this.Controls.Add(this.chkForAllFollowing);
            this.Controls.Add(this.grbAction);
            this.Controls.Add(this.lblActionDoubleFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HandleExistingFilesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Aktion für vorhandene Datei(en)";
            this.grbAction.ResumeLayout(false);
            this.grbAction.PerformLayout();
            this.grbSourceProperty.ResumeLayout(false);
            this.grbDestinationProperty.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblActionDoubleFile;
        private System.Windows.Forms.GroupBox grbAction;
        private System.Windows.Forms.TextBox txtAction_AddText_Text;
        private System.Windows.Forms.CheckBox chkForAllFollowing;
        private System.Windows.Forms.ComboBox cboAction_AddText_Template;
        private System.Windows.Forms.Button btnAction_AddText_Transfair;
        private Widgets.ReadOnlyPropertyGrid prgSourceProperty;
        private System.Windows.Forms.GroupBox grbSourceProperty;
        private System.Windows.Forms.GroupBox grbDestinationProperty;
        private Widgets.ReadOnlyPropertyGrid prgDestinationProperty;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblActionDefaultSettings;
        private System.Windows.Forms.RadioButton rabAction_OverwriteAll;
        private System.Windows.Forms.RadioButton rabAction_Skipp;
        private System.Windows.Forms.RadioButton rabAction_AddText;
        private System.Windows.Forms.RadioButton rabAction_OverwriteIfDifferentLengthOrNewer;
        private System.Windows.Forms.RadioButton rabAction_OverwriteIfDifferentLength;
        private System.Windows.Forms.RadioButton rabAction_OverwriteIfNewer;
        private System.Windows.Forms.RadioButton rabAction_AskAnyTime;
        private System.Windows.Forms.RadioButton rabAction_OverwriteIfDifferentChecksum;
    }
}