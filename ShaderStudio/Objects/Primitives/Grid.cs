using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XNA = Microsoft.Xna.Framework;

namespace ShaderStudio.Objects.Primitives
{

    public class Grid:Primitive
    {

        #region Vertices
        public float[] vertices = new float[]
       {


           -5f,  0f, -5f,   1.0f, 1.0f, 1.0f,
            5f,  0f, -5f,   1.0f, 1.0f, 1.0f,
            -5f,  0f, 5f,   1.0f, 1.0f, 1.0f,
            5f, 0f, 5f,     1.0f, 1.0f, 1.0f,

            -5f,  0f, 0f,   1.0f, 1.0f, 1.0f,
            5f, 0f, 0f,     1.0f, 1.0f, 1.0f,
            0f,  0f, -5f,   1.0f, 1.0f, 1.0f,
            0f, 0f, 5f,     1.0f, 1.0f, 1.0f,

            -5f,  0f, -1f, 0.5f, 0.5f, 0.5f,
            5f, 0f, -1f,   0.5f, 0.5f, 0.5f,
           -5f,  0f, -2f,  0.5f, 0.5f, 0.5f,
            5f, 0f, -2f,   0.5f, 0.5f, 0.5f,
            -5f,  0f, -3f, 0.5f, 0.5f, 0.5f,
            5f, 0f, -3f,   0.5f, 0.5f, 0.5f,
            -5f,  0f, -4f, 0.5f, 0.5f, 0.5f,
            5f, 0f, -4f,   0.5f, 0.5f, 0.5f,

            -5f,  0f, 1f,  0.5f, 0.5f, 0.5f,
            5f, 0f, 1f,    0.5f, 0.5f, 0.5f,
           -5f,  0f, 2f,   0.5f, 0.5f, 0.5f,
            5f, 0f, 2f,    0.5f, 0.5f, 0.5f,
            -5f,  0f, 3f,  0.5f, 0.5f, 0.5f,
            5f, 0f, 3f,    0.5f, 0.5f, 0.5f,
            -5f,  0f, 4f,  0.5f, 0.5f, 0.5f,
            5f, 0f, 4f,    0.5f, 0.5f, 0.5f,

            1f,  0f, -5f,  0.5f, 0.5f, 0.5f,
            1f, 0f, 5f,    0.5f, 0.5f, 0.5f,
            2f,  0f, -5f,  0.5f, 0.5f, 0.5f,
            2f, 0f, 5f,    0.5f, 0.5f, 0.5f,
            3f,  0f, -5f,  0.5f, 0.5f, 0.5f,
            3f, 0f, 5f,    0.5f, 0.5f, 0.5f,
            4f,  0f, -5f,  0.5f, 0.5f, 0.5f,
            4f, 0f, 5f,    0.5f, 0.5f, 0.5f,

            -1f,  0f, -5f, 0.5f, 0.5f, 0.5f,
            -1f, 0f, 5f,   0.5f, 0.5f, 0.5f,
            -2f,  0f, -5f, 0.5f, 0.5f, 0.5f,
            -2f, 0f, 5f,   0.5f, 0.5f, 0.5f,
            -3f,  0f, -5f, 0.5f, 0.5f, 0.5f,
            -3f, 0f, 5f,   0.5f, 0.5f, 0.5f,
            -4f,  0f, -5f, 0.5f, 0.5f, 0.5f,
            -4f, 0f, 5f,   0.5f, 0.5f, 0.5f,

       };
        #endregion
        #region Indices
        public uint[] indices = new uint[]
        {
            0,1,
            2,3,
            1,3,
            0,2,
            4,5,
            6,7,


            8,9,
            10,11,
            12,13,
            14,15,

            16,17,
            18,19,
            20,21,
            22,23,

            24,25,
            26,27,
            28,29,
            30,31,

            32,33,
            34,35,
            36,37,
            38,39
        };
        #endregion


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

        public Grid()
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
