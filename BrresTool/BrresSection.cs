using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Chadsoft.CTools.Brres
{
    public abstract class BrresSection : IDisposable
    {
        protected bool disposed;
        public long Address;

        public string Name { get; set; }
        
        ~BrresSection()
        {
            Dispose(false);
        }

        public abstract void Write(EndianBinaryWriter writer);
        public abstract void WriteNames(EndianBinaryWriter writer, Dictionary<string, long> names);

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;
            }
        }
    }
    public class BrresCommonSectionHeader
    {
        public long Address { get; set; }

        public int Tag { get; set; }
        public int Length { get; set; }
        public int Version { get; set; }
        public int BrresOffset { get; set; }

        public BrresCommonSectionHeader(EndianBinaryReader reader)
        {
            if (reader.BaseStream.Length < 0x10)
                throw new InvalidDataException();

            Address = reader.BaseStream.Position;

            Tag = reader.ReadInt32();
            Length = reader.ReadInt32();
            Version = reader.ReadInt32();
            BrresOffset = reader.ReadInt32();

            if (reader.BaseStream.Length - reader.BaseStream.Position < Length - 0x10)
                throw new InvalidDataException();
        }

        public void Write(EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            writer.Write(Tag);
            writer.Write(Length);
            writer.Write(Version);
            writer.Write(BrresOffset);
        }
    }
}
