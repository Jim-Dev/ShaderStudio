using OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShaderStudio.Core
{
    public class ImageTexture
    {
        public uint TexturePointer;

        private IntPtr bmpPointer;
        private System.Drawing.Imaging.BitmapData bmpData;

        private Bitmap imageBitmap;

        public void Lock()
        {
            bmpData =
               imageBitmap.LockBits(new Rectangle(0, 0, imageBitmap.Width, imageBitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite,
               imageBitmap.PixelFormat);

            bmpPointer = bmpData.Scan0;
        }

        public void Unlock()
        {
            imageBitmap.UnlockBits(bmpData);
            bmpPointer = IntPtr.Zero;
        }
        /*
        public ImageTexture(string fileName)
        {
            //ImageTexture(fileName, RotateFlipType.RotateNoneFlipNone);
        }
        */
        public ImageTexture(string fileName, RotateFlipType flipType)
        {
            string imagePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Textures", fileName);
            if (File.Exists(imagePath))
            {
                imageBitmap = (Bitmap)Image.FromFile(imagePath);
                imageBitmap.RotateFlip(flipType);
                TexturePointer = Gl.GenTexture();
                Bind();
                Gl.TexParameterI(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, new int[] { Gl.REPEAT });
                Gl.TexParameterI(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, new int[] { Gl.REPEAT });
                Gl.TexParameterI(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, new int[] { Gl.LINEAR });
                Gl.TexParameterI(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, new int[] { Gl.LINEAR });




                Lock();
                if (Bitmap.IsAlphaPixelFormat(imageBitmap.PixelFormat))
                    Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, imageBitmap.Width, imageBitmap.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, bmpPointer);
                else
                    Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgb, imageBitmap.Width, imageBitmap.Height, 0, PixelFormat.Bgr, PixelType.UnsignedByte, bmpPointer);

                Gl.GenerateMipmap(TextureTarget.Texture2d);

                Unlock();
            }


        }

        public void Bind()
        {
            // Lock();
            Gl.BindTexture(TextureTarget.Texture2d, TexturePointer);
            // Unlock();
        }

        public void Release()
        {
            imageBitmap.Dispose();
        }
        public static byte[] ImageToByte2(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
