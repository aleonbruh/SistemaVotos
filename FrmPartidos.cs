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
    public partial class FrmPartidos : Form
    {
        public FrmPartidos()
        {
            InitializeComponent();
        }

        private void FrmPartidos_Load(object sender, EventArgs e)
        {
            CargarPartidos();
        }

        private void CargarPartidos()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT IdPartido, Nombre, Siglas, CASE WHEN Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS Estado FROM Partidos";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPartidos.DataSource = dt;
                dgvPartidos.Columns["IdPartido"].Visible = false;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (FrmPartidoEditar frm = new FrmPartidoEditar())
            {
                frm.ShowDialog();
                CargarPartidos();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvPartidos.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvPartidos.CurrentRow.Cells["IdPartido"].Value);
            using (FrmPartidoEditar frm = new FrmPartidoEditar(id))
            {
                frm.ShowDialog();
                CargarPartidos();
            }
        }

        private void dgvPartidos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPartidos.CurrentRow != null)
            {
                string estado = dgvPartidos.CurrentRow.Cells["Estado"].Value.ToString();
                btnEliminar.Text = (estado == "Activo") ? "Eliminar" : "Restaurar";
            }

        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPartidos.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvPartidos.CurrentRow.Cells["IdPartido"].Value);
            string nombre = dgvPartidos.CurrentRow.Cells["Nombre"].Value.ToString();
            string estado = dgvPartidos.CurrentRow.Cells["Estado"].Value.ToString();

            int nuevoEstado = (estado == "Activo") ? 0 : 1;
            string accion = (estado == "Activo") ? "eliminar" : "restaurar";
            string accionNotificacion = (estado == "Activo") ? "eliminado" : "restaurado";

            if (MessageBox.Show($"¿Está seguro de {accion} el partido '{nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    string query = "UPDATE Partidos SET Estado = @nuevoEstado WHERE IdPartido = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@nuevoEstado", nuevoEstado);
                        cmd.Parameters.AddWithValue("@id", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                Notificaciones.Guardar("Partido", $"El usuario {SesionActual.Usuario} ha {accionNotificacion} el partido {nombre}.");
                CargarPartidos();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarPartidos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT IdPartido, Nombre, Siglas, CASE WHEN Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS EstadoTexto FROM Partidos WHERE Nombre LIKE @filtro OR Siglas LIKE @filtro";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@filtro", "%" + txtBuscar.Text.Trim() + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPartidos.DataSource = dt;
                dgvPartidos.Columns["IdPartido"].Visible = false;
            }
        }
    }
}
