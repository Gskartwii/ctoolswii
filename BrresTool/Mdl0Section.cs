using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using Chadsoft.CTools.Models;

namespace Chadsoft.CTools.Brres
{
    public class Mdl0Section : BrresSection 
    {
        private Model model;

        public const int Tag = 0x4d444c30;

        public Collection<int> SectionPointers { get; private set; }
        public Collection<IndexGroup> Sections { get; private set; }
        public BrresCommonSectionHeader Header { get; private set; }
        public int NameOffset { get; set; }

        public Mdl0Header Mdl0Header { get; private set; }
        public Mdl0BoneTable BoneTable { get; private set; }
        public Collection<Mdl0DrawList> DrawLists { get; private set; }
        public Collection<Mdl0Bone> Bones { get; private set; }
        public Collection<Mdl0VertexGroup> VertexGroups { get; private set; }
        public Collection<Mdl0NormalGroup> NormalGroups { get; private set; }
        public Collection<Mdl0ColourGroup> ColourGroups { get; private set; }
        public Collection<Mdl0UvGroup> UvGroups { get; private set; }
        public Collection<Mdl0Material> Materials { get; private set; }
        public Collection<Mdl0Shader> Shaders { get; private set; }
        public Collection<Mdl0Polygon> Polygons { get; private set; }
        public Collection<Mdl0TexturePointer> TexturePointers { get; private set; }

        public Mdl0Section(EndianBinaryReader reader, BrresCommonSectionHeader header)
        {
            int[] pointers;

            Address = reader.BaseStream.Position - 0x10;

            Header = header;

            if (header.Length < 4 * (header.Version + 3) + 0x50)
                throw new InvalidDataException();

            SectionPointers = new Collection<int>();
            pointers = reader.ReadInt32s(header.Version + 3);
            for (int i = 0; i < pointers.Length; i++)
                SectionPointers.Add(pointers[i]);

            for (int i = 0; i < SectionPointers.Count; i++)
                if (SectionPointers[i] > header.Length)
                    throw new InvalidDataException();

            NameOffset = reader.ReadInt32();
            Mdl0Header = new Mdl0Header(reader);
            Name = BrresFile.ReadBrresString(reader, Address + NameOffset);

            LoadBoneTable(reader);
            LoadSections(reader);
            LoadSection0(reader);
            LoadSection1(reader);
            LoadSection2(reader);
            LoadSection3(reader);
            LoadSection4(reader);
            LoadSection5(reader);
            LoadSection8(reader);
            LoadSection9(reader);
            LoadSectionA(reader);
            LoadSectionB(reader);
        }

        private void LoadBoneTable(EndianBinaryReader reader)
        {
            BrresFile.SafeSeek(reader, Mdl0Header.Address + Mdl0Header.BoneTableOffset, 4);

            BoneTable = new Mdl0BoneTable(reader);
        }

        private void LoadSections(EndianBinaryReader reader)
        {
            Sections = new Collection<IndexGroup>();
            for (int i = 0; i < SectionPointers.Count; i++)
                if (SectionPointers[i] != 0)
                {
                    BrresFile.SafeSeek(reader, Address + SectionPointers[i], 0x18);

                    Sections.Add(new IndexGroup(reader));
                }
                else
                    Sections.Add(null);
        }

        private void LoadSection0(EndianBinaryReader reader)
        {
            DrawLists = new Collection<Mdl0DrawList>();
            if (Sections[0] != null)
                for (int i = 1; i < Sections[0].Entries.Count; i++)
                {
                    BrresFile.SafeSeek(reader, Sections[0].Address + Sections[0].Entries[i].DataOffset, 0xd0);

                    DrawLists.Add(new Mdl0DrawList(reader));
                }
        }

        private void LoadSection1(EndianBinaryReader reader)
        {
            Bones = new Collection<Mdl0Bone>();
            if (Sections[1] != null)
                for (int i = 1; i < Sections[1].Entries.Count; i++)
                {
                    BrresFile.SafeSeek(reader, Sections[1].Address + Sections[1].Entries[i].DataOffset, 0xd0);

                    Bones.Add(new Mdl0Bone(reader));
                }

            for (int i = 0; i < Bones.Count; i++)
                Bones[i].LoadTree(Bones);
        }

