using System;
using System.ComponentModel;
using System.IO;
using Chadsoft.CTools.Image;

namespace Chadsoft.CTools.Brres
{
    public class Tex0Image : ImageData
    {
        private ImageDataFormat _format;

        public Tex0Section Section { get; private set; }

        public override string Type
        {
            get { return "TEX0"; }
        }

        public override ImageDataFormat Format
        {
            get { return _format; }
        }

        public override int Levels
        {
            get { return Section.Tex0Header.MipMapLevels; }
        }

        public override int Width
        {
            get { return Section.Tex0Header.Width; }
        }

        public override int Height
        {
            get { return Section.Tex0Header.Height; }
        }

        public Tex0Image(Tex0Section section)
        {
            Section = section;

            switch (section.Tex0Header.Format)
            {
                case 0x0:
                    _format = ImageDataFormat.I4;
                    break;
                case 0x1:
                    _format = ImageDataFormat.I8;
                    break;
                case 0x2:
                    _format = ImageDataFormat.IA4;
                    break;
                case 0x3:
                    _format = ImageDataFormat.IA8;
                    break;
                case 0x4:
                    _format = ImageDataFormat.RGB565;
                    break;
                case 0x5:
                    _format = ImageDataFormat.RGB5A3;
                    break;
                case 0x6:
                    _format = ImageDataFormat.Rgba32;
                    break;
                case 0xe:
                    _format = ImageDataFormat.Cmpr;
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        public override int GetWidth(int level)
        {
            return Width >> level;
        }

        public override int GetHeight(int level)
        {
            return Height >> level;
        }

        public override ImageDataFormat[] GetFormats()
        {
            return new ImageDataFormat[] { ImageDataFormat.Cmpr, ImageDataFormat.I4, ImageDataFormat.I8, ImageDataFormat.IA4, ImageDataFormat.IA8, ImageDataFormat.RGB565, ImageDataFormat.RGB5A3, ImageDataFormat.Rgba32 };
        }

        public override byte[] GetData(int level, ProgressChangedEventHandler progress)
        {
            byte[] data;

            data = GetDataFor(level);

            return _format.ConvertFrom(data, GetWidth(level), GetHeight(level), progress);
        }

        public override void ImportTo(byte[] data, int level, ProgressChangedEventHandler progress)
        {
            byte[] formattedData;
            int offset, length;

            formattedData = _format.ConvertTo(data, GetWidth(level), GetHeight(level), progress);
            offset = 0;

            for (int i = 0; i < level; i++)
                offset += _format.RoundHeight(GetHeight(i)) * _format.RoundWidth(GetWidth(i)) * _format.BitsPerPixel >> 3;
            
            Array.Copy(formattedData, 0, Section.Data, offset, formattedData.Length);
        }

        public override void Import(byte[] data, ImageDataFormat format, int levels, int width, int height, ProgressChangedEventHandler progress)
        {
            int length;

            Section.Tex0Header.Width = (ushort)width;
            Section.Tex0Header.Height = (ushort)height;
            Section.Tex0Header.MipMapLevels = levels;
            _format = format;
            if (format == ImageDataFormat.I4)
                Section.Tex0Header.Format = 0x0;
            else if (format == ImageDataFormat.I8)
                Section.Tex0Header.Format = 0x1;
            else if (format == ImageDataFormat.IA4)
                Section.Tex0Header.Format = 0x2;
            else if (format == ImageDataFormat.IA8)
                Section.Tex0Header.Format = 0x3;
            else if (format == ImageDataFormat.RGB565)
                Section.Tex0Header.Format = 0x4;
            else if (format == ImageDataFormat.RGB5A3)
                Section.Tex0Header.Format = 0x5;
            else if (format == ImageDataFormat.Rgba32)
                Section.Tex0Header.Format = 0x6;
            else if (format == ImageDataFormat.Cmpr)
                Section.Tex0Header.Format = 0xe;

            length = 0;
            for (int i = 0; i < levels; i++)
                length += format.RoundWidth(GetWidth(i)) * format.RoundHeight(GetHeight(i)) * format.BitsPerPixel >> 3;

            Section.Data = new byte[length];

            for (int i = 0; i < levels; i++)
            {
                ImportTo(Resize(data, width, height, GetWidth(i), GetHeight(i)), i, progress);
            }
        }

        public override void Reformat(ImageDataFormat format, int levels, int width, int height, ProgressChangedEventHandler progress)
        {
            int length, oldWidth, oldHeight;
            byte[][] data;

            data = new byte[levels][];

            for (int i = 0; i < data.Length && i < Levels; i++)
                data[i] = GetData(i, progress);

            oldWidth = Section.Tex0Header.Width;
            oldHeight =Section.Tex0Header.Height;

            Section.Tex0Header.Width = (ushort)width;
            Section.Tex0Header.Height = (ushort)height;
            Section.Tex0Header.MipMapLevels = levels;

            _format = format;
            if (format == ImageDataFormat.I4)
                Section.Tex0Header.Format = 0x0;
            else if (format == ImageDataFormat.I8)
                Section.Tex0Header.Format = 0x1;
            else if (format == ImageDataFormat.IA4)
                Section.Tex0Header.Format = 0x2;
            else if (format == ImageDataFormat.IA8)
                Section.Tex0Header.Format = 0x3;
            else if (format == ImageDataFormat.RGB565)
                Section.Tex0Header.Format = 0x4;
            else if (format == ImageDataFormat.RGB5A3)
                Section.Tex0Header.Format = 0x5;
            else if (format == ImageDataFormat.Rgba32)
                Section.Tex0Header.Format = 0x6;
            else if (format == ImageDataFormat.Cmpr)
                Section.Tex0Header.Format = 0xe;

            length = 0;
            for (int i = 0; i < levels; i++)
			    length += format.RoundWidth(GetWidth(i)) * format.RoundHeight(GetHeight(i)) * format.BitsPerPixel >> 3;

            Section.Data = new byte[length];

            for (int i = 0; i < levels; i++)
            {
                if (data[i] != null)
                    ImportTo(Resize(data[i], oldWidth >> i, oldHeight >> i, GetWidth(i), GetHeight(i)), i, progress);
                else
                    ImportTo(Resize(data[0], oldWidth, oldHeight, GetWidth(i), GetHeight(i)), i, progress);
            }
        }

        private byte[] GetDataFor(int level)
        {
            byte[] temp;
            int offset, length;

            offset = 0;

            for (int i = 0; i < level; i++)
                offset += _format.RoundHeight(GetHeight(i)) * _format.RoundWidth(GetWidth(i)) * _format.BitsPerPixel >> 3;

            length = _format.RoundHeight(GetHeight(level)) * _format.RoundWidth(GetWidth(level)) * _format.BitsPerPixel >> 3;
            temp = new byte[length];

            Array.Copy(Section.Data, offset, temp, 0, length);

            return temp;
        }
    }
}
