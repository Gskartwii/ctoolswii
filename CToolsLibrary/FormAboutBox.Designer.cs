// CTools library - Library functions for CTools
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

namespace Chadsoft.CTools
{
    partial class FormAboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.okButton = new System.Windows.Forms.Button();
            this.programLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.versionUpdateLabel = new System.Windows.Forms.Label();
            this.pluginListBox = new System.Windows.Forms.ListBox();
            this.pluginIconPictureBox = new System.Windows.Forms.PictureBox();
            this.pluginNameLabel = new System.Windows.Forms.Label();
            this.pluginVersionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pluginIconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(226, 267);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // programLabel
            // 
            this.programLabel.AutoSize = true;
            this.programLabel.Location = new System.Drawing.Point(13, 13);
            this.programLabel.Name = "programLabel";
            this.programLabel.Size = new System.Drawing.Size(74, 13);
            this.programLabel.TabIndex = 1;
            this.programLabel.Text = "CTools Library";
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(28, 41);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(59, 13);
            this.versionLabel.TabIndex = 1;
            this.versionLabel.Text = "Version {0}";
            // 
            // versionUpdateLabel
            // 
            this.versionUpdateLabel.AutoSize = true;
            this.versionUpdateLabel.Location = new System.Drawing.Point(28, 54);
            this.versionUpdateLabel.Name = "versionUpdateLabel";
            this.versionUpdateLabel.Size = new System.Drawing.Size(207, 13);
            this.versionUpdateLabel.TabIndex = 1;
            this.versionUpdateLabel.Text = "There is an update available to version {0}";
            // 
            // pluginListBox
            // 
            this.pluginListBox.FormattingEnabled = true;
            this.pluginListBox.Location = new System.Drawing.Point(31, 70);
            this.pluginListBox.Name = "pluginListBox";
            this.pluginListBox.Size = new System.Drawing.Size(253, 147);
            this.pluginListBox.TabIndex = 2;
            this.pluginListBox.SelectedIndexChanged += new System.EventHandler(this.pluginListBox_SelectedIndexChanged);
            // 
            // pluginIconPictureBox
            // 
            this.pluginIconPictureBox.Location = new System.Drawing.Point(31, 224);
            this.pluginIconPictureBox.Name = "pluginIconPictureBox";
            this.pluginIconPictureBox.Size = new System.Drawing.Size(32, 32);
            this.pluginIconPictureBox.TabIndex = 3;
            this.pluginIconPictureBox.TabStop = false;
            // 
            // pluginNameLabel
            // 
            this.pluginNameLabel.AutoSize = true;
            this.pluginNameLabel.Location = new System.Drawing.Point(69, 224);
            this.pluginNameLabel.Name = "pluginNameLabel";
            this.pluginNameLabel.Size = new System.Drawing.Size(0, 13);
            this.pluginNameLabel.TabIndex = 4;
            // 
            // pluginVersionLabel
            // 
            this.pluginVersionLabel.AutoSize = true;
            this.pluginVersionLabel.Location = new System.Drawing.Point(69, 243);
            this.pluginVersionLabel.Name = "pluginVersionLabel";
            this.pluginVersionLabel.Size = new System.Drawing.Size(0, 13);
            this.pluginVersionLabel.TabIndex = 5;
            // 
            // FormAboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 302);
            this.Controls.Add(this.pluginVersionLabel);
            this.Controls.Add(this.pluginNameLabel);
            this.Controls.Add(this.pluginIconPictureBox);
            this.Controls.Add(this.pluginListBox);
            this.Controls.Add(this.versionUpdateLabel);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.programLabel);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAboutBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Box";
            ((System.ComponentModel.ISupportInitialize)(this.pluginIconPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label programLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label versionUpdateLabel;
        private System.Windows.Forms.ListBox pluginListBox;
        private System.Windows.Forms.PictureBox pluginIconPictureBox;
        private System.Windows.Forms.Label pluginNameLabel;
        private System.Windows.Forms.Label pluginVersionLabel;

    }
}
