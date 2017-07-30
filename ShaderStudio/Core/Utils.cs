using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ShaderStudio.Core
{
    public static class Utils
    {
        public static string GetRelativePathString(string path)
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path);
        }
        public static bool TryParseVector3(string data, out Vector3 vector)
        {
            bool output = false;
            vector = Vector3.Zero;
            try
            {
                string[] stringValues = data.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                if (stringValues.Length != 3)
                    output = false;
                else
                {
                    float x, y, z;
                    if (float.TryParse(stringValues[0], out x) && float.TryParse(stringValues[1], out y) && float.TryParse(stringValues[2], out z))
                    {
                        vector = new Vector3(x, y, z);
                        output = true;
                    }
                }
            }
            catch (Exception)
            {
                output = false;
            }
            return output;
        }
    }
}