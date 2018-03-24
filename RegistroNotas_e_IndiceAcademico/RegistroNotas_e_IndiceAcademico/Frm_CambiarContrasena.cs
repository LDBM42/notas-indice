using System;
using System.Data;
using System.Data.OleDb;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace RegistroNotas_e_IndiceAcademico
{
    public partial class Frm_CambiarPass : Form
    {
        SoundPlayer sonido;
        private string user;
        private string password;

        public Frm_CambiarPass()
        {
            InitializeComponent();
        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Thread.Sleep(100);

            EditParent EP = this.Owner as EditParent;
            EP.Cambiar_Imagenes(false);
            EP.isHidden("Cam_Pass");
            reproducirSonido("tech_Ambiente.wav", true);
            Hide();
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

        private void Btn_Cambiar_Click(object sender, EventArgs e)
        {
            if (Buscar_Registro(Tbx_User_Actual.Text, Tbx_Pass_Actual.Text))
             {
                reproducirSonido("warning.wav", false);

                DialogResult Respuesta = MessageBox.Show("Está seguro de Modificar el Usiario?", "Advertencia", MessageBoxButtons.YesNo);
                if (Respuesta == DialogResult.Yes) // si la respuesta es si
                {
                    reproducirSonido("click.wav", false);
                    Thread.Sleep(100);

                    Modificar_Registro(Tbx_User_Actual.Text, Tbx_User_Nuevo.Text, Tbx_Pass_Nuevo.Text);
                    MessageBox.Show("Usuario Modificado!");
                }
                else
                {
                    reproducirSonido("click.wav", false);
                    Thread.Sleep(100);
                }
            }
            else
            {
                reproducirSonido("click.wav", false);
                Thread.Sleep(100);
                MessageBox.Show("Nombre de Usuario y/o Contraseña Incorrecta(o,os)!");
            }
        }

        private void Tbx_User_Actual_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                SendKeys.Send("{TAB}"); //hace que se presione la tecla TAB por código
            }
        }

        private void Tbx_Pass_Actual_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                SendKeys.Send("{TAB}"); //hace que se presione la tecla TAB por código
            }
        }

        private void Tbx_User_Nuevo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                SendKeys.Send("{TAB}"); //hace que se presione la tecla TAB por código
            }
        }

        private void Tbx_Pass_Nuevo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                SendKeys.Send("{TAB}"); //hace que se presione la tecla TAB por código
            }
        }

        private void Tbx_Pass_Nuevo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                if (Tbx_Pass_Nuevo.Text != Tbx_Pass_Nuevo2.Text)
                {
                    MessageBox.Show("Los dos Password están Diferentes");
                    Tbx_Pass_Nuevo2.Text = "";
                    Tbx_Pass_Nuevo.Focus();
                    Tbx_Pass_Nuevo.SelectAll();
                }
                else
                {
                    Btn_Cambiar.PerformClick();
                }

                
            }
        }

        private void Tbx_Pass_Nuevo2_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_Pass_Nuevo.Text == Tbx_Pass_Nuevo2.Text)
            {
                Btn_Cambiar.Enabled = true;
            }
            else
            {
                Btn_Cambiar.Enabled = false;
            }
        }

        /*
                private bool Modificar_Registro(string usuarioActual, string nuevoUser, string nuevoPass)
                {
                    //Conección
                    OleDbConnection Conexion = new OleDbConnection();
                    Conexion.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source = " + Application.StartupPath + @"\Notas_e_Indice_BBDD\Notas_e_Indice_BBDD.accdb;" + "Persist Security Info = false";


                    //Instrucción SQL
                    string CadenaSQL = "UPDATE Usuarios SET "; //Actualiza la tabla personal estableciendo nombre, direccion y edad, con los datos especificados.
                    CadenaSQL = CadenaSQL + " User = '" + nuevoUser + "',";// cuando la variable es string en la secuencia SQL se le agregan esas comillas simples "'"
                    CadenaSQL = CadenaSQL + " Password = '" + nuevoPass + "' ";// la coma permite separar cada campo de su variable de asignación
                   //Hay que decir donde es que se va a actualizar.
                    //Actualizar lo anterior a este registro (osea, donde el id sea = a Cod.
                    CadenaSQL = CadenaSQL + " WHERE User=" + "'" + usuarioActual + "'";
                    // si no se indica donde es que se va a cambiar, todos los datos de la tabla personal se cambiarian y no solo donde el id es igual a Cod.
                    //INSERTE DENTRO de la tabla Alumnos, en los campos Direccion, Telefono y edad, los VALORES Doreccion, Telefono y Eda.


                    //Crear comando
                    OleDbCommand Comando = Conexion.CreateCommand();  //Asignamos la cadena SQL al comando
                    Comando.CommandText = CadenaSQL;

                    //Ejecutar la consulta de acción
                    Conexion.Open(); //Abrimos la conexion
                    Comando.ExecuteNonQuery(); //Ejecutamos la consulta
                    Conexion.Close(); //Cerramos la conexion
                    return true;
                } */

        private bool Modificar_Registro(string usuarioActual, string nuevoUser, string nuevoPass)
        {
            //Conección
            OleDbConnection Conexion = new OleDbConnection();
            Conexion.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source = " + Application.StartupPath + @"\Notas_e_Indice_BBDD\Pass.accdb;" + "Persist Security Info = false";


            //Instrucción SQL
            string CadenaSQL = "UPDATE Usuarios SET "; //Actualiza la tabla personal estableciendo nombre, direccion y edad, con los datos especificados.
            CadenaSQL = CadenaSQL + " User = '" + nuevoUser + "',";// cuando la variable es string en la secuencia SQL se le agregan esas comillas simples "'"
            CadenaSQL = CadenaSQL + " Password = '" + nuevoPass + "' ";// la coma permite separar cada campo de su variable de asignación
                                                                       //Hay que decir donde es que se va a actualizar.
                                                                       //Actualizar lo anterior a este registro (osea, donde el id sea = a Cod.
            CadenaSQL = CadenaSQL + " WHERE User=" + "'" + usuarioActual + "'";
            // si no se indica donde es que se va a cambiar, todos los datos de la tabla personal se cambiarian y no solo donde el id es igual a Cod.
            //INSERTE DENTRO de la tabla Alumnos, en los campos Direccion, Telefono y edad, los VALORES Doreccion, Telefono y Eda.


            //Crear comando
            OleDbCommand Comando = Conexion.CreateCommand();  //Asignamos la cadena SQL al comando
            Comando.CommandText = CadenaSQL;

            //Ejecutar la consulta de acción
            Conexion.Open(); //Abrimos la conexion
            Comando.ExecuteNonQuery(); //Ejecutamos la consulta
            Conexion.Close(); //Cerramos la conexion
            return true;
        }

        private bool Buscar_Registro(string Usuario, string pass)
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
            String CadenaSQL = "SELECT * FROM Usuarios WHERE User=" + "'" + Usuario + "' AND Password='" + pass + "'"; //selecciona todos los campor de la tabla personal donde el id sea igual al codigo

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

        private void Tbx_Pass_Nuevo_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_Pass_Nuevo.Text == Tbx_Pass_Nuevo2.Text)
            {
                Btn_Cambiar.Enabled = true;
            }
            else
            {
                Btn_Cambiar.Enabled = false;
            }
        }
    }
}
