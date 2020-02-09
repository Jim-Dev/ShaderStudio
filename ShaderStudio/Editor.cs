using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using XNA = Microsoft.Xna.Framework;

using ScintillaNET;
using OpenGL;
using ShaderStudio.Core;
using ShaderStudio.Objects.Primitives;

using System.IO;
using ShaderStudio.Objects.Lights;

namespace ShaderStudio
{
    public partial class Editor : Form
    {
        private object fsw_lock = new object();
        private bool isWatcherProcessingFile = false;

        #region Constants
        public const string MESSAGE_GL_CONTEXT_CREATED = "OpenGL Context Created";
        public const string MESSAGE_GL_CONTEXT_DESTROYED = "OpenGL Context Destroyed";
        #endregion

        #region MouseFlags

        private bool isLeftMousePressed = false;
        private bool isRightMousePressed = false;
        private bool isMiddleMousePressed = false;

        private Point lastMouseLocation = Point.Empty;
        private Point deltaMouseLocation = Point.Empty;

        private XNA.Vector3 startCameraPosition = new XNA.Vector3(0, 1, 5);

        #endregion

        Cube cube1;
        private float CameraMovementSpeed = 0.75f;
        private float CameraRotationSpeed = 0.25f;

        private float CameraDeltaPitch = 0;
        private float CameraDeltaYaw = 0;

        public Editor()
        {
            InitializeComponent();
            ShadersManager.Instance.CompilationError += Instance_CompilationError;
            ShadersManager.Instance.CompilationSuccess += Instance_CompilationSuccess;
        }

        private void Instance_CompilationSuccess(object sender, EventArgs e)
        {
            StringBuilder sBuilder = new StringBuilder(ShadersManager.Instance.InfoLog);
            if (cube1 != null && cube1.ShaderProgram != null)
            {
                sBuilder.AppendLine("Attributes:");
                foreach (string shaderAttribute in cube1.ShaderProgram.GetAttributeNames())
                {
                    sBuilder.AppendLine("\t" + shaderAttribute);
                }
                sBuilder.AppendLine("Uniforms:");
                foreach (string shaderUniforms in cube1.ShaderProgram.GetUniformNames())
                {
                    sBuilder.AppendLine("\t" + shaderUniforms);
                }
            }

            txbOutput.Text = sBuilder.ToString();
            sBuilder.Clear();
            this.Invalidate(true);
        }

        private void Instance_CompilationError(object sender, EventArgs e)
        {
            txbOutput.Text = ShadersManager.Instance.InfoLog;
            this.Invalidate(true);
        }

        #region OpenGL Canvas

        private void ResetCamera()
        {
            Scene.CurrentScene.ActiveCamera.Position = startCameraPosition;
            Scene.CurrentScene.ActiveCamera.Rotation = Camera.Default.Rotation;
            Scene.CurrentScene.ActiveCamera.CameraDirection = Camera.Default.CameraDirection;
            Scene.CurrentScene.ActiveCamera.CameraFront = Camera.Default.CameraFront;
            Scene.CurrentScene.ActiveCamera.CameraTarget = Camera.Default.CameraTarget;
        }

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

            ShadersManager.Instance.ReloadShaders();

            cube1 = new Cube();
            cube1.Name = "CUBE1";
            cube1.Scale = new XNA.Vector3(1.5f, 1.5f, 1.5f);

            Scene.CurrentScene.AddSceneObject(cube1);

            ResetCamera();

            AddDefaultLights();
        }

        private void AddDefaultLights()
        {
            AmbientLight ambientLight = new AmbientLight(XNA.Color.White, 0.05f);
            Scene.CurrentScene.AddSceneObject(ambientLight);

            PointLight simpleLight = new PointLight(XNA.Color.Cyan, 1f);
            simpleLight.Position = new Microsoft.Xna.Framework.Vector3(1, 0.5f, 0.5f);
            PointLight simpleLight2 = new PointLight(XNA.Color.Magenta, 1f);
            simpleLight2.Position = new Microsoft.Xna.Framework.Vector3(-1, 0.5f, 0.5f);

            PointLight simpleLight3 = new PointLight(XNA.Color.Yellow, 0.2f);
            simpleLight3.Position = new Microsoft.Xna.Framework.Vector3(0, 2.5f, 0.25f);
            PointLight simpleLight4 = new PointLight(XNA.Color.Yellow, 0.2f);
            simpleLight4.Position = new Microsoft.Xna.Framework.Vector3(0, -2.5f, 0.25f);


            DirectionalLight dirLight0 = new DirectionalLight(XNA.Color.Red, 0.25f);
            dirLight0.Position = new Microsoft.Xna.Framework.Vector3(1, 0, 1);
            dirLight0.Direction = -dirLight0.Position;
            DirectionalLight dirLight1 = new DirectionalLight(XNA.Color.Blue, 0.25f);
            dirLight1.Position = new Microsoft.Xna.Framework.Vector3(-1, 0, 1);
            dirLight1.Direction = -dirLight1.Position;

            SpotLight spotLight0 = new SpotLight(XNA.Color.Green, 0.5f);
            spotLight0.Position = new XNA.Vector3(0, -0.5f, 2.5f);
            spotLight0.Direction = -spotLight0.Position;
            spotLight0.InnerAngle = 15f;
            spotLight0.OuterAngle = 20f;
            SpotLight spotLight1 = new SpotLight(XNA.Color.Cyan, 0.5f);
            spotLight1.Position = new XNA.Vector3(0, 0.5f, 2.5f);
            spotLight1.Direction = new XNA.Vector3(0, 0, -1);
            spotLight1.InnerAngle = 5f;
            spotLight1.OuterAngle = 10f;

            Scene.CurrentScene.AddSceneObject(simpleLight, "Light0");
            Scene.CurrentScene.AddSceneObject(simpleLight2, "Light1");
            Scene.CurrentScene.AddSceneObject(simpleLight3, "Light2");
            Scene.CurrentScene.AddSceneObject(simpleLight4, "Light3");

            Scene.CurrentScene.AddSceneObject(dirLight0, "DirLight0");
            Scene.CurrentScene.AddSceneObject(dirLight1, "DirLight1");

            Scene.CurrentScene.AddSceneObject(spotLight0, "spotLight0");
            Scene.CurrentScene.AddSceneObject(spotLight1, "spotLight1");
        }

