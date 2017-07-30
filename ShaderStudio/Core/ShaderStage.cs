using OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShaderStudio.Core
{
    public class ShaderStage:ShaderBase
    {
        #region fields
        private string[] shaderSource;
        private ShaderType stage;

        private const string SUCCESSFUL_COMPILATION = "{0} shader successfully compiled with no errors";
        private const string UNSUCCESSFUL_COMPILATION = "=====\n{0} shader compiled with errors\n=====\n";
        #endregion

        #region properties
        public uint ShaderID
        {
            get { return objectId; }
        }
        public ShaderType Stage
        {
            get { return stage; }
        }
        public string ShaderName
        {
            get { return name; }
        }
        public int CompilationStatus
        {
            get { return this.actionStatus; }
        }
        #endregion

        public string[] ShaderSource
        {
            get { return this.shaderSource; }
            set
            {
                if (value != null)
                {
                    this.isDirty = true;
                    for (int i = 0; i < value.Length; i++)
                    {
                        if (!value[i].EndsWith(Environment.NewLine))
                            value[i] += Environment.NewLine;
                    }

                    this.shaderSource = value;
                    Gl.ShaderSource(ShaderID, this.shaderSource);
                }
            }
        }

        public ShaderStage(ShaderType shaderType, string shaderName)
        {
            objectId = Gl.CreateShader(shaderType);
            stage = shaderType;
            name = shaderName;
            infoLog = new StringBuilder("Shader log : ", 1024);
            infoLog.EnsureCapacity(1024);
        }



        public bool CompileShader()
        {
            int infoLogLenght;

            Gl.CompileShader(ShaderID);
            Gl.GetShader(ShaderID, ShaderParameterName.CompileStatus, out base.actionStatus);
            Gl.GetShader(ShaderID, ShaderParameterName.InfoLogLength, out infoLogLenght);

            infoLog = new StringBuilder(string.Format(UNSUCCESSFUL_COMPILATION, ShaderName), infoLogLenght);
            infoLog.EnsureCapacity(infoLogLenght);

            int x;
            if (CompilationStatus == Gl.TRUE)
            {
                this.isDirty = false;
                infoLog.Clear();
                infoLog.Append(string.Format(SUCCESSFUL_COMPILATION, ShaderName));
                return true;
            }
            else
            {
                Gl.GetShaderInfoLog(ShaderID, 1024, out x, infoLog);
                return false;
            }
        }

        public void DeleteShader()
        {
            Gl.DeleteShader(ShaderID);
        }

        public void PrintShaderSource()
        {
            Console.WriteLine("======");
            foreach (string shaderLine in ShaderSource)
            {
                Console.WriteLine(shaderLine.Replace(Environment.NewLine, string.Empty));
            }
            Console.WriteLine("======");
        }

        private StringBuilder GetInfoLog()
        {
            const int MaxInfoLength = 64 * 1024;        // 64 KB

            StringBuilder logInfo = new StringBuilder(MaxInfoLength);
            int logLength;

            Gl.GetInfoLogARB(ShaderID, MaxInfoLength - 1, out logLength, logInfo);
            StringBuilder sb = new StringBuilder(logInfo.Capacity);

            string[] compilerLogLines = logInfo.ToString().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            return (sb);
        }

        public static ShaderStage LoadFromFile(string fileName, ShaderType shaderType)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Shaders", fileName);
            if (File.Exists(path))
            {
                string[] fileContent = File.ReadAllLines(path);

                for (int i = 0; i < fileContent.Length; i++)
                {
                    fileContent[i] += Environment.NewLine;
                }

                string shaderName = Path.GetFileNameWithoutExtension(fileName);
                ShaderStage output = new ShaderStage(shaderType, shaderName);

                output.ShaderSource = fileContent;
                output.CompileShader();


                return output;
            }
            else return null;
        }

        public void SaveToFile(string path)
        {
            StringBuilder sBuilder = new StringBuilder();
            foreach (string shaderSourceLine in ShaderSource)
            {
                sBuilder.Append(shaderSourceLine);
            }
            string output = sBuilder.ToString();
            File.WriteAllText(path, output, Encoding.UTF8);
        }
    }
}
