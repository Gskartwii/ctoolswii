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
using System.Collections.ObjectModel;
using System.IO;

namespace Chadsoft.CTools.Bmg
{
    public class BmgFile : IDisposable 
    {
        bool disposed;

        public BmgHeader Header { get; private set; }
        public Inf1 Inf1 { get; private set; }
        public Dat1 Dat1 { get; private set; }
        public Mid1 Mid1 { get; private set; }

        public Collection<BmgMessage> Messages { get; private set; }
        public string Name { get; set; }
        public bool Changed { get; set; }

        public BmgFile(byte[] data)
            : this(new MemoryStream(data))
        {

        }

        public BmgFile(Stream input)
        {
            BmgMessage message;
            EndianBinaryReader reader;

            reader = new EndianBinaryReader(input);
            Header = new BmgHeader(reader);
            Inf1 = new Inf1(reader);
            Dat1 = new Dat1(reader);
            if (Header.Sections >= 3)
            {
                Mid1 = new Mid1(reader);
                if (Mid1.Messages != Inf1.Messages)
                    throw new InvalidDataException();
            }

            Messages = new Collection<BmgMessage>();

            for (int i = 0; i < Inf1.Messages; i++)
            {
                message = new BmgMessage(this, i);
                InsertMessage(Messages, message);
            }            
        }

        ~BmgFile()
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
                    if (Messages != null) 
                        Messages.Clear();
                }

                Messages = null;
                Inf1 = null;
                Dat1 = null;
                Mid1 = null;
                Header = null;
                Name = null;

                disposed = true;
            }
        }

        public void Save(Stream stream)
        {
            EndianBinaryWriter writer;

            writer = new EndianBinaryWriter(stream);

            Recalculate();

            Header.Write(writer);
            Inf1.Write(writer);
            Dat1.Write(writer);
            if (Header.Sections >= 3)
                Mid1.Write(writer);
        }

        private static void InsertMessage(Collection<BmgMessage> Messages, BmgMessage message)
        {
            bool added;

            added = false;
            for (int i = 0; i < Messages.Count; i++)
            {
                if (Messages[i].Id > message.Id)
                {
                    added = true;
                    Messages.Insert(i, message);
                    break;
                }
            }

            if (!added)
                Messages.Add(message);
        }

        private void Recalculate()
        {
            MemoryStream output;
            EndianBinaryWriter writer;
            bool replace;
            string newLine;

            output = new MemoryStream();
            writer = new EndianBinaryWriter(output);

            writer.Write((short)0);
            replace = Bmg.Properties.Settings.Default.ReplaceSequences;
            newLine = Bmg.Properties.Settings.Default.NewLine;

            foreach (BmgMessage message in Messages)
            {
                if (message.Data == null)
                    message.Data = new int[Inf1.Stride >> 2];

                if (string.IsNullOrEmpty(message.Message))
                {
                    message.Data[0] = 0;
                }
                else
                {
                    message.Data[0] = (int)output.Position;
                    output.Write(message.Binary, 0, message.Binary.Length);
                    writer.Write((short)0);
                }
            }
            
            output.Write(new byte[0x1F], 0, (int)(0x20 - ((output.Length + 8) % 0x20)) % 0x20);
            Dat1.Data = output.ToArray();
            Dat1.SectionSize = Dat1.Data.Length + 8;
            output.Close();

            if (Header.Sections >= 3)
            {
                Mid1.Messages = (short)Messages.Count;
                Mid1.SectionSize = 0x10 + Mid1.Messages * 4;
                Mid1.SectionSize += (0x20 - (Mid1.SectionSize % 0x20)) % 0x20;
                Mid1.Entries = new int[(Mid1.SectionSize - 0x10) / 4];

                for (int i = 0; i < Messages.Count; i++)
                {
                    Mid1.Entries[i] = Messages[i].Id;
                }
            }

            Inf1.Messages = (short)Messages.Count;
            Inf1.SectionSize = 0x10 + Inf1.Messages * Inf1.Stride;
            Inf1.SectionSize += (0x20 - (Inf1.SectionSize % 0x20)) % 0x20;
            Inf1.Entries = new int[(Inf1.SectionSize - 0x10) / 4];

            Header.FileSize = 0x20 + Inf1.SectionSize + Dat1.SectionSize + Header.Sections >= 3 ? Mid1.SectionSize : 0;

            for (int i = 0; i < Messages.Count; i++)
            {
                Inf1[i] = Messages[i].Data;
            }
        }
    }
}
