using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ShaderStudio.Objects.Primitives;

namespace ShaderStudio.Objects.Lights
{
    public class Light : Renderable
    {
        public const string DEFAULT_LIGHT_VERTEX_STAGE = "LightVertex";
        public const string DEFAULT_LIGHT_FRAGMENT_STAGE = "LightFragment";
        public readonly Vector3 DEFAULT_LIGHT_GIZMO_SCALE = new Vector3(0.1f, 0.1f, 0.1f);

        public const string SHADER_PARAM_LIGHT_COLOR = "LightColor";
        public const string SHADER_PARAM_LIGHT_INTENSITY = "LightIntensity";

        private Primitives.Primitive gizmo;
        public Color LightColor = Color.White;
        public float LightIntensity = 1;
        public eLightType LightType { get; protected set; }

        public enum eLightType
        {
            None,
            Ambient,
            Point,
            Directional,
            Spot
        }

        public Light(Color lightColor, float lightIntensity)
          : base()
        {
            this.LightColor = lightColor;
            this.LightIntensity = lightIntensity;
            InitializeGizmo();
        }
        public Light(Color lightColor)
            : base()
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
            gizmo.RegisteredStages.Add(DEFAULT_LIGHT_VERTEX_STAGE);
            gizmo.RegisteredStages.Add(DEFAULT_LIGHT_FRAGMENT_STAGE);
            gizmo.Reload();
            gizmo.Scale = DEFAULT_LIGHT_GIZMO_SCALE;
        }

        public override void Render(Matrix ViewMatrix, Matrix ProjectionMatrix)
        {
            gizmo.Position = this.Position;
            gizmo.Render(ViewMatrix, ProjectionMatrix);
            gizmo.ShaderProgram.SetVector(SHADER_PARAM_LIGHT_COLOR, LightColor, false);
            gizmo.ShaderProgram.SetFloat(SHADER_PARAM_LIGHT_INTENSITY, LightIntensity);
        }
    }
}
