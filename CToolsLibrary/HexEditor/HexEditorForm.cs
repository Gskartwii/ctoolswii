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

using System;
using System.Drawing;
using System.Globalization;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace Chadsoft.CTools.HexEditor
{
    public partial class HexEditorForm : Form
    {
        public HexEditorInstance Instance { get; private set; }
        public bool IsClosing { get; private set; }

        internal byte[] Data { get { return data; } }
        internal bool Changed { get; set; }

        private byte[] data;
        private BufferedGraphics bufferedGraphics;
        private int selectedByte;
        private bool halfSelected, asciiSelected; 

        public HexEditorForm(HexEditorInstance instance)
        {
            InitializeComponent();

            Instance = instance;

            SetupInstance();
        }

        private void SetupInstance()
        {
            this.Text = string.Format(this.Text, Instance.Name);
            data = Instance.Data;
            mainVScrollBar.Maximum = data.Length >> 4;            

            SetupRender();
        }

        private void SetupRender()
        {
            if (bufferedGraphics != null)
                bufferedGraphics.Dispose();

            bufferedGraphics = BufferedGraphicsManager.Current.Allocate(displayPanel.CreateGraphics(), new Rectangle(Point.Empty, displayPanel.Size));

            Redraw();
        }

        private void Redraw()
        {
            int index, x, y;
            Brush brush;
            char c;

            if (bufferedGraphics == null)
                return;

            bufferedGraphics.Graphics.Clear(SystemColors.Window);
            index = mainVScrollBar.Value * 16;
            y = 10;

            for (int i = 0; i < displayPanel.Height; i += 16, y += 16)
            {
                bufferedGraphics.Graphics.DrawString(index.ToString("X8"), displayPanel.Font, SystemBrushes.ControlText, 10, y);
                x = 90;

                for (int j = 0; j < 16; j++, index++, x += 25)
                {
                    if (index < data.Length)
                    {
                        if (index == selectedByte)
                        {
                            bufferedGraphics.Graphics.FillRectangle(asciiSelected ? SystemBrushes.InactiveCaption : SystemBrushes.Highlight , x, y, 20, 15);
                            brush = SystemBrushes.HighlightText;
                        }
                        else
                            brush = SystemBrushes.ControlText;

                        bufferedGraphics.Graphics.DrawString(data[index].ToString("X2"), displayPanel.Font, brush, x, y);
                    }
                    else if (index == data.Length && index == selectedByte)
                    {
                        bufferedGraphics.Graphics.FillRectangle(asciiSelected ? SystemBrushes.InactiveCaption : SystemBrushes.Highlight, x, y, 20, 15);
                    }

                    if (j == 7)
                        x += 10;
                }

                x += 20; index -= 16;
                for (int j = 0; j < 16; j++, index++, x += 11)
                {
                    if (index < data.Length)
                    {
                        c = Convert.ToChar(data[index]);

                        if (char.IsControl(c))
                            c = '.';

                        if (index == selectedByte)
                        {
                            bufferedGraphics.Graphics.FillRectangle(!asciiSelected ? SystemBrushes.InactiveCaption : SystemBrushes.Highlight, x, y, 11, 15);
                            brush = SystemBrushes.HighlightText;
                        }
                        else
                            brush = SystemBrushes.ControlText;

                        bufferedGraphics.Graphics.DrawString(c.ToString(), displayPanel.Font, brush, x, y);
                    }
                    else if (index == data.Length && index == selectedByte)
                    {
                        bufferedGraphics.Graphics.FillRectangle(!asciiSelected ? SystemBrushes.InactiveCaption : SystemBrushes.Highlight, x, y, 11, 15);
                    }

                    if (j == 7)
                        x += 5;
                }
                
            }

            Render();
        }

        private void Render()
        {
            if (bufferedGraphics == null)
                return;

            bufferedGraphics.Render();
        }

        private void displayPanel_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }

        private void displayPanel_Resize(object sender, EventArgs e)
        {
            SetupRender();
        }

        private void mainVScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            Redraw();
        }

        private void displayPanel_MouseUp(object sender, MouseEventArgs e)
        {
            SelectByte(e.X, e.Y);
        }

        private void SelectByte(int x, int y)
        {
            int bx, by, index;

            y -= 10;
            x -= 90;

            if (y < 0)
                y = 0;
            if (x < 0)
                x = 0;

            asciiSelected = false;

            if (x < 200)
                bx = x / 25;
            else if (x < 410)
                bx = (x - 10) / 25;
            else
            {
                asciiSelected = true;
                x -= 430;
                if (x < 0)
                    x = 0;

                if (x < 88)
                    bx = x / 11;
                else if (x < 181)
                    bx = (x - 5) / 11;
                else
                    bx = 15;
                    
            }

            by = y / 16;
            by += mainVScrollBar.Value;

            index = (by << 4) + bx;
            selectedByte = Math.Min(Math.Max(index, 0), data.Length);
            halfSelected = false;
            Redraw();
        }

        private void HexEditorForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            byte b;
            if (!e.Handled)
            {
                if (asciiSelected)
                {
                    b = Encoding.ASCII.GetBytes(new char[] { e.KeyChar })[0];
                    if (!overwriteToolStripMenuItem.Checked || selectedByte == data.Length)
                    {
                        InsertBytes(b, selectedByte, 1);
                    }
                    else if (overwriteToolStripMenuItem.Checked)
                    {
                        data[selectedByte] = b;
                    }

                    selectedByte++;
                    ScrollToCursor();
                }
                else
                {
                    if (!byte.TryParse(e.KeyChar.ToString(), NumberStyles.HexNumber, CultureInfo.CurrentUICulture, out b))
                        SystemSounds.Beep.Play();
                    else
                    {
                        if (halfSelected && data.Length > selectedByte)
                            b |= (byte)(data[selectedByte] & 0xF0);
                        else
                            b <<= 4;

                        if ((!halfSelected && !overwriteToolStripMenuItem.Checked) || selectedByte == data.Length)
                        {
                            InsertBytes(b, selectedByte, 1);
                        }
                        else 
                        {
                            data[selectedByte] = b;
                        }

                        if (halfSelected)
                        {
                            halfSelected = false;
                            selectedByte++;
                        }
                        else
                        {
                            halfSelected = true;
                        }
                        ScrollToCursor();
                    }

                    
                }

                e.Handled = true;
                Redraw();
            }
        }
        
        private void HexEditorForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Handled)
                if (e.KeyCode == Keys.Insert)
                {
                    AlternateOverwrite();

                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    if (selectedByte != data.Length)
                    {
                        DeleteBytes(selectedByte, 1);
                        Redraw();
                    }
                    else
                        SystemSounds.Beep.Play();

                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down
                    || e.KeyCode == Keys.PageDown || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.Home || e.KeyCode == Keys.End)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Left:
                            selectedByte--;
                            break;
                        case Keys.Right:
                            selectedByte++;
                            break;
                        case Keys.Up:
                            selectedByte -= 16;
                            break;
                        case Keys.Down:
                            selectedByte += 16;
                            break;
                        case Keys.PageUp:
                            selectedByte -= 256;
                            break;
                        case Keys.PageDown:
                            selectedByte += 256;
                            break;
                        case Keys.Home:
                            if (e.Control)
                                selectedByte = 0;
                            else
                                selectedByte &= ~0xF;
                            break;
                        case Keys.End:
                            if (e.Control)
                                selectedByte = data.Length;
                            else
                                selectedByte |= 0xF;
                            break;

                    }
                    selectedByte = Math.Min(Math.Max(selectedByte, 0), data.Length);

                    ScrollToCursor();

                    e.Handled = true;

                    Redraw();
                }
        }

        private void ScrollToCursor()
        {
            int scroll;
            scroll = selectedByte / 16;
            scroll -= mainVScrollBar.Value;
            if (scroll < 0)
                mainVScrollBar.Value = selectedByte / 16;
            else if (scroll > displayPanel.Height / 16)
                mainVScrollBar.Value = Math.Max(Math.Min(selectedByte / 16, mainVScrollBar.Maximum), 0);
        }

        private void AlternateOverwrite()
        {
            overwriteToolStripMenuItem.Checked = !overwriteToolStripMenuItem.Checked;
            if (overwriteToolStripMenuItem.Checked)
                overwriteStatusToolStripStatusLabel.Text = "Ovr";
            else
                overwriteStatusToolStripStatusLabel.Text = "Ins";
        }

        private void InsertBytes(byte b, int offset, int count)
        {
            byte[] temp;

            temp = new byte[data.Length + count];

            Array.Copy(data, 0, temp, 0, offset);

            for (int i = 0; i < count; i++)
            {
                temp[offset + i] = b;
            }

            Array.Copy(data, offset, temp, offset + count, data.Length - offset);

            mainVScrollBar.Maximum = data.Length >> 4;
            data = temp;
            Changed = true;
        }

        private void DeleteBytes(int offset, int count)
        {
            byte[] temp;

            temp = new byte[data.Length - count];

            Array.Copy(data, 0, temp, 0, offset);
            Array.Copy(data, offset + count, temp, offset, temp.Length - offset);

            mainVScrollBar.Maximum = data.Length >> 4;
            data = temp;
            Changed = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Changed &= !Instance.DoSave();
        }

        private void HexEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClosing = true;
            e.Cancel = !Instance.DoClose();
            IsClosing = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void overwriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlternateOverwrite();
        }

        private void gotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToForm goToForm;

            goToForm = new GoToForm(data.Length);

            if (goToForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                selectedByte = goToForm.Offset;
                halfSelected = false;
                ScrollToCursor();

                Redraw();
            }
        }

    }
}
