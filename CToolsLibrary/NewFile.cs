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

using System.Drawing;

namespace Chadsoft.CTools
{
    public class NewFile
    {
        private byte[] data;

        public virtual string Name { get; protected set; }
        public virtual string DefaultName { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual Image Icon { get; protected set; }
        public virtual FileFormat Format { get; protected set; }

        public virtual byte[] GetData()
        {
            return data;
        }

        protected NewFile()
        {

        }

        public NewFile(string name, string defaultName, string description, Image icon, FileFormat format, byte[] data)
        {
            Name = name;
            DefaultName = defaultName;
            Description = description;
            Icon = icon;
            Format = format;
            this.data = data;
        }
    }
}
