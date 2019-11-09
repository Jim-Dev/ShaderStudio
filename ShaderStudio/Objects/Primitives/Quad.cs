using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XNA = Microsoft.Xna.Framework;

namespace ShaderStudio.Objects.Primitives
{
    public class Quad:Primitive
    {
        float[] vertices = new float[] {
            //position          //vertex color           //normal               // texture coords
             0.5f,  0.5f, 0.0f,   1.0f, 0.0f, 0.0f,      0.0f,  0.0f, 1.0f,     1.0f, 1.0f,   // top right
             0.5f, -0.5f, 0.0f,   0.0f, 1.0f, 0.0f,      0.0f,  0.0f, 1.0f,     1.0f, 0.0f,   // bottom right
            -0.5f, -0.5f, 0.0f,   0.0f, 0.0f, 1.0f,      0.0f,  0.0f, 1.0f,     0.0f, 0.0f,   // bottom left
            -0.5f,  0.5f, 0.0f,   1.0f, 1.0f, 0.0f,      0.0f,  0.0f, 1.0f,     0.0f, 1.0f    // top left 

        };
        uint[] indices = new uint[] {  // note that we start from 0!
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
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

        public Quad()
            :base()
        {
            Reload();
        }

       
        public override void Render(XNA.Matrix ViewMatrix, XNA.Matrix ProjectionMatrix)
        {
            base.Render(ViewMatrix, ProjectionMatrix);
            Gl.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, IntPtr.Zero);

            Gl.BindVertexArray(0);
        }

    }
}
