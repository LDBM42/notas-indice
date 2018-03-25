namespace RegistroNotas_e_IndiceAcademico
{
    partial class Frm_Principal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Principal));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.registroDeNotasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registroDeNotasToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.asignaturasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cálculoDelÍndiceAcadémicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarContraseñaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Pbx_Center = new System.Windows.Forms.PictureBox();
            this.Pbx_Left = new System.Windows.Forms.PictureBox();
            this.Pbx_right = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx_Center)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx_Left)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx_right)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registroDeNotasToolStripMenuItem,
            this.registroDeNotasToolStripMenuItem1,
            this.asignaturasToolStripMenuItem,
            this.cálculoDelÍndiceAcadémicoToolStripMenuItem,
            this.cambiarContraseñaToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1230, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // registroDeNotasToolStripMenuItem
            // 
            this.registroDeNotasToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.registroDeNotasToolStripMenuItem.Name = "registroDeNotasToolStripMenuItem";
            this.registroDeNotasToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
            this.registroDeNotasToolStripMenuItem.Text = "Registro de Estudiantes";
            this.registroDeNotasToolStripMenuItem.Click += new System.EventHandler(this.registroEstudiantesToolStripMenuItem_Click);
            // 
            // registroDeNotasToolStripMenuItem1
            // 
            this.registroDeNotasToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.registroDeNotasToolStripMenuItem1.Name = "registroDeNotasToolStripMenuItem1";
            this.registroDeNotasToolStripMenuItem1.Size = new System.Drawing.Size(140, 24);
            this.registroDeNotasToolStripMenuItem1.Text = "Registro de Notas";
            this.registroDeNotasToolStripMenuItem1.Click += new System.EventHandler(this.registroDeNotasToolStripMenuItem1_Click);
            // 
            // asignaturasToolStripMenuItem
            // 
            this.asignaturasToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.asignaturasToolStripMenuItem.Name = "asignaturasToolStripMenuItem";
            this.asignaturasToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
            this.asignaturasToolStripMenuItem.Text = "Asignaturas";
            this.asignaturasToolStripMenuItem.Click += new System.EventHandler(this.asignaturasToolStripMenuItem_Click);
            // 
            // cálculoDelÍndiceAcadémicoToolStripMenuItem
            // 
            this.cálculoDelÍndiceAcadémicoToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.cálculoDelÍndiceAcadémicoToolStripMenuItem.Name = "cálculoDelÍndiceAcadémicoToolStripMenuItem";
            this.cálculoDelÍndiceAcadémicoToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.cálculoDelÍndiceAcadémicoToolStripMenuItem.Text = "Cálculo del Índice Académico";
            this.cálculoDelÍndiceAcadémicoToolStripMenuItem.Click += new System.EventHandler(this.cálculoDelÍndiceAcadémicoToolStripMenuItem_Click);
            // 
            // cambiarContraseñaToolStripMenuItem
            // 
            this.cambiarContraseñaToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.cambiarContraseñaToolStripMenuItem.Name = "cambiarContraseñaToolStripMenuItem";
            this.cambiarContraseñaToolStripMenuItem.Padding = new System.Windows.Forms.Padding(4, 0, 8, 0);
            this.cambiarContraseñaToolStripMenuItem.Size = new System.Drawing.Size(159, 24);
            this.cambiarContraseñaToolStripMenuItem.Text = "Cambiar Contraseña";
            this.cambiarContraseñaToolStripMenuItem.Visible = false;
            this.cambiarContraseñaToolStripMenuItem.Click += new System.EventHandler(this.cambiarContraseñaToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.AutoSize = false;
            this.salirToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(38)))), ((int)(((byte)(32)))));
            this.salirToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(100, 24);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.Pbx_Center, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Pbx_Left, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Pbx_right, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1230, 627);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // Pbx_Center
            // 
            this.Pbx_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pbx_Center.Image = global::RegistroNotas_e_IndiceAcademico.Properties.Resources.ITSC2;
            this.Pbx_Center.Location = new System.Drawing.Point(413, 3);
            this.Pbx_Center.Name = "Pbx_Center";
            this.Pbx_Center.Size = new System.Drawing.Size(404, 621);
            this.Pbx_Center.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pbx_Center.TabIndex = 9;
            this.Pbx_Center.TabStop = false;
            // 
            // Pbx_Left
            // 
            this.Pbx_Left.Dock = System.Windows.Forms.DockStyle.Left;
            this.Pbx_Left.Image = ((System.Drawing.Image)(resources.GetObject("Pbx_Left.Image")));
            this.Pbx_Left.Location = new System.Drawing.Point(3, 3);
            this.Pbx_Left.Name = "Pbx_Left";
            this.Pbx_Left.Size = new System.Drawing.Size(404, 621);
            this.Pbx_Left.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Pbx_Left.TabIndex = 8;
            this.Pbx_Left.TabStop = false;
            // 
            // Pbx_right
            // 
            this.Pbx_right.Dock = System.Windows.Forms.DockStyle.Right;
            this.Pbx_right.Image = global::RegistroNotas_e_IndiceAcademico.Properties.Resources.Cuadro_negro_LDBM;
            this.Pbx_right.Location = new System.Drawing.Point(823, 3);
            this.Pbx_right.Name = "Pbx_right";
            this.Pbx_right.Size = new System.Drawing.Size(404, 621);
            this.Pbx_right.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Pbx_right.TabIndex = 7;
            this.Pbx_right.TabStop = false;
            // 
            // Frm_Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1230, 655);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Estudiantes";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pbx_Center)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx_Left)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx_right)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem registroDeNotasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registroDeNotasToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem asignaturasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cálculoDelÍndiceAcadémicoToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.PictureBox Pbx_right;
        private System.Windows.Forms.PictureBox Pbx_Center;
        private System.Windows.Forms.PictureBox Pbx_Left;
        private System.Windows.Forms.ToolStripMenuItem cambiarContraseñaToolStripMenuItem;
    }
}

