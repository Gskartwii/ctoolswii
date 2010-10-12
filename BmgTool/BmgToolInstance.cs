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

namespace Chadsoft.CTools.Bmg
{
    public class BmgToolInstance : EditorInstance, IDisposable
    {
        private bool disposed;
        private Editor _editor;
        private string path;
        private Collection<EditorInstance> editors;
        private Collection<BmgMessage> locks;

        internal BmgFile Bmg { get; set; }
        internal FormMain MainWindow { get; private set; }
        internal bool IsEditor { get { return _editor != null; } }
        internal bool Loaded { get { return Bmg != null; } }
        public override Editor Editor
        {
            get { return _editor; }
        }

        public BmgToolInstance(string[] args)
            : base(null, null, null)
        {
            editors = new Collection<EditorInstance>();
            locks = new Collection<BmgMessage>();
            SetupForm();
        }

        public BmgToolInstance(byte[] data, string name, Editor editor, EventHandler<SaveEventArgs> saveEvent, EventHandler closeEvent)
            : base(data, saveEvent, closeEvent)
        {
            _editor = editor;

            editors = new Collection<EditorInstance>();
            locks = new Collection<BmgMessage>();
            SetupForm();
            Open(new MemoryStream(data));
            Bmg.Name = name;

            MainWindow.UpdateInterface();
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

                if (Bmg.Changed)
                {
                    result = MessageBox.Show(Program.GetString("MessageFileClose", Bmg.Name), MainWindow.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                    if (result == DialogResult.Cancel) return false;
                    else if (result == DialogResult.Yes)
                    {
                        if (!SaveAs()) return false;
                    }
                    else
                        Bmg.Changed = false;
                }

                Bmg.Dispose();
                Bmg = null;
                path = null;

                if (IsEditor)
                    OnClose();
            }

            return true;
        }

        internal void NewFile()
        {
            Open(new MemoryStream(Properties.Resources.FormatNewBmg));
            path = null;
        }

        internal bool SaveBmg()
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
                Bmg.Save(mem);
                Bmg.Changed &= !(saved = OnSave(mem.ToArray()));
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

                Bmg.Save(stream);
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

        internal void Open(Stream data)
        {
            if (!Close()) return;

            try
            {
                Bmg = new BmgFile(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorLoad", ex.Message), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                Bmg = null;
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

            stream = null;

            try
            {
                stream = new FileStream(this.path = path, FileMode.Open, FileAccess.Read);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorLoad", ex.Message), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                stream.Close();
                return;
            }

            Open(stream);
        }

        internal void Run()
        {
            Application.Run(MainWindow);
        }

        public void Dispose()
        {
            if (!disposed)
            {
                MainWindow.Dispose();
                if (Loaded)
                    Bmg.Dispose();

                disposed = true;
            }
        }

        internal void OpenBinary(BmgMessage message)
        {
            Editor editor;
            EditorInstance instance;

            if (IsLocked(message))
            {
                MessageBox.Show(Program.GetString("MessageErrorLock"), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            editor = ToolManager.GetEditor("*.bin", message.Binary, 0);
            instance = editor.CreateInstance(message.Binary, "message" + message.Id.ToString() + ".bin", new EventHandler<SaveEventArgs>(editorInstance_Save), new EventHandler(editorInstance_Closed));

            editors.Add(instance);
            locks.Add(message);
        }

        private void editorInstance_Save(object sender, SaveEventArgs e)
        {
            int file;
            EditorInstance editorInstance;

            editorInstance = (EditorInstance)sender;
            file = editors.IndexOf(editorInstance);

            locks[file].Binary = editorInstance.Data;
                        
            e.Success = true;
            MainWindow.UpdateInterface();
            MainWindow.UpdateItem(Bmg.Messages.IndexOf(locks[file]));

            Bmg.Changed = true;
        }

        private void editorInstance_Closed(object sender, EventArgs e)
        {
            int file;
            BmgMessage message;
            EditorInstance editorInstance;

            editorInstance = (EditorInstance)sender;
            file = editors.IndexOf(editorInstance);

            if (file == -1) return;

            message = locks[file];
            editors.RemoveAt(file);
            locks.RemoveAt(file);

            MainWindow.UpdateItem(Bmg.Messages.IndexOf(message));
        }

        internal bool IsLocked(BmgMessage message)
        {
            return locks.Contains(message);
        }
    }
}
