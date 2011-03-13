using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace Chadsoft.CTools.Brres
{
    public class Mdl0ColourGroup
    {
        public long Address { get; set; }

        public int Length { get; set; }
        public int Mdl0Offset { get; set; }
        public int DataOffset { get; set; }
        public int NameOffset { get; set; }
        public int Index { get; set; }
        public int Unknown14 { get; set; } // 0x00000001
        public int Type { get; set; }
        public byte Stride { get; set; }
        public byte Unknown1D { get; set; }
        public short ColourCount { get; set; }

        public Collection<Mdl0Colour> Colours { get; private set; }
        public string Name { get; set; }

        public Mdl0ColourGroup(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            Length = reader.ReadInt32();
            Mdl0Offset = reader.ReadInt32();
            DataOffset = reader.ReadInt32();
            NameOffset = reader.ReadInt32();
            Index = reader.ReadInt32();
            Unknown14 = reader.ReadInt32();
            Type = reader.ReadInt32();
            Stride = reader.ReadByte();
            Unknown1D = reader.ReadByte();
            ColourCount = reader.ReadInt16();

            BrresFile.SafeSeek(reader, Address + DataOffset, Stride * ColourCount);

            Colours = new Collection<Mdl0Colour>();

            for (int i = 0; i < ColourCount; i++)
                Colours.Add(new Mdl0Colour(reader, this));

            Name = BrresFile.ReadBrresString(reader, Address + NameOffset);
        }

        public void Write(EndianBinaryWriter writer, long mdl0Address)
        {
            Address = writer.BaseStream.Position;

            ColourCount = (short)Colours.Count;
            Mdl0Offset = (int)(mdl0Address - Address);

            writer.Write(Length);
            writer.Write(Mdl0Offset);
            writer.Write(DataOffset);
            writer.Write(NameOffset);
            writer.Write(Index);
            writer.Write(Unknown14);
            writer.Write(Type);
            writer.Write(Stride);
            writer.Write(Unknown1D);
            writer.Write(ColourCount);
            
            for (int i = 0; i < Colours.Count; i++)
                Colours[i].Write(writer, this);

            writer.WritePadding(0x20, 0);

            Length = (int)(writer.BaseStream.Position - Address);
        }

        public void WriteNames(EndianBinaryWriter writer, Dictionary<string, long> names)
        {
            NameOffset = (int)(BrresFile.WriteString(writer, names, Name) - Address);
        }
    }

    public class Mdl0Colour
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public Mdl0Colour()
        {

        }

        public Mdl0Colour(EndianBinaryReader reader, Mdl0ColourGroup group)
        {
            byte temp;

            switch (group.Type)
            {
                case 0: // rgb565
                    temp = reader.ReadByte();
                    R = (byte)(temp & 0xF8 | temp >> 5);
                    G = (byte)(temp << 5 & 0xE0 | temp >> 1 & 0x03 | (temp = reader.ReadByte()) >> 3 & 0x1C);
                    B = (byte)(temp << 3 & 0xF8 | temp >> 2 & 0x07);
                    A = 255;
                    break;
                case 1: // rgb8
                    R = reader.ReadByte();
                    G = reader.ReadByte();
                    B = reader.ReadByte();
                    A = 255;
                    break;
                case 2: // rgbx8
                    R = reader.ReadByte();
                    G = reader.ReadByte();
                    B = reader.ReadByte();
                    A = 255;
                    // TBD: Investigate
                    reader.ReadByte();
                    break;
                case 3: // rgba4
                    temp = reader.ReadByte();
                    R = (byte)(temp & 0xF0 | temp >> 4);
                    G = (byte)(temp << 4 & 0xF0 | temp & 0x0F);
                    temp = reader.ReadByte();
                    B = (byte)(temp & 0xF0 | temp >> 4);
                    A = (byte)(temp << 4 & 0xF0 | temp & 0x0F);
                    break;
                case 4: // rgba6
                    temp = reader.ReadByte();
                    R = (byte)(temp & 0xFC | temp >> 6);
                    G = (byte)(temp << 6 & 0xC0 | temp & 0x03 | (temp = reader.ReadByte()) >> 2 & 0x3C);
                    B = (byte)(temp << 4 & 0xF0 | temp >> 2 & 0x03 | (temp = reader.ReadByte()) >> 4 & 0x0C);
                    A = (byte)(temp << 2 & 0xFC | temp >> 4 & 0x3);
                    break;
                case 5: // rgba8
                    R = reader.ReadByte();
                    G = reader.ReadByte();
                    B = reader.ReadByte();
                    A = reader.ReadByte();
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        public Mdl0Colour(EndianBinaryReader reader, int type)
        {
            byte temp;

            switch (type)
            {
                case 0: // rgb565
                    temp = reader.ReadByte();
                    R = (byte)(temp & 0xF8 | temp >> 5);
                    G = (byte)(temp << 5 & 0xE0 | temp >> 1 & 0x03 | (temp = reader.ReadByte()) >> 3 & 0x1C);
                    B = (byte)(temp << 3 & 0xF8 | temp >> 2 & 0x07);
                    A = 255;
                    break;
                case 1: // rgb8
                    R = reader.ReadByte();
                    G = reader.ReadByte();
                    B = reader.ReadByte();
                    A = 255;
                    break;
                case 2: // rgbx8
                    R = reader.ReadByte();
                    G = reader.ReadByte();
                    B = reader.ReadByte();
                    A = 255;
                    // TBD: Investigate
                    reader.ReadByte();
                    break;
                case 3: // rgba4
                    temp = reader.ReadByte();
                    R = (byte)(temp & 0xF0 | temp >> 4);
                    G = (byte)(temp << 4 & 0xF0 | temp & 0x0F);
                    temp = reader.ReadByte();
                    B = (byte)(temp & 0xF0 | temp >> 4);
                    A = (byte)(temp << 4 & 0xF0 | temp & 0x0F);
                    break;
                case 4: // rgba6
                    temp = reader.ReadByte();
                    R = (byte)(temp & 0xFC | temp >> 6);
                    G = (byte)(temp << 6 & 0xC0 | temp & 0x03 | (temp = reader.ReadByte()) >> 2 & 0x3C);
                    B = (byte)(temp << 4 & 0xF0 | temp >> 2 & 0x03 | (temp = reader.ReadByte()) >> 4 & 0x0C);
                    A = (byte)(temp << 2 & 0xFC | temp >> 4 & 0x3);
                    break;
                case 5: // rgba8
                    R = reader.ReadByte();
                    G = reader.ReadByte();
                    B = reader.ReadByte();
                    A = reader.ReadByte();
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        public void Write(EndianBinaryWriter writer, Mdl0ColourGroup group)
        {
            switch (group.Type)
            {
                case 0: // rgb565
                    writer.Write((ushort)(R << 8 & 0xF800 | G << 3 & 0x07E0 | B >> 3 & 0x001F));
                    break;
                case 1: // rgb8
                    writer.Write(R);
                    writer.Write(G);
                    writer.Write(B);
                    break;
                case 2: // rgbx8
                    writer.Write(R);
                    writer.Write(G);
                    writer.Write(B);
                    writer.Write(0);
                    break;
                case 3: // rgba4
                    writer.Write((ushort)(R << 8 & 0xF000 | G << 4 & 0x0F00 | B & 0x00F0 | A >> 4 & 0x000F));
                    break;
                case 4: // rgba6
                    writer.Write(R & 0xFC | G >> 6 & 0x03);
                    writer.Write(G << 2 & 0xF0 | B >> 4 & 0x0F);
                    writer.Write(B << 4 & 0xC0 | A >> 2 & 0x3F);
                    break;
                case 5: // rgba8
                    writer.Write(R);
                    writer.Write(G);
                    writer.Write(B);
                    writer.Write(A);                    
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        public int ToArgb()
        {
            return A << 24 | R << 16 | G << 8 | B;
        }
    }
}
