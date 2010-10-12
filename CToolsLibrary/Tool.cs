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
    public class Tool
    {
        public virtual string Name { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual string Author { get; protected set; }
        public virtual Version Version { get; protected set; }
        public virtual Image Icon { get; protected set; }
        public virtual ReadOnlyCollection<Editor> Editors { get; protected set; }
        public virtual ReadOnlyCollection<FileFormat> Formats { get; protected set; }
        public virtual ReadOnlyCollection<NewFile> NewFiles { get; protected set; }

        protected Tool()
        {

        }

        public Tool(string name, string description, string author, Version version, Image icon, ReadOnlyCollection<Editor> editors, ReadOnlyCollection<FileFormat> formats, ReadOnlyCollection<NewFile> newFiles)
        {
            Name = name;
            Description = description;
            Author = author;
            Version = version;
            Icon = icon;
            Editors = editors;
            Formats = formats;
            NewFiles = newFiles;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
