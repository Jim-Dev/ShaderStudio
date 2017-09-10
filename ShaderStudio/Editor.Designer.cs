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
            this.panelShaders = new System.Windows.Forms.Panel();
            this.panelProperties = new System.Windows.Forms.Panel();
            this.spcMain = new System.Windows.Forms.SplitContainer();
            this.spcRender = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.spcMain)).BeginInit();
            this.spcMain.Panel2.SuspendLayout();
            this.spcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcRender)).BeginInit();
            this.spcRender.Panel1.SuspendLayout();
            this.spcRender.SuspendLayout();
            this.SuspendLayout();
            // 
            // GLCanvas
            // 
            this.GLCanvas.Animation = true;
            this.GLCanvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.GLCanvas.ColorBits = ((uint)(24u));
            this.GLCanvas.DepthBits = ((uint)(1u));
            this.GLCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLCanvas.Location = new System.Drawing.Point(0, 0);
            this.GLCanvas.MultisampleBits = ((uint)(0u));
            this.GLCanvas.Name = "GLCanvas";
            this.GLCanvas.Size = new System.Drawing.Size(639, 552);
            this.GLCanvas.StencilBits = ((uint)(0u));
            this.GLCanvas.TabIndex = 0;
            this.GLCanvas.ContextCreated += new System.EventHandler<OpenGL.GlControlEventArgs>(this.GLCanvas_ContextCreated);
            this.GLCanvas.ContextDestroying += new System.EventHandler<OpenGL.GlControlEventArgs>(this.GLCanvas_ContextDestroying);
            this.GLCanvas.Render += new System.EventHandler<OpenGL.GlControlEventArgs>(this.GLCanvas_Render);
            this.GLCanvas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GLCanvas_KeyDown);
            // 
            // panelShaders
            // 
            this.panelShaders.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelShaders.Location = new System.Drawing.Point(0, 0);
            this.panelShaders.Name = "panelShaders";
            this.panelShaders.Size = new System.Drawing.Size(150, 681);
            this.panelShaders.TabIndex = 1;
            // 
            // panelProperties
            // 
            this.panelProperties.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelProperties.Location = new System.Drawing.Point(1114, 0);
            this.panelProperties.Name = "panelProperties";
            this.panelProperties.Size = new System.Drawing.Size(150, 681);
            this.panelProperties.TabIndex = 2;
            // 
            // spcMain
            // 
            this.spcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcMain.Location = new System.Drawing.Point(150, 0);
            this.spcMain.Name = "spcMain";
            // 
            // spcMain.Panel2
            // 
            this.spcMain.Panel2.Controls.Add(this.spcRender);
            this.spcMain.Size = new System.Drawing.Size(964, 681);
            this.spcMain.SplitterDistance = 321;
            this.spcMain.TabIndex = 3;
            // 
            // spcRender
            // 
            this.spcRender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcRender.Location = new System.Drawing.Point(0, 0);
            this.spcRender.Name = "spcRender";
            this.spcRender.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcRender.Panel1
            // 
            this.spcRender.Panel1.Controls.Add(this.GLCanvas);
            this.spcRender.Size = new System.Drawing.Size(639, 681);
            this.spcRender.SplitterDistance = 552;
            this.spcRender.TabIndex = 0;
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.spcMain);
            this.Controls.Add(this.panelProperties);
            this.Controls.Add(this.panelShaders);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Editor";
            this.Text = "ShaderStudio - Editor";
            this.spcMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcMain)).EndInit();
            this.spcMain.ResumeLayout(false);
            this.spcRender.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcRender)).EndInit();
            this.spcRender.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenGL.GlControl GLCanvas;
        private System.Windows.Forms.Panel panelShaders;
        private System.Windows.Forms.Panel panelProperties;
        private System.Windows.Forms.SplitContainer spcMain;
        private System.Windows.Forms.SplitContainer spcRender;
    }
}

