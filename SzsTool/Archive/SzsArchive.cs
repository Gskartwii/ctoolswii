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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Chadsoft.CTools.Szs.Archive
{
    public class SzsArchive : IDisposable
    {
        private bool disposed;

        public string Name { get; set; }
        public int Size { get; private set; }
        public ArchiveHeader Header { get; private set; }
        public ArchiveEntry Root { get; private set; }
        public bool Changed { get; set; }

        public SzsArchive(Stream stream, string name)
            : this(stream, name, true)
	    {
	    }

        public SzsArchive(Stream stream, string name, bool loadData)
        {
            EndianBinaryReader reader;

            reader = new EndianBinaryReader(stream);

            Name = name;
            Header = new ArchiveHeader(reader);
            Root = ArchiveEntry.LoadTree(reader, Header);

            if (loadData) 
                LoadData(Root, stream);
        }

        ~SzsArchive()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);           
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Header = null;
                }

                if (Root != null)
                    Root.Dispose();
                Root = null;

                disposed = true;
            }
        }

        private void LoadData(ArchiveEntry Root, Stream stream)
        {
            List<ArchiveEntry> entries;
            int location, first, last, start, offset, run;
            byte[] current;

            entries = Root.GetFiles();
            entries.Sort();

            location = (int)stream.Position;
            first = last = 0;

            current = new byte[0x100];

            while (first < entries.Count && location < stream.Length)
            {
                stream.Read(current, 0, current.Length);

                while (last < entries.Count && entries[last].FileOffset <= location + current.Length)
                    last++;

                while (first < entries.Count && location >= entries[first].FileOffset + entries[first].FileLength)
                    first++;

                for (int i = first; i < last; i++)
                {
                    start = Math.Max(entries[i].FileOffset - location, 0);
                    offset = Math.Max(location - entries[i].FileOffset, 0);
                    run = Math.Min(current.Length - start,
                        entries[i].FileOffset + entries[i].FileLength - (location + start));
                    
                    Array.Copy(current, start, entries[i].Data, offset, run);
                }

                location += current.Length;
            }
        }

        public void Save(Stream stream)
        {
            EndianBinaryWriter writer; 
            string nameTable;
            int id, offset;

            nameTable = ""; id = 0;
            Root.BuildNameTable(ref nameTable, ref id);

            Header.ContentsSize = Root.FolderEnd * 0xc + nameTable.Length;
            Header.DataStart = Header.ContentsSize + Header.ContentsStart + (0x20 - ((Header.ContentsSize + Header.ContentsStart) % 0x20)) % 0x20;
            offset = Header.DataStart;

            Root.BuildFileTable(ref offset);

            writer = new EndianBinaryWriter(stream);
            Header.Save(writer);
            Root.Save(writer);
            writer.Write(nameTable, Encoding.ASCII, false);

            writer.WritePadding(0x20, 0);

            Root.SaveData(stream);

            Changed = false;
        }

    }
}
