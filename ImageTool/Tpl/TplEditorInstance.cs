using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Chadsoft.CTools.Image.Properties;
using System.IO;

namespace Chadsoft.CTools.Image.Tpl
{
    public class TplEditorInstance : EditorInstance, IDisposable
    {
        private bool disposed;
        private Editor _editor;
        private string path;

        internal TplImage Image { get; set; }
        internal TplEditorForm MainWindow { get; private set; }
        internal bool IsEditor { get { return _editor != null; } }
        internal bool Loaded { get { return Image != null; } }
        public override Editor Editor
        {
            get { return _editor; }
        }
        public string Name { get; private set; }


        public TplEditorInstance(string[] args)
            : base(null, null, null)
        {
            Name = null;

            SetupForm();
        }

        public TplEditorInstance(byte[] data, string name, Editor editor, EventHandler<SaveEventArgs> saveEvent, EventHandler closeEvent)
            : base(data, saveEvent, closeEvent)
        {
            _editor = editor;
            Name = name;

            SetupForm();
            Open(new MemoryStream(data));

            MainWindow.UpdateInterface();
        }

        ~TplEditorInstance()
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
                        Image.Dispose();

                }

                MainWindow = null;
                Image = null;

                disposed = true;
            }
        }

        public override bool CloseEditor()
        {
            return Close();
        }

        private void SetupForm()
        {
            MainWindow = new TplEditorForm(this);
            MainWindow.Show();
        }

        internal bool Close()
        {
            DialogResult result;

            if (Loaded)
            {
                if (Image.Changed)
                {
                    result = MessageBox.Show(Program.GetString("MessageFileClose", Name), MainWindow.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                    if (result == DialogResult.Cancel) return false;
                    else if (result == DialogResult.Yes)
                    {
                        if (!SaveAs()) return false;
                    }
                    else
                        Image.Changed = false;
                }

                Image.Dispose();
                Image = null;
                path = null;

                if (IsEditor)
                    OnClose();
            }

            return true;
        }

        internal void NewFile()
        {
            Open(new MemoryStream(Properties.Resources.FormatNewTpl));
            path = null;
        }

        internal bool SaveTpl()
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
                Image.Save(mem);
                Image.Changed &= !(saved = OnSave(mem.ToArray()));
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

                Image.Save(stream);
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
                Image = new TplImage(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorLoad", ex.Message), MainWindow.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                Image = null;
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

                if (stream != null)
                    stream.Close();
                return;
            }

            Open(stream);
        }

        internal void Run()
        {
            Application.Run(MainWindow);
        }
    }
}
