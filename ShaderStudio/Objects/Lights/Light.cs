using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ShaderStudio.Objects.Primitives;

namespace ShaderStudio.Objects.Lights
{
    public class Light:Renderable
    {
        private Primitives.Primitive gizmo;
        public Color LightColor = Color.White;
        public float LightIntensity = 1;

        public Light(Color lightColor, float lightIntensity)
          : base()
        {
            this.LightColor = lightColor;
            this.LightIntensity = lightIntensity;
            InitializeGizmo();
        }
        public Light(Color lightColor)
            :base()
        {
            this.LightColor = lightColor;
            LightIntensity = 1f;
            InitializeGizmo();
        }

        public Light()
            : base()
        {
            LightColor = Color.White;
            LightIntensity = 1f;
            InitializeGizmo();
        }

        private void InitializeGizmo()
        {
            gizmo = new Cube();
            gizmo.RegisteredStages.Clear();
            gizmo.RegisteredStages.Add("LightVertex");
            gizmo.RegisteredStages.Add("LightFragment");
            gizmo.Reload();
            gizmo.Scale = new Vector3(0.1f, 0.1f, 0.1f);
        }

        public override void Render(Matrix ViewMatrix, Matrix ProjectionMatrix)
        {

            gizmo.Position = this.Position;
            gizmo.Render(ViewMatrix, ProjectionMatrix);
            gizmo.ShaderProgram.SetVector("LightColor", LightColor);
            gizmo.ShaderProgram.SetFloat("LightIntensity", LightIntensity);
        }
    }
}
