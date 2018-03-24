using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Media;
using System.Threading;

namespace RegistroNotas_e_IndiceAcademico
{
    public partial class Frm_RegistroEstudiantes : Form
    {

        string sexo;
        bool registroExiste;

        SoundPlayer sonido;

        public Frm_RegistroEstudiantes()
        {
            InitializeComponent();
        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Thread.Sleep(100);

            EditParent EP = this.Owner as EditParent;
            EP.Cambiar_Imagenes(false);
            EP.isHidden("Reg_Est");
            reproducirSonido("tech_Ambiente.wav", true);
            Hide();


        }

        private void Btn_Limpiar_Click(object sender, EventArgs e)
        {
            reproducirSonido("clean.wav", false);
            Thread.Sleep(100);

            Limpiar_Formulario();
        }

        private void Limpiar_Formulario()
        {
            Tbx_Matricula.Focus();
            Tbx_Matricula.SelectAll();
            Tbx_Nombre.Clear();
            Tbx_Apellido.Clear();
            Tbx_Direccion.Clear();
            Tbx_Telefono.Clear();
            Tbx_Edad.Clear();
            Rbtn_Masculino.Checked = false;
            Rbtn_Femenino.Checked = false;
        }

        private void Tbx_Matricula_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_Matricula.Text == "")// evita que se deje en Nulo la casilla, para evitar errores
            {
                Tbx_Matricula.Focus();
                Tbx_Matricula.Text = "0";
                Tbx_Matricula.SelectAll();
            }
            if (Tbx_Matricula.TextLength == 9)
            {
                Btn_Buscar.Enabled = true;
                SendKeys.Send("{TAB}"); //hace que se presione la tecla TAB por código
            }
            else
            {
                Btn_Buscar.Enabled = false;
            }

        }

        private void Tbx_Matricula_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                SendKeys.Send("{TAB}"); //hace que se presione la tecla TAB por código
            }
            else
            {
                //si el caracter presionado es numerico o borrar lo permite, sino no
                e.Handled = Validadores.ValidadorMatricula(e.KeyChar);
            }
        }

        private void Tbx_Nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                SendKeys.Send("{TAB}"); //hace que se presione la tecla TAB por código
            }
            else
            {
                //acepta las letras, espacios y caracteres de control como enter y backspace
                if (Char.IsLetter(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsSeparator(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void Tbx_Apellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                SendKeys.Send("{TAB}"); //hace que se presione la tecla TAB por código
            }
            else
            {
                //acepta las letras, espacios y caracteres de control como enter y backspace
                if (Char.IsLetter(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsSeparator(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void Tbx_Direccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                SendKeys.Send("{TAB}"); //hace que se presione la tecla TAB por código
            }
        }

        private void Tbx_Telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                SendKeys.Send("{TAB}"); //hace que se presione la tecla TAB por código
            }
            else
            {
                //si el caracter presionado es numerico o borrar lo permite, sino no
                e.Handled = Validadores.ValidadorNumerico(e.KeyChar);
            }
        }

        private void Tbx_Edad_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                Rbtn_Masculino.Focus();
            }
            else
            {
                //si el caracter presionado es numerico o borrar lo permite, sino no
                e.Handled = Validadores.ValidadorNumerico(e.KeyChar);
            }
        }

        private void Rbtn_Masculino_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                Tbx_Carrera.Focus();
            }
            else
            if (e.KeyChar == (char)32)
            {
                if (Rbtn_Masculino.Checked == true)
                {
                    Rbtn_Femenino.Focus();
                }
                else
                {
                    Rbtn_Masculino.Focus();
                }
            }
        }

        private void Rbtn_Femenino_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                Tbx_Carrera.Focus();
            }
            else
            if (e.KeyChar == (char)32)
            {
                if (Rbtn_Femenino.Checked == true)
                {
                    Rbtn_Masculino.Focus();
                }
                else
                {
                    Rbtn_Femenino.Focus();
                }
            }
        }

        private void Tbx_Carrera_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada

                Btn_Guardar.PerformClick(); // permite hacer clic en el boton por codigo.
            }
            else
            {
                //si el caracter presionado es numerico o borrar lo permite, sino no
                e.Handled = Validadores.ValidadorNumerico(e.KeyChar);
            }
        }

        private void Tbx_Telefono_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_Telefono.TextLength == 10)
            {
                SendKeys.Send("{TAB}"); //hace que se presione la tecla TAB por código
            }
        }

        private void Tbx_Edad_TextChanged(object sender, EventArgs e)
        {

            if (Tbx_Edad.Text == "")
            {
                Tbx_Edad.Focus();
                Tbx_Edad.Text = "18";
                Tbx_Edad.SelectAll();
            }

            if (Tbx_Edad.TextLength == 2)
            {
                Rbtn_Masculino.Focus();
            }
        }

        private void Btn_Guardar_Click(object sender, EventArgs e)
        {
            if (registroExiste)
            {

                reproducirSonido("warning.wav", false);
                Thread.Sleep(100);

                if (Rbtn_Masculino.Checked == true)
                {
                    sexo = "Masculino";
                }
                else
                {
                    sexo = "Femenino";
                }


                //DialogResult Es un objeto que permite almacenar una respuesta.
                DialogResult Respuesta = MessageBox.Show("Está seguro de Modificar el registro actual?", "Advertencia", MessageBoxButtons.YesNo);
                if (Respuesta == DialogResult.Yes) // si la respuesta es si
                {
                    Modificar_Registro(Tbx_Matricula.Text, Tbx_Direccion.Text, Tbx_Telefono.Text, Tbx_Edad.Text);

                    MessageBox.Show("Registro Modificado!");

                    Limpiar_Formulario();

                    Btn_Buscar.Text = "Buscar";

                    Bloquear_Controles();
                    Btn_Buscar.Enabled = true;

                    Tbx_Matricula.Focus();
                    Tbx_Matricula.SelectAll();
                }
            }
            else
            {

                reproducirSonido("click.wav", false);
                Thread.Sleep(100);

                if (Tbx_Nombre.Text == "")
                {
                    if (Tbx_Apellido.Text == "")
                    {
                        MessageBox.Show("Favor introducir un Nombre y un Apellido");
                        Tbx_Nombre.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Favor introducir un Nombre");
                        Tbx_Nombre.Focus();
                    }
                }
                else
                if (Tbx_Apellido.Text == "")
                {
                    MessageBox.Show("Favor introducir un Apellido");
                    Tbx_Apellido.Focus();
                }
                else
                {
                    if (Rbtn_Masculino.Checked == true)
                    {
                        sexo = "Masculino";
                    }
                    else
                    {
                        sexo = "Femenino";
                    }

                    Agregar_Registro(Tbx_Matricula.Text, Tbx_Nombre.Text, Tbx_Apellido.Text, Tbx_Direccion.Text, Tbx_Telefono.Text, Tbx_Edad.Text, sexo, Tbx_Carrera.Text);


                    MessageBox.Show("Alumno Agregado!");

                    Limpiar_Formulario();
                    Bloquear_Controles();
                    Btn_Buscar.Enabled = true;
                }
            }
        }

        private bool Agregar_Registro(string Matricula, string Nombre, string Apellido, string Direccion, string Telefono, string Edad, string Sexo, string Carrera)
        {
            //transformar de cadena a texto
            int Eda = Convert.ToInt32(Edad);

            //Conección
            OleDbConnection Conexion = new OleDbConnection();
            Conexion.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source = " + Application.StartupPath + @"Notas_e_Indice_BBDD\Notas_e_Indice_BBDD.accdb;" + "Persist Security Info = false";

            //Instrucción SQL
            string CadenaSQL = "INSERT INTO Alumnos (Matricula, Nombre, Apellido, Direccion, Telefono, Sexo, Edad, Carrera) ";
            CadenaSQL = CadenaSQL + "VALUES ('" + Matricula + "',";
            CadenaSQL = CadenaSQL + "       '" + Nombre + "',";
            CadenaSQL = CadenaSQL + "       '" + Apellido + "',";
            CadenaSQL = CadenaSQL + "       '" + Direccion + "',";
            CadenaSQL = CadenaSQL + "       '" + Telefono + "',";
            CadenaSQL = CadenaSQL + "       '" + Sexo + "',";
            CadenaSQL = CadenaSQL + "       " + Eda + ",";
            CadenaSQL = CadenaSQL + "       '" + Carrera + "')";

            //INSERTE DENTRO de la tabla personal, en los campos id, nombre, direccion y edad, los VALORES Codigo, Nombre, Direccion y Edad.

            //Crear comando
            OleDbCommand Comando = Conexion.CreateCommand();  //Asignamos la cadena SQL al comando
            Comando.CommandText = CadenaSQL;

            //Ejecutar la consulta de acción
            Conexion.Open(); //Abrimos la conexion
            Comando.ExecuteNonQuery(); //Ejecutamos la consulta
            Conexion.Close(); //Cerramos la conexion
            return true;
        }


        private bool Modificar_Registro(string matricula, string direccion, string telefono, string edad)
        {
            //transformar de cadena a texto
            int eda = Convert.ToInt32(edad);

            //Conección
            OleDbConnection Conexion = new OleDbConnection();
            Conexion.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source = " + Application.StartupPath + @"\Notas_e_Indice_BBDD\Notas_e_Indice_BBDD.accdb;" + "Persist Security Info = false";


            //Instrucción SQL
            string CadenaSQL = "UPDATE Alumnos SET "; //Actualiza la tabla personal estableciendo nombre, direccion y edad, con los datos especificados.
            CadenaSQL = CadenaSQL + " Direccion = '" + direccion + "',";// cuando la variable es string en la secuencia SQL se le agregan esas comillas simples "'"
            CadenaSQL = CadenaSQL + " Telefono = '" + telefono + "',";// la coma permite separar cada campo de su variable de asignación
            CadenaSQL = CadenaSQL + " Edad      = " + eda + " ";// la coma permite separar cada campo de su variable de asignación
            //Hay que decir donde es que se va a actualizar.
            //Actualizar lo anterior a este registro (osea, donde el id sea = a Cod.
            CadenaSQL = CadenaSQL + " WHERE Matricula = '" + matricula + "'";
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


        private void Btn_Buscar_Click(object sender, EventArgs e)
        {
            if (Tbx_Matricula.Enabled == false)
            {
                reproducirSonido("click.wav", false);
                Thread.Sleep(100);

                Limpiar_Formulario();

                Tbx_Matricula.Enabled = true;
                Tbx_Matricula.Text = "0";
                Tbx_Matricula.Focus();
                Tbx_Matricula.SelectAll();


                Btn_Guardar.Enabled = false;
                Btn_Eliminar.Enabled = false;

                Btn_Buscar.Text = "Buscar";
                Btn_Guardar.Text = "Guardar";
            }
            else
            {
                if (Buscar_Registro(Tbx_Matricula.Text) == false)
                {
                    reproducirSonido("click.wav", false);
                    Thread.Sleep(100);

                    registroExiste = false;
                    MessageBox.Show("Alumno No Encontrado!");
                    Limpiar_Formulario();
                    Desbloquear_Controles();

                    Btn_Eliminar.Enabled = false;

                    Tbx_Matricula.Enabled = true;
                }
                else
                {
                    reproducirSonido("click.wav", false);
                    Thread.Sleep(100);

                    registroExiste = true;
                    MessageBox.Show("Alumno Encontrado!");

                    Bloquear_Controles();
                    //Desbloquear los controles que se pueden modificar
                    Tbx_Direccion.Enabled = true;
                    Tbx_Telefono.Enabled = true;
                    Tbx_Edad.Enabled = true;
                    Tbx_Matricula.Enabled = false;

                    Btn_Buscar.Text = "Otro";
                    Btn_Buscar.Enabled = true;

                    Btn_Guardar.Text = "Modificar";
                    Btn_Guardar.Enabled = true;
                    Btn_Eliminar.Enabled = true;

                    Tbx_Matricula.Focus();
                    Tbx_Matricula.SelectAll();
                }
            }

        }

        private bool Buscar_Registro(string matricula)
        {
            //Convertir string a entero
            //int mat = Convert.ToInt32(matricula);

            // conexión *************************
            OleDbConnection Conexion = new OleDbConnection();   // Crea la conexión
            Conexion.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source = " + Application.StartupPath + @"\Notas_e_Indice_BBDD\Notas_e_Indice_BBDD.accdb;" + "Persist Security Info = false";
            //Configurar la conexión, indicandole el proveedor de MIcrosoft que nos permite usar ACCESS
            //El origen del archivo de la base de datos ACCESS. Nota: los '\' se deben de escribir dobles '\\'
            //Y el dato de seguridad


            // cadena SQL  *************************
            //Esta es una cadena SQL que nos permitira hacer la busqueda
            String CadenaSQL = "SELECT * FROM Alumnos WHERE matricula=" + "'" + matricula + "'"; //selecciona todos los campor de la tabla personal donde el id sea igual al codigo

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
                Tbx_Nombre.Text = ds.Tables[0].Rows[0]["Nombre"].ToString();
                Tbx_Apellido.Text = ds.Tables[0].Rows[0]["Apellido"].ToString();
                Tbx_Direccion.Text = ds.Tables[0].Rows[0]["Direccion"].ToString();
                Tbx_Telefono.Text = ds.Tables[0].Rows[0]["Telefono"].ToString();
                Tbx_Edad.Text = ds.Tables[0].Rows[0]["Edad"].ToString();

                sexo = ds.Tables[0].Rows[0]["Sexo"].ToString();
                if (sexo == "Masculino")
                {
                    Rbtn_Masculino.Checked = true;
                }
                else
                {
                    Rbtn_Femenino.Checked = true;
                }

                Tbx_Carrera.Text = ds.Tables[0].Rows[0]["Carrera"].ToString();
                //dentro del dataset en la tabla 0, en la fila 0, con el campo nombre... y se convierte a string.


                // cerrar o liberar la memoria del dataset
                ds.Dispose();

                return true; //el registro existe
            }
        }

        private void Bloquear_Controles() // Bloquea controles
        {
            Tbx_Matricula.Enabled = true;
            Tbx_Nombre.Enabled = false;
            Tbx_Apellido.Enabled = false;
            Tbx_Direccion.Enabled = false;
            Tbx_Telefono.Enabled = false;
            Tbx_Edad.Enabled = false;
            tableLayoutSexo.Enabled = false;
            Tbx_Carrera.Enabled = false;

            Lab_Matricula.Enabled = true;
            Lab_Nombre.Enabled = false;
            Lab_Apellido.Enabled = false;
            Lab_Direccion.Enabled = false;
            Lab_Telefono.Enabled = false;
            Lab_Edad.Enabled = false;
            Lab_Sexo.Enabled = false;
            Lab_Carrera.Enabled = false;

            Btn_Guardar.Enabled = false;
            Btn_Buscar.Enabled = false;
            Btn_Limpiar.Enabled = false;
            Btn_Eliminar.Enabled = false;
        }
        private void Desbloquear_Controles() // Desbloquea controles
        {
            Tbx_Matricula.Enabled = false;
            Tbx_Nombre.Enabled = true;
            Tbx_Apellido.Enabled = true;
            Tbx_Direccion.Enabled = true;
            Tbx_Telefono.Enabled = true;
            Tbx_Edad.Enabled = true;
            tableLayoutSexo.Enabled = true;
            Tbx_Carrera.Enabled = true;

            Lab_Matricula.Enabled = true;
            Lab_Nombre.Enabled = true;
            Lab_Apellido.Enabled = true;
            Lab_Direccion.Enabled = true;
            Lab_Telefono.Enabled = true;
            Lab_Edad.Enabled = true;
            Lab_Sexo.Enabled = true;
            Lab_Carrera.Enabled = true;

            Btn_Guardar.Enabled = true;
            Btn_Buscar.Enabled = true;
            Btn_Limpiar.Enabled = true;
            Btn_Eliminar.Enabled = true;

            Tbx_Nombre.Focus(); // Pone el foco en el textbox nombre         
        }

        private void Frm_RegistroEstudiantes_Load(object sender, EventArgs e)
        {
            Bloquear_Controles();
        }

        private void Btn_Eliminar_Click(object sender, EventArgs e)
        {
            reproducirSonido("warning.wav", false);
            Thread.Sleep(100);

            //DialogResult Es un objeto que permite almacenar una respuesta.
            DialogResult Respuesta = MessageBox.Show("Está seguro de Eliminar este Alumno?", "Advertencia", MessageBoxButtons.YesNo);
            if (Respuesta == DialogResult.Yes) // si la respuesta es si
            {
                Eliminar_Registro(Tbx_Matricula.Text);

                MessageBox.Show("Alumno Eliminado!");

                Limpiar_Formulario();

                Btn_Buscar.Text = "Buscar";
                Btn_Guardar.Text = "Guardar";

                Bloquear_Controles();
                Btn_Buscar.Enabled = true;

                Tbx_Matricula.Focus();
                Tbx_Matricula.SelectAll();
            }
        }

        private bool Eliminar_Registro(string matricula)
        {
            //Solo cambia la Instrucción SQL y Se elimina el ID porque este no se cambiará


            //Conección
            OleDbConnection Conexion = new OleDbConnection();
            Conexion.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source = " + Application.StartupPath + @"\Notas_e_Indice_BBDD\Notas_e_Indice_BBDD.accdb;" + "Persist Security Info = false";

            //Instrucción SQL

            string CadenaSQL = "DELETE FROM Alumnos WHERE Matricula = '" + matricula + "'"; //Borrar de personal, donde el id es igual a el valor de Cod.


            //Crear comando
            OleDbCommand Comando = Conexion.CreateCommand();  //Asignamos la cadena SQL al comando
            Comando.CommandText = CadenaSQL; //configuracion del comando

            //Ejecutar la consulta de acción
            Conexion.Open(); //Abrimos la conexion
            Comando.ExecuteNonQuery(); //Ejecutamos la consulta
            Conexion.Close(); //Cerramos la conexion
            return true;
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
    }

}
