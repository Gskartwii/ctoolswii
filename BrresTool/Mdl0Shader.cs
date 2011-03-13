using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Chadsoft.CTools.Brres
{
    public class Mdl0Shader
    {
        public long Address { get; set; }

        public int Length { get; set; }
        public int Mdl0Offset { get; set; }
        public int Index { get; set; }
        public byte LayerCount { get; set; }
        public byte UnknownD { get; set; }
        public byte UnknownE { get; set; }
        public byte UnknownF { get; set; }
        public byte[] LayerInformation { get; private set; }
        public int Unknown18 { get; set; }
        public int Unknown1C { get; set; }
        public byte[] Shader { get; set; }

        public Mdl0Shader(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            Length = reader.ReadInt32();
            Mdl0Offset = reader.ReadInt32();
            Index = reader.ReadInt32();
            LayerCount = reader.ReadByte();
            UnknownD = reader.ReadByte();
            UnknownE = reader.ReadByte();
            UnknownF = reader.ReadByte();
            LayerInformation = reader.ReadBytes(8);
            Unknown18 = reader.ReadInt32();
            Unknown1C = reader.ReadInt32();
            Shader = reader.ReadBytes(Length - 0x20);
        }

        public void Write(EndianBinaryWriter writer, long mdl0Address)
        {
            Address = writer.BaseStream.Position;

            Mdl0Offset = (int)(mdl0Address - Address);

            writer.Write(Length);
            writer.Write(Mdl0Offset);
            writer.Write(Index);
            writer.Write(LayerCount);
            writer.Write(UnknownD);
            writer.Write(UnknownE);
            writer.Write(UnknownF);
            writer.Write(LayerInformation, 0, 8);
            writer.Write(Unknown18);
            writer.Write(Unknown1C);
            writer.Write(Shader, 0, Shader.Length);

            writer.WritePadding(0x20, 0);

            Length = (int)(writer.BaseStream.Position - Address);
        }
    }
}
