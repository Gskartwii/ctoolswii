using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;

namespace Chadsoft.CTools.Brres
{
    public class RootSection : BrresSection
    {
        public int Tag { get; set; }
        public int Length { get; set; }
        public IndexGroup Root { get; private set; }
        public Collection<IndexGroup> Folders { get; private set; }

        public RootSection(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            Tag = reader.ReadInt32();
            Length = reader.ReadInt32();

            Root = new IndexGroup(reader);
            Folders = new Collection<IndexGroup>();

            for (int i = 1; i < Root.Entries.Count; i++)
            {
                BrresFile.SafeSeek(reader, Root.Address + Root.Entries[i].DataOffset, 0x18);

                Folders.Add(new IndexGroup(reader));
            }
        }

        public override void Write(EndianBinaryWriter writer)
        {
            List<BrresSection> sections;

            Address = writer.BaseStream.Position;

            writer.Write(Tag);
            writer.Write(Length);
            Root.Write(writer);

            for (int i = 0; i < Folders.Count; i++)
            {
                Root.Entries[i + 1].DataOffset = (int)(Folders[i].Address - Root.Address);
                Folders[i].Write(writer);
            }

            Length = (int)(writer.BaseStream.Position - Address);

            writer.WritePadding(0x20, 0);

            sections = new List<BrresSection>();

            for (int i = 0; i < Folders.Count; i++)
                for (int j = 1; j < Folders[i].Entries.Count; j++)
                {
                    if (!sections.Contains(Folders[i].Entries[j].Section))
                    {
                        sections.Add(Folders[i].Entries[i].Section);
                        Folders[i].Entries[j].Section.Write(writer);
                        writer.WritePadding(0x20, 0);
                    }
                }
        }

        public override void WriteNames(EndianBinaryWriter writer, Dictionary<string, long> names)
        {
            Root.WriteNames(writer, names);

            for (int i = 0; i < Folders.Count; i++)
                Folders[i].WriteNames(writer, names);

            for (int i = 0; i < Folders.Count; i++)
                for (int j = 1; j < Folders[i].Entries.Count; j++)
                    Folders[i].Entries[j].Section.WriteNames(writer, names);
        }
    }
}
