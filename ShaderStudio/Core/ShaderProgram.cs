using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XNA = Microsoft.Xna.Framework;

namespace ShaderStudio.Core
{
    public class ShaderProgram : ShaderBase
    {

        private bool hasCompilationError = false;

        public bool HasCompilationError
        {
            get { return hasCompilationError; }
            private set { this.hasCompilationError = value; }
        }

        public enum eMatrixType
        {
            Matrix2,
            Matrix2x3,
            Matrix2x4,
            Matrix3,
            Matrix3x2,
            Matrix3x4,
            Matrix4,
            Matrix4x2,
            Matrix4x3
        }

        private List<string> registeredShaders;

        #region properties
        public uint ProgramID
        {
            get { return objectId; }
        }

        public string ProgramName
        {
            get { return name; }
        }
        public int LinkStatus
        {
            get { return base.actionStatus; }
        }

        public List<string> RegisteredShaders
        {
            get { return this.registeredShaders; }
        }
        #endregion

        public ShaderProgram()
        {
            infoLog = new StringBuilder();
            registeredShaders = new List<string>();
            BuildShaderProgram();
        }

        public ShaderProgram(string[] shaderStageNames)
        {
            infoLog = new StringBuilder();
            registeredShaders = new List<string>();
            registeredShaders.AddRange(shaderStageNames);
            BuildShaderProgram();
        }

        private void BuildShaderProgram()
        {
            this.objectId = Gl.CreateProgram();

            foreach (string shaderStage in registeredShaders)
            {
                ShaderStage tmpShader = ShadersManager.Instance.GetShaderByName(shaderStage);
                if (tmpShader.CompileShader())
                {
                    AttachShader(tmpShader);
                }
                else
                {
                    HasCompilationError = true;
                    infoLog.Clear();
                    infoLog.Append(tmpShader.InfoLog);
                }
            }
            LinkProgram();
            //Delete tmpShader somehow
        }


        private void AttachShader(uint shaderID)
        {
            Gl.AttachShader(ProgramID, shaderID);
        }

        private void AttachShader(ShaderStage shader)
        {
            AttachShader(shader.ShaderID);
        }

        private void LinkProgram()
        {
            Gl.LinkProgram(ProgramID);


            StringBuilder infoLog = new StringBuilder("_PROGRAM_", 1024);
            int linkStatus;
            Gl.GetProgram(ProgramID, Gl.LINK_STATUS, out linkStatus);
            int x;
            if (linkStatus != Gl.TRUE)
            {
                Gl.GetShaderInfoLog(ProgramID, 1024, out x, infoLog);
            }
            //Console.WriteLine(infoLog);
            //PrintUniforms();
            //PrintAttributes();
        }
        public void Use()
        {
            Gl.UseProgram(ProgramID);
        }

        public void SetBool(string name, bool value)
        {
            if (value)
                Gl.Uniform1(Gl.GetUniformLocation(ProgramID, name), 1);
            else
                Gl.Uniform1(Gl.GetUniformLocation(ProgramID, name), 0);
        }
        public void SetInt(string name, int value)
        {
            int loc = Gl.GetUniformLocation(ProgramID, name);
            Gl.Uniform1(loc, value);
        }
        public void SetFloat(string name, float value)
        {
            Gl.Uniform1(Gl.GetUniformLocation(ProgramID, name), value);
        }
        public void SetVector(string name, float valueX, float valueY)
        {
            Gl.Uniform2(Gl.GetUniformLocation(ProgramID, name), valueX, valueY);
        }
        public void SetVector(string name, float valueX, float valueY, float valueZ)
        {
            Gl.Uniform3(Gl.GetUniformLocation(ProgramID, name), valueX, valueY, valueZ);
        }
        public void SetVector(string name, float valueX, float valueY, float valueZ, float valueW)
        {
            Gl.Uniform4(Gl.GetUniformLocation(ProgramID, name), valueX, valueY, valueZ, valueW);
        }
        public void SetVector(string name, XNA.Vector2 vector)
        {
            SetVector(name, vector.X, vector.Y);
        }
        public void SetVector(string name, XNA.Vector3 vector)
        {
            SetVector(name, vector.X, vector.Y, vector.Z);
        }
        public void SetVector(string name, XNA.Vector4 vector)
        {
            SetVector(name, vector.X, vector.Y, vector.Z, vector.W);
        }
        public void SetVector(string name, XNA.Color color, bool useAlpha)
        {
            if (useAlpha)
                SetVector(name, color.ToVector4());
            else
                SetVector(name, color.ToVector3());
        }
        public void SetMatrix(string name, int count, bool transpose, float[] values, eMatrixType matrixType)
        {
            switch (matrixType)
            {
                case eMatrixType.Matrix2:
                    Gl.UniformMatrix2(Gl.GetUniformLocation(ProgramID, name), count, transpose, values);
                    break;
                case eMatrixType.Matrix2x3:
                    Gl.UniformMatrix2x3(Gl.GetUniformLocation(ProgramID, name), count, transpose, values);
                    break;
                case eMatrixType.Matrix2x4:
                    Gl.UniformMatrix2x4(Gl.GetUniformLocation(ProgramID, name), count, transpose, values);
                    break;
                case eMatrixType.Matrix3:
                    Gl.UniformMatrix3(Gl.GetUniformLocation(ProgramID, name), count, transpose, values);
                    break;
                case eMatrixType.Matrix3x2:
                    Gl.UniformMatrix3x2(Gl.GetUniformLocation(ProgramID, name), count, transpose, values);
                    break;
                case eMatrixType.Matrix3x4:
                    Gl.UniformMatrix3x4(Gl.GetUniformLocation(ProgramID, name), count, transpose, values);
                    break;
                case eMatrixType.Matrix4:
                    Gl.UniformMatrix4(Gl.GetUniformLocation(ProgramID, name), count, transpose, values);
                    break;
                case eMatrixType.Matrix4x2:
                    Gl.UniformMatrix4x2(Gl.GetUniformLocation(ProgramID, name), count, transpose, values);
                    break;
                case eMatrixType.Matrix4x3:
                    Gl.UniformMatrix4x3(Gl.GetUniformLocation(ProgramID, name), count, transpose, values);
                    break;
                default:
                    break;
            }


        }

