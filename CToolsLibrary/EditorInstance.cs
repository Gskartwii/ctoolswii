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

namespace Chadsoft.CTools
{
    public abstract class EditorInstance
    {
        public abstract Editor Editor { get; }
        public byte[] Data { get; private set; }

        public event EventHandler<SaveEventArgs> Save;
        public event EventHandler Closed;

        public EditorInstance(byte[] data, EventHandler<SaveEventArgs> saveEvent, EventHandler closeEvent)
        {
            Data = data;
            Save += saveEvent;
            Closed += closeEvent;
        }

        protected bool OnSave(byte[] data)
        {
            SaveEventArgs e;

            Data = data;
            Save(this, e = new SaveEventArgs());

            return e.Success;
        }

        protected void OnClose()
        {
            Closed(this, EventArgs.Empty);
        }

        public abstract bool CloseEditor();
    }
}
