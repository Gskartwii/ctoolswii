using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace Chadsoft.CTools.Brres
{
    public class Mdl0DrawList
    {
        public long Address { get; set; }

        public Collection<Mdl0DrawListInstruction> Instructions { get; private set; }

        public Mdl0DrawList(EndianBinaryReader reader)
        {
            Mdl0DrawListInstruction instruction;

            Address = reader.BaseStream.Position;

            Instructions = new Collection<Mdl0DrawListInstruction>();

            do
            {
                instruction = new Mdl0DrawListInstruction(reader);
                Instructions.Add(instruction);
            } while (instruction.Instruction != 1);
        }

        public void Write(EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            for (int i = 0; i < Instructions.Count; i++)
            {
                Instructions[i].Write(writer);
            }
        }
    }

    public class Mdl0DrawListInstruction
    {
        public byte Instruction { get; set; }
        public short Parameter1 { get; set; }
        public short Parameter2 { get; set; }
        public short Parameter3 { get; set; }
        public byte Parameter4 { get; set; }

        public Mdl0DrawListInstruction(EndianBinaryReader reader)
        {
            Instruction = reader.ReadByte();

            switch (Instruction)
            {
                case 1:
                    break;
                case 2:
                    Parameter1 = reader.ReadInt16();
                    Parameter2 = reader.ReadInt16();
                    break;
                case 4:
                    Parameter1 = reader.ReadInt16();
                    Parameter2 = reader.ReadInt16();
                    Parameter3 = reader.ReadInt16();
                    Parameter4 = reader.ReadByte();
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(Instruction);

            switch (Instruction)
            {
                case 2:
                    writer.Write(Parameter1);
                    writer.Write(Parameter2);
                    break;
                case 4:
                    writer.Write(Parameter1);
                    writer.Write(Parameter2);
                    writer.Write(Parameter3);
                    writer.Write(Parameter4);
                    break;
            }
        }
    }
}
