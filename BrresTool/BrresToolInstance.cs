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
using System.IO;
using System.Windows.Forms;

namespace Chadsoft.CTools.Brres
{
    public class BrresToolInstance : EditorInstance, IDisposable
    {
        private bool disposed;
        private Editor _editor;
        private string path;

        internal BrresFile Brres { get; set; }
        internal FormMain MainWindow { get; private set; }
        internal bool IsEditor { get { return _editor != null; } }
        internal bool Loaded { get { return Brres != null; } }
        public override Editor Editor
        {
            get { return _editor; }
        }

        public BrresToolInstance(string[] args)
            : base(null, null, null)
        {
            SetupForm();
        }

        public BrresToolInstance(byte[] data, string name, Editor editor, EventHandler<SaveEventArgs> saveEvent, EventHandler closeEvent)
            : base(data, saveEvent, closeEvent)
        {
            _editor = editor;

            SetupForm();
            Open(new MemoryStream(data), name);
            Brres.Name = name;

            MainWindow.UpdateInterface();
        }

        ~BrresToolInstance()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (MainWindow != null)
                        MainWindow.Dispose();
                    if (Loaded)
                        Brres.Dispose();
                }

                MainWindow = null;
                Brres = null;

                disposed = true;
            }
        }

        public override bool CloseEditor()
        {
            if (!Close()) return false;

            MainWindow.Close();

            return true;
        }

        private void SetupForm()
        {
            MainWindow = new FormMain(this);
            MainWindow.Show();
        }

        internal bool Close()
        {
            DialogResult result;

            if (Loaded)
            {
                if (Brres.Changed)
                {
                    result = MessageBox.Show(Program.GetString("MessageFileClose", Brres.Name), MainWindow.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                    if (result == DialogResult.Cancel) 
                        return false;
                    else if (result == DialogResult.Yes)
                    {
                        if (!SaveAs()) return false;
                    }
                    else
                        Brres.Changed = false;
                }

                Brres.Dispose();
                Brres = null;
                path = null;

                if (IsEditor)
                    OnClose();
            }

            return true;
        }

        internal void NewFile()
        {
            Open(new MemoryStream(null), "Untitled.brres"); //Properties.Resources.FormatNewBrres));
            path = null;
        }

        internal bool SaveBrres()
        {
            if (IsEditor)
                return SaveEditor();
            else
                return SaveFile();
        }

        internal bool SaveAs()
        {
            if (IsEditor)
                return SaveEditor();
            else
                return SaveFileAs();
        }

        internal bool SaveEditor()
        {
            bool saved;
            MemoryStream mem;

            saved = false;
            mem = new MemoryStream();
            try
            {
                Brres.Save(mem);
                Brres.Changed &= !(saved = OnSave(mem.ToArray()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorSave", ex.Message), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                mem.Close();
            }

            return saved;
        }

        internal bool SaveFile()
        {
            FileStream stream;
            bool success;

            if (string.IsNullOrEmpty(path)) return SaveFileAs();

            stream = null;
            try
            {
                stream = new FileStream(path, FileMode.Create);

                Brres.Save(stream);
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorLoad", ex.Message), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                success = false;
            }
            finally
            {
                stream.Close();
            }

            return success;
        }

        internal bool SaveFileAs()
        {
            if (MainWindow.FileSaveFileDialog.ShowDialog() == DialogResult.Cancel) return false;

            path = MainWindow.FileSaveFileDialog.FileName;

            return SaveFile();
        }

        internal void Open(Stream data, string name)
        {
            if (!Close()) return;

            try
            {
                Brres = new BrresFile(data);
                Brres.Name = name;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorLoad", ex.Message), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                Brres = null;
            }
            finally
            {
                data.Close();
            }
        }

        internal void OpenFile()
        {
            if (MainWindow.FileOpenFileDialog.ShowDialog() == DialogResult.Cancel) return;

            OpenFile(MainWindow.FileOpenFileDialog.FileName);
        }

        internal void OpenFile(string path)
        {
            FileStream stream;

            if (!Close()) return;

            stream = null;

            try
            {
                stream = new FileStream(this.path = path, FileMode.Open, FileAccess.Read);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorLoad", ex.Message), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (stream != null)
                    stream.Close();
                return;
            }

            Open(stream, Path.GetFileName(path));
        }

        internal void Run()
        {
            Application.Run(MainWindow);
        }
    }
}
