using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;

namespace Chadsoft.CTools.Brres
{
    public class Pat0Section : BrresSection
    {
        public const int Tag = 0x50415430;

        public BrresCommonSectionHeader Header { get; private set; }
        public Pat0Header Pat0Header { get; private set; }
        public IndexGroup IndexGroup { get; private set; }
        public Collection<Pat0Entry> Entries { get; private set; }
        public Collection<int> StringOffsetList1 { get; private set; }
        public Collection<int> StringList3 { get; private set; }
        public Collection<string> StringList1 { get; private set; }

        public Pat0Section(EndianBinaryReader reader, BrresCommonSectionHeader header)
        {
            Address = reader.BaseStream.Position - 0x10;

            Header = header;
            Pat0Header = new Pat0Header(reader);
            IndexGroup = new IndexGroup(reader);
            Name = BrresFile.ReadBrresString(reader, Address + Pat0Header.NameOffset);

            Entries = new Collection<Pat0Entry>();

            for (int i = 1; i < IndexGroup.Entries.Count; i++)
            {
                BrresFile.SafeSeek(reader, IndexGroup.Address + IndexGroup.Entries[i].DataOffset, 0xc);

                Entries.Add(new Pat0Entry(reader));
            }

            StringOffsetList1 = new Collection<int>();
            StringList3 = new Collection<int>();

            BrresFile.SafeSeek(reader, Address + Pat0Header.StringList1Offset, 4 * Pat0Header.StringList1Count);
            for (int i = 0; i < Pat0Header.StringList1Count; i++)
                StringOffsetList1.Add(reader.ReadInt32());
            BrresFile.SafeSeek(reader, Address + Pat0Header.StringList3Offset, 4 * Pat0Header.StringList1Count);
            for (int i = 0; i < Pat0Header.StringList1Count; i++) // yes, list 1 count, it's weird/wrong
                StringList3.Add(reader.ReadInt32());
            StringList1 = new Collection<string>();
            for (int i = 0; i < Pat0Header.StringList1Count; i++)
                StringList1.Add(BrresFile.ReadBrresString(reader, Address + Pat0Header.StringList1Offset + StringOffsetList1[i]));
        }

        public override void Write(EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            Pat0Header.DataOffset = (int)(IndexGroup.Address - Pat0Header.Address);
            Pat0Header.EntryCount = (short)(Entries.Count);
            Pat0Header.StringList1Count = (short)StringOffsetList1.Count;
            
            Header.Write(writer);
            Pat0Header.Write(writer);
            IndexGroup.Write(writer);

            while (IndexGroup.Entries.Count != Entries.Count + 1)
                if (IndexGroup.Entries.Count < Entries.Count + 1)
                    IndexGroup.Entries.Add(new IndexEntry());
                else
                    IndexGroup.Entries.RemoveAt(0);

            for (int i = 0; i < Entries.Count; i++)
            {
                IndexGroup.Entries[i + 1].DataOffset = (int)(Entries[i].Address - IndexGroup.Address);
                Entries[i].Write(writer);                
            }

            while (StringOffsetList1.Count != StringList1.Count)
                if (StringOffsetList1.Count < StringList1.Count)
                    StringOffsetList1.Add(0);
                else
                    StringOffsetList1.RemoveAt(0);

            while (StringList3.Count != StringList1.Count)
                if (StringList3.Count < StringList1.Count)
                    StringList3.Add(0);
                else
                    StringList3.RemoveAt(0);

            Pat0Header.StringList1Offset = (int)(writer.BaseStream.Position - Address);
            for (int i = 0; i < StringOffsetList1.Count; i++)
                writer.Write(StringOffsetList1[i]);
            Pat0Header.StringList3Offset = (int)(writer.BaseStream.Position - Address);
            for (int i = 0; i < StringList3.Count; i++)
                writer.Write(StringList3[i]);
        }

        public override void WriteNames(EndianBinaryWriter writer, Dictionary<string, long> names)
        {
            Pat0Header.NameOffset = (int)(BrresFile.WriteString(writer, names, Name) - Address);

            for (int i = 0; i < Entries.Count; i++)
                Entries[i].WriteNames(writer, names);

            for (int i = 0; i < StringList1.Count; i++)
                StringOffsetList1[i] = (int)(BrresFile.WriteString(writer, names, StringList1[i]) - Address - Pat0Header.StringList1Offset);
        }
    }

