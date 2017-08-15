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


        public virtual string DEFAULT_NAME
        {
            get { return "UnnamedObject"; }
     
        }

        public virtual string Name
        {
            get
            {
                if (this.name == string.Empty)
                    this.name = DEFAULT_NAME;
                return this.name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    this.name = DEFAULT_NAME;
                else
                    this.name = value;
            }

        }

        private string name = string.Empty;

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
