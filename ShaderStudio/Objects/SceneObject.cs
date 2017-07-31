using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaderStudio.Objects
{
    public class SceneObject:Transformation
    {
        public eObjectType ObjectType = eObjectType.Empty;

        public enum eObjectType
        {
            Empty,
            Camera,
            Light,
            Gizmo,
            Entity
        }

        public SceneObject()
            : base()
        {
            ObjectType = eObjectType.Empty;
        }
    }
}
