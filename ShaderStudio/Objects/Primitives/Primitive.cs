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
        public const string DEFAULT_TEXTURE_MAIN_NAME = "vertical.png";
        public const string DEFAULT_TEXTURE_SECONDARY_NAME = "horizontal.png";
        public virtual float[] Vertices { get; }
        public virtual uint[] Indices { get; }

        private string mainTextureName=string.Empty;
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
            ShaderProgram.Use();
            Gl.BindVertexArray(VAO);


            SetMVP(ViewMatrix, ProjectionMatrix);
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
                shaderProgram = ShadersManager.Instance.BuildShaderProgram(RegisteredStages.ToArray());

            if (mainTextureName == string.Empty)
                mainTextureName = DEFAULT_TEXTURE_MAIN_NAME;
            mainImageTexture = new ImageTexture(mainTextureName, RotateFlipType.RotateNoneFlipNone);

            if (secondaryTextureName == string.Empty)
                secondaryTextureName = DEFAULT_TEXTURE_SECONDARY_NAME;
             secondaryImageTexture = new ImageTexture(secondaryTextureName, RotateFlipType.RotateNoneFlipNone);


            Gl.ActiveTexture(TextureUnit.Texture0);
            mainImageTexture.Bind();
            mainImageTexture.Release();
            Gl.ActiveTexture(TextureUnit.Texture1);
            secondaryImageTexture.Bind();
            secondaryImageTexture.Release();

        }

        public override void SetProgramParameters()
        {
            ShaderProgram.Use();
            ShaderProgram.SetInt("texBackground", 0);
            ShaderProgram.SetInt("texForeground", 1);
        }

    }
}
