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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using Chadsoft.CTools.Szs.Archive;

namespace Chadsoft.CTools.Szs
{
    public class SzsExplorerInstance : EditorInstance, IDisposable
    {
        private bool disposed;
        private SzsArchive _archive;
        private string _filePath;
        private bool _compress;
        private Editor _editor;
        private Collection<EditorInstance> editors;
        private Collection<ArchiveEntry> locks;

        internal MainForm MainWindow
        {
            get;
            private set;
        }

        internal SzsArchive Archive
        {
            get
            {
                return _archive;
            }
            set
            {
                _archive = value;
                MainWindow.UpdateInterface();
            }
        }

        public override Editor Editor
        {
            get
            {
                return _editor;
            }
        }

        internal bool Loaded
        {
            get
            {
                return Archive != null;
            }
        }

        internal bool IsEditor
        {
            get
            {
                return Editor != null;
            }
        }

        public SzsExplorerInstance(string[] args)
            : base(null, null, null)
        {
            editors = new Collection<EditorInstance>();
            locks = new Collection<ArchiveEntry>();
            CreateForm();

            if (args.Length == 1)
            {
                OpenArchive(args[0]);
            }
        }

        public SzsExplorerInstance(byte[] data, string name, Editor editor, EventHandler<SaveEventArgs> saveEvent, EventHandler closeEvent)
            : base(data, saveEvent, closeEvent)
        {
            MemoryStream openStream;
            Yaz0Stream yaz0Stream;

            editors = new Collection<EditorInstance>();
            locks = new Collection<ArchiveEntry>();
            _editor = editor;
            
            CreateForm();
            openStream = null;
            yaz0Stream = null;

            try
            {
                openStream = new MemoryStream(data);
                yaz0Stream = new Yaz0Stream(openStream, CompressionMode.Decompress);
                _compress = yaz0Stream.ReadHeader();

                if (_compress)
                    OpenArchive(yaz0Stream, name);
                else
                {
                    openStream.Seek(0, SeekOrigin.Begin);
                    OpenArchive(openStream, name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorLoad", ex.Message), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            finally
            {
                if (openStream != null)
                    openStream.Close();
                if (yaz0Stream != null)
                    yaz0Stream.Close();
            }

            if (_archive == null)
                throw new Exception();

            if (!MainWindow.IsDisposed)
                MainWindow.Show();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                MainWindow.Dispose();
                if (_archive != null)
                    _archive.Dispose();

                disposed = true;
            }
        }

        public override bool CloseEditor()
        {
            return CloseFile();
        }

        private void CreateForm()
        {
            MainWindow = new MainForm(this);
        }

        internal void Run()
        {
            Application.Run(MainWindow);
        }

        internal void NewArchive()
        {
            if (IsEditor) return;

            if (!CloseFile())
                return;

            OpenArchive(new MemoryStream(Properties.Resources.FormatArchiveNew), "untitled.szs");
        }

        internal void OpenArchive()
        {          
            if (MainWindow.ArchiveOpenFileDialog.ShowDialog() == DialogResult.Cancel) return;

            OpenArchive(MainWindow.ArchiveOpenFileDialog.FileName);
        }

        internal void OpenArchive(string path)
        {
            Stream openStream;
            Yaz0Stream yaz0Stream;

            yaz0Stream = null;
            openStream = null;

            if (IsEditor) return;

            try
            {
                openStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                yaz0Stream = new Yaz0Stream(openStream, CompressionMode.Decompress);

                _compress = yaz0Stream.ReadHeader();
                _filePath = path;

                if (_compress)
                {
                    OpenArchive(yaz0Stream, Path.GetFileName(_filePath));
                }
                else
                {
                    openStream.Seek(0, SeekOrigin.Begin);
                    OpenArchive(openStream, Path.GetFileName(_filePath));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorLoad", ex.Message), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (openStream != null)
                    openStream.Close();
                if (yaz0Stream != null)
                    yaz0Stream.Close();
            }
        }

        internal void OpenArchive(Stream stream, string name)
        {
            if (!IsEditor && !CloseFile()) return;

            try
            {
                Archive = new SzsArchive(stream, name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorLoad", ex.Message), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                stream.Close();
            }
        }

        internal bool CloseFile()
        {
            DialogResult result;

            if (Archive != null)
            {
                if (editors.Count > 0)
                {
                    result = MessageBox.Show(Program.GetString("MessageErrorCloseLock"), MainWindow.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                        return false;

                    while (editors.Count > 0)
                    {
                        if (!editors[0].CloseEditor())
                            return false;
                    }
                }

                if (Archive.Changed)
                {
                    result = MessageBox.Show(Program.GetString("MessageFileClose", Archive.Name), MainWindow.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        if (!SaveFileAs())
                            return false;
                    }
                    else if (result == DialogResult.Cancel)
                        return false;
                }

                Archive.Dispose();
                Archive = null;
                _filePath = null;
            }

            if (IsEditor)
                if (!MainWindow.IsClosing)
                    MainWindow.Close();
                else
                    OnClose();

            return true;
        }

        internal bool SaveFileAs()
        {
            string oldPath, oldName; bool oldCompress;

            if (IsEditor) return SaveFile();

            try
            {
                if (MainWindow.ArchiveSaveFileDialog.ShowDialog() == DialogResult.Cancel) return false;

                oldPath = _filePath;
                oldCompress = _compress;
                oldName = Archive.Name;

                _filePath = MainWindow.ArchiveSaveFileDialog.FileName;
                _compress = MainWindow.ArchiveSaveFileDialog.FilterIndex != 2;
                Archive.Name = Path.GetFileName(_filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageSaveError", ex.Message), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!SaveFile())
            {
                _filePath = oldPath;
                _compress = oldCompress;
                Archive.Name = oldName;
                return false;
            }
            return true;
        }

        internal bool SaveFile()
        {
            if (IsEditor)
                return SaveFileEditor();
            else
                return SaveFileToFile();
        }

        private bool SaveFileEditor()
        {
            Stream stream; MemoryStream dataStream; Yaz0Stream compressStream;

            stream = null; dataStream = null; compressStream = null;
            try
            {
                if (_compress)
                {
                    stream = compressStream = new Yaz0Stream(dataStream = new MemoryStream(), CompressionMode.Compress);
                }
                else
                    stream = dataStream = new MemoryStream();

                Archive.Save(stream);

                stream.Flush();

                if (!OnSave(dataStream.ToArray()))
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorSave", ex.Message), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool SaveFileToFile()
        {
            Stream stream; FileStream fileStream; Yaz0Stream compressStream;

            stream = null; fileStream = null; compressStream = null;
            try
            {
                if (string.IsNullOrEmpty(_filePath))
                    return SaveFileAs();

                if (_compress)
                {
                    stream = compressStream = new Yaz0Stream(fileStream = new FileStream(_filePath, FileMode.Create), CompressionMode.Compress);
                }
                else
                    stream = fileStream = new FileStream(_filePath, FileMode.Create);

                Archive.Save(stream);

                stream.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorSave", ex.Message), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
                if (compressStream != null)
                    compressStream.Dispose();
            }

            return true;
        }

        internal void ExportFile(ArchiveEntry file)
        {
            string extension, filter;
            Stream fileStream;

            fileStream = null;
            try
            {
                if (string.IsNullOrEmpty(file.Name))
                {
                    extension = "";
                    MainWindow.FileSaveFileDialog.Filter = Program.GetString("FilterAllFiles");
                }
                else
                {
                    extension = file.Extension;

                    filter = Program.GetString("Filter" + extension.ToUpper());
                    if (string.IsNullOrEmpty(filter))
                        filter = extension + " files (*." + extension + ")|*." + extension;

                    MainWindow.FileSaveFileDialog.Filter = filter + "|" + Program.GetString("FilterAllFiles");
                }

                MainWindow.FileSaveFileDialog.FileName = file.Name;
                MainWindow.FileSaveFileDialog.Title = Program.GetString("TitleFileExport");

                if (MainWindow.FileSaveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

                fileStream = MainWindow.FileSaveFileDialog.OpenFile();
                fileStream.Write(file.Data, 0, file.Data.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorExport", ex.Message), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
        }

        internal void ExportFiles(ArchiveEntry[] roots)
        {
            string files;
            string[] errors;

            MainWindow.FileFolderBrowserDialog.Description = Program.GetString("DescriptionExportFolder");

            if (MainWindow.FileFolderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            errors = ExportAll(MainWindow.FileFolderBrowserDialog.SelectedPath, roots);

            if (errors.Length > 0)
            {
                if (errors.Length == 1)
                {
                    files = errors[0];

                    MessageBox.Show(Program.GetString("MessageErrorExportSingle", files), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    files = "";
                    for (int i = 0; i < errors.Length; i++)
                    {
                        files += errors[i] + "\n";
                    }

                    MessageBox.Show(Program.GetString("MessageErrorExportMultiple", files), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        internal ArchiveEntry AddFolder(ArchiveEntry parent)
        {
            ArchiveEntry newFolder;
            string name; int attempt;
            name = Program.GetString("NameFolderUntitled", "");
            attempt = 1;

            while (parent.ContainsChild(name))
            {
                name = Program.GetString("NameFolderUntitled", "(" + attempt++.ToString() + ")");
            }

            newFolder = new ArchiveEntry(name, parent);
            parent.Children.Add(newFolder);

            Archive.Changed = true;
            return newFolder;
        }

        internal void NewFile(ArchiveEntry folder)
        {
            NewFile newFile;
            NewFile[] files;
            NewFileForm form;
            ArchiveEntry entry;

            files = ToolManager.NewFiles.ToArray();
            form = new NewFileForm(files);

            if (form.ShowDialog() == DialogResult.Cancel) return;

            newFile = form.SelectedFile;
            entry = new ArchiveEntry(GetUniqueName(newFile.DefaultName, folder), folder, newFile.GetData());

            folder.Children.Add(entry);
            Archive.Changed = true;
        }

        internal string[] ExportAll(string path, ArchiveEntry[] items)
        {
            List<string> fails;
            Stream fileStream;
            DirectoryInfo info;

            info = new DirectoryInfo(path);
            fails = new List<string>();

            if (!info.Exists)
                info.Create();

            foreach (ArchiveEntry item in items)
            {
                if (item.IsFolder)
                {
                    fails.AddRange(ExportAll(path + "\\" + item.Name, item.Children.ToArray()));
                }
                else
                {
                    fileStream = null;

                    try
                    {
                        fileStream = new FileStream(path + "\\" + item.Name, FileMode.Create);
                        fileStream.Write(item.Data, 0, item.Data.Length);
                    }
                    catch (Exception)
                    {
                        fails.Add(item.FullPath);
                    }
                    finally
                    {
                        if (fileStream != null)
                            fileStream.Close();
                    }
                }
            }

            return fails.ToArray();
        }

        internal void Delete(ArchiveEntry[] items)
        {
            string error;
            ArchiveEntry[] deletes;

            if (items.Length > 1)
            {
                if (MessageBox.Show(Program.GetString("MessageDeleteFiles"), MainWindow.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
            }
            else
                if (MessageBox.Show(Program.GetString("MessageDeleteFile"), MainWindow.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;


            deletes = RemoveLocks(items);

            foreach (ArchiveEntry item in deletes)
            {
                item.Parent.Children.Remove(item);
            }

            if (deletes.Length != items.Length)
            {
                deletes = GetLocks(items);

                error = "";
                for (int i = 0; i < deletes.Length; i++)
                {
                    error += deletes[i].FullPath + "\n";
                }
                error = error.Remove(error.Length - 1);

                if (deletes.Length == 1)
                    MessageBox.Show(Program.GetString("MessageErrorLock", error), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show(Program.GetString("MessageErrorLocks", error), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Archive.Changed = true;
        }

        internal string[] ImportDirectory(string directory, ArchiveEntry parent)
        {
            DirectoryInfo info;
            string[] files;
            FileInfo[] fileInfos;
            List<string> fails;

            fails = new List<string>();
            info = new DirectoryInfo(directory);

            fileInfos = info.GetFiles();
            files = new string[fileInfos.Length];

            for (int i = 0; i < fileInfos.Length; i++)
            {
                files[i] = fileInfos[i].FullName;
            }

            fails.AddRange(ImportFiles(files, parent));

            foreach (DirectoryInfo dir in info.GetDirectories())
            {
                fails.AddRange(ImportDirectory(dir.FullName, parent.GetDirectory(dir.Name)));
            }

            Archive.Changed = true;
            return fails.ToArray();
        }

        internal void ImportFiles(ArchiveEntry parent)
        {
            string[] errors;
            string errorString;

            try
            {
                MainWindow.FileOpenFileDialog.Filter = Program.GetString("FilterAllFiles");
                MainWindow.FileOpenFileDialog.FileName = "";
                MainWindow.FileOpenFileDialog.Title = Program.GetString("TitleFileReplace");
                MainWindow.FileOpenFileDialog.Multiselect = true;

                if (MainWindow.FileOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

                errors = ImportFiles(MainWindow.FileOpenFileDialog.FileNames, parent);

                if (errors.Length > 0)
                {
                    errorString = "";
                    foreach (string item in errors)
                    {
                        errorString += item + '\n';
                    }

                    if (errors.Length == 1)
                    {
                        MessageBox.Show(Program.GetString("MessageErrorImportMultiple", errorString), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show(Program.GetString("MessageErrorImportSingle", errorString), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorImport", ex.Message), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Archive.Changed = true;
        }

        internal void ImportFolder(ArchiveEntry parent)
        {
            string[] errors;
            string errorString;

            try
            {
                MainWindow.FileFolderBrowserDialog.ShowNewFolderButton = false;
                MainWindow.FileFolderBrowserDialog.Description = Program.GetString("DescriptionImportFolder");

                if (MainWindow.FileFolderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

                errors = ImportDirectory(MainWindow.FileFolderBrowserDialog.SelectedPath, parent);

                if (errors.Length > 0)
                {
                    errorString = "";
                    foreach (string item in errors)
                    {
                        errorString += item + '\n';
                    }

                    if (errors.Length == 1)
                    {
                        MessageBox.Show(Program.GetString("MessageErrorImportMultiple", errorString), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show(Program.GetString("MessageErrorImportSingle", errorString), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorImport", ex.Message), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Archive.Changed = true;
        }

        internal string[] ImportFiles(string[] files, ArchiveEntry parent)
        {
            ArchiveEntry entry;
            FileStream stream;
            List<string> fails;
            byte[] data;

            fails = new List<string>();

            foreach (string file in files)
            {
                stream = null;
                try
                {
                    stream = new FileStream(file, FileMode.Open);

                    if (stream.Length > Int32.MaxValue)
                        throw new InsufficientMemoryException();

                    data = new byte[stream.Length];

                    stream.Read(data, 0, data.Length);
                    stream.Close();
                    stream = null;

                    entry = new ArchiveEntry(GetUniqueName(Path.GetFileName(file), parent), parent, data);
                    parent.Children.Add(entry);
                }
                catch (Exception)
                {
                    fails.Add(file);
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                }
            }

            Archive.Changed = true;
            return fails.ToArray();
        }

        internal void ReplaceFile(ArchiveEntry file)
        {
            string extension, filter;
            Stream fileStream;

            fileStream = null;

            if (IsLocked(file))
            {
                MessageBox.Show(Program.GetString("MessageErrorFileLock"), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (string.IsNullOrEmpty(file.Name))
                {
                    extension = "";
                    MainWindow.FileOpenFileDialog.Filter = Program.GetString("FilterAllFiles");
                }
                else
                {
                    extension = file.Extension;
                    filter = Program.GetString("Filter" + extension.ToUpper());
                    if (string.IsNullOrEmpty(filter))
                        filter = extension + " files (*." + extension + ")|*." + extension;

                    MainWindow.FileOpenFileDialog.Filter = filter + "|" + Program.GetString("FilterAllFiles");
                }

                MainWindow.FileOpenFileDialog.FileName = file.Name;
                MainWindow.FileOpenFileDialog.Title = Program.GetString("TitleFileReplace");
                MainWindow.FileOpenFileDialog.Multiselect = false;

                if (MainWindow.FileOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

                fileStream = MainWindow.FileOpenFileDialog.OpenFile();

                if (fileStream.Length > Int32.MaxValue)
                    throw new InsufficientMemoryException();

                file.Data = new byte[fileStream.Length];
                file.FileLength = file.Data.Length;
                fileStream.Read(file.Data, 0, file.Data.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorReplace", ex.Message), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
            Archive.Changed = true;
        }

        internal void OpenExternal(ArchiveEntry item)
        {
            FileInfo outputFile;
            FileStream fileStream;

            fileStream = null;

            try
            {
                outputFile = new FileInfo(Path.ChangeExtension(Path.GetTempFileName(), item.Extension));
                fileStream = outputFile.OpenWrite();

                fileStream.Write(item.Data, 0, item.Data.Length);

                Process.Start(outputFile.FullName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorOpenExternal", ex.Message), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
        }

        internal void MoveItems(ArchiveEntry[] items, ArchiveEntry newFolder)
        {
            string error;
            ArchiveEntry[] deletes;

            deletes = RemoveLocks(items);
            Delete(deletes);
            CopyItems(deletes, newFolder);

            if (deletes.Length != items.Length)
            {
                deletes = GetLocks(items);

                error = "";
                for (int i = 0; i < deletes.Length; i++)
                {
                    error += deletes[i].FullPath + "\n";
                }
                error = error.Remove(error.Length - 1);

                if (deletes.Length == 1)
                    MessageBox.Show(Program.GetString("MessageErrorLock", error), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show(Program.GetString("MessageErrorLocks", error), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Archive.Changed = true;
        }

        internal void CopyItems(ArchiveEntry[] items, ArchiveEntry newFolder)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].IsFolder)
                    CopyItems(items[i].Children.ToArray(), newFolder.GetDirectory(items[i].Name));
                else
                    newFolder.Children.Add(new ArchiveEntry(GetUniqueName(items[i].Name, newFolder), newFolder, items[i].Data));
            }
            Archive.Changed = true;
        }

        internal string GetUniqueName(string name, ArchiveEntry folder)
        {
            string start, extension;
            int attempt;

            attempt = 1;
            if (name.Contains("."))
            {
                extension = name.Substring(name.LastIndexOf("."));
                start = name.Remove(name.Length - extension.Length);
            }
            else
            {
                extension = "";
                start = name;
            }

            while (folder.ContainsChild(name))
            {
                name = start + " (" + attempt++.ToString() + ")" + extension;
            }

            return name;
        }

        internal void OpenFile(ArchiveEntry archiveEntry, Editor editor)
        {
            EditorInstance editorInstance;

            if (IsLocked(archiveEntry))
            {
                MessageBox.Show(Program.GetString("MessageErrorFileLock"), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                editorInstance = editor.CreateInstance(archiveEntry.Data, archiveEntry.Name, editorInstance_Save, editorInstance_Closed);
                editors.Add(editorInstance);
                locks.Add(archiveEntry);
            }
            catch (Exception)
            {
                
            }
        }

        private ArchiveEntry[] RemoveLocks(ArchiveEntry[] entries)
        {
            List<ArchiveEntry> entryList;

            entryList = new List<ArchiveEntry>();

            foreach (ArchiveEntry item in entries)
            {
                if (!IsLocked(item))
                    entryList.Add(item);
            }

            return entryList.ToArray();
        }

        private ArchiveEntry[] GetLocks(ArchiveEntry[] entries)
        {
            List<ArchiveEntry> entryList;

            entryList = new List<ArchiveEntry>();

            foreach (ArchiveEntry item in entries)
            {
                if (item.IsFolder)
                    entryList.AddRange(item.Children.ToArray());
                else if (locks.Contains(item))
                    entryList.Add(item);
            }

            return entryList.ToArray();
        }

        private bool IsLocked(ArchiveEntry[] entries)
        {
            foreach (ArchiveEntry item in entries)
            {
                if (IsLocked(item))
                    return true;
            }

            return false;
        }

        private bool IsLocked(ArchiveEntry entry)
        {
            if (entry.IsFolder)
                return IsLocked(entry.Children.ToArray());
            else
                return locks.Contains(entry);
        }

        private void editorInstance_Save(object sender, SaveEventArgs e)
        {
            int file;
            EditorInstance editorInstance;

            editorInstance = (EditorInstance)sender;
            file = editors.IndexOf(editorInstance);

            locks[file].Data = editorInstance.Data;
            locks[file].FileLength = editorInstance.Data.Length;

            e.Success = true;
            MainWindow.UpdateInterface();
        }

        private void editorInstance_Closed(object sender, EventArgs e)
        {
            int file;
            EditorInstance editorInstance;

            editorInstance = (EditorInstance)sender;
            file = editors.IndexOf(editorInstance);

            editors.RemoveAt(file);
            locks.RemoveAt(file);
        }
    }
}
