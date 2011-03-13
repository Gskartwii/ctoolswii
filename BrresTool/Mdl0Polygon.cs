using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using Chadsoft.CTools.Models;

namespace Chadsoft.CTools.Brres
{
    public class Mdl0Polygon
    {
        public long Address { get; set; }

        public int Length { get; set; }
        public int Mdl0Offset { get; set; }
        public int BoneIndex { get; set; }
        public int Unknown0C { get; set; }
        public int Unknown10 { get; set; }
        public int Unknown14 { get; set; }
        public Collection<Mdl0PolygonSection> Sections { get; private set; }
        public int Unknown18 { get; set; }
        public int Unknown1C { get; set; }
        public int NameOffset { get; set; }
        public int Index { get; set; }
        public int VertexCount { get; set; }
        public int FaceCount { get; set; }
        public short VertexGroupIndex { get; set; }
        public short NormalGroupIndex { get; set; }
        public short[] ColourGroupIndicies { get; set; }
        public short[] UvGroupInicies { get; set; }
        public int Unknown48 { get; set; }
        public int BoneTableOffset { get; set; }

        public Mdl0PolygonBoneTable BoneTable { get; private set; }
        public byte[] VertexDefinitions { get; set; }
        public byte[] VertexData { get; set; }
        public string Name { get; set; }

        public Mdl0VertexGroup VertexGroup { get; set; }
        public Mdl0NormalGroup NormalGroup { get; set; }
        public Mdl0ColourGroup[] ColourGroups { get; private set; }
        public Mdl0UvGroup[] UvGroups { get; private set; }
        public Mdl0Bone[] Bones { get; set; }

        public Mdl0Polygon(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            Length = reader.ReadInt32();
            Mdl0Offset = reader.ReadInt32();
            BoneIndex = reader.ReadInt32();
            Unknown0C = reader.ReadInt32();
            Unknown10 = reader.ReadInt32();
            Unknown14 = reader.ReadInt32();
            
            Sections = new Collection<Mdl0PolygonSection>();
            for (int i = 0; i < 2; i++)
                Sections.Add(new Mdl0PolygonSection(reader));

            Unknown18 = reader.ReadInt32();
            Unknown1C = reader.ReadInt32();
            NameOffset = reader.ReadInt32();
            Index = reader.ReadInt32();
            VertexCount = reader.ReadInt32();
            FaceCount = reader.ReadInt32();
            VertexGroupIndex = reader.ReadInt16();
            NormalGroupIndex = reader.ReadInt16();
            ColourGroupIndicies = reader.ReadInt16s(2);
            UvGroupInicies = reader.ReadInt16s(8);
            Unknown48 = reader.ReadInt32();
            BoneTableOffset = reader.ReadInt32();

            BrresFile.SafeSeek(reader, Address + BoneTableOffset, 4);
            BoneTable = new Mdl0PolygonBoneTable(reader);
            
            BrresFile.SafeSeek(reader, Sections[0].Address + Sections[0].Offset, Sections[0].Size);
            VertexDefinitions = reader.ReadBytes(Sections[0].Size);

            BrresFile.SafeSeek(reader, Sections[1].Address + Sections[1].Offset, Sections[1].Size);
            VertexData = reader.ReadBytes(Sections[1].Size);

            Name = BrresFile.ReadBrresString(reader, Address + NameOffset);

            ColourGroups = new Mdl0ColourGroup[2];
            UvGroups = new Mdl0UvGroup[8];
            if (BoneIndex != -1)
                Bones = new Mdl0Bone[1];
            else
                Bones = new Mdl0Bone[BoneTable.Count];
        }

