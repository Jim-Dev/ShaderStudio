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
        private Matrix inverseTransformMatrix = Matrix.Identity;

        public Matrix TransformMatrix
        {
            get { return this.transformMatrix; }
            private set { this.transformMatrix = value; }
        }
        public Matrix InverseTransformMatrix
        {
            get { return this.inverseTransformMatrix; }
            private set { this.inverseTransformMatrix = value; }
        }
        public Transformation()
        {
        }

        public void UpdateTransform()
        {
            TransformMatrix = GetTransformation();
            this.InverseTransformMatrix = Matrix.Invert(this.transformMatrix);
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
