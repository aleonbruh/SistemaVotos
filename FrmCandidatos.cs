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
    public partial class FrmCandidatos : Form
    {
        public FrmCandidatos()
        {
            InitializeComponent();
            CargarCandidatos();
        }

        private void CargarCandidatos()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT c.IdCandidato, c.NombreCompleto, CASE WHEN p.Siglas IS NOT NULL THEN p.Siglas ELSE 'No asignado' END AS Partido, c.TipoEleccion, CASE WHEN c.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS Estado FROM Candidatos c INNER JOIN Partidos p ON c.IdPartido = p.IdPartido";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvCandidatos.DataSource = dt;
                dgvCandidatos.Columns["IdCandidato"].Visible = false;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (FrmCandidatoEditar frm = new FrmCandidatoEditar())
            {
                frm.ShowDialog();
                CargarCandidatos();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCandidatos.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvCandidatos.CurrentRow.Cells["IdCandidato"].Value);
            using (FrmCandidatoEditar frm = new FrmCandidatoEditar(id))
            {
                frm.ShowDialog();
                CargarCandidatos();
            }
        }

        private void dgvCandidatos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCandidatos.CurrentRow != null)
            {
                string estado = dgvCandidatos.CurrentRow.Cells["Estado"].Value.ToString();
                btnEliminar.Text = (estado == "Activo") ? "Eliminar" : "Restaurar";
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCandidatos.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvCandidatos.CurrentRow.Cells["IdCandidato"].Value);
            string nombre = dgvCandidatos.CurrentRow.Cells["NombreCompleto"].Value.ToString();
            string estado = dgvCandidatos.CurrentRow.Cells["Estado"].Value.ToString();

            int nuevoEstado = (estado == "Activo") ? 0 : 1;
            string accion = (estado == "Activo") ? "desactivar" : "activar";
            string accionNotificacion = (estado == "Activo") ? "desactivado" : "activado";

            if (MessageBox.Show($"¿Está seguro de que desea {accion} al candidato '{nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    string query = "UPDATE Candidatos SET Estado = @nuevoEstado WHERE IdCandidato = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@nuevoEstado", nuevoEstado);
                        cmd.Parameters.AddWithValue("@id", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                Notificaciones.Guardar("Candidato", $"El usuario {SesionActual.Usuario} ha {accionNotificacion} el candidato {nombre}.");
                CargarCandidatos();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarCandidatos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT c.IdCandidato, c.NombreCompleto, CASE WHEN p.Siglas IS NOT NULL THEN p.Siglas ELSE 'No asignado' END AS Partido, c.TipoEleccion, CASE WHEN c.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS Estado FROM Candidatos c INNER JOIN Partidos p ON c.IdPartido = p.IdPartido WHERE c.NombreCompleto LIKE @busqueda OR p.Siglas LIKE @busqueda OR c.TipoEleccion LIKE @busqueda";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@busqueda", "%" + txtBusqueda.Text + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvCandidatos.DataSource = dt;
                dgvCandidatos.Columns["IdCandidato"].Visible = false;
            }
        }
    }
}
