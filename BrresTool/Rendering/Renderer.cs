using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Chadsoft.CTools.Models;
using System.Collections.ObjectModel;

namespace Chadsoft.CTools.Rendering
{
    public abstract class Renderer
    {
        private Collection<Model> _models;
        private Collection<bool> _drawModels;
        private Matrix2x1 mouseLocation;
        private bool _invalid;

        protected bool Invalid { get { return _invalid; } set { _invalid = value; if (_invalid) if (Invalidated != null) Invalidated(this, EventArgs.Empty); } }

        public event EventHandler Invalidated;

        public Panel Panel { get; private set; }
        public Matrix3x1 CameraPosition { get; private set; }
        public Matrix3x1 CameraDirection { get; private set; }
        public Matrix3x1 CameraUp { get; private set; }
        public Matrix3x1 CameraRight { get; private set; }
        public float ZNear { get; private set; }
        public float ZFar { get; private set; }
        public ReadOnlyCollection<Model> Models { get { return new ReadOnlyCollection<Model>(_models); } }
        
        public Renderer(Panel panel)
        {
            _models = new Collection<Model>();
            _drawModels = new Collection<bool>();

            Panel = panel;

            Panel.Paint += new PaintEventHandler(Panel_Paint);
            Panel.Invalidated += new InvalidateEventHandler(Panel_Invalidated);
            Panel.MouseDown += new MouseEventHandler(Panel_MouseDown);
            Panel.MouseMove += new MouseEventHandler(Panel_MouseMove);
            Panel.MouseUp += new MouseEventHandler(Panel_MouseMove);

            CameraPosition = new Matrix3x1(0, 3000, -5000);
            CameraDirection = new Matrix3x1(0, 0, 1);
            CameraUp = new Matrix3x1(0, 1, 0);
            CameraRight = new Matrix3x1(1, 0, 0);

            mouseLocation = new Matrix2x1(0, 0);
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            ForceRender();
        }

        private void Panel_Invalidated(object sender, InvalidateEventArgs e)
        {
            ForceRender();
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Matrix2x1(e.X, e.Y);
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            float dx, dy;

            dx = e.X - mouseLocation.X;
            dy = e.Y - mouseLocation.Y;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    CameraDirection = CameraDirection * (float)Math.Cos(dy / 100) + CameraUp * (float)Math.Sin(dy / 100);
                    CameraDirection = CameraDirection * (float)Math.Cos(dx / 100) + CameraRight * (float)Math.Sin(dx / 100);
                    ReAlignCamera();

                    Invalidate();
                    break;
                case MouseButtons.Middle:
                    CameraUp = CameraUp * (float)Math.Cos(dy / 100) + CameraRight * (float)Math.Sin(dy / 100);
                    ReAlignCamera();

                    Invalidate();
                    break;
                case MouseButtons.Right:
                    CameraPosition += CameraDirection * -dy * 10 + CameraRight * -dx * 10;

                    Invalidate();
                    break;
            }

            mouseLocation = new Matrix2x1(e.X, e.Y);
        }

        private void ReAlignCamera()
        {
            Matrix3x1 right;

            right = CameraDirection.Cross(CameraUp);

            if (right.X == 0 && right.Y == 0 && right.Z == 0)
            {
                CameraUp = CameraRight.Cross(CameraDirection);
                CameraUp /= CameraUp.Length();
            }
            else
            {
                CameraRight = right / right.Length();
                CameraUp = CameraRight.Cross(CameraDirection);
                CameraUp /= CameraUp.Length();
            }
        }

        public void LoadModel(Model model)
        {
            _models.Add(model);
            _drawModels.Add(true);

            OnLoadModel(model);

            Invalidate();
        }

        protected abstract void OnLoadModel(Model model);

        public void UnloadModel(Model model)
        {
            int index;

            index = _models.IndexOf(model);

            if (index != -1)
            {
                OnUnloadModel(model);

                _models.RemoveAt(index);
                _drawModels.RemoveAt(index);

                Invalidate();
            }
        }

        protected abstract void OnUnloadModel(Model model);

        public void DisableRender(Model model)
        {
            int index;

            index = _models.IndexOf(model);

            if (index != -1)
            {
                _drawModels[index] = false;

                Invalidate();
            }
        }

        public void EnableRender(Model model)
        {
            int index;

            index = _models.IndexOf(model);

            if (index != -1)
            {
                _drawModels[index] = true;

                Invalidate();
            }
        }

        public bool IsRendered(Model model)
        {
            int index;

            index = _models.IndexOf(model);

            if (index != -1)
                return _drawModels[index];

            return false;
        }

        public void Invalidate()
        {
            Invalid = true;
        }

        public void Render()
        {
            if (!Invalid) return;

            OnRender();
        }

        protected abstract void OnRender();

        public void ForceRender()
        {
            Invalidate();
            Render();
        }

        public void UnloadAll()
        {
            for (int i = 0; i < _models.Count; )
                UnloadModel(_models[i]);
        }
    }
}
