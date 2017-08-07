using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShaderStudio.Core;
using OpenGL;
using XNA = Microsoft.Xna.Framework;

namespace ShaderStudio.Objects.Primitives
{
    public class Primitive : Renderable
    {
        public virtual float[] Vertices { get; }
        public virtual uint[] Indices { get; }



        public Primitive(ShaderProgram shaderProgram)
           : base(shaderProgram)
        {
            ObjectType = eObjectType.Entity;
        }

        public Primitive()
           : base()
        {
            ObjectType = eObjectType.Entity;
        }

        public override void Render(XNA.Matrix ViewMatrix, XNA.Matrix ProjectionMatrix)
        {
            ShaderProgram.Use();
            Gl.BindVertexArray(VAO);


            SetMVP(ViewMatrix, ProjectionMatrix);
        }

        public override void SetBuffers()
        {
            vAO = Gl.GenVertexArray();
            vBO = Gl.GenBuffer();
            eBO = Gl.GenBuffer();

            Gl.BindVertexArray(VAO);

            Gl.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            Gl.BufferData(BufferTarget.ArrayBuffer, ((uint)Vertices.Length) * sizeof(float), Vertices, BufferUsage.StaticDraw);

            Gl.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            Gl.BufferData(BufferTarget.ElementArrayBuffer, ((uint)Indices.Length) * sizeof(uint), Indices, BufferUsage.StaticDraw);

            Gl.VertexAttribPointer(0, 3, VertexAttribType.Float, false, 8 * sizeof(float), IntPtr.Zero);
            Gl.EnableVertexAttribArray(0);

            Gl.VertexAttribPointer(1, 3, VertexAttribType.Float, false, 8 * sizeof(float), IntPtr.Zero + 3 * sizeof(float));
            Gl.EnableVertexAttribArray(1);

            Gl.VertexAttribPointer(2, 2, VertexAttribType.Float, false, 8 * sizeof(float), IntPtr.Zero + 6 * sizeof(float));
            Gl.EnableVertexAttribArray(2);

            Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);
            Gl.BindVertexArray(0);

        }
        public override void SetProgram()
        {

            if (RegisteredStages.Count == 0)

                shaderProgram = ShadersManager.Instance.GetDefaultShader();
            else
                shaderProgram = ShadersManager.Instance.BuildShaderProgram(RegisteredStages.ToArray());
          
        }

    }
}