        private void LoadSection2(EndianBinaryReader reader)
        {
            VertexGroups = new Collection<Mdl0VertexGroup>();
            if (Sections[2] != null)
                for (int i = 1; i < Sections[2].Entries.Count; i++)
                {
                    BrresFile.SafeSeek(reader, Sections[2].Address + Sections[2].Entries[i].DataOffset, 0x38);

                    VertexGroups.Add(new Mdl0VertexGroup(reader));
                }
        }

        private void LoadSection3(EndianBinaryReader reader)
        {
            NormalGroups = new Collection<Mdl0NormalGroup>();
            if (Sections[3] != null)
                for (int i = 1; i < Sections[3].Entries.Count; i++)
                {
                    BrresFile.SafeSeek(reader, Sections[3].Address + Sections[3].Entries[i].DataOffset, 0x20);

                    NormalGroups.Add(new Mdl0NormalGroup(reader));
                }
        }

        private void LoadSection4(EndianBinaryReader reader)
        {
            ColourGroups = new Collection<Mdl0ColourGroup>();
            if (Sections[4] != null)
                for (int i = 1; i < Sections[4].Entries.Count; i++)
                {
                    BrresFile.SafeSeek(reader, Sections[4].Address + Sections[4].Entries[i].DataOffset, 0x20);

                    ColourGroups.Add(new Mdl0ColourGroup(reader));
                }
        }

        private void LoadSection5(EndianBinaryReader reader)
        {
            UvGroups = new Collection<Mdl0UvGroup>();
            if (Sections[5] != null)
                for (int i = 1; i < Sections[5].Entries.Count; i++)
                {
                    BrresFile.SafeSeek(reader, Sections[5].Address + Sections[5].Entries[i].DataOffset, 0x30);

                    UvGroups.Add(new Mdl0UvGroup(reader));
                }
        }

        private void LoadSection8(EndianBinaryReader reader)
        {
            Dictionary<long, int> addresses;

            addresses = new Dictionary<long, int>();
            Materials = new Collection<Mdl0Material>();
            if (Sections[8] != null)
                for (int i = 1; i < Sections[8].Entries.Count; i++)
                {
                    BrresFile.SafeSeek(reader, Sections[8].Address + Sections[8].Entries[i].DataOffset, 0x5D0);

                    if (!addresses.ContainsKey(reader.BaseStream.Position))
                    {
                        addresses.Add(reader.BaseStream.Position, Materials.Count);
                        Materials.Add(new Mdl0Material(reader));
                    }
                    else
                        Materials.Add(Materials[addresses[reader.BaseStream.Position]]);
                }
        }

        private void LoadSection9(EndianBinaryReader reader)
        {
            Dictionary<long, int> addresses;

            addresses = new Dictionary<long, int>();
            Shaders = new Collection<Mdl0Shader>();
            if (Sections[9] != null)
                for (int i = 1; i < Sections[9].Entries.Count; i++)
                {
                    BrresFile.SafeSeek(reader, Sections[9].Address + Sections[9].Entries[i].DataOffset, 0x20);
                    
                    if (!addresses.ContainsKey(reader.BaseStream.Position))
                    {
                        addresses.Add(reader.BaseStream.Position, Shaders.Count);
                        Shaders.Add(new Mdl0Shader(reader));
                    }
                    else
                        Shaders.Add(Shaders[addresses[reader.BaseStream.Position]]);
                }

            for (int i = 0; i < Shaders.Count; i++)
                for (int j = 0; j < Materials.Count; j++)
                    if (Materials[j].ShaderOffset + Materials[j].Address == Shaders[i].Address)
                        Materials[j].Shader = Shaders[i];
        }

