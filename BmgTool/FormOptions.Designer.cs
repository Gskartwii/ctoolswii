// CTools bmg tool - Text editing service for CTools
// Copyright (C) 2010 Chadderz

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace Chadsoft.CTools.Bmg
{
    partial class FormOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOptions));
            this.optionsTabControl = new System.Windows.Forms.TabControl();
            this.generalTabPage = new System.Windows.Forms.TabPage();
            this.lineEndingsComboBox = new System.Windows.Forms.ComboBox();
            this.lineEndingsLabel = new System.Windows.Forms.Label();
            this.pleaceHoldersCheckBox = new System.Windows.Forms.CheckBox();
            this.okButton = new System.Windows.Forms.Button();
            this.optionsTabControl.SuspendLayout();
            this.generalTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // optionsTabControl
            // 
            this.optionsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsTabControl.Controls.Add(this.generalTabPage);
            this.optionsTabControl.Location = new System.Drawing.Point(12, 12);
            this.optionsTabControl.Name = "optionsTabControl";
            this.optionsTabControl.SelectedIndex = 0;
            this.optionsTabControl.Size = new System.Drawing.Size(290, 241);
            this.optionsTabControl.TabIndex = 0;
            // 
            // generalTabPage
            // 
            this.generalTabPage.Controls.Add(this.lineEndingsComboBox);
            this.generalTabPage.Controls.Add(this.lineEndingsLabel);
            this.generalTabPage.Controls.Add(this.pleaceHoldersCheckBox);
            this.generalTabPage.Location = new System.Drawing.Point(4, 22);
            this.generalTabPage.Name = "generalTabPage";
            this.generalTabPage.Padding = new System.Windows.Forms.Padding(15);
            this.generalTabPage.Size = new System.Drawing.Size(282, 215);
            this.generalTabPage.TabIndex = 0;
            this.generalTabPage.Text = "General";
            this.generalTabPage.UseVisualStyleBackColor = true;
            // 
            // lineEndingsComboBox
            // 
            this.lineEndingsComboBox.FormattingEnabled = true;
            this.lineEndingsComboBox.Items.AddRange(new object[] {
            "Default",
            "LF",
            "CR",
            "CR+LF"});
            this.lineEndingsComboBox.Location = new System.Drawing.Point(95, 18);
            this.lineEndingsComboBox.Name = "lineEndingsComboBox";
            this.lineEndingsComboBox.Size = new System.Drawing.Size(121, 21);
            this.lineEndingsComboBox.TabIndex = 5;
            this.lineEndingsComboBox.SelectedIndexChanged += new System.EventHandler(this.lineEndingsComboBox_SelectedIndexChanged);
            // 
            // lineEndingsLabel
            // 
            this.lineEndingsLabel.AutoSize = true;
            this.lineEndingsLabel.Location = new System.Drawing.Point(18, 21);
            this.lineEndingsLabel.Name = "lineEndingsLabel";
            this.lineEndingsLabel.Size = new System.Drawing.Size(71, 13);
            this.lineEndingsLabel.TabIndex = 4;
            this.lineEndingsLabel.Text = "Line Endings:";
            // 
            // pleaceHoldersCheckBox
            // 
            this.pleaceHoldersCheckBox.AutoSize = true;
            this.pleaceHoldersCheckBox.Checked = global::Chadsoft.CTools.Bmg.Properties.Settings.Default.ReplaceSequences;
            this.pleaceHoldersCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pleaceHoldersCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chadsoft.CTools.Bmg.Properties.Settings.Default, "ReplaceSequences", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pleaceHoldersCheckBox.Location = new System.Drawing.Point(21, 45);
            this.pleaceHoldersCheckBox.Name = "pleaceHoldersCheckBox";
            this.pleaceHoldersCheckBox.Size = new System.Drawing.Size(117, 17);
            this.pleaceHoldersCheckBox.TabIndex = 3;
            this.pleaceHoldersCheckBox.Text = "Show Placeholders";
            this.pleaceHoldersCheckBox.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(227, 259);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 292);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.optionsTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOptions";
            this.Text = "Options";
            this.optionsTabControl.ResumeLayout(false);
            this.generalTabPage.ResumeLayout(false);
            this.generalTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl optionsTabControl;
        private System.Windows.Forms.TabPage generalTabPage;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ComboBox lineEndingsComboBox;
        private System.Windows.Forms.Label lineEndingsLabel;
        private System.Windows.Forms.CheckBox pleaceHoldersCheckBox;
    }
}