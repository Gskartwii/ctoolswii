using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Chadsoft.CTools.Image.Tpl
{
    public class TplHeader
    {
        public const int TplTag = 0x0020AF30;

        public int Tag { get; set; }
        public int Unknown4 { get; set; }
        public int Unknown8 { get; set; }
        public int ImageHeaderStart { get; set; }
        public int PaletteHeaderStart { get; set; }

        public TplHeader(EndianBinaryReader reader)
        {
            if (reader.BaseStream.Length < 0x14)
                throw new InvalidDataException();

            Tag = reader.ReadInt32();

            if (Tag != TplTag)
                throw new InvalidDataException();

            Unknown4 = reader.ReadInt32();
            Unknown8 = reader.ReadInt32();
            ImageHeaderStart = reader.ReadInt32();
            PaletteHeaderStart = reader.ReadInt32();
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(Tag);
            writer.Write(Unknown4);
            writer.Write(Unknown8);
            writer.Write(ImageHeaderStart);
            writer.Write(PaletteHeaderStart);
        }
    }

    public class TplPaletteHeader
    {
        public short Entries { get; set; }
        public short Unknown2 { get; set; }
        public int Format { get; set; }
        public int PaletteStart { get; set; }

        public TplPaletteHeader (EndianBinaryReader reader)
	    {
            if (reader.BaseStream.Length < 0xC)
                throw new InvalidDataException();

            Entries = reader.ReadInt16();
            Unknown2 = reader.ReadInt16();
            Format = reader.ReadInt32();
            PaletteStart = reader.ReadInt32();
	    }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(Entries);
            writer.Write(Unknown2);
            writer.Write(Format);
            writer.Write(PaletteStart);
        }
    }

    public class TplImageHeader
    {
        public short Height { get; set; }
        public short Width { get; set; }
        public int Format { get; set; }
        public int ImageStart { get; set; }
        public int UnknownC { get; set; }
        public int Unknown10 { get; set; }
        public int Unknown14 { get; set; }
        public int Unknown18 { get; set; }

        public TplImageHeader(EndianBinaryReader reader)
        {
            if (reader.BaseStream.Length < 0x1C)
                throw new InvalidDataException();

            Height = reader.ReadInt16();
            Width = reader.ReadInt16();
            Format = reader.ReadInt32();
            ImageStart = reader.ReadInt32();
            UnknownC = reader.ReadInt32();
            Unknown10 = reader.ReadInt32();
            Unknown14 = reader.ReadInt32();
            Unknown18 = reader.ReadInt32();
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(Height);
            writer.Write(Width);
            writer.Write(Format);
            writer.Write(ImageStart);
            writer.Write(UnknownC);
            writer.Write(Unknown10);
            writer.Write(Unknown14);
            writer.Write(Unknown18);
        }
    }
}
