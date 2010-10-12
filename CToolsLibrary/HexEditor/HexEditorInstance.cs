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
using Chadsoft.CTools.Properties;

namespace Chadsoft.CTools.HexEditor
{
    public class HexEditorInstance : EditorInstance 
    {
        private Editor _editor;

        public override Editor Editor
        {
            get { return _editor; }
        }

        public string Name { get; private set; }

        public HexEditorForm MainWindow { get; private set; }

        public HexEditorInstance(byte[] data, string name, EventHandler<SaveEventArgs> saveEvent, EventHandler closeEvent, Editor editor)
            : base(data, saveEvent, closeEvent)
        {
            _editor = editor;
            Name = name;

            MainWindow = new HexEditorForm(this);
            MainWindow.Show();
        }

        public override bool CloseEditor()
        {
            return DoClose();
        }

        internal bool DoSave()
        {
            if (OnSave(MainWindow.Data))
            {
                MainWindow.Changed = false;
                return true;
            }

            return false;
        }

        internal bool DoClose()
        {
            DialogResult result;

            if (MainWindow.Changed)
            {
                result = MessageBox.Show(ResourceSet.MessageFileClose, MainWindow.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.Cancel)
                    return false;
                else if (result == DialogResult.Yes)
                {
                    if (!DoSave())
                        return false;
                }
                else
                {
                    MainWindow.Changed = false;
                }
            }

            if (!MainWindow.IsClosing)
                MainWindow.Close();
            else
                OnClose();

            return true;
        }
    }
}
