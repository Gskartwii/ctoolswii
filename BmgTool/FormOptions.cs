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
using System.Windows.Forms;

namespace Chadsoft.CTools.Bmg
{
    public partial class FormOptions : Form
    {
        internal static void Display()
        {
            FormOptions options;

            options = new FormOptions();

            options.ShowDialog();
            options.Dispose();
        }

        public FormOptions()
        {
            InitializeComponent();

            switch (Properties.Settings.Default.NewLine)
            {
                case "\n":
                    lineEndingsComboBox.SelectedIndex = 1;
                    break;
                case "\r":
                    lineEndingsComboBox.SelectedIndex = 2;
                    break;
                case "\r\n":
                    lineEndingsComboBox.SelectedIndex = 3;
                    break;
                default:
                    Properties.Settings.Default.NewLine = "";
                    lineEndingsComboBox.SelectedIndex = 0;
                    break;
            }
        }

        private void lineEndingsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Created)
            {
                switch (lineEndingsComboBox.SelectedIndex)
	            {
                    case 1:
                        Properties.Settings.Default.NewLine = "\n";
                        break;
                    case 2:
                        Properties.Settings.Default.NewLine = "\r";
                        break;
                    case 3:
                        Properties.Settings.Default.NewLine = "\r\n";
                        break;
                    default:
                        Properties.Settings.Default.NewLine = "";
                        break;
	            }
            }
        }
    }
}
