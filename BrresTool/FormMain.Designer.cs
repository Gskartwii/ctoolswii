namespace Chadsoft.CTools.Brres
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.filesViewerSplitContainer = new System.Windows.Forms.SplitContainer();
            this.fileTreeView = new System.Windows.Forms.TreeView();
            this.treeViewImageList = new System.Windows.Forms.ImageList(this.components);
            this.filesHeaderLabel = new System.Windows.Forms.Label();
            this.viewerOptionsSplitContainer = new System.Windows.Forms.SplitContainer();
            this.viewerSettingsSplitContainer = new System.Windows.Forms.SplitContainer();
            this.viewerPanel = new System.Windows.Forms.Panel();
            this.viewerHeaderLabel = new System.Windows.Forms.Label();
            this.viewerSettingsHeaderLabel = new System.Windows.Forms.Label();
            this.optionsHeaderLabel = new System.Windows.Forms.Label();
            this.FileSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.FileOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.modelDisplayPanel = new System.Windows.Forms.Panel();
            this.imageDataEditorControl = new Chadsoft.CTools.Image.ImageDataEditorControl();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewerSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filesViewerSplitContainer)).BeginInit();
            this.filesViewerSplitContainer.Panel1.SuspendLayout();
            this.filesViewerSplitContainer.Panel2.SuspendLayout();
            this.filesViewerSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewerOptionsSplitContainer)).BeginInit();
            this.viewerOptionsSplitContainer.Panel1.SuspendLayout();
            this.viewerOptionsSplitContainer.Panel2.SuspendLayout();
            this.viewerOptionsSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewerSettingsSplitContainer)).BeginInit();
            this.viewerSettingsSplitContainer.Panel1.SuspendLayout();
            this.viewerSettingsSplitContainer.Panel2.SuspendLayout();
            this.viewerSettingsSplitContainer.SuspendLayout();
            this.viewerPanel.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(792, 24);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::Chadsoft.CTools.Brres.Properties.Resources.page;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::Chadsoft.CTools.Brres.Properties.Resources.folder;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(137, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::Chadsoft.CTools.Brres.Properties.Resources.disk;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(137, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 24);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(792, 25);
            this.mainToolStrip.Stretch = true;
            this.mainToolStrip.TabIndex = 2;
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = global::Chadsoft.CTools.Brres.Properties.Resources.page;
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "New";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = global::Chadsoft.CTools.Brres.Properties.Resources.folder;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "Open";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = global::Chadsoft.CTools.Brres.Properties.Resources.disk;
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // filesViewerSplitContainer
            // 
            this.filesViewerSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filesViewerSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.filesViewerSplitContainer.Location = new System.Drawing.Point(0, 49);
            this.filesViewerSplitContainer.Name = "filesViewerSplitContainer";
            // 
            // filesViewerSplitContainer.Panel1
            // 
            this.filesViewerSplitContainer.Panel1.Controls.Add(this.fileTreeView);
            this.filesViewerSplitContainer.Panel1.Controls.Add(this.filesHeaderLabel);
            // 
            // filesViewerSplitContainer.Panel2
            // 
            this.filesViewerSplitContainer.Panel2.Controls.Add(this.viewerOptionsSplitContainer);
            this.filesViewerSplitContainer.Size = new System.Drawing.Size(792, 502);
            this.filesViewerSplitContainer.SplitterDistance = 187;
            this.filesViewerSplitContainer.TabIndex = 3;
            // 
            // fileTreeView
            // 
            this.fileTreeView.AllowDrop = true;
            this.fileTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileTreeView.ImageIndex = 0;
            this.fileTreeView.ImageList = this.treeViewImageList;
            this.fileTreeView.LabelEdit = true;
            this.fileTreeView.Location = new System.Drawing.Point(0, 23);
            this.fileTreeView.Name = "fileTreeView";
            this.fileTreeView.SelectedImageIndex = 0;
            this.fileTreeView.Size = new System.Drawing.Size(187, 479);
            this.fileTreeView.TabIndex = 5;
            this.fileTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.fileTreeView_AfterSelect);
            // 
            // treeViewImageList
            // 
            this.treeViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeViewImageList.ImageStream")));
            this.treeViewImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.treeViewImageList.Images.SetKeyName(0, "folder.png");
            this.treeViewImageList.Images.SetKeyName(1, "page_gear.png");
            this.treeViewImageList.Images.SetKeyName(2, "TEX0");
            this.treeViewImageList.Images.SetKeyName(3, "MDL0");
            this.treeViewImageList.Images.SetKeyName(4, "CHR0");
            // 
            // filesHeaderLabel
            // 
            this.filesHeaderLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.filesHeaderLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.filesHeaderLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.filesHeaderLabel.Location = new System.Drawing.Point(0, 0);
            this.filesHeaderLabel.Name = "filesHeaderLabel";
            this.filesHeaderLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.filesHeaderLabel.Size = new System.Drawing.Size(187, 23);
            this.filesHeaderLabel.TabIndex = 1;
            this.filesHeaderLabel.Text = "Files";
            this.filesHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // viewerOptionsSplitContainer
            // 
            this.viewerOptionsSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewerOptionsSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.viewerOptionsSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.viewerOptionsSplitContainer.Name = "viewerOptionsSplitContainer";
            // 
            // viewerOptionsSplitContainer.Panel1
            // 
            this.viewerOptionsSplitContainer.Panel1.Controls.Add(this.viewerSettingsSplitContainer);
            // 
            // viewerOptionsSplitContainer.Panel2
            // 
            this.viewerOptionsSplitContainer.Panel2.Controls.Add(this.optionsHeaderLabel);
            this.viewerOptionsSplitContainer.Size = new System.Drawing.Size(601, 502);
            this.viewerOptionsSplitContainer.SplitterDistance = 397;
            this.viewerOptionsSplitContainer.TabIndex = 0;
            // 
            // viewerSettingsSplitContainer
            // 
            this.viewerSettingsSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewerSettingsSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.viewerSettingsSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.viewerSettingsSplitContainer.Name = "viewerSettingsSplitContainer";
            this.viewerSettingsSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // viewerSettingsSplitContainer.Panel1
            // 
            this.viewerSettingsSplitContainer.Panel1.Controls.Add(this.viewerPanel);
            this.viewerSettingsSplitContainer.Panel1.Controls.Add(this.viewerHeaderLabel);
            // 
            // viewerSettingsSplitContainer.Panel2
            // 
            this.viewerSettingsSplitContainer.Panel2.Controls.Add(this.viewerSettingsHeaderLabel);
            this.viewerSettingsSplitContainer.Size = new System.Drawing.Size(397, 502);
            this.viewerSettingsSplitContainer.SplitterDistance = 348;
            this.viewerSettingsSplitContainer.TabIndex = 0;
            // 
            // viewerPanel
            // 
            this.viewerPanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.viewerPanel.Controls.Add(this.imageDataEditorControl);
            this.viewerPanel.Controls.Add(this.modelDisplayPanel);
            this.viewerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewerPanel.Location = new System.Drawing.Point(0, 23);
            this.viewerPanel.Name = "viewerPanel";
            this.viewerPanel.Size = new System.Drawing.Size(397, 325);
            this.viewerPanel.TabIndex = 2;
            // 
            // viewerHeaderLabel
            // 
            this.viewerHeaderLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.viewerHeaderLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.viewerHeaderLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.viewerHeaderLabel.Location = new System.Drawing.Point(0, 0);
            this.viewerHeaderLabel.Name = "viewerHeaderLabel";
            this.viewerHeaderLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.viewerHeaderLabel.Size = new System.Drawing.Size(397, 23);
            this.viewerHeaderLabel.TabIndex = 1;
            this.viewerHeaderLabel.Text = "Viewer";
            this.viewerHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // viewerSettingsHeaderLabel
            // 
            this.viewerSettingsHeaderLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.viewerSettingsHeaderLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.viewerSettingsHeaderLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.viewerSettingsHeaderLabel.Location = new System.Drawing.Point(0, 0);
            this.viewerSettingsHeaderLabel.Name = "viewerSettingsHeaderLabel";
            this.viewerSettingsHeaderLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.viewerSettingsHeaderLabel.Size = new System.Drawing.Size(397, 23);
            this.viewerSettingsHeaderLabel.TabIndex = 1;
            this.viewerSettingsHeaderLabel.Text = "Viewer Settings";
            this.viewerSettingsHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // optionsHeaderLabel
            // 
            this.optionsHeaderLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.optionsHeaderLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.optionsHeaderLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.optionsHeaderLabel.Location = new System.Drawing.Point(0, 0);
            this.optionsHeaderLabel.Name = "optionsHeaderLabel";
            this.optionsHeaderLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.optionsHeaderLabel.Size = new System.Drawing.Size(200, 23);
            this.optionsHeaderLabel.TabIndex = 1;
            this.optionsHeaderLabel.Text = "Options";
            this.optionsHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FileSaveFileDialog
            // 
            this.FileSaveFileDialog.Filter = "brres files (*.brres)|*.brres|All files|*.*";
            // 
            // FileOpenFileDialog
            // 
            this.FileOpenFileDialog.Filter = "brres files (*.brres)|*.brres|All files|*.*";
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 551);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(792, 22);
            this.mainStatusStrip.TabIndex = 4;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // modelDisplayPanel
            // 
            this.modelDisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelDisplayPanel.Location = new System.Drawing.Point(0, 0);
            this.modelDisplayPanel.Name = "modelDisplayPanel";
            this.modelDisplayPanel.Size = new System.Drawing.Size(397, 325);
            this.modelDisplayPanel.TabIndex = 4;
            // 
            // imageDataEditorControl
            // 
            this.imageDataEditorControl.BackColor = System.Drawing.SystemColors.Control;
            this.imageDataEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageDataEditorControl.Location = new System.Drawing.Point(0, 0);
            this.imageDataEditorControl.Name = "imageDataEditorControl";
            this.imageDataEditorControl.ShowControls = true;
            this.imageDataEditorControl.ShowMip = true;
            this.imageDataEditorControl.Size = new System.Drawing.Size(397, 325);
            this.imageDataEditorControl.TabIndex = 3;
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.panelsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // panelsToolStripMenuItem
            // 
            this.panelsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesToolStripMenuItem,
            this.viewerSettingsToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.panelsToolStripMenuItem.Name = "panelsToolStripMenuItem";
            this.panelsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.panelsToolStripMenuItem.Text = "&Panels";
            // 
            // viewerSettingsToolStripMenuItem
            // 
            this.viewerSettingsToolStripMenuItem.Name = "viewerSettingsToolStripMenuItem";
            this.viewerSettingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.viewerSettingsToolStripMenuItem.Text = "&Viewer Settings";
            this.viewerSettingsToolStripMenuItem.Click += new System.EventHandler(this.viewerSettingsToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // filesToolStripMenuItem
            // 
            this.filesToolStripMenuItem.Checked = true;
            this.filesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            this.filesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.filesToolStripMenuItem.Text = "&Files";
            this.filesToolStripMenuItem.Click += new System.EventHandler(this.filesToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.filesViewerSplitContainer);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.Controls.Add(this.mainStatusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormMain";
            this.Text = "BRRES Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.filesViewerSplitContainer.Panel1.ResumeLayout(false);
            this.filesViewerSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.filesViewerSplitContainer)).EndInit();
            this.filesViewerSplitContainer.ResumeLayout(false);
            this.viewerOptionsSplitContainer.Panel1.ResumeLayout(false);
            this.viewerOptionsSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewerOptionsSplitContainer)).EndInit();
            this.viewerOptionsSplitContainer.ResumeLayout(false);
            this.viewerSettingsSplitContainer.Panel1.ResumeLayout(false);
            this.viewerSettingsSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewerSettingsSplitContainer)).EndInit();
            this.viewerSettingsSplitContainer.ResumeLayout(false);
            this.viewerPanel.ResumeLayout(false);
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.SplitContainer filesViewerSplitContainer;
        private System.Windows.Forms.SplitContainer viewerOptionsSplitContainer;
        private System.Windows.Forms.SplitContainer viewerSettingsSplitContainer;
        private System.Windows.Forms.Label filesHeaderLabel;
        private System.Windows.Forms.Label viewerHeaderLabel;
        private System.Windows.Forms.Label viewerSettingsHeaderLabel;
        private System.Windows.Forms.Label optionsHeaderLabel;
        private System.Windows.Forms.TreeView fileTreeView;
        private System.Windows.Forms.Panel viewerPanel;
        internal System.Windows.Forms.SaveFileDialog FileSaveFileDialog;
        internal System.Windows.Forms.OpenFileDialog FileOpenFileDialog;
        private System.Windows.Forms.ImageList treeViewImageList;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private Image.ImageDataEditorControl imageDataEditorControl;
        private System.Windows.Forms.Panel modelDisplayPanel;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem panelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewerSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
    }
}