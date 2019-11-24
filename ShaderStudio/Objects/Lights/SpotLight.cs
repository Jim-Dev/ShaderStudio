using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShaderStudio.Objects.Lights
{
    public class SpotLight:Light
    {
        public Vector3 Direction { get; set; }
        public float InnerAngle { get; set; }
        public float OuterAngle { get; set; }

        public SpotLight(Color lightColor, float lightIntensity)
       : base(lightColor, lightIntensity)
        {
            LightType = eLightType.Spot;
            Name = "SpotLight";
        }
        public SpotLight(Color lightColor)
            : base(lightColor)
        {
            LightType = eLightType.Spot;
            Name = "SpotLight";
        }
        public SpotLight()
            : base()
        {
            LightType = eLightType.Spot;
            Name = "SpotLight";
        }
    }
}
