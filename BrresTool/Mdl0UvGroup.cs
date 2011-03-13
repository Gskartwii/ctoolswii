using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using Chadsoft.CTools.Models;

namespace Chadsoft.CTools.Brres
{
    public class Mdl0UvGroup
    {
        
        public long Address { get; set; }

        public int Length { get; set; }
        public int Mdl0Offset { get; set; }
        public int DataOffset { get; set; }
        public int NameOffset { get; set; }
        public int Index { get; set; }
        public int Unknown14 { get; set; } // 0x00000001
        public int Type { get; set; }
        public byte Divisor { get; set; }
        public byte Stride { get; set; }
        public short UvCount { get; set; }
        public Matrix2x1 Minimum { get; private set; }
        public Matrix2x1 Maximum { get; private set; }

        public Collection<Mdl0Uv> Uvs { get; private set; }
        public string Name { get; set; }

        public Mdl0UvGroup(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            Length = reader.ReadInt32();
            Mdl0Offset = reader.ReadInt32();
            DataOffset = reader.ReadInt32();
            NameOffset = reader.ReadInt32();
            Index = reader.ReadInt32();
            Unknown14 = reader.ReadInt32();
            Type = reader.ReadInt32();
            Divisor = reader.ReadByte();
            Stride = reader.ReadByte();
            UvCount = reader.ReadInt16();
            Minimum = new Matrix2x1(reader);
            Maximum = new Matrix2x1(reader);

            BrresFile.SafeSeek(reader, Address + DataOffset, Stride * UvCount);

            Uvs = new Collection<Mdl0Uv>();

            for (int i = 0; i < UvCount; i++)
                Uvs.Add(new Mdl0Uv(reader, this));

            Name = BrresFile.ReadBrresString(reader, Address + NameOffset);
        }

        public void Write(EndianBinaryWriter writer, long mdl0Address)
        {
            Address = writer.BaseStream.Position;

            UvCount = (short)Uvs.Count;
            Mdl0Offset = (int)(mdl0Address - Address);

            writer.Write(Length);
            writer.Write(Mdl0Offset);
            writer.Write(DataOffset);
            writer.Write(NameOffset);
            writer.Write(Index);
            writer.Write(Unknown14);
            writer.Write(Type);
            writer.Write(Divisor);
            writer.Write(Stride);
            writer.Write(UvCount);
            Minimum.Write(writer);
            Maximum.Write(writer);

            writer.WritePadding(0x20, 0);

            DataOffset = (int)(writer.BaseStream.Position - Address);

            for (int i = 0; i < Uvs.Count; i++)
                Uvs[i].Write(writer, this);

            writer.WritePadding(0x20, 0);

            Length = (int)(writer.BaseStream.Position - Address);
        }

        public void WriteNames(EndianBinaryWriter writer, Dictionary<string, long> names)
        {
            NameOffset = (int)(BrresFile.WriteString(writer, names, Name) - Address);
        }
    }

    public class Mdl0Uv
    {
        public float U { get; set; }
        public float V { get; set; }

        public Mdl0Uv()
        {

        }

        public Mdl0Uv(EndianBinaryReader reader, Mdl0UvGroup group)
        {
            switch (group.Type)
            {
                case 0: // byte2
                    U = reader.ReadByte() / (float)Math.Pow(2, group.Divisor);
                    V = reader.ReadByte() / (float)Math.Pow(2, group.Divisor);
                    break;
                case 1: // sbyte2
                    U = reader.ReadSByte() / (float)Math.Pow(2, group.Divisor);
                    V = reader.ReadSByte() / (float)Math.Pow(2, group.Divisor);
                    break;
                case 2: // ushort2
                    U = reader.ReadUInt16() / (float)Math.Pow(2, group.Divisor);
                    V = reader.ReadUInt16() / (float)Math.Pow(2, group.Divisor);
                    break;
                case 3: // short2
                    U = reader.ReadInt16() / (float)Math.Pow(2, group.Divisor);
                    V = reader.ReadInt16() / (float)Math.Pow(2, group.Divisor);
                    break;
                case 4: // float2
                    U = reader.ReadSingle();
                    V = reader.ReadSingle();
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        public Mdl0Uv(EndianBinaryReader reader, int type, int divisor)
        {
            switch (type)
            {
                case 0: // byte2
                    U = reader.ReadByte() / (float)Math.Pow(2, divisor);
                    V = reader.ReadByte() / (float)Math.Pow(2, divisor);
                    break;
                case 1: // sbyte2
                    U = reader.ReadSByte() / (float)Math.Pow(2, divisor);
                    V = reader.ReadSByte() / (float)Math.Pow(2, divisor);
                    break;
                case 2: // ushort2
                    U = reader.ReadUInt16() / (float)Math.Pow(2, divisor);
                    V = reader.ReadUInt16() / (float)Math.Pow(2, divisor);
                    break;
                case 3: // short2
                    U = reader.ReadInt16() / (float)Math.Pow(2, divisor);
                    V = reader.ReadInt16() / (float)Math.Pow(2, divisor);
                    break;
                case 4: // float2
                    U = reader.ReadSingle();
                    V = reader.ReadSingle();
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        public void Write(EndianBinaryWriter writer, Mdl0UvGroup group)
        {
            switch (group.Type)
            {
                case 0: // byte2
                    writer.Write((byte)(U * Math.Pow(2, group.Divisor)));
                    writer.Write((byte)(V * Math.Pow(2, group.Divisor)));
                    break;
                case 1: // sbyte2
                    writer.Write((sbyte)(U * Math.Pow(2, group.Divisor)));
                    writer.Write((sbyte)(V * Math.Pow(2, group.Divisor)));
                    break;
                case 2: // ushort2
                    writer.Write((ushort)(U * Math.Pow(2, group.Divisor)));
                    writer.Write((ushort)(V * Math.Pow(2, group.Divisor)));
                    break;
                case 3: // short2
                    writer.Write((short)(U * Math.Pow(2, group.Divisor)));
                    writer.Write((short)(V * Math.Pow(2, group.Divisor)));
                    break;
                case 4: // float2
                    writer.Write((float)(U * Math.Pow(2, group.Divisor)));
                    writer.Write((float)(V * Math.Pow(2, group.Divisor)));
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        public Matrix2x1 ToMatrix()
        {
            return new Matrix2x1(U, V);
        }
    }
}
