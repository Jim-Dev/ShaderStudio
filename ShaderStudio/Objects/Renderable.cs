using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShaderStudio.Core;
using Microsoft.Xna.Framework;

namespace ShaderStudio.Objects
{
    public class Renderable:SceneObject
    {
        protected ShaderProgram shaderProgram;

        public ShaderProgram ShaderProgram
        {
            get { return this.shaderProgram; }
        }

        public Renderable(ShaderProgram shaderProgram)
        {
            this.shaderProgram = shaderProgram;
        }

        public List<string> RegisteredStages = new List<string>();

        public Renderable()
        {
            //this.shaderProgram = new ShaderProgram();
            this.shaderProgram = ShadersManager.Instance.GetDefaultShader();
        }

        protected uint vAO;
        protected uint vBO;
        protected uint eBO;
        private uint vertexStride;

        public uint VAO
        {
            get { return this.vAO; }
        }
        public uint VBO
        {
            get { return this.vBO; }
        }
        public uint EBO
        {
            get { return this.eBO; }
        }

        public virtual void Render()
        {
            throw new NotImplementedException();
        }
        public virtual void Render(Matrix ViewMatrix, Matrix ProjectionMatrix)
        {
            throw new NotImplementedException();
        }
        public uint VertexStride
        {
            get { return this.vertexStride; }
            protected set { this.vertexStride = value; }
        }

        public virtual void SetBuffers() { throw new NotImplementedException(); }
        public virtual void SetProgram() { throw new NotImplementedException(); }
        public virtual void SetProgramParameters() { }
        public virtual void SetMVP(Matrix ViewMatrix, Matrix ProjectionMatrix)
        {
            UpdateTransform();
            ShaderProgram.SetMatrix("inverse_model", 1, false, InverseTransformMatrix, ShaderProgram.eMatrixType.Matrix4);
            ShaderProgram.SetMatrix("model", 1, false, TransformMatrix, ShaderProgram.eMatrixType.Matrix4);
            shaderProgram.SetMatrix("view", 1, false, ViewMatrix, ShaderProgram.eMatrixType.Matrix4);
            shaderProgram.SetMatrix("projection", 1, false, ProjectionMatrix, ShaderProgram.eMatrixType.Matrix4);
        }

        public void Reload()
        {
            SetBuffers();
            SetProgram();

            SetProgramParameters();
        }

        public void ModifyShader(ShaderProgram program)
        {
            Reload();
            this.shaderProgram = program;
        }
    }
}
