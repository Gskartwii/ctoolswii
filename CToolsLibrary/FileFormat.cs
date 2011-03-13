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

namespace Chadsoft.CTools
{
    public class FileFormat
    {
        private FormatMatchDelegate _formatMatch;

        public virtual string Name { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual string Category { get; protected set; }
        public virtual Image Icon { get; protected set; }

        public virtual int FormatMatch(string name, byte[] data, int offset)
        {
            if (_formatMatch != null)
                return _formatMatch(name, data, offset);
            else
                return 0;
        }

        protected FileFormat()
        {

        }

        public FileFormat(string name, string description, string category, Image icon, FormatMatchDelegate formatMatch)
        {
            Name = name;
            Description = description;
            Category = category;
            Icon = icon;
            _formatMatch = formatMatch;
        }        
    }

    public class DummyFileFormat : FileFormat
    {
        string[] extensions;

        public DummyFileFormat(string name, string description, string category, Image icon, string[] extensions)
        {
            Name = name;
            Description = description;
            Category = category;
            Icon = icon;
            this.extensions = extensions;
        }

        public override int FormatMatch(string name, byte[] data, int offset)
        {
            for (int i = 0; i < extensions.Length; i++)
            {
                if (name.EndsWith(extensions[i], StringComparison.OrdinalIgnoreCase))
                    return 10;
            }

            return 0;
        }
    }

    public delegate int FormatMatchDelegate(string name, byte[] data, int offset);
}
