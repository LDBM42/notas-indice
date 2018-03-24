using System;
using System.Windows.Forms;
using System.Media;
using System.Threading;

namespace RegistroNotas_e_IndiceAcademico
{
    public partial class Frm_Asignaturas : Form
    {
        SoundPlayer sonido;

        public Frm_Asignaturas()
        {
            InitializeComponent();
        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Thread.Sleep(100);

            EditParent EP = this.Owner as EditParent;
            EP.Cambiar_Imagenes(false);
            EP.isHidden("Asi");
            reproducirSonido("tech_Ambiente.wav", true);
            Hide();
        }

        private void Frm_Asignaturas_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'notas_e_Indice_BBDDDataSet1.Asignaturas' Puede moverla o quitarla según sea necesario.
            this.asignaturasTableAdapter.Fill(this.notas_e_Indice_BBDDDataSet1.Asignaturas);

        }


        private void Cbx_Cuatrimestre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cbx_Cuatrimestre.Text == "PRIMER CUATRIMESTRE")
            {
                primerCuatrimestreToolStripButton.PerformClick();
            }
            else
                if ((Cbx_Cuatrimestre.Text == "SEGUNDO CUATRIMESTRE"))
            {
                segundoCuatrimestre1ToolStripButton.PerformClick();
            }
            else
                if ((Cbx_Cuatrimestre.Text == "TERCER CUATRIMESTRE"))
            {
                tercerCuatrimestre1ToolStripButton.PerformClick();
            }
            else
                if ((Cbx_Cuatrimestre.Text == "CUARTO CUATRIMESTRE"))
            {
                cuartoCuatrimestreToolStripButton.PerformClick();
            }
            else
                if ((Cbx_Cuatrimestre.Text == "QUINTO CUATRIMESTRE"))
            {
                quintoCuatrimestreToolStripButton.PerformClick();
            }
            else
                if ((Cbx_Cuatrimestre.Text == "SEXTO CUATRIMESTRE"))
            {
                sextoCuatrimestreToolStripButton.PerformClick();
            }
            else
                if ((Cbx_Cuatrimestre.Text == "SEPTIMO CUATRIMESTRE"))
            {
                septimoCuatrimestreToolStripButton.PerformClick();
            }
            else
            {
                todosLosCuatrimestres1ToolStripButton.PerformClick();
            }
        }

        private void primerCuatrimestreToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.asignaturasTableAdapter.PrimerCuatrimestre(this.notas_e_Indice_BBDDDataSet1.Asignaturas);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }



        private void cuartoCuatrimestreToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.asignaturasTableAdapter.CuartoCuatrimestre(this.notas_e_Indice_BBDDDataSet1.Asignaturas);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void quintoCuatrimestreToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.asignaturasTableAdapter.QuintoCuatrimestre(this.notas_e_Indice_BBDDDataSet1.Asignaturas);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void sextoCuatrimestreToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.asignaturasTableAdapter.SextoCuatrimestre(this.notas_e_Indice_BBDDDataSet1.Asignaturas);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void septimoCuatrimestreToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.asignaturasTableAdapter.SeptimoCuatrimestre(this.notas_e_Indice_BBDDDataSet1.Asignaturas);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void segundoCuatrimestre1ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.asignaturasTableAdapter.SegundoCuatrimestre1(this.notas_e_Indice_BBDDDataSet1.Asignaturas);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void tercerCuatrimestre1ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.asignaturasTableAdapter.TercerCuatrimestre1(this.notas_e_Indice_BBDDDataSet1.Asignaturas);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void todosLosCuatrimestres1ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.asignaturasTableAdapter.TodosLosCuatrimestres1(this.notas_e_Indice_BBDDDataSet1.Asignaturas);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
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