        public void Write(EndianBinaryWriter writer, long mdl0Address)
        {
            Address = writer.BaseStream.Position;

            mdl0Address = (int)(mdl0Address - Address);
            if (VertexGroup != null) VertexGroupIndex = (short)VertexGroup.Index;
            else VertexGroupIndex = -1;
            if (NormalGroup != null) NormalGroupIndex = (short)NormalGroup.Index;
            else NormalGroupIndex = -1;
            for (int i = 0; i < ColourGroupIndicies.Length; i++)
                if (ColourGroups[i] != null) ColourGroupIndicies[i] = (short)ColourGroups[i].Index;
                else ColourGroupIndicies[i] = -1;
            for (int i = 0; i < UvGroupInicies.Length; i++)
                if (UvGroups[i] != null) UvGroupInicies[i] = (short)UvGroups[i].Index;
                else UvGroupInicies[i] = -1;

            BoneTableOffset = (int)(BoneTable.Address - Address);

            writer.Write(Length);
            writer.Write(Mdl0Offset);
            writer.Write(BoneIndex);
            writer.Write(Unknown0C);
            writer.Write(Unknown10);
            writer.Write(Unknown14);

            for (int i = 0; i < 2; i++)
                Sections[i].Write(writer);

            writer.Write(Unknown18);
            writer.Write(Unknown1C);
            writer.Write(NameOffset);
            writer.Write(Index);
            writer.Write(VertexCount);
            writer.Write(FaceCount);
            writer.Write(VertexGroupIndex);
            writer.Write(NormalGroupIndex);
            writer.Write(ColourGroupIndicies, 0, ColourGroupIndicies.Length);
            writer.Write(UvGroupInicies, 0, UvGroupInicies.Length);
            writer.Write(Unknown48);
            writer.Write(BoneTableOffset);

            BoneTableOffset = (int)(writer.BaseStream.Position - Address);
            BoneTable.Write(writer);

            writer.WritePadding(0x20, 0);
            Sections[0].Offset = (int)(writer.BaseStream.Position - Sections[0].Address);
            Sections[0].Size = VertexDefinitions.Length;
            writer.Write(VertexDefinitions, 0, VertexDefinitions.Length);
            writer.WritePadding(0x20, 0);
            Sections[1].Offset = (int)(writer.BaseStream.Position - Sections[1].Address);
            Sections[1].Size = VertexData.Length;
            writer.Write(VertexData, 0, VertexData.Length);

            writer.WritePadding(0x20, 0);
            
            Length = (int)(writer.BaseStream.Position - Address);
        }

        public void WriteNames(EndianBinaryWriter writer, Dictionary<string, long> names)
        {
            NameOffset = (int)(BrresFile.WriteString(writer, names, Name) - Address);
        }

        public Polygon GeneratePolygon(Model model)
        {
            VertexDescription desc;
            Polygon polygon;
            EndianBinaryWriter writer;

            polygon = new Polygon(model);
            desc = new VertexDescription(VertexDefinitions);
            writer = new EndianBinaryWriter(polygon.VertexData, Endianness.LittleEndian);
            polygon.VertexDataFormat = desc.GetFormat();

            GenerateVertexBuffer(polygon, desc, writer);

            return polygon;
        }

