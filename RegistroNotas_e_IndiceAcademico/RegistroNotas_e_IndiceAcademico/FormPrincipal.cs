using System;
using System.Windows.Forms;
using System.Media;
using System.Threading;

namespace RegistroNotas_e_IndiceAcademico
{
    public partial class Frm_Principal : Form, EditParent
    {
        Frm_RegistroEstudiantes Frm_reg_est = new Frm_RegistroEstudiantes();
        Frm_Asignaturas Frm_Asi = new Frm_Asignaturas();
        Registro_Notas Frm_Not = new Registro_Notas();
        Frm_Calculo_Indice Frm_Ind = new Frm_Calculo_Indice();
        Frm_CambiarPass Frm_Cam_pass = new Frm_CambiarPass();


        bool isShown_Reg_Est = false;
        bool isShown_Asi = false;
        bool isShown_Not = false;
        bool isShown_Ind = false;
        bool isShown_Cam_Pass = false;

        SoundPlayer sonido;

        public Frm_Principal()
        {
            InitializeComponent();
            reproducirSonido("tech_Ambiente.wav", true);
        }


        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Application.Exit();
        }


        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Thread.Sleep(100);
            reproducirSonido("tech_Ambiente_4.wav", false);

            if (isShown_Cam_Pass == false) // para saber si ya esta abierto el Form
            {
                isShown_Cam_Pass = true;
                isShown_Reg_Est = false;
                isShown_Not = false;
                isShown_Asi = false;
                isShown_Ind = false;
                Frm_Cam_pass.Show(this);
            }

            Cambiar_Imagenes(true);

            Frm_Asi.Hide();
            Frm_Not.Hide();
            Frm_Ind.Hide();
            Frm_reg_est.Hide();
        }


        private void registroEstudiantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Thread.Sleep(100);
            reproducirSonido("tech_Ambiente_4.wav", false);

            if (isShown_Reg_Est == false) // para saber si ya esta abierto el Form
            {
                isShown_Reg_Est = true;
                isShown_Not = false;
                isShown_Asi = false;
                isShown_Ind = false;
                isShown_Cam_Pass = false;
                Frm_reg_est.Show(this);
                
            }
            
            Cambiar_Imagenes(true);

            Frm_Asi.Hide();
            Frm_Not.Hide();
            Frm_Ind.Hide();
            Frm_Cam_pass.Hide();

        }
        private void asignaturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Thread.Sleep(100);
            reproducirSonido("tech_Ambiente_4.wav", false);

            if (isShown_Asi == false) // para saber si ya esta abierto el Form
            {
                isShown_Asi = true;
                isShown_Reg_Est = false;
                isShown_Not = false;
                isShown_Ind = false;
                isShown_Cam_Pass = false;
                Frm_Asi.Show(this);
            }
            
            Cambiar_Imagenes(true);

            Frm_Ind.Hide();
            Frm_Not.Hide();
            Frm_reg_est.Hide();
            Frm_Cam_pass.Hide();
        }
        private void registroDeNotasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Thread.Sleep(100);
            reproducirSonido("tech_Ambiente_4.wav", false);

            if (isShown_Not == false) // para saber si ya esta abierto el Form
            {
                isShown_Not = true;
                isShown_Asi = false;
                isShown_Reg_Est = false;
                isShown_Ind = false;
                isShown_Cam_Pass = false;
                Frm_Not.Show(this);
            }
            
            Cambiar_Imagenes(true);

            Frm_Asi.Hide();
            Frm_Ind.Hide();
            Frm_reg_est.Hide();
            Frm_Cam_pass.Hide();
        }
        private void cálculoDelÍndiceAcadémicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reproducirSonido("click.wav", false);
            Thread.Sleep(100);
            reproducirSonido("tech_Ambiente_4.wav", false);

            if (isShown_Ind == false) // para saber si ya esta abierto el Form
            {
                isShown_Ind = true;
                isShown_Not = false;
                isShown_Asi = false;
                isShown_Reg_Est = false;
                isShown_Cam_Pass = false;
                Frm_Ind.Show(this);
            }
            
            Cambiar_Imagenes(true);

            Frm_Asi.Hide();
            Frm_Not.Hide();
            Frm_reg_est.Hide();
            Frm_Cam_pass.Hide();
        }

        void EditParent.Cambiar_Imagenes(bool SioNo)
        {
            Cambiar_Imagenes(SioNo);
        }

        void Cambiar_Imagenes (bool si)
        {
            if (si)
            {
                Pbx_Left.Image = Properties.Resources.giphy;
                Pbx_right.Image = Properties.Resources.giphy;
            }
            else
            {
                Pbx_Left.Image = Properties.Resources.Cuadro_negro;
                Pbx_right.Image = Properties.Resources.Cuadro_negro_LDBM;
            }
        }

        void EditParent.isHidden(string Formulario)
        {
            if (Formulario == "Reg_Est")
            {
                isShown_Reg_Est = false;
            }
            else
                if (Formulario == "Not")
            {
                isShown_Not = false;
            }
            else
                if (Formulario == "Asi")
            {
                isShown_Asi = false;
            }
            else
                if (Formulario == "Ind")
            {
                isShown_Ind = false;
            }
            else
                if (Formulario == "Cam_Pass")
            {
                isShown_Cam_Pass = false;
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
                sonido = new SoundPlayer(Application.StartupPath + @"\son\"+nombreArchivo);
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
