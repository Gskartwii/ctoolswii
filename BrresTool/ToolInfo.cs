// CTools brres tool - Model editing service for CTools
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

namespace Chadsoft.CTools.Brres
{
    public static class ToolInfo
    {
        private static Tool _tool;
        private static Editor brresTool;

        public static Tool Tool
        {
            get
            {
                if (_tool == null)
                    SetupTool();
                return _tool;
            }
        }

        private static void SetupTool()
        {
            ReadOnlyCollection<FileFormat> formats;

            formats = GetFormats();
            _tool = new Tool(
                "BRRES Editor",
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
                    new FileFormat(Program.GetString("FormatNameBrres"), Program.GetString("FormatDescriptionBrres"), Program.GetString("FormatCategoryModels"), Properties.Resources.ApplicationIcon, BmgFormatMatch),
                });
        }

        private static ReadOnlyCollection<Editor> GetEditors(ReadOnlyCollection<FileFormat> formats)
        {
            return new ReadOnlyCollection<Editor>(new Editor[]
                {
                    brresTool = new Editor("BRRES Editor", Program.GetString("AssemblyDescription"), "Chadderz", new Version(1,0,0,0), Properties.Resources.ApplicationIcon, formats, CreateInstance, RenderPreview),
                });
        }

        private static ReadOnlyCollection<NewFile> GetNewFiles(ReadOnlyCollection<FileFormat> formats)
        {
            return new ReadOnlyCollection<NewFile>(new NewFile[]
                {
                    new NewFile(Program.GetString("FormatNameBrres"), "Untitled.brres", Program.GetString("FormatDescriptionBrres"), Properties.Resources.ApplicationIcon,  formats[0], null), // Properties.Resources.FormatNewBrres),
                });
        }

        private static int BmgFormatMatch(string name, byte[] data, int offset)
        {
            if (data.Length >= offset + 4 && data[offset + 0] == 0x62 && data[offset + 1] == 0x72 && data[offset + 2] == 0x65 && data[offset + 3] == 0x73)
                return 100;
            else
                return 0;
        }

        private static EditorInstance CreateInstance(byte[] data, string name, EventHandler<SaveEventArgs> saveEvent, EventHandler closeEvent)
        {
            return new BrresToolInstance(data, name, brresTool, saveEvent, closeEvent);
        }

        private static void RenderPreview(byte[] data, Graphics graphics)
        {
            // TBD
        }
    
    }
}
