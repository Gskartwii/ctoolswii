// CTools szs tool - Archive editor for CTools
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

using System.Collections.ObjectModel;
using System.IO;

namespace Chadsoft.CTools.Szs.Archive
{
    public class ArchiveHeader
    {
        public const int U8Tag = 0x55AA382D;

        public int Tag { get; set; }
        public int ContentsStart { get; set; }
        public int ContentsSize { get; set; }
        public int DataStart { get; set; }
        public Collection<byte> HeaderPadding { get; private set; }

        public ArchiveHeader(EndianBinaryReader reader)
        {
            Tag = reader.ReadInt32();

            if (Tag != U8Tag) throw new InvalidDataException();

            ContentsStart = reader.ReadInt32();
            ContentsSize = reader.ReadInt32();
            DataStart = reader.ReadInt32();
            HeaderPadding = new Collection<byte>(reader.ReadBytes(ContentsStart - 0x10));
        }

        public void Save(EndianBinaryWriter writer)
        {
            byte[] padding;

            padding = new byte[HeaderPadding.Count];
            HeaderPadding.CopyTo(padding, 0);

            writer.Write(Tag);
            writer.Write(ContentsStart);
            writer.Write(ContentsSize);
            writer.Write(DataStart);
            writer.Write(padding, 0, padding.Length);
        }
    }
}
