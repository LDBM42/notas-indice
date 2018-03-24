using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Media;
using System.Threading;

namespace RegistroNotas_e_IndiceAcademico
{

    public partial class Frm_Calculo_Indice : Form
    {
        // Variables Globales
        bool activar_Mensaje = false;

        double indice_Base_100;
        double indice_Base_4;

        SoundPlayer sonido;

        public Frm_Calculo_Indice()
        {
            InitializeComponent();
        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Thread.Sleep(100);

            EditParent EP = this.Owner as EditParent;
            EP.Cambiar_Imagenes(false);
            EP.isHidden("Ind");
            reproducirSonido("tech_Ambiente.wav", true);
            Hide();
        }

        private void Tbx_Matricula_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_Matricula.Text == "")
            {
                Tbx_Matricula.Focus();
                Tbx_Matricula.Text = "0";
                Tbx_Matricula.SelectAll();
            }
        }

        private void Tbx_Matricula_TextChanged_1(object sender, EventArgs e)
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
                Btn_Buscar.Focus();
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

        private void Btn_Buscar_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Thread.Sleep(100);

            if (Tbx_Matricula.Enabled == false)
            {
                Limpiar_Formulario();

                Btn_Buscar.Text = "Buscar";
                Btn_Guardar.Text = "Guardar";
                Desbloquear_Controles();
                Btn_Buscar.Enabled = true;
                Btn_Guardar.Enabled = false;

                Cbx_Cuatrimestre.Enabled = false;
                Tbx_Nombre.Enabled = false;
                Tbx_Apellido.Enabled = false;

                activar_Mensaje = false;

                Tbx_Matricula.Enabled = true;
                Tbx_Matricula.Text = "0";
                Tbx_Matricula.Focus();
                Tbx_Matricula.SelectAll();
            }
            else
            {
                if (Buscar_Registro(Tbx_Matricula.Text, Cbx_Cod_Asignatura1.Text, Cbx_Cod_Asignatura2.Text, Cbx_Cod_Asignatura3.Text, Cbx_Cod_Asignatura4.Text, Cbx_Cod_Asignatura5.Text, Cbx_Cod_Asignatura6.Text, Cbx_Cod_Asignatura7.Text) == false)
                {
                    MessageBox.Show("Alumno No Encontrado!");
                    Limpiar_Formulario();
                    Desbloquear_Controles();

                    Tbx_Nombre.Enabled = false;
                    Tbx_Apellido.Enabled = false;

                    Btn_Guardar.Enabled = false;

                    activar_Mensaje = false;

                    Tbx_Matricula.Enabled = true;
                    Tbx_Matricula.Focus();
                    Tbx_Matricula.SelectAll();

                }
                else
                {
                    MessageBox.Show("Alumno Encontrado!");

                    Calcular_IndiceTotal(Tbx_Matricula.Text);

                    Bloquear_Controles();
                    Cbx_Cuatrimestre.Enabled = true;
                    Tbx_Matricula.Enabled = false;

                    Btn_Buscar.Text = "Otro";
                    Btn_Buscar.Enabled = true;

                    Btn_Guardar.Text = "Modificar";

                    activar_Mensaje = true;
                    timer1.Start();

                    Btn_Calcular.Enabled = true;
                    Btn_Calcular.Focus();


                }
            }
        }

        private bool Buscar_Registro(string matricula, string cod_asig1, string cod_asig2, string cod_asig3, string cod_asig4, string cod_asig5, string cod_asig6, string cod_asig7)
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
            String CadenaSQL = "SELECT * FROM Alumnos WHERE Matricula=" + "'" + matricula + "'"; //selecciona todos los campor de la tabla personal donde Mtricula sea igual a matricula
            String CadenaSQL1 = "SELECT * FROM Notas WHERE Matricula=" + "'" + matricula + "' AND Codigo_Asignatura = '" + cod_asig1 + "'";
            String CadenaSQL2 = "SELECT * FROM Notas WHERE Matricula=" + "'" + matricula + "' AND Codigo_Asignatura = '" + cod_asig2 + "'";
            String CadenaSQL3 = "SELECT * FROM Notas WHERE Matricula=" + "'" + matricula + "' AND Codigo_Asignatura = '" + cod_asig3 + "'";
            String CadenaSQL4 = "SELECT * FROM Notas WHERE Matricula=" + "'" + matricula + "' AND Codigo_Asignatura = '" + cod_asig4 + "'";
            String CadenaSQL5 = "SELECT * FROM Notas WHERE Matricula=" + "'" + matricula + "' AND Codigo_Asignatura = '" + cod_asig5 + "'";
            String CadenaSQL6 = "SELECT * FROM Notas WHERE Matricula=" + "'" + matricula + "' AND Codigo_Asignatura = '" + cod_asig6 + "'";
            String CadenaSQL7 = "SELECT * FROM Notas WHERE Matricula=" + "'" + matricula + "' AND Codigo_Asignatura = '" + cod_asig7 + "'";
            // adaptador  *************************
            OleDbDataAdapter Adaptador = new OleDbDataAdapter(CadenaSQL, Conexion); // Es una caja que se llena con cualquer dato: en este caso le pasamos como parametro
                                                                                    // la cadena SQL y la Coneccion
            OleDbDataAdapter Adaptador1 = new OleDbDataAdapter(CadenaSQL1, Conexion);
            OleDbDataAdapter Adaptador2 = new OleDbDataAdapter(CadenaSQL2, Conexion);
            OleDbDataAdapter Adaptador3 = new OleDbDataAdapter(CadenaSQL3, Conexion);
            OleDbDataAdapter Adaptador4 = new OleDbDataAdapter(CadenaSQL4, Conexion);
            OleDbDataAdapter Adaptador5 = new OleDbDataAdapter(CadenaSQL5, Conexion);
            OleDbDataAdapter Adaptador6 = new OleDbDataAdapter(CadenaSQL6, Conexion);
            OleDbDataAdapter Adaptador7 = new OleDbDataAdapter(CadenaSQL7, Conexion);

            // dataset  *************************
            // sirve par almacenar la tabla, es el objeto  con el cual podemos manipular los datos
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            DataSet ds2 = new DataSet();
            DataSet ds3 = new DataSet();
            DataSet ds4 = new DataSet();
            DataSet ds5 = new DataSet();
            DataSet ds6 = new DataSet();
            DataSet ds7 = new DataSet();


            // llenar el dataser  *************************
            Conexion.Open(); // abre la base de datos, lo que significa que se hace el enlace entre el programa y la base de datos
            Adaptador.Fill(ds); //llenamos el data set con el contenido del adaptador
            Adaptador1.Fill(ds1);
            Adaptador2.Fill(ds2);
            Adaptador3.Fill(ds3);
            Adaptador4.Fill(ds4);
            Adaptador5.Fill(ds5);
            Adaptador6.Fill(ds6);
            Adaptador7.Fill(ds7);

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
                //cargar los campos Nombre y Apellido con los datos del registro
                Tbx_Nombre.Text = ds.Tables[0].Rows[0]["Nombre"].ToString();
                Tbx_Apellido.Text = ds.Tables[0].Rows[0]["Apellido"].ToString();
                //dentro del dataset en la tabla 0, en la fila 0, con el campo Nombre... y se convierte a string.

                Tbx_Base100.Text = ds1.Tables[0].Rows[0]["Indice_Academico_100"].ToString();
                Tbx_Base4.Text = ds1.Tables[0].Rows[0]["Indice_Academico_4"].ToString();
                
                
                if (ds1.Tables[0].Rows.Count != 0)  // revisa las filas de la tabla 0 para ver su tamaño. Si es igual a cero entonces devuelve false, de lo contrario true.
                {
                    Tbx_CR1.Text = ds1.Tables[0].Rows[0]["Creditos"].ToString();
                    Tbx_Nota1.Text = ds1.Tables[0].Rows[0]["Nota_Final"].ToString();
                    Tbx_Base100.Text = ds1.Tables[0].Rows[0]["Indice_Academico_100"].ToString();
                    Tbx_Base4.Text = ds1.Tables[0].Rows[0]["Indice_Academico_4"].ToString();
                }
                else
                {
                    Tbx_CR1.Text = "0";
                    Tbx_Nota1.Text = "0";
                }

                if (ds2.Tables[0].Rows.Count != 0)  // revisa las filas de la tabla 0 para ver su tamaño. Si es igual a cero entonces devuelve false, de lo contrario true.
                {
                    Tbx_CR2.Text = ds2.Tables[0].Rows[0]["Creditos"].ToString();
                    Tbx_Nota2.Text = ds2.Tables[0].Rows[0]["Nota_Final"].ToString();
                }
                else
                {
                    Tbx_CR2.Text = "0";
                    Tbx_Nota2.Text = "0";
                }

                if (ds3.Tables[0].Rows.Count != 0)  // revisa las filas de la tabla 0 para ver su tamaño. Si es igual a cero entonces devuelve false, de lo contrario true.
                {
                    Tbx_CR3.Text = ds3.Tables[0].Rows[0]["Creditos"].ToString();
                    Tbx_Nota3.Text = ds3.Tables[0].Rows[0]["Nota_Final"].ToString();
                }
                else
                {
                    Tbx_CR3.Text = "0";
                    Tbx_Nota3.Text = "0";
                }

                if (ds4.Tables[0].Rows.Count != 0)  // revisa las filas de la tabla 0 para ver su tamaño. Si es igual a cero entonces devuelve false, de lo contrario true.
                {
                    Tbx_CR4.Text = ds4.Tables[0].Rows[0]["Creditos"].ToString();
                    Tbx_Nota4.Text = ds4.Tables[0].Rows[0]["Nota_Final"].ToString();
                }
                else
                {
                    Tbx_CR4.Text = "0";
                    Tbx_Nota4.Text = "0";
                }

                if (ds5.Tables[0].Rows.Count != 0)  // revisa las filas de la tabla 0 para ver su tamaño. Si es igual a cero entonces devuelve false, de lo contrario true.
                {
                    Tbx_CR5.Text = ds5.Tables[0].Rows[0]["Creditos"].ToString();
                    Tbx_Nota5.Text = ds5.Tables[0].Rows[0]["Nota_Final"].ToString();
                }
                else
                {
                    Tbx_CR5.Text = "0";
                    Tbx_Nota5.Text = "0";
                }

                if (ds6.Tables[0].Rows.Count != 0)  // revisa las filas de la tabla 0 para ver su tamaño. Si es igual a cero entonces devuelve false, de lo contrario true.
                {
                    Tbx_CR6.Text = ds6.Tables[0].Rows[0]["Creditos"].ToString();
                    Tbx_Nota6.Text = ds6.Tables[0].Rows[0]["Nota_Final"].ToString();
                }
                else
                {
                    Tbx_CR6.Text = "0";
                    Tbx_Nota6.Text = "0";
                }

                if (ds7.Tables[0].Rows.Count != 0)  // revisa las filas de la tabla 0 para ver su tamaño. Si es igual a cero entonces devuelve false, de lo contrario true.
                {
                    Tbx_CR7.Text = ds7.Tables[0].Rows[0]["Creditos"].ToString();
                    Tbx_Nota7.Text = ds7.Tables[0].Rows[0]["Nota_Final"].ToString();
                }
                else
                {
                    Tbx_CR7.Text = "0";
                    Tbx_Nota7.Text = "0";
                }

                //Calcula el indice total
                Calcular_IndiceTotal(matricula);

                // cerrar o liberar la memoria del dataset
                ds.Dispose();
                ds1.Dispose();
                ds2.Dispose();
                ds3.Dispose();
                ds4.Dispose();
                ds5.Dispose();
                ds6.Dispose();
                ds7.Dispose();

                return true; //el registro existe
            }
        }

        private bool Calcular_IndiceTotal(string matricula)
        {
            // conexión *************************
            OleDbConnection Conexion = new OleDbConnection();   // Crea la conexión
            Conexion.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source = " + Application.StartupPath + @"\Notas_e_Indice_BBDD\Notas_e_Indice_BBDD.accdb;" + "Persist Security Info = false";
            //Configurar la conexión, indicandole el proveedor de MIcrosoft que nos permite usar ACCESS
            //El origen del archivo de la base de datos ACCESS. Nota: los '\' se deben de escribir dobles '\\'
            //Y el dato de seguridad


            // cadena SQL  *************************
            //Esta es una cadena SQL que nos permitira hacer la busqueda
            String CadenaSQL01 = "SELECT DISTINCT Notas.Cuatrimestre, Notas.Indice_Academico_100, Notas.Indice_Academico_4 FROM Notas WHERE Matricula = '" + matricula + "'";
           
            // adaptador  *************************
           // Es una caja que se llena con cualquer dato: en este caso le pasamos como parametro
                                                                                    // la cadena SQL y la Coneccion
            OleDbDataAdapter Adaptador01 = new OleDbDataAdapter(CadenaSQL01, Conexion); 

            // dataset  *************************
            // sirve par almacenar la tabla, es el objeto  con el cual podemos manipular los datos
           
            DataSet ds01 = new DataSet();
           


            // llenar el dataser  *************************
            Conexion.Open(); // abre la base de datos, lo que significa que se hace el enlace entre el programa y la base de datos
            //llenamos el data set con el contenido del adaptador
            Adaptador01.Fill(ds01);
            
            Conexion.Close(); // cerramos la coneccion


            // contar registros  *************************
            // cuenta cuantos registros se almacenaron en el dataset
            if (ds01.Tables[0].Rows.Count == 0)  // revisa las filas de la tabla 0 para ver su tamaño. Si es igual a cero entonces devuelve false, de lo contrario true.
            {  // Cuantas filas o registros tiene la tabla cero del dataset?
                ds01.Dispose(); //para cerrar aunque el garbage collector lo hace automatico
                return false; //el registro no fue encontrado
            }
            else
            {
                // Calcular indice total
                Tbx_Base100_Total.Text = Math.Round(((Convert.ToDouble(ds01.Tables[0].Rows[0]["Indice_Academico_100"]) + Convert.ToDouble(ds01.Tables[0].Rows[1]["Indice_Academico_100"]) + Convert.ToDouble(ds01.Tables[0].Rows[2]["Indice_Academico_100"]) + Convert.ToDouble(ds01.Tables[0].Rows[3]["Indice_Academico_100"]) + Convert.ToDouble(ds01.Tables[0].Rows[4]["Indice_Academico_100"]) + Convert.ToDouble(ds01.Tables[0].Rows[5]["Indice_Academico_100"]) + Convert.ToDouble(ds01.Tables[0].Rows[6]["Indice_Academico_100"])) / 7), 2).ToString();
                Tbx_B4_Total.Text = Math.Round(((Convert.ToDouble(ds01.Tables[0].Rows[0]["Indice_Academico_4"]) + Convert.ToDouble(ds01.Tables[0].Rows[1]["Indice_Academico_4"]) + Convert.ToDouble(ds01.Tables[0].Rows[2]["Indice_Academico_4"]) + Convert.ToDouble(ds01.Tables[0].Rows[3]["Indice_Academico_4"]) + Convert.ToDouble(ds01.Tables[0].Rows[4]["Indice_Academico_4"]) + Convert.ToDouble(ds01.Tables[0].Rows[5]["Indice_Academico_4"]) + Convert.ToDouble(ds01.Tables[0].Rows[6]["Indice_Academico_4"])) / 7), 2).ToString();


                ds01.Dispose();
                return true; //el registro existe
            }
        }


        private void Btn_Calcular_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Thread.Sleep(100);

            double CR1 = Convert.ToDouble(Tbx_CR1.Text);
            double CR2 = Convert.ToDouble(Tbx_CR2.Text);
            double CR3 = Convert.ToDouble(Tbx_CR3.Text);
            double CR4 = Convert.ToDouble(Tbx_CR4.Text);
            double CR5 = Convert.ToDouble(Tbx_CR5.Text);
            double CR6 = Convert.ToDouble(Tbx_CR6.Text);
            double CR7 = Convert.ToDouble(Tbx_CR7.Text);

            double Nota1 = Convert.ToDouble(Tbx_Nota1.Text);
            double Nota2 = Convert.ToDouble(Tbx_Nota2.Text);
            double Nota3 = Convert.ToDouble(Tbx_Nota3.Text);
            double Nota4 = Convert.ToDouble(Tbx_Nota4.Text);
            double Nota5 = Convert.ToDouble(Tbx_Nota5.Text);
            double Nota6 = Convert.ToDouble(Tbx_Nota6.Text);
            double Nota7 = Convert.ToDouble(Tbx_Nota7.Text);


            double CR_Total = CR1 + CR2 + CR3 + CR4 + CR5 + CR6 + CR7;
            Tbx_CR_Total.Text = Convert.ToString(CR_Total);

            double CR_Nota1 = CR1 * Nota1;
            Tbx_CR_Nota1.Text = Convert.ToString(CR_Nota1);
            double CR_Nota2 = CR2 * Nota2;
            Tbx_CR_Nota2.Text = Convert.ToString(CR_Nota2);
            double CR_Nota3 = CR3 * Nota3;
            Tbx_CR_Nota3.Text = Convert.ToString(CR_Nota3);
            double CR_Nota4 = CR4 * Nota4;
            Tbx_CR_Nota4.Text = Convert.ToString(CR_Nota4);
            double CR_Nota5 = CR5 * Nota5;
            Tbx_CR_Nota5.Text = Convert.ToString(CR_Nota5);
            double CR_Nota6 = CR6 * Nota6;
            Tbx_CR_Nota6.Text = Convert.ToString(CR_Nota6);
            double CR_Nota7 = CR7 * Nota7;
            Tbx_CR_Nota7.Text = Convert.ToString(CR_Nota7);

            double CR_Nota_Total = CR_Nota1 + CR_Nota2 + CR_Nota3 + CR_Nota4 + CR_Nota5 + CR_Nota6 + CR_Nota7;
            Tbx_CR_Nota_Total.Text = Convert.ToString(CR_Nota_Total);

            indice_Base_100 = CR_Nota_Total / CR_Total;
            indice_Base_100 = Math.Round(indice_Base_100, 2);
            Tbx_Base100.Text = Convert.ToString(indice_Base_100);

            indice_Base_4 = (indice_Base_100 * 4) / 100;
            indice_Base_4 = Math.Round(indice_Base_4, 2);
            Tbx_Base4.Text = Convert.ToString(indice_Base_4);


            if (Btn_Guardar.Text == "Guardar")
            {
                Btn_Guardar.Enabled = false;
                Btn_Limpiar.Enabled = true;
            }
            else
            {
                Btn_Calcular.BackColor = Color.White;
                double base100 = Convert.ToDouble(Tbx_Base100.Text);
                if (base100 >= 1 || base100 <= 100)
                {
                    Btn_Guardar.Enabled = true;
                    Btn_Guardar.Focus();

                }
            }
        }

        private void Cbx_Cuatrimestre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cbx_Cuatrimestre.Text == "PRIMER CUATRIMESTRE")
            {
                Cbx_Cod_Asignatura1.Text = "ESP-101";
                Cbx_Cod_Asignatura2.Text = "HIST-101";
                Cbx_Cod_Asignatura3.Text = "INF-101";
                Cbx_Cod_Asignatura4.Text = "MAT-101";
                Cbx_Cod_Asignatura5.Text = " ";
                Cbx_Cod_Asignatura6.Text = " ";
                Cbx_Cod_Asignatura7.Text = " ";
                Buscar_Registro(Tbx_Matricula.Text, Cbx_Cod_Asignatura1.Text, Cbx_Cod_Asignatura2.Text, Cbx_Cod_Asignatura3.Text, Cbx_Cod_Asignatura4.Text, Cbx_Cod_Asignatura5.Text, Cbx_Cod_Asignatura6.Text, Cbx_Cod_Asignatura7.Text);
            }
            else
                if ((Cbx_Cuatrimestre.Text == "SEGUNDO CUATRIMESTRE"))
            {
                Cbx_Cod_Asignatura1.Text = "FIS-100";
                Cbx_Cod_Asignatura2.Text = "INF-102";
                Cbx_Cod_Asignatura3.Text = "ING-101";
                Cbx_Cod_Asignatura4.Text = "MAT-102";
                Cbx_Cod_Asignatura5.Text = " ";
                Cbx_Cod_Asignatura6.Text = " ";
                Cbx_Cod_Asignatura7.Text = " ";
                Buscar_Registro(Tbx_Matricula.Text, Cbx_Cod_Asignatura1.Text, Cbx_Cod_Asignatura2.Text, Cbx_Cod_Asignatura3.Text, Cbx_Cod_Asignatura4.Text, Cbx_Cod_Asignatura5.Text, Cbx_Cod_Asignatura6.Text, Cbx_Cod_Asignatura7.Text);
            }
            else
                if ((Cbx_Cuatrimestre.Text == "TERCER CUATRIMESTRE"))
            {
                Cbx_Cod_Asignatura1.Text = "COM-103";
                Cbx_Cod_Asignatura2.Text = "ELEC-101";
                Cbx_Cod_Asignatura3.Text = "ING-102";
                Cbx_Cod_Asignatura4.Text = "MAT-103";
                Cbx_Cod_Asignatura5.Text = "SOF-101";
                Cbx_Cod_Asignatura6.Text = " ";
                Cbx_Cod_Asignatura7.Text = " ";
                Buscar_Registro(Tbx_Matricula.Text, Cbx_Cod_Asignatura1.Text, Cbx_Cod_Asignatura2.Text, Cbx_Cod_Asignatura3.Text, Cbx_Cod_Asignatura4.Text, Cbx_Cod_Asignatura5.Text, Cbx_Cod_Asignatura6.Text, Cbx_Cod_Asignatura7.Text);
            }
            else
                if ((Cbx_Cuatrimestre.Text == "CUARTO CUATRIMESTRE"))
            {
                Cbx_Cod_Asignatura1.Text = "EST-101";
                Cbx_Cod_Asignatura2.Text = "INF-108";
                Cbx_Cod_Asignatura3.Text = "ING-103";
                Cbx_Cod_Asignatura4.Text = "MAT-105";
                Cbx_Cod_Asignatura5.Text = "SOF-102";
                Cbx_Cod_Asignatura6.Text = " ";
                Cbx_Cod_Asignatura7.Text = " ";
                Buscar_Registro(Tbx_Matricula.Text, Cbx_Cod_Asignatura1.Text, Cbx_Cod_Asignatura2.Text, Cbx_Cod_Asignatura3.Text, Cbx_Cod_Asignatura4.Text, Cbx_Cod_Asignatura5.Text, Cbx_Cod_Asignatura6.Text, Cbx_Cod_Asignatura7.Text);
            }
            else
                if ((Cbx_Cuatrimestre.Text == "QUINTO CUATRIMESTRE"))
            {
                Cbx_Cod_Asignatura1.Text = "CON-100";
                Cbx_Cod_Asignatura2.Text = "ING-104";
                Cbx_Cod_Asignatura3.Text = "SOF-103";
                Cbx_Cod_Asignatura4.Text = "SOF-104";
                Cbx_Cod_Asignatura5.Text = "SOF-105";
                Cbx_Cod_Asignatura6.Text = " ";
                Cbx_Cod_Asignatura7.Text = " ";
                Buscar_Registro(Tbx_Matricula.Text, Cbx_Cod_Asignatura1.Text, Cbx_Cod_Asignatura2.Text, Cbx_Cod_Asignatura3.Text, Cbx_Cod_Asignatura4.Text, Cbx_Cod_Asignatura5.Text, Cbx_Cod_Asignatura6.Text, Cbx_Cod_Asignatura7.Text);
            }
            else
                if ((Cbx_Cuatrimestre.Text == "SEXTO CUATRIMESTRE"))
            {
                Cbx_Cod_Asignatura1.Text = "ADM-100";
                Cbx_Cod_Asignatura2.Text = "EMP-100";
                Cbx_Cod_Asignatura3.Text = "SOF-106";
                Cbx_Cod_Asignatura4.Text = "SOF-107";
                Cbx_Cod_Asignatura5.Text = "SOF-108";
                Cbx_Cod_Asignatura6.Text = "SOF-109";
                Cbx_Cod_Asignatura7.Text = "SOF-300";
                Buscar_Registro(Tbx_Matricula.Text, Cbx_Cod_Asignatura1.Text, Cbx_Cod_Asignatura2.Text, Cbx_Cod_Asignatura3.Text, Cbx_Cod_Asignatura4.Text, Cbx_Cod_Asignatura5.Text, Cbx_Cod_Asignatura6.Text, Cbx_Cod_Asignatura7.Text);
            }
            else
                if ((Cbx_Cuatrimestre.Text == "SEPTIMO CUATRIMESTRE"))
            {
                Cbx_Cod_Asignatura1.Text = "ADM-102";
                Cbx_Cod_Asignatura2.Text = "ETI-100";
                Cbx_Cod_Asignatura3.Text = "SOF-111";
                Cbx_Cod_Asignatura4.Text = "SOF-112";
                Cbx_Cod_Asignatura5.Text = "SOF-113";
                Cbx_Cod_Asignatura6.Text = " ";
                Cbx_Cod_Asignatura7.Text = " ";
                Buscar_Registro(Tbx_Matricula.Text, Cbx_Cod_Asignatura1.Text, Cbx_Cod_Asignatura2.Text, Cbx_Cod_Asignatura3.Text, Cbx_Cod_Asignatura4.Text, Cbx_Cod_Asignatura5.Text, Cbx_Cod_Asignatura6.Text, Cbx_Cod_Asignatura7.Text);
            }
        }

        private void Frm_Calculo_Indice_Load(object sender, EventArgs e)
        {
            Cbx_Cod_Asignatura1.Text = "ESP-101";
            Cbx_Cod_Asignatura2.Text = "HIST-101";
            Cbx_Cod_Asignatura3.Text = "INF-101";
            Cbx_Cod_Asignatura4.Text = "MAT-101";
        }

        private void Btn_Guardar_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Thread.Sleep(100);

            Modificar_Registro();

            MessageBox.Show("Indice Académico Agregado");

            Calcular_IndiceTotal(Tbx_Matricula.Text);

            timer1.Stop();
            Btn_Guardar.BackColor = Color.LightGray;

            Lab_Para_Modificar.Visible = false;
            Lab_Primero.Visible = false;
            Lab_Calcular.Visible = false;

            Limpiar_Formulario();

            Cbx_Cuatrimestre.Enabled = true;
            Cbx_Cuatrimestre.Focus();
            Cbx_Cuatrimestre.SelectAll();

            Btn_Guardar.Enabled = false;

        }

        private bool Modificar_Registro()
        {
            //Solo cambia la Instrucción SQL y Se elimina el ID porque este no se cambiará

            //double base100 = Convert.ToDouble(Indice_Base100);
            //double base4 = Convert.ToDouble(Indice_Base4);
            string cuatrimestre = Cbx_Cuatrimestre.Text;

            //Conección
            OleDbConnection Conexion = new OleDbConnection();
            Conexion.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source = " + Application.StartupPath + @"\Notas_e_Indice_BBDD\Notas_e_Indice_BBDD.accdb;" + "Persist Security Info = false";

            //Instrucción SQL

            string CadenaSQL = "UPDATE Notas SET "; //Actualiza la tabla personal estableciendo nombre, direccion y edad, con los datos especificados.
            CadenaSQL = CadenaSQL + " Indice_Academico_100      = " + indice_Base_100 + ",";// la coma permite separar cada campo de su variable de asignación
            CadenaSQL = CadenaSQL + " Indice_Academico_4      = " + indice_Base_4 + " ";// la coma permite separar cada campo de su variable de asignación
                                                                                        //Hay que decir donde es que se va a actualizar.
                                                                                        //Actualizar lo anterior a este registro (osea, donde el id sea = a Cod.
            CadenaSQL = CadenaSQL + " WHERE Cuatrimestre = '" + cuatrimestre + "'";
            // si no se indica donde es que se va a cambiar, todos los datos de la tabla personal se cambiarian y no solo donde el id es igual a Cod.

            //Crear comando
            OleDbCommand Comando = Conexion.CreateCommand();  //Asignamos la cadena SQL al comando
            Comando.CommandText = CadenaSQL; //configuracion del comando

            //Ejecutar la consulta de acción
            Conexion.Open(); //Abrimos la conexion
            Comando.ExecuteNonQuery(); //Ejecutamos la consulta
            Conexion.Close(); //Cerramos la conexion
            return true;
        }

        private void Btn_Limpiar_Click(object sender, EventArgs e)
        {
            reproducirSonido("clean.wav", false);
            Thread.Sleep(100);

            Limpiar_Formulario();
        }

        private void Limpiar_Formulario()
        {
            Tbx_Nombre.Clear();
            Tbx_Apellido.Clear();

            Tbx_CR1.Clear();
            Tbx_CR2.Clear();
            Tbx_CR3.Clear();
            Tbx_CR4.Clear();
            Tbx_CR5.Clear();
            Tbx_CR6.Clear();
            Tbx_CR7.Clear();
            Tbx_CR_Total.Clear();

            Tbx_Nota1.Clear();
            Tbx_Nota2.Clear();
            Tbx_Nota3.Clear();
            Tbx_Nota4.Clear();
            Tbx_Nota5.Clear();
            Tbx_Nota6.Clear();
            Tbx_Nota7.Clear();

            Tbx_CR_Nota1.Clear();
            Tbx_CR_Nota2.Clear();
            Tbx_CR_Nota3.Clear();
            Tbx_CR_Nota4.Clear();
            Tbx_CR_Nota5.Clear();
            Tbx_CR_Nota6.Clear();
            Tbx_CR_Nota7.Clear();
            Tbx_CR_Nota_Total.Clear();

            Tbx_Base100.Clear();
            Tbx_Base4.Clear();

            Tbx_Matricula.Focus();
            Tbx_Matricula.SelectAll();
        }

        private void Bloquear_Controles() // Bloquea controles
        {
            Tbx_Matricula.Enabled = true;
            Tbx_Nombre.Enabled = false;
            Tbx_Apellido.Enabled = false;

            Tbx_CR1.Enabled = false;
            Tbx_CR2.Enabled = false;
            Tbx_CR3.Enabled = false;
            Tbx_CR4.Enabled = false;
            Tbx_CR5.Enabled = false;
            Tbx_CR6.Enabled = false;
            Tbx_CR7.Enabled = false;

            Tbx_Nota1.Enabled = false;
            Tbx_Nota2.Enabled = false;
            Tbx_Nota3.Enabled = false;
            Tbx_Nota4.Enabled = false;
            Tbx_Nota5.Enabled = false;
            Tbx_Nota6.Enabled = false;
            Tbx_Nota7.Enabled = false;

            Cbx_Cuatrimestre.Enabled = false;

            Btn_Calcular.Enabled = false;
            Btn_Guardar.Enabled = false;
            Btn_Buscar.Enabled = false;
            Btn_Limpiar.Enabled = false;
        }

        private void Desbloquear_Controles() // Desbloquea controles
        {
            Tbx_Matricula.Enabled = false;
            Tbx_Nombre.Enabled = true;
            Tbx_Apellido.Enabled = true;

            Tbx_CR1.Enabled = true;
            Tbx_CR2.Enabled = true;
            Tbx_CR3.Enabled = true;
            Tbx_CR4.Enabled = true;
            Tbx_CR5.Enabled = true;
            Tbx_CR6.Enabled = true;
            Tbx_CR7.Enabled = true;

            Tbx_Nota1.Enabled = true;
            Tbx_Nota2.Enabled = true;
            Tbx_Nota3.Enabled = true;
            Tbx_Nota4.Enabled = true;
            Tbx_Nota5.Enabled = true;
            Tbx_Nota6.Enabled = true;
            Tbx_Nota7.Enabled = true;

            Cbx_Cuatrimestre.Enabled = true;

            Btn_Guardar.Enabled = true;
            Btn_Buscar.Enabled = true;
            Btn_Limpiar.Enabled = true;
            Btn_Calcular.Enabled = true;

            Tbx_Nombre.Focus(); // Pone el foco en el textbox nombre         
        }



        private void Tbx_CR1_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_CR1.Text != "")
            {
                Btn_Calcular.Enabled = true;
                Tbx_CR1.Focus();
                Tbx_CR1.SelectAll();

                double CR1 = Convert.ToDouble(Tbx_CR1.Text);

                if (CR1 > 4)
                {
                    MessageBox.Show("Favor Introducir valores del 1-4");
                    Tbx_CR1.Clear();
                }
                else
                {
                    if (Tbx_CR1.Text.Length == 1 && Tbx_CR1.Text != "0")
                    {
                        Tbx_Nota1.Focus();
                    }
                }
            }
            else
            {
                Tbx_CR1.Text = "0";
                Tbx_CR1.Focus();
                Tbx_CR1.SelectAll();
            }

        }

        private void Tbx_CR2_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_CR2.Text != "")
            {
                Btn_Calcular.Enabled = true;
                Tbx_CR2.Focus();
                Tbx_CR2.SelectAll();

                double CR2 = Convert.ToDouble(Tbx_CR2.Text);

                if (CR2 > 4)
                {
                    MessageBox.Show("Favor Introducir valores del 1-4");
                    Tbx_CR2.Clear();
                }
                else
                {
                    if (Tbx_CR2.Text.Length == 1 && Tbx_CR2.Text != "0")
                    {
                        Tbx_Nota2.Focus();
                    }
                }
            }
            else
            {
                Tbx_CR2.Text = "0";
                Tbx_CR2.Focus();
                Tbx_CR2.SelectAll();
            }
        }

        private void Tbx_CR3_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_CR3.Text != "")
            {
                Btn_Calcular.Enabled = true;

                double CR3 = Convert.ToDouble(Tbx_CR3.Text);

                if (CR3 > 4)
                {
                    MessageBox.Show("Favor Introducir valores del 1-4");
                    Tbx_CR3.Clear();
                }
                else
                {
                    if (Tbx_CR3.Text.Length == 1 && Tbx_CR3.Text != "0")
                    {
                        Tbx_Nota3.Focus();
                    }
                }
            }
            else
            {
                Tbx_CR3.Focus();
                Tbx_CR3.Text = "0";
                Tbx_CR3.SelectAll();
            }
        }

        private void Tbx_CR4_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_CR4.Text != "")
            {
                Btn_Calcular.Enabled = true;

                double CR4 = Convert.ToDouble(Tbx_CR4.Text);

                if (CR4 > 4)
                {
                    MessageBox.Show("Favor Introducir valores del 1-4");
                    Tbx_CR4.Clear();
                }
                else
                {
                    if (Tbx_CR4.Text.Length == 1 && Tbx_CR4.Text != "0")
                    {
                        Tbx_Nota4.Focus();
                    }
                }
            }
            else
            {
                Tbx_CR4.Focus();
                Tbx_CR4.Text = "0";
                Tbx_CR4.SelectAll();
            }
        }

        private void Tbx_CR5_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_CR5.Text != "")
            {
                Btn_Calcular.Enabled = true;

                double CR5 = Convert.ToDouble(Tbx_CR5.Text);

                if (CR5 > 4)
                {
                    MessageBox.Show("Favor Introducir valores del 1-4");
                    Tbx_CR5.Clear();
                }
                else
                {
                    if (Tbx_CR5.Text.Length == 1 && Tbx_CR5.Text != "0")
                    {
                        Tbx_Nota5.Focus();
                    }
                }
            }
            else
            {
                Tbx_CR5.Focus();
                Tbx_CR5.Text = "0";
                Tbx_CR5.SelectAll();
            }
        }

        private void Tbx_CR6_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_CR6.Text != "")
            {
                Btn_Calcular.Enabled = true;

                double CR6 = Convert.ToDouble(Tbx_CR6.Text);

                if (CR6 > 4)
                {
                    MessageBox.Show("Favor Introducir valores del 1-4");
                    Tbx_Nota6.Clear();
                }
                else
                {
                    if (Tbx_CR6.Text.Length == 1 && Tbx_CR6.Text != "0")
                    {
                        Tbx_Nota6.Focus();
                    }
                }
            }
            else
            {
                Tbx_CR6.Focus();
                Tbx_CR6.Text = "0";
                Tbx_CR6.SelectAll();
            }
        }

        private void Tbx_CR7_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_CR7.Text != "")
            {
                Btn_Calcular.Enabled = true;

                double CR7 = Convert.ToDouble(Tbx_CR7.Text);

                if (CR7 > 4)
                {
                    MessageBox.Show("Favor Introducir valores del 1-4");
                    Tbx_CR7.Clear();
                }
                else
                {
                    if (Tbx_CR7.Text.Length == 1 && Tbx_CR7.Text != "0")
                    {
                        Tbx_Nota7.Focus();
                    }
                }
            }
            else
            {
                Tbx_CR7.Focus();
                Tbx_CR7.Text = "0";
                Tbx_CR7.SelectAll();
            }
        }

        private void Tbx_Nota1_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_Nota1.Text != "")
            {
                Btn_Calcular.Enabled = true;

                double Nota1 = Convert.ToDouble(Tbx_Nota1.Text);

                if (Nota1 > 100)
                {
                    MessageBox.Show("Favor Introducir valores del 0-100");
                    Tbx_Nota1.Clear();
                }
                else
                {
                    if (Tbx_Nota1.Text.Length == 4 || Tbx_Nota1.Text == "100")
                    {
                        Tbx_CR2.Focus();
                    }
                }
            }
            else
            {
                Tbx_Nota1.Focus();
                Tbx_Nota1.Text = "0";
                Tbx_Nota1.SelectAll();
            }
        }

        private void Tbx_Nota2_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_Nota2.Text != "")
            {
                Btn_Calcular.Enabled = true;

                double Nota2 = Convert.ToDouble(Tbx_Nota2.Text);

                if (Nota2 > 100)
                {
                    MessageBox.Show("Favor Introducir valores del 0-100");
                    Tbx_Nota2.Clear();
                }
                else
                {
                    if (Tbx_Nota2.Text.Length == 4 || Tbx_Nota2.Text == "100")
                    {
                        Tbx_CR3.Focus();
                    }
                }
            }
            else
            {
                Tbx_Nota2.Focus();
                Tbx_Nota2.Text = "0";
                Tbx_Nota2.SelectAll();
            }
        }

        private void Tbx_Nota3_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_Nota3.Text != "")
            {
                Btn_Calcular.Enabled = true;

                double Nota3 = Convert.ToDouble(Tbx_Nota3.Text);

                if (Nota3 > 100)
                {
                    MessageBox.Show("Favor Introducir valores del 0-100");
                    Tbx_Nota3.Clear();
                }
                else
                {
                    if (Tbx_Nota3.Text.Length == 4 || Tbx_Nota3.Text == "100")
                    {
                        Tbx_CR4.Focus();
                    }
                }
            }
            else
            {
                Tbx_Nota3.Focus();
                Tbx_Nota3.Text = "0";
                Tbx_Nota3.SelectAll();
            }
        }

        private void Tbx_Nota4_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_Nota4.Text != "")
            {
                Btn_Calcular.Enabled = true;

                double Nota4 = Convert.ToDouble(Tbx_Nota4.Text);

                if (Nota4 > 100)
                {
                    MessageBox.Show("Favor Introducir valores del 0-100");
                    Tbx_Nota4.Clear();
                }
                else
                {
                    if (Tbx_Nota4.Text.Length == 4 || Tbx_Nota4.Text == "100")
                    {
                        Tbx_CR5.Focus();
                    }
                }
            }
            else
            {
                Tbx_Nota4.Focus();
                Tbx_Nota4.Text = "0";
                Tbx_Nota4.SelectAll();
            }
        }

        private void Tbx_Nota5_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_Nota5.Text != "")
            {
                Btn_Calcular.Enabled = true;

                double Nota5 = Convert.ToDouble(Tbx_Nota5.Text);

                if (Nota5 > 100)
                {
                    MessageBox.Show("Favor Introducir valores del 0-100");
                    Tbx_Nota5.Clear();
                }
                else
                {
                    if (Tbx_Nota5.Text.Length == 4 || Tbx_Nota5.Text == "100")
                    {
                        Tbx_CR6.Focus();
                    }
                }
            }
            else
            {
                Tbx_Nota5.Focus();
                Tbx_Nota5.Text = "0";
                Tbx_Nota5.SelectAll();
            }
        }

        private void Tbx_Nota6_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_Nota6.Text != "")
            {
                Btn_Calcular.Enabled = true;

                double Nota6 = Convert.ToDouble(Tbx_Nota6.Text);

                if (Nota6 > 100)
                {
                    MessageBox.Show("Favor Introducir valores del 0-100");
                    Tbx_Nota6.Clear();
                }
                else
                {
                    if (Tbx_Nota6.Text.Length == 4 || Tbx_Nota6.Text == "100")
                    {
                        Tbx_CR7.Focus();
                    }
                }
            }
            else
            {
                Tbx_Nota6.Focus();
                Tbx_Nota6.Text = "0";
                Tbx_Nota6.SelectAll();
            }
        }

        private void Tbx_Nota7_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_Nota7.Text != "")
            {
                Btn_Calcular.Enabled = true;

                double Nota7 = Convert.ToDouble(Tbx_Nota7.Text);

                if (Nota7 > 100)
                {
                    MessageBox.Show("Favor Introducir valores del 0-100");
                    Tbx_Nota7.Clear();
                }
                else
                {
                    if (Tbx_Nota7.Text.Length == 4 || Tbx_Nota7.Text == "100")
                    {
                        Btn_Calcular.Focus();
                    }
                }
            }
            else
            {
                Tbx_Nota7.Focus();
                Tbx_Nota7.Text = "0";
                Tbx_Nota7.SelectAll();
            }
        }




        private void Tbx_CR1_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNum_y_Enter(e);
        }

        private void Tbx_CR2_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNum_y_Enter(e);
        }

        private void Tbx_CR3_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNum_y_Enter(e);
        }

        private void Tbx_CR4_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNum_y_Enter(e);
        }

        private void Tbx_CR5_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNum_y_Enter(e);
        }

        private void Tbx_CR6_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNum_y_Enter(e);
        }

        private void Tbx_CR7_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNum_y_Enter(e);
        }

        private void Tbx_Nota1_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumFloat_y_Enter(e);
        }

        private void Tbx_Nota2_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumFloat_y_Enter(e);
        }

        private void Tbx_Nota3_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumFloat_y_Enter(e);
        }

        private void Tbx_Nota4_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumFloat_y_Enter(e);
        }

        private void Tbx_Nota5_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumFloat_y_Enter(e);
        }

        private void Tbx_Nota6_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumFloat_y_Enter(e);
        }

        private void Tbx_Nota7_KeyPress(object sender, KeyPressEventArgs e)
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

        private void SoloNum_y_Enter(KeyPressEventArgs e)
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
                e.Handled = Validadores.ValidadorNumerico(e.KeyChar);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (activar_Mensaje == true)
            {
                if (Btn_Calcular.BackColor == Color.LightGray)
                {
                    Btn_Calcular.BackColor = Color.White;
                }
                else
                    if (Lab_Calcular.Visible == true)
                {
                    Btn_Calcular.BackColor = Color.LightGray;
                }
                else
                     if (Lab_Para_Modificar.Visible == false)
                {
                    Lab_Para_Modificar.Visible = true;
                }
                else
                    if (Lab_Para_Modificar.Visible == true && Lab_Primero.Visible == false)
                {
                    Lab_Primero.Visible = true;
                }
                else
                    if (Lab_Para_Modificar.Visible == true && Lab_Primero.Visible == true)
                {
                    Lab_Calcular.Visible = true;
                }

                if (Btn_Guardar.Enabled == true)
                {
                    Btn_Calcular.BackColor = Color.White;
                    Btn_Calcular.ForeColor = Color.Black;

                    if (Btn_Guardar.BackColor == Color.White)
                    {
                        Btn_Guardar.BackColor = Color.LightGray;
                    }
                    else
                    {
                        Btn_Guardar.BackColor = Color.White;
                    }
                }
            }
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