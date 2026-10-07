using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVotos
{
    public partial class FrmCentros : Form
    {
        public FrmCentros()
        {
            InitializeComponent();
        }

        private void FrmCentros_Load(object sender, EventArgs e)
        {
            CargarCentros();
        }

        private void CargarCentros()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT IdCentro, Nombre, Direccion, CASE WHEN Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS Estado FROM CentrosVotacion";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvCentros.DataSource = dt;
                dgvCentros.Columns["IdCentro"].Visible = false;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (FrmCentroEditar frm = new FrmCentroEditar())
            {
                frm.ShowDialog();
                CargarCentros();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCentros.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvCentros.CurrentRow.Cells["IdCentro"].Value);
            using (FrmCentroEditar frm = new FrmCentroEditar(id))
            {
                frm.ShowDialog();
                CargarCentros();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCentros.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvCentros.CurrentRow.Cells["IdCentro"].Value);
            string nombre = dgvCentros.CurrentRow.Cells["Nombre"].Value.ToString();
            if (MessageBox.Show($"¿Está seguro de eliminar el centro de votación '{nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    string query = "UPDATE CentrosVotacion SET Estado = 0 WHERE IdCentro = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                CargarCentros();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarCentros();
        }
    }
}
