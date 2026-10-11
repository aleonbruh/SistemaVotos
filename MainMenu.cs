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
            }
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
        private void electoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Electores frm = new Electores();
            frm.MdiParent = this;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void partidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPartidos frm = new FrmPartidos();
            frm.MdiParent = this;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void candidatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCandidatos frm = new FrmCandidatos();
            frm.MdiParent = this;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void centrosDeVotacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCentros frm = new FrmCentros();
            frm.MdiParent = this;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void mesasDeVotacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMesas frm = new FrmMesas();
            frm.MdiParent = this;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void registrarVotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVoto frm = new FrmVoto();
            frm.MdiParent = this;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }
    }
}
