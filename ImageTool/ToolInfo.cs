// CTools szs tool - Archive editor for CTools
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
using System.Drawing;
using System.IO;
using System.IO.Compression;
using Chadsoft.CTools.Image.Tpl;
using Chadsoft.CTools.Image.Bti;

namespace Chadsoft.CTools.Image
{
    public static class ToolInfo
    {
        private static Tool tool;
        private static Editor tplEditor, btiEditor;

        public static Tool Tool
        {
            get
            {
                if (tool == null)
                    SetupTool();
                return tool;
            }
        }

        private static void SetupTool()
        {
            ReadOnlyCollection<FileFormat> formats;

            formats = GetFormats();
            tool = new Tool(
                "Image Editor",
                Program.GetString("AssemblyDescription"), 
                "Chadderz", 
                new Version(1,0,0,0),
                Properties.Resources.ApplicationIcon,
                GetEditors(formats),
                formats,
                GetNewFiles(formats)
                );
        }

        private static ReadOnlyCollection<FileFormat> GetFormats()
        {
            return new ReadOnlyCollection<FileFormat>(new FileFormat[]
                {
                    new FileFormat(Program.GetString("FormatNameTpl"), Program.GetString("FormatDescriptionTpl"), Program.GetString("FormatCategoryImages"), Properties.Resources.FormatImageTpl, TplFormatMatch),
                    new FileFormat(Program.GetString("FormatNameBti"), Program.GetString("FormatDescriptionBti"), Program.GetString("FormatCategoryImages"), Properties.Resources.FormatImageBti, BtiFormatMatch),
                });
        }

        private static ReadOnlyCollection<Editor> GetEditors(ReadOnlyCollection<FileFormat> formats)
        {
            return new ReadOnlyCollection<Editor>(new Editor[]
                {
                    tplEditor = new Editor("Tpl Image Editor", Program.GetString("AssemblyDescription"), "Chadderz", new Version(1,0,0,0), Properties.Resources.ApplicationIcon, new ReadOnlyCollection<FileFormat>(new FileFormat[] { formats[0] }), CreateTplInstance, RenderTplPreview),
                    btiEditor = new Editor("Bti Image Editor", Program.GetString("AssemblyDescription"), "Chadderz", new Version(1,0,0,0), Properties.Resources.ApplicationIcon, new ReadOnlyCollection<FileFormat>(new FileFormat[] { formats[1] }), CreateBtiInstance, RenderBtiPreview),
                });
        }

        private static ReadOnlyCollection<NewFile> GetNewFiles(ReadOnlyCollection<FileFormat> formats)
        {
            return new ReadOnlyCollection<NewFile>(new NewFile[]
                {
                    new NewFile(Program.GetString("FormatNameTpl"), "Untitled.tpl", Program.GetString("FormatDescriptionTpl"), Properties.Resources.FormatImageTpl,  formats[0], Properties.Resources.FormatNewTpl),
                    new NewFile(Program.GetString("FormatNameBti"), "Untitled.bti", Program.GetString("FormatDescriptionBti"), Properties.Resources.FormatImageBti,  formats[1], Properties.Resources.FormatNewBti),
                });
        }

        private static int TplFormatMatch(string name, byte[] data, int offset)
        {
            if (data.Length >= offset + 4 && data[offset + 0] == 0x00 && data[offset + 1] == 0x20 && data[offset + 2] == 0xaf && data[offset + 3] == 0x30)
                return 100;
            else
                return 0;
        }

        private static int BtiFormatMatch(string name, byte[] data, int offset)
        {
            ImageDataFormat format;
            MemoryStream ms;
            EndianBinaryReader reader;
            BtiHeader header;

            if (data.Length < 0x20)
                return 0;

            ms = new MemoryStream(data);
            reader = new EndianBinaryReader(ms);
            header = new BtiHeader(reader);

            if (header.ImageDataStart >= data.Length || header.ImageDataStart < 0x20)
                return 0;

            switch (header.Format)
            {
                case 0x0:
                    format = ImageDataFormat.I4;
                    break;
                case 0x1:
                    format = ImageDataFormat.I8;
                    break;
                case 0x2:
                    format = ImageDataFormat.IA4;
                    break;
                case 0x3:
                    format = ImageDataFormat.IA8;
                    break;
                case 0x4:
                    format = ImageDataFormat.RGB565;
                    break;
                case 0x5:
                    format = ImageDataFormat.RGB5A3;
                    break;
                case 0x6:
                    format = ImageDataFormat.Rgba32;
                    break;
                case 0xe:
                    format = ImageDataFormat.Cmpr;
                    break;
                default:
                    return 0;
            }

            if (data.Length < header.ImageDataStart + (format.RoundWidth(header.Width) * format.RoundHeight(header.Height) * format.BitsPerPixel >> 3))
                return 0;
            else
                return 50;
        }
        
        private static EditorInstance CreateTplInstance(byte[] data, string name, EventHandler<SaveEventArgs> saveEvent, EventHandler closeEvent)
        {
            return new TplEditorInstance(data, name, tplEditor, saveEvent, closeEvent);
        }

        private static EditorInstance CreateBtiInstance(byte[] data, string name, EventHandler<SaveEventArgs> saveEvent, EventHandler closeEvent)
        {
            return new BtiEditorInstance(data, name, btiEditor, saveEvent, closeEvent);
        }

        private static void RenderTplPreview(byte[] data, Graphics graphics)
        {
            TplImage image;
            MemoryStream stream;
            Bitmap bitmapImage;

            stream = new MemoryStream(data);
            image = new TplImage(stream);

            bitmapImage = ImageData.ToBitmap(image.GetColorData(0), image.Width, image.Height);

            graphics.DrawImageUnscaled(bitmapImage, 0, 0);

            bitmapImage.Dispose();
            image.Dispose();
            stream.Close();
        }
        private static void RenderBtiPreview(byte[] data, Graphics graphics)
        {            
            BtiImage image;
            MemoryStream stream;
            Bitmap bitmapImage;

            stream = new MemoryStream(data);
            image = new BtiImage(stream);

            bitmapImage = ImageData.ToBitmap(image.GetColorData(0), image.Width, image.Height);

            graphics.DrawImageUnscaled(bitmapImage, 0, 0);

            bitmapImage.Dispose();
            image.Dispose();
            stream.Close();
        }
    }
}
