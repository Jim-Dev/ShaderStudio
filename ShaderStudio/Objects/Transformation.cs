using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
namespace ShaderStudio.Objects
{
    public class Transformation
    {
        public Vector3 Position = Vector3.Zero;
        public Quaternion Rotation = Quaternion.Identity;
        public Vector3 Scale = Vector3.One;


        private Matrix transformMatrix = Matrix.Identity;

        public Matrix TransformMatrix
        {
            get
            {
                this.transformMatrix = GetTransformation();
                return this.transformMatrix;
            }
        }

        public Transformation()
        {
        }

        private Matrix GetTransformation()
        {
            Matrix output = Matrix.Identity;

            output *= Matrix.CreateScale(Scale);
            output *= Matrix.CreateFromQuaternion(Rotation);
            output *= Matrix.CreateTranslation(Position);

            return output;
        }
    }
}