        private void GenerateVertexBuffer(Polygon polygon, VertexDescription desc, EndianBinaryWriter writer)
        {
            byte[] bones;
            byte instruction;
            int count, index, destination;
            MemoryStream stream; 
            EndianBinaryReader reader;
            PolygonRenderInstruction renderInstruction;

            stream = new MemoryStream(VertexData);
            reader = new EndianBinaryReader(stream);
            bones = new byte[21];

            while (stream.Position < VertexData.Length)
            {
                instruction = reader.ReadByte();
                count = 0;

                switch (instruction & 0xf8)
                {
                    case 0x20:
                        renderInstruction = new PolygonRenderInstruction()
                        {
                            Command = PolygonRenderInstructionCommand.SetMatrix,
                            Index = index = reader.ReadUInt16(),
                            Destination = destination = (reader.ReadInt16() & 0xFFF) / 0xc,
                        };
                        polygon.Instructions.Add(renderInstruction);
                        bones[destination] = (byte)index;
                        break;
                    case 0x28:
                        reader.ReadInt32();
                        break;
                    case 0x30:
                        reader.ReadInt32();
                        break;
                    case 0x38:
                        reader.ReadInt32();
                        break;
                    case 0x80:
                        renderInstruction = new PolygonRenderInstruction()
                        {
                            Command = PolygonRenderInstructionCommand.DrawQuads,
                            Count = (count = reader.ReadUInt16()) / 4,
                        };
                        polygon.Instructions.Add(renderInstruction);
                        break;
                    case 0x90:
                        polygon.Instructions.Add(new PolygonRenderInstruction() 
                        { 
                            Command = PolygonRenderInstructionCommand.DrawTriangles,
                            Count = (count = reader.ReadUInt16()) / 3,                            
                        });
                        break;
                    case 0x98:
                        renderInstruction = new PolygonRenderInstruction()
                        {
                            Command = PolygonRenderInstructionCommand.DrawTriangleStrip,
                            Count = (count = reader.ReadUInt16()) - 2,
                        };
                        polygon.Instructions.Add(renderInstruction);
                        break;
                    case 0xa0:
                        renderInstruction = new PolygonRenderInstruction()
                        {
                            Command = PolygonRenderInstructionCommand.DrawTriangleFan,
                            Count = (count = reader.ReadUInt16()) - 2,
                        };
                        polygon.Instructions.Add(renderInstruction);
                        break;
                    case 0xa8:
                        polygon.Instructions.Add(new PolygonRenderInstruction()
                        {
                            Command = PolygonRenderInstructionCommand.DrawLines,
                            Count = (count = reader.ReadUInt16()) / 2,
                        });
                        break;
                    case 0xb0:
                        renderInstruction = new PolygonRenderInstruction()
                        {
                            Command = PolygonRenderInstructionCommand.DrawLineStrip,
                            Count = (count = reader.ReadUInt16()) - 1,
                        };
                        polygon.Instructions.Add(renderInstruction);
                        break;
                    case 0xb8:
                        renderInstruction = new PolygonRenderInstruction()
                        {
                            Command = PolygonRenderInstructionCommand.DrawPoints,
                            Count = count = reader.ReadUInt16(),
                        };
                        polygon.Instructions.Add(renderInstruction);
                        break;
                    case 0x00:
                        break;
                    default:
                        return;
                }


                switch (instruction & 0xf8)
                {
                    case 0x80:
                    case 0x90:
                    case 0x98:
                    case 0xa0:
                    case 0xa8:
                    case 0xb0:
                    case 0xb8:
                        for (int i = 0; i < count; i++)
                        {
                            ReadVertex(desc, bones, reader, writer);
                        }
                        break;
                }
            }
        }

