using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ScintillaNET;
using OpenGL;
using ShaderStudio.Core;
using ShaderStudio.Objects.Primitives;

namespace ShaderStudio
{
    public partial class Editor : Form
    {
        #region Constants
        public const string MESSAGE_GL_CONTEXT_CREATED = "OpenGL Context Created";
        public const string MESSAGE_GL_CONTEXT_DESTROYED = "OpenGL Context Destroyed";
        #endregion

        Camera DefaultCam;
        Quad quad1;

        public Editor()
        {
            InitializeComponent();
        }

        #region OpenGL Canvas
        private void GLCanvas_ContextCreated(object sender, GlControlEventArgs e)
        {
            // Here you can allocate resources Or initialize state
            Gl.MatrixMode(MatrixMode.Projection);
            Gl.LoadIdentity();
            Gl.Ortho(0.0, 1.0F, 0.0, 1.0, 0.0, 1.0);

            Gl.MatrixMode(MatrixMode.Modelview);
            Gl.LoadIdentity();

            Gl.Enable(EnableCap.DepthTest);

            Console.WriteLine(MESSAGE_GL_CONTEXT_CREATED);

            DefaultCam = Camera.Default;
            ShadersManager.Instance.LoadShaders();

            quad1 = new Quad();

        }
        private void GLCanvas_ContextDestroying(object sender, GlControlEventArgs e)
        {
            Console.WriteLine(MESSAGE_GL_CONTEXT_DESTROYED);
        }

        private void GLCanvas_Render(object sender, GlControlEventArgs e)
        {
            //SetClearColor(Color.SlateBlue);
            Gl.VB.Viewport(0, 0, GLCanvas.Width, GLCanvas.Height);
            Gl.VB.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            quad1.Render(DefaultCam.GetViewMatrix(), DefaultCam.GetProjectionMatrix((float)GLCanvas.Width, (float)GLCanvas.Height));
        }
        #endregion

        private void GLCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                quad1.RegisteredStages.Clear();
                quad1.Reload();
            }
            else if (e.KeyCode == Keys.F2)
            {
                quad1.RegisteredStages.Clear();
                quad1.RegisteredStages.Add("NoneVertex");
                quad1.RegisteredStages.Add("CyanFragment");
                quad1.Reload();
            }
            else if (e.KeyCode == Keys.F3)
            {
                quad1.RegisteredStages.Clear();
                quad1.RegisteredStages.Add("DefaultVertex");
                quad1.RegisteredStages.Add("DefaultFragment");
                quad1.Reload();
            }
        }
    }
}
