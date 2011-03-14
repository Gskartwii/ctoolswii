using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace Chadsoft.CTools.Brres
{
    public class Tex0Section : BrresSection
    {
        public const int Tag = 0x54455830;

        public byte[] Data { get; set; }
        public BrresCommonSectionHeader Header { get; private set; }
        public Tex0Header Tex0Header { get; private set; }
        public Tex0Image Image { get; set; }
        
        public Tex0Section(EndianBinaryReader reader, BrresCommonSectionHeader header)
        {
            Address = reader.BaseStream.Position - 0x10;

            Header = header;
            Tex0Header = new Tex0Header(reader);

            BrresFile.SafeSeek(reader, Address + Tex0Header.DataStart, Header.Length - Tex0Header.DataStart);

            Data = reader.ReadBytes(Header.Length - Tex0Header.DataStart);
            Name = BrresFile.ReadBrresString(reader, Address + Tex0Header.NameOffset);
        }

        public override void Write(EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            Header.Write(writer);
            Tex0Header.Write(writer);
            writer.WritePadding(0x20, 0);
            Tex0Header.DataStart = (int)(writer.BaseStream.Position - Address);
            writer.Write(Data, 0, Data.Length);
        }

        public override void WriteNames(EndianBinaryWriter writer, Dictionary<string, long> names)
        {
            Tex0Header.NameOffset = (int)(BrresFile.WriteString(writer, names, Name) - Address);
        }

        public Tex0Image GenerateImage()
        {
            if (Image == null)
                Image = new Tex0Image(this);

            return Image;
        }
    }

    public class Tex0Header
    {
        public int DataStart { get; set; }
        public int NameOffset { get; set; }
        public int Unknown8 { get; set; }
        public ushort Width { get; set; }
        public ushort Height { get; set; }
        public int Format { get; set; }
        public int MipMapLevels { get; set; }

        public Tex0Header(EndianBinaryReader reader)
        {
            DataStart = reader.ReadInt32();
            NameOffset = reader.ReadInt32();
            Unknown8 = reader.ReadInt32();
            Width = reader.ReadUInt16();
            Height = reader.ReadUInt16();
            Format = reader.ReadInt32();
            MipMapLevels = reader.ReadInt32();
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(DataStart);
            writer.Write(NameOffset);
            writer.Write(Unknown8);
            writer.Write(Width);
            writer.Write(Height);
            writer.Write(Format);
            writer.Write(MipMapLevels);  
        }
    }
}
