using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using Chadsoft.CTools.Models;

namespace Chadsoft.CTools.Brres
{
    public class Mdl0Bone
    {
        public long Address { get; set; }

        public int Length { get; set; }
        public int Mdl0Offset { get; set; }
        public int NameOffset { get; set; }
        public int Index { get; set; }
        public int NodeId { get; set; }
        public uint Flags { get; set; }
        public int Unknown18 { get; set; } // 0x00000000
        public int Unknown1C { get; set; } // 0x00000000
        public Matrix3x1 Scale { get; private set; }
        public Matrix3x1 Rotation { get; private set; }
        public Matrix3x1 Translation { get; private set; }
        public Matrix3x1 Minimum { get; private set; }
        public Matrix3x1 Maximum { get; private set; }
        public int ParentOffset { get; set; }
        public int FirstChildOffset { get; set; }
        public int NextOffset { get; set; }
        public int PreviousOffset { get; set; }
        public int Unknown6C { get; set; } // 0x00000000
        public Matrix4x3 Transformation { get; set; }
        public Matrix4x3 Inverse { get; set; }

        public Mdl0Bone Parent { get; set; }
        public Mdl0Bone FirstChild { get; set; }
        public Mdl0Bone Next { get; set; }
        public Mdl0Bone Previous { get; set; }
        public string Name { get; set; }

        public Mdl0Bone(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            Length = reader.ReadInt32();
            Mdl0Offset = reader.ReadInt32();
            NameOffset = reader.ReadInt32();
            Index = reader.ReadInt32();
            NodeId = reader.ReadInt32();
            Flags = reader.ReadUInt32();
            Unknown18 = reader.ReadInt32();
            Unknown1C = reader.ReadInt32();
            Scale = new Matrix3x1(reader);
            Rotation = new Matrix3x1(reader);
            Translation = new Matrix3x1(reader);
            Minimum = new Matrix3x1(reader);
            Maximum = new Matrix3x1(reader);
            ParentOffset = reader.ReadInt32();
            FirstChildOffset = reader.ReadInt32();
            NextOffset = reader.ReadInt32();
            PreviousOffset = reader.ReadInt32();
            Unknown6C = reader.ReadInt32();
            Transformation = new Matrix4x3(reader);
            Inverse = new Matrix4x3(reader);

            Name = BrresFile.ReadBrresString(reader, Address + NameOffset);
        }

        public void LoadTree(Collection<Mdl0Bone> bones)
        {
            for (int i = 0; i < bones.Count; i++)
            {
                if (ParentOffset != 0 && bones[i].Address == Address + ParentOffset)
                    Parent = bones[i];
                if (FirstChildOffset != 0 && bones[i].Address == Address + FirstChildOffset)
                    FirstChild = bones[i];
                if (NextOffset != 0 && bones[i].Address == Address + NextOffset)
                    Next = bones[i];
                if (PreviousOffset != 0 && bones[i].Address == Address + PreviousOffset)
                    Previous = bones[i];
            }
        }

        public void Write(EndianBinaryWriter writer, long mdl0Address)
        {
            Address = writer.BaseStream.Position;

            if (Parent != null) ParentOffset = (int)(Parent.Address - Address);
            else ParentOffset = 0;
            if (FirstChild != null) FirstChildOffset = (int)(FirstChild.Address - Address);
            else FirstChildOffset = 0;
            if (Next != null) NextOffset = (int)(Next.Address - Address);
            else NextOffset = 0;
            if (Previous != null) PreviousOffset = (int)(Previous.Address - Address);
            else PreviousOffset = 0;
            Mdl0Offset = (int)(mdl0Address - Address);

            writer.Write(Length);
            writer.Write(Mdl0Offset);
            writer.Write(NameOffset);
            writer.Write(Index);
            writer.Write(NodeId);
            writer.Write(Flags);
            writer.Write(Unknown18);
            writer.Write(Unknown1C);
            Scale.Write(writer);
            Rotation.Write(writer);
            Translation.Write(writer);
            Minimum.Write(writer);
            Maximum.Write(writer);
            writer.Write(ParentOffset);
            writer.Write(FirstChildOffset);
            writer.Write(NextOffset);
            writer.Write(PreviousOffset);
            writer.Write(Unknown6C);
            Transformation.Write(writer);
            Inverse.Write(writer);

            Length = (int)(writer.BaseStream.Position - Address);            
        }

        public void WriteNames(EndianBinaryWriter writer, Dictionary<string, long> names)
        {
            NameOffset = (int)(BrresFile.WriteString(writer, names, Name) - Address);
        }

        public Bone ToBone()
        {
            return new Bone() { Matrix = Transformation };// Matrix4x3.Transformation(Scale, Rotation, Translation) };
        }
    }
}