        private void LoadSectionA(EndianBinaryReader reader)
        {
            Polygons = new Collection<Mdl0Polygon>();
            if (Sections[10] != null)
                for (int i = 1; i < Sections[10].Entries.Count; i++)
                {
                    BrresFile.SafeSeek(reader, Sections[10].Address + Sections[10].Entries[i].DataOffset, 0x20);

                    Polygons.Add(new Mdl0Polygon(reader));
                }

            for (int i = 0; i < Polygons.Count; i++)
            {
                for (int j = 0; j < VertexGroups.Count; j++)
                    if (VertexGroups[j].Index == Polygons[i].VertexGroupIndex)
                    {
                        Polygons[i].VertexGroup = VertexGroups[j];
                    }

                for (int j = 0; j < NormalGroups.Count; j++)
                    if (NormalGroups[j].Index == Polygons[i].NormalGroupIndex)
                    {
                        Polygons[i].NormalGroup = NormalGroups[j];
                    }

                for (int j = 0; j < ColourGroups.Count; j++)
                    for (int k = 0; k < Polygons[i].ColourGroupIndicies.Length; k++)
                        if (ColourGroups[j].Index == Polygons[i].ColourGroupIndicies[k])
                        {
                            Polygons[i].ColourGroups[k] = ColourGroups[j];
                        }

                for (int j = 0; j < UvGroups.Count; j++)
                    for (int k = 0; k < Polygons[i].UvGroupInicies.Length; k++)
                        if (UvGroups[j].Index == Polygons[i].UvGroupInicies[k])
                        {
                            Polygons[i].UvGroups[k] = UvGroups[j];
                        }

                if (Polygons[i].BoneIndex == -1)
                {
                    for (int j = 0; j < Bones.Count; j++)
                        for (int k = 0; k < Polygons[i].BoneTable.Bones.Length; k++)
                            if (Bones[j].Index == Polygons[i].BoneTable.Bones[k])
                            {
                                Polygons[i].Bones[k] = Bones[j];
                            }
                }
                else
                    for (int j = 0; j < Bones.Count; j++)
                        if (Bones[j].Index == Polygons[i].BoneIndex)
                        {
                            Polygons[i].Bones[0] = Bones[j];
                        }
            }
        }

        private void LoadSectionB(EndianBinaryReader reader)
        {
            TexturePointers = new Collection<Mdl0TexturePointer>();
            if (Sections[11] != null)
                for (int i = 1; i < Sections[11].Entries.Count; i++)
                {
                    BrresFile.SafeSeek(reader, Sections[11].Address + Sections[11].Entries[i].DataOffset, 0x20);

                    TexturePointers.Add(new Mdl0TexturePointer(reader));
                }

            for (int i = 0; i < TexturePointers.Count; i++)
                for (int j = 0; j < TexturePointers[i].Count; j++)
                    for (int k = 0; k < Materials.Count; k++)
                        if (Materials[k].Address == TexturePointers[i].Pointers[j].MaterialOffset + TexturePointers[i].Address)
                        {
                            TexturePointers[i].Pointers[j].Material = Materials[k];
                            for (int l = 0; l < Materials[k].LayerCount; l++)
                                if (Materials[k].Layers[l].Address == TexturePointers[i].Pointers[j].LayerOffset + TexturePointers[i].Address)
                                {
                                    TexturePointers[i].Pointers[j].Layer = Materials[k].Layers[l];
                                    break;
                                }
                            break;
                        }
        }

        public override void Write(EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            Header.Write(writer);
            writer.Write(SectionPointers.ToArray(), 0, SectionPointers.Count);
            writer.Write(NameOffset);
            Mdl0Header.Write(writer);

            Mdl0Header.BoneTableOffset = (int)(writer.BaseStream.Position - Mdl0Header.Address);
            BoneTable.BoneTable = new int[Bones.Count];
            for (int i = 0; i < BoneTable.BoneTable.Length; i++)
                BoneTable.BoneTable[Bones[i].NodeId] = i;
            BoneTable.Write(writer);

            WriteSection0Header(writer);
            WriteSection1Header(writer);
            WriteSection2Header(writer);
            WriteSection3Header(writer);
            WriteSection4Header(writer);
            WriteSection5Header(writer);
            WriteSection8Header(writer);
            WriteSection9Header(writer);
            WriteSectionAHeader(writer);
            WriteSectionBHeader(writer);

            WriteSectionB(writer);
            WriteSection0(writer);
            WriteSection1(writer);
            WriteSection8(writer);
            WriteSection9(writer);
            WriteSectionA(writer);
            WriteSection2(writer);
            WriteSection3(writer);
            WriteSection4(writer);
            WriteSection5(writer);

            writer.WritePadding(0x20, 0);

            Header.Length = (int)(writer.BaseStream.Position - Address);
        }

