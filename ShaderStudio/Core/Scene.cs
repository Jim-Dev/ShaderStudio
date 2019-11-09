using ShaderStudio.Objects;
using ShaderStudio.Objects.Lights;
using ShaderStudio.Objects.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XNA = Microsoft.Xna.Framework;

namespace ShaderStudio.Core
{
    public class Scene
    {
        private Camera activeCamera;
        Dictionary<string, SceneObject> SceneObjects;//ObjectName,Object

        private Grid gridFloor;

        private int activeObjectIndex;

        private float deltaTime;
        private float lastFrameTime;
        private float totalTime;

        public SceneObject ActiveObject
        {
            get { return GetSceneObjectByIndex(this.activeObjectIndex); }
        }

        public event EventHandler<EventArgs> OnActiveObjectChanged;

        public Light AmbientLight = new Light();

        public Camera ActiveCamera
        {
            get
            {
                if (this.activeCamera == null)
                    this.activeCamera = Camera.Default;
                return this.activeCamera;
            }
        }

        public float DeltaTime
        {
            get { return this.deltaTime; }
        }
        public float TotalTime
        {
            get { return this.totalTime; }
            private set { this.totalTime = value; }
        }
        private static volatile Scene instance;
        private static object syncRoot = new Object();

        public static Scene CurrentScene
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Scene();
                    }
                }

                return instance;
            }
        }


        public Scene()
        {
            SceneObjects = new Dictionary<string, SceneObject>();
            gridFloor = new Grid();
            AmbientLight.Position = new Microsoft.Xna.Framework.Vector3(1,1,1);
            Light simpleLight = new Light(XNA.Color.Cyan,1f);
            simpleLight.Position = new Microsoft.Xna.Framework.Vector3(1,2.125f, 1);
            AddSceneObject(AmbientLight);
            AddSceneObject(simpleLight,"Light0");

        }

        public void Render(float SceneWidth, float SceneHeight)
        {
            if (!TimeManager.Instance.IsStarted)
                TimeManager.Instance.Start();

            TotalTime = TimeManager.Instance.GetElapsedSeconds(); //Delta time calculation - refactor

            CurrentScene.deltaTime = TotalTime - lastFrameTime;
            lastFrameTime = TotalTime;

            gridFloor.Render(CurrentScene.ActiveCamera.GetViewMatrix(), CurrentScene.ActiveCamera.GetProjectionMatrix(SceneWidth, SceneHeight));
            foreach (Renderable renderObj in GetAllRenderables())
            {
                Light simpleLight = Scene.CurrentScene.SceneObjects["Light0"] as Light;
                simpleLight.Position.X = 2.0f * (float)Math.Sin(TimeManager.Instance.GetElapsedSeconds() / 2);
                simpleLight.Position.Z = 2.0f * (float)Math.Cos(TimeManager.Instance.GetElapsedSeconds() / 2);
                renderObj.Render(CurrentScene.ActiveCamera.GetViewMatrix(), CurrentScene.ActiveCamera.GetProjectionMatrix(SceneWidth, SceneHeight));


                renderObj?.ShaderProgram?.SetVector(Constants.ShaderConstants.SHADER_PARAM_LIT_CAMERA_POSITION, Scene.CurrentScene.ActiveCamera.Position);

                renderObj?.ShaderProgram?.SetVector(Constants.ShaderConstants.SHADER_PARAM_LIT_AMBIENT_COLOR, AmbientLight.LightColor, false);
                renderObj?.ShaderProgram?.SetFloat(Constants.ShaderConstants.SHADER_PARAM_LIT_AMBIENT_INTENSITY, AmbientLight.LightIntensity);

                renderObj?.ShaderProgram?.SetVector(Constants.ShaderConstants.SHADER_PARAM_LIT_SIMPLELIGHT_COLOR, simpleLight.LightColor, false);
                renderObj?.ShaderProgram?.SetFloat(Constants.ShaderConstants.SHADER_PARAM_LIT_SIMPLELIGHT_INTENSITY, simpleLight.LightIntensity);
                renderObj?.ShaderProgram?.SetVector(Constants.ShaderConstants.SHADER_PARAM_LIT_SIMPLELIGHT_POSITION, simpleLight.Position);

            }
        }

        public void AddSceneObject(SceneObject sceneObject)
        {
            if (!SceneObjects.ContainsKey(sceneObject.Name))
                SceneObjects.Add(sceneObject.Name, sceneObject);
        }

        public void AddSceneObject(SceneObject sceneObject, string newName)
        {
            sceneObject.Name = newName;
            AddSceneObject(sceneObject);
        }

        public void SelectObject(int objectIndex)
        {
            if (objectIndex == -1)
            {
                this.activeObjectIndex = GetSceneObjectCount() - 1;
                if (OnActiveObjectChanged != null)
                    OnActiveObjectChanged.Invoke(this, EventArgs.Empty);
            }
            else
            {
                this.activeObjectIndex = objectIndex;
                if (OnActiveObjectChanged != null)
                    OnActiveObjectChanged.Invoke(this, EventArgs.Empty);
            }
        }
        public void SelectObject(string objectName)
        {
            SelectObject(GetSceneObjectIndex(objectName));
        }

        public SceneObject GetSceneObjectByIndex(int index)
        {
            if (SceneObjects.Count > index)
            {
                return SceneObjects.Values.ElementAt(index);
            }
            else
                return null;
        }
        public SceneObject GetSceneObjectByIndex(string objectName)
        {
            if (SceneObjects.ContainsKey(objectName))
                return SceneObjects[objectName];
            else
                return null;
        }

        public int GetSceneObjectIndex(string objectName)
        {
            return SceneObjects.Keys.ToList().IndexOf(objectName);
        }
        public bool RemoveSceneObject(string objectName)
        {
            if (SceneObjects.ContainsKey(objectName))
            {
                SceneObjects.Remove(objectName);
                return true;
            }
            else
                return false;
        }
        public int GetSceneObjectCount()
        {
            return SceneObjects.Count;
        }

        public void ClearScene()
        {
            SceneObjects.Clear();
        }

        public List<Renderable> GetAllRenderables()
        {
            List<Renderable> output = new List<Renderable>();

            foreach (SceneObject sceneObject in SceneObjects.Values)
            {
                if (sceneObject is Renderable)
                {
                    Renderable renderObj = (Renderable)sceneObject;
                    output.Add(renderObj);
                }
            }

            return output;
        }
    }
}
