namespace RegistroNotas_e_IndiceAcademico
{
    partial class Frm_Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LoadBar = new System.Windows.Forms.Timer(this.components);
            this.Gbx_Login = new System.Windows.Forms.GroupBox();
            this.Btn_Salir = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Lab_User = new System.Windows.Forms.Label();
            this.Btn_Login = new System.Windows.Forms.Button();
            this.Tbx_Pass = new System.Windows.Forms.TextBox();
            this.Tbx_User = new System.Windows.Forms.TextBox();
            this.Lab_Pass = new System.Windows.Forms.Label();
            this.circularProgressBar = new CircularProgressBar.CircularProgressBar();
            this.Alarm = new System.Windows.Forms.Timer(this.components);
            this.AlarmColor = new System.Windows.Forms.Timer(this.components);
            this.Lab_CountDown_L = new System.Windows.Forms.Label();
            this.Lab_CountDown_R = new System.Windows.Forms.Label();
            this.Tmr_CountDoun = new System.Windows.Forms.Timer(this.components);
            this.Gbx_Login.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoadBar
            // 
            this.LoadBar.Interval = 300;
            this.LoadBar.Tick += new System.EventHandler(this.LoadBar_Tick_1);
            // 
            // Gbx_Login
            // 
            this.Gbx_Login.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Gbx_Login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(81)))), ((int)(((byte)(86)))));
            this.Gbx_Login.Controls.Add(this.Btn_Salir);
            this.Gbx_Login.Controls.Add(this.textBox1);
            this.Gbx_Login.Controls.Add(this.Lab_User);
            this.Gbx_Login.Controls.Add(this.Btn_Login);
            this.Gbx_Login.Controls.Add(this.Tbx_Pass);
            this.Gbx_Login.Controls.Add(this.Tbx_User);
            this.Gbx_Login.Controls.Add(this.Lab_Pass);
            this.Gbx_Login.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Gbx_Login.Location = new System.Drawing.Point(517, 240);
            this.Gbx_Login.Name = "Gbx_Login";
            this.Gbx_Login.Size = new System.Drawing.Size(370, 276);
            this.Gbx_Login.TabIndex = 10;
            this.Gbx_Login.TabStop = false;
            this.Gbx_Login.Visible = false;
            // 
            // Btn_Salir
            // 
            this.Btn_Salir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(38)))), ((int)(((byte)(32)))));
            this.Btn_Salir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Salir.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Salir.ForeColor = System.Drawing.Color.White;
            this.Btn_Salir.Location = new System.Drawing.Point(335, 1);
            this.Btn_Salir.Name = "Btn_Salir";
            this.Btn_Salir.Size = new System.Drawing.Size(35, 30);
            this.Btn_Salir.TabIndex = 3;
            this.Btn_Salir.Text = "X";
            this.Btn_Salir.UseVisualStyleBackColor = false;
            this.Btn_Salir.Click += new System.EventHandler(this.Btn_Salir_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Montserrat Subrayada", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(370, 32);
            this.textBox1.TabIndex = 15;
            // 
            // Lab_User
            // 
            this.Lab_User.AutoSize = true;
            this.Lab_User.Font = new System.Drawing.Font("Montserrat Subrayada", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_User.ForeColor = System.Drawing.Color.White;
            this.Lab_User.Location = new System.Drawing.Point(152, 56);
            this.Lab_User.Name = "Lab_User";
            this.Lab_User.Size = new System.Drawing.Size(66, 24);
            this.Lab_User.TabIndex = 14;
            this.Lab_User.Text = "User";
            // 
            // Btn_Login
            // 
            this.Btn_Login.BackColor = System.Drawing.Color.White;
            this.Btn_Login.Font = new System.Drawing.Font("Montserrat Subrayada", 8.25F);
            this.Btn_Login.ForeColor = System.Drawing.Color.Black;
            this.Btn_Login.Location = new System.Drawing.Point(34, 199);
            this.Btn_Login.Name = "Btn_Login";
            this.Btn_Login.Size = new System.Drawing.Size(303, 36);
            this.Btn_Login.TabIndex = 2;
            this.Btn_Login.Text = "LogIn";
            this.Btn_Login.UseVisualStyleBackColor = false;
            this.Btn_Login.Click += new System.EventHandler(this.Btn_Login_Click);
            // 
            // Tbx_Pass
            // 
            this.Tbx_Pass.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tbx_Pass.Location = new System.Drawing.Point(34, 150);
            this.Tbx_Pass.Name = "Tbx_Pass";
            this.Tbx_Pass.PasswordChar = '*';
            this.Tbx_Pass.Size = new System.Drawing.Size(303, 30);
            this.Tbx_Pass.TabIndex = 1;
            this.Tbx_Pass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tbx_Pass_KeyPress);
            // 
            // Tbx_User
            // 
            this.Tbx_User.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tbx_User.Location = new System.Drawing.Point(34, 83);
            this.Tbx_User.Name = "Tbx_User";
            this.Tbx_User.Size = new System.Drawing.Size(303, 30);
            this.Tbx_User.TabIndex = 0;
            this.Tbx_User.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tbx_User_KeyPress);
            // 
            // Lab_Pass
            // 
            this.Lab_Pass.AutoSize = true;
            this.Lab_Pass.Font = new System.Drawing.Font("Montserrat Subrayada", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_Pass.ForeColor = System.Drawing.Color.White;
            this.Lab_Pass.Location = new System.Drawing.Point(153, 123);
            this.Lab_Pass.Name = "Lab_Pass";
            this.Lab_Pass.Size = new System.Drawing.Size(65, 24);
            this.Lab_Pass.TabIndex = 10;
            this.Lab_Pass.Text = "Pass";
            // 
            // circularProgressBar
            // 
            this.circularProgressBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.circularProgressBar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.circularProgressBar.AnimationSpeed = 500;
            this.circularProgressBar.BackColor = System.Drawing.Color.Transparent;
            this.circularProgressBar.Font = new System.Drawing.Font("Univers LT Std 59 UltraCn", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.circularProgressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.circularProgressBar.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.circularProgressBar.InnerMargin = 2;
            this.circularProgressBar.InnerWidth = -1;
            this.circularProgressBar.Location = new System.Drawing.Point(527, 215);
            this.circularProgressBar.MarqueeAnimationSpeed = 2000;
            this.circularProgressBar.Name = "circularProgressBar";
            this.circularProgressBar.OuterColor = System.Drawing.Color.Transparent;
            this.circularProgressBar.OuterMargin = 0;
            this.circularProgressBar.OuterWidth = 0;
            this.circularProgressBar.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(38)))), ((int)(((byte)(32)))));
            this.circularProgressBar.ProgressWidth = 10;
            this.circularProgressBar.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.circularProgressBar.Size = new System.Drawing.Size(350, 327);
            this.circularProgressBar.StartAngle = 270;
            this.circularProgressBar.SubscriptColor = System.Drawing.Color.Transparent;
            this.circularProgressBar.SubscriptMargin = new System.Windows.Forms.Padding(0);
            this.circularProgressBar.SubscriptText = "";
            this.circularProgressBar.SuperscriptColor = System.Drawing.Color.Transparent;
            this.circularProgressBar.SuperscriptMargin = new System.Windows.Forms.Padding(0);
            this.circularProgressBar.SuperscriptText = "";
            this.circularProgressBar.TabIndex = 0;
            this.circularProgressBar.Text = "0%";
            this.circularProgressBar.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            // 
            // Alarm
            // 
            this.Alarm.Tick += new System.EventHandler(this.Alarm_Tick);
            // 
            // AlarmColor
            // 
            this.AlarmColor.Tick += new System.EventHandler(this.AlarmColor_Tick);
            // 
            // Lab_CountDown_L
            // 
            this.Lab_CountDown_L.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Lab_CountDown_L.AutoSize = true;
            this.Lab_CountDown_L.BackColor = System.Drawing.Color.Transparent;
            this.Lab_CountDown_L.Font = new System.Drawing.Font("Microsoft Sans Serif", 400F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_CountDown_L.ForeColor = System.Drawing.SystemColors.Menu;
            this.Lab_CountDown_L.Location = new System.Drawing.Point(-457, 1);
            this.Lab_CountDown_L.Margin = new System.Windows.Forms.Padding(0);
            this.Lab_CountDown_L.Name = "Lab_CountDown_L";
            this.Lab_CountDown_L.Size = new System.Drawing.Size(1057, 755);
            this.Lab_CountDown_L.TabIndex = 12;
            this.Lab_CountDown_L.Text = "10";
            this.Lab_CountDown_L.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.Lab_CountDown_L.Visible = false;
            // 
            // Lab_CountDown_R
            // 
            this.Lab_CountDown_R.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Lab_CountDown_R.AutoSize = true;
            this.Lab_CountDown_R.BackColor = System.Drawing.Color.Transparent;
            this.Lab_CountDown_R.Font = new System.Drawing.Font("Microsoft Sans Serif", 400F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_CountDown_R.ForeColor = System.Drawing.SystemColors.Menu;
            this.Lab_CountDown_R.Location = new System.Drawing.Point(743, 1);
            this.Lab_CountDown_R.Margin = new System.Windows.Forms.Padding(0);
            this.Lab_CountDown_R.Name = "Lab_CountDown_R";
            this.Lab_CountDown_R.Size = new System.Drawing.Size(1057, 755);
            this.Lab_CountDown_R.TabIndex = 13;
            this.Lab_CountDown_R.Text = "10";
            this.Lab_CountDown_R.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.Lab_CountDown_R.Visible = false;
            // 
            // Tmr_CountDoun
            // 
            this.Tmr_CountDoun.Interval = 1000;
            this.Tmr_CountDoun.Tick += new System.EventHandler(this.Tmr_CountDoun_Tick);
            // 
            // Frm_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1404, 756);
            this.Controls.Add(this.circularProgressBar);
            this.Controls.Add(this.Gbx_Login);
            this.Controls.Add(this.Lab_CountDown_L);
            this.Controls.Add(this.Lab_CountDown_R);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_Login";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Login_Load);
            this.Gbx_Login.ResumeLayout(false);
            this.Gbx_Login.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer LoadBar;
        private System.Windows.Forms.GroupBox Gbx_Login;
        private System.Windows.Forms.Label Lab_User;
        private System.Windows.Forms.TextBox Tbx_Pass;
        private System.Windows.Forms.TextBox Tbx_User;
        private System.Windows.Forms.Label Lab_Pass;
        private System.Windows.Forms.TextBox textBox1;
        private CircularProgressBar.CircularProgressBar circularProgressBar;
        private System.Windows.Forms.Button Btn_Salir;
        private System.Windows.Forms.Button Btn_Login;
        private System.Windows.Forms.Timer Alarm;
        private System.Windows.Forms.Timer AlarmColor;
        private System.Windows.Forms.Label Lab_CountDown_L;
        private System.Windows.Forms.Label Lab_CountDown_R;
        private System.Windows.Forms.Timer Tmr_CountDoun;
    }
}