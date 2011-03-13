using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;

namespace Chadsoft.CTools.Brres
{
    public class Mdl0TexturePointer
    {
        public long Address { get; set; }

        public int Count { get; set; }
        public Collection<Mdl0TexturePointerEntry> Pointers { get; private set; }

        public Mdl0TexturePointer(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            Count = reader.ReadInt32();

            Pointers = new Collection<Mdl0TexturePointerEntry>();
            for (int i = 0; i < Count; i++)
                Pointers.Add(new Mdl0TexturePointerEntry(reader));
        }

        public void Write(EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            Count = Pointers.Count;

            writer.Write(Count);

            for (int i = 0; i < Pointers.Count; i++)
                Pointers[i].Write(writer, Address);
        }
    }

    public class Mdl0TexturePointerEntry
    {
        public int MaterialOffset { get; set; }
        public int LayerOffset { get; set; }

        public Mdl0Material Material { get; set; }
        public Mdl0MaterialLayer Layer { get; set; }

        public Mdl0TexturePointerEntry(EndianBinaryReader reader)
        {
            MaterialOffset = reader.ReadInt32();
            LayerOffset = reader.ReadInt32();
        }

        public void Write(EndianBinaryWriter writer, long address)
        {
            MaterialOffset = (int)(Material.Address - address);
            LayerOffset = (int)(Layer.Address - address);

            writer.Write(MaterialOffset);
            writer.Write(LayerOffset);
        }
    }
}
