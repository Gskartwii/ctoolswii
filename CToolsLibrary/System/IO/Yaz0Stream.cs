// CTools library - Library functions for CTools
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

using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO.Compression
{
    public class Yaz0Stream : Stream
    {
        private static int LookBackCache = -1;
        private static int lookBack { get { if (LookBackCache == -1) LookBackCache = Chadsoft.CTools.Properties.Settings.Default.compressionLookBack; return LookBackCache; } }

        private const int threadChunk = 0x10000;
        private MemoryStream dataBuffer;
        private EndianBinaryReader reader;
        private EndianBinaryWriter writer;
        private int codeBits, toCopy, copyPosition, _position;
        private byte codeByte;
        private byte[] writeBuffer;
        private List<Contraction>[] contractions;

        public CompressionMode Mode { get; private set; }

        public Stream BaseStream { get; private set; }

        public override bool CanRead { get { return Mode ==  CompressionMode.Decompress; } }

        public override bool CanSeek { get { return BaseStream.CanSeek && CanRead; } }

        public override bool CanTimeout { get { return BaseStream.CanTimeout; } }

        public override bool CanWrite { get { return Mode == CompressionMode.Compress; } }

        public bool HasHeader { get; private set; }

        public int DecompressedSize { get; private set; }

        public override long Length
        {
            get { return DecompressedSize; }
        }

        public override long Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (!CanSeek) throw new InvalidOperationException();

                throw new NotImplementedException();
            }
        }

        public override int ReadTimeout
        {
            get
            {
                return BaseStream.ReadTimeout;
            }
            set
            {
                BaseStream.ReadTimeout = value;
            }
        }

        public override int WriteTimeout
        {
            get
            {
                return BaseStream.WriteTimeout;
            }
            set
            {
                BaseStream.WriteTimeout = value;
            }
        }

        public Yaz0Stream(Stream baseStream, CompressionMode mode)
        {
            Mode = mode;
            BaseStream = baseStream;

            if ((mode == CompressionMode.Compress && !baseStream.CanWrite) || (mode == CompressionMode.Decompress && !baseStream.CanRead)) throw new ArgumentException("baseStream");

            if (mode == CompressionMode.Decompress)
                reader = new EndianBinaryReader(baseStream);
            else
            {
                writer = new EndianBinaryWriter(baseStream);
                dataBuffer = new MemoryStream();
            }

            DecompressedSize = -1;
        }

        public bool ReadHeader()
        {
            string tag;

            if (!CanRead || HasHeader) throw new InvalidOperationException();
            
            tag = reader.ReadString(Encoding.ASCII, 4);

            if (tag != "Yaz0") return false;

            DecompressedSize = reader.ReadInt32s(3)[0];
            dataBuffer = new MemoryStream(DecompressedSize);

            HasHeader = true;

            return true;
        }

        public override void Flush()
        {
            if (CanWrite)
                WriteFlush();
            BaseStream.Flush();
        }

        private void WriteFlush()
        {
            int chunkCount;
            ParallelLoopResult result;

            if (HasHeader) throw new InvalidOperationException();

            chunkCount = (int)Math.Ceiling((double)dataBuffer.Length / threadChunk);
            contractions = new List<Contraction>[chunkCount];
            writeBuffer = dataBuffer.ToArray();

            WriteHeader(writeBuffer.Length);

            result = Parallel.For(0, chunkCount, FindContractions);

            while (!result.IsCompleted)
                Thread.Sleep(100);

            Compile();
        }

        private void Compile()
        {
            List<Contraction> fullContractions;
            int codeBits, tempLoc, current; 
            byte codeByte;
            byte[] temp;

            fullContractions = new List<Contraction>();

            for (int i = 0; i < contractions.Length; i++)
            {
                fullContractions.AddRange(contractions[i]);
                contractions[i].Clear();
                contractions[i] = null;
            }

            contractions = null;
            temp = new byte[3 * 8];
            codeBits = 0;
            codeByte = 0;
            tempLoc = 0;
            current = 0;

            for (int i = 0; i < writeBuffer.Length;)
            {
                if (codeBits == 8)
                {
                    BaseStream.WriteByte(codeByte);
                    BaseStream.Write(temp, 0, tempLoc);
                    codeBits = 0;
                    codeByte = 0;
                    tempLoc = 0;
                }

                if (current < fullContractions.Count && fullContractions[current].Location == i)
                {
                    if (fullContractions[current].Size >= 0x12)
                    {
                        temp[tempLoc++] = (byte)(fullContractions[current].Offset >> 8);
                        temp[tempLoc++] = (byte)(fullContractions[current].Offset & 0xFF);
                        temp[tempLoc++] = (byte)(fullContractions[current].Size - 0x12);
                    }
                    else
                    {
                        temp[tempLoc++] = (byte)((fullContractions[current].Offset >> 8) | ((fullContractions[current].Size - 2) << 4));
                        temp[tempLoc++] = (byte)(fullContractions[current].Offset & 0xFF);
                    }

                    i += fullContractions[current++].Size;

                    while (current < fullContractions.Count && fullContractions[current].Location < i)
                        current++;
                }
                else
                {
                    codeByte |= (byte)(1 << (7 - codeBits));
                    temp[tempLoc++] = writeBuffer[i++];
                }

                codeBits++;
            }

            BaseStream.WriteByte(codeByte);
            BaseStream.Write(temp, 0, tempLoc);
        }

        private void WriteHeader(int size)
        {
            if (!CanWrite || HasHeader) throw new InvalidOperationException();

            writer.Write("Yaz0", Encoding.ASCII, false);
            writer.Write(DecompressedSize = size);
            writer.Write(0);
            writer.Write(0);

            toCopy = 1;

            HasHeader = true;
        }

        private void FindContractions(int chunk)
        {
            int from, to, run, bestRun, bestOffset;
            Contraction contraction;

            contractions[chunk] = new List<Contraction>();

            from = chunk * threadChunk;
            to = Math.Min(from + threadChunk, writeBuffer.Length);

            for (int i = from; i < to;)
            {
                bestRun = 0;
                bestOffset = 0;

                for (int j = i -1; j > 0 && j >= i - lookBack; j--)
                {
                    run = 0;

                    while (i + run < writeBuffer.Length && run < 0x111 && writeBuffer[j + run] == writeBuffer[i + run])
                        run++;

                    if (run > bestRun)
                    {
                        bestRun = run;
                        bestOffset = i - j - 1;

                        if (run == 0x111) break;
                    }
                }

                if (bestRun >= 3)
                {
                    contraction = new Contraction(i, bestRun, bestOffset);
                    contractions[chunk].Add(contraction);
                    i += bestRun;
                }
                else
                    i++;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            byte current, code1, code2, code3;

            if (!CanRead || !HasHeader) throw new InvalidOperationException();

            count = (int)Math.Min(count, Length - Position);

            if (count == 0) return count;

            for (int read = 0; read < count; read++)
            {
                if (toCopy > 0)
                {
                    dataBuffer.Seek(copyPosition, SeekOrigin.Begin);
                    current = (byte)dataBuffer.ReadByte();
                    dataBuffer.Seek(_position, SeekOrigin.Begin);
                    dataBuffer.WriteByte(current); 
                    buffer[offset] = current;

                    toCopy--; 
                    offset++; 
                    _position++;
                    copyPosition++;
                }
                else
                {
                    if (codeBits == 0)
                    {
                        codeByte = (byte)BaseStream.ReadByte();
                        codeBits = 8;
                    }

                    if ((codeByte & 0x80) == 0)
                    {
                        code1 = (byte)BaseStream.ReadByte();
                        code2 = (byte)BaseStream.ReadByte();

                        copyPosition = _position -(((code1 & 0xf) << 8 | code2) + 1);

                        if ((code1 & 0xF0) == 0)
                        {
                            code3 = (byte)BaseStream.ReadByte();

                            toCopy = code3 + 0x12;
                        }
                        else
                        {
                            toCopy = (code1 >> 4) + 2;
                        }

                        read--;
                    }
                    else
                    {
                        current = (byte)BaseStream.ReadByte();
                        dataBuffer.WriteByte(current);
                        buffer[offset] = current;

                        offset++; _position++;
                    }

                    codeByte <<= 1;
                    codeBits--;
                }
            }

            return count;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = offset;
                    break;
                case SeekOrigin.Current:
                    Position += offset;
                    break;
                case SeekOrigin.End:
                    Position = Length + offset;
                    break;
            }

            return Position;
        }

        public override void SetLength(long value)
        {
            throw new InvalidOperationException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (!CanWrite || HasHeader) throw new InvalidOperationException();

            if (count == 0) return;

            dataBuffer.Write(buffer, offset, count);
            _position += count;
        }

        private struct Contraction
        {
            public int Location;
            public int Size;
            public int Offset;

            public Contraction(int loc, int sz, int off)
            {
                Location = loc;
                Size = sz;
                Offset = off;
            }
        }
    }
}
