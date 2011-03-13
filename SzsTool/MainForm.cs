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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Chadsoft.CTools.Szs.Archive;

namespace Chadsoft.CTools.Szs
{
    internal partial class MainForm : Form
    {
        BufferedGraphics previewBuffer;
        Editor[] editors;
        int _lastRefresh;
        SzsExplorerInstance _instance;

        internal SzsExplorerInstance Instance { get { return _instance; } }

        internal bool IsClosing { get; private set; }

        public MainForm(SzsExplorerInstance instance)
        {
            _instance = instance;

            InitializeComponent();
            LoadGroups();
            LoadEditor();
            UpdateInterface();
        }

        public void UpdateInterface()
        {
            saveAsToolStripMenuItem.Enabled =
                saveToolStripMenuItem.Enabled =
                saveToolStripButton.Enabled =
                Instance.Loaded;

            LoadTree();

            RefreshInterface("Ready");
        }

        private void LoadEditor()
        {
            openToolStripMenuItem.Visible = newToolStripMenuItem.Visible =
                saveAsToolStripMenuItem.Visible = openToolStripButton.Visible =
                newToolStripButton.Visible = fileMainMenuToolStripSeparator0.Visible = !Instance.IsEditor;
        }

        private void LoadGroups()
        {
            List<string> groups;
            string key;

            groups = new List<string>();

            foreach (FileFormat format in ToolManager.FileFormats)
            {
                if (!groups.Contains(format.Category) && !string.IsNullOrEmpty(format.Category))
                    groups.Add(format.Category);

                key = "file" + format.Name.Replace(" ", "");
                if (!fileImageList.Images.ContainsKey(key))
                {
                    fileImageList.Images.Add(key, format.Icon);
                }
            }

            foreach (string group in groups)
	        {
                fileListView.Groups.Add("file" + group.Replace(" ", ""), group);
	        }
        }

        private void LoadTree()
        {
            TreeNode rootNode;

            if (!Instance.Loaded)
                folderTreeView.Nodes.Clear();
            else
            {
                try
                {
                    folderTreeView.Enabled = false;
                    RefreshInterface("TreeLoading");

                    if (folderTreeView.Nodes.Count == 0)
                        rootNode = folderTreeView.Nodes.Add(Instance.Archive.Name);
                    else
                        rootNode = folderTreeView.Nodes[0];

                    rootNode.Text = Instance.Archive.Name;
                    rootNode.Tag = Instance.Archive.Root;
                    rootNode.SelectedImageIndex = rootNode.ImageIndex = 2;

                    LoadNodes(rootNode.Nodes, Instance.Archive.Root.Children);

                    rootNode.Expand();
                    RefreshInterface("TreeLoaded");
                }
                catch (Exception ex)
                {
                    RefreshInterface("TreeLoadError", ex.Message);
                    folderTreeView.Nodes.Clear();

                }
                finally
                {
                    folderTreeView.Enabled = true;
                }
            }
            LoadFiles();
        }

        private void LoadNodes(TreeNodeCollection nodes, Collection<ArchiveEntry> children)
        {
            TreeNode node;
            int nodeIndex;

            for (int i = nodeIndex = 0; i < children.Count; i++)
            {
                if (children[i].IsFolder)
                {
                    if (nodes.Count <= nodeIndex)
                        nodes.Add(node = new TreeNode());
                    else
                        node = nodes[nodeIndex];

                    nodeIndex++;
                    node.Text = node.Name = children[i].Name;
                    node.Tag = children[i];

                    LoadNodes(node.Nodes, children[i].Children);

                    node.Expand();
                }
            }

            while (nodeIndex < nodes.Count)
                nodes.RemoveAt(nodeIndex);

            RefreshInterface(null);
        }

