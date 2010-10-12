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

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chadsoft.CTools
{
    public partial class NewFileForm : Form
    {
        public NewFile SelectedFile
        {
            get
            {
                if (newFileListView.SelectedIndices.Count == 1)
                    return newFileListView.SelectedItems[0].Tag as NewFile;
                else
                    return null;
            }
        }

        public NewFileForm(NewFile[] files)
        {
            InitializeComponent();

            SetupListView(files);
        }

        private void SetupListView(NewFile[] files)
        {
            ListViewItem item;

            CreateImageList(files);

            for (int i = 0; i < files.Length; i++)
            {
                item = new ListViewItem(files[i].Name);
                item.ToolTipText = files[i].Description;
                item.ImageIndex = i;
                item.Tag = files[i];

                newFileListView.Items.Add(item);
            }
        }

        private void CreateImageList(NewFile[] files)
        {
            ImageList imageList;

            imageList = new ImageList();
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(32, 32);

            foreach (NewFile item in files)
            {
                imageList.Images.Add(item.Icon);
            }

            newFileListView.LargeImageList = imageList;
        }

        private void newFileListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            okButton.Enabled = newFileListView.SelectedIndices.Count == 1;
        }

        private void newFileListView_ItemActivate(object sender, EventArgs e)
        {
            if (okButton.Enabled)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
