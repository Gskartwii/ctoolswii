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

using System.ComponentModel;
using System.IO;

namespace Chadsoft.CTools.Szs.Archive
{
    public class ArchiveInfo
    {
        int _size;

        [LocalizedDisplayName("ArchiveInfoSizeName")]
        [LocalizedDescription("ArchiveInfoSizeDescription")]
        [LocalizedCategory("CategoryData")]
        public string Size { get; private set; }
        [LocalizedDisplayName("ArchiveInfoSubFileCountName")]
        [LocalizedDescription("ArchiveInfoSubFileCountDescription")]
        [LocalizedCategory("CategoryData")]
        public int SubFileCount { get; private set; }
        [LocalizedDisplayName("ArchiveInfoSubFolderCountName")]
        [LocalizedDescription("ArchiveInfoSubFolderCountDescription")]
        [LocalizedCategory("CategoryData")]
        public int SubFolderCount { get; private set; }
        [LocalizedDisplayName("ArchiveInfoTypeName")]
        [LocalizedDescription("ArchiveInfoTypeDescription")]
        [LocalizedCategory("CategoryData")]
        public string ArchiveType { get; private set; }

        public ArchiveInfo(ArchiveEntry root)
        {
            Add(root);
            ArchiveType = Program.GetString("ArchiveTypeU8");
        }

        private void Add(ArchiveEntry entry)
        {
            if (entry.IsFolder)
            {
                SubFolderCount++;

                foreach (ArchiveEntry child in entry.Children)
                {
                    if (child.IsFolder)
                    {
                        SubFolderCount++;
                        EnumerateChildren(child);
                    }
                    else
                    {
                        SubFileCount++;
                        _size += child.FileLength;
                    }
                }
            }
            else
            {
                SubFileCount++;

                _size += entry.FileLength;
            }

            Size = FileSize.FormatSize(_size);
        }

        private void EnumerateChildren(ArchiveEntry entry)
        {
            foreach (ArchiveEntry child in entry.Children)
            {
                if (child.IsFolder)
                {
                    SubFolderCount++;
                    EnumerateChildren(child);
                }
                else
                {
                    SubFileCount++;
                    _size += child.FileLength;
                }
            }
        }
    }
    public class ArchiveFileInfo
    {
        ArchiveEntry _entry;

        [LocalizedDisplayName("ArchiveFileInfoNameName")]
        [LocalizedDescription("ArchiveFileInfoNameDescription")]
        [LocalizedCategory("CategoryAppearance")]
        public string Name { get { return _entry.Name; } set { if (!string.IsNullOrWhiteSpace(value) && !_entry.Parent.ContainsChild(value)) _entry.Name = value; } }
        [LocalizedDisplayName("ArchiveFileInfoSizeName")]
        [LocalizedDescription("ArchiveFileInfoSizeDescription")]
        [LocalizedCategory("CategoryData")]
        public string Size { get; private set; }
        [LocalizedDisplayName("ArchiveFileInfoFileOffsetName")]
        [LocalizedDescription("ArchiveFileInfoFileOffsetDescription")]
        [LocalizedCategory("CategoryData")]
        public string FileOffset { get; private set; }
        [LocalizedDisplayName("ArchiveFileInfoFormatName")]
        [LocalizedDescription("ArchiveFileInfoFormatDescription")]
        [LocalizedCategory("CategoryData")]
        public string Format { get; private set; }
        [LocalizedDisplayName("ArchiveFileInfoFullPathName")]
        [LocalizedDescription("ArchiveFileInfoFullPathDescription")]
        [LocalizedCategory("CategoryAppearance")]
        public string FullPath { get; private set; }

        public ArchiveFileInfo(ArchiveEntry entry)
        {
            _entry = entry;
            Size = FileSize.FormatSize(entry.FileLength);
            FileOffset = "0x" + entry.FileOffset.ToString("X8");
            Format = ToolManager.GetFormat(entry.Name, entry.Data, 0).Description;
            FullPath = entry.FullPath;
        }
    }
    public class ArchiveDirectoryInfo
    {
        ArchiveEntry _entry;
        int _size;
        
