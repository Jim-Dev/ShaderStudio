using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShaderStudio.Objects.Lights
{
    public class DirectionalLight:Light
    {
        public Vector3 Direction { get; set; }

        public DirectionalLight(Color lightColor, float lightIntensity)
        : base(lightColor, lightIntensity)
        {
            LightType = eLightType.Directional;
            Name = "DirectionalLight";
        }
        public DirectionalLight(Color lightColor)
            : base(lightColor)
        {
            LightType = eLightType.Directional;
            Name = "DirectionalLight";
        }
        public DirectionalLight()
            : base()
        {
            LightType = eLightType.Directional;
            Name = "DirectionalLight";
        }
    }
}