        private void ReadVertex(VertexDescription desc, byte[] bones, EndianBinaryReader reader, EndianBinaryWriter writer)
        {
            int matricies, colour0, colour1;
            Matrix3x1 position, normal;
            Matrix2x1 tex0, tex1, tex2, tex3, tex4, tex5, tex6, tex7;

            matricies = 0;
            if (desc.PosMatrix)
            {
                matricies = reader.ReadByte();

                if (matricies / 3 * 3 != matricies)
                    return;
                matricies /= 3;
                //matricies |= reader.ReadByte() / 3;// Array.IndexOf(bones, reader.ReadByte()) & 0x1F;
            }
            if (desc.TexMatrix0)
                matricies |= (reader.ReadByte() & 0x7) << 5;
            if (desc.TexMatrix1)
                matricies |= (reader.ReadByte() & 0x7) << 7;
            if (desc.TexMatrix2)
                matricies |= (reader.ReadByte() & 0x7) << 11;
            if (desc.TexMatrix3)
                matricies |= (reader.ReadByte() & 0x7) << 14;
            if (desc.TexMatrix4)
                matricies |= (reader.ReadByte() & 0x7) << 17;
            if (desc.TexMatrix5)
                matricies |= (reader.ReadByte() & 0x7) << 20;
            if (desc.TexMatrix6)
                matricies |= (reader.ReadByte() & 0x7) << 23;
            if (desc.TexMatrix7)
                matricies |= (reader.ReadByte() & 0x7) << 26;
            if (desc.HasMatrix)
                writer.Write(matricies);

            switch (desc.Pos)
            {
                case VertexDescription.ElementPresence.NotPresent:
                    break;
                case VertexDescription.ElementPresence.Direct:
                    position = new Mdl0Vertex(reader, (int)desc.PosFormat, desc.PosScale).ToMatrix();
                    position.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index8:
                    position = VertexGroup.Verticies[reader.ReadByte()].ToMatrix();
                    position.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index16:
                    position = VertexGroup.Verticies[reader.ReadUInt16()].ToMatrix();
                    position.Write(writer);
                    break;
            }

            switch (desc.Norm)
            {
                case VertexDescription.ElementPresence.NotPresent:
                    break;
                case VertexDescription.ElementPresence.Direct:
                    normal = new Mdl0Normal(reader, (int)desc.NormFormat, 0).ToMatrix();
                    normal.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index8:
                    normal = NormalGroup.Normals[reader.ReadByte()].ToMatrix();
                    normal.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index16:
                    normal = NormalGroup.Normals[reader.ReadUInt16()].ToMatrix();
                    normal.Write(writer);
                    break;
            }

            switch (desc.Col0)
            {
                case VertexDescription.ElementPresence.NotPresent:
                    break;
                case VertexDescription.ElementPresence.Direct:
                    colour0 = new Mdl0Colour(reader, (int)desc.Col0Format).ToArgb();
                    writer.Write(colour0);
                    break;
                case VertexDescription.ElementPresence.Index8:
                    colour0 = ColourGroups[0].Colours[reader.ReadByte()].ToArgb();
                    writer.Write(colour0);
                    break;
                case VertexDescription.ElementPresence.Index16:
                    colour0 = ColourGroups[0].Colours[reader.ReadUInt16()].ToArgb();
                    writer.Write(colour0);
                    break;
            }

            switch (desc.Col1)
            {
                case VertexDescription.ElementPresence.NotPresent:
                    break;
                case VertexDescription.ElementPresence.Direct:
                    colour1 = new Mdl0Colour(reader, (int)desc.Col1Format).ToArgb();
                    writer.Write(colour1);
                    break;
                case VertexDescription.ElementPresence.Index8:
                    colour1 = ColourGroups[1].Colours[reader.ReadByte()].ToArgb();
                    writer.Write(colour1);
                    break;
                case VertexDescription.ElementPresence.Index16:
                    colour1 = ColourGroups[1].Colours[reader.ReadUInt16()].ToArgb();
                    writer.Write(colour1);
                    break;
            }

            switch (desc.Tex0)
            {
                case VertexDescription.ElementPresence.NotPresent:
                    break;
                case VertexDescription.ElementPresence.Direct:
                    tex0 = new Mdl0Uv(reader, (int)desc.Tex0Format, 0).ToMatrix();
                    tex0.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index8:
                    tex0 = UvGroups[0].Uvs[reader.ReadByte()].ToMatrix();
                    tex0.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index16:
                    tex0 = UvGroups[0].Uvs[reader.ReadUInt16()].ToMatrix();
                    tex0.Write(writer);
                    break;
            }

            switch (desc.Tex1)
            {
                case VertexDescription.ElementPresence.NotPresent:
                    break;
                case VertexDescription.ElementPresence.Direct:
                    tex1 = new Mdl0Uv(reader, (int)desc.Tex1Format, 0).ToMatrix();
                    tex1.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index8:
                    tex1 = UvGroups[1].Uvs[reader.ReadByte()].ToMatrix();
                    tex1.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index16:
                    tex1 = UvGroups[1].Uvs[reader.ReadUInt16()].ToMatrix();
                    tex1.Write(writer);
                    break;
            }

            switch (desc.Tex2)
            {
                case VertexDescription.ElementPresence.NotPresent:
                    break;
                case VertexDescription.ElementPresence.Direct:
                    tex2 = new Mdl0Uv(reader, (int)desc.Tex2Format, 0).ToMatrix();
                    tex2.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index8:
                    tex2 = UvGroups[2].Uvs[reader.ReadByte()].ToMatrix();
                    tex2.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index16:
                    tex2 = UvGroups[2].Uvs[reader.ReadUInt16()].ToMatrix();
                    tex2.Write(writer);
                    break;
            }

            switch (desc.Tex3)
            {
                case VertexDescription.ElementPresence.NotPresent:
                    break;
                case VertexDescription.ElementPresence.Direct:
                    tex3 = new Mdl0Uv(reader, (int)desc.Tex3Format, 0).ToMatrix();
                    tex3.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index8:
                    tex3 = UvGroups[3].Uvs[reader.ReadByte()].ToMatrix();
                    tex3.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index16:
                    tex3 = UvGroups[3].Uvs[reader.ReadUInt16()].ToMatrix();
                    tex3.Write(writer);
                    break;
            }

            switch (desc.Tex4)
            {
                case VertexDescription.ElementPresence.NotPresent:
                    break;
                case VertexDescription.ElementPresence.Direct:
                    tex4 = new Mdl0Uv(reader, (int)desc.Tex4Format, 0).ToMatrix();
                    tex4.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index8:
                    tex4 = UvGroups[4].Uvs[reader.ReadByte()].ToMatrix();
                    tex4.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index16:
                    tex4 = UvGroups[4].Uvs[reader.ReadUInt16()].ToMatrix();
                    tex4.Write(writer);
                    break;
            }

            switch (desc.Tex5)
            {
                case VertexDescription.ElementPresence.NotPresent:
                    break;
                case VertexDescription.ElementPresence.Direct:
                    tex5 = new Mdl0Uv(reader, (int)desc.Tex5Format, 0).ToMatrix();
                    tex5.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index8:
                    tex5 = UvGroups[5].Uvs[reader.ReadByte()].ToMatrix();
                    tex5.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index16:
                    tex5 = UvGroups[5].Uvs[reader.ReadUInt16()].ToMatrix();
                    tex5.Write(writer);
                    break;
            }

            switch (desc.Tex6)
            {
                case VertexDescription.ElementPresence.NotPresent:
                    break;
                case VertexDescription.ElementPresence.Direct:
                    tex6 = new Mdl0Uv(reader, (int)desc.Tex6Format, 0).ToMatrix();
                    tex6.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index8:
                    tex6 = UvGroups[6].Uvs[reader.ReadByte()].ToMatrix();
                    tex6.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index16:
                    tex6 = UvGroups[6].Uvs[reader.ReadUInt16()].ToMatrix();
                    tex6.Write(writer);
                    break;
            }

            switch (desc.Tex7)
            {
                case VertexDescription.ElementPresence.NotPresent:
                    break;
                case VertexDescription.ElementPresence.Direct:
                    tex7 = new Mdl0Uv(reader, (int)desc.Tex7Format, 0).ToMatrix();
                    tex7.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index8:
                    tex7 = UvGroups[7].Uvs[reader.ReadByte()].ToMatrix();
                    tex7.Write(writer);
                    break;
                case VertexDescription.ElementPresence.Index16:
                    tex7 = UvGroups[7].Uvs[reader.ReadUInt16()].ToMatrix();
                    tex7.Write(writer);
                    break;
            }
        }