        [LocalizedDisplayName("ArchiveDirectoryInfoNameName")]
        [LocalizedDescription("ArchiveDirectoryInfoNameDescription")]
        [LocalizedCategory("CategoryAppearance")]
        public string Name { get { return _entry.Name; } set { if (!_entry.IsRoot && !string.IsNullOrWhiteSpace(value) && !_entry.Parent.ContainsChild(value)) _entry.Name = value; } }
        [LocalizedDisplayName("ArchiveDirectoryInfoSizeName")]
        [LocalizedDescription("ArchiveDirectoryInfoSizeDescription")]
        [LocalizedCategory("CategoryData")]
        public string Size { get; private set; }
        [LocalizedDisplayName("ArchiveDirectoryInfoFileCountName")]
        [LocalizedDescription("ArchiveDirectoryInfoFileCountDescription")]
        [LocalizedCategory("CategoryData")]
        public int FileCount { get; private set; }
        [LocalizedDisplayName("ArchiveDirectoryInfoFolderCountName")]
        [LocalizedDescription("ArchiveDirectoryInfoFolderCountDescription")]
        [LocalizedCategory("CategoryData")]
        public int FolderCount { get; private set; }
        [LocalizedDisplayName("ArchiveDirectoryInfoSubFileCountName")]
        [LocalizedDescription("ArchiveDirectoryInfoSubFileCountDescription")]
        [LocalizedCategory("CategoryData")]
        public int SubFileCount { get; private set; }
        [LocalizedDisplayName("ArchiveDirectoryInfoSubFolderCountName")]
        [LocalizedDescription("ArchiveDirectoryInfoSubFolderCountDescription")]
        [LocalizedCategory("CategoryData")]
        public int SubFolderCount { get; private set; }
        [LocalizedDisplayName("ArchiveDirectoryInfoFullPathName")]
        [LocalizedDescription("ArchiveDirectoryInfoFullPathDescription")]
        [LocalizedCategory("CategoryAppearance")]
        public string FullPath { get; private set; }

        public ArchiveDirectoryInfo(ArchiveEntry entry)
        {
            _entry = entry;
            FullPath = entry.FullPath;

            foreach (ArchiveEntry child in entry.Children)
            {
                if (child.IsFolder)
                {
                    SubFolderCount++;
                    FolderCount++;
                    EnumerateChildren(child);
                }
                else
                {
                    SubFileCount++;
                    FileCount++;
                    _size += child.FileLength;
                }
            }


            Size = FileSize.FormatSize(_size);
        }

        private void EnumerateChildren(ArchiveEntry entry)
        {
            foreach (ArchiveEntry child in entry.Children)
            {
                if (child.IsFolder)
                {
                    FolderCount++;
                    EnumerateChildren(child);
                }
                else
                {
                    FileCount++;
                    _size += child.FileLength;
                }
            }
        }
    }
    public class ArchiveMultipleInfo
    {
        int _size;

        [LocalizedDisplayName("ArchiveMultipleInfoSizeName")]
        [LocalizedDescription("ArchiveMultipleInfoSizeDescription")]
        [LocalizedCategory("CategoryData")]
        public string Size { get; private set; }
        [LocalizedDisplayName("ArchiveMultipleInfoSubFileCountName")]
        [LocalizedDescription("ArchiveMultipleInfoSubFileCountDescription")]
        [LocalizedCategory("CategoryData")]
        public int SubFileCount { get; private set; }
        [LocalizedDisplayName("ArchiveMultipleInfoSubFolderCountName")]
        [LocalizedDescription("ArchiveMultipleInfoSubFolderCountDescription")]
        [LocalizedCategory("CategoryData")]
        public int SubFolderCount { get; private set; }
        [LocalizedDisplayName("ArchiveMultipleInfoFileCountName")]
        [LocalizedDescription("ArchiveMultipleInfoFileCountDescription")]
        [LocalizedCategory("CategoryData")]
        public int FileCount { get; private set; }
        [LocalizedDisplayName("ArchiveMultipleInfoFolderCountName")]
        [LocalizedDescription("ArchiveMultipleInfoFolderCountDescription")]
        [LocalizedCategory("CategoryData")]
        public int FolderCount { get; private set; }

        public void Add(ArchiveEntry entry)
        {
            if (entry.IsFolder)
            {
                FolderCount++;
                SubFolderCount++;

                foreach (ArchiveEntry child in entry.Children)
                {
                    if (child.IsFolder)
                    {
                        SubFolderCount++;
                        EnumerateChildren(child);
                    }
                    else
                    {
                        SubFileCount++;
                        _size += child.FileLength;
                    }
                }
            }
            else
            {
                FileCount++;
                SubFileCount++;

                _size += entry.FileLength;
            }

            Size = FileSize.FormatSize(_size);
        }

        private void EnumerateChildren(ArchiveEntry entry)
        {
            foreach (ArchiveEntry child in entry.Children)
            {
                if (child.IsFolder)
                {
                    SubFolderCount++;
                    EnumerateChildren(child);
                }
                else
                {
                    SubFileCount++;
                    _size += child.FileLength;
                }
            }
        }
    }
}