        public void SetMatrix(string name, int count, bool transpose, XNA.Matrix values, eMatrixType matrixType)
        {
            switch (matrixType)
            {
                case eMatrixType.Matrix2:
                    SetMatrix(name, count, transpose, new float[] { values.M11, values.M12, values.M21, values.M22 }, matrixType);
                    break;
                case eMatrixType.Matrix2x3:
                    SetMatrix(name, count, transpose, new float[] { values.M11, values.M12, values.M21, values.M22, values.M31, values.M32 }, matrixType);
                    break;
                case eMatrixType.Matrix2x4:
                    SetMatrix(name, count, transpose, new float[] { values.M11, values.M12, values.M21, values.M22, values.M31, values.M32, values.M41, values.M42 }, matrixType);
                    break;
                case eMatrixType.Matrix3:
                    SetMatrix(name, count, transpose, new float[] { values.M11, values.M12, values.M13, values.M21, values.M22, values.M23, values.M31, values.M32, values.M33, values.M41, values.M42, values.M43 }, matrixType);
                    break;
                case eMatrixType.Matrix3x2:
                    SetMatrix(name, count, transpose, new float[] { values.M11, values.M12, values.M13, values.M21, values.M22, values.M23, values.M31, values.M32, values.M33 }, matrixType);
                    break;
                case eMatrixType.Matrix3x4:
                    SetMatrix(name, count, transpose, new float[] { values.M11, values.M12, values.M13, values.M21, values.M22, values.M23, values.M31, values.M32, values.M33, values.M41, values.M42, values.M43 }, matrixType);
                    break;
                case eMatrixType.Matrix4:
                    SetMatrix(name, count, transpose, new float[] { values.M11, values.M12, values.M13, values.M14, values.M21, values.M22, values.M23, values.M24, values.M31, values.M32, values.M33, values.M34, values.M41, values.M42, values.M43, values.M44 }, matrixType);
                    break;
                case eMatrixType.Matrix4x2:
                    SetMatrix(name, count, transpose, new float[] { values.M11, values.M12, values.M13, values.M14, values.M21, values.M22, values.M23, values.M24 }, matrixType);
                    break;
                case eMatrixType.Matrix4x3:
                    SetMatrix(name, count, transpose, new float[] { values.M11, values.M12, values.M13, values.M14, values.M21, values.M22, values.M23, values.M24, values.M31, values.M32, values.M33, values.M34 }, matrixType);
                    break;
                default:
                    break;
            }
        }

        public List<string> GetAttributeNames()
        {

            List<string> output = new List<string>();

            int count, lenght, size, type;//https://stackoverflow.com/questions/440144/in-opengl-is-there-a-way-to-get-a-list-of-all-uniforms-attribs-used-by-a-shade
            StringBuilder name = new StringBuilder(1024);
            Gl.GetProgram(ProgramID, Gl.ACTIVE_ATTRIBUTES, out count);
            for (uint i = 0; i < count; i++)
            {
                Gl.GetActiveAttrib(ProgramID, i, 1024, out lenght, out size, out type, name);
                output.Add(name.ToString());
            }
            return output;
        }
        public List<string> GetUniformNames()
        {
            List<string> output = new List<string>();

            int count, lenght, size, type;//https://stackoverflow.com/questions/440144/in-opengl-is-there-a-way-to-get-a-list-of-all-uniforms-attribs-used-by-a-shade
            StringBuilder name = new StringBuilder(1024);
            Gl.GetProgram(ProgramID, Gl.ACTIVE_UNIFORMS, out count);
            for (uint i = 0; i < count; i++)
            {
                Gl.GetActiveUniform(ProgramID, i, 1024, out lenght, out size, out type, name);
                output.Add(name.ToString());
            }

            return output;
        }
        public void PrintUniforms()
        {
            Console.WriteLine("== UNIFORM NAMES ==");
            foreach (string uniformName in GetUniformNames())
            {
                Console.WriteLine(uniformName);
            }
            Console.WriteLine("===================");
        }
        public void PrintAttributes()
        {
            Console.WriteLine("== ATTRIBUTES NAMES ==");
            foreach (string attributeName in GetAttributeNames())
            {
                Console.WriteLine(attributeName);
            }
            Console.WriteLine("======================");
        }
    }
}
