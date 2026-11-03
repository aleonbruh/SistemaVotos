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
    public partial class FrmUsuarios : Form
    {
        public FrmUsuarios()
        {
            InitializeComponent();
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT IdUsuario, NombreCompleto, Usuario, Rol, CASE WHEN Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS Estado FROM Usuarios";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvUsuarios.DataSource = dt;
                dgvUsuarios.Columns["IdUsuario"].Visible = false;
            }
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow != null)
            {
                string estado = dgvUsuarios.CurrentRow.Cells["Estado"].Value.ToString();
                btnDesactivar.Text = (estado == "Activo") ? "Desactivar" : "Reactivar";
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["IdUsuario"].Value);
            using (FrmUsuarioEditar frm = new FrmUsuarioEditar(id))
            {
                frm.ShowDialog();
                CargarUsuarios();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (FrmUsuarioEditar frm = new FrmUsuarioEditar())
            {
                frm.ShowDialog();
                CargarUsuarios();
            }
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["IdUsuario"].Value);
            string nombre = dgvUsuarios.CurrentRow.Cells["NombreCompleto"].Value.ToString();
            string estado = dgvUsuarios.CurrentRow.Cells["Estado"].Value.ToString();

            int nuevoEstado = (estado == "Activo") ? 0 : 1;
            string accion = (estado == "Activo") ? "desactivar" : "reactivar";
            string accionNotificacion = (estado == "Activo") ? "desactivado" : "reactivado";

            if (MessageBox.Show($"¿Deseas {accion} este usuario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    string query = "UPDATE Usuarios SET Estado = @nuevoEstado WHERE IdUsuario = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@nuevoEstado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                Notificaciones.Guardar("Usuario", $"El usuario {SesionActual.Usuario} ha {accionNotificacion} al usuario: {nombre}.");
                MessageBox.Show($"Usuario {accionNotificacion} correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios();
            }
        }
    }
}
