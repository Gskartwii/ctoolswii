// CTools szs tool - Archive editor for CTools
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

namespace Chadsoft.CTools.Szs
{
    partial class MainForm
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
            System.Windows.Forms.Label propertiesHeaderLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.explorerSplitContainer = new System.Windows.Forms.SplitContainer();
            this.folderTreeView = new System.Windows.Forms.TreeView();
            this.folderContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.folderNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderNewFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderNewFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderImportFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderImportFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderContextMenuToolStripSeparator0 = new System.Windows.Forms.ToolStripSeparator();
            this.folderExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderRenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderImageList = new System.Windows.Forms.ImageList(this.components);
            this.folderHeaderLabel = new System.Windows.Forms.Label();
            this.filePreviewSplitContainer = new System.Windows.Forms.SplitContainer();
            this.fileListView = new System.Windows.Forms.ListView();
            this.nameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sizeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.formatColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fileOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpenWithToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpenExternallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileContextMenuToolStripSeparator0 = new System.Windows.Forms.ToolStripSeparator();
            this.fileExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileReplaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileRenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNewFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNewFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileImportFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileImportFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileImageList = new System.Windows.Forms.ImageList(this.components);
            this.previewPanel = new System.Windows.Forms.Panel();
            this.previewComboBox = new System.Windows.Forms.ComboBox();
            this.previewHeaderLabel = new System.Windows.Forms.Label();
            this.fileTopPanel = new System.Windows.Forms.Panel();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.pathHeaderLabel = new System.Windows.Forms.Label();
            this.propertiesPanel = new System.Windows.Forms.Panel();
            this.propertiesPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMainMenuToolStripSeparator0 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMainMenuToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMainMenuToolStripSeparator0 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMainMenuToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMainMenuToolStripSeparator0 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ArchiveOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ArchiveSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.FileFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.FileSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.FileOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            propertiesHeaderLabel = new System.Windows.Forms.Label();
            this.mainToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.mainToolStripContainer.ContentPanel.SuspendLayout();
            this.mainToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.mainToolStripContainer.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.explorerSplitContainer)).BeginInit();
            this.explorerSplitContainer.Panel1.SuspendLayout();
            this.explorerSplitContainer.Panel2.SuspendLayout();
            this.explorerSplitContainer.SuspendLayout();
            this.folderContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filePreviewSplitContainer)).BeginInit();
            this.filePreviewSplitContainer.Panel1.SuspendLayout();
            this.filePreviewSplitContainer.Panel2.SuspendLayout();
            this.filePreviewSplitContainer.SuspendLayout();
            this.fileContextMenuStrip.SuspendLayout();
            this.fileTopPanel.SuspendLayout();
            this.propertiesPanel.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertiesHeaderLabel
            // 
            propertiesHeaderLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(propertiesHeaderLabel, "propertiesHeaderLabel");
            propertiesHeaderLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            propertiesHeaderLabel.Name = "propertiesHeaderLabel";
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
            this.mainToolStripContainer.ContentPanel.Controls.Add(this.explorerSplitContainer);
            this.mainToolStripContainer.ContentPanel.Controls.Add(this.propertiesPanel);
            resources.ApplyResources(this.mainToolStripContainer.ContentPanel, "mainToolStripContainer.ContentPanel");
            resources.ApplyResources(this.mainToolStripContainer, "mainToolStripContainer");
            this.mainToolStripContainer.Name = "mainToolStripContainer";
            // 
            // mainToolStripContainer.TopToolStripPanel
            // 
            this.mainToolStripContainer.TopToolStripPanel.Controls.Add(this.mainMenuStrip);
            this.mainToolStripContainer.TopToolStripPanel.Controls.Add(this.mainToolStrip);
            // 
            // mainStatusStrip
            // 
            resources.ApplyResources(this.mainStatusStrip, "mainStatusStrip");
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.mainStatusStrip.Name = "mainStatusStrip";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            resources.ApplyResources(this.statusLabel, "statusLabel");
            // 
            // explorerSplitContainer
            // 
            resources.ApplyResources(this.explorerSplitContainer, "explorerSplitContainer");
            this.explorerSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.explorerSplitContainer.Name = "explorerSplitContainer";
            // 
            // explorerSplitContainer.Panel1
            // 
            this.explorerSplitContainer.Panel1.Controls.Add(this.folderTreeView);
            this.explorerSplitContainer.Panel1.Controls.Add(this.folderHeaderLabel);
            // 
            // explorerSplitContainer.Panel2
            // 
            this.explorerSplitContainer.Panel2.Controls.Add(this.filePreviewSplitContainer);
            this.explorerSplitContainer.Panel2.Controls.Add(this.fileTopPanel);
            // 
            // folderTreeView
            // 
            this.folderTreeView.AllowDrop = true;
            this.folderTreeView.ContextMenuStrip = this.folderContextMenu;
            resources.ApplyResources(this.folderTreeView, "folderTreeView");
            this.folderTreeView.ImageList = this.folderImageList;
            this.folderTreeView.LabelEdit = true;
            this.folderTreeView.Name = "folderTreeView";
            this.folderTreeView.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.folderTreeView_BeforeLabelEdit);
            this.folderTreeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.folderTreeView_AfterLabelEdit);
            this.folderTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.folderTreeView_ItemDrag);
            this.folderTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.folderTreeView_AfterSelect);
            this.folderTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.folderTreeView_DragDrop);
            this.folderTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.folderTreeView_DragEnter);
            this.folderTreeView.DragOver += new System.Windows.Forms.DragEventHandler(this.folderTreeView_DragOver);
            this.folderTreeView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.folderTreeView_KeyUp);
            this.folderTreeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.folderTreeView_MouseClick);
            // 
            // folderContextMenu
            // 
            this.folderContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.folderNewToolStripMenuItem,
            this.folderImportToolStripMenuItem,
            this.folderContextMenuToolStripSeparator0,
            this.folderExportToolStripMenuItem,
            this.folderRenameToolStripMenuItem,
            this.folderDeleteToolStripMenuItem});
            this.folderContextMenu.Name = "fileContextMenuStrip";
            resources.ApplyResources(this.folderContextMenu, "folderContextMenu");
            this.folderContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.folderContextMenu_Opening);
            // 
            // folderNewToolStripMenuItem
            // 
            this.folderNewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.folderNewFolderToolStripMenuItem,
            this.folderNewFileToolStripMenuItem});
            this.folderNewToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.page;
            this.folderNewToolStripMenuItem.Name = "folderNewToolStripMenuItem";
            resources.ApplyResources(this.folderNewToolStripMenuItem, "folderNewToolStripMenuItem");
            // 
            // folderNewFolderToolStripMenuItem
            // 
            this.folderNewFolderToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.folder_add;
            this.folderNewFolderToolStripMenuItem.Name = "folderNewFolderToolStripMenuItem";
            resources.ApplyResources(this.folderNewFolderToolStripMenuItem, "folderNewFolderToolStripMenuItem");
            this.folderNewFolderToolStripMenuItem.Click += new System.EventHandler(this.folderNewFolderToolStripMenuItem_Click);
            // 
            // folderNewFileToolStripMenuItem
            // 
            this.folderNewFileToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.page_white_add;
            this.folderNewFileToolStripMenuItem.Name = "folderNewFileToolStripMenuItem";
            resources.ApplyResources(this.folderNewFileToolStripMenuItem, "folderNewFileToolStripMenuItem");
            this.folderNewFileToolStripMenuItem.Click += new System.EventHandler(this.folderNewFileToolStripMenuItem_Click);
            // 
            // folderImportToolStripMenuItem
            // 
            this.folderImportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.folderImportFilesToolStripMenuItem,
            this.folderImportFolderToolStripMenuItem});
            this.folderImportToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.box;
            this.folderImportToolStripMenuItem.Name = "folderImportToolStripMenuItem";
            resources.ApplyResources(this.folderImportToolStripMenuItem, "folderImportToolStripMenuItem");
            // 
            // folderImportFilesToolStripMenuItem
            // 
            this.folderImportFilesToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.page_copy;
            this.folderImportFilesToolStripMenuItem.Name = "folderImportFilesToolStripMenuItem";
            resources.ApplyResources(this.folderImportFilesToolStripMenuItem, "folderImportFilesToolStripMenuItem");
            this.folderImportFilesToolStripMenuItem.Click += new System.EventHandler(this.folderImportFilesToolStripMenuItem_Click);
            // 
            // folderImportFolderToolStripMenuItem
            // 
            this.folderImportFolderToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.folder;
            this.folderImportFolderToolStripMenuItem.Name = "folderImportFolderToolStripMenuItem";
            resources.ApplyResources(this.folderImportFolderToolStripMenuItem, "folderImportFolderToolStripMenuItem");
            this.folderImportFolderToolStripMenuItem.Click += new System.EventHandler(this.folderImportFolderToolStripMenuItem_Click);
            // 
            // folderContextMenuToolStripSeparator0
            // 
            this.folderContextMenuToolStripSeparator0.Name = "folderContextMenuToolStripSeparator0";
            resources.ApplyResources(this.folderContextMenuToolStripSeparator0, "folderContextMenuToolStripSeparator0");
            // 
            // folderExportToolStripMenuItem
            // 
            this.folderExportToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.package_go;
            this.folderExportToolStripMenuItem.Name = "folderExportToolStripMenuItem";
            resources.ApplyResources(this.folderExportToolStripMenuItem, "folderExportToolStripMenuItem");
            this.folderExportToolStripMenuItem.Click += new System.EventHandler(this.folderExportToolStripMenuItem_Click);
            // 
            // folderRenameToolStripMenuItem
            // 
            this.folderRenameToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.textfield;
            this.folderRenameToolStripMenuItem.Name = "folderRenameToolStripMenuItem";
            resources.ApplyResources(this.folderRenameToolStripMenuItem, "folderRenameToolStripMenuItem");
            this.folderRenameToolStripMenuItem.Click += new System.EventHandler(this.folderRenameToolStripMenuItem_Click);
            // 
            // folderDeleteToolStripMenuItem
            // 
            this.folderDeleteToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.cross;
            this.folderDeleteToolStripMenuItem.Name = "folderDeleteToolStripMenuItem";
            resources.ApplyResources(this.folderDeleteToolStripMenuItem, "folderDeleteToolStripMenuItem");
            this.folderDeleteToolStripMenuItem.Click += new System.EventHandler(this.folderDeleteToolStripMenuItem_Click);
            // 
            // folderImageList
            // 
            this.folderImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("folderImageList.ImageStream")));
            this.folderImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.folderImageList.Images.SetKeyName(0, "folder.png");
            this.folderImageList.Images.SetKeyName(1, "folder_go.png");
            this.folderImageList.Images.SetKeyName(2, "compress.png");
            // 
            // folderHeaderLabel
            // 
            this.folderHeaderLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.folderHeaderLabel, "folderHeaderLabel");
            this.folderHeaderLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.folderHeaderLabel.Name = "folderHeaderLabel";
            // 
            // filePreviewSplitContainer
            // 
            resources.ApplyResources(this.filePreviewSplitContainer, "filePreviewSplitContainer");
            this.filePreviewSplitContainer.Name = "filePreviewSplitContainer";
            // 
            // filePreviewSplitContainer.Panel1
            // 
            this.filePreviewSplitContainer.Panel1.Controls.Add(this.fileListView);
            // 
            // filePreviewSplitContainer.Panel2
            // 
            this.filePreviewSplitContainer.Panel2.Controls.Add(this.previewPanel);
            this.filePreviewSplitContainer.Panel2.Controls.Add(this.previewComboBox);
            this.filePreviewSplitContainer.Panel2.Controls.Add(this.previewHeaderLabel);
            // 
            // fileListView
            // 
            this.fileListView.AllowColumnReorder = true;
            this.fileListView.AllowDrop = true;
            this.fileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.sizeColumn,
            this.formatColumn});
            this.fileListView.ContextMenuStrip = this.fileContextMenuStrip;
            resources.ApplyResources(this.fileListView, "fileListView");
            this.fileListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("fileListView.Groups")))});
            this.fileListView.LabelEdit = true;
            this.fileListView.LargeImageList = this.fileImageList;
            this.fileListView.Name = "fileListView";
            this.fileListView.SmallImageList = this.fileImageList;
            this.fileListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.fileListView.UseCompatibleStateImageBehavior = false;
            this.fileListView.View = System.Windows.Forms.View.Tile;
            this.fileListView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.fileListView_AfterLabelEdit);
            this.fileListView.ItemActivate += new System.EventHandler(this.fileListView_ItemActivate);
            this.fileListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.fileListView_ItemDrag);
            this.fileListView.SelectedIndexChanged += new System.EventHandler(this.fileListView_SelectedIndexChanged);
            this.fileListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.fileListView_DragDrop);
            this.fileListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.fileListView_DragEnter);
            this.fileListView.DragOver += new System.Windows.Forms.DragEventHandler(this.fileListView_DragOver);
            this.fileListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.fileListView_KeyUp);
            // 
            // nameColumn
            // 
            resources.ApplyResources(this.nameColumn, "nameColumn");
            // 
            // sizeColumn
            // 
            resources.ApplyResources(this.sizeColumn, "sizeColumn");
            // 
            // formatColumn
            // 
            resources.ApplyResources(this.formatColumn, "formatColumn");
            // 
            // fileContextMenuStrip
            // 
            this.fileContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileOpenToolStripMenuItem,
            this.fileOpenWithToolStripMenuItem,
            this.fileOpenExternallyToolStripMenuItem,
            this.fileContextMenuToolStripSeparator0,
            this.fileExportToolStripMenuItem,
            this.fileReplaceToolStripMenuItem,
            this.fileRenameToolStripMenuItem,
            this.fileDeleteToolStripMenuItem,
            this.fileNewToolStripMenuItem,
            this.fileImportToolStripMenuItem});
            this.fileContextMenuStrip.Name = "fileContextMenuStrip";
            resources.ApplyResources(this.fileContextMenuStrip, "fileContextMenuStrip");
            this.fileContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.fileContextMenuStrip_Opening);
            // 
            // fileOpenToolStripMenuItem
            // 
            this.fileOpenToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.page_white_edit;
            this.fileOpenToolStripMenuItem.Name = "fileOpenToolStripMenuItem";
            resources.ApplyResources(this.fileOpenToolStripMenuItem, "fileOpenToolStripMenuItem");
            this.fileOpenToolStripMenuItem.Click += new System.EventHandler(this.fileOpenToolStripMenuItem_Click);
            // 
            // fileOpenWithToolStripMenuItem
            // 
            this.fileOpenWithToolStripMenuItem.Name = "fileOpenWithToolStripMenuItem";
            resources.ApplyResources(this.fileOpenWithToolStripMenuItem, "fileOpenWithToolStripMenuItem");
            // 
            // fileOpenExternallyToolStripMenuItem
            // 
            this.fileOpenExternallyToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.page_white_go;
            this.fileOpenExternallyToolStripMenuItem.Name = "fileOpenExternallyToolStripMenuItem";
            resources.ApplyResources(this.fileOpenExternallyToolStripMenuItem, "fileOpenExternallyToolStripMenuItem");
            this.fileOpenExternallyToolStripMenuItem.Click += new System.EventHandler(this.fileOpenExternallyToolStripMenuItem_Click);
            // 
            // fileContextMenuToolStripSeparator0
            // 
            this.fileContextMenuToolStripSeparator0.Name = "fileContextMenuToolStripSeparator0";
            resources.ApplyResources(this.fileContextMenuToolStripSeparator0, "fileContextMenuToolStripSeparator0");
            // 
            // fileExportToolStripMenuItem
            // 
            this.fileExportToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.package_go;
            this.fileExportToolStripMenuItem.Name = "fileExportToolStripMenuItem";
            resources.ApplyResources(this.fileExportToolStripMenuItem, "fileExportToolStripMenuItem");
            this.fileExportToolStripMenuItem.Click += new System.EventHandler(this.fileExportToolStripMenuItem_Click);
            // 
            // fileReplaceToolStripMenuItem
            // 
            this.fileReplaceToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.package_green;
            this.fileReplaceToolStripMenuItem.Name = "fileReplaceToolStripMenuItem";
            resources.ApplyResources(this.fileReplaceToolStripMenuItem, "fileReplaceToolStripMenuItem");
            this.fileReplaceToolStripMenuItem.Click += new System.EventHandler(this.fileReplaceToolStripMenuItem_Click);
            // 
            // fileRenameToolStripMenuItem
            // 
            this.fileRenameToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.textfield;
            this.fileRenameToolStripMenuItem.Name = "fileRenameToolStripMenuItem";
            resources.ApplyResources(this.fileRenameToolStripMenuItem, "fileRenameToolStripMenuItem");
            this.fileRenameToolStripMenuItem.Click += new System.EventHandler(this.fileRenameToolStripMenuItem_Click);
            // 
            // fileDeleteToolStripMenuItem
            // 
            this.fileDeleteToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.cross;
            this.fileDeleteToolStripMenuItem.Name = "fileDeleteToolStripMenuItem";
            resources.ApplyResources(this.fileDeleteToolStripMenuItem, "fileDeleteToolStripMenuItem");
            this.fileDeleteToolStripMenuItem.Click += new System.EventHandler(this.fileDeleteToolStripMenuItem_Click);
            // 
            // fileNewToolStripMenuItem
            // 
            this.fileNewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileNewFolderToolStripMenuItem,
            this.fileNewFileToolStripMenuItem});
            this.fileNewToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.page;
            this.fileNewToolStripMenuItem.Name = "fileNewToolStripMenuItem";
            resources.ApplyResources(this.fileNewToolStripMenuItem, "fileNewToolStripMenuItem");
            // 
            // fileNewFolderToolStripMenuItem
            // 
            this.fileNewFolderToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.folder_add;
            this.fileNewFolderToolStripMenuItem.Name = "fileNewFolderToolStripMenuItem";
            resources.ApplyResources(this.fileNewFolderToolStripMenuItem, "fileNewFolderToolStripMenuItem");
            this.fileNewFolderToolStripMenuItem.Click += new System.EventHandler(this.fileNewFolderToolStripMenuItem_Click);
            // 
            // fileNewFileToolStripMenuItem
            // 
            this.fileNewFileToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.page_white_add;
            this.fileNewFileToolStripMenuItem.Name = "fileNewFileToolStripMenuItem";
            resources.ApplyResources(this.fileNewFileToolStripMenuItem, "fileNewFileToolStripMenuItem");
            this.fileNewFileToolStripMenuItem.Click += new System.EventHandler(this.fileNewFileToolStripMenuItem_Click);
            // 
            // fileImportToolStripMenuItem
            // 
            this.fileImportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileImportFilesToolStripMenuItem,
            this.fileImportFolderToolStripMenuItem});
            this.fileImportToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.box;
            this.fileImportToolStripMenuItem.Name = "fileImportToolStripMenuItem";
            resources.ApplyResources(this.fileImportToolStripMenuItem, "fileImportToolStripMenuItem");
            // 
            // fileImportFilesToolStripMenuItem
            // 
            this.fileImportFilesToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.page_copy;
            this.fileImportFilesToolStripMenuItem.Name = "fileImportFilesToolStripMenuItem";
            resources.ApplyResources(this.fileImportFilesToolStripMenuItem, "fileImportFilesToolStripMenuItem");
            this.fileImportFilesToolStripMenuItem.Click += new System.EventHandler(this.fileImportFilesToolStripMenuItem_Click);
            // 
            // fileImportFolderToolStripMenuItem
            // 
            this.fileImportFolderToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.folder;
            this.fileImportFolderToolStripMenuItem.Name = "fileImportFolderToolStripMenuItem";
            resources.ApplyResources(this.fileImportFolderToolStripMenuItem, "fileImportFolderToolStripMenuItem");
            this.fileImportFolderToolStripMenuItem.Click += new System.EventHandler(this.fileImportFolderToolStripMenuItem_Click);
            // 
            // fileImageList
            // 
            this.fileImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("fileImageList.ImageStream")));
            this.fileImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.fileImageList.Images.SetKeyName(0, "folder");
            // 
            // previewPanel
            // 
            resources.ApplyResources(this.previewPanel, "previewPanel");
            this.previewPanel.Name = "previewPanel";
            this.previewPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.previewPanel_Paint);
            this.previewPanel.Resize += new System.EventHandler(this.previewPanel_Resize);
            // 
            // previewComboBox
            // 
            resources.ApplyResources(this.previewComboBox, "previewComboBox");
            this.previewComboBox.FormattingEnabled = true;
            this.previewComboBox.Name = "previewComboBox";
            this.previewComboBox.SelectedIndexChanged += new System.EventHandler(this.previewComboBox_SelectedIndexChanged);
            // 
            // previewHeaderLabel
            // 
            this.previewHeaderLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.previewHeaderLabel, "previewHeaderLabel");
            this.previewHeaderLabel.Name = "previewHeaderLabel";
            // 
            // fileTopPanel
            // 
            this.fileTopPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fileTopPanel.Controls.Add(this.pathTextBox);
            this.fileTopPanel.Controls.Add(this.pathHeaderLabel);
            resources.ApplyResources(this.fileTopPanel, "fileTopPanel");
            this.fileTopPanel.Name = "fileTopPanel";
            // 
            // pathTextBox
            // 
            resources.ApplyResources(this.pathTextBox, "pathTextBox");
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.ReadOnly = true;
            // 
            // pathHeaderLabel
            // 
            this.pathHeaderLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.pathHeaderLabel, "pathHeaderLabel");
            this.pathHeaderLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pathHeaderLabel.Name = "pathHeaderLabel";
            // 
            // propertiesPanel
            // 
            this.propertiesPanel.Controls.Add(this.propertiesPropertyGrid);
            this.propertiesPanel.Controls.Add(propertiesHeaderLabel);
            resources.ApplyResources(this.propertiesPanel, "propertiesPanel");
            this.propertiesPanel.Name = "propertiesPanel";
            // 
            // propertiesPropertyGrid
            // 
            resources.ApplyResources(this.propertiesPropertyGrid, "propertiesPropertyGrid");
            this.propertiesPropertyGrid.Name = "propertiesPropertyGrid";
            this.propertiesPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertiesPropertyGrid_PropertyValueChanged);
            // 
            // mainMenuStrip
            // 
            resources.ApplyResources(this.mainMenuStrip, "mainMenuStrip");
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Name = "mainMenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.fileMainMenuToolStripSeparator0,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.fileMainMenuToolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.page;
            resources.ApplyResources(this.newToolStripMenuItem, "newToolStripMenuItem");
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.folder;
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // fileMainMenuToolStripSeparator0
            // 
            this.fileMainMenuToolStripSeparator0.Name = "fileMainMenuToolStripSeparator0";
            resources.ApplyResources(this.fileMainMenuToolStripSeparator0, "fileMainMenuToolStripSeparator0");
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::Chadsoft.CTools.Szs.Properties.Resources.disk;
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // fileMainMenuToolStripSeparator1
            // 
            this.fileMainMenuToolStripSeparator1.Name = "fileMainMenuToolStripSeparator1";
            resources.ApplyResources(this.fileMainMenuToolStripSeparator1, "fileMainMenuToolStripSeparator1");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listViewModeToolStripMenuItem,
            this.previewToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            // 
            // listViewModeToolStripMenuItem
            // 
            this.listViewModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.largeIconsToolStripMenuItem,
            this.detailsToolStripMenuItem,
            this.smallIconsToolStripMenuItem,
            this.listToolStripMenuItem,
            this.tileToolStripMenuItem});
            this.listViewModeToolStripMenuItem.Name = "listViewModeToolStripMenuItem";
            resources.ApplyResources(this.listViewModeToolStripMenuItem, "listViewModeToolStripMenuItem");
            // 
            // largeIconsToolStripMenuItem
            // 
            this.largeIconsToolStripMenuItem.Name = "largeIconsToolStripMenuItem";
            resources.ApplyResources(this.largeIconsToolStripMenuItem, "largeIconsToolStripMenuItem");
            this.largeIconsToolStripMenuItem.Click += new System.EventHandler(this.fileViewToolStripMenuItems_Click);
            // 
            // detailsToolStripMenuItem
            // 
            this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
            resources.ApplyResources(this.detailsToolStripMenuItem, "detailsToolStripMenuItem");
            this.detailsToolStripMenuItem.Click += new System.EventHandler(this.fileViewToolStripMenuItems_Click);
            // 
            // smallIconsToolStripMenuItem
            // 
            this.smallIconsToolStripMenuItem.Name = "smallIconsToolStripMenuItem";
            resources.ApplyResources(this.smallIconsToolStripMenuItem, "smallIconsToolStripMenuItem");
            this.smallIconsToolStripMenuItem.Click += new System.EventHandler(this.fileViewToolStripMenuItems_Click);
            // 
            // listToolStripMenuItem
            // 
            this.listToolStripMenuItem.Name = "listToolStripMenuItem";
            resources.ApplyResources(this.listToolStripMenuItem, "listToolStripMenuItem");
            this.listToolStripMenuItem.Click += new System.EventHandler(this.fileViewToolStripMenuItems_Click);
            // 
            // tileToolStripMenuItem
            // 
            this.tileToolStripMenuItem.Checked = true;
            this.tileToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tileToolStripMenuItem.Name = "tileToolStripMenuItem";
            resources.ApplyResources(this.tileToolStripMenuItem, "tileToolStripMenuItem");
            this.tileToolStripMenuItem.Click += new System.EventHandler(this.fileViewToolStripMenuItems_Click);
            // 
            // previewToolStripMenuItem
            // 
            this.previewToolStripMenuItem.Checked = true;
            this.previewToolStripMenuItem.CheckOnClick = true;
            this.previewToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
            resources.ApplyResources(this.previewToolStripMenuItem, "previewToolStripMenuItem");
            this.previewToolStripMenuItem.CheckedChanged += new System.EventHandler(this.previewToolStripMenuItem_CheckedChanged);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.editMainMenuToolStripSeparator0,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.editMainMenuToolStripSeparator1,
            this.selectAllToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            resources.ApplyResources(this.undoToolStripMenuItem, "undoToolStripMenuItem");
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            resources.ApplyResources(this.redoToolStripMenuItem, "redoToolStripMenuItem");
            // 
            // editMainMenuToolStripSeparator0
            // 
            this.editMainMenuToolStripSeparator0.Name = "editMainMenuToolStripSeparator0";
            resources.ApplyResources(this.editMainMenuToolStripSeparator0, "editMainMenuToolStripSeparator0");
            // 
            // cutToolStripMenuItem
            // 
            resources.ApplyResources(this.cutToolStripMenuItem, "cutToolStripMenuItem");
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            // 
            // copyToolStripMenuItem
            // 
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            // 
            // pasteToolStripMenuItem
            // 
            resources.ApplyResources(this.pasteToolStripMenuItem, "pasteToolStripMenuItem");
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            // 
            // editMainMenuToolStripSeparator1
            // 
            this.editMainMenuToolStripSeparator1.Name = "editMainMenuToolStripSeparator1";
            resources.ApplyResources(this.editMainMenuToolStripSeparator1, "editMainMenuToolStripSeparator1");
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            resources.ApplyResources(this.selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customizeToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            // 
            // customizeToolStripMenuItem
            // 
            this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            resources.ApplyResources(this.customizeToolStripMenuItem, "customizeToolStripMenuItem");
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.helpMainMenuToolStripSeparator0,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            resources.ApplyResources(this.contentsToolStripMenuItem, "contentsToolStripMenuItem");
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            resources.ApplyResources(this.indexToolStripMenuItem, "indexToolStripMenuItem");
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            resources.ApplyResources(this.searchToolStripMenuItem, "searchToolStripMenuItem");
            // 
            // helpMainMenuToolStripSeparator0
            // 
            this.helpMainMenuToolStripSeparator0.Name = "helpMainMenuToolStripSeparator0";
            resources.ApplyResources(this.helpMainMenuToolStripSeparator0, "helpMainMenuToolStripSeparator0");
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // mainToolStrip
            // 
            resources.ApplyResources(this.mainToolStrip, "mainToolStrip");
            this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton});
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Stretch = true;
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = global::Chadsoft.CTools.Szs.Properties.Resources.page;
            resources.ApplyResources(this.newToolStripButton, "newToolStripButton");
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = global::Chadsoft.CTools.Szs.Properties.Resources.folder;
            resources.ApplyResources(this.openToolStripButton, "openToolStripButton");
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = global::Chadsoft.CTools.Szs.Properties.Resources.disk;
            resources.ApplyResources(this.saveToolStripButton, "saveToolStripButton");
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // ArchiveOpenFileDialog
            // 
            resources.ApplyResources(this.ArchiveOpenFileDialog, "ArchiveOpenFileDialog");
            this.ArchiveOpenFileDialog.ShowReadOnly = true;
            // 
            // ArchiveSaveFileDialog
            // 
            resources.ApplyResources(this.ArchiveSaveFileDialog, "ArchiveSaveFileDialog");
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainToolStripContainer);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.mainToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.mainToolStripContainer.BottomToolStripPanel.PerformLayout();
            this.mainToolStripContainer.ContentPanel.ResumeLayout(false);
            this.mainToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.mainToolStripContainer.TopToolStripPanel.PerformLayout();
            this.mainToolStripContainer.ResumeLayout(false);
            this.mainToolStripContainer.PerformLayout();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.explorerSplitContainer.Panel1.ResumeLayout(false);
            this.explorerSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.explorerSplitContainer)).EndInit();
            this.explorerSplitContainer.ResumeLayout(false);
            this.folderContextMenu.ResumeLayout(false);
            this.filePreviewSplitContainer.Panel1.ResumeLayout(false);
            this.filePreviewSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.filePreviewSplitContainer)).EndInit();
            this.filePreviewSplitContainer.ResumeLayout(false);
            this.fileContextMenuStrip.ResumeLayout(false);
            this.fileTopPanel.ResumeLayout(false);
            this.fileTopPanel.PerformLayout();
            this.propertiesPanel.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer mainToolStripContainer;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.SplitContainer explorerSplitContainer;
        private System.Windows.Forms.TreeView folderTreeView;
        private System.Windows.Forms.Label folderHeaderLabel;
        private System.Windows.Forms.Panel fileTopPanel;
        private System.Windows.Forms.Panel propertiesPanel;
        private System.Windows.Forms.PropertyGrid propertiesPropertyGrid;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Label pathHeaderLabel;
        private System.Windows.Forms.ListView fileListView;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator fileMainMenuToolStripSeparator0;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator fileMainMenuToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator editMainMenuToolStripSeparator0;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator editMainMenuToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator helpMainMenuToolStripSeparator0;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ImageList folderImageList;
        internal System.Windows.Forms.OpenFileDialog ArchiveOpenFileDialog;
        internal System.Windows.Forms.SaveFileDialog ArchiveSaveFileDialog;
        private System.Windows.Forms.ImageList fileImageList;
        private System.Windows.Forms.SplitContainer filePreviewSplitContainer;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listViewModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem largeIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.ColumnHeader sizeColumn;
        private System.Windows.Forms.ContextMenuStrip fileContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem previewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileExportToolStripMenuItem;
        private System.Windows.Forms.Panel previewPanel;
        private System.Windows.Forms.ComboBox previewComboBox;
        private System.Windows.Forms.ToolStripMenuItem fileReplaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileNewFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileNewFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileDeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileImportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileImportFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileImportFolderToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip folderContextMenu;
        private System.Windows.Forms.ToolStripMenuItem folderExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderDeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderNewFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderNewFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderImportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderImportFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderImportFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator folderContextMenuToolStripSeparator0;
        private System.Windows.Forms.ToolStripMenuItem fileOpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileOpenWithToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileOpenExternallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator fileContextMenuToolStripSeparator0;
        internal System.Windows.Forms.FolderBrowserDialog FileFolderBrowserDialog;
        internal System.Windows.Forms.SaveFileDialog FileSaveFileDialog;
        internal System.Windows.Forms.OpenFileDialog FileOpenFileDialog;
        private System.Windows.Forms.ColumnHeader formatColumn;
        private System.Windows.Forms.ToolStripMenuItem fileRenameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderRenameToolStripMenuItem;
        private System.Windows.Forms.Label previewHeaderLabel;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
    }
}