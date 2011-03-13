using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chadsoft.CTools.Image
{
    partial class FormReformatImageDialog : Form
    {
        public FormReformatImageDialog(ImageData image)
        {
            int minDimension;

            InitializeComponent();

            foreach (ImageDataFormat format in image.GetFormats())
            {
                formatComboBox.Items.Add(format.Name);

                if (image.Format == format)
                    formatComboBox.SelectedIndex = formatComboBox.Items.Count - 1;
            }

            widthNumericUpDown.Value = image.Width;
            heightNumericUpDown.Value = image.Height;

            minDimension = (int)Math.Min(widthNumericUpDown.Value, heightNumericUpDown.Value);
            mipMapLevelsNumericUpDown.Maximum = (int)Math.Floor(Math.Log(minDimension, 2));
            mipMapLevelsNumericUpDown.Value = image.Levels;
        }

        private void widthHeightNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            int minDimension;

            minDimension = (int)Math.Min(widthNumericUpDown.Value, heightNumericUpDown.Value);

            mipMapLevelsNumericUpDown.Maximum = (int)Math.Floor(Math.Log(minDimension, 2));
        }
    }
}
