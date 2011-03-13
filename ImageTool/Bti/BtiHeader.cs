using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Chadsoft.CTools.Image.Bti
{
    public class BtiHeader
    {
        public byte Format { get; set; }
        public byte Unknown1 { get; set; }
        public short Width { get; set; }
        public short Height { get; set; }
        public short Unknown6 { get; set; }
        public int Unknown8 { get; set; }
        public int UnknownC { get; set; }
        public int Unknown10 { get; set; }
        public int Unknown14 { get; set; }
        public int Unknown18 { get; set; }
        public int ImageDataStart { get; set; }

        public BtiHeader(EndianBinaryReader reader)
        {
            if (reader.BaseStream.Length < 0x20)
                throw new InvalidDataException();

            Format = reader.ReadByte();
            Unknown1 = reader.ReadByte();
            Width = reader.ReadInt16();
            Height = reader.ReadInt16();

            Unknown6 = reader.ReadInt16();
            Unknown8 = reader.ReadInt32();
            UnknownC = reader.ReadInt32();
            Unknown10 = reader.ReadInt32();
            Unknown14 = reader.ReadInt32();
            Unknown18 = reader.ReadInt32();
            ImageDataStart = reader.ReadInt32();
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(Format);
            writer.Write(Unknown1);
            writer.Write(Width);
            writer.Write(Height);
            writer.Write(Unknown6);
            writer.Write(Unknown8);
            writer.Write(UnknownC);
            writer.Write(Unknown10);
            writer.Write(Unknown14);
            writer.Write(Unknown18);
            writer.Write(ImageDataStart);
        }
    }
}