        private void LoadFiles()
        {
            ArchiveEntry entry, child;
            ListViewItem item;
            int fileIndex;

            if (folderTreeView.SelectedNode == null) entry = null;
            else entry = folderTreeView.SelectedNode.Tag as ArchiveEntry;

            if (entry == null)
            {
                fileListView.Items.Clear();
                propertiesPropertyGrid.SelectedObject = null;
            }
            else
            {
                try
                {
                    fileListView.Enabled = false;
                    RefreshInterface("FileListLoading");

                    for (fileIndex = 0; fileIndex < entry.Children.Count; fileIndex++)
                    {
                        child = entry.Children[fileIndex];

                        if (fileListView.Items.Count <= fileIndex)
                            fileListView.Items.Add(item = new ListViewItem());
                        else
                            item = fileListView.Items[fileIndex];

                        SetupListItem(child, item);
                        RefreshInterface(null);
                    }

                    while (fileIndex < fileListView.Items.Count)
                        fileListView.Items.RemoveAt(fileIndex);

                    fileListView.SelectedItems.Clear();

                    //propertiesPropertyGrid.SelectedObject = new ArchiveDirectoryInfo(entry);

                    RefreshInterface("FileListLoaded");
                }
                catch (Exception ex)
                {
                    fileListView.Items.Clear();
                    RefreshInterface("FileListLoadError", ex.Message);
                }
                finally
                {
                    fileListView.Enabled = true;
                }
            }

            LoadPreview();
            LoadProperties();
            RefreshInterface("Ready");
        }

        private void SetupListItem(ArchiveEntry child, ListViewItem item)
        {
            ArchiveDirectoryInfo dirInfo;
            FileFormat format;

            item.Text = child.Name;
            item.Tag = child;
            if (child.IsFolder)
            {
                dirInfo = new ArchiveDirectoryInfo(child);
                item.Group = fileListView.Groups["folderGroup"];
                item.ImageKey = "folder";
                if (item.SubItems.Count != 3)
                {
                    item.SubItems.Add(dirInfo.Size);
                    item.SubItems.Add("");
                }
                else
                {
                    item.SubItems[1].Text = dirInfo.Size;
                    item.SubItems[2].Text = "";
                }
            }
            else
            {
                format = ToolManager.GetFormat(child.Name, child.Data, 0);

                item.ImageKey = "file" + format.Name.Replace(" ", "");
                item.Group = fileListView.Groups["file" + format.Category.Replace(" ", "")];

                if (item.SubItems.Count != 3)
                {
                    item.SubItems.Add(FileSize.FormatSize(child.FileLength));
                    item.SubItems.Add(format.Description);
                }
                else
                {
                    item.SubItems[1].Text = FileSize.FormatSize(child.FileLength);
                    item.SubItems[2].Text = format.Description;
                }
            }
        }

        private void LoadPreview()
        {
            ArchiveEntry file;

            try
            {
                previewPanel.Enabled = false;

                RefreshInterface("PreviewLoading");


                if (fileListView.SelectedItems.Count != 1)
                {
                    editors = new Editor[0];
                    previewComboBox.Visible = false;
                    previewComboBox.Items.Clear();
                }
                else
                {
                    file = fileListView.SelectedItems[0].Tag as ArchiveEntry;

                    if (file.IsFolder)
                    {
                        editors = new Editor[0];
                        previewComboBox.Visible = false;
                        previewComboBox.Items.Clear();
                    }
                    else
                    {
                        editors = ToolManager.GetEditors(file.Name, file.Data, 0);

                        previewComboBox.Items.Clear();
                        previewComboBox.Visible = editors.Length > 1;

                        for (int i = 0; i < editors.Length; i++)
                            previewComboBox.Items.Add(editors[i].Name);

                        previewComboBox.SelectedIndex = 0;
                    }
                }

                RefreshInterface("PreviewLoaded");
                RedrawPreview();
            }
            catch (Exception ex)
            {
                RefreshInterface("PreviewError", ex.Message);
            }
            finally
            {
                previewPanel.Enabled = true;
            }

            RefreshInterface("Ready");
        }

