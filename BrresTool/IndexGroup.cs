using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;

namespace Chadsoft.CTools.Brres
{
    public class IndexGroup
    {
        public long Address { get; set; }

        public int Length { get; set; }
        public int EntryCount { get; set; }
        public Collection<IndexEntry> Entries { get; private set; }

        public IndexGroup(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            if (reader.BaseStream.Length - Address < 0x8)
                throw new InvalidDataException();

            Address = reader.BaseStream.Position;
            Length = reader.ReadInt32();
            EntryCount = reader.ReadInt32();
            Entries = new Collection<IndexEntry>();

            for (int i = 0; i <= EntryCount; i++)
                Entries.Add(new IndexEntry(reader));

            for (int i = 1; i < Entries.Count; i++)
                Entries[i].Name = BrresFile.ReadBrresString(reader, Address + Entries[i].NameOffset);

            reader.BaseStream.Seek(Address + 0x8 + Entries.Count * 0x10, SeekOrigin.Begin);
        }

        public IndexGroup(int entries)
        {
            EntryCount = entries + 1;
            Length = EntryCount * 0x10 + 0x8;
            Entries = new Collection<IndexEntry>();

            for (int i = 0; i < EntryCount; i++)
            {
                Entries.Add(new IndexEntry());
            }
        }
        
        public void Write(EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            EntryCount = Entries.Count - 1;
            Length = EntryCount * 0x10 + 0x18;

            writer.Write(Length);
            writer.Write(EntryCount);

            for (int i = 0; i < Entries.Count; i++)
            {
                Entries[i].Write(this, writer);
            }
        }

        public void WriteNames(EndianBinaryWriter writer, Dictionary<string, long> names)
        {
            for (int i = 0; i < Entries.Count; i++)
                Entries[i].WriteNames(this, writer, names);
        }
    }

    public class IndexEntry
    {
        public long Address { get; set; }
        public ushort ID { get; set; }
        public short Unknown2 { get; set; }
        public ushort Left { get; set; }
        public ushort Right { get; set; }
        public int NameOffset { get; set; }
        public int DataOffset { get; set; }
        public string Name { get; set; }
        public BrresSection Section { get; set; }

        public IndexEntry()
        {
            
        }        

        public IndexEntry(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            if (reader.BaseStream.Length - reader.BaseStream.Position < 0x10)
                throw new InvalidDataException();

            ID = reader.ReadUInt16();
            Unknown2 = reader.ReadInt16();
            Left = reader.ReadUInt16();
            Right = reader.ReadUInt16();
            NameOffset = reader.ReadInt32();
            DataOffset = reader.ReadInt32();
        }

        public void Write(IndexGroup group, EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            if (Section != null)
            {
                DataOffset = (int)(Section.Address - group.Address);
                Name = Section.Name;
            }

            writer.Write(ID);
            writer.Write(Unknown2);
            writer.Write(Left);
            writer.Write(Right);
            writer.Write(NameOffset);
            writer.Write(DataOffset);
        }

        public void WriteNames(IndexGroup group, EndianBinaryWriter writer, Dictionary<string, long> names)
        {
            if (Name != null)
                NameOffset = (int)(BrresFile.WriteString(writer, names, Name) - group.Address);
            else
                NameOffset = 0;
        }
    }
}
