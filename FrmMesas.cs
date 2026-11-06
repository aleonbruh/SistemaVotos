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
    public partial class FrmMesas : Form
    {
        public FrmMesas()
        {
            InitializeComponent();
        }

        private void FrmMesas_Load(object sender, EventArgs e)
        {
            CargarMesas();
        }

        private void CargarMesas()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT m.IdMesa, m.NumeroMesa, CASE WHEN c.IdCentro IS NOT NULL THEN c.Nombre ELSE 'No asignado' END AS Centro, CASE WHEN m.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS Estado FROM MesasVotacion m LEFT JOIN CentrosVotacion c ON m.IdCentro = c.IdCentro";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvMesas.DataSource = dt;
                dgvMesas.Columns["IdMesa"].Visible = false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT m.IdMesa, m.NumeroMesa, CASE WHEN c.IdCentro IS NOT NULL THEN c.Nombre ELSE 'No asignado' END AS Centro, CASE WHEN m.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS Estado FROM MesasVotacion m INNER JOIN CentrosVotacion c ON m.IdCentro = c.IdCentro WHERE c.Nombre LIKE @busqueda";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@busqueda", "%" + txtBusqueda.Text + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvMesas.DataSource = dt;
                dgvMesas.Columns["IdMesa"].Visible = false;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (FrmMesaNueva frm = new FrmMesaNueva())
            {
                frm.ShowDialog();
                CargarMesas();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMesas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una mesa para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int id = Convert.ToInt32(dgvMesas.CurrentRow.Cells["IdMesa"].Value);
            int numMesa = Convert.ToInt32(dgvMesas.CurrentRow.Cells["NumeroMesa"].Value);
            if (MessageBox.Show($"¿Está seguro de eliminar la mesa número '{numMesa}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    string query = "UPDATE MesasVotacion SET Estado = 0 WHERE IdMesa = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                CargarMesas();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarMesas();
        }
    }
}
