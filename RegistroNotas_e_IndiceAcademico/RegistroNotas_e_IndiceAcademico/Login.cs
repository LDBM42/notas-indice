using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Data.OleDb;

namespace RegistroNotas_e_IndiceAcademico
{
    public partial class Frm_Login : Form
    {
        int i = 0;
        SoundPlayer sonido;
        private string user = "Administrador";
        private string password = "1234";
        private int countDown = 0;
        private int AColor_countDown = 0;
        private int Count_Timer = 0;
        private bool entrar;
        private bool on_Off = false;

        public Frm_Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            Buscar_Registro();
            circularProgressBar.Value = 0;
            circularProgressBar.Minimum = 0;
            circularProgressBar.Maximum = 100;
            reproducirSonido("system_Load.wav", false);
            LoadBar.Start();
        }
        private void LoadBar_Tick_1(object sender, EventArgs e)
        {
            circularProgressBar.Text = Convert.ToString(circularProgressBar.Value) + "%";
            circularProgressBar.Increment(14);
            circularProgressBar.Update();

            i = i+11;
            if (i >= 100)
            {
                LoadBar.Stop();
                Gbx_Login.Visible = true;
                circularProgressBar.Visible = false;
                Tbx_User.Focus();
            }
        }

        private bool PassWord(string name, string password)
        {
            if (name == this.user && password == this.password)
            {
                return true;
            }
            else
            {
                countDown += 1;
                return false;
            }
        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Application.Exit();
        }


