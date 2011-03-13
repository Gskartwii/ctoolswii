using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Chadsoft.CTools.Brres
{
    public class BrresHeader
    {
        public const int bresTag = 0x62726573;

        public long Address { get; set; }
        public int Tag { get; set; }
        public short Endianness { get; set; }
        public short Unknown6 { get; set; }
        public int FileSize { get; set; }
        public short RootOffset { get; set; }
        public short Sections { get; set; }

        public BrresHeader(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            if (reader.BaseStream.Length - Address < 0x10)
                throw new InvalidDataException();

            Tag = reader.ReadInt32();

            if (Tag != bresTag)
                throw new InvalidDataException();

            Endianness = reader.ReadInt16();

            if ((int)Endianness == 0xFFFE)
                if (reader.Endianness == System.IO.Endianness.LittleEndian)
                    reader.Endianness = System.IO.Endianness.BigEndian;
                else
                    reader.Endianness = System.IO.Endianness.LittleEndian;

            Unknown6 = reader.ReadInt16();
            FileSize = reader.ReadInt32();
            RootOffset = reader.ReadInt16();
            Sections = reader.ReadInt16();
            
            if (reader.BaseStream.Length < FileSize)
                throw new InvalidDataException();
        }

        public void Write(EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            writer.Write(Tag);
            writer.Write(Endianness);
            writer.Write(Unknown6);
            writer.Write(FileSize);
            writer.Write(RootOffset);
            writer.Write(Sections);
        }
    }
}
