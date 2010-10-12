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

using System;
using System.Collections.ObjectModel;
using System.Drawing;
using Chadsoft.CTools.HexEditor;
using Chadsoft.CTools.Properties;

namespace Chadsoft.CTools
{
    public static class ToolInfo
    {
        private static Tool tool;
        private static Editor binaryEditor;

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
                "CToolsLibrary",
                ResourceSet.AssemblyDescription,
                "Chadderz", 
                new Version(1,0,4,0),
                ResourceSet.AssemblyImage,
                GetEditors(formats),
                formats,
                GetNewFiles(formats)
                );
        }

        private static ReadOnlyCollection<FileFormat> GetFormats()
        {
            return new ReadOnlyCollection<FileFormat>(new FileFormat[]
                {
                    new FileFormat(ResourceSet.FormatNameUnknown, ResourceSet.FormatDescriptionUnknown, ResourceSet.FormatCategoryData, ResourceSet.FormatImageUnknown, UnknownFormatMatch),
                    new DummyFileFormat(ResourceSet.FormatNameBinary, ResourceSet.FormatDescriptionBinary, ResourceSet.FormatCategoryData, ResourceSet.FormatImageBinary, new string[] { ".bin", ".dat" }),
                    new DummyFileFormat(ResourceSet.FormatNameBlight, ResourceSet.FormatDescriptionBlight, ResourceSet.FormatCategoryLighting, ResourceSet.FormatImageBlight, new string[] { ".blight" }),
                    new DummyFileFormat(ResourceSet.FormatNameBlmap, ResourceSet.FormatDescriptionBlmap, ResourceSet.FormatCategoryLighting, ResourceSet.FormatImageBlmap, new string[] { ".blmap" }),
                    new DummyFileFormat(ResourceSet.FormatNameBrasd, ResourceSet.FormatDescriptionBrasd, ResourceSet.FormatCategoryAnimations, ResourceSet.FormatImageBrasd, new string[] { ".brasd" }),
                    new DummyFileFormat(ResourceSet.FormatNameBrctr, ResourceSet.FormatDescriptionBrctr, ResourceSet.FormatCategoryData, ResourceSet.FormatImageBrctr, new string[] { ".brctr" }),
                    new DummyFileFormat(ResourceSet.FormatNameBreff, ResourceSet.FormatDescriptionBreff, ResourceSet.FormatCategoryEffects, ResourceSet.FormatImageBreff, new string[] { ".breff" }),
                    new DummyFileFormat(ResourceSet.FormatNameBreft, ResourceSet.FormatDescriptionBreft, ResourceSet.FormatCategoryEffects, ResourceSet.FormatImageBreft, new string[] { ".breft" }),
                    new DummyFileFormat(ResourceSet.FormatNameBrfnt, ResourceSet.FormatDescriptionBrfnt, ResourceSet.FormatCategoryFonts, ResourceSet.FormatImageBrfnt, new string[] { ".brfnt" }),
                    new DummyFileFormat(ResourceSet.FormatNameBrlan, ResourceSet.FormatDescriptionBrlan, ResourceSet.FormatCategoryAnimations, ResourceSet.FormatImageBrlan, new string[] { ".brlan" }),
                    new DummyFileFormat(ResourceSet.FormatNameBrlyt, ResourceSet.FormatDescriptionBrlyt, ResourceSet.FormatCategoryLayouts, ResourceSet.FormatImageBrlyt, new string[] { ".brlyt" }),
                    new DummyFileFormat(ResourceSet.FormatNameBrres, ResourceSet.FormatDescriptionBrres, ResourceSet.FormatCategoryModels, ResourceSet.FormatImageBrres, new string[] { ".brres" }),
                    new DummyFileFormat(ResourceSet.FormatNameBrstm, ResourceSet.FormatDescriptionBrstm, ResourceSet.FormatCategoryMusic, ResourceSet.FormatImageSound, new string[] { ".brstm" }),
                    new DummyFileFormat(ResourceSet.FormatNameBwav, ResourceSet.FormatDescriptionBwav, ResourceSet.FormatCategoryMusic, ResourceSet.FormatImageSound, new string[] { ".bwav" }),
                    new DummyFileFormat(ResourceSet.FormatNameMid, ResourceSet.FormatDescriptionMid, ResourceSet.FormatCategoryMusic, ResourceSet.FormatImageSound, new string[] { ".mid", ".midi" }),
                    new DummyFileFormat(ResourceSet.FormatNameBti, ResourceSet.FormatDescriptionBti, ResourceSet.FormatCategoryImages, ResourceSet.FormatImageBti, new string[] { ".bti", ".btienv", ".btimat" }),
                    new DummyFileFormat(ResourceSet.FormatNameCsv, ResourceSet.FormatDescriptionCsv, ResourceSet.FormatCategoryData, ResourceSet.FormatImageBinary, new string[] { ".csv" }),
                    new DummyFileFormat(ResourceSet.FormatNameKcl, ResourceSet.FormatDescriptionKcl, ResourceSet.FormatCategoryData, ResourceSet.FormatImageKcl, new string[] { ".kcl" }),
                    new DummyFileFormat(ResourceSet.FormatNameKmp, ResourceSet.FormatDescriptionKmp, ResourceSet.FormatCategoryData, ResourceSet.FormatImageKmp, new string[] { ".kmp" }),
                    new DummyFileFormat(ResourceSet.FormatNameRsca, ResourceSet.FormatDescriptionRsca, ResourceSet.FormatCategoryAnimations, ResourceSet.FormatImageRsca, new string[] { ".rsca" }),
                    new DummyFileFormat(ResourceSet.FormatNameThp, ResourceSet.FormatDescriptionThp, ResourceSet.FormatCategoryVideo, ResourceSet.FormatImageThp, new string[] { ".thp" }),
                    new DummyFileFormat(ResourceSet.FormatNameTpl, ResourceSet.FormatDescriptionTpl, ResourceSet.FormatCategoryImages, ResourceSet.FormatImageTpl, new string[] { ".tpl" }),
                    new DummyFileFormat(ResourceSet.FormatNameTxt, ResourceSet.FormatDescriptionTxt, ResourceSet.FormatCategoryData, ResourceSet.FormatImageBinary, new string[] { ".txt" }),
                });
        }

        private static ReadOnlyCollection<Editor> GetEditors(ReadOnlyCollection<FileFormat> formats)
        {
            return new ReadOnlyCollection<Editor>(new Editor[]
                {
                    binaryEditor = new Editor(ResourceSet.EditorNameBinary, ResourceSet.EditorDescriptionBinary, "Chadderz", new Version(1,0,0,0), ResourceSet.FormatImageBinary, new ReadOnlyCollection<FileFormat>(new FileFormat[] { formats[0],formats[1]}), CreateInstanceBinary, GeneratePreviewBinary),
                });
        }

        private static ReadOnlyCollection<NewFile> GetNewFiles(ReadOnlyCollection<FileFormat> formats)
        {
            return new ReadOnlyCollection<NewFile>(new NewFile[]
                {
                    new NewFile(ResourceSet.FileNameEmpty, "file", ResourceSet.FileDescriptionEmpty, ResourceSet.FileImageEmpty,  formats[0], new byte[0]),
                });
        }

        private static int UnknownFormatMatch(string name, byte[] data, int offset)
        {
            return 1;
        }

        private static EditorInstance CreateInstanceBinary(byte[] data, string name, EventHandler<SaveEventArgs> saveEvent, EventHandler closeEvent)
        {
            return new HexEditorInstance(data, name, saveEvent, closeEvent, binaryEditor);
        }

        private static void GeneratePreviewBinary(byte[] data, Graphics graphics)
        {
            int index, x, y;
            char c;
            Font font;

            index = 0;
            y = 10;
            font = new Font("Courier New", 10);

            for (int i = 0; i < graphics.ClipBounds.Height; i += 16, y += 16)
            {
                graphics.DrawString(index.ToString("X8"), font, SystemBrushes.ControlText, 10, y);
                x = 90;

                for (int j = 0; j < 16; j++, index++, x += 25)
                {
                    if (index < data.Length)
                    {
                        graphics.DrawString(data[index].ToString("X2"), font, SystemBrushes.ControlText, x, y);
                    }

                    if (j == 7)
                        x += 10;
                }

                x += 20; index -= 16;
                for (int j = 0; j < 16; j++, index++, x += 11)
                {
                    if (index < data.Length)
                    {
                        c = Convert.ToChar(data[index]);

                        if (char.IsControl(c))
                            c = '.';

                        graphics.DrawString(c.ToString(), font, SystemBrushes.ControlText, x, y);
                    }

                    if (j == 7)
                        x += 5;
                }

            }
        }
    }
}