        private void WriteSection0Header(EndianBinaryWriter writer)
        {
            if (DrawLists.Count > 0)
            {
                if (Sections[0] == null)
                    Sections.Add(new IndexGroup(DrawLists.Count));
                else if (Sections[0].Entries.Count != DrawLists.Count + 1)
                {
                    while (Sections[0].Entries.Count != DrawLists.Count + 1)
                        if (Sections[0].Entries.Count > DrawLists.Count + 1)
                            Sections[0].Entries.RemoveAt(DrawLists.Count + 1);
                        else
                            Sections[0].Entries.Add(new IndexEntry());
                }
                SectionPointers[0] = (int)(writer.BaseStream.Position - Address);

                for (int i = 0; i < DrawLists.Count; i++)
                    Sections[0].Entries[i + 1].DataOffset = (int)(DrawLists[i].Address - Sections[0].Address);

                Sections[0].Write(writer);
            }
            else
                SectionPointers[0] = 0;
        }

        private void WriteSection1Header(EndianBinaryWriter writer)
        {
            if (Bones.Count > 0)
            {
                if (Sections[1] == null)
                    Sections.Add(new IndexGroup(Bones.Count));
                else if (Sections[1].Entries.Count != Bones.Count + 1)
                {
                    while (Sections[1].Entries.Count != Bones.Count + 1)
                        if (Sections[1].Entries.Count > Bones.Count + 1)
                            Sections[1].Entries.RemoveAt(Bones.Count + 1);
                        else
                            Sections[1].Entries.Add(new IndexEntry());
                }
                SectionPointers[1] = (int)(writer.BaseStream.Position - Address);

                for (int i = 0; i < Bones.Count; i++)
                    Sections[1].Entries[i + 1].DataOffset = (int)(Bones[i].Address - Sections[1].Address);

                Sections[1].Write(writer);
            }
            else
                SectionPointers[1] = 0;
        }

        private void WriteSection2Header(EndianBinaryWriter writer)
        {
            if (VertexGroups.Count > 0)
            {
                if (Sections[2] == null)
                    Sections.Add(new IndexGroup(VertexGroups.Count));
                else if (Sections[2].Entries.Count != VertexGroups.Count + 1)
                {
                    while (Sections[2].Entries.Count != VertexGroups.Count + 1)
                        if (Sections[2].Entries.Count > VertexGroups.Count + 1)
                            Sections[2].Entries.RemoveAt(VertexGroups.Count + 1);
                        else
                            Sections[2].Entries.Add(new IndexEntry());
                }
                SectionPointers[2] = (int)(writer.BaseStream.Position - Address);

                for (int i = 0; i < VertexGroups.Count; i++)
                    Sections[2].Entries[i + 1].DataOffset = (int)(VertexGroups[i].Address - Sections[2].Address);

                Sections[2].Write(writer);
            }
            else
                SectionPointers[2] = 0;
        }

        private void WriteSection3Header(EndianBinaryWriter writer)
        {
            if (NormalGroups.Count > 0)
            {
                if (Sections[3] == null)
                    Sections.Add(new IndexGroup(NormalGroups.Count));
                else if (Sections[3].Entries.Count != NormalGroups.Count + 1)
                {
                    while (Sections[3].Entries.Count != NormalGroups.Count + 1)
                        if (Sections[3].Entries.Count > NormalGroups.Count + 1)
                            Sections[3].Entries.RemoveAt(NormalGroups.Count + 1);
                        else
                            Sections[3].Entries.Add(new IndexEntry());
                }
                SectionPointers[3] = (int)(writer.BaseStream.Position - Address);

                for (int i = 0; i < NormalGroups.Count; i++)
                    Sections[3].Entries[i + 1].DataOffset = (int)(NormalGroups[i].Address - Sections[3].Address);

                Sections[3].Write(writer);
            }
            else
                SectionPointers[3] = 0;
        }

        private void WriteSection4Header(EndianBinaryWriter writer)
        {
            if (ColourGroups.Count > 0)
            {
                if (Sections[4] == null)
                    Sections.Add(new IndexGroup(ColourGroups.Count));
                else if (Sections[4].Entries.Count != ColourGroups.Count + 1)
                {
                    while (Sections[4].Entries.Count != ColourGroups.Count + 1)
                        if (Sections[4].Entries.Count > ColourGroups.Count + 1)
                            Sections[4].Entries.RemoveAt(ColourGroups.Count + 1);
                        else
                            Sections[4].Entries.Add(new IndexEntry());
                }
                SectionPointers[4] = (int)(writer.BaseStream.Position - Address);

                for (int i = 0; i < ColourGroups.Count; i++)
                    Sections[4].Entries[i + 1].DataOffset = (int)(ColourGroups[i].Address - Sections[4].Address);

                Sections[4].Write(writer);
            }
            else
                SectionPointers[4] = 0;
        }

