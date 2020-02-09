using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenGL;

using XNA = Microsoft.Xna.Framework;

namespace ShaderStudio.Objects.Primitives
{
    public class Cross : Primitive
    {
        float[] vertices = new float[] {
            //position              //vertex color 

             0.0f,    0.0f, 0.0f,   1.0f, 1.0f, 0.0f,  //0 Mid
                                                                                          
            -0.5f,   0.0f,  0.0f,   1.0f, 1.0f, 0.0f,   //1 Left
             0.5f,   0.0f,  0.0f,   1.0f, 1.0f, 0.0f,   //2 Right
             0.0f,  -0.5f,  0.0f,   1.0f, 1.0f, 0.0f,   //3 Bottom
             0.0f,   0.5f,  0.0f,   1.0f, 1.0f, 0.0f,   //4 Top

             0.0f,   0.0f, -0.5f,   1.0f, 1.0f, 0.0f,   //5 NearZ
             0.0f,   0.0f,  0.5f,   1.0f, 1.0f, 0.0f,   //6 FarZ

        };
        uint[] indices = new uint[] {
            1,0,
            2,0,
            4,0,
            3,0,
            5,0,
            6,0
        };

        public override float[] Vertices
        {
            get
            {
                return this.vertices;
            }
        }

        public override uint[] Indices
        {
            get
            {
                return this.indices;
            }
        }

        public Cross()
            : base()
        {
            RegisteredStages.Clear();
            RegisteredStages.Add("VertexColorVertex");
            RegisteredStages.Add("VertexColorFragment");
            Reload();
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

            Gl.VertexAttribPointer(0, 3, VertexAttribType.Float, false, 6 * sizeof(float), IntPtr.Zero);
            Gl.EnableVertexAttribArray(0);

            Gl.VertexAttribPointer(1, 3, VertexAttribType.Float, false, 6 * sizeof(float), IntPtr.Zero + 3 * sizeof(float));
            Gl.EnableVertexAttribArray(1);

            Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);
            Gl.BindVertexArray(0);
        }

        public override void Render(XNA.Matrix ViewMatrix, XNA.Matrix ProjectionMatrix)
        {
            base.Render(ViewMatrix, ProjectionMatrix);
            Gl.DrawElements(PrimitiveType.Lines, indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);

            Gl.BindVertexArray(0);
        }

    }
}
