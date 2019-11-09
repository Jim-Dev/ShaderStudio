using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaderStudio.Core
{
    public static class Constants
    {
        public static class ShaderConstants
        {
            public const string SHADER_PARAM_LIT_CAMERA_POSITION = "CameraPosition";

            public const string SHADER_PARAM_LIT_AMBIENT_COLOR = "L_AmbientColor";
            public const string SHADER_PARAM_LIT_AMBIENT_INTENSITY = "L_AmbientIntensity";

            public const string SHADER_PARAM_LIT_SIMPLELIGHT_COLOR = "L_SimpleLightColor";
            public const string SHADER_PARAM_LIT_SIMPLELIGHT_INTENSITY = "L_SimpleLightIntensity";
            public const string SHADER_PARAM_LIT_SIMPLELIGHT_POSITION = "L_SimpleLightPosition";

        }
    }
}