        private void RedrawPreview()
        {
            int previewIndex;

            if (previewBuffer == null || previewBuffer.Graphics.ClipBounds.Size != previewPanel.Size)
            {
                if (previewBuffer != null) previewBuffer.Dispose();

                previewBuffer = BufferedGraphicsManager.Current.Allocate(previewPanel.CreateGraphics(), new Rectangle(Point.Empty, previewPanel.Size));
                previewBuffer.Graphics.SetClip(new Rectangle(Point.Empty, previewPanel.Size));
            }

            previewBuffer.Graphics.Clear(previewPanel.BackColor);
            if (editors.Length == 0)
            {
                previewBuffer.Graphics.DrawString(Program.GetString("PreviewNoPreview"), previewPanel.Font, new SolidBrush(previewPanel.ForeColor), previewBuffer.Graphics.ClipBounds, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }
            else
            {
                try
                {
                    previewIndex = previewComboBox.SelectedIndex;

                    if (previewIndex == -1 || previewIndex >= editors.Length)
                        previewIndex = 0;

                    editors[previewIndex].GeneratePreview((fileListView.SelectedItems[0].Tag as ArchiveEntry).Data, previewBuffer.Graphics);
                }
                catch (Exception)
                {
                    //RefreshInterface("PreviewError", ex.Message);
                }
            }

            RenderPreview();
        }

        private void RenderPreview()
        {
            if (previewPanel.Visible && previewBuffer != null)
            {
                previewBuffer.Render();
            }
        }

        private void LoadProperties()
        {
            ArchiveMultipleInfo info;
            ArchiveEntry item;

            RefreshInterface("PropertiesLoading");

            if (fileListView.SelectedItems.Count == 1)
            {
                item = (ArchiveEntry)fileListView.SelectedItems[0].Tag;
                if (item.IsFolder)
                    propertiesPropertyGrid.SelectedObject = new ArchiveDirectoryInfo(item);
                else
                    propertiesPropertyGrid.SelectedObject = new ArchiveFileInfo(item);
            }
            else if (fileListView.SelectedItems.Count == 0)
            {
                propertiesPropertyGrid.SelectedObject = null;
            }
            else
            {
                info = new ArchiveMultipleInfo();

                foreach (ListViewItem listItem in fileListView.SelectedItems)
                {
                    item = (ArchiveEntry)listItem.Tag;
                    info.Add(item);
                }

                propertiesPropertyGrid.SelectedObject = info;
            }
            RefreshInterface("PropertiesLoaded");
            RefreshInterface("Ready");
        }

        private void SelectInTree(ArchiveEntry item)
        {
            string folder;
            ArchiveEntry current;
            TreeNode node;
            Stack<string> path;

            current = item;
            path = new Stack<string>();

            while (!current.IsRoot)
            {
                path.Push(current.Name);
                current = current.Parent;
            }

            node = folderTreeView.Nodes[0];

            while (path.Count > 0)
            {
                folder = path.Pop();

                foreach (TreeNode subNode in node.Nodes)
                    if (subNode.Name == folder)
                    {
                        node = subNode;
                        break;
                    }
            }

            folderTreeView.SelectedNode = node;
        }

        private void PopulateOpenWith()
        {
            ToolStripItem item;

            fileOpenWithToolStripMenuItem.DropDownItems.Clear();

            for (int i = 0; i < editors.Length; i++)
            {
                item = fileOpenWithToolStripMenuItem.DropDownItems.Add(editors[i].Name, editors[i].Icon);
                item.ToolTipText = editors[i].Description;
                item.Tag = editors[i];
                item.Click += new EventHandler(fileOpeWithItem_Click);
            }

            fileOpenWithToolStripMenuItem.Visible = fileOpenWithToolStripMenuItem.DropDownItems.Count != 0;
        }

        private void RefreshInterface(string statusKey, params object[] args)
        {
            string message;

            if (!string.IsNullOrEmpty(statusKey))
            {
                message = Program.GetString("Status" + statusKey, args);
            }
            else
                message = null;

            if (!string.IsNullOrEmpty(message))
            {
                statusLabel.Text = message;

                if (statusKey.Contains("Error"))
                    MessageBox.Show(statusLabel.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (Environment.TickCount - _lastRefresh > 100)
            {
                Application.DoEvents();
                _lastRefresh = Environment.TickCount;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClosing = true;
            e.Cancel = !Instance.CloseFile();
            IsClosing = false;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Instance.NewArchive();
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Instance.OpenArchive();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Instance.SaveFile();
            UpdateInterface();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Instance.SaveFileAs();
            UpdateInterface();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fileViewToolStripMenuItems_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in listViewModeToolStripMenuItem.DropDownItems)
            {
                item.Checked = item == sender;
            }

            fileListView.View = (View)listViewModeToolStripMenuItem.DropDownItems.IndexOf(sender as ToolStripMenuItem);
        }

        private void previewToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            filePreviewSplitContainer.Panel2Collapsed = !previewToolStripMenuItem.Checked;
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            Instance.NewArchive();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            Instance.OpenArchive();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            Instance.SaveFile();
        }

        private void propertiesPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateInterface();
        }

        private void folderContextMenu_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = folderTreeView.SelectedNode == null;
        }

