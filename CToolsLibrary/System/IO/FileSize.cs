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

using Chadsoft.CTools.Properties;

namespace System.IO
{
    public static class FileSize
    {
        public static string[] SIExtensions = new string[] { " B", " KB", " MB", " GB", " TB" };
        public static string[] BinaryExtensions = new string[] { " B", " KiB", " MiB", " GiB", " TiB" };

        public const int SIStep = 1000;
        public const int BinaryStep = 1024;

        private static string[] Extensions
        {
            get
            {
                if (Settings.Default.binaryUnits)
                    return BinaryExtensions;
                else
                    return SIExtensions;
            }
        }

        private static int Step
        {
            get
            {
                if (Settings.Default.binaryUnits)
                    return BinaryStep;
                else
                    return SIStep;
            }
        }

        public static string FormatSize(int size)
        {
            double fileSize;
            int extension;

            fileSize = size;
            extension = 0;

            while (fileSize > Step / 2)
            {
                fileSize /= Step;
                extension++;
            }

            if (extension > 0)
                return fileSize.ToString("#0.00") + Extensions[extension];
            else
                return fileSize.ToString() + Extensions[extension];
        }
    }
}
