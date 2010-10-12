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
using System.Drawing;

namespace Chadsoft.CTools.Bmg
{
    public static class ToolInfo
    {
        private static Tool tool;
        private static Editor bmgTool;

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
                "BMG Editor",
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
                    new FileFormat(Program.GetString("FormatNameBmg"), Program.GetString("FormatDescriptionBmg"), Program.GetString("FormatCategoryData"), Properties.Resources.ApplicationIcon, BmgFormatMatch),
                });
        }

        private static ReadOnlyCollection<Editor> GetEditors(ReadOnlyCollection<FileFormat> formats)
        {
            return new ReadOnlyCollection<Editor>(new Editor[]
                {
                    bmgTool = new Editor("BMG Editor", Program.GetString("AssemblyDescription"), "Chadderz", new Version(1,0,0,0), Properties.Resources.ApplicationIcon, formats, CreateInstance, RenderPreview),
                });
        }

        private static ReadOnlyCollection<NewFile> GetNewFiles(ReadOnlyCollection<FileFormat> formats)
        {
            return new ReadOnlyCollection<NewFile>(new NewFile[]
                {
                    new NewFile(Program.GetString("FormatNameBmg"), "Untitled.bmg", Program.GetString("FormatDescriptionBmg"), Properties.Resources.ApplicationIcon,  formats[0], Properties.Resources.FormatNewBmg),
                    new NewFile(Program.GetString("FormatNameOldBmg"), "Untitled.bmg", Program.GetString("FormatDescriptionOldBmg"), Properties.Resources.ApplicationIcon,  formats[0], Properties.Resources.FormatNewOldBmg),
                });
        }

        private static int BmgFormatMatch(string name, byte[] data, int offset)
        {
            if (data.Length >= 4 && data[0] == 0x4D && data[1] == 0x45 && data[2] == 0x53 && data[3] == 0x47)
                return 100;
            else
                return 0;
        }

        private static EditorInstance CreateInstance(byte[] data, string name, EventHandler<SaveEventArgs> saveEvent, EventHandler closeEvent)
        {
            return new BmgToolInstance(data, name, bmgTool, saveEvent, closeEvent);
        }

        private static void RenderPreview(byte[] data, Graphics graphics)
        {
            int y, x;
            Font previewFont;
            BmgFile bmg;

            previewFont = new Font("Microsoft Sans Serif", 8.25f);
            bmg = null;

            try
            {
                bmg = new BmgFile(data);
                x = y = 10;

                for (int i = 0; i < bmg.Messages.Count && y < graphics.ClipBounds.Height; i++)
                {
                    graphics.DrawString(bmg.Messages[i].Message, previewFont, SystemBrushes.ControlText, x, y);
                    y += 16;
                }
            }
            catch (Exception ex)
            {
                graphics.Clear(SystemColors.Control);
                graphics.DrawString(ex.Message, previewFont, SystemBrushes.ControlText, graphics.ClipBounds, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

            }
            finally
            {
                if (bmg != null)
                    bmg.Dispose();
            }
            
        }
    
    }
}
