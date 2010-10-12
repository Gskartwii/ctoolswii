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

using System;
using System.ComponentModel;
using System.Media;
using System.Windows.Forms;

namespace Chadsoft.CTools.Bmg
{
    public partial class FormMain : Form
    {
        public BmgToolInstance Instance { get; private set; }

        public bool IsClosing { get; set; }

        public FormMain(BmgToolInstance instance)
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

            Properties.Settings.Default.PropertyChanged += new PropertyChangedEventHandler(Default_PropertyChanged);
        }

        private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Instance.Loaded)
            {
                for (int i = 0; i < Instance.Bmg.Messages.Count; i++)
                {
                    Instance.Bmg.Messages[i].Binary = Instance.Bmg.Messages[i].Binary;
                    UpdateItem(i);
                }
            }
        }

        public void UpdateInterface()
        {
            saveAsToolStripMenuItem.Enabled =
                saveToolStripMenuItem.Enabled =
                saveToolStripButton.Enabled =
                Instance.Loaded;

            LoadMessages();
        }

        public void UpdateItem(int index)
        {
            if (Instance.Loaded && messageBindingSource.DataSource != null && messageBindingSource.Count > index)
            {
                if (messageBindingSource.Position == index)
                {
                    messageBindingSource.ResetCurrentItem();
                    messageBindingSource_CurrentChanged(messageBindingSource, EventArgs.Empty);
                }
                else
                    messageBindingSource.ResetItem(index);
            }
        }

        private void LoadMessages()
        {
            if (Instance.Loaded)
            {
                if (messageBindingSource.DataSource != Instance.Bmg.Messages)
                    messageBindingSource.DataSource = Instance.Bmg.Messages;

                messageDataGridView.Visible =
                    messageBindingNavigator.Enabled = 
                    messagesMessageSplitContainer.Panel2.Enabled = true;
                idDataGridViewTextBoxColumn.Visible = Instance.Bmg.Header.Sections >= 3;                
            }
            else
            {
                messageDataGridView.Visible =
                    messageBindingNavigator.Enabled =
                    messagesMessageSplitContainer.Panel2.Enabled = false;
                messageBindingSource.DataSource = null;
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
            Instance.SaveBmg();
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
            Instance.SaveBmg();
            UpdateInterface();
        }

        private void messageBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            bool locked;
            BmgMessage current;
            
            current = (BmgMessage)messageBindingSource.Current;
            
            if (messageBindingSource.Current != null)
            {
                locked = Instance.IsLocked(current);
                messageRichTextBox.Text = (current).Message;

                bindingNavigatorDeleteItem.Enabled = !locked;
                messageRichTextBox.ReadOnly = locked;
                hexEditButton.Enabled = updateButton.Enabled = !locked;
            }
            else
                messageRichTextBox.Text = "";

            messagesMessageSplitContainer.Panel2.Enabled = messageBindingSource.Current != null;

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
        }

        private void messageBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (Instance.Loaded)
                e.NewObject = new BmgMessage(Instance.Bmg);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOptions.Display();            
        }

        private void hexEditButton_Click(object sender, EventArgs e)
        {
            BmgMessage current;

            current = (BmgMessage)messageBindingSource.Current;
            if (current == null) return;

            bindingNavigatorDeleteItem.Enabled = false;
            messageRichTextBox.ReadOnly = true;
            hexEditButton.Enabled = updateButton.Enabled = false;
            
            Instance.OpenBinary(current);
        }

        private void messageDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            BmgMessage current;
            
            current = (BmgMessage)e.Row.DataBoundItem;
            e.Cancel = Instance.IsLocked(current);

            if (e.Cancel)
            {
                MessageBox.Show(Program.GetString("MessageErrorLock"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void messageDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            e.Cancel = Instance.IsLocked(Instance.Bmg.Messages[e.RowIndex]);

            if (e.Cancel)
            {
                SystemSounds.Exclamation.Play();
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            UpdateFromMessage();
        }

        private void UpdateFromMessage()
        {
            BmgMessage current;

            current = (BmgMessage)messageBindingSource.Current;

            if (current != null && current.Message != messageRichTextBox.Text)
            {
                current.Message = messageRichTextBox.Text;
                UpdateItem(messageBindingSource.Position);
            }

            Instance.Bmg.Changed = true;
        }

        private void messageDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(Program.GetString("MessageErrorInvalidData"), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);            
        }

        private void messageRichTextBox_Leave(object sender, EventArgs e)
        {
            UpdateFromMessage();
        }

        private void messageDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Instance.Bmg.Changed = true;
        }
    }
}