        private void folderExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Instance.ExportFiles(new ArchiveEntry[] { folderTreeView.SelectedNode.Tag as ArchiveEntry });
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorExport", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void folderRenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderTreeView.SelectedNode.BeginEdit();
        }

        private void folderDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Instance.Delete(new ArchiveEntry[] { folderTreeView.SelectedNode.Tag as ArchiveEntry });
                UpdateInterface();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorDelete", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void folderNewFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArchiveEntry parent, newFolder;

            try
            {
                parent = folderTreeView.SelectedNode.Tag as ArchiveEntry;
                newFolder = Instance.AddFolder(parent);

                UpdateInterface();

                SelectInTree(newFolder);
                folderTreeView.SelectedNode.BeginEdit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorNewFolder", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void folderNewFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArchiveEntry parent;

            try
            {
                parent = folderTreeView.SelectedNode.Tag as ArchiveEntry;

                Instance.NewFile(parent);

                UpdateInterface();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorNewFile", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void folderImportFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Instance.ImportFiles(folderTreeView.SelectedNode.Tag as ArchiveEntry);

            UpdateInterface();
        }

        private void folderImportFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Instance.ImportFolder(folderTreeView.SelectedNode.Tag as ArchiveEntry);

            UpdateInterface();
        }

        private void fileContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            bool folder;