        private void WriteSection5Header(EndianBinaryWriter writer)
        {
            if (UvGroups.Count > 0)
            {
                if (Sections[5] == null)
                    Sections.Add(new IndexGroup(UvGroups.Count));
                else if (Sections[5].Entries.Count != UvGroups.Count + 1)
                {
                    while (Sections[5].Entries.Count != UvGroups.Count + 1)
                        if (Sections[5].Entries.Count > UvGroups.Count + 1)
                            Sections[5].Entries.RemoveAt(UvGroups.Count + 1);
                        else
                            Sections[5].Entries.Add(new IndexEntry());
                }
                SectionPointers[5] = (int)(writer.BaseStream.Position - Address);

                for (int i = 0; i < UvGroups.Count; i++)
                    Sections[5].Entries[i + 1].DataOffset = (int)(UvGroups[i].Address - Sections[5].Address);

                Sections[5].Write(writer);
            }
            else
                SectionPointers[5] = 0;
        }

        private void WriteSection8Header(EndianBinaryWriter writer)
        {
            if (Materials.Count > 0)
            {
                if (Sections[8] == null)
                    Sections.Add(new IndexGroup(Materials.Count));
                else if (Sections[8].Entries.Count != Materials.Count + 1)
                {
                    while (Sections[8].Entries.Count != Materials.Count + 1)
                        if (Sections[8].Entries.Count > Materials.Count + 1)
                            Sections[8].Entries.RemoveAt(Materials.Count + 1);
                        else
                            Sections[8].Entries.Add(new IndexEntry());
                }
                SectionPointers[8] = (int)(writer.BaseStream.Position - Address);

                for (int i = 0; i < Materials.Count; i++)
                    Sections[8].Entries[i + 1].DataOffset = (int)(Materials[i].Address - Sections[8].Address);

                Sections[8].Write(writer);
            }
            else
                SectionPointers[8] = 0;
        }

        private void WriteSection9Header(EndianBinaryWriter writer)
        {
            if (Shaders.Count > 0)
            {
                if (Sections[9] == null)
                    Sections.Add(new IndexGroup(Shaders.Count));
                else if (Sections[9].Entries.Count != Shaders.Count + 1)
                {
                    while (Sections[9].Entries.Count != Shaders.Count + 1)
                        if (Sections[9].Entries.Count > Shaders.Count + 1)
                            Sections[9].Entries.RemoveAt(Shaders.Count + 1);
                        else
                            Sections[9].Entries.Add(new IndexEntry());
                }
                SectionPointers[9] = (int)(writer.BaseStream.Position - Address);

                for (int i = 0; i < Shaders.Count; i++)
                    Sections[9].Entries[i + 1].DataOffset = (int)(Shaders[i].Address - Sections[9].Address);

                Sections[9].Write(writer);
            }
            else
                SectionPointers[9] = 0;
        }

        private void WriteSectionAHeader(EndianBinaryWriter writer)
        {
            if (Polygons.Count > 0)
            {
                if (Sections[10] == null)
                    Sections.Add(new IndexGroup(Polygons.Count));
                else if (Sections[10].Entries.Count != Polygons.Count + 1)
                {
                    while (Sections[10].Entries.Count != Polygons.Count + 1)
                        if (Sections[10].Entries.Count > Polygons.Count + 1)
                            Sections[10].Entries.RemoveAt(Polygons.Count + 1);
                        else
                            Sections[10].Entries.Add(new IndexEntry());
                }
                SectionPointers[10] = (int)(writer.BaseStream.Position - Address);

                for (int i = 0; i < Polygons.Count; i++)
                    Sections[10].Entries[i + 1].DataOffset = (int)(Polygons[i].Address - Sections[10].Address);

                Sections[10].Write(writer);
            }
            else
                SectionPointers[10] = 0;
        }

        private void WriteSectionBHeader(EndianBinaryWriter writer)
        {
            if (TexturePointers.Count > 0)
            {
                if (Sections[11] == null)
                    Sections.Add(new IndexGroup(TexturePointers.Count));
                else if (Sections[11].Entries.Count != TexturePointers.Count + 1)
                {
                    while (Sections[11].Entries.Count != TexturePointers.Count + 1)
                        if (Sections[11].Entries.Count > TexturePointers.Count + 1)
                            Sections[11].Entries.RemoveAt(TexturePointers.Count + 1);
                        else
                            Sections[11].Entries.Add(new IndexEntry());
                }
                SectionPointers[11] = (int)(writer.BaseStream.Position - Address);

                for (int i = 0; i < TexturePointers.Count; i++)
                    Sections[11].Entries[i + 1].DataOffset = (int)(TexturePointers[i].Address - Sections[11].Address);

                Sections[11].Write(writer);
            }
            else
                SectionPointers[11] = 0;
        }

