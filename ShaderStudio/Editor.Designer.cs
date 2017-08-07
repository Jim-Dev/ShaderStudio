namespace ShaderStudio
{
    partial class Editor
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.GLCanvas = new OpenGL.GlControl();
            this.SuspendLayout();
            // 
            // GLCanvas
            // 
            this.GLCanvas.Animation = true;
            this.GLCanvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.GLCanvas.ColorBits = ((uint)(24u));
            this.GLCanvas.DepthBits = ((uint)(0u));
            this.GLCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLCanvas.Location = new System.Drawing.Point(0, 0);
            this.GLCanvas.MultisampleBits = ((uint)(0u));
            this.GLCanvas.Name = "GLCanvas";
            this.GLCanvas.Size = new System.Drawing.Size(1264, 681);
            this.GLCanvas.StencilBits = ((uint)(0u));
            this.GLCanvas.TabIndex = 0;
            this.GLCanvas.ContextCreated += new System.EventHandler<OpenGL.GlControlEventArgs>(this.GLCanvas_ContextCreated);
            this.GLCanvas.ContextDestroying += new System.EventHandler<OpenGL.GlControlEventArgs>(this.GLCanvas_ContextDestroying);
            this.GLCanvas.Render += new System.EventHandler<OpenGL.GlControlEventArgs>(this.GLCanvas_Render);
            this.GLCanvas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GLCanvas_KeyDown);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.GLCanvas);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Editor";
            this.Text = "ShaderStudio - Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private OpenGL.GlControl GLCanvas;
    }
}