            if (fileListView.SelectedItems.Count == 0)
            {
                e.Cancel = folderTreeView.SelectedNode == null;

                fileExportToolStripMenuItem.Visible = fileDeleteToolStripMenuItem.Visible = fileRenameToolStripMenuItem.Visible = false;
                fileNewToolStripMenuItem.Visible = fileImportToolStripMenuItem.Visible = folderTreeView.SelectedNode != null;
                fileReplaceToolStripMenuItem.Visible = false;
                fileContextMenuToolStripSeparator0.Visible = fileOpenToolStripMenuItem.Visible = fileOpenWithToolStripMenuItem.Visible = fileOpenExternallyToolStripMenuItem.Visible = false;
            }
            else if (fileListView.SelectedItems.Count == 1)
            {
                folder = ((ArchiveEntry)fileListView.SelectedItems[0].Tag).IsFolder;
                fileExportToolStripMenuItem.Visible = fileDeleteToolStripMenuItem.Visible = fileRenameToolStripMenuItem.Visible = true;
                fileNewToolStripMenuItem.Visible = fileImportToolStripMenuItem.Visible = false;
                fileReplaceToolStripMenuItem.Visible = !folder;
                fileContextMenuToolStripSeparator0.Visible = fileOpenToolStripMenuItem.Visible = fileOpenWithToolStripMenuItem.Visible = fileOpenExternallyToolStripMenuItem.Visible = !folder;

                if (!folder)
                    PopulateOpenWith();
            }
            else
            {
                fileExportToolStripMenuItem.Visible = fileDeleteToolStripMenuItem.Visible =  true;
                fileNewToolStripMenuItem.Visible = fileImportToolStripMenuItem.Visible = fileRenameToolStripMenuItem.Visible = false;
                fileReplaceToolStripMenuItem.Visible = false;
                fileContextMenuToolStripSeparator0.Visible = fileOpenToolStripMenuItem.Visible = fileOpenWithToolStripMenuItem.Visible = fileOpenExternallyToolStripMenuItem.Visible = false;
            }
        }

        private void fileOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editors.Length > 0)
            {
                Instance.OpenFile(fileListView.SelectedItems[0].Tag as ArchiveEntry, editors[0]);
            }
        }

        private void fileOpeWithItem_Click(object sender, EventArgs e)
        {
            Instance.OpenFile(fileListView.SelectedItems[0].Tag as ArchiveEntry, (sender as ToolStripItem).Tag as Editor);
        }

        private void fileOpenExternallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArchiveEntry item;

            item = fileListView.SelectedItems[0].Tag as ArchiveEntry;

            Instance.OpenExternal(item);
        }

        private void fileExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArchiveEntry entry;
            ArchiveEntry[] roots;

            try
            {
                if (fileListView.SelectedItems.Count == 0)
                {
                    return;
                }
                else if (fileListView.SelectedItems.Count == 1)
                {
                    entry = fileListView.SelectedItems[0].Tag as ArchiveEntry;

                    if (!entry.IsFolder)
                        Instance.ExportFile(entry);
                    else
                    {
                        roots = new ArchiveEntry[1];
                        roots[0] = entry;
                        Instance.ExportFiles(roots);
                    }
                }
                else
                {
                    roots = new ArchiveEntry[fileListView.SelectedItems.Count];

                    for (int i = 0; i < fileListView.SelectedItems.Count; i++)
                    {
                        roots[i] = fileListView.SelectedItems[i].Tag as ArchiveEntry;
                    }

                    Instance.ExportFiles(roots);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorExport", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fileRenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileListView.SelectedItems[0].BeginEdit();
        }

        private void fileReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArchiveEntry file;


            try
            {
                file = fileListView.SelectedItems[0].Tag as ArchiveEntry;

                Instance.ReplaceFile(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorReplace", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fileDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArchiveEntry[] items;

            try
            {
                items = new ArchiveEntry[fileListView.SelectedItems.Count];

                for (int i = 0; i < items.Length; i++)
                {
                    items[i] = fileListView.SelectedItems[i].Tag as ArchiveEntry;
                }

                Instance.Delete(items);
                UpdateInterface();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorDelete", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fileNewFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArchiveEntry parent, newFolder;

            try
            {
                parent = folderTreeView.SelectedNode.Tag as ArchiveEntry;
                newFolder = Instance.AddFolder(parent);

                UpdateInterface();

                foreach (ListViewItem item in fileListView.Items)
                {
                    if (item.Tag == newFolder)
                    {
                        item.BeginEdit();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorNewFolder", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fileNewFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArchiveEntry parent;

            try
            {
                parent = folderTreeView.SelectedNode.Tag as ArchiveEntry;

                Instance.NewFile(parent);

                UpdateInterface();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorNewFile", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fileImportFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Instance.ImportFiles(folderTreeView.SelectedNode.Tag as ArchiveEntry);

            UpdateInterface();
        }

        private void fileImportFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Instance.ImportFolder(folderTreeView.SelectedNode.Tag as ArchiveEntry);

            UpdateInterface();
        }

        private void fileListView_ItemActivate(object sender, EventArgs e)
        {
            ArchiveEntry item;

            if (fileListView.SelectedItems.Count > 1) return;

            item = fileListView.SelectedItems[0].Tag as ArchiveEntry;

            if (item == null) return;

            if (item.IsFolder)
            {
                SelectInTree(item);
            }
            else
            {
                Instance.OpenFile(item, editors[0]);
            }
        }

        private void fileListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPreview();
            LoadProperties();
        }

        private void fileListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            e.CancelEdit = string.IsNullOrWhiteSpace(e.Label);
            if (!e.CancelEdit)
            {
                ((ArchiveEntry)fileListView.Items[e.Item].Tag).Name = e.Label;
                UpdateInterface();
            }
        }

        private void fileListView_KeyUp(object sender, KeyEventArgs e)
        {
            ArchiveEntry[] items;

            switch (e.KeyCode)
            {
                case Keys.Delete:
                    if (fileListView.SelectedItems.Count > 0)
                    {
                        items = new ArchiveEntry[fileListView.SelectedItems.Count];

                        for (int i = 0; i < items.Length; i++)
                        {
                            items[i] = fileListView.SelectedItems[i].Tag as ArchiveEntry;
                        }

                        Instance.Delete(items);
                        UpdateInterface();
                    }
                    break;
            }
        }

        private void fileListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ArchiveEntry[] entries;

            entries = new ArchiveEntry[fileListView.SelectedItems.Count];

            for (int i = 0; i < entries.Length; i++)
            {
                entries[i] = fileListView.SelectedItems[i].Tag as ArchiveEntry;
            }

            fileListView.DoDragDrop(entries, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void fileListView_DragDrop(object sender, DragEventArgs e)
        {
            ArchiveEntry folder;
            folder = fileListViewGetDragFolder(e);
            
            if (e.Effect == DragDropEffects.Copy)
                Instance.CopyItems(e.Data.GetData(typeof(ArchiveEntry[])) as ArchiveEntry[], folder);
            else if (e.Effect == DragDropEffects.Move)
                Instance.MoveItems(e.Data.GetData(typeof(ArchiveEntry[])) as ArchiveEntry[], folder);
            
            UpdateInterface();
        }

        private void fileListView_DragEnter(object sender, DragEventArgs e)
        {
            fileListViewDragging(e);
        }

        private void fileListView_DragOver(object sender, DragEventArgs e)
        {
            fileListViewDragging(e);
        }

        private void fileListViewDragging(DragEventArgs e)
        {
            ArchiveEntry folder;
            ArchiveEntry[] copies;

            if (!Instance.Loaded || folderTreeView.SelectedNode == null || (e.KeyState & 0x1) == 0)
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                folder = fileListViewGetDragFolder(e);

                copies = e.Data.GetData(typeof(ArchiveEntry[])) as ArchiveEntry[];

                if (copies == null)
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }

                foreach (ArchiveEntry copy in copies)
                {
                    if (copy.IsFolder && folder.IsChildOf(copy))
                    {
                        e.Effect = DragDropEffects.None;
                        return;
                    }
                } 

                if ((e.KeyState & 0x8) == 0)
                    e.Effect = DragDropEffects.Move;
                else
                    e.Effect = DragDropEffects.Copy;
            }
        }

        private ArchiveEntry fileListViewGetDragFolder(DragEventArgs e)
        {
            ArchiveEntry folder;
            ListViewItem item;
            Point upper;

            upper = fileListView.PointToClient(fileListView.Location);
            item = fileListView.GetItemAt(e.X + upper.X, e.Y + upper.Y);

            if (item == null || !(item.Tag as ArchiveEntry).IsFolder)
                item = null;

            folder = folderTreeView.SelectedNode.Tag as ArchiveEntry;
            if (item != null)
                folder = item.Tag as ArchiveEntry;
            return folder;
        }

        private void folderTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            pathTextBox.Text = folderTreeView.SelectedNode.FullPath;

            LoadFiles();

            if (e.Node != null && e.Node.Parent != null)
                propertiesPropertyGrid.SelectedObject = new ArchiveDirectoryInfo(e.Node.Tag as ArchiveEntry);
            else if (e.Node != null)
                propertiesPropertyGrid.SelectedObject = new ArchiveInfo(e.Node.Tag as ArchiveEntry);
            else
                propertiesPropertyGrid.SelectedObject = null;
        }

        private void folderTreeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.CancelEdit = ((ArchiveEntry)e.Node.Tag).IsRoot;
        }

        private void folderTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.CancelEdit = string.IsNullOrWhiteSpace(e.Label);
            if (!e.CancelEdit)
            {
                ((ArchiveEntry)e.Node.Tag).Name = e.Label;
                UpdateInterface();
            }
        }

        private void folderTreeView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                folderTreeView.SelectedNode = folderTreeView.HitTest(e.X, e.Y).Node;
            }
        }

        private void folderTreeView_KeyUp(object sender, KeyEventArgs e)
        {
            ArchiveEntry folder;
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    if (folderTreeView.SelectedNode != null)
                    {
                        folder = folderTreeView.SelectedNode.Tag as ArchiveEntry;

                        if (!folder.IsRoot)
                        {
                            Instance.Delete(new ArchiveEntry[] { folder });
                            UpdateInterface();
                        }
                    }

                    break;
            }
        }

        private void folderTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode node;
            ArchiveEntry entry;

            node = e.Item as TreeNode;

            if (node != null)
            {
                entry = node.Tag as ArchiveEntry;
                if (entry != null && !entry.IsRoot)
                    folderTreeView.DoDragDrop(new ArchiveEntry[] { entry }, DragDropEffects.Move | DragDropEffects.Copy);
            }
        }

        private void folderTreeView_DragDrop(object sender, DragEventArgs e)
        {
            ArchiveEntry folder;
            folder = folderTreeViewGetDragFolder(e);

            if (e.Effect == DragDropEffects.Copy)
                Instance.CopyItems(e.Data.GetData(typeof(ArchiveEntry[])) as ArchiveEntry[], folder);
            else if (e.Effect == DragDropEffects.Move)
                Instance.MoveItems(e.Data.GetData(typeof(ArchiveEntry[])) as ArchiveEntry[], folder);
            
            UpdateInterface();
        }

        private void folderTreeView_DragEnter(object sender, DragEventArgs e)
        {
            folderTreeViewDragging(e);
        }

        private void folderTreeView_DragOver(object sender, DragEventArgs e)
        {
            folderTreeViewDragging(e);
        }

        private void folderTreeViewDragging(DragEventArgs e)
        {
            ArchiveEntry folder;
            ArchiveEntry[] copies;

            if (!Instance.Loaded || (e.KeyState & 0x1) == 0)
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                folder = folderTreeViewGetDragFolder(e);
                copies = e.Data.GetData(typeof(ArchiveEntry[])) as ArchiveEntry[];

                if (folder == null || copies == null)
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }

                foreach (ArchiveEntry copy in copies)
                {
                    if (copy.IsFolder && folder.IsChildOf(copy))
                    {
                        e.Effect = DragDropEffects.None;
                        return;
                    }
                }

                if ((e.KeyState & 0x8) == 0)
                    e.Effect = DragDropEffects.Move;
                else
                    e.Effect = DragDropEffects.Copy;
            }
        }

        private ArchiveEntry folderTreeViewGetDragFolder(DragEventArgs e)
        {
            TreeNode item;
            Point upper;

            upper = folderTreeView.PointToClient(fileListView.Location);
            item = folderTreeView.GetNodeAt(e.X + upper.X, e.Y + upper.Y);

            if (item == null)
                return null;

            return item.Tag as ArchiveEntry;
        }

        private void previewComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RedrawPreview();
        }

        private void previewPanel_Paint(object sender, PaintEventArgs e)
        {
            RenderPreview();
        }

        private void previewPanel_Resize(object sender, EventArgs e)
        {
            RedrawPreview();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAboutBox aboutBox;

            aboutBox = new FormAboutBox();
            aboutBox.ShowDialog();
        }

    }
}
