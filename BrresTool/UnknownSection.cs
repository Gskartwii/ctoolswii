using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Chadsoft.CTools.Brres
{
    public class UnknownSection : BrresSection 
    {
        public byte[] Data { get; set; }
        public BrresCommonSectionHeader Header { get; private set; }

        public UnknownSection(EndianBinaryReader reader, BrresCommonSectionHeader header)
        {
            Address = reader.BaseStream.Position - 0x10;

            Header = header;

            Data = reader.ReadBytes(Header.Length - 0x10);
        }

        public override void Write(EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            Header.Write(writer);
            writer.Write(Data, 0, Data.Length);
        }

        public override void WriteNames(EndianBinaryWriter writer, Dictionary<string, long> names)
        {
            throw new NotImplementedException();
        }
    }
}
