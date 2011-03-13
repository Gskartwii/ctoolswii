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
using System.Globalization;
using System.IO;
using System.Text;

namespace Chadsoft.CTools.Bmg
{
    public class BmgMessage
    {
        private static Encoding encoding = Encoding.BigEndianUnicode;
        private string _message;
        private byte[] _binary;

        public int Index { get; set; }
        public int Id { get; set; }
        public int[] Data { get; set; }
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                _binary = ToBinary(value);
            }
        }
                
        public byte[] Binary
        {
            get
            {
                return _binary;
            }
            set
            {
                _binary = value;
                _message = FromBinary(value);
            }
        }
        
        public BmgMessage(BmgFile file, int index)
        {
            int binaryLength;
            Index = index;
            
            if (file.Mid1 != null)
                Id = file.Mid1.Entries[index];
            Data = file.Inf1[index];

            if (Data[0] == 0x00)
            {
                _message = null;
                _binary = new byte[0];
            }
            else
            {
                _message = FromBinary(file.Dat1.Data, file.Inf1[index][0], out binaryLength);
                _binary = new byte[binaryLength];
                
                Array.Copy(file.Dat1.Data, file.Inf1[index][0], Binary, 0, Binary.Length);
            }
        }

        public BmgMessage(BmgFile file)
        {
            if (file.Messages.Count > 0)
                Id = file.Messages[file.Messages.Count - 1].Id + 1;
            else
                Id = 0;

            Message = "";
            Index = file.Messages.Count + 1;
        }

        private static byte[] ToBinary(string value)
        {
            byte length;
            byte[] temp;
            string newLine;
            bool replace;
            MemoryStream output;

            newLine = Properties.Settings.Default.NewLine;
            replace = Properties.Settings.Default.ReplaceSequences;
            output = new MemoryStream();

            if (!string.IsNullOrEmpty(newLine))
            {
                value = value
                    .Replace("\r\n", newLine)
                    .Replace("\n", newLine)
                    .Replace("\r", newLine);
            }

            if (replace)
            {
                value += '\0';
                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i] == '\\' && value.Length >= i + 2)
                    {
                        if (value[i + 1] == '\\')
                        {
                            temp = encoding.GetBytes(value.ToCharArray(), i++, 1);
                            output.Write(temp, 0, temp.Length);
                        }
                        else if (value[i + 1] == 'x' && value.Length >= i + 6)
                        {
                            temp = new byte[0x8];
                            temp[1] = 0x1a;
                            temp[2] = 0x8;
                            temp[3] = 0x1;
                            byte.TryParse(value.Substring(i + 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp[0x6]);
                            byte.TryParse(value.Substring(i + 4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp[0x7]);
                            output.Write(temp, 0, temp.Length);
                            i += 5;
                        }
                        else if (value[i + 1] == 'q' && value.Length >= i + 14)
                        {
                            temp = new byte[0xa];
                            temp[1] = 0x1a;
                            temp[2] = 0xa;
                            temp[3] = 0x2;
                            byte.TryParse(value.Substring(i + 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp[0x4]);
                            byte.TryParse(value.Substring(i + 4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp[0x5]);
                            byte.TryParse(value.Substring(i + 6, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp[0x6]);
                            byte.TryParse(value.Substring(i + 8, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp[0x7]);
                            byte.TryParse(value.Substring(i + 10, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp[0x8]);
                            byte.TryParse(value.Substring(i + 12, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp[0x9]);
                            output.Write(temp, 0, temp.Length);
                            i += 13;
                        }
                        else if (value[i + 1] == 'p' && value.Length >= i + 10)
                        {
                            temp = new byte[0x8];
                            temp[1] = 0x1a;
                            temp[2] = 0x8;
                            temp[3] = 0x2;
                            byte.TryParse(value.Substring(i + 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp[0x4]);
                            byte.TryParse(value.Substring(i + 4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp[0x5]);
                            byte.TryParse(value.Substring(i + 6, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp[0x6]);
                            byte.TryParse(value.Substring(i + 8, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp[0x7]);
                            output.Write(temp, 0, temp.Length);
                            i += 9;
                        }
                        else if (value[i + 1] == 's' && value.Length >= i + 6)
                        {
                            temp = new byte[0x6];
                            temp[1] = 0x1a;
                            temp[2] = 0x6;
                            temp[3] = 0x2;
                            byte.TryParse(value.Substring(i + 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp[0x4]);
                            byte.TryParse(value.Substring(i + 4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp[0x5]);
                            output.Write(temp, 0, temp.Length);
                            i += 5;
                        }
                        else
                        {
                            if (value.Length >= i + 3 && byte.TryParse(value.Substring(i + 1, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out length) && value.Length >= i + 1 + (length - 2) * 2)
                            {
                                temp = new byte[length];
                                temp[1] = 0x1a;
                                temp[2] = length;

                                i += 3;
                                for (int j = 3; j < length; j++, i += 2)
                                {
                                    byte.TryParse(value.Substring(i, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out temp[j]);
                                }
                                output.Write(temp, 0, temp.Length);
                                i--;
                            }
                            else
                            {
                                temp = encoding.GetBytes(value.ToCharArray(), i, 1);
                                output.Write(temp, 0, temp.Length);
                            }
                        }
                    }
                    else
                    {
                        temp = encoding.GetBytes(value.ToCharArray(), i, 1);
                        output.Write(temp, 0, temp.Length);
                    }
                }
            }
            else
            {
                temp = encoding.GetBytes(value + '\0');
                output.Write(temp, 0, temp.Length);
            }

            temp = output.ToArray();
            output.Close();

            return temp;
        }

        private string FromBinary(byte[] value)
        {
            int temp;

            return FromBinary(value, 0, out temp);
        }

        private static string FromBinary(byte[] binary, int index, out int binaryLength)
        {
            Decoder decoder;
            char[] result;
            int pos, initial, readBytes, readChars;
            bool success, replace;

            result = new char[0x3FF];
            decoder = encoding.GetDecoder();
            pos = 0;
            initial = index;
            replace = Bmg.Properties.Settings.Default.ReplaceSequences;

            while (index < binary.Length && pos < result.Length && !(binary[index] == 0x00 && binary[index + 1] == 0x00))
            {
                success = false;

                while (index < binary.Length && !success)
                {
                    decoder.Convert(binary, index, 2, result, pos, 1, false, out readBytes, out readChars, out success);

                    pos += readChars;
                    index += readBytes;
                }

                if (replace)
                    ReplaceSequences(binary, ref index, result, ref pos);
            }

            binaryLength = index - initial;

            return new string(result, 0, pos);
        }

        private static void ReplaceSequences(byte[] binary, ref int index, char[] result, ref int pos)
        {
            int length;

            if (result[pos - 1] == '\\')
                result[pos++] = '\\';
            else if (result[pos - 1] == '\x001A')
            {
                result[pos - 1] = '\\';

                length = binary[index] - 2;
                if (binary[index + 1] == 1 && length == 6 && binary[index + 2] == 0 & binary[index + 3] == 0)
                {
                    result[pos++] = 'x';
                    index += 4;
                    length = 2;
                }
                else if (binary[index + 1] == 2 && length == 8)
                {
                    result[pos++] = 'q';
                    length -= 2;
                    index += 2;
                }
                else if (binary[index + 1] == 2 && length == 6)
                {
                    result[pos++] = 'p';
                    length -= 2;
                    index += 2;
                }
                else if (binary[index + 1] == 2 && length == 4)
                {
                    result[pos++] = 's';
                    length -= 2;
                    index += 2;
                }


                for (int i = 0; i < length; i++, index++)
                {
                    result[pos++] = binary[index].ToString("X2")[0];
                    result[pos++] = binary[index].ToString("X2")[1];
                }

                //index += Data[index];
            }
        }
    }
}