        private class VertexDescription
        {
            public bool ByteDequant, NormalIndex3; // Not implemented

            public bool PosMatrix, TexMatrix0, TexMatrix1, TexMatrix2, TexMatrix3, 
                TexMatrix4, TexMatrix5, TexMatrix6, TexMatrix7;

            public ElementPresence Pos, Norm, Col0, Col1, Tex0, Tex1, Tex2, Tex3,
                Tex4, Tex5, Tex6, Tex7;

            public int PosFields, NormFields, Col0Fields, Col1Fields, Tex0Fields, Tex1Fields,
                Tex2Fields, Tex3Fields, Tex4Fields, Tex5Fields, Tex6Fields, Tex7Fields;

            public ElementFormat PosFormat, NormFormat, Col0Format, Col1Format, Tex0Format, Tex1Format,
                Tex2Format, Tex3Format, Tex4Format, Tex5Format, Tex6Format, Tex7Format;

            public int PosScale, Tex0Scale, Tex1Scale, Tex2Scale, Tex3Scale, Tex4Scale, Tex5Scale,
                Tex6Scale, Tex7Scale;

            public int ColourCount, NormalCount, TextureCount;

            public VertexDescription(byte[] code)
            {
                int location;
                byte current;

                location = 0;

                while (location < code.Length)
                {
                    switch (code[location++])
                    {
                        case 0x8:
                            switch (code[location++] & 0xf0)
	                        {
                                case 0x50:
                                    location++;
                                    current = code[location++];
                                    Col1 = (ElementPresence)(current << 1 & 0x2);
                                    current = code[location++];
                                    Col1 |= (ElementPresence)(current >> 7 & 0x1);
                                    Col0 = (ElementPresence)(current >> 5 & 0x3);
                                    Norm = (ElementPresence)(current >> 3 & 0x3);
                                    Pos = (ElementPresence)(current >> 1 & 0x3);
                                    TexMatrix7 = (current & 0x1) > 0;
                                    current = code[location++];
                                    TexMatrix6 = (current & 0x80) > 0;
                                    TexMatrix5 = (current & 0x40) > 0;
                                    TexMatrix4 = (current & 0x20) > 0;
                                    TexMatrix3 = (current & 0x10) > 0;
                                    TexMatrix2 = (current & 0x8) > 0;
                                    TexMatrix1 = (current & 0x4) > 0;
                                    TexMatrix0 = (current & 0x2) > 0;
                                    PosMatrix = (current & 0x1) > 0;
                                    break;
                                case 0x60:
                                    location += 2;
                                    current = code[location++];
                                    Tex7 = (ElementPresence)(current >> 6 & 0x3);
                                    Tex6 = (ElementPresence)(current >> 4 & 0x3);
                                    Tex5 = (ElementPresence)(current >> 2 & 0x3);
                                    Tex4 = (ElementPresence)(current >> 0 & 0x3);
                                    current = code[location++];
                                    Tex3 = (ElementPresence)(current >> 6 & 0x3);
                                    Tex2 = (ElementPresence)(current >> 4 & 0x3);
                                    Tex1 = (ElementPresence)(current >> 2 & 0x3);
                                    Tex0 = (ElementPresence)(current >> 0 & 0x3);
                                    break;
                                case 0x70:
                                    current = code[location++];
                                    NormalIndex3 = (current & 0x80) > 0;
                                    ByteDequant = (current & 0x40) > 0;
                                    Tex0Scale = current >> 1 & 0x1f;
                                    Tex0Format = (ElementFormat)(current << 2 & 0x4);
                                    current = code[location++];
                                    Tex0Format |= (ElementFormat)(current >> 6 & 0x3);
                                    Tex0Fields = ((current & 0x20) > 0) ? 2 : 1; // not implemented
                                    Col1Format = (ElementFormat)(current >> 2 & 0x7);
                                    Col1Fields = ((current & 0x2) > 0) ? 4 : 3; // not implemented
                                    Col0Format = (ElementFormat)(current << 2 & 0x4);
                                    current = code[location++];
                                    Col0Format |= (ElementFormat)(current >> 6 & 0x3);
                                    Col0Fields = ((current & 0x20) > 0) ? 4 : 3; // not implemented
                                    NormFormat = (ElementFormat)(current >> 2 & 0x7);
                                    NormFields = ((current & 0x2) > 0) ? 3 : 1; // not implemented
                                    PosScale = (current << 4 & 0x10) + ((current = code[location++]) >> 4 & 0xf);
                                    PosFormat = (ElementFormat)(current >> 1 & 0x7);
                                    PosFields = ((current & 0x1) > 0) ? 3 : 2; // not implemented                                    
                                    break;
                                case 0x80:
                                    current = code[location++];
                                    Tex4Format = (ElementFormat)(current >> 4 & 0x3);
                                    Tex4Fields = ((current & 0x8) > 0) ? 2 : 1; // not implemented
                                    Tex3Scale = (current << 2 & 0x1c) + ((current = code[location++]) >> 6 & 0x3);
                                    Tex3Format = (ElementFormat)(current >> 3 & 0x3);
                                    Tex3Fields = ((current & 0x4) > 0) ? 2 : 1; // not implemented
                                    Tex2Scale = (current << 3 & 0x18) + ((current = code[location++]) >> 5 & 0x7);
                                    Tex2Format = (ElementFormat)(current >> 2 & 0x3);
                                    Tex2Fields = ((current & 0x2) > 0) ? 2 : 1; // not implemented
                                    Tex1Scale = (current << 4 & 0x10) + ((current = code[location++]) >> 4 & 0xf);
                                    Tex1Format = (ElementFormat)(current >> 1 & 0x3);
                                    Tex1Fields = ((current & 0x1) > 0) ? 2 : 1; // not implemented
                                    break;
                                case 0x90:
                                    current = code[location++];
                                    Tex7Scale = current >> 3 & 0x1f;
                                    Tex7Format = (ElementFormat)(current >> 0 & 0x3);
                                    current = code[location++];
                                    Tex7Fields = ((current & 0x80) > 0) ? 2 : 1; // not implemented
                                    Tex6Scale = current >> 2 & 0x1f;
                                    Tex6Format = (ElementFormat)(current << 1 & 0x2);
                                    current = code[location++];
                                    Tex6Format |= (ElementFormat)(current >> 7 & 0x1);
                                    Tex6Fields = ((current & 0x40) > 0) ? 2 : 1; // not implemented
                                    Tex5Scale = current >> 1 & 0x1f;
                                    current = code[location++];
                                    Tex5Format |= (ElementFormat)(current >> 6 & 0x3);
                                    Tex5Fields = ((current & 0x20) > 0) ? 2 : 1; // not implemented
                                    Tex4Scale = current >> 0 & 0x1f;
                                    break;
                                default:
                                    location += 4;
                                    break;
	                        }
                            break;
                        case 0x10:
                            location++;
                            current = code[location++];

                            switch (BitConverter.ToInt16(code, location))
	                        {
                                case 0x0810:
                                    location += 5;
                                    current = code[location++];

                                    TextureCount = current >> 4 & 0xf;
                                    NormalCount = current >> 2 & 0x3;
                                    ColourCount = current >> 0 & 0x3;
                                    break;
                                default:
                                    location += 2 + 4 * ((current & 0xf) + 1);
                                    break;
	                        }
                            break;
                        case 0x0:
                            break;
                        default:
                            return;
                    }
                }
            }

