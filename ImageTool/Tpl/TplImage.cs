using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.ComponentModel;

namespace Chadsoft.CTools.Image.Tpl
{
    public class TplImage : ImageData
    {
        private byte[] data, palette;
        private ImageDataFormat _format;

        public TplHeader Header { get; private set; }
        public TplPaletteHeader PaletteHeader { get; private set; }
        public TplImageHeader ImageHeader { get; private set; }
        
        public TplImage(Stream imageData)
        {
            EndianBinaryReader reader;

            reader = new EndianBinaryReader(imageData);

            Header = new TplHeader(reader);

            if (Header.PaletteHeaderStart != 0)
            {
                imageData.Seek(Header.PaletteHeaderStart, SeekOrigin.Begin);
                PaletteHeader = new TplPaletteHeader(reader);

                imageData.Seek(PaletteHeader.PaletteStart, SeekOrigin.Begin);
                // TBD: Proper palettes
                palette = reader.ReadBytes(PaletteHeader.Entries * 2);

            }

            imageData.Seek(Header.ImageHeaderStart, SeekOrigin.Begin);
            ImageHeader = new TplImageHeader(reader);

            switch (ImageHeader.Format)
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
            }

            imageData.Seek(ImageHeader.ImageStart, SeekOrigin.Begin);
            data = reader.ReadBytes(Format.RoundWidth(ImageHeader.Width) * Format.RoundHeight(ImageHeader.Height) * _format.BitsPerPixel >> 3);
            
        }

        public override string Type
        {
            get { return Program.GetString("FromatDescriptionTpl"); }
        }

        public override ImageDataFormat Format
        {
            get { return _format; }
        }

        public override int Levels
        {
            get { return 1; }
        }

        public override int Width
        {
            get { return ImageHeader.Width; }
        }

        public override int Height
        {
            get { return ImageHeader.Height; }
        }

        public override int GetWidth(int level)
        {
            if (level != 0)
                throw new ArgumentException("level");

            return Width;
        }

        public override int GetHeight(int level)
        {
            if (level != 0)
                throw new ArgumentException("level");

            return Height;
        }

        public override ImageDataFormat[] GetFormats()
        {
            return new ImageDataFormat[] { ImageDataFormat.Cmpr, ImageDataFormat.I4, ImageDataFormat.I8, ImageDataFormat.IA4, ImageDataFormat.IA8, ImageDataFormat.RGB565, ImageDataFormat.RGB5A3, ImageDataFormat.Rgba32 };
        }

        public override byte[] GetData(int level,  ProgressChangedEventHandler progress)
        {
            if (level != 0)
                throw new ArgumentException("level");

            return _format.ConvertFrom(data, Width, Height, progress);
        }

        public override void ImportTo(byte[] data, int level, ProgressChangedEventHandler progress)
        {
            if (level != 0)
                throw new ArgumentException("level");

            data = _format.ConvertTo(data, Width, Height, progress);
        }

        public override void Import(byte[] data, ImageDataFormat format, int levels, int width, int height, ProgressChangedEventHandler progress)
        {
            if (levels != 1)
                throw new ArgumentException("levels");

            _format = format;
            ImageHeader.Width = (short)width;
            ImageHeader.Height = (short)height;

            this.data = format.ConvertTo(data, width, height,  progress);
        }

        public override void Save(Stream output)
        {
            EndianBinaryWriter writer;

            writer = new EndianBinaryWriter(output);

            if (PaletteHeader != null)
            {
                Header.PaletteHeaderStart = 0x14;
                Header.ImageHeaderStart = 0x20 + palette.Length;
            }

            Header.Write(writer);

            if (PaletteHeader != null)
            {
                PaletteHeader.Write(writer);
                writer.Write(palette, 0, palette.Length);
            }

            ImageHeader.ImageStart = (int)(0x40 - ((output.Position + 0x1c) % 0x40)) % 040;
            ImageHeader.Write(writer);

            writer.WritePadding(0x40, 0);
            writer.Write(data, 0, data.Length);
        }

        public override void Reformat(ImageDataFormat format, int levels, int width, int height, ProgressChangedEventHandler progress)
        {
            Bitmap image;
            byte[] data;
            
            if (levels != 1)
                throw new ArgumentException("level");

            data = GetData(0, progress);
            image = ToBitmap(data, Width, Height);
            image = new Bitmap(image, new Size(width, height));
            data = GetData(image);

            Import(data, format, levels, width, height, progress);
        }
    }
}