        private void FVertexWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (!isWatcherProcessingFile)
            {
                isWatcherProcessingFile = true;
                lock (fsw_lock)
                {
                    if (cube1 != null)
                    {
                        cube1.RegisteredStages.Clear();
                        cube1.RegisteredStages.Add("CurrentVertex");
                        cube1.RegisteredStages.Add("CurrentFragment");
                        cube1.Reload();

                        //-- Ugly hack to avoid FileSystemWatcher be called twice
                        System.Timers.Timer watcherResetTimer = new System.Timers.Timer(1000) { AutoReset = false };
                        watcherResetTimer.Elapsed += (timerElapsedSender, timerElapsedArgs) =>
                        {
                            isWatcherProcessingFile = false;
                        };
                        watcherResetTimer.Start();
                        //--
                    }
                }
            }
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

            ///cube1.Rotation *= XNA.Quaternion.CreateFromYawPitchRoll(0.005f, 0.01f, 0);
            cube1.Rotation *= XNA.Quaternion.CreateFromYawPitchRoll(-0.005f, 0, 0);
            cube1?.ShaderProgram?.SetFloat("Time", Scene.CurrentScene.TotalTime);
            //Scene.CurrentScene.AmbientLight.LightIntensity = (float)Math.Abs(Math.Sin((double)Scene.CurrentScene.TotalTime));
            Scene.CurrentScene.Render((float)GLCanvas.Width, (float)GLCanvas.Height);

            HandleMouseInput();
        }
        #endregion

        #region MouseControl

        private void HandleMouseInput()
        {

            #region CamRotation
            CameraDeltaPitch = 0;
            CameraDeltaYaw = 0;

            CameraDeltaPitch += deltaMouseLocation.X * CameraRotationSpeed * Scene.CurrentScene.DeltaTime;
            CameraDeltaYaw += deltaMouseLocation.Y * CameraRotationSpeed * Scene.CurrentScene.DeltaTime;

            //XNA.Quaternion q = XNA.Quaternion.CreateFromYawPitchRoll(CameraDeltaPitch,CameraDeltaYaw , 0);
            #endregion

            if (isLeftMousePressed)
                Scene.CurrentScene.ActiveCamera.Position += new XNA.Vector3(deltaMouseLocation.X, -deltaMouseLocation.Y, 0) * CameraMovementSpeed * Scene.CurrentScene.DeltaTime;
            else if (isRightMousePressed)
            {
               Scene.CurrentScene.ActiveCamera.CameraTarget+= new XNA.Vector3(deltaMouseLocation.X, -deltaMouseLocation.Y, 0) * CameraMovementSpeed * Scene.CurrentScene.DeltaTime;
                //Scene.CurrentScene.ActiveCamera.CameraFront = XNA.Vector3.Normalize(XNA.Vector3.Transform(Scene.CurrentScene.ActiveCamera.CameraFront, q));
                //Scene.CurrentScene.ActiveCamera.SetDirection(CameraDeltaPitch,CameraDeltaYaw, 0);

            }

            Console.WriteLine(Scene.CurrentScene.ActiveCamera.Rotation);
        }

        private void GLCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    isLeftMousePressed = true;
                    lastMouseLocation = e.Location;
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Right:
                    isRightMousePressed = true;
                    lastMouseLocation = e.Location;
                    break;
                case MouseButtons.Middle:
                    isMiddleMousePressed = true;
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                    break;
            }
        }
        private void GLCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    isLeftMousePressed = false;
                    lastMouseLocation = Point.Empty;
                    deltaMouseLocation = Point.Empty;
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Right:
                    isRightMousePressed = false;
                    lastMouseLocation = Point.Empty;
                    deltaMouseLocation = Point.Empty;
                    break;
                case MouseButtons.Middle:
                    isMiddleMousePressed = false;
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                    break;
            }
        }
        private void GLCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                deltaMouseLocation = new Point(lastMouseLocation.X - e.X, lastMouseLocation.Y - e.Y);
                lastMouseLocation = e.Location;
            }
        }

        private void GLCanvas_DoubleClick(object sender, EventArgs e)
        {
            ResetCamera();
        }
        #endregion

        private void btnClearOutput_Click(object sender, EventArgs e)
        {
            txbOutput.Text = string.Empty;
        }

    }
}
