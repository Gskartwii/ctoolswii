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
using System.Collections.ObjectModel;
using System.Drawing;

namespace Chadsoft.CTools
{
    public class Editor
    {
        private CreateInstanceDelegate _createInstance;
        private GeneratePreviewDelegate _generatePreview;

        public virtual string Name { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual string Author { get; protected set; }
        public virtual Version Version { get; protected set; }
        public virtual Image Icon { get; protected set; }
        public virtual ReadOnlyCollection<FileFormat> EditorFormats { get; protected set; }

        public virtual EditorInstance CreateInstance(byte[] data, string name, EventHandler<SaveEventArgs> saveEvent, EventHandler closeEvent)
        {
            return _createInstance(data, name, saveEvent, closeEvent);
        }

        public virtual void GeneratePreview(byte[] data, Graphics graphics)
        {
            _generatePreview(data, graphics);
        }

        protected Editor()
        {

        }

        public Editor(string name, string description, string author, Version version, Image icon, ReadOnlyCollection<FileFormat> formats, CreateInstanceDelegate createInstance, GeneratePreviewDelegate previewDelegate)
        {
            Name = name;
            Description = description;
            Author = author;
            Version = version;
            Icon = icon;
            EditorFormats = formats;
            this._createInstance = createInstance;
            _generatePreview = previewDelegate;
        }

        public virtual int FormatMatch(string name, byte[] data, int offset)
        {
            int match = 0;

            foreach (FileFormat format in EditorFormats)
                match = Math.Max(match, format.FormatMatch(name, data, offset));

            return match;
        }
    }

    public delegate EditorInstance CreateInstanceDelegate(byte[] data, string name, EventHandler<SaveEventArgs> saveEvent, EventHandler closeEvent);
    public delegate void GeneratePreviewDelegate(byte[] data, Graphics graphics);
}
