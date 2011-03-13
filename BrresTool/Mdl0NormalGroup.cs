using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using Chadsoft.CTools.Models;

namespace Chadsoft.CTools.Brres
{
    public class Mdl0NormalGroup
    {
        public long Address { get; set; }

        public int Length { get; set; }
        public int Mdl0Offset { get; set; }
        public int DataOffset { get; set; }
        public int NameOffset { get; set; }
        public int Index { get; set; }
        public int Unknown14 { get; set; } // 0x00000000
        public int Type { get; set; }
        public byte Divisor { get; set; }
        public byte Stride { get; set; }
        public short NormalCount { get; set; }

        public Collection<Mdl0Normal> Normals { get; private set; }
        public string Name { get; set; }

        public Mdl0NormalGroup(EndianBinaryReader reader)
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
            NormalCount = reader.ReadInt16();

            BrresFile.SafeSeek(reader, Address + DataOffset, Stride * NormalCount);

            Normals = new Collection<Mdl0Normal>();

            for (int i = 0; i < NormalCount; i++)
                Normals.Add(new Mdl0Normal(reader, this));

            Name = BrresFile.ReadBrresString(reader, Address + NameOffset);
        }

        public void Write(EndianBinaryWriter writer, long mdl0Address)
        {
            Address = writer.BaseStream.Position;

            Mdl0Offset = (int)(mdl0Address - Address);
            NormalCount = (short)Normals.Count;

            writer.Write(Length);
            writer.Write(Mdl0Offset);
            writer.Write(DataOffset);
            writer.Write(NameOffset);
            writer.Write(Index);
            writer.Write(Unknown14);
            writer.Write(Type);
            writer.Write(Divisor);
            writer.Write(Stride);
            writer.Write(NormalCount);
            
            for (int i = 0; i < Normals.Count; i++)
                Normals[i].Write(writer, this);

            writer.WritePadding(0x20, 0);

            Length = (int)(writer.BaseStream.Position - Address);
        }

        public void WriteNames(EndianBinaryWriter writer, Dictionary<string, long> names)
        {
            NameOffset = (int)(BrresFile.WriteString(writer, names, Name) - Address);
        }
    }

    public class Mdl0Normal
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Mdl0Normal()
        {

        }

        public Mdl0Normal(EndianBinaryReader reader, Mdl0NormalGroup group)
        {
            switch (group.Type)
            {
                case 0: // byte3
                    X = reader.ReadByte() / (float)Math.Pow(2, group.Divisor);
                    Y = reader.ReadByte() / (float)Math.Pow(2, group.Divisor);
                    Z = reader.ReadByte() / (float)Math.Pow(2, group.Divisor);
                    break;
                case 1: // sbyte3
                    X = reader.ReadSByte() / (float)Math.Pow(2, group.Divisor);
                    Y = reader.ReadSByte() / (float)Math.Pow(2, group.Divisor);
                    Z = reader.ReadSByte() / (float)Math.Pow(2, group.Divisor);
                    break;
                case 2: // ushort3
                    X = reader.ReadUInt16() / (float)Math.Pow(2, group.Divisor);
                    Y = reader.ReadUInt16() / (float)Math.Pow(2, group.Divisor);
                    Z = reader.ReadUInt16() / (float)Math.Pow(2, group.Divisor);
                    break;
                case 3: // short3
                    X = reader.ReadInt16() / (float)Math.Pow(2, group.Divisor);
                    Y = reader.ReadInt16() / (float)Math.Pow(2, group.Divisor);
                    Z = reader.ReadInt16() / (float)Math.Pow(2, group.Divisor);
                    break;
                case 4: // float3
                    X = reader.ReadSingle();
                    Y = reader.ReadSingle();
                    Z = reader.ReadSingle();
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        public Mdl0Normal(EndianBinaryReader reader, int type, int divisor)
        {
            switch (type)
            {
                case 0: // byte3
                    X = reader.ReadByte() / (float)Math.Pow(2, divisor);
                    Y = reader.ReadByte() / (float)Math.Pow(2, divisor);
                    Z = reader.ReadByte() / (float)Math.Pow(2, divisor);
                    break;
                case 1: // sbyte3
                    X = reader.ReadSByte() / (float)Math.Pow(2, divisor);
                    Y = reader.ReadSByte() / (float)Math.Pow(2, divisor);
                    Z = reader.ReadSByte() / (float)Math.Pow(2, divisor);
                    break;
                case 2: // ushort3
                    X = reader.ReadUInt16() / (float)Math.Pow(2, divisor);
                    Y = reader.ReadUInt16() / (float)Math.Pow(2, divisor);
                    Z = reader.ReadUInt16() / (float)Math.Pow(2, divisor);
                    break;
                case 3: // short3
                    X = reader.ReadInt16() / (float)Math.Pow(2, divisor);
                    Y = reader.ReadInt16() / (float)Math.Pow(2, divisor);
                    Z = reader.ReadInt16() / (float)Math.Pow(2, divisor);
                    break;
                case 4: // float3
                    X = reader.ReadSingle();
                    Y = reader.ReadSingle();
                    Z = reader.ReadSingle();
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        public void Write(EndianBinaryWriter writer, Mdl0NormalGroup group)
        {
            switch (group.Type)
            {
                case 0: // byte3
                    writer.Write((byte)(X * Math.Pow(2, group.Divisor)));
                    writer.Write((byte)(Y * Math.Pow(2, group.Divisor)));
                    writer.Write((byte)(Z * Math.Pow(2, group.Divisor)));
                    break;
                case 1: // sbyte3
                    writer.Write((sbyte)(X * Math.Pow(2, group.Divisor)));
                    writer.Write((sbyte)(Y * Math.Pow(2, group.Divisor)));
                    writer.Write((sbyte)(Z * Math.Pow(2, group.Divisor)));
                    break;
                case 2: // ushort3
                    writer.Write((ushort)(X * Math.Pow(2, group.Divisor)));
                    writer.Write((ushort)(Y * Math.Pow(2, group.Divisor)));
                    writer.Write((ushort)(Z * Math.Pow(2, group.Divisor)));
                    break;
                case 3: // short3
                    writer.Write((short)(X * Math.Pow(2, group.Divisor)));
                    writer.Write((short)(Y * Math.Pow(2, group.Divisor)));
                    writer.Write((short)(Z * Math.Pow(2, group.Divisor)));
                    break;
                case 4: // float3
                    writer.Write((float)(X * Math.Pow(2, group.Divisor)));
                    writer.Write((float)(Y * Math.Pow(2, group.Divisor)));
                    writer.Write((float)(Z * Math.Pow(2, group.Divisor)));
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        public Matrix3x1 ToMatrix()
        {
            return new Matrix3x1(X, Y, Z);
        }
    }
}
