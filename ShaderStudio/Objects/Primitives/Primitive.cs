using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShaderStudio.Core;
using OpenGL;
using XNA = Microsoft.Xna.Framework;
using System.Drawing;
using System.IO;

namespace ShaderStudio.Objects.Primitives
{
    public class Primitive : Renderable
    {
        private readonly object lock_object = new object();
        public const string DEFAULT_TEXTURE_MAIN_NAME = "vertical.png";
        public const string DEFAULT_TEXTURE_SECONDARY_NAME = "horizontal.png";
        public const int DEFAULT_TEXTURE_SLOT_MAX = 8;
        public virtual float[] Vertices { get; }
        public virtual uint[] Indices { get; }

        private string mainTextureName = string.Empty;
        private string secondaryTextureName = string.Empty;

        private ImageTexture mainImageTexture;
        private ImageTexture secondaryImageTexture;

        public Primitive(ShaderProgram shaderProgram)
           : base(shaderProgram)
        {
            ObjectType = eObjectType.Entity;
        }

        public Primitive()
           : base()
        {
            ObjectType = eObjectType.Entity;
        }

        public override void Render(XNA.Matrix ViewMatrix, XNA.Matrix ProjectionMatrix)
        {
            if (ShaderProgram != null)
            {
                ShaderProgram.Use();
                Gl.BindVertexArray(VAO);
                SetMVP(ViewMatrix, ProjectionMatrix);
            }
        }

        public override void SetBuffers()
        {
            vAO = Gl.GenVertexArray();
            vBO = Gl.GenBuffer();
            eBO = Gl.GenBuffer();

            Gl.BindVertexArray(VAO);

            Gl.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            Gl.BufferData(BufferTarget.ArrayBuffer, ((uint)Vertices.Length) * sizeof(float), Vertices, BufferUsage.StaticDraw);

            Gl.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            Gl.BufferData(BufferTarget.ElementArrayBuffer, ((uint)Indices.Length) * sizeof(uint), Indices, BufferUsage.StaticDraw);

            Gl.VertexAttribPointer(0, 3, VertexAttribType.Float, false, 8 * sizeof(float), IntPtr.Zero);
            Gl.EnableVertexAttribArray(0);

            Gl.VertexAttribPointer(1, 3, VertexAttribType.Float, false, 8 * sizeof(float), IntPtr.Zero + 3 * sizeof(float));
            Gl.EnableVertexAttribArray(1);

            Gl.VertexAttribPointer(2, 2, VertexAttribType.Float, false, 8 * sizeof(float), IntPtr.Zero + 6 * sizeof(float));
            Gl.EnableVertexAttribArray(2);

            Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);
            Gl.BindVertexArray(0);

        }
        public override void SetProgram()
        {

            if (RegisteredStages.Count == 0)
                shaderProgram = ShadersManager.Instance.GetDefaultShader();
            else
            {
                shaderProgram = ShadersManager.Instance.BuildShaderProgram(RegisteredStages.ToArray());
            }

            //---TODO: Fix bug on ImageTexture that prevents 
            for (int i = 0; i < DEFAULT_TEXTURE_SLOT_MAX; i++)
            {
                string texturePath = string.Format(ImageTexture.DEFAULT_TEXTURE_PATH_TEMPLATE, i);

                if (!File.Exists(texturePath))
                    File.Copy("Resources/Textures/128-128-128.png", texturePath);
            }
            
            ImageTexture tex0 = new ImageTexture(string.Format(ImageTexture.DEFAULT_TEXTURE_FILENAME_TEMPLATE, 0), RotateFlipType.RotateNoneFlipNone);
            ImageTexture tex1 = new ImageTexture(string.Format(ImageTexture.DEFAULT_TEXTURE_FILENAME_TEMPLATE, 1), RotateFlipType.RotateNoneFlipNone);
            ImageTexture tex2 = new ImageTexture(string.Format(ImageTexture.DEFAULT_TEXTURE_FILENAME_TEMPLATE, 2), RotateFlipType.RotateNoneFlipNone);
            ImageTexture tex3 = new ImageTexture(string.Format(ImageTexture.DEFAULT_TEXTURE_FILENAME_TEMPLATE, 3), RotateFlipType.RotateNoneFlipNone);
            ImageTexture tex4 = new ImageTexture(string.Format(ImageTexture.DEFAULT_TEXTURE_FILENAME_TEMPLATE, 4), RotateFlipType.RotateNoneFlipNone);
            ImageTexture tex5 = new ImageTexture(string.Format(ImageTexture.DEFAULT_TEXTURE_FILENAME_TEMPLATE, 5), RotateFlipType.RotateNoneFlipNone);
            ImageTexture tex6 = new ImageTexture(string.Format(ImageTexture.DEFAULT_TEXTURE_FILENAME_TEMPLATE, 6), RotateFlipType.RotateNoneFlipNone);
            ImageTexture tex7 = new ImageTexture(string.Format(ImageTexture.DEFAULT_TEXTURE_FILENAME_TEMPLATE, 7), RotateFlipType.RotateNoneFlipNone);

            Gl.ActiveTexture(TextureUnit.Texture0);
            tex0.Bind();
            tex0.Release();
            Gl.ActiveTexture(TextureUnit.Texture1);
            tex1.Bind();
            tex1.Release();
            Gl.ActiveTexture(TextureUnit.Texture2);
            tex2.Bind();
            tex2.Release();
            Gl.ActiveTexture(TextureUnit.Texture3);
            tex3.Bind();
            tex3.Release();
            Gl.ActiveTexture(TextureUnit.Texture4);
            tex4.Bind();
            tex4.Release();
            Gl.ActiveTexture(TextureUnit.Texture5);
            tex5.Bind();
            tex5.Release();
            Gl.ActiveTexture(TextureUnit.Texture6);
            tex6.Bind();
            tex6.Release();
            Gl.ActiveTexture(TextureUnit.Texture7);
            tex7.Bind();
            tex7.Release();
            //---

            //InitializeTextures();
        }

        private void InitializeTexture(int textureId)
        {
            string texturePath = string.Format(ImageTexture.DEFAULT_TEXTURE_PATH_TEMPLATE, textureId);

            if (!File.Exists(texturePath))
                File.Copy("Resources/Textures/128-128-128.png", texturePath);

            string textureFileName = string.Format(ImageTexture.DEFAULT_TEXTURE_FILENAME_TEMPLATE, textureId);
            ImageTexture tmpImageTexture = new ImageTexture(textureFileName, RotateFlipType.RotateNoneFlipNone);
            Console.WriteLine(tmpImageTexture.TexturePointer);
            TextureUnit tu = ImageTexture.TEXTURE_UNITS_MAPPING[textureId];
            Gl.ActiveTexture(tu);
            tmpImageTexture.Bind();
            tmpImageTexture.Release();
            Console.WriteLine(textureId);
            Console.WriteLine(tu);
            Console.WriteLine(textureFileName);
        }

        private void InitializeTextures()
        {
            for (int i = 0; i < DEFAULT_TEXTURE_SLOT_MAX; i++)
            {
                InitializeTexture(i);
            }
        }

        public override void SetProgramParameters()
        {
            if (ShaderProgram != null)
            {
                ShaderProgram.Use();
                
                for (int i = 0; i < DEFAULT_TEXTURE_SLOT_MAX; i++)
                {
                    string paramName = string.Format(ImageTexture.DEFAULT_TEXTURE_SHADER_PARAM_TEMPLATE, i);
                    ShaderProgram.SetInt(paramName, i);
                }
            }
        }

    }
}