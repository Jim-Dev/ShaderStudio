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
            this.fswCurrentShaderWatcher = new System.IO.FileSystemWatcher();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClearOutput = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txbOutput2 = new System.Windows.Forms.TextBox();
            this.txbOutput = new ScintillaNET.Scintilla();
            ((System.ComponentModel.ISupportInitialize)(this.fswCurrentShaderWatcher)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.GLCanvas.Size = new System.Drawing.Size(1264, 507);
            this.GLCanvas.StencilBits = ((uint)(0u));
            this.GLCanvas.TabIndex = 0;
            this.GLCanvas.ContextCreated += new System.EventHandler<OpenGL.GlControlEventArgs>(this.GLCanvas_ContextCreated);
            this.GLCanvas.ContextDestroying += new System.EventHandler<OpenGL.GlControlEventArgs>(this.GLCanvas_ContextDestroying);
            this.GLCanvas.Render += new System.EventHandler<OpenGL.GlControlEventArgs>(this.GLCanvas_Render);
            this.GLCanvas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GLCanvas_KeyDown);
            // 
            // fswCurrentShaderWatcher
            // 
            this.fswCurrentShaderWatcher.EnableRaisingEvents = true;
            this.fswCurrentShaderWatcher.Filter = "Current*";
            this.fswCurrentShaderWatcher.NotifyFilter = ((System.IO.NotifyFilters)((System.IO.NotifyFilters.FileName | System.IO.NotifyFilters.Size)));
            this.fswCurrentShaderWatcher.Path = "Resources\\Shaders";
            this.fswCurrentShaderWatcher.SynchronizingObject = this;
            this.fswCurrentShaderWatcher.Changed += new System.IO.FileSystemEventHandler(this.FVertexWatcher_Changed);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panel1.Controls.Add(this.btnClearOutput);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1264, 23);
            this.panel1.TabIndex = 1;
            // 
            // btnClearOutput
            // 
            this.btnClearOutput.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearOutput.ForeColor = System.Drawing.Color.Red;
            this.btnClearOutput.Location = new System.Drawing.Point(1239, 0);
            this.btnClearOutput.Name = "btnClearOutput";
            this.btnClearOutput.Size = new System.Drawing.Size(25, 23);
            this.btnClearOutput.TabIndex = 0;
            this.btnClearOutput.Text = "X";
            this.btnClearOutput.UseVisualStyleBackColor = true;
            this.btnClearOutput.Click += new System.EventHandler(this.btnClearOutput_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.GLCanvas);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txbOutput2);
            this.splitContainer1.Panel2.Controls.Add(this.txbOutput);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1264, 681);
            this.splitContainer1.SplitterDistance = 507;
            this.splitContainer1.TabIndex = 2;
            // 
            // txbOutput2
            // 
            this.txbOutput2.Location = new System.Drawing.Point(827, 34);
            this.txbOutput2.Multiline = true;
            this.txbOutput2.Name = "txbOutput2";
            this.txbOutput2.Size = new System.Drawing.Size(386, 124);
            this.txbOutput2.TabIndex = 3;
            // 
            // txbOutput
            // 
            this.txbOutput.Location = new System.Drawing.Point(0, 23);
            this.txbOutput.Name = "txbOutput";
            this.txbOutput.Size = new System.Drawing.Size(810, 147);
            this.txbOutput.TabIndex = 2;
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Editor";
            this.Text = "ShaderStudio - Editor";
            ((System.ComponentModel.ISupportInitialize)(this.fswCurrentShaderWatcher)).EndInit();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenGL.GlControl GLCanvas;
        private System.IO.FileSystemWatcher fswCurrentShaderWatcher;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClearOutput;
        private ScintillaNET.Scintilla txbOutput;
        private System.Windows.Forms.TextBox txbOutput2;
    }
}

