using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Media;
using System.Threading;

namespace RegistroNotas_e_IndiceAcademico
{
    public partial class Registro_Notas : Form
    {
        bool sinLaboratorio;
        bool registroExiste;
        bool notaExiste;
        string asignatura;
        string nombre;
        string apellido;
        string total = "0";
        double parcial1;
        double parcial2;
        double notaLab;
        double acumulado;
        double examen_Final;
        double valor_Practicas_Asignaturas = 30;
        double tl = 0; //Total

        SoundPlayer sonido;

        public Registro_Notas()
        {
            InitializeComponent();
        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Thread.Sleep(100);

            EditParent EP = Owner as EditParent;
            EP.Cambiar_Imagenes(false);
            EP.isHidden("Not");
            reproducirSonido("tech_Ambiente.wav", true);
            Hide();
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

        private void Tbx_1Parcial_TextChanged(object sender, EventArgs e)
        {
            
            if (Tbx_1Parcial.Text != "") // Solo entra aquí si no está vacio
            {
                parcial1 = Convert.ToDouble(Tbx_1Parcial.Text);
                if (parcial1 > 10)
                {
                    MessageBox.Show("La nota del Parcial debe ser de 0 a 10 Pts");
                    Tbx_1Parcial.Clear();
                }
                else
                {
                    Tbx_2Parcial.Enabled = true;

                    if (Tbx_1Parcial.Text.Length == 3)
                    {
                        Tbx_2Parcial.Focus();
                    }
                }
            }
            else
            {
                Tbx_1Parcial.Focus();
                Tbx_1Parcial.Text = "0";
                Tbx_1Parcial.SelectAll();
            }
        }

        private void Tbx_2Parcial_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_2Parcial.Text != "") // Solo entra aquí si no está vacio
            {
                parcial2 = Convert.ToDouble(Tbx_2Parcial.Text);
                if (parcial2 > 10)
                {
                    MessageBox.Show("La nota del Parcial debe ser de 0 a 10 Pts");
                    Tbx_2Parcial.Clear();
                }
                else
                {
                    if (Tbx_2Parcial.Text.Length == 3)
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
            }
            else
            {
                Tbx_2Parcial.Text = "0";
                Tbx_2Parcial.SelectAll();
            }
        }

