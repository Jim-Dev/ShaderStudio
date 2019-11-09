using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShaderStudio.Core;

using XNA = Microsoft.Xna.Framework;
using OpenGL;

namespace ShaderStudio.Objects.Primitives
{
    public class Cube : Primitive
    {
        #region Vertices
        public float[] vertices = new float[]
       {
            //position              //vertex color      //normal                //vertex coord
            -0.5f, -0.5f, -0.5f,    1.0f, 0.0f, 0.0f,   0.0f,  0.0f, -1.0f,     0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,    1.0f, 0.0f, 0.0f,   0.0f,  0.0f, -1.0f,     1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,    1.0f, 0.0f, 0.0f,   0.0f,  0.0f, -1.0f,     1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,    1.0f, 0.0f, 0.0f,   0.0f,  0.0f, -1.0f,     0.0f, 1.0f,
                                                        
            -0.5f, -0.5f,  0.5f,    0.0f, 1.0f, 0.0f,   0.0f,  0.0f, 1.0f,      0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,    0.0f, 1.0f, 0.0f,   0.0f,  0.0f, 1.0f,      1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,    0.0f, 1.0f, 0.0f,   0.0f,  0.0f, 1.0f,      1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,    0.0f, 1.0f, 0.0f,   0.0f,  0.0f, 1.0f,      0.0f, 1.0f,
                                                         
            -0.5f,  0.5f,  0.5f,    0.0f, 0.0f, 1.0f,   -1.0f,  0.0f,  0.0f,    1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,    0.0f, 0.0f, 1.0f,   -1.0f,  0.0f,  0.0f,    1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,    0.0f, 0.0f, 1.0f,   -1.0f,  0.0f,  0.0f,    0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,    0.0f, 0.0f, 1.0f,   -1.0f,  0.0f,  0.0f,    0.0f, 0.0f,
                                                         
             0.5f,  0.5f,  0.5f,    1.0f, 0.0f, 1.0f,   1.0f,  0.0f,  0.0f,     1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,    1.0f, 0.0f, 1.0f,   1.0f,  0.0f,  0.0f,     1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,    1.0f, 0.0f, 1.0f,   1.0f,  0.0f,  0.0f,     0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,    1.0f, 0.0f, 1.0f,   1.0f,  0.0f,  0.0f,     0.0f, 0.0f,
                                                         
            -0.5f, -0.5f, -0.5f,    0.0f, 1.0f, 1.0f,   0.0f, -1.0f,  0.0f,     0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,    0.0f, 1.0f, 1.0f,   0.0f, -1.0f,  0.0f,     1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,    0.0f, 1.0f, 1.0f,   0.0f, -1.0f,  0.0f,     1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,    0.0f, 1.0f, 1.0f,   0.0f, -1.0f,  0.0f,     0.0f, 0.0f,
                                                         
            -0.5f,  0.5f, -0.5f,    1.0f, 1.0f, 0.0f,   0.0f,  1.0f,  0.0f,     0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,    1.0f, 1.0f, 0.0f,   0.0f,  1.0f,  0.0f,     1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,    1.0f, 1.0f, 0.0f,   0.0f,  1.0f,  0.0f,     1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,    1.0f, 1.0f, 0.0f,   0.0f,  1.0f,  0.0f,     0.0f, 0.0f
       };
        #endregion
        #region Indices
        public uint[] indices = new uint[]
        {
            0,1,2,
            2,3,0,

            4,5,6,
            6,7,4,

            8,9,10,
            10,11,8,

            12,13,14,
            14,15,12,

            16,17,18,
            18,19,16,

            20,21,22,
            22,23,20
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

        public Cube()
             : base()
        {
            Reload();

        }

        public override void Render(XNA.Matrix ViewMatrix, XNA.Matrix ProjectionMatrix)
        {
            if (ShaderProgram!=null)
            {
                base.Render(ViewMatrix, ProjectionMatrix);
                Gl.DrawElements(PrimitiveType.Triangles, 36, DrawElementsType.UnsignedInt, IntPtr.Zero);

                Gl.BindVertexArray(0);
            }
        }
    }
}
