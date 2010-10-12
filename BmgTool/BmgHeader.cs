// CTools bmg tool - Text editing service for CTools
// Copyright (C) 2010 Chadderz

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.IO;

namespace Chadsoft.CTools.Bmg
{
    public class BmgHeader
    {
        public const int MESGTag = 0x4D455347;
        public const int bmg1Tag = 0x626D6731;

        public int Tag { get; set; }
        public int bmgTag { get; set; }
        public int FileSize { get; set; }
        public int Sections { get; set; }
        public int Unknown { get; set; }
        public int[] Padding { get; set; }

        public BmgHeader(EndianBinaryReader reader)
        {
            if (reader.BaseStream.Length < 0x20)
                throw new InvalidDataException();

            Tag = reader.ReadInt32();
            bmgTag = reader.ReadInt32();

            if (Tag != MESGTag || bmgTag != bmg1Tag)
                throw new InvalidDataException();

            FileSize = reader.ReadInt32();
            Sections = reader.ReadInt32();
            Unknown = reader.ReadInt32();
            Padding = reader.ReadInt32s(3);


            if (reader.BaseStream.Length < FileSize)
                throw new InvalidDataException();
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(Tag);
            writer.Write(bmgTag);
            writer.Write(FileSize);
            writer.Write(Sections);
            writer.Write(Unknown);
            writer.Write(Padding, 0, Padding.Length);
        }
    }

    public class Inf1
    {
        public const int INF1Tag = 0x494E4631;

        public int Tag { get; set; }
        public int SectionSize { get; set; }
        public short Messages { get; set; }
        public short Stride { get; set; }
        public int Padding { get; set; }
        public int[] Entries { get; set; }

        public Inf1(EndianBinaryReader reader)
        {
            if (reader.BaseStream.Length - reader.BaseStream.Position < 0x10)
                throw new InvalidDataException();

            Tag = reader.ReadInt32();

            if (Tag != INF1Tag)
                throw new InvalidDataException();

            SectionSize = reader.ReadInt32();
            Messages = reader.ReadInt16();
            Stride = reader.ReadInt16();
            Padding = reader.ReadInt32();

            if (reader.BaseStream.Length - reader.BaseStream.Position < SectionSize - 0x10
                || Messages * Stride + 0x10 > SectionSize)
                throw new InvalidDataException();

            Entries = reader.ReadInt32s((SectionSize - 0x10) >> 2);
        }

        public int[] this[int index]
        {
            get
            {
                int[] temp;

                temp = new int[Stride >> 2];
                Array.Copy(Entries, index * temp.Length, temp, 0, temp.Length);

                return temp;
            }
            set 
            { 
                Array.Copy(value, 0, Entries, index * (Stride >> 2), (Stride >> 2));
            }
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(Tag);
            writer.Write(SectionSize);
            writer.Write(Messages);
            writer.Write(Stride);
            writer.Write(Padding);
            writer.Write(Entries, 0, Entries.Length);
        }
    }

    public class Dat1
    {
        public const int DAT1Tag = 0x44415431;

        public int Tag { get; set; }
        public int SectionSize { get; set; }
        public byte[] Data { get; set; }

        public Dat1(EndianBinaryReader reader)
        {
            if (reader.BaseStream.Length - reader.BaseStream.Position < 0x8)
                throw new InvalidDataException();

            Tag = reader.ReadInt32();

            if (Tag != DAT1Tag)
                throw new InvalidDataException();

            SectionSize = reader.ReadInt32();

            if (reader.BaseStream.Length - reader.BaseStream.Position < SectionSize - 0x8)
                throw new InvalidDataException();

            Data = reader.ReadBytes(SectionSize - 8);
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(Tag);
            writer.Write(SectionSize);
            writer.Write(Data, 0, Data.Length);
        }
    }

    public class Mid1
    {
        public const int MID1Tag = 0x4D494431;

        public int Tag { get; set; }
        public int SectionSize { get; set; }
        public short Messages { get; set; }
        public short[] Padding { get; set; }
        public int[] Entries { get; set; }

        public Mid1(EndianBinaryReader reader)
        {
            if (reader.BaseStream.Length - reader.BaseStream.Position < 0x10)
                throw new InvalidDataException();

            Tag = reader.ReadInt32();

            if (Tag != MID1Tag)
                throw new InvalidDataException();

            SectionSize = reader.ReadInt32();
            Messages = reader.ReadInt16();
            Padding = reader.ReadInt16s(3);

            if (reader.BaseStream.Length - reader.BaseStream.Position < SectionSize - 0x10
                || Messages * 4 + 0x10 > SectionSize)
                throw new InvalidDataException();

            Entries = reader.ReadInt32s((SectionSize - 0x10) >> 2);
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(Tag);
            writer.Write(SectionSize);
            writer.Write(Messages);
            writer.Write(Padding, 0, Padding.Length);
            writer.Write(Entries, 0 ,Entries.Length);
        }
    }
}
