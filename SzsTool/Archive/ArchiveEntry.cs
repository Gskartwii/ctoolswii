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
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Chadsoft.CTools.Szs.Archive
{
    public class ArchiveEntry : IDisposable, IComparable<ArchiveEntry>
    {
        string _name;
        private bool _disposed;

        public string Name { get { return _name; } set { _name = value; Changed = true; } }
        public string Extension { get { if (Name.Contains(".")) return Name.Substring(Name.LastIndexOf('.') + 1); else return ""; } }
        public bool IsFolder { get { return (Flags & 0x1) != 0; } set { Flags = (byte)((Flags & 0xFE) | (value ? 1 : 0)); } }
        public bool IsRoot { get { return IsFolder && Id == FolderParent; } }
        public int Id { get; set; }
        
        public byte Flags { get; set; }
        public int NameOffset { get; set; }
        public int FileOffset { get; set; }
        public int FileLength { get; set; }
        public int FolderParent { get { return FileOffset; } set { FileOffset = value; } }
        public int FolderEnd { get { return FileLength; } set { FileLength = value; } }
        public byte[] Data { get; set; }

        public bool Changed { get; set; }

        public ArchiveEntry Parent { get; set; }
        public Collection<ArchiveEntry> Children { get; private set; }

        public static ArchiveEntry LoadTree(EndianBinaryReader reader, ArchiveHeader header)
        {
            ArchiveEntry root;

            root = LoadData(reader);

            LoadNames(reader, root, header);

            return root;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private static ArchiveEntry LoadData(EndianBinaryReader reader)
        {
            ArchiveEntry root;
            ArchiveEntry currentParent, current;

            root = currentParent = new ArchiveEntry(reader);
            if (!root.IsRoot)
                throw new InvalidDataException();

            root.Id = 0;

            for (int i = 1; i < root.FolderEnd; i++)
            {
                while (currentParent.FolderEnd <= i)
                    currentParent = currentParent.Parent;

                current = new ArchiveEntry(reader);
                current.Id = i;

                current.Parent = currentParent;
                currentParent.Children.Add(current);
                if (current.IsFolder)
                    currentParent = current;
            }

            return root;
        }

        private static void LoadNames(EndianBinaryReader reader, ArchiveEntry root, ArchiveHeader header)
        {
            string nameTable;
            nameTable = reader.ReadString(Encoding.ASCII, header.ContentsSize - 0xc * root.FolderEnd);

            AssignNames(root, nameTable);
        }

        private static void AssignNames(ArchiveEntry root, string nameTable)
        {
            if (root.NameOffset > nameTable.Length)
                throw new InvalidDataException();
            else
                root._name = nameTable.Substring(root.NameOffset);

            root._name = root.Name.Remove(root.Name.IndexOf('\0'));

            foreach (ArchiveEntry child in root.Children)
            {
                AssignNames(child, nameTable);
            }
        }

        private ArchiveEntry(EndianBinaryReader reader)
        {
            NameOffset = reader.ReadInt32();
            Flags = (byte)((NameOffset >> 24) & 0xff);
            NameOffset = NameOffset & 0xFFFFFF;
            FileOffset = reader.ReadInt32();
            FileLength = reader.ReadInt32();
            Children = new Collection<ArchiveEntry>();
            if (!IsFolder)
                Data = new byte[FileLength];
        }

        public ArchiveEntry(string name, ArchiveEntry parent)
        {
            Name = name;
            IsFolder = true;
            FolderParent = 1; // To stop it thinking it is root
            Parent = parent;
            Children = new Collection<ArchiveEntry>();
        }

        public ArchiveEntry(string name, ArchiveEntry parent, byte[] data)
        {
            Name = name;
            IsFolder = false;
            Parent = parent;
            Data = data;
            FileLength = data.Length;
        }

        ~ArchiveEntry()
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
            if (!_disposed)
            {
                if (IsFolder)
                {
                    foreach (ArchiveEntry child in Children)
                    {
                        child.Dispose();
                    }

                    Children.Clear();
                    Children = null;
                }
                else
                    Data = null;

                Parent = null;

                _disposed = true;
            }
        }

        public string FullPath
        {
            get 
            {
                ArchiveEntry current;
                string name;

                current = this;
                name = this.Name;

                while (!current.IsRoot)
                {
                    current = current.Parent;
                    name = current.Name + "\\" + name;
                }

                return name;
            }
        }

        public int CompareTo(ArchiveEntry other)
        {
            return Math.Sign(FileOffset - other.FileOffset);
        }

        public List<ArchiveEntry> GetFiles()
        {
            List<ArchiveEntry> files;

            files = new List<ArchiveEntry>();

            foreach (ArchiveEntry child in Children)
            {
                if (child.IsFolder)
                    files.AddRange(child.GetFiles());
                else
                    files.Add(child);
            }

            return files;
        }

        public void BuildNameTable(ref string table, ref int id)
        {
            NameOffset = table.Length;
            table += Name + '\0';
            Id = id++;

            if (IsFolder)
            {
                foreach (ArchiveEntry child in Children)
                {
                    child.BuildNameTable(ref table, ref id);
                }

                if (Parent != null)
                    FolderParent = Parent.Id;
                FolderEnd = id;
            }
        }

        public void BuildFileTable(ref int fileOffset)
        {
            if (!IsFolder)
            {
                FileLength = Data.Length;
                FileOffset = fileOffset;

                fileOffset += FileLength;
                fileOffset += (0x20 - (fileOffset % 0x20)) % 0x20;
            }
            else
            {
                foreach (ArchiveEntry child in Children)
                {
                    child.BuildFileTable(ref fileOffset);
                }
            }
        }

        public void Save(EndianBinaryWriter writer)
        {
            writer.Write((uint)(((uint)Flags << 24) | (uint)NameOffset));
            writer.Write(FileOffset);
            writer.Write(FileLength);

            if (IsFolder)
            {
                foreach (ArchiveEntry child in Children)
                {
                    child.Save(writer);
                }
            }
        }

        public void SaveData(Stream Stream)
        {
            if (IsFolder)
            {
                foreach (ArchiveEntry child in Children)
                {
                    child.SaveData(Stream);
                }
            }
            else
            {
                Stream.Write(Data, 0, Data.Length);

                while (Stream.Position % 0x20 != 0)
                    Stream.WriteByte(0);
            }
        }

        public bool ContainsChild(string name)
        {
            foreach (ArchiveEntry child in Children)
            {
                if (child.Name == name)
                    return true;
            }

            return false;
        }

        public ArchiveEntry GetDirectory(string name)
        {
            ArchiveEntry newDir;

            if (!IsFolder) return null;

            foreach (ArchiveEntry child in Children)
            {
                if (child.IsFolder && child.Name == name)
                    return child;
            }

            newDir = new ArchiveEntry(name, this);
            Children.Add(newDir);

            return newDir;
        }

        public bool IsChildOf(ArchiveEntry copy)
        {
            if (IsRoot)
                return false;
            else if (this == copy)
                return true;
            else
                return Parent.IsChildOf(copy);
        }
    }

}
