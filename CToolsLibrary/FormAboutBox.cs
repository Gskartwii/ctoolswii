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
using System.Windows.Forms;

namespace Chadsoft.CTools
{
    public partial class FormAboutBox : Form
    {
        public FormAboutBox()
        {
            InitializeComponent();

            versionLabel.Text = string.Format(versionLabel.Text, ToolManager.CToolsVersion);
            if (Properties.CToolsSettings.Default.lastTimeUpdate != null && new Version(Properties.CToolsSettings.Default.lastTimeUpdate) > ToolManager.CToolsVersion)
            {
                versionUpdateLabel.Visible = true;
                versionUpdateLabel.Text = string.Format(versionUpdateLabel.Text, Properties.CToolsSettings.Default.lastTimeUpdate);
            }
            else
            {
                versionUpdateLabel.Visible = false;
            }

            foreach (Tool tool in ToolManager.Tools)
            {
                pluginListBox.Items.Add(tool);
            }
        }

        private void pluginListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tool tool;

            tool = pluginListBox.SelectedItem as Tool;

            if (tool != null)
            {
                pluginNameLabel.Text = tool.Name;
                pluginVersionLabel.Text = tool.Version.ToString();
                pluginIconPictureBox.Image = tool.Icon;
            }
            else
            {
                pluginNameLabel.Text = "";
                pluginVersionLabel.Text = "";
                pluginIconPictureBox.Image = null;
            }
        }
    }
}
