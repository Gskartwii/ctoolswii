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
            this.mainToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.messagesMessageSplitContainer = new System.Windows.Forms.SplitContainer();
            this.messageDataGridView = new System.Windows.Forms.DataGridView();
            this.messageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.messageBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.messagesHeaderLabel = new System.Windows.Forms.Label();
            this.updateButton = new System.Windows.Forms.Button();
            this.hexEditButton = new System.Windows.Forms.Button();
            this.placeholderExplanationLabel = new System.Windows.Forms.Label();
            this.messageRichTextBox = new System.Windows.Forms.RichTextBox();
            this.messageLabel = new System.Windows.Forms.Label();
            this.messageHeaderLabel = new System.Windows.Forms.Label();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.FileOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.FileSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.mainToolStripContainer.ContentPanel.SuspendLayout();
            this.mainToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.mainToolStripContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messagesMessageSplitContainer)).BeginInit();
            this.messagesMessageSplitContainer.Panel1.SuspendLayout();
            this.messagesMessageSplitContainer.Panel2.SuspendLayout();
            this.messagesMessageSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messageBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messageBindingNavigator)).BeginInit();
            this.messageBindingNavigator.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
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
            this.mainToolStripContainer.ContentPanel.Controls.Add(this.messagesMessageSplitContainer);
            this.mainToolStripContainer.ContentPanel.Size = new System.Drawing.Size(792, 502);
            this.mainToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.mainToolStripContainer.Name = "mainToolStripContainer";
            this.mainToolStripContainer.Size = new System.Drawing.Size(792, 573);
            this.mainToolStripContainer.TabIndex = 0;
            this.mainToolStripContainer.Text = "toolStripContainer1";
            // 
            // mainToolStripContainer.TopToolStripPanel
            // 
            this.mainToolStripContainer.TopToolStripPanel.Controls.Add(this.mainMenuStrip);
            this.mainToolStripContainer.TopToolStripPanel.Controls.Add(this.mainToolStrip);
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 0);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(792, 22);
            this.mainStatusStrip.TabIndex = 0;
            // 
            // messagesMessageSplitContainer
            // 
            this.messagesMessageSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messagesMessageSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.messagesMessageSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.messagesMessageSplitContainer.Name = "messagesMessageSplitContainer";
            this.messagesMessageSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // messagesMessageSplitContainer.Panel1
            // 
            this.messagesMessageSplitContainer.Panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.messagesMessageSplitContainer.Panel1.Controls.Add(this.messageDataGridView);
            this.messagesMessageSplitContainer.Panel1.Controls.Add(this.messageBindingNavigator);
            this.messagesMessageSplitContainer.Panel1.Controls.Add(this.messagesHeaderLabel);
            this.messagesMessageSplitContainer.Panel1MinSize = 0;
            // 
            // messagesMessageSplitContainer.Panel2
            // 
            this.messagesMessageSplitContainer.Panel2.Controls.Add(this.updateButton);
            this.messagesMessageSplitContainer.Panel2.Controls.Add(this.hexEditButton);
            this.messagesMessageSplitContainer.Panel2.Controls.Add(this.placeholderExplanationLabel);
            this.messagesMessageSplitContainer.Panel2.Controls.Add(this.messageRichTextBox);
            this.messagesMessageSplitContainer.Panel2.Controls.Add(this.messageLabel);
            this.messagesMessageSplitContainer.Panel2.Controls.Add(this.messageHeaderLabel);
            this.messagesMessageSplitContainer.Panel2.Enabled = false;
            this.messagesMessageSplitContainer.Panel2MinSize = 205;
            this.messagesMessageSplitContainer.Size = new System.Drawing.Size(792, 502);
            this.messagesMessageSplitContainer.SplitterDistance = 284;
            this.messagesMessageSplitContainer.TabIndex = 0;
            // 
            // messageDataGridView
            // 
            this.messageDataGridView.AutoGenerateColumns = false;
            this.messageDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.messageDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.messageDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.messageDataGridViewTextBoxColumn});
            this.messageDataGridView.DataSource = this.messageBindingSource;
            this.messageDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageDataGridView.Location = new System.Drawing.Point(0, 48);
            this.messageDataGridView.MultiSelect = false;
            this.messageDataGridView.Name = "messageDataGridView";
            this.messageDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.messageDataGridView.Size = new System.Drawing.Size(792, 236);
            this.messageDataGridView.TabIndex = 1;
            this.messageDataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.messageDataGridView_CellBeginEdit);
            this.messageDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.messageDataGridView_CellEndEdit);
            this.messageDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.messageDataGridView_DataError);
            this.messageDataGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.messageDataGridView_UserDeletingRow);
            // 
            // messageBindingSource
            // 
            this.messageBindingSource.AllowNew = true;
            this.messageBindingSource.DataSource = typeof(Chadsoft.CTools.Bmg.BmgMessage);
            this.messageBindingSource.AddingNew += new System.ComponentModel.AddingNewEventHandler(this.messageBindingSource_AddingNew);
            this.messageBindingSource.CurrentChanged += new System.EventHandler(this.messageBindingSource_CurrentChanged);
            // 
            // messageBindingNavigator
            // 
            this.messageBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.messageBindingNavigator.AllowItemReorder = true;
            this.messageBindingNavigator.BindingSource = this.messageBindingSource;
            this.messageBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.messageBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.messageBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.messageBindingNavigator.Location = new System.Drawing.Point(0, 23);
            this.messageBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.messageBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.messageBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.messageBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.messageBindingNavigator.Name = "messageBindingNavigator";
            this.messageBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.messageBindingNavigator.Size = new System.Drawing.Size(792, 25);
            this.messageBindingNavigator.TabIndex = 2;
            this.messageBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = global::Chadsoft.CTools.Bmg.Properties.Resources.add;
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = global::Chadsoft.CTools.Bmg.Properties.Resources.cross;
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = global::Chadsoft.CTools.Bmg.Properties.Resources.resultset_first;
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = global::Chadsoft.CTools.Bmg.Properties.Resources.resultset_previous;
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = global::Chadsoft.CTools.Bmg.Properties.Resources.resultset_next;
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = global::Chadsoft.CTools.Bmg.Properties.Resources.resultset_last;
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // messagesHeaderLabel
            // 
            this.messagesHeaderLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.messagesHeaderLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.messagesHeaderLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.messagesHeaderLabel.Location = new System.Drawing.Point(0, 0);
            this.messagesHeaderLabel.Name = "messagesHeaderLabel";
            this.messagesHeaderLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.messagesHeaderLabel.Size = new System.Drawing.Size(792, 23);
            this.messagesHeaderLabel.TabIndex = 0;
            this.messagesHeaderLabel.Text = "Messages";
            this.messagesHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // updateButton
            // 
            this.updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.updateButton.Location = new System.Drawing.Point(419, 159);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 6;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // hexEditButton
            // 
            this.hexEditButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hexEditButton.Location = new System.Drawing.Point(500, 159);
            this.hexEditButton.Name = "hexEditButton";
            this.hexEditButton.Size = new System.Drawing.Size(75, 23);
            this.hexEditButton.TabIndex = 6;
            this.hexEditButton.Text = "Hex Edit";
            this.hexEditButton.UseVisualStyleBackColor = true;
            this.hexEditButton.Click += new System.EventHandler(this.hexEditButton_Click);
            // 
            // placeholderExplanationLabel
            // 
            this.placeholderExplanationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.placeholderExplanationLabel.DataBindings.Add(new System.Windows.Forms.Binding("Visible", global::Chadsoft.CTools.Bmg.Properties.Settings.Default, "ReplaceSequences", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.placeholderExplanationLabel.Location = new System.Drawing.Point(416, 47);
            this.placeholderExplanationLabel.Name = "placeholderExplanationLabel";
            this.placeholderExplanationLabel.Size = new System.Drawing.Size(335, 109);
            this.placeholderExplanationLabel.TabIndex = 5;
            this.placeholderExplanationLabel.Text = resources.GetString("placeholderExplanationLabel.Text");
            this.placeholderExplanationLabel.Visible = global::Chadsoft.CTools.Bmg.Properties.Settings.Default.ReplaceSequences;
            // 
            // messageRichTextBox
            // 
            this.messageRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.messageRichTextBox.Location = new System.Drawing.Point(12, 47);
            this.messageRichTextBox.Name = "messageRichTextBox";
            this.messageRichTextBox.Size = new System.Drawing.Size(398, 150);
            this.messageRichTextBox.TabIndex = 4;
            this.messageRichTextBox.Text = "";
            this.messageRichTextBox.Leave += new System.EventHandler(this.messageRichTextBox_Leave);
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.Location = new System.Drawing.Point(13, 31);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(53, 13);
            this.messageLabel.TabIndex = 3;
            this.messageLabel.Text = "Message:";
            // 
            // messageHeaderLabel
            // 
            this.messageHeaderLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.messageHeaderLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.messageHeaderLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.messageHeaderLabel.Location = new System.Drawing.Point(0, 0);
            this.messageHeaderLabel.Name = "messageHeaderLabel";
            this.messageHeaderLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.messageHeaderLabel.Size = new System.Drawing.Size(792, 23);
            this.messageHeaderLabel.TabIndex = 1;
            this.messageHeaderLabel.Text = "Message";
            this.messageHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(792, 24);
            this.mainMenuStrip.TabIndex = 0;
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
            this.newToolStripMenuItem.Image = global::Chadsoft.CTools.Bmg.Properties.Resources.page;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::Chadsoft.CTools.Bmg.Properties.Resources.folder;
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
            this.saveToolStripMenuItem.Image = global::Chadsoft.CTools.Bmg.Properties.Resources.disk;
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
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
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
            this.mainToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 24);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(792, 25);
            this.mainToolStrip.Stretch = true;
            this.mainToolStrip.TabIndex = 1;
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = global::Chadsoft.CTools.Bmg.Properties.Resources.page;
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "New";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = global::Chadsoft.CTools.Bmg.Properties.Resources.folder;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "Open";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = global::Chadsoft.CTools.Bmg.Properties.Resources.disk;
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // FileOpenFileDialog
            // 
            this.FileOpenFileDialog.Filter = "bmg files (*.bmg)|*.bmg|All files|*.*";
            // 
            // FileSaveFileDialog
            // 
            this.FileSaveFileDialog.Filter = "bmg files (*.bmg)|*.bmg|All files|*.*";
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "ID";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // messageDataGridViewTextBoxColumn
            // 
            this.messageDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.messageDataGridViewTextBoxColumn.DataPropertyName = "Message";
            this.messageDataGridViewTextBoxColumn.HeaderText = "Message";
            this.messageDataGridViewTextBoxColumn.Name = "messageDataGridViewTextBoxColumn";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.mainToolStripContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormMain";
            this.Text = "BMG Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.mainToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.mainToolStripContainer.BottomToolStripPanel.PerformLayout();
            this.mainToolStripContainer.ContentPanel.ResumeLayout(false);
            this.mainToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.mainToolStripContainer.TopToolStripPanel.PerformLayout();
            this.mainToolStripContainer.ResumeLayout(false);
            this.mainToolStripContainer.PerformLayout();
            this.messagesMessageSplitContainer.Panel1.ResumeLayout(false);
            this.messagesMessageSplitContainer.Panel1.PerformLayout();
            this.messagesMessageSplitContainer.Panel2.ResumeLayout(false);
            this.messagesMessageSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messagesMessageSplitContainer)).EndInit();
            this.messagesMessageSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messageDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messageBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messageBindingNavigator)).EndInit();
            this.messageBindingNavigator.ResumeLayout(false);
            this.messageBindingNavigator.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer mainToolStripContainer;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.SplitContainer messagesMessageSplitContainer;
        private System.Windows.Forms.DataGridView messageDataGridView;
        private System.Windows.Forms.Label messagesHeaderLabel;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.BindingSource messageBindingSource;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.Label messageHeaderLabel;
        internal System.Windows.Forms.OpenFileDialog FileOpenFileDialog;
        internal System.Windows.Forms.SaveFileDialog FileSaveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.BindingNavigator messageBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.RichTextBox messageRichTextBox;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.Label placeholderExplanationLabel;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button hexEditButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageDataGridViewTextBoxColumn;
    }
}