        private void Tbx_NotaLab_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_NotaLab.Text != "") // Solo entra aquí si no está vacio
            {
                notaLab = Convert.ToDouble(Tbx_NotaLab.Text);
                if (notaLab > 30)
                {
                    MessageBox.Show("La nota de Laboratorio debe ser de 0 a 30");
                    Tbx_NotaLab.Clear();
                }
                else
                {

                    Tbx_Practicas_Asignaturas.Enabled = true;

                    if (Tbx_NotaLab.Text.Length == 4)
                    {
                        Tbx_Practicas_Asignaturas.Focus();
                    }
                }
            }
            else
            {
                Tbx_NotaLab.Text = "0";
                Tbx_NotaLab.SelectAll();
            }
        }

        private void Tbx_Practicas_Asignaturas_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_Practicas_Asignaturas.Text != "") // Solo entra aquí si no está vacio
            {

                acumulado = Convert.ToDouble(Tbx_Practicas_Asignaturas.Text);
                if (acumulado > valor_Practicas_Asignaturas)
                {
                    MessageBox.Show("La nota Acumulada debe ser de 0 a " + valor_Practicas_Asignaturas + " Pts");
                    Tbx_Practicas_Asignaturas.Clear();
                }
                else
                {
                    Tbx_Examen_Final.Enabled = true;

                    if (Tbx_Practicas_Asignaturas.Text.Length == 4)
                    {
                        Tbx_Examen_Final.Focus();
                    }
                }
            }
            else
            {
                Tbx_Practicas_Asignaturas.Text = "0";
                Tbx_Practicas_Asignaturas.SelectAll();
            }
        }

        private void Tbx_Examen_Final_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_Examen_Final.Text != "") // Solo entra aquí si no está vacio
            {
                examen_Final = Convert.ToDouble(Tbx_Examen_Final.Text);
                if (examen_Final > 20)
                {
                    MessageBox.Show("La nota del Examen Final debe ser de 0 a 20 Pts");
                    Tbx_Examen_Final.Clear();
                }
                else
                {
                    Btn_Calcular.Enabled = true;

                    if (Tbx_Examen_Final.Text.Length == 4)
                    {
                        Btn_Calcular.Focus();
                    }
                }
            }
            else
            {
                Tbx_Examen_Final.Text = "0";
                Tbx_Examen_Final.SelectAll();
            }
        }

        private void Btn_Calcular_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Thread.Sleep(100);
            string literal = "";
            string anuncio = "";
            tl = parcial1 + parcial2 + notaLab + acumulado + examen_Final;
            total = Convert.ToString(tl);

            if (tl >= 90)
            {
                literal = "A";
                anuncio = "¡Felicidades! has Aprobado";
            }
            else
            if (tl >= 80)
            {
                literal = "B";
                anuncio = "¡Felicidades! has Aprobado";
            }
            else
            if (tl >= 70)
            {
                literal = "C";
                anuncio = "¡Felicidades! has Aprobado";
            }
            else
            if (tl >= 60)
            {
                literal = "D";
                anuncio = "Vas a Extraordinario";
            }
            else
            {
                literal = "F";
                anuncio = "¡Lo siento! estás Reprobado";
            }

            Tbx_NotaFinal.Text = total;
            Tbx_Literal.Text = literal;
            Lab_Anuncio.Text = anuncio;

            Btn_Guardar.Focus();
        }

        private void Tbx_Nombre_TextChanged(object sender, EventArgs e)
        {
            nombre = Tbx_Nombre.Text;
        }

        private void Tbx_Apellido_TextChanged(object sender, EventArgs e)
        {
            apellido = Tbx_Apellido.Text;
        }

        private void Btn_Limpiar_Click(object sender, EventArgs e)
        {
            reproducirSonido("clean.wav", false);
            Thread.Sleep(100);

            Limpiar_Formulario();
        }

        private void Limpiar_Formulario()
        {
            Tbx_Nombre.Focus();
            Tbx_Nombre.Clear();
            Tbx_Apellido.Clear();
            Tbx_1Parcial.Clear();
            Tbx_2Parcial.Clear();
            Tbx_NotaLab.Clear();
            Tbx_Practicas_Asignaturas.Clear();
            Tbx_Examen_Final.Clear();
            Tbx_NotaFinal.Clear();
            Tbx_Literal.Clear();
            Lab_Anuncio.Text = "";
        }

        private void Btn_Buscar_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Thread.Sleep(100);

            if (Tbx_Matricula.Enabled == false)
            {
                registroExiste = false;
                Limpiar_Formulario();

                Btn_Guardar.Enabled = false;
                Btn_Eliminar.Enabled = false;
                Btn_Limpiar.Enabled = true;

                Btn_Buscar.Text = "Buscar";
                Btn_Guardar.Text = "Guardar";

                Tbx_Matricula.Enabled = true;
                Tbx_Matricula.Text = "0";
                Tbx_Matricula.Focus();
                Tbx_Matricula.SelectAll();
            }
            else
            {
                if (Buscar_Registro(Tbx_Matricula.Text, Cbx_Asignatura.Text) == false)
                {
                    registroExiste = false;
                    MessageBox.Show("Alumno No Encontrado!");
                    Limpiar_Formulario();
                    Desbloquear_Controles();
                    
                    Cbx_Asignatura.Enabled = false;

                    Btn_Guardar.Enabled = false;
                    Btn_Eliminar.Enabled = false;

                    Tbx_Nombre.Enabled = false;
                    Tbx_Apellido.Enabled = false;
                    Tbx_Matricula.Enabled = true;
                }
                else
                {
                    registroExiste = true;
                    MessageBox.Show("Alumno Encontrado!");

                    Bloquear_Controles();
                    //Desbloquear los controles que se pueden modificar
                    Tbx_1Parcial.Enabled = true;
                    Tbx_2Parcial.Enabled = true;
                    Tbx_NotaLab.Enabled = false;
                    Tbx_Practicas_Asignaturas.Enabled = true;
                    Tbx_Examen_Final.Enabled = true;
                    Tbx_Literal.Enabled = true;
                    Tbx_Matricula.Enabled = false;
                    Cbx_Cod_Asignatura.Enabled = true;
                    Cbx_Asignatura.Enabled = true;
                    
                    Btn_Buscar.Text = "Otro";
                    Btn_Buscar.Enabled = true;

                    if (notaExiste == false)
                    {
                        Btn_Guardar.Text = "Guardar";
                    }
                    else
                    {

                        Btn_Guardar.Text = "Modificar";
                    }

                    Btn_Guardar.Enabled = true;
                    Btn_Eliminar.Enabled = true;

                    Cbx_Cod_Asignatura.Focus();
                    Cbx_Cod_Asignatura.SelectAll();
                }
            }
        }

        private bool Buscar_Registro(string matricula, string asignatura)
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
            String CadenaSQL = "SELECT * FROM Alumnos WHERE Matricula=" + "'" + matricula + "'"; //selecciona todos los campor de la tabla personal donde el id sea igual al codigo
            String CadenaSQL2 = "SELECT * FROM Notas WHERE Matricula=" + "'" + matricula + "' AND Asignatura = '" + asignatura + "'";
            // adaptador  *************************
            OleDbDataAdapter Adaptador = new OleDbDataAdapter(CadenaSQL, Conexion); // Es una caja que se llena con cualquer dato: en este caso le pasamos como parametro
                                                                                    // la cadena SQL y la Coneccion
            OleDbDataAdapter Adaptador2 = new OleDbDataAdapter(CadenaSQL2, Conexion);

            // dataset  *************************
            // sirve par almacenar la tabla, es el objeto  con el cual podemos manipular los datos
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();


            // llenar el dataser  *************************
            Conexion.Open(); // abre la base de datos, lo que significa que se hace el enlace entre el programa y la base de datos
            Adaptador.Fill(ds); //llenamos el data set con el contenido del adaptador
            Adaptador2.Fill(ds2);
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
                //dentro del dataset en la tabla 0, en la fila 0, con el campo nombre... y se convierte a string.

                if (ds2.Tables[0].Rows.Count != 0)  // revisa las filas de la tabla 0 para ver su tamaño. Si es igual a cero entonces devuelve false, de lo contrario true.
                {
                    notaExiste = true;
                    Tbx_1Parcial.Text = ds2.Tables[0].Rows[0]["Primer_Parcial"].ToString();
                    Tbx_2Parcial.Text = ds2.Tables[0].Rows[0]["Segundo_Parcial"].ToString();
                    Tbx_NotaLab.Text = ds2.Tables[0].Rows[0]["Nota_Laboratorio"].ToString();
                    Tbx_Practicas_Asignaturas.Text = ds2.Tables[0].Rows[0]["Practica_Asignaturas"].ToString();
                    Tbx_Examen_Final.Text = ds2.Tables[0].Rows[0]["Examen_Final"].ToString();
                    Tbx_Creditos.Text = ds2.Tables[0].Rows[0]["Creditos"].ToString();
                }
                else
                {
                    // para saber si existe algun registro en la tabla notas y así decidir si guardarla o modificarla
                    notaExiste = false;

                    Tbx_1Parcial.Text = "0";
                    Tbx_2Parcial.Text = "0";
                    Tbx_NotaLab.Text = "0";
                    Tbx_Practicas_Asignaturas.Text = "0";
                    Tbx_Examen_Final.Text = "0";
                }
                // cerrar o liberar la memoria del dataset
                ds.Dispose();
                ds2.Dispose();

                return true; //el registro existe
            }
        }

        private void Cbx_Asignatura_SelectedIndexChanged(object sender, EventArgs e)
        {
            asignatura = Cbx_Asignatura.Text;
        }

        private void Tbx_1Parcial_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumFloat_y_Enter(e);
        }

        private void Tbx_2Parcial_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumFloat_y_Enter(e);
        }

        private void Tbx_NotaLab_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumFloat_y_Enter(e);
        }

        private void Tbx_Practicas_Asignaturas_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumFloat_y_Enter(e);
        }

        private void Tbx_Examen_Final_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                Btn_Calcular.Focus();
            }
            else
            {
                //acepta Números, Punto, espacios y caracteres de control como enter y backspace
                e.Handled = Validadores.ValidadorFlotante(e.KeyChar);
            }
            
        }

        //Escribir solo si son Números
        private void SoloNumFloat_y_Enter(KeyPressEventArgs e)
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
                //acepta Números, Punto, espacios y caracteres de control como enter y backspace
                e.Handled = Validadores.ValidadorFlotante(e.KeyChar);
            }
        }

        private void Btn_Guardar_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Thread.Sleep(100);

            if (registroExiste && notaExiste)
            {
                reproducirSonido("warning.wav", false);
                Thread.Sleep(100);

                //DialogResult Es un objeto que permite almacenar una respuesta.
                DialogResult Respuesta = MessageBox.Show("Está seguro de Modificar el registro actual?", "Advertencia", MessageBoxButtons.YesNo);
                if (Respuesta == DialogResult.Yes) // si la respuesta es si
                {
                    Modificar_Registro(Tbx_Creditos.Text, Cbx_Cuatrimestre.Text, Cbx_Cod_Asignatura.Text, Cbx_Asignatura.Text, Tbx_1Parcial.Text, Tbx_2Parcial.Text, Tbx_NotaLab.Text, Tbx_Practicas_Asignaturas.Text, Tbx_Examen_Final.Text, Tbx_Literal.Text);

                    MessageBox.Show("Registro Modificado!");

                    Btn_Limpiar.Enabled = true;

                    Cbx_Cod_Asignatura.Focus();
                    Cbx_Cod_Asignatura.SelectAll();
                }
            }
            else
            {
                Agregar_Registro(Tbx_Matricula.Text, Tbx_Creditos.Text, Cbx_Cuatrimestre.Text, Cbx_Cod_Asignatura.Text, Cbx_Asignatura.Text, Tbx_1Parcial.Text, Tbx_2Parcial.Text, Tbx_NotaLab.Text, Tbx_Practicas_Asignaturas.Text, Tbx_Examen_Final.Text, Tbx_Literal.Text);

                MessageBox.Show("Registro Agregado!");

                Limpiar_Formulario();
                Bloquear_Controles();
                Tbx_Matricula.Enabled = false;
                Btn_Guardar.Enabled = true;
                Btn_Buscar.Enabled = true;

                Cbx_Cod_Asignatura.Focus();
                Cbx_Cod_Asignatura.SelectAll();
            }
        }

        private bool Agregar_Registro(string Matricula, string Creditos1, string Cuatrimestre, string Codigo_Asignatura, string Asignatura, string Primer_Parcial1, string Segundo_Parcial1, string Nota_Laboratorio1, string Practicas_Asignaturas1, string Examen_Final1, string Literal)
        {
            //transformar de cadena a double
            double Creditos = Convert.ToDouble(Creditos1);
            double Indice_Constante100 = 0;
            double Indice_Constante4 = 0;
            double Primer_Parcial = Convert.ToDouble(Primer_Parcial1);
            double Segundo_Parcial = Convert.ToDouble(Segundo_Parcial1);
            double Nota_Laboratorio = Convert.ToDouble(Nota_Laboratorio1);
            double Practicas_Asignaturas = Convert.ToDouble(Practicas_Asignaturas1);
            double Examen_Final = Convert.ToDouble(Examen_Final1);


            //Conección
            OleDbConnection Conexion = new OleDbConnection();
            Conexion.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source = " + Application.StartupPath + @"\Notas_e_Indice_BBDD\Notas_e_Indice_BBDD.accdb;" + "Persist Security Info = false";


            //Instrucción SQL
            string CadenaSQL = "INSERT INTO Notas (Matricula, Creditos, Cuatrimestre, Codigo_Asignatura, Asignatura, Primer_Parcial, Segundo_Parcial, Nota_Laboratorio, Practica_Asignaturas, Examen_Final, Literal, Indice_Academico_100, Indice_Academico_4) ";
            CadenaSQL = CadenaSQL + "VALUES ('" + Matricula + "',";
            CadenaSQL = CadenaSQL + "       " + Creditos + ",";
            CadenaSQL = CadenaSQL + "       '" + Cuatrimestre + "',";
            CadenaSQL = CadenaSQL + "       '" + Codigo_Asignatura + "',";
            CadenaSQL = CadenaSQL + "       '" + Asignatura + "',";
            CadenaSQL = CadenaSQL + "       " + Primer_Parcial + ",";
            CadenaSQL = CadenaSQL + "       " + Segundo_Parcial + ",";
            CadenaSQL = CadenaSQL + "       " + Nota_Laboratorio + ",";
            CadenaSQL = CadenaSQL + "       " + Practicas_Asignaturas + ",";
            CadenaSQL = CadenaSQL + "       " + Examen_Final + ",";
            CadenaSQL = CadenaSQL + "       '" + Literal + "',";
            CadenaSQL = CadenaSQL + "       " + Indice_Constante100 + ",";
            CadenaSQL = CadenaSQL + "       " + Indice_Constante4 + ")";
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

        private bool Modificar_Registro(string Creditos1, string Cuatrimestre, string Codigo_Asignatura, string Asignatura, string Primer_Parcial1, string Segundo_Parcial1, string Nota_Laboratorio1, string Practicas_Asignaturas1, string Examen_Final1, string Literal)
        {
            //transformar de cadena a double
            double Primer_Parcial = Convert.ToDouble(Primer_Parcial1);
            double Segundo_Parcial = Convert.ToDouble(Segundo_Parcial1);
            double Nota_Laboratorio = Convert.ToDouble(Nota_Laboratorio1);
            double Practicas_Asignaturas = Convert.ToDouble(Practicas_Asignaturas1);
            double Examen_Final = Convert.ToDouble(Examen_Final1);


            //Conección
            OleDbConnection Conexion = new OleDbConnection();
            Conexion.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source = " + Application.StartupPath + @"\Notas_e_Indice_BBDD\Notas_e_Indice_BBDD.accdb;" + "Persist Security Info = false";


            //Instrucción SQL
            string CadenaSQL = "UPDATE Notas SET "; //Actualiza la tabla Notas estableciendo ...X datos... con los datos especificados.
            CadenaSQL = CadenaSQL + " Primer_Parcial = " + Primer_Parcial + ",";
            CadenaSQL = CadenaSQL + " Segundo_Parcial = " + Segundo_Parcial + ",";
            CadenaSQL = CadenaSQL + " Nota_Laboratorio = " + Nota_Laboratorio + ",";
            CadenaSQL = CadenaSQL + " Practica_Asignaturas = " + Practicas_Asignaturas + ",";
            CadenaSQL = CadenaSQL + " Examen_Final = " + Examen_Final + ",";
            CadenaSQL = CadenaSQL + " Literal      = '" + Literal + "' ";
            //Hay que decir donde es que se va a actualizar.
            //Actualizar lo anterior a este registro (osea, donde el id sea = a Cod.
            CadenaSQL = CadenaSQL + " WHERE Codigo_Asignatura = '" + Codigo_Asignatura + "'";
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


        private void Cbx_Cod_Asignatura_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string codigoAsignatura = Cbx_Cod_Asignatura.Text;

            switch (codigoAsignatura)
            {
                case "ADM-102":
                    {
                        SinLaboratorio();
                        Cbx_Asignatura.Text = "FUNDAMENTOS DE ADMINISTRACION Y GESTION DE PROYECTOS";
                        break;
                    }
                case "ADM-100":
                    {
                        SinLaboratorio();
                        Cbx_Asignatura.Text = "FUNDAMENTOS DE ADMINISTRACION ORGANIZACIONAL";
                        break;
                    }
                case "COM-103":
                    {
                        SinLaboratorio();
                        Cbx_Asignatura.Text = "COMUNICACION Y DINAMICAS DE GRUPO";
                        break;
                    }
                case "CON-100":
                    {
                        SinLaboratorio();
                        Cbx_Asignatura.Text = "CONTABILIDAD";
                        break;
                    }
                case "ELEC-101":
                    {
                        ConLaboratorio();
                        Cbx_Asignatura.Text = "FUNDAMENTOS DE ELECTRONICA PARA INFORMÁTICA";
                        break;
                    }
                case "EMP-100":
                    {
                        SinLaboratorio();
                        Cbx_Asignatura.Text = "EMPRENDIMIENTO";
                        break;
                    }
                case "ESP-101":
                    {
                        SinLaboratorio();
                        Cbx_Asignatura.Text = "LENGUA ESPAÑOLA";
                        break;
                    }
                case "EST-101":
                    {
                        SinLaboratorio();
                        Cbx_Asignatura.Text = "PROBABILIDAD Y ESTADÍSTICA";
                        break;
                    }
                case "ETI-100":
                    {
                        SinLaboratorio();
                        Cbx_Asignatura.Text = "ETICA PROFESIONAL";
                        break;
                    }
                case "FIS-100":
                    {
                        ConLaboratorio();
                        Cbx_Asignatura.Text = "FISICA BASICA";
                        break;
                    }
                case "HIST-101":
                    {
                        SinLaboratorio();
                        Cbx_Asignatura.Text = "HISTORIA SOCIAL DOMINICANA";
                        break;
                    }
                case "INF-101":
                    {
                        ConLaboratorio();
                        Cbx_Asignatura.Text = "INFORMATICA";
                        break;
                    }
                case "INF-102":
                    {
                        ConLaboratorio();
                        Cbx_Asignatura.Text = "FUNDAMENTOS DE PROGRAMACION";
                        break;
                    }
                case "INF-108":
                    {
                        SinLaboratorio();
                        Cbx_Asignatura.Text = "ARQUITECTURA Y ORGANIZACIÓN DEL COMPUTADOR";
                        break;
                    }
                case "ING-101":
                    {
                        ConLaboratorio();
                        Cbx_Asignatura.Text = "INGLES I";
                        break;
                    }
                case "ING-102":
                    {
                        ConLaboratorio();
                        Cbx_Asignatura.Text = "INGLES II";
                        break;
                    }
                case "ING-103":
                    {
                        ConLaboratorio();
                        Cbx_Asignatura.Text = "INGLES III";
                        break;
                    }
                case "ING-104":
                    {
                        ConLaboratorio();
                        Cbx_Asignatura.Text = "INGLES IV";
                        break;
                    }
                case "MAT-101":
                    {
                        SinLaboratorio();
                        Cbx_Asignatura.Text = "MATEMATICA";
                        break;
                    }
                case "MAT-102":
                    {
                        SinLaboratorio();
                        Cbx_Asignatura.Text = "CÁLCULO DIFERENCIAL";
                        break;
                    }
                case "MAT-103":
                    {
                        SinLaboratorio();
                        Cbx_Asignatura.Text = "CALCULO INTEGRAL";
                        break;
                    }
                case "MAT-105":
                    {
                        SinLaboratorio();
                        Cbx_Asignatura.Text = "MATEMÁTICA DISCRETA";
                        break;
                    }
                case "SOF-101":
                    {
                        ConLaboratorio();
                        Cbx_Asignatura.Text = "LOGICA DE PROGRAMACION";
                        break;
                    }
                case "SOF-102":
                    {
                        ConLaboratorio();
                        Cbx_Asignatura.Text = "PROGRAMACION ORIENTADA A OBJETO";
                        break;
                    }
                case "SOF-103":
                    {
                        SinLaboratorio();
                        Cbx_Asignatura.Text = "INTRODUCCION A LA INGENIERIA DE SOFTWARE";
                        break;
                    }
                case "SOF-104":
                    {
                        ConLaboratorio();
                        Cbx_Asignatura.Text = "ALGORITMOS Y ESTRUCTURAS DE DATOS";
                        break;
                    }
                case "SOF-105":
                    {
                        SinLaboratorio();
                        Cbx_Asignatura.Text = "SISTEMAS OPERATIVOS Y REDES";
                        break;
                    }
                case "SOF-106":
                    {
                        ConLaboratorio();
                        Cbx_Asignatura.Text = "INTERACCION HOMBRE/MAQUINA";
                        break;
                    }
                case "SOF-107":
                    {
                        ConLaboratorio();
                        Cbx_Asignatura.Text = "ANALISIS Y DISENO DE SOFTWARE";
                        break;
                    }
                case "SOF-108":
                    {
                        ConLaboratorio();
                        Cbx_Asignatura.Text = "BASE DE DATOS";
                        break;
                    }
                case "SOF-109":
                    {
                        ConLaboratorio();
                        Cbx_Asignatura.Text = "PROGRAMACION ORIENTADA AL WEB";
                        break;
                    }
                case "SOF-111":
                    {
                        ConLaboratorio();
                        Cbx_Asignatura.Text = "CONSTRUCCION DE SOFTWARE";
                        break;
                    }
                case "SOF-112":
                    {
                        ConLaboratorio();
                        Cbx_Asignatura.Text = "PRUEBA DE SOFTWARE";
                        break;
                    }
                case "SOF-113":
                    {
                        ConLaboratorio();
                        Cbx_Asignatura.Text = "PROYECTO FINAL";
                        break;
                    }
                case "SOF-300":
                    {
                        SinLaboratorio();
                        Cbx_Asignatura.Text = "ELECTIVA PROFESIONALIZANTE";
                        break;
                    }
            }

            

            if (Cbx_Cod_Asignatura.Text == "ESP-101" || Cbx_Cod_Asignatura.Text == "HIST-101" ||
                Cbx_Cod_Asignatura.Text == "INF-101" || Cbx_Cod_Asignatura.Text == "MAT-101")
            {
                Cbx_Cuatrimestre.Text = "PRIMER CUATRIMESTRE";
            }
            else
                if (Cbx_Cod_Asignatura.Text == "FIS-100" || Cbx_Cod_Asignatura.Text == "INF-102" ||
                Cbx_Cod_Asignatura.Text == "ING-101" || Cbx_Cod_Asignatura.Text == "MAT-102")
            {
                Cbx_Cuatrimestre.Text = "SEGUNDO CUATRIMESTRE";
            }
            else
                if (Cbx_Cod_Asignatura.Text == "COM-103" || Cbx_Cod_Asignatura.Text == "ELEC-101" ||
                Cbx_Cod_Asignatura.Text == "ING-102" || Cbx_Cod_Asignatura.Text == "MAT-103" || Cbx_Cod_Asignatura.Text == "SOF-101")
            {
                Cbx_Cuatrimestre.Text = "TERCER CUATRIMESTRE";
            }
            else
                if (Cbx_Cod_Asignatura.Text == "EST-101" || Cbx_Cod_Asignatura.Text == "INF-108" ||
                Cbx_Cod_Asignatura.Text == "ING-103" || Cbx_Cod_Asignatura.Text == "MAT-105" || Cbx_Cod_Asignatura.Text == "SOF-102")
            {
                Cbx_Cuatrimestre.Text = "CUARTO CUATRIMESTRE";
            }
            else
                if (Cbx_Cod_Asignatura.Text == "CON-100" || Cbx_Cod_Asignatura.Text == "ING-104" ||
                Cbx_Cod_Asignatura.Text == "SOF-103" || Cbx_Cod_Asignatura.Text == "SOF-104" || Cbx_Cod_Asignatura.Text == "SOF-105")
            {
                Cbx_Cuatrimestre.Text = "QUINTO CUATRIMESTRE";
            }
            else
                 if (Cbx_Cod_Asignatura.Text == "ADM-100" || Cbx_Cod_Asignatura.Text == "EMP-100" || Cbx_Cod_Asignatura.Text == "SOF-106" ||
                Cbx_Cod_Asignatura.Text == "SOF-107" || Cbx_Cod_Asignatura.Text == "SOF-108" || Cbx_Cod_Asignatura.Text == "SOF-109" || Cbx_Cod_Asignatura.Text == "SOF-300")
            {
                Cbx_Cuatrimestre.Text = "SEXTO CUATRIMESTRE";
            }
            else
                if (Cbx_Cod_Asignatura.Text == "ADM-102" || Cbx_Cod_Asignatura.Text == "ETI-100" ||
                Cbx_Cod_Asignatura.Text == "SOF-111" || Cbx_Cod_Asignatura.Text == "SOF-112" || Cbx_Cod_Asignatura.Text == "SOF-113")
            {
                Cbx_Cuatrimestre.Text = "SEPTIMO CUATRIMESTRE";
            }

        }


        private void Bloquear_Controles() // Bloquea controles
        {
            Tbx_Matricula.Enabled = true;
            Tbx_Nombre.Enabled = false;
            Tbx_Apellido.Enabled = false;
            //Tbx_1Parcial.Enabled = false;
            //Tbx_2Parcial.Enabled = false;
            //Tbx_NotaLab.Enabled = false;
            //Tbx_Practicas_Asignaturas.Enabled = false;
            //Tbx_Examen_Final.Enabled = false;
            Tbx_Creditos.Enabled = false;
            Tbx_NotaFinal.Enabled = false;
            Tbx_Literal.Enabled = false;
            
            //Cbx_Asignatura.Enabled = false;

            //Btn_Calcular.Enabled = false;
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
            Tbx_1Parcial.Enabled = true;
            Tbx_2Parcial.Enabled = true;
            Tbx_NotaLab.Enabled = true;
            Tbx_Practicas_Asignaturas.Enabled = true;
            Tbx_Examen_Final.Enabled = true;
            
            Cbx_Asignatura.Enabled = true;

            Btn_Guardar.Enabled = true;
            Btn_Buscar.Enabled = true;
            Btn_Limpiar.Enabled = true;
            Btn_Eliminar.Enabled = true;

            Tbx_Nombre.Focus(); // Pone el foco en el textbox nombre         
        }

        private void Registro_Notas_Load(object sender, EventArgs e)
        {
            Bloquear_Controles();

            Btn_Limpiar.Enabled = true;
            Cbx_Cod_Asignatura.Enabled = false;
            Cbx_Asignatura.Enabled = false;
            Tbx_NotaLab.Enabled = false;

            Cbx_Asignatura.Text = "FUNDAMENTOS DE ADMINISTRACION ORGANIZACIONAL";
        }

        private void Btn_Eliminar_Click(object sender, EventArgs e)
        {
            reproducirSonido("warning.wav", false);
            Thread.Sleep(100);

            //DialogResult Es un objeto que permite almacenar una respuesta.
            DialogResult Respuesta = MessageBox.Show("Está seguro de Eliminar el Registro Actual?", "Advertencia", MessageBoxButtons.YesNo);
            if (Respuesta == DialogResult.Yes) // si la respuesta es si
            {
                Eliminar_Registro(Tbx_Matricula.Text);

                MessageBox.Show("Registro Eliminado!");

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

            string CadenaSQL = "DELETE FROM Notas WHERE Matricula = '" + matricula + "'"; //Borrar de personal, donde el id es igual a el valor de Cod.


            //Crear comando
            OleDbCommand Comando = Conexion.CreateCommand();  //Asignamos la cadena SQL al comando
            Comando.CommandText = CadenaSQL; //configuracion del comando

            //Ejecutar la consulta de acción
            Conexion.Open(); //Abrimos la conexion
            Comando.ExecuteNonQuery(); //Ejecutamos la consulta
            Conexion.Close(); //Cerramos la conexion
            return true;
        }

        private void Cbx_Asignatura_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            string Asignatura = Cbx_Asignatura.Text;

            switch (Asignatura)
            {
                case "FUNDAMENTOS DE ADMINISTRACION Y GESTION DE PROYECTOS":
                    {
                        SinLaboratorio();
                        Cbx_Cod_Asignatura.Text = "ADM-102";
                        Tbx_Creditos.Text = "3";
                        break;
                    }
                case "FUNDAMENTOS DE ADMINISTRACION ORGANIZACIONAL":
                    {
                        SinLaboratorio();
                        Cbx_Cod_Asignatura.Text = "ADM-100";
                        Tbx_Creditos.Text = "3";
                        break;
                    }
                case "COMUNICACION Y DINAMICAS DE GRUPO":
                    {
                        SinLaboratorio();
                        Cbx_Cod_Asignatura.Text = "COM-103";
                        Tbx_Creditos.Text = "2";
                        break;
                    }
                case "CONTABILIDAD":
                    {
                        SinLaboratorio();
                        Cbx_Cod_Asignatura.Text = "CON-100";
                        Tbx_Creditos.Text = "3";
                        break;
                    }
                case "FUNDAMENTOS DE ELECTRONICA PARA INFORMÁTICA":
                    {
                        ConLaboratorio();
                        Cbx_Cod_Asignatura.Text = "ELEC-101";
                        Tbx_Creditos.Text = "3";
                        break;
                    }
                case "EMPRENDIMIENTO":
                    {
                        SinLaboratorio();
                        Cbx_Cod_Asignatura.Text = "EMP-100";
                        Tbx_Creditos.Text = "2";
                        break;
                    }
                case "LENGUA ESPAÑOLA":
                    {
                        SinLaboratorio();
                        Cbx_Cod_Asignatura.Text = "ESP-101";
                        Tbx_Creditos.Text = "3";
                        break;
                    }
                case "PROBABILIDAD Y ESTADÍSTICA":
                    {
                        SinLaboratorio();
                        Cbx_Cod_Asignatura.Text = "EST-101";
                        Tbx_Creditos.Text = "4";
                        break;
                    }
                case "ETICA PROFESIONAL":
                    {
                        SinLaboratorio();
                        Cbx_Cod_Asignatura.Text = "ETI-100";
                        Tbx_Creditos.Text = "2";
                        break;
                    }
                case "FISICA BASICA":
                    {
                        ConLaboratorio();
                        Cbx_Cod_Asignatura.Text = "FIS-100";
                        Tbx_Creditos.Text = "4";
                        break;
                    }
                case "HISTORIA SOCIAL DOMINICANA":
                    {
                        SinLaboratorio();
                        Cbx_Cod_Asignatura.Text = "HIST-101";
                        Tbx_Creditos.Text = "3";
                        break;
                    }
                case "INFORMATICA":
                    {
                        ConLaboratorio();
                        Cbx_Cod_Asignatura.Text = "INF-101";
                        Tbx_Creditos.Text = "2";
                        break;
                    }
                case "FUNDAMENTOS DE PROGRAMACION":
                    {
                        ConLaboratorio();
                        Cbx_Cod_Asignatura.Text = "INF-102";
                        Tbx_Creditos.Text = "4";
                        break;
                    }
                case "ARQUITECTURA Y ORGANIZACIÓN DEL COMPUTADOR":
                    {
                        SinLaboratorio();
                        Cbx_Cod_Asignatura.Text = "INF-108";
                        Tbx_Creditos.Text = "3";
                        break;
                    }
                case "INGLES I":
                    {
                        ConLaboratorio();
                        Cbx_Cod_Asignatura.Text = "ING-101";
                        Tbx_Creditos.Text = "3";
                        break;
                    }
                case "INGLES II":
                    {
                        ConLaboratorio();
                        Cbx_Cod_Asignatura.Text = "ING-102";
                        Tbx_Creditos.Text = "3";
                        break;
                    }
                case "INGLES III":
                    {
                        ConLaboratorio();
                        Cbx_Cod_Asignatura.Text = "ING-103";
                        Tbx_Creditos.Text = "3";
                        break;
                    }
                case "INGLES IV":
                    {
                        ConLaboratorio();
                        Cbx_Cod_Asignatura.Text = "ING-104";
                        Tbx_Creditos.Text = "3";
                        break;
                    }
                case "MATEMATICA":
                    {
                        SinLaboratorio();
                        Cbx_Cod_Asignatura.Text = "MAT-101";
                        Tbx_Creditos.Text = "4";
                        break;
                    }
                case "CÁLCULO DIFERENCIAL":
                    {
                        SinLaboratorio();
                        Cbx_Cod_Asignatura.Text = "MAT-102";
                        Tbx_Creditos.Text = "4";
                        break;
                    }
                case "CALCULO INTEGRAL":
                    {
                        SinLaboratorio();
                        Cbx_Cod_Asignatura.Text = "MAT-103";
                        Tbx_Creditos.Text = "4";
                        break;
                    }
                case "MATEMÁTICA DISCRETA":
                    {
                        SinLaboratorio();
                        Cbx_Cod_Asignatura.Text = "MAT-105";
                        Tbx_Creditos.Text = "4";
                        break;
                    }
                case "LOGICA DE PROGRAMACION":
                    {
                        ConLaboratorio();
                        Cbx_Cod_Asignatura.Text = "SOF-101";
                        Tbx_Creditos.Text = "3";
                        break;
                    }
                case "PROGRAMACION ORIENTADA A OBJETO":
                    {
                        ConLaboratorio();
                        Cbx_Cod_Asignatura.Text = "SOF-102";
                        Tbx_Creditos.Text = "3";
                        break;
                    }
                case "INTRODUCCION A LA INGENIERIA DE SOFTWARE":
                    {
                        SinLaboratorio();
                        Cbx_Cod_Asignatura.Text = "SOF-103";
                        Tbx_Creditos.Text = "3";
                        break;
                    }
                case "ALGORITMOS Y ESTRUCTURAS DE DATOS":
                    {
                        ConLaboratorio();
                        Cbx_Cod_Asignatura.Text = "SOF-104";
                        Tbx_Creditos.Text = "4";
                        break;
                    }
                case "SISTEMAS OPERATIVOS Y REDES":
                    {
                        SinLaboratorio();
                        Cbx_Cod_Asignatura.Text = "SOF-105";
                        Tbx_Creditos.Text = "4";
                        break;
                    }
                case "INTERACCION HOMBRE/MAQUINA":
                    {
                        ConLaboratorio();
                        Cbx_Cod_Asignatura.Text = "SOF-106";
                        Tbx_Creditos.Text = "3";
                        break;
                    }
                case "ANALISIS Y DISENO DE SOFTWARE":
                    {
                        ConLaboratorio();
                        Cbx_Cod_Asignatura.Text = "SOF-107";
                        Tbx_Creditos.Text = "4";
                        break;
                    }
                case "BASE DE DATOS":
                    {
                        ConLaboratorio();
                        Cbx_Cod_Asignatura.Text = "SOF-108";
                        Tbx_Creditos.Text = "4";
                        break;
                    }
                case "PROGRAMACION ORIENTADA AL WEB":
                    {
                        ConLaboratorio();
                        Cbx_Cod_Asignatura.Text = "SOF-109";
                        Tbx_Creditos.Text = "3";
                        break;
                    }
                case "CONSTRUCCION DE SOFTWARE":
                    {
                        ConLaboratorio();
                        Cbx_Cod_Asignatura.Text = "SOF-111";
                        Tbx_Creditos.Text = "4";
                        break;
                    }
                case "PRUEBA DE SOFTWARE":
                    {
                        ConLaboratorio();
                        Cbx_Cod_Asignatura.Text = "SOF-112";
                        Tbx_Creditos.Text = "3";
                        break;
                    }
                case "PROYECTO FINAL":
                    {
                        ConLaboratorio();
                        Cbx_Cod_Asignatura.Text = "SOF-113";
                        Tbx_Creditos.Text = "4";
                        break;
                    }
                case "ELECTIVA PROFESIONALIZANTE":
                    {
                        SinLaboratorio();
                        Cbx_Cod_Asignatura.Text = "SOF-300";
                        Tbx_Creditos.Text = "3";
                        break;
                    }
            }
        }

        private void SinLaboratorio()
        {
            sinLaboratorio = true;
            valor_Practicas_Asignaturas = 60;

            //Para buscar al cambiar la asignatura
            if (registroExiste)
            {
                Buscar_Registro(Tbx_Matricula.Text, Cbx_Asignatura.Text);
            }

            Tbx_NotaLab.Enabled = false;
        }

        private void ConLaboratorio()
        {
            sinLaboratorio = false;
            valor_Practicas_Asignaturas = 30;

            //Para buscar al cambiar la asignatura
            if (registroExiste)
            {
                Buscar_Registro(Tbx_Matricula.Text, Cbx_Asignatura.Text);
            }

            Tbx_NotaLab.Enabled = true;
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

        private void Btn_Random_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Thread.Sleep(100);
            reproducirSonido("calculating.wav", false);
            Thread.Sleep(100);

            Random rand = new Random();
            double Parcial1 = rand.Next(5, 10);
            double Parcial2 = rand.Next(5, 10);
            double Ex_Final = rand.Next(10, 20);
            double Pract_Asig_30 = rand.Next(20, 30);
            double Pract_Asig_60 = rand.Next(45, 60);
            double Laborat = rand.Next(20, 30);

            Tbx_1Parcial.Text = Parcial1.ToString();
            Tbx_2Parcial.Text = Parcial2.ToString();
            Tbx_Examen_Final.Text = Ex_Final.ToString();

            if (sinLaboratorio)
            {
                Tbx_NotaLab.Enabled = false;
                Tbx_Practicas_Asignaturas.Text = Pract_Asig_60.ToString();
            }
            else
            {
                Tbx_NotaLab.Enabled = true;
                Tbx_Practicas_Asignaturas.Text = Pract_Asig_30.ToString();
                Tbx_NotaLab.Text = Laborat.ToString();
            }
        }
    }
}
