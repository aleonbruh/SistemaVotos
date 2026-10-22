using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVotos
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            if (SesionActual.Rol == "Operador")
            {
                partidosToolStripMenuItem.Visible = false;
                candidatosToolStripMenuItem.Visible = false;
                reportesToolStripMenuItem.Visible = false;
                notificacionesToolStripMenuItem.Visible = false;
            }
        }

        //Metodo para mostrar los formularios hijos dentro del formulario principal
        private void ShowChildForm(Form form)
        {
            form.MdiParent = this;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void electoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Electores());
        }

        private void partidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new FrmPartidos());
        }

        private void candidatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new FrmCandidatos());
        }

        private void centrosDeVotacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new FrmCentros());
        }

        private void mesasDeVotacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new FrmMesas());
        }

        private void registrarVotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new FrmVoto());
        }

        private void resultadosDeAlcaldeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new FrmReporteAlcalde());
        }

        private void resultadosDeDiputadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new FrmReporteDiputados());
        }

        private void votosValidosYNulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new FrmReporteVotos());
        }

        private void notificacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new FrmNotificaciones());
        }
    }
}
