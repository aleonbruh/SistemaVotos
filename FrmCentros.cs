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
                string query = "SELECT c.IdCentro, c.Nombre, c.Direccion, CASE WHEN d.IdDistrito IS NOT NULL THEN d.Nombre ELSE 'No asignado' END AS Distrito, CASE WHEN c.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS Estado FROM CentrosVotacion c LEFT JOIN Distritos d ON c.IdDistrito = d.IdDistrito";
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

        private void dgvCentros_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCentros.CurrentRow != null)
            {
                string estado = dgvCentros.CurrentRow.Cells["Estado"].Value.ToString();
                btnEliminar.Text = (estado == "Activo") ? "Eliminar" : "Restaurar";
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCentros.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvCentros.CurrentRow.Cells["IdCentro"].Value);
            string nombre = dgvCentros.CurrentRow.Cells["Nombre"].Value.ToString();
            string estado = dgvCentros.CurrentRow.Cells["Estado"].Value.ToString();

            int nuevoEstado = (estado == "Activo") ? 0 : 1;
            string accion = (estado == "Activo") ? "eliminar" : "restaurar";
            string accionNotificacion = (estado == "Activo") ? "eliminado" : "restaurado";

            if (MessageBox.Show($"¿Está seguro de {accion} el centro de votación '{nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    string query = "UPDATE CentrosVotacion SET Estado = @nuevoEstado WHERE IdCentro = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@nuevoEstado", nuevoEstado);
                        cmd.Parameters.AddWithValue("@id", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                Notificaciones.Guardar("Centro de Votación", $"El usuario {SesionActual.Usuario} ha {accionNotificacion} el centro de votación {nombre}.");
                CargarCentros();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarCentros();
        }
    }
}