    public class Pat0Header
    {
        public long Address { get; set; }

        public int DataOffset { get; set; }
        public int StringList1Offset { get; set; }
        public int StringList2Offset { get; set; } // Something is wrong with these; misinformation?
        public int StringList3Offset { get; set; } // Something is wrong with these; misinformation?
        public int Length { get; set; }
        public int Unknown14 { get; set; } // 0x00000000
        public int NameOffset { get; set; } 
        public int Unknown1C { get; set; } // 0x00000000
        public short Duration { get; set; } // Probably, it's something to do with that!
        public short EntryCount { get; set; }
        public short StringList1Count { get; set; }
        public short StringList2Count { get; set; } // Something is wrong with these; misinformation?
        public short StringList3Count { get; set; } // Something is wrong with these; misinformation?
        public short Unknown2A { get; set; } // 0x0001 or 0x0000

        public Pat0Header(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            DataOffset = reader.ReadInt32();
            StringList1Offset = reader.ReadInt32();
            StringList2Offset = reader.ReadInt32();
            StringList3Offset = reader.ReadInt32();
            Length = reader.ReadInt32();
            Unknown14 = reader.ReadInt32();
            NameOffset = reader.ReadInt32();
            Unknown1C = reader.ReadInt32();
            Duration = reader.ReadInt16();
            EntryCount = reader.ReadInt16();
            StringList1Count = reader.ReadInt16();
            StringList2Count = reader.ReadInt16();
            StringList3Count = reader.ReadInt16();
            Unknown2A = reader.ReadInt16();
        }

        public void Write(EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            writer.Write(DataOffset);
            writer.Write(StringList1Offset);
            writer.Write(StringList2Offset);
            writer.Write(StringList3Offset);
            writer.Write(Length);
            writer.Write(Unknown14);
            writer.Write(NameOffset);
            writer.Write(Unknown1C);
            writer.Write(Duration);
            writer.Write(EntryCount);
            writer.Write(StringList1Count);
            writer.Write(StringList2Count);
            writer.Write(StringList3Count);
            writer.Write(Unknown2A);
        }
    }

    public class Pat0Entry
    {
        public long Address { get; set; }

        public int NameOffset { get; set; }
        public int Type { get; set; }
        public int DataOffset { get; set; }

        public string Name { get; set; }
        public Pat0EntryData Data { get; set; }

        public Pat0Entry(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            NameOffset = reader.ReadInt32();
            Type = reader.ReadInt32();
            DataOffset = reader.ReadInt32();

            Name = BrresFile.ReadBrresString(reader, Address + NameOffset);

            if (DataOffset != 0)
            {
                BrresFile.SafeSeek(reader, Address + DataOffset, 8);
                Data = new Pat0EntryData(reader);
            }
        }

        public void Write(EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            if (Data != null)
                DataOffset = (int)(Data.Address - Address);
            else
                DataOffset = 0;

            writer.Write(NameOffset);
            writer.Write(Type);
            writer.Write(DataOffset);

            if (Data != null)
                Data.Write(writer);
        }

        public void WriteNames(EndianBinaryWriter writer, Dictionary<string, long> names)
        {
            NameOffset = (int)(BrresFile.WriteString(writer, names, Name) - Address);
        }
    }

    public class Pat0EntryData
    {
        public long Address { get; set; }

        public int Count { get; set; }
        public float Interval { get; set; }

        public Collection<Pat0EntryDataEntry> Entries { get; private set; }

        public Pat0EntryData(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            Count = reader.ReadInt32();
            Interval = reader.ReadSingle();

            Entries = new Collection<Pat0EntryDataEntry>();

            for (int i = 0; i < Count; i++)
            {
                Entries.Add(new Pat0EntryDataEntry(reader));
            }
        }

        public void Write(EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            writer.Write(Count);
            writer.Write(Interval);

            for (int i = 0; i < Entries.Count; i++)
                Entries[i].Write(writer);
        }
    }

    public class Pat0EntryDataEntry
    {
        public float Time { get; set; }
        public short Texture { get; set; }
        public short Unknown6 { get; set; }

        public Pat0EntryDataEntry(EndianBinaryReader reader)
        {
            Time = reader.ReadSingle();
            Texture = reader.ReadInt16();
            Unknown6 = reader.ReadInt16();
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(Time);
            writer.Write(Texture);
            writer.Write(Unknown6);
        }
    }

}
