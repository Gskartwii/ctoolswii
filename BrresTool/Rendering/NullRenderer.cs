using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chadsoft.CTools.Models;
using System.Windows.Forms;
using System.Drawing;
using Chadsoft.CTools.Brres;

namespace Chadsoft.CTools.Rendering
{
    public class NullRenderer : Renderer
    {
        private BufferedGraphics graphics;
        private Rectangle bufferSize;

        public NullRenderer(Panel panel) : base(panel)
        {
            bufferSize = new Rectangle(0, 0, Panel.Width, Panel.Height);
            graphics = BufferedGraphicsManager.Current.Allocate(Panel.CreateGraphics(), bufferSize);
            Panel.Resize += new EventHandler(Panel_Resize);
        }

        private void Panel_Resize(object sender, EventArgs e)
        {
            Rectangle newSize;

            newSize = new Rectangle(0, 0, Panel.Width, Panel.Height);

            if (newSize != bufferSize)
            {
                graphics.Dispose();

                bufferSize = newSize;
                graphics = BufferedGraphicsManager.Current.Allocate(Panel.CreateGraphics(), bufferSize);

                ForceRender();
            }
        }

        protected override void OnLoadModel(Model model)
        {
        }

        protected override void OnUnloadModel(Model model)
        {
        }

        protected override void OnRender()
        {
            Font font;
            Brush brush;
            StringFormat stringFormat;

            font = new Font("Arial", 12, FontStyle.Bold);
            brush = SystemBrushes.InfoText;
            stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            graphics.Graphics.Clear(SystemColors.Info);
            graphics.Graphics.DrawString(Program.GetString("NullRenderer"), font, brush, bufferSize, stringFormat);

            graphics.Render();
        }
    }
}
