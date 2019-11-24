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
        private Dictionary<string, SceneObject> sceneObjects;//ObjectName,Object
        private SceneLights SceneLights { get; set; }

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

        //public Light AmbientLight = new Light();

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
            sceneObjects = new Dictionary<string, SceneObject>();
            gridFloor = new Grid();
            SceneLights = new SceneLights();
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
                Light simpleLight=null;
                if (SceneLights.PointLightsCount>0)
                simpleLight = SceneLights.GetLightsByType(Light.eLightType.Point)[0];
                simpleLight.Position.X = 2.0f * (float)Math.Sin(TimeManager.Instance.GetElapsedSeconds() / 2);
                simpleLight.Position.Z = 2.0f * (float)Math.Cos(TimeManager.Instance.GetElapsedSeconds() / 2);

                Light simpleLight2 = null;
                if (SceneLights.PointLightsCount > 1)
                    simpleLight2 = SceneLights.GetLightsByType(Light.eLightType.Point)[1];
                simpleLight2.Position.X = 2.0f * (float)Math.Sin(-TimeManager.Instance.GetElapsedSeconds());
                simpleLight2.Position.Z = 2.0f * (float)Math.Cos(-TimeManager.Instance.GetElapsedSeconds());

                renderObj.Render(CurrentScene.ActiveCamera.GetViewMatrix(), CurrentScene.ActiveCamera.GetProjectionMatrix(SceneWidth, SceneHeight));


                renderObj?.ShaderProgram?.SetVector(Constants.ShaderConstants.SHADER_PARAM_LIT_CAMERA_POSITION, Scene.CurrentScene.ActiveCamera.Position);

                renderObj?.ShaderProgram?.SetVector(Constants.ShaderConstants.SHADER_PARAM_LIT_AMBIENT_COLOR, SceneLights.Ambient.LightColor, false);
                renderObj?.ShaderProgram?.SetFloat(Constants.ShaderConstants.SHADER_PARAM_LIT_AMBIENT_INTENSITY, SceneLights.Ambient.LightIntensity);

                if (simpleLight != null)
                {
                    renderObj?.ShaderProgram?.SetVector(Constants.ShaderConstants.SHADER_PARAM_LIT_SIMPLELIGHT_COLOR, simpleLight.LightColor, false);
                    renderObj?.ShaderProgram?.SetFloat(Constants.ShaderConstants.SHADER_PARAM_LIT_SIMPLELIGHT_INTENSITY, simpleLight.LightIntensity);
                    renderObj?.ShaderProgram?.SetVector(Constants.ShaderConstants.SHADER_PARAM_LIT_SIMPLELIGHT_POSITION, simpleLight.Position);
                }
                renderObj?.ShaderProgram?.SetVector("material.ambient", 1.0f, 0.5f, 0.31f);
                renderObj?.ShaderProgram?.SetVector("material.diffuse", 1.0f, 0.5f, 0.31f);
                renderObj?.ShaderProgram?.SetVector("material.specular", 0.5f, 0.5f, 0.5f);
                renderObj?.ShaderProgram?.SetFloat("material.shininess", 32.0f);

                int lightIndex = 0;
                foreach (PointLight pointLight in SceneLights.GetLightsByType(Light.eLightType.Point))
                {
                    renderObj?.ShaderProgram?.SetVector(string.Format("PointLights[{0}].position", lightIndex), pointLight.Position);
                    renderObj?.ShaderProgram?.SetVector(string.Format("PointLights[{0}].diffuse", lightIndex), pointLight.LightColor, false);
                    renderObj?.ShaderProgram?.SetVector(string.Format("PointLights[{0}].specular", lightIndex), pointLight.LightColor, false);
                    renderObj?.ShaderProgram?.SetFloat(string.Format("PointLights[{0}].intensity", lightIndex), pointLight.LightIntensity);

                    renderObj?.ShaderProgram?.SetFloat(string.Format("PointLights[{0}].constant", lightIndex), pointLight.Constant);
                    renderObj?.ShaderProgram?.SetFloat(string.Format("PointLights[{0}].linear", lightIndex), pointLight.Linear);
                    renderObj?.ShaderProgram?.SetFloat(string.Format("PointLights[{0}].quadratic", lightIndex), pointLight.Quadratic);
                    lightIndex++;
                }
                lightIndex = 0;
                foreach (DirectionalLight directionalLight in SceneLights.GetLightsByType(Light.eLightType.Directional))
                {
                    renderObj?.ShaderProgram?.SetVector(string.Format("DirLights[{0}].direction", lightIndex), directionalLight.Direction);
                    renderObj?.ShaderProgram?.SetVector(string.Format("DirLights[{0}].diffuse", lightIndex), directionalLight.LightColor, false);
                    renderObj?.ShaderProgram?.SetVector(string.Format("DirLights[{0}].specular", lightIndex), directionalLight.LightColor, false);
                    renderObj?.ShaderProgram?.SetFloat(string.Format("DirLights[{0}].intensity", lightIndex), directionalLight.LightIntensity);

                    lightIndex++;
                }

            }
        }

        public void AddSceneObject(SceneObject sceneObject)
        {
            if (sceneObject is Light)
            {
                SceneLights.AddSceneLight(sceneObject as Light);
            }
            if (!sceneObjects.ContainsKey(sceneObject.Name))
                sceneObjects.Add(sceneObject.Name, sceneObject);
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
            if (sceneObjects.Count > index)
            {
                return sceneObjects.Values.ElementAt(index);
            }
            else
                return null;
        }
        public SceneObject GetSceneObjectByIndex(string objectName)
        {
            if (sceneObjects.ContainsKey(objectName))
                return sceneObjects[objectName];
            else
                return null;
        }

        public int GetSceneObjectIndex(string objectName)
        {
            return sceneObjects.Keys.ToList().IndexOf(objectName);
        }
        public bool RemoveSceneObject(string objectName)
        {
            if (sceneObjects.ContainsKey(objectName))
            {
                sceneObjects.Remove(objectName);
                return true;
            }
            else
                return false;
        }
        public int GetSceneObjectCount()
        {
            return sceneObjects.Count;
        }

        public void ClearScene()
        {
            sceneObjects.Clear();
        }

        public List<Renderable> GetAllRenderables()
        {
            List<Renderable> output = new List<Renderable>();

            foreach (SceneObject sceneObject in sceneObjects.Values)
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

    public class SceneLights
    {
        public const int MAX_LIGHT_DIRECTIONAL = 2;
        public const int MAX_LIGHT_POINT = 4;
        public const int MAX_LIGHT_SPOT = 2;

        public AmbientLight Ambient { get; private set; }

        public int PointLightsCount { get; private set; }
        public int DirectionalLightsCount { get; private set; }
        public int SpotLightsCount { get; private set; }

        private Dictionary<string, Light> sceneLightObjects;//LightObjectName,LightObject

        public SceneLights()
        {
            Ambient = DefaultAmbientLight;
            sceneLightObjects = new Dictionary<string, Light>();
        }

        public AmbientLight DefaultAmbientLight
        {
            get
            {
                return new AmbientLight(new XNA.Color(0.5f, 0.5f, 0.5f), 0.5f);
            }
        }

        public List<Light> GetLightsByType(Light.eLightType lightType)
        {
            List<Light> output = new List<Light>();
            foreach (Light light in sceneLightObjects.Values)
            {
                if (light.LightType == lightType)
                    output.Add(light);
            }
            return output;
        }
        public List<Light> Lights()
        {
            return sceneLightObjects.Values.ToList();
        }
        public void AddSceneLight(Light light)
        {
            if (light != null && !sceneLightObjects.ContainsKey(light.Name))
            {
                switch (light.LightType)
                {
                    case Light.eLightType.None:
                        throw new InvalidOperationException("LightType should not be None");
                    case Light.eLightType.Ambient:
                        Ambient = light as AmbientLight;
                        break;
                    case Light.eLightType.Point:
                        if (PointLightsCount < MAX_LIGHT_POINT)
                        {
                            PointLightsCount++;
                            sceneLightObjects.Add(light.Name, light);
                        }
                        break;
                    case Light.eLightType.Directional:
                        if (DirectionalLightsCount < MAX_LIGHT_DIRECTIONAL)
                        {
                            DirectionalLightsCount++;
                            sceneLightObjects.Add(light.Name, light);
                        }
                        break;
                    case Light.eLightType.Spot:
                        if (SpotLightsCount < SpotLightsCount)
                        {
                            SpotLightsCount++;
                            sceneLightObjects.Add(light.Name, light);
                        }
                        break;
                    default:
                        break;
                }
            }

        }
        public void RemoveSceneLight(string lightObjectName)
        {
            if (sceneLightObjects.ContainsKey(lightObjectName))
            {
                switch (sceneLightObjects[lightObjectName].LightType)
                {
                    case Light.eLightType.Ambient:
                        Ambient = DefaultAmbientLight;
                        break;
                    case Light.eLightType.Point:
                        PointLightsCount--;
                        break;
                    case Light.eLightType.Directional:
                        DirectionalLightsCount--;
                        break;
                    case Light.eLightType.Spot:
                        SpotLightsCount--;
                        break;
                    default:
                        break;
                }
                sceneLightObjects.Remove(lightObjectName);
            }
        }
    }
}
