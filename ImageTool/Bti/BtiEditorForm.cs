using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Chadsoft.CTools.Image.Bti
{
    public partial class BtiEditorForm : Form
    {
        public BtiEditorInstance Instance { get; private set; }
        public bool IsClosing { get; private set; }

        internal bool Changed { get; set; }
                
        public BtiEditorForm(BtiEditorInstance instance)
        {
            InitializeComponent();

            Instance = instance;

            SetupInstance();
            UpdateInterface();
        }

        private void SetupInstance()
        {
            if (Instance.Name != null)
                this.Text = string.Format(this.Tag.ToString(), Instance.Name);

            openToolStripMenuItem.Visible = newToolStripMenuItem.Visible =
                saveAsToolStripMenuItem.Visible = openToolStripButton.Visible =
                newToolStripButton.Visible = fileToolStripSeparator1.Visible = !Instance.IsEditor;
        }

        public void UpdateInterface()
        {
            saveAsToolStripMenuItem.Enabled =
                saveToolStripMenuItem.Enabled =
                saveToolStripButton.Enabled =
                imageDataEditorControl.Enabled =
                Instance.Loaded;

            if (imageDataEditorControl.Image != Instance.Image)
                imageDataEditorControl.SetImage(Instance.Image);
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
            Instance.SaveBti();
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
            Instance.SaveBti();
            UpdateInterface();
        }

        private void tplEditorForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void reformatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageDataEditorControl.Reformat();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageDataEditorControl.Export();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageDataEditorControl.Import();
        }
    }
}
