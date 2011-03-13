using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using Chadsoft.CTools.Rendering;

namespace Chadsoft.CTools.Brres
{
    public partial class FormMain : Form
    {
        private int _lastRefresh;
        private Renderer _renderer;

        public BrresToolInstance Instance { get; private set; }
        public bool IsClosing { get; set; }

        public FormMain(BrresToolInstance instance)
        {
            InitializeComponent();

            Instance = instance;

            SetupInterface();
            UpdateInterface();
        }

        private void SetupInterface()
        {
            openToolStripMenuItem.Visible = newToolStripMenuItem.Visible =
                saveAsToolStripMenuItem.Visible = openToolStripButton.Visible =
                newToolStripButton.Visible = toolStripSeparator1.Visible = !Instance.IsEditor;
            _renderer = new DirectXRenderer (modelDisplayPanel);
            _renderer.Invalidated += new EventHandler(renderer_Invalidated);
        }

        private void renderer_Invalidated(object sender, EventArgs e)
        {
            _renderer.Render();
        }

        public void UpdateInterface()
        {
            saveAsToolStripMenuItem.Enabled =
                saveToolStripMenuItem.Enabled =
                saveToolStripButton.Enabled =
                Instance.Loaded;

            LoadTree();
            LoadViewer();

            RefreshInterface("Ready");
        }

        private void LoadTree()
        {
            TreeNode rootNode;

            if (!Instance.Loaded)
                fileTreeView.Nodes.Clear();
            else
            {
                try
                {
                    fileTreeView.Enabled = false;
                    RefreshInterface("TreeLoading");

                    if (fileTreeView.Nodes.Count == 0)
                        rootNode = fileTreeView.Nodes.Add(Instance.Brres.Name);
                    else
                        rootNode = fileTreeView.Nodes[0];

                    rootNode.Text = Instance.Brres.Name;
                    rootNode.Tag = null;
                    rootNode.SelectedImageIndex = rootNode.ImageIndex = 0;

                    LoadNodes(rootNode.Nodes);

                    rootNode.Expand();
                    RefreshInterface("TreeLoaded");
                }
                catch (Exception ex)
                {
                    RefreshInterface("TreeLoadError", ex.Message);
                    fileTreeView.Nodes.Clear();

                }
                finally
                {
                    fileTreeView.Enabled = true;
                }
            }
        }

        private void LoadNodes(TreeNodeCollection nodes)
        {
            TreeNode node;
            int nodeIndex;

            nodeIndex = 0;

            for (int i = 0; i < Instance.Brres.RootSection.Folders.Count; i++)
            {
                if (nodes.Count <= nodeIndex)
                    nodes.Add(node = new TreeNode());
                else
                    node = nodes[nodeIndex];

                nodeIndex++;
                node.Text = node.Name = Instance.Brres.RootSection.Root.Entries[i + 1].Name;
                node.Tag = Instance.Brres.RootSection.Root.Entries[i + 1];
                node.ImageIndex = node.SelectedImageIndex = 0;

                AddNodes(node.Nodes, Instance.Brres.RootSection.Folders[i]);

                node.Expand();
            }

            while (nodeIndex < nodes.Count)
                nodes.RemoveAt(nodeIndex);

            RefreshInterface(null);
        }

        private void AddNodes(TreeNodeCollection nodes, IndexGroup indexGroup)
        {
            TreeNode node;
            int nodeIndex;

            nodeIndex = 0;

            for (int i = 1; i < indexGroup.Entries.Count; i++)
            {
                if (nodes.Count <= nodeIndex)
                    nodes.Add(node = new TreeNode());
                else
                    node = nodes[nodeIndex];

                nodeIndex++;
                node.Text = node.Name = indexGroup.Entries[i].Name;
                node.Tag = indexGroup.Entries[i];

                if (indexGroup.Entries[i].Section.GetType() == typeof(Mdl0Section))
                    node.ImageKey = node.SelectedImageKey = "MDL0";
                else if (indexGroup.Entries[i].Section.GetType() == typeof(Tex0Section))
                        node.ImageKey = node.SelectedImageKey = "Tex0";                    
                else
                    node.ImageIndex = node.SelectedImageIndex = 1;

            }

            while (nodeIndex < nodes.Count)
                nodes.RemoveAt(nodeIndex);

            RefreshInterface(null);
        }

        private void LoadViewer()
        {
            IndexEntry current;

            _renderer.UnloadAll();
            if (fileTreeView.SelectedNode == null || (current = fileTreeView.SelectedNode.Tag as IndexEntry) == null || current.Section == null || current.Section.GetType() == typeof(UnknownSection))
            {
                modelDisplayPanel.Visible = false;
                imageDataEditorControl.Visible = false;
                viewerSettingsToolStripMenuItem.Visible = false;
                optionsToolStripMenuItem.Visible = false;
                viewerSettingsSplitContainer.Panel2Collapsed = true;
                viewerOptionsSplitContainer.Panel2Collapsed = true;
            }
            else
            {
                if (current.Section.GetType() == typeof(Mdl0Section))
                {
                    _renderer.LoadModel(((Mdl0Section)current.Section).GenerateModel(Instance.Brres));

                    modelDisplayPanel.Visible = true;
                    imageDataEditorControl.Visible = false;
                    viewerSettingsToolStripMenuItem.Visible = true;
                    optionsToolStripMenuItem.Visible = true;
                    viewerSettingsSplitContainer.Panel2Collapsed = !viewerSettingsToolStripMenuItem.Checked;
                    viewerOptionsSplitContainer.Panel2Collapsed = !optionsToolStripMenuItem.Checked;

                    _renderer.Render();
                }
                else if (current.Section.GetType() == typeof(Tex0Section))
                {
                    modelDisplayPanel.Visible = false;
                    imageDataEditorControl.Visible = true;
                    imageDataEditorControl.SetImage(((Tex0Section)current.Section).GenerateImage());
                    viewerSettingsToolStripMenuItem.Visible = false;
                    optionsToolStripMenuItem.Visible = false;
                    viewerSettingsSplitContainer.Panel2Collapsed = true;
                    viewerOptionsSplitContainer.Panel2Collapsed = true;
                }                
            }
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

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Instance.NewFile();
            UpdateInterface();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Instance.OpenFile();
            UpdateInterface();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Instance.SaveBrres();
            UpdateInterface();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Instance.SaveAs();
            UpdateInterface();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            Instance.NewFile();
            UpdateInterface();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            Instance.OpenFile();
            UpdateInterface();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            Instance.SaveBrres();
            UpdateInterface();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClosing = true;
            e.Cancel = !Instance.Close();
            UpdateInterface();
            IsClosing = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAboutBox aboutBox;

            aboutBox = new FormAboutBox();
            aboutBox.ShowDialog();
            aboutBox.Dispose();
        }

        private void fileTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LoadViewer();
        }

        private void filesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filesViewerSplitContainer.Panel1Collapsed = !(filesToolStripMenuItem.Checked = filesViewerSplitContainer.Panel1Collapsed);
        }

        private void viewerSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewerSettingsSplitContainer.Panel2Collapsed = !(viewerSettingsToolStripMenuItem.Checked = viewerSettingsSplitContainer.Panel2Collapsed);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewerOptionsSplitContainer.Panel2Collapsed = !(optionsToolStripMenuItem.Checked = viewerOptionsSplitContainer.Panel2Collapsed);
        }
    }
}
