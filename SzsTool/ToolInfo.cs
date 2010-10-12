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
using Chadsoft.CTools.Szs.Archive;

namespace Chadsoft.CTools.Szs
{
    public static class ToolInfo
    {
        private static Tool tool;
        private static Editor SzsExplorer;

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
                "SZS Explorer",
                Program.GetString("AssemblyDescription"), 
                "Chadderz", 
                new Version(1,0,3,0),
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
                    new FileFormat(Program.GetString("FormatArchiveName"), Program.GetString("FormatArchiveDescription"), Program.GetString("FormatCategoryArchives"), Properties.Resources.FormatImageArchive, ArchiveFormatMatch),
                    new FileFormat(Program.GetString("FormatCompressedArchiveName"), Program.GetString("FormatCompressedArchiveDescription"), Program.GetString("FormatCategoryArchives"), Properties.Resources.FormatImageArchive, CompressedArchiveFormatMatch),
                });
        }

        private static ReadOnlyCollection<Editor> GetEditors(ReadOnlyCollection<FileFormat> formats)
        {
            return new ReadOnlyCollection<Editor>(new Editor[]
                {
                    SzsExplorer = new Editor("SZS Explorer", Program.GetString("AssemblyDescription"), "Chadderz", new Version(1,0,1,0), Properties.Resources.ApplicationIcon, formats, CreateInstance, RenderPreview),
                });
        }

        private static ReadOnlyCollection<NewFile> GetNewFiles(ReadOnlyCollection<FileFormat> formats)
        {
            return new ReadOnlyCollection<NewFile>(new NewFile[]
                {
                    new NewFile(Program.GetString("FormatArchiveName"), "Untitled.arc", Program.GetString("FormatArchiveDescription"), Properties.Resources.FormatImageArchive,  formats[0], Properties.Resources.FormatArchiveNew),
                    new NewFile(Program.GetString("FormatCompressedArchiveName"), "Untitled.szs", Program.GetString("FormatCompressedArchiveDescription"), Properties.Resources.FormatImageArchive,  formats[1], Properties.Resources.FormatCompressedArchiveNew),
                });
        }

        private static int ArchiveFormatMatch(string name, byte[] data, int offset)
        {
            if (data.Length >= 4 && data[0] == 0x55 && data[1] == 0xAA && data[2] == 0x38 && data[3] == 0x2D)
                return 100;
            else
                return 0;
        }

        private static int CompressedArchiveFormatMatch(string name, byte[] data, int offset)
        {
            MemoryStream ms;
            Yaz0Stream yz;
            EndianBinaryReader reader;
            int match;

            if (data.Length < 0x15) return 0;

            ms = new MemoryStream(data);
            yz = new Yaz0Stream(ms, CompressionMode.Decompress);

            try
            {
                if (!yz.ReadHeader())
                    match = 0;
                else
                {
                    reader = new EndianBinaryReader(yz);

                    if (reader.ReadInt32() == ArchiveHeader.U8Tag)
                        match = 100;
                    else
                        match = 0;
                }
            }
            catch (Exception)
            {
                match = 0;
            }
            finally
            {
                yz.Close();
                ms.Close();
            }

            return match;
        }

        private static EditorInstance CreateInstance(byte[] data, string name, EventHandler<SaveEventArgs> saveEvent, EventHandler closeEvent)
        {
            return new SzsExplorerInstance(data, name, SzsExplorer, saveEvent, closeEvent);
        }

        private static void RenderPreview(byte[] data, Graphics graphics)
        {
            int y, x;
            MemoryStream memoryStream;
            Yaz0Stream yaz0Stream;
            SzsArchive archive;
            Font previewFont;

            memoryStream = new MemoryStream(data);
            yaz0Stream = new Yaz0Stream(memoryStream, CompressionMode.Decompress);
            archive = null;
            previewFont = new Font("Microsoft Sans Serif", 8.25f);

            try
            {
                if (yaz0Stream.ReadHeader())
                    archive = new SzsArchive(yaz0Stream, "", false);
                else
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    archive = new SzsArchive(memoryStream, "", false);
                }

                x = y = 10;

                RenderPreviewNode(archive.Root, graphics, previewFont, x, ref y);
            }
            catch (Exception ex)
            {
                graphics.Clear(SystemColors.Control);
                graphics.DrawString(ex.Message, previewFont, SystemBrushes.ControlText, graphics.ClipBounds, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }
            finally
            {
                if (archive != null)
                    archive.Dispose();

                yaz0Stream.Close();
                memoryStream.Close();
            }
        }

        private static void RenderPreviewNode(ArchiveEntry archiveEntry, Graphics graphics, Font previewFont, int x, ref int y)
        {
            graphics.DrawImageUnscaled(Properties.Resources.folder, x, y);

            graphics.DrawString(archiveEntry.Name, previewFont, SystemBrushes.ControlText, x + 16, y);

            y += 16;

            foreach (ArchiveEntry item in archiveEntry.Children)
            {
                if (item.IsFolder)
                    RenderPreviewNode(item, graphics, previewFont, x + 20, ref y);

                if (y > graphics.ClipBounds.Height) return;
            }
        }
    }
}
