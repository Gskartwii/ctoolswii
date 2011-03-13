using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing.Imaging;

namespace Chadsoft.CTools.Image 
{
    [ToolboxBitmap("Chadsoft.CTools.Image.Resources.picture_edit.png")]
    [DisplayName("Image Data Editor Control")]
    public partial class ImageDataEditorControl : UserControl
    {
        private BufferedGraphics buffer;
        private int[] colorData;
        private ImageDataFormat newFormat;
        private int newWidth, newHeight, newLevels;
        private Bitmap newImage;

        [Browsable(false)]
        public ImageData Image { get; private set; }
        [Category("Appearance")]
        public bool ShowMip { get { return mipLabel.Visible; } set { mipLabel.Visible = mipNumericUpDown.Visible = value; } }
        [Category("Appearance")]
        public bool ShowControls { get { return importButton.Visible; } set { importButton.Visible = exportButton.Visible = reformatButton.Visible = value; } }

        public ImageDataEditorControl()
        {
            InitializeComponent();

            viewComboBox.SelectedIndex = 0;

            SetupRender();
            UpdateInterface();
            Render();
        }

        public void Import()
        {
            if (Image == null)
                return;

            if (FileOpenFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            try
            {
                newImage = new Bitmap(FileOpenFileDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorLoad", ex.Message), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            newFormat = Image.Format;
            newWidth = newImage.Width;
            newHeight = newImage.Height;
            newLevels = Image.Levels;

            formatterBackgroundWorker.RunWorkerAsync(false);
        }

        public void Export()
        {
            ImageFormat outputFormat;
            Bitmap bitmap;
            int mip;

            if (Image == null)
                return;

            if (FileSaveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            mip = (int)(mipNumericUpDown.Value - 1);

            try
            {
                bitmap = ImageData.ToBitmap(Image.GetData(mip), Image.Width >> mip, Image.Height >> mip);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorFormat", ex.Message), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch (FileSaveFileDialog.FilterIndex)
            {
                case 0:
                    outputFormat = ImageFormat.Bmp;
                    break;
                case 1:
                    outputFormat = ImageFormat.Jpeg;
                    break;
                case 2:
                    outputFormat = ImageFormat.Gif;
                    break;
                case 3:
                    outputFormat = ImageFormat.Tiff;
                    break;
                case 4:
                    outputFormat = ImageFormat.Png;
                    break;
                default:
                    outputFormat = ImageFormat.Bmp;
                    break;
            }

            try
            {
                bitmap.Save(FileSaveFileDialog.FileName, outputFormat);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetString("MessageErrorSave", ex.Message), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void Reformat()
        {
            FormReformatImageDialog dialog;

            if (Image == null)
                return;

            dialog = new FormReformatImageDialog(Image);
            dialog.mipMapLevelsNumericUpDown.ReadOnly = !ShowMip;

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            newFormat = Image.GetFormats()[dialog.formatComboBox.SelectedIndex];
            newWidth = (int)dialog.widthNumericUpDown.Value;
            newHeight = (int)dialog.heightNumericUpDown.Value;
            newLevels = (int)dialog.mipMapLevelsNumericUpDown.Value;

            formatterBackgroundWorker.RunWorkerAsync(true);
        }

        public void SetImage(ImageData data)
        {
            Image = data;

            colorData = null;
            UpdateInterface();

            if (Image.Format == ImageDataFormat.I4 || Image.Format == ImageDataFormat.I8)
                viewComboBox.SelectedIndex = 2;
            else
                viewComboBox.SelectedIndex = 0;

            Render();
        }

        private void SetupRender()
        {
            if (buffer != null)
                buffer.Dispose();

            buffer = BufferedGraphicsManager.Current.Allocate(displayPanel.CreateGraphics(), new Rectangle(0, 0, displayPanel.Width, displayPanel.Height));
        }

        private void Render()
        {
            int mip, width, height;
            double zoom;
            Graphics g;
            Bitmap image;
            int[] temp;

            if (buffer == null)
                return;

            g = buffer.Graphics;
            g.Clear(displayPanel.BackColor);

            if (Image == null)
            {
                buffer.Render();
                return;
            }

            mip = (int)(mipNumericUpDown.Value - 1);
            zoom = Math.Pow(2, zoomTrackBar.Value / 32.0);
            g.SetClip(new Rectangle(0, 0, (width = (int)Math.Ceiling(Image.GetWidth(mip) * zoom)) - (int)zoom, (height = (int)Math.Ceiling(Image.GetHeight(mip) * zoom)) - (int)zoom));

            image = Properties.Resources.transparent;

            for (int x = 0; x < width; x += image.Width)
                for (int y = 0; y < height; y += image.Height)
                {
                    g.DrawImageUnscaled(image, x, y);
                }

            if (colorData == null)
            {
                colorData = Image.GetColorData(mip, reportProgress);

                formatProgressBar.Value = 0;
                formatProgressBar.Visible = false;
            }
            temp = new int[colorData.Length];

            switch (viewComboBox.SelectedIndex)
            {
                case 0: // default
                    Array.Copy(colorData, temp, colorData.Length);
                    break;
                case 1: // alpha
                    for (int i = 0; i < colorData.Length; i++)
                        temp[i] = Color.FromArgb(colorData[i] >> 24 & 0xff, colorData[i] >> 24 & 0xff, colorData[i] >> 24 & 0xff).ToArgb();
                    break;
                case 2: // colour
                    for (int i = 0; i < colorData.Length; i++)
                        temp[i] = colorData[i] | -16777216;
                    break;
                case 3: // red
                    for (int i = 0; i < colorData.Length; i++)
                        temp[i] = colorData[i] & 0x00ff0000 | -16777216;
                    break;
                case 4: // green
                    for (int i = 0; i < colorData.Length; i++)
                        temp[i] = colorData[i] & 0x0000ff00 | -16777216;
                    break;
                case 5: // blue
                    for (int i = 0; i < colorData.Length; i++)
                        temp[i] = colorData[i] & 0x000000ff | -16777216;
                    break;
            }

            image = ImageData.ToBitmap(temp, Image.GetWidth(mip), Image.GetHeight(mip));

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.DrawImage(image, -displayHScrollBar.Value, -displayVScrollBar.Value, width, height);

            buffer.Render();
        }

        private void UpdateInterface()
        {
            if (Image != null)
            {
                dimensionsLabel.Text = string.Format(CultureInfo.CurrentCulture, dimensionsLabel.Tag.ToString(), Image.Width, Image.Height);
                formatLabel.Text = string.Format(CultureInfo.CurrentCulture, formatLabel.Tag.ToString(), Image.Format.Description);
                if (mipNumericUpDown.Maximum != Image.Levels)
                    mipNumericUpDown.Maximum = Image.Levels;
            }
            else
            {
                dimensionsLabel.Text = string.Format(CultureInfo.CurrentCulture, dimensionsLabel.Tag.ToString(), "", "");
                formatLabel.Text = string.Format(CultureInfo.CurrentCulture, formatLabel.Tag.ToString(), "");
                if (mipNumericUpDown.Maximum != 1)
                    mipNumericUpDown.Maximum = 1;
            }

            UpdateScrollBars();
        }

        private void UpdateScrollBars()
        {
            int vmax, hmax, width, height, mip;
            double zoom;

            if (Image == null)
            {
                displayHScrollBar.Maximum = displayVScrollBar.Maximum = displayHScrollBar.Value = displayVScrollBar.Value = 0;
            }
            else
            {
                mip = (int)(mipNumericUpDown.Value - 1);
                zoom = Math.Pow(2, zoomTrackBar.Value / 32.0);

                width = (int)(Image.GetWidth(mip) * zoom);
                height = (int)(Image.GetHeight(mip) * zoom);

                hmax = Math.Max(width - displayPanel.Width, 0);
                vmax = Math.Max(height - displayPanel.Height, 0);

                if (hmax < displayHScrollBar.Value)
                    displayHScrollBar.Value = hmax;
                if (vmax < displayVScrollBar.Value)
                    displayVScrollBar.Value = vmax;

                if (displayHScrollBar.Maximum != hmax)
                {
                    displayHScrollBar.LargeChange = displayPanel.Width;
                    displayHScrollBar.Maximum = hmax + displayHScrollBar.LargeChange;
                }
                if (displayVScrollBar.Maximum != vmax)
                {
                    displayVScrollBar.LargeChange = displayPanel.Height;
                    displayVScrollBar.Maximum = vmax + displayVScrollBar.LargeChange;
                }
            }
        }

        private void displayPanel_Resize(object sender, EventArgs e)
        {
            SetupRender();            
        }

        private void displayPanel_Paint(object sender, PaintEventArgs e)
        {
            UpdateScrollBars();
            Render();
        }

        private void zoomTrackBar_Scroll(object sender, EventArgs e)
        {
            double zoom;

            zoom = Math.Pow(2, zoomTrackBar.Value / 32.0);
            zoom *= 100;

            zoomValueLabel.Text = string.Format(zoomValueLabel.Tag.ToString(), zoom);

            UpdateScrollBars();
            Render();
        }

        private void viewComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Render();
        }

        private void mipNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            colorData = null;

            UpdateScrollBars();
            Render();
        }

        private void reformatButton_Click(object sender, EventArgs e)
        {
            Reformat();
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            Import();
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            Export();
        }

        private void formatterBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if ((bool)e.Argument)
            {
                Image.Reformat(newFormat, newLevels, newWidth, newHeight, reportProgress);
            }
            else
            {
                Image.Import(ImageData.GetData(newImage), newFormat, newLevels, newWidth, newHeight, reportProgress);
            }

            Image.Changed = true;
        }

        private void reportProgress(object sender, ProgressChangedEventArgs e)
        {
            if (InvokeRequired)
                Invoke(new ProgressChangedEventHandler(reportProgress), sender, e);
            else
            {
                if (!formatProgressBar.Visible)
                    formatProgressBar.Visible = true;

                formatProgressBar.Value = e.ProgressPercentage;
                Application.DoEvents();
            }
        }

        private void formatterBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (formatProgressBar.Visible)
            {
                formatProgressBar.Visible = false;
                formatProgressBar.Value = 0;
            }

            if (e.Error != null)
                MessageBox.Show(Program.GetString("MessageErrorFormat", e.Error.Message), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            colorData = null;

            UpdateInterface();
            Render();
        }

        private void scrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            Render();
        }
    }
}
