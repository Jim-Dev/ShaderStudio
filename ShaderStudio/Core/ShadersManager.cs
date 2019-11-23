using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenGL;

namespace ShaderStudio.Core
{
    public sealed class ShadersManager
    {
        public const string VERTEX_SHADER_EXTENSION = ".vert";
        public const string TESSELLATION_CONTROL_SHADER_EXTENSION = ".tesc";
        public const string TESSELLATION_EVALUATION_SHADER_EXTENSION = ".tese";
        public const string GEOMETRY_SHADER_EXTENSION = ".geom";
        public const string FRAGMENT_SHADER_EXTENSION = ".frag";
        public const string COMPUTE_SHADER_EXTENSION = ".comp";

        public const string SHADERS_FOLDER = "Resources/Shaders";

        public const string CURRENT_SHADER_FILENAME = "Current";
        public const string DEFAULT_SHADER_FILENAME = "Default";


        private Dictionary<string, ShaderStage> LoadedShaders;//ShaderName,Shader

        public event EventHandler CompilationError;
        public event EventHandler CompilationSuccess;

        private static volatile ShadersManager instance;
        private static object syncRoot = new Object();

        private readonly StringBuilder infoLog;
        public string InfoLog
        {
            get { return this.infoLog.ToString(); }
        }

        public static ShadersManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ShadersManager();
                    }
                }

                return instance;
            }
        }

        public ShadersManager()
        {
            LoadedShaders = new Dictionary<string, ShaderStage>();
            Initialize();
            infoLog = new StringBuilder();
        }

        private void Initialize()
        {
            string currentVertPath = Path.Combine(SHADERS_FOLDER, CURRENT_SHADER_FILENAME + "Vertex" + VERTEX_SHADER_EXTENSION);
            string currentFragPath = Path.Combine(SHADERS_FOLDER, CURRENT_SHADER_FILENAME + "Fragment" + FRAGMENT_SHADER_EXTENSION);

            if (!File.Exists(currentVertPath))
                File.Copy(Path.Combine(SHADERS_FOLDER, DEFAULT_SHADER_FILENAME + "Vertex" + VERTEX_SHADER_EXTENSION),
                    Path.Combine(SHADERS_FOLDER, CURRENT_SHADER_FILENAME + "Vertex" + VERTEX_SHADER_EXTENSION));

            if (!File.Exists(currentFragPath))
                File.Copy(Path.Combine(SHADERS_FOLDER, DEFAULT_SHADER_FILENAME + "Fragment" + FRAGMENT_SHADER_EXTENSION),
                    Path.Combine(SHADERS_FOLDER, CURRENT_SHADER_FILENAME + "Fragment" + FRAGMENT_SHADER_EXTENSION));
        }

        public void Reset()
        {
            LoadedShaders.Clear();
        }

        public void ReloadShaders()
        {
            if (Directory.Exists(Utils.GetRelativePathString(SHADERS_FOLDER)))
            {
                Reset();
                foreach (string filePath in Directory.GetFiles(Utils.GetRelativePathString(SHADERS_FOLDER), "*", SearchOption.AllDirectories))
                {
                    ShaderStage tmpShader;
                    string fileName = Path.GetFileName(filePath);
                    switch (Path.GetExtension(fileName))
                    {

                        case VERTEX_SHADER_EXTENSION:
                            tmpShader = ShaderStage.LoadFromFile(fileName, ShaderType.VertexShader);
                            LoadedShaders.Add(tmpShader.ShaderName, tmpShader);
                            break;
                        case TESSELLATION_CONTROL_SHADER_EXTENSION:
                            tmpShader = ShaderStage.LoadFromFile(fileName, ShaderType.TessControlShader);
                            LoadedShaders.Add(tmpShader.ShaderName, tmpShader);
                            break;
                        case TESSELLATION_EVALUATION_SHADER_EXTENSION:
                            tmpShader = ShaderStage.LoadFromFile(fileName, ShaderType.TessEvaluationShader);
                            LoadedShaders.Add(tmpShader.ShaderName, tmpShader);
                            break;
                        case GEOMETRY_SHADER_EXTENSION:
                            tmpShader = ShaderStage.LoadFromFile(fileName, ShaderType.GeometryShader);
                            LoadedShaders.Add(tmpShader.ShaderName, tmpShader);
                            break;
                        case FRAGMENT_SHADER_EXTENSION:
                            tmpShader = ShaderStage.LoadFromFile(fileName, ShaderType.FragmentShader);
                            LoadedShaders.Add(tmpShader.ShaderName, tmpShader);
                            break;
                        case COMPUTE_SHADER_EXTENSION:
                            tmpShader = ShaderStage.LoadFromFile(fileName, ShaderType.ComputeShader);
                            LoadedShaders.Add(tmpShader.ShaderName, tmpShader);
                            break;
                        default:
                            break;
                    }

                }
            }
            else
            {
                Directory.CreateDirectory(Utils.GetRelativePathString(SHADERS_FOLDER));
            }
        }

        public void SaveShader(string shaderName)
        {
            ShaderStage shader = GetShaderByName(shaderName);
            string ShaderFullPath = string.Format(@"{0}\{1}{2}", Utils.GetRelativePathString(SHADERS_FOLDER),
                shader.ShaderName, GetShaderExtension(shader.Stage));
            shader.SaveToFile(ShaderFullPath);
        }

        public void UpdateShaderSource(string shaderName, string[] newShaderSource)
        {
            ShaderStage shader = GetShaderByName(shaderName);
            shader.ShaderSource = newShaderSource;
        }

        public ShaderStage GetFirstShaderByType(ShaderType shaderType)
        {
            ShaderStage output = GetAllShadersByType(shaderType).First();
            int i = 2;
            i = i * i;
            return output;
        }

        public List<ShaderStage> GetAllShadersByType(ShaderType shadersType)
        {
            List<ShaderStage> output = new List<ShaderStage>();

            foreach (ShaderStage shader in LoadedShaders.Values)
            {
                if (shader.Stage == shadersType)
                    output.Add(shader);
            }

            return output;
        }

        public ShaderStage GetShaderByName(string shaderName)
        {
            if (LoadedShaders.ContainsKey(shaderName))
                return LoadedShaders[shaderName];
            else
                return null;

        }

        public string GetShaderExtension(ShaderType shaderType)
        {
            switch (shaderType)
            {
                case ShaderType.ComputeShader:
                    return COMPUTE_SHADER_EXTENSION;
                case ShaderType.FragmentShader:
                    return FRAGMENT_SHADER_EXTENSION;
                case ShaderType.GeometryShader:
                    return GEOMETRY_SHADER_EXTENSION;
                case ShaderType.TessControlShader:
                    return TESSELLATION_CONTROL_SHADER_EXTENSION;
                case ShaderType.TessEvaluationShader:
                    return TESSELLATION_EVALUATION_SHADER_EXTENSION;
                case ShaderType.VertexShader:
                    return VERTEX_SHADER_EXTENSION;
                default:
                    return string.Empty;
            }
        }

        public string GetFullShaderName(ShaderStage shader)
        {
            return string.Format("{0}{1}", shader.ShaderName, GetShaderExtension(shader.Stage));
        }
        public string GetFullShaderName(string shaderName)
        {
            ShaderStage outputShader;
            outputShader = GetShaderByName(shaderName);
            if (outputShader != null)

                return GetFullShaderName(outputShader);
            else
                return string.Empty;
        }
        public ShaderProgram GetDefaultShader()
        {
            return BuildShaderProgram(new string[] { "NoneVertex", "NoneFragment" });
        }

        public ShaderProgram BuildShaderProgram(string[] shaderNames)
        {
            ReloadShaders();
            ShaderProgram output = new ShaderProgram(shaderNames);
            if (output.HasCompilationError)
            {
                infoLog.Clear();
                infoLog.Append(output.InfoLog);
                CompilationError?.Invoke(this,EventArgs.Empty);
                return null;
            }
            else
            {
                CompilationSuccess?.Invoke(this, EventArgs.Empty);
                return output;
            }
        }
    }
}