        private void WriteSection0(EndianBinaryWriter writer)
        {
            for (int i = 0; i < DrawLists.Count; i++)
                DrawLists[i].Write(writer);
        }

        private void WriteSection1(EndianBinaryWriter writer)
        {
            writer.WritePadding(0x10, 0);
            for (int i = 0; i < Bones.Count; i++)
            {
                Sections[1].Entries[i + 1].Name = Bones[i].Name;
                Bones[i].Write(writer, Address);
            }
        }

        private void WriteSection2(EndianBinaryWriter writer)
        {
            writer.WritePadding(0x10, 0);
            for (int i = 0; i < VertexGroups.Count; i++)
            {
                Sections[2].Entries[i + 1].Name = VertexGroups[i].Name;
                VertexGroups[i].Write(writer, Address);
            }
        }

        private void WriteSection3(EndianBinaryWriter writer)
        {
            writer.WritePadding(0x10, 0);
            for (int i = 0; i < NormalGroups.Count; i++)
            {
                Sections[3].Entries[i + 1].Name = NormalGroups[i].Name;
                NormalGroups[i].Write(writer, Address);
            }
        }

        private void WriteSection4(EndianBinaryWriter writer)
        {
            writer.WritePadding(0x10, 0);
            for (int i = 0; i < ColourGroups.Count; i++)
            {
                Sections[4].Entries[i + 1].Name = ColourGroups[i].Name;
                ColourGroups[i].Write(writer, Address);
            }
        }

        private void WriteSection5(EndianBinaryWriter writer)
        {
            writer.WritePadding(0x10, 0);
            for (int i = 0; i < UvGroups.Count; i++)
            {
                Sections[5].Entries[i + 1].Name = UvGroups[i].Name;
                UvGroups[i].Write(writer, Address);
            }
        }

        private void WriteSection8(EndianBinaryWriter writer)
        {
            List<Mdl0Material> written;

            written = new List<Mdl0Material>();
            writer.WritePadding(0x10, 0);
            for (int i = 0; i < Materials.Count; i++)
            {
                Sections[8].Entries[i + 1].Name = Materials[i].Name;
                if (!written.Contains(Materials[i]))
                {
                    written.Add(Materials[i]);
                    Materials[i].Write(writer, Address);
                }
            }
        }

        private void WriteSection9(EndianBinaryWriter writer)
        {
            List<Mdl0Shader> written;

            written = new List<Mdl0Shader>();
            writer.WritePadding(0x10, 0);
            for (int i = 0; i < Shaders.Count; i++)
            {
                Sections[9].Entries[i + 1].Name = Materials[i].Name;
                if (!written.Contains(Shaders[i]))
                {
                    written.Add(Shaders[i]);
                    Shaders[i].Write(writer, Address);
                }
            }
        }

        private void WriteSectionA(EndianBinaryWriter writer)
        {
            writer.WritePadding(0x10, 0);
            for (int i = 0; i < Polygons.Count; i++)
            {
                Sections[10].Entries[i + 1].Name = Polygons[i].Name;
                Polygons[i].Write(writer, Address);
            }
        }

        private void WriteSectionB(EndianBinaryWriter writer)
        {
            for (int i = 0; i < TexturePointers.Count; i++)
                TexturePointers[i].Write(writer);
        }

        public override void WriteNames(EndianBinaryWriter writer, Dictionary<string, long> names)
        {
            if (Name != null)
                NameOffset = (int)(BrresFile.WriteString(writer, names, Name) - Address);
            else
                NameOffset = 0;

            for (int i = 0; i < Sections.Count; i++)
                if (Sections[i] != null)
                    Sections[i].WriteNames(writer, names);
            for (int i = 0; i < Bones.Count; i++)
                Bones[i].WriteNames(writer, names);
            for (int i = 0; i < VertexGroups.Count; i++)
                VertexGroups[i].WriteNames(writer, names);
            for (int i = 0; i < NormalGroups.Count; i++)
                NormalGroups[i].WriteNames(writer, names);
            for (int i = 0; i < ColourGroups.Count; i++)
                ColourGroups[i].WriteNames(writer, names);
            for (int i = 0; i < UvGroups.Count; i++)
                UvGroups[i].WriteNames(writer, names);
            for (int i = 0; i < Materials.Count; i++)
                Materials[i].WriteNames(writer, names);
            for (int i = 0; i < Polygons.Count; i++)
                Polygons[i].WriteNames(writer, names);
        }

