using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace ShaderStudio.Objects.Lights
{
    public class AmbientLight : Light
    {
        public AmbientLight(Color lightColor, float lightIntensity)
         : base(lightColor, lightIntensity)
        {
            LightType = eLightType.Ambient;
            Name = "AmbientLight";
        }
        public AmbientLight(Color lightColor)
            : base(lightColor)
        {
            LightType = eLightType.Ambient;
            Name = "AmbientLight";
        }
        public AmbientLight()
            : base()
        {
            LightType = eLightType.Ambient;
            Name = "AmbientLight";
        }
    }
}