        private void reproducirSonido(string nombreArchivo, bool loop)
        {
            if (sonido != null)
            {
                sonido.Stop();
            }
            //SystemSounds.Hand.Play(); // Sonido de windows
            try
            {
                sonido = new SoundPlayer(Application.StartupPath + @"\son\" + nombreArchivo);
                if (loop == true)
                {
                    sonido.PlayLooping();
                }
                else
                {
                    sonido.Play();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            entrar = PassWord(Tbx_User.Text, Tbx_Pass.Text);

            if (entrar == true)
            {
                reproducirSonido("click.wav", false);

                reproducirSonido("access_Granted.wav", false);
                Thread.Sleep(700);

                Tmr_CountDoun.Stop();
                Alarm.Stop();
                AlarmColor.Stop();

                Frm_Principal Frm_Principal = new Frm_Principal();
                Frm_Principal.Show();
                this.Hide();
            }
            else
            {
                if (on_Off == false)
                {
                    reproducirSonido("access_Denied.wav", false);
                    Thread.Sleep(700);
                    on_Off = true;
                    reproducirSonido("autorizacion_Requerida.wav", false);
                    Tmr_CountDoun.Start();
                    Alarm.Start();
                }

                Tbx_User.Clear();
                Tbx_Pass.Clear();

                Tbx_User.Focus();
                
                if (countDown == 4)
                {
                    MessageBox.Show("Ha Fallado 4 veces, Pograma Terminado!");
                    Application.Exit();
                }
            }     
        }

        private void Alarm_Tick(object sender, EventArgs e)
        {
            i = i + 1;
            if (i > 270 && entrar == false)
            {
                AlarmColor.Start();
                Alarm.Stop();
                reproducirSonido("alarm.wav", true);

            }
        }

        private void Tbx_User_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                SendKeys.Send("{TAB}"); //hace que se presione la tecla TAB por código
            }
        }

        private void Tbx_Pass_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                Btn_Login.PerformClick();
            }
        }

        private void AlarmColor_Tick(object sender, EventArgs e)
        {
            if (AColor_countDown == 0)
            {
                this.BackColor = Color.FromArgb(225, 38, 32);
                AColor_countDown = 1;
            }
            else
            {
                this.BackColor = Color.White;
                AColor_countDown = 0;
            }
        }

        private void Tmr_CountDoun_Tick(object sender, EventArgs e)
        {
            Count_Timer += 1;

            if (Count_Timer == 4)
            {
                Lab_CountDown_L.Visible = true;
                Lab_CountDown_R.Visible = true;
            }
            else
                if (Count_Timer == 6)
            {
                int x_L = Lab_CountDown_L.Location.X;
                int x_R = Lab_CountDown_R.Location.X;
                Lab_CountDown_L.Location = new Point((x_L + 190), Lab_CountDown_L.Location.Y);
                Lab_CountDown_R.Location = new Point((x_R + 140), Lab_CountDown_R.Location.Y);
                Lab_CountDown_L.Text = "9";
                Lab_CountDown_R.Text = "9";
            }
            else
                if (Count_Timer == 7)
            {
                Lab_CountDown_L.Text = "8";
                Lab_CountDown_R.Text = "8";
            }
            else
                if (Count_Timer == 8)
            {
                Lab_CountDown_L.Text = "7";
                Lab_CountDown_R.Text = "7";
            }
            else
                if (Count_Timer == 9)
            {
                Lab_CountDown_L.Text = "6";
                Lab_CountDown_R.Text = "6";
            }
            else
                if (Count_Timer == 10)
            {
                Lab_CountDown_L.Text = "5";
                Lab_CountDown_R.Text = "5";
            }
            else
                if (Count_Timer == 11)
            {
                Lab_CountDown_L.Text = "4";
                Lab_CountDown_R.Text = "4";
            }
            else
                if (Count_Timer == 12)
            {
                Lab_CountDown_L.Text = "3";
                Lab_CountDown_R.Text = "3";
            }
            else
                if (Count_Timer == 13)
            {
                Lab_CountDown_L.Text = "2";
                Lab_CountDown_R.Text = "2";
            }
            else
                if (Count_Timer == 14)
            {
                Lab_CountDown_L.Text = "1";
                Lab_CountDown_R.Text = "1";
            }
            else
                if (Count_Timer == 15)
            {
                Lab_CountDown_L.Text = "0";
                Lab_CountDown_R.Text = "0";
            }
            else
                if (Count_Timer == 16)
            {
                Tbx_User.Enabled = false;
                Tbx_Pass.Enabled = false;
                Btn_Login.Enabled = false;
                Btn_Salir.Enabled = false;

                Lab_CountDown_L.Visible = false;
                Lab_CountDown_R.Visible = false;
            }
            else
                if (Count_Timer == 22)
            {
                Application.Exit();
            }
        }



        private bool Buscar_Registro()
        {
            //Convertir string a entero
            //int mat = Convert.ToInt32(matricula);

            // conexión *************************
            OleDbConnection Conexion = new OleDbConnection();   // Crea la conexión
            Conexion.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source = " + Application.StartupPath + @"\Notas_e_Indice_BBDD\Pass.accdb;" + "Persist Security Info = false";
            //Configurar la conexión, indicandole el proveedor de MIcrosoft que nos permite usar ACCESS
            //El origen del archivo de la base de datos ACCESS. Nota: los '\' se deben de escribir dobles '\\'
            //Y el dato de seguridad


            // cadena SQL  *************************
            //Esta es una cadena SQL que nos permitira hacer la busqueda
            String CadenaSQL = "SELECT * FROM Usuarios WHERE id= 1"; //selecciona todos los campor de la tabla personal donde el id sea igual al codigo

            // adaptador  *************************
            OleDbDataAdapter Adaptador = new OleDbDataAdapter(CadenaSQL, Conexion); // Es una caja que se llena con cualquer dato: en este caso le pasamos como parametro
                                                                                    // la cadena SQL y la Coneccion
            
            // dataset  *************************
            // sirve par almacenar la tabla, es el objeto  con el cual podemos manipular los datos
            DataSet ds = new DataSet();

            // llenar el dataser  *************************
            Conexion.Open(); // abre la base de datos, lo que significa que se hace el enlace entre el programa y la base de datos
            Adaptador.Fill(ds); //llenamos el data set con el contenido del adaptador
            Conexion.Close(); // cerramos la coneccion


            // contar registros  *************************
            // cuenta cuantos registros se almacenaron en el dataset
            if (ds.Tables[0].Rows.Count == 0)  // revisa las filas de la tabla 0 para ver su tamaño. Si es igual a cero entonces devuelve false, de lo contrario true.
            {  // Cuantas filas o registros tiene la tabla cero del dataset?
                ds.Dispose(); //para cerrar aunque el garbage collector lo hace automatico
                return false; //el registro no fue encontrado
            }
            else
            {
                //cargar los campos Nombre, Dirección y Edad con los datos del registro
                user = ds.Tables[0].Rows[0]["User"].ToString();
                password = ds.Tables[0].Rows[0]["Password"].ToString();
                //dentro del dataset en la tabla 0, en la fila 0, con el campo nombre... y se convierte a string.

                // cerrar o liberar la memoria del dataset
                ds.Dispose();

                return true; //el registro existe
            }
        }

    }
}