        public Model GenerateModel(BrresFile brres)
        {
            if (model == null)
            {
                model = new Model();

                if (Bones.Count == BoneTable.Count)
                    for (int i = 0; i < Bones.Count; i++)
                        model.Bones.Add(Bones[BoneTable.BoneTable[i]].ToBone());
                else
                    for (int i = 0; i < Bones.Count; i++)
                        model.Bones.Add(Bones[i].ToBone());

                if (Bones.Count == BoneTable.Count)
                    for (int i = 0; i < Bones.Count; i++)
                    {
                        if (Bones[BoneTable.BoneTable[i]].Parent != null)
                            model.Bones[i].Parent = model.Bones[BoneTable.BoneTable[Bones[BoneTable.BoneTable[i]].Parent.Index]];
                    }
                else
                    for (int i = 0; i < Bones.Count; i++)
                    {
                        if (Bones[i].Parent != null)
                            model.Bones[i].Parent = model.Bones[Bones[i].Parent.Index];
                    }

                for (int i = 0; i < Materials.Count; i++)
                    model.Materials.Add(Materials[i].ToMaterial(brres));

                for (int i = 0; i < Polygons.Count; i++)
                    model.Polygons.Add(Polygons[i].GeneratePolygon(model));

                for (int i = 0; i < DrawLists.Count; i++)
                    for (int j = 0; j < DrawLists[i].Instructions.Count; j++)
                    {
                        switch (DrawLists[i].Instructions[j].Instruction)
	                    {
		                    case 4:
                                model.Instructions.Add(new ModelRenderInstruction() { Polygon = DrawLists[i].Instructions[j].Parameter1, Material = DrawLists[i].Instructions[j].Parameter2 });
                                break;
	                    }
                    }
            }

            return model;
        }
    }

    public class Mdl0Header
    {
        public long Address { get; set; }
        public int Length { get; set; }
        public int Mdl0Offset { get; set; }
        public int Unknown8 { get; set; } // 0x00000000
        public int UnknownC { get; set; } // 0x00000000
        public int VertexCount { get; set; }
        public int FaceCount { get; set; }
        public int Unknown18 { get; set; } // 0x00000000
        public int BoneCount { get; set; }
        public int Unknown20 { get; set; } // 0x01000000
        public int BoneTableOffset { get; set; }
        public Matrix3x1 Min { get; private set; }
        public Matrix3x1 Max { get; private set; }

        public Mdl0Header(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            Length = reader.ReadInt32();
            Mdl0Offset = reader.ReadInt32();
            Unknown8 = reader.ReadInt32();
            UnknownC = reader.ReadInt32();
            VertexCount = reader.ReadInt32();
            FaceCount = reader.ReadInt32();
            Unknown18 = reader.ReadInt32();
            BoneCount = reader.ReadInt32();
            Unknown20 = reader.ReadInt32();
            BoneTableOffset = reader.ReadInt32();
            Min = new Matrix3x1(reader);
            Max = new Matrix3x1(reader);
        }

        public void Write(EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            writer.Write(Length);
            writer.Write(Mdl0Offset);
            writer.Write(Unknown8);
            writer.Write(UnknownC);
            writer.Write(VertexCount);
            writer.Write(FaceCount);
            writer.Write(Unknown18);
            writer.Write(BoneCount);
            writer.Write(Unknown20);
            writer.Write(BoneTableOffset);
            Min.Write(writer);
            Max.Write(writer);            
        }
    }

    public class Mdl0BoneTable
    {
        public long Address { get; set; }

        public int Count { get; set; }
        public int[] BoneTable { get; set; }

        public Mdl0BoneTable(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            Count = reader.ReadInt32();
            BoneTable = reader.ReadInt32s(Count);
        }

        public void Write(EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            Count = BoneTable.Length;

            writer.Write(Count);
            writer.Write(BoneTable, 0, BoneTable.Length);
        }
    }
}