            public enum ElementPresence
            {
                NotPresent = 0,
                Direct = 1, 
                Index8 = 2,
                Index16 = 3,
            }

            public enum ElementFormat
            {
                UByte = 0,
                Byte= 1,
                UShort = 2,
                Short = 3,
                Float = 4,
                RGB565 = 0,
                RGB888 = 1,
                RGB888x = 3,
                ARGB4444 = 4,
                ARGB6666 = 5,
                ARGB8888 = 6
            }

            public bool HasMatrix { get { return PosMatrix || TexMatrix0 || TexMatrix1 || TexMatrix2 || TexMatrix3 || TexMatrix4 || TexMatrix5 || TexMatrix6 || TexMatrix7; } }

            public PolygonVertexDataFormat GetFormat()
            {
                PolygonVertexDataFormat format;

                format = PolygonVertexDataFormat.None;

                if (Pos != ElementPresence.NotPresent)
                    format |= PolygonVertexDataFormat.Position;
                if (Norm != ElementPresence.NotPresent)
                    format |= PolygonVertexDataFormat.Normal;
                format |= (PolygonVertexDataFormat)(ColourCount << (int)PolygonVertexDataFormat.ColourShift);
                format |= (PolygonVertexDataFormat)(TextureCount << (int)PolygonVertexDataFormat.TextureShift);
                if (HasMatrix)
                    format |= PolygonVertexDataFormat.Transform;

                return format;
            }
        }
    }

    public class Mdl0PolygonSection
    {
        public long Address { get; set; }

        public int Size { get; set; }
        public int Size2 { get; set; }
        public int Offset { get; set; }

        public Mdl0PolygonSection(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            Size = reader.ReadInt32();
            Size2 = reader.ReadInt32();
            Offset = reader.ReadInt32();
        }

        public void Write(EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            writer.Write(Size);
            writer.Write(Size2);
            writer.Write(Offset);            
        }
    }

    public class Mdl0PolygonBoneTable
    {
        public long Address { get; set; }
        public int Count { get; set; }
        public short[] Bones { get; set; }

        public Mdl0PolygonBoneTable(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            Count = reader.ReadInt32();
            Bones = reader.ReadInt16s(Count);
        }

        public void Write(EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            Count = Bones.Length;

            writer.Write(Count);
            writer.Write(Bones, 0, Count);
        }
    }
}
