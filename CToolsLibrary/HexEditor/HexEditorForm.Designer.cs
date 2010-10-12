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

namespace Chadsoft.CTools.HexEditor
{
    partial class HexEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HexEditorForm));
            this.mainToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.statusToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.overwriteStatusToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.displayPanel = new System.Windows.Forms.Panel();
            this.mainVScrollBar = new System.Windows.Forms.VScrollBar();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gotoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overwriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.mainToolStripContainer.ContentPanel.SuspendLayout();
            this.mainToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.mainToolStripContainer.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainToolStripContainer
            // 
            // 
            // mainToolStripContainer.BottomToolStripPanel
            // 
            this.mainToolStripContainer.BottomToolStripPanel.Controls.Add(this.mainStatusStrip);
            // 
            // mainToolStripContainer.ContentPanel
            // 
            this.mainToolStripContainer.ContentPanel.Controls.Add(this.displayPanel);
            this.mainToolStripContainer.ContentPanel.Controls.Add(this.mainVScrollBar);
            this.mainToolStripContainer.ContentPanel.Size = new System.Drawing.Size(624, 394);
            this.mainToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.mainToolStripContainer.Name = "mainToolStripContainer";
            this.mainToolStripContainer.Size = new System.Drawing.Size(624, 442);
            this.mainToolStripContainer.TabIndex = 0;
            this.mainToolStripContainer.Text = "toolStripContainer1";
            // 
            // mainToolStripContainer.TopToolStripPanel
            // 
            this.mainToolStripContainer.TopToolStripPanel.Controls.Add(this.mainMenuStrip);
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusToolStripStatusLabel,
            this.overwriteStatusToolStripStatusLabel});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 0);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(624, 24);
            this.mainStatusStrip.TabIndex = 0;
            // 
            // statusToolStripStatusLabel
            // 
            this.statusToolStripStatusLabel.Name = "statusToolStripStatusLabel";
            this.statusToolStripStatusLabel.Size = new System.Drawing.Size(583, 19);
            this.statusToolStripStatusLabel.Spring = true;
            this.statusToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // overwriteStatusToolStripStatusLabel
            // 
            this.overwriteStatusToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.overwriteStatusToolStripStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.overwriteStatusToolStripStatusLabel.Name = "overwriteStatusToolStripStatusLabel";
            this.overwriteStatusToolStripStatusLabel.Size = new System.Drawing.Size(26, 19);
            this.overwriteStatusToolStripStatusLabel.Text = "Ins";
            // 
            // displayPanel
            // 
            this.displayPanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.displayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayPanel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayPanel.Location = new System.Drawing.Point(0, 0);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(607, 394);
            this.displayPanel.TabIndex = 1;
            this.displayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.displayPanel_Paint);
            this.displayPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.displayPanel_MouseUp);
            this.displayPanel.Resize += new System.EventHandler(this.displayPanel_Resize);
            // 
            // mainVScrollBar
            // 
            this.mainVScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.mainVScrollBar.Location = new System.Drawing.Point(607, 0);
            this.mainVScrollBar.Name = "mainVScrollBar";
            this.mainVScrollBar.Size = new System.Drawing.Size(17, 394);
            this.mainVScrollBar.TabIndex = 0;
            this.mainVScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.mainVScrollBar_Scroll);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(624, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.fileToolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::Chadsoft.CTools.Properties.Resources.disk;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // fileToolStripSeparator1
            // 
            this.fileToolStripSeparator1.Name = "fileToolStripSeparator1";
            this.fileToolStripSeparator1.Size = new System.Drawing.Size(135, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gotoToolStripMenuItem,
            this.overwriteToolStripMenuItem,
            this.toolStripSeparator1,
            this.deleteRangeToolStripMenuItem,
            this.copyRangeToolStripMenuItem,
            this.insertRangeToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // gotoToolStripMenuItem
            // 
            this.gotoToolStripMenuItem.Image = global::Chadsoft.CTools.Properties.Resources.arrow_right;
            this.gotoToolStripMenuItem.Name = "gotoToolStripMenuItem";
            this.gotoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.gotoToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.gotoToolStripMenuItem.Text = "&Goto";
            this.gotoToolStripMenuItem.Click += new System.EventHandler(this.gotoToolStripMenuItem_Click);
            // 
            // overwriteToolStripMenuItem
            // 
            this.overwriteToolStripMenuItem.Name = "overwriteToolStripMenuItem";
            this.overwriteToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.overwriteToolStripMenuItem.Text = "Overwrite Mode";
            this.overwriteToolStripMenuItem.Click += new System.EventHandler(this.overwriteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(156, 6);
            this.toolStripSeparator1.Visible = false;
            // 
            // deleteRangeToolStripMenuItem
            // 
            this.deleteRangeToolStripMenuItem.Name = "deleteRangeToolStripMenuItem";
            this.deleteRangeToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.deleteRangeToolStripMenuItem.Text = "Delete Range";
            this.deleteRangeToolStripMenuItem.Visible = false;
            // 
            // copyRangeToolStripMenuItem
            // 
            this.copyRangeToolStripMenuItem.Name = "copyRangeToolStripMenuItem";
            this.copyRangeToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.copyRangeToolStripMenuItem.Text = "Copy Range";
            this.copyRangeToolStripMenuItem.Visible = false;
            // 
            // insertRangeToolStripMenuItem
            // 
            this.insertRangeToolStripMenuItem.Name = "insertRangeToolStripMenuItem";
            this.insertRangeToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.insertRangeToolStripMenuItem.Text = "Insert Range";
            this.insertRangeToolStripMenuItem.Visible = false;
            // 
            // HexEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.mainToolStripContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.mainMenuStrip;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "HexEditorForm";
            this.Text = "Hex Editor - {0}";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HexEditorForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HexEditorForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HexEditorForm_KeyPress);
            this.mainToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.mainToolStripContainer.BottomToolStripPanel.PerformLayout();
            this.mainToolStripContainer.ContentPanel.ResumeLayout(false);
            this.mainToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.mainToolStripContainer.TopToolStripPanel.PerformLayout();
            this.mainToolStripContainer.ResumeLayout(false);
            this.mainToolStripContainer.PerformLayout();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer mainToolStripContainer;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel overwriteStatusToolStripStatusLabel;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator fileToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem overwriteToolStripMenuItem;
        private System.Windows.Forms.Panel displayPanel;
        private System.Windows.Forms.VScrollBar mainVScrollBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem deleteRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gotoToolStripMenuItem;
    }
}