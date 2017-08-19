using ShaderStudio.Objects;
using ShaderStudio.Objects.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public SceneObject ActiveObject
        {
            get { return GetSceneObjectByIndex(this.activeObjectIndex); }
        }

        public event EventHandler<EventArgs> OnActiveObjectChanged;


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
        }

        public void Render(float SceneWidth, float SceneHeight)
        {
            if (!TimeManager.Instance.IsStarted)
                TimeManager.Instance.Start();

            float currentFrame = TimeManager.Instance.GetElapsedSeconds(); //Delta time calculation - refactor
            CurrentScene.deltaTime = currentFrame - lastFrameTime;
            lastFrameTime = currentFrame;

            gridFloor.Render(CurrentScene.ActiveCamera.GetViewMatrix(), CurrentScene.ActiveCamera.GetProjectionMatrix(SceneWidth, SceneHeight));
            foreach (Renderable renderObj in GetAllRenderables())
            {
                renderObj.Render(CurrentScene.ActiveCamera.GetViewMatrix(), CurrentScene.ActiveCamera.GetProjectionMatrix(SceneWidth, SceneHeight));
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
