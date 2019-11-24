using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ShaderStudio.Objects.Lights
{
    public class PointLight:Light
    {
        public const float DEFAULT_CONSTANT = 1f;
        public const float DEFAULT_LINEAR = 0.09f;
        public const float DEFAULT_QUADRATIC = 0.032f;

        private float constant = DEFAULT_CONSTANT;
        private float linear = DEFAULT_LINEAR;
        private float quadratic = DEFAULT_QUADRATIC;

        public float Constant
        {
            get { return constant; }
            set { constant = value; }
        }
        public float Linear
        {
            get { return linear; }
            set { linear = value; }
        }
        public float Quadratic
        {
            get { return quadratic; }
            set { quadratic = value; }
        }

        public PointLight(Color lightColor, float lightIntensity)
         : base(lightColor, lightIntensity)
        {
            LightType = eLightType.Point;
            Name = "PointLight";
        }
        public PointLight(Color lightColor)
            : base(lightColor)
        {
            LightType = eLightType.Point;
            Name = "PointLight";
        }
        public PointLight()
            : base()
        {
            LightType = eLightType.Point;
            Name = "PointLight";
        }
    }
}
