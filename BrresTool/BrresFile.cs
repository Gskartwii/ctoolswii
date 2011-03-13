using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;

namespace Chadsoft.CTools.Brres
{
    public class BrresFile : IDisposable
    {
        bool disposed;

        public long Address { get; set; }
        public Endianness Endianness { get; set; }
        public bool Changed { get; set; }
        public string Name { get; set; }
        public BrresHeader Header { get; set; }
        public RootSection RootSection { get; set; }

        public BrresFile(Stream data)
        {
            BrresCommonSectionHeader sectionHeader;
            EndianBinaryReader reader;
            
            reader = new EndianBinaryReader(data);

            Address = reader.BaseStream.Position;
            Header = new BrresHeader(reader);
            Endianness = reader.Endianness;

            reader.BaseStream.Seek(Address + Header.RootOffset, SeekOrigin.Begin);
            RootSection = new RootSection(reader);

            for (int i = 0; i < RootSection.Folders.Count; i++)
                for (int j = 1; j < RootSection.Folders[i].Entries.Count; j++)
                {
                    reader.BaseStream.Seek(RootSection.Folders[i].Address + RootSection.Folders[i].Entries[j].DataOffset, SeekOrigin.Begin);

                    sectionHeader = new BrresCommonSectionHeader(reader);

                    switch (sectionHeader.Tag)
                    {
                        case Mdl0Section.Tag:
                            RootSection.Folders[i].Entries[j].Section = new Mdl0Section(reader, sectionHeader);
                            break;
                        case Tex0Section.Tag:
                            RootSection.Folders[i].Entries[j].Section = new Tex0Section(reader, sectionHeader);
                            break;
                        case Pat0Section.Tag:
                            RootSection.Folders[i].Entries[j].Section = new Pat0Section(reader, sectionHeader);
                            break;
                        default:
                            RootSection.Folders[i].Entries[j].Section = new UnknownSection(reader, sectionHeader);
                            break;

                    }
                }
        }

        public void Save(Stream stream)
        {
            EndianBinaryWriter writer;
            Dictionary<string, long> names;

            writer = new EndianBinaryWriter(stream);
            names = new Dictionary<string,long>();

            Address = writer.BaseStream.Position;

            Header.Write(writer);
            RootSection.Write(writer);
            writer.WritePadding(0x20, 0);
            Header.RootOffset = (short)(RootSection.Address - Address);
            Header.FileSize = (int)(stream.Length - Address);
            Header.Sections = 1;
            for (int i = 0; i < RootSection.Folders.Count; i++)
            {
                Header.Sections += (short)(RootSection.Folders[i].EntryCount - 1);
            }
            RootSection.WriteNames(writer, names);
            writer.WritePadding(0x100, 0);
            writer.BaseStream.Seek(Address, SeekOrigin.Begin);
            Header.Write(writer);
            RootSection.Write(writer);
        }

        ~BrresFile()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {

                }

                disposed = true;
            }
        }

        public static string ReadBrresString(EndianBinaryReader reader, long addess)
        {
            int stringLength;

            SafeSeek(reader, addess - 4, 4);
            stringLength = reader.ReadInt32();

            if (reader.BaseStream.Length < addess + stringLength)
                throw new InvalidDataException();

            return reader.ReadString(Encoding.ASCII, stringLength);
        }

        public static void SafeSeek(EndianBinaryReader reader, long address, int length)
        {
            if (reader.BaseStream.Length < address + length)
                throw new InvalidDataException();
            else if (address < 0)
                throw new InvalidDataException();

            reader.BaseStream.Seek(address, SeekOrigin.Begin);
        }
        
        public Tex0Section GetTextureByName(string name)
        {
            for (int i = 0; i < RootSection.Folders.Count; i++)
                for (int j = 1; j < RootSection.Folders[i].Entries.Count; j++)
                {
                    if (RootSection.Folders[i].Entries[j].Name == name && RootSection.Folders[i].Entries[j].Section.GetType() == typeof(Tex0Section))
                        return RootSection.Folders[i].Entries[j].Section as Tex0Section;
                }

            return null;
        }

        public static long WriteString(EndianBinaryWriter writer, Dictionary<string, long> names, string Name)
        {
            long address;

            if (names.ContainsKey(Name))
                return names[Name];

            writer.Write(Name.Length);
            address = writer.BaseStream.Position;
            names.Add(Name, address);
            writer.Write(Name, Encoding.ASCII, false);
            writer.WritePadding(4, 0x00);

            return address;
        }
    }
}
