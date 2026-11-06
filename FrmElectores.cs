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
    public partial class Electores : Form
    {
        public Electores()
        {
            InitializeComponent();
            CargarElectores();
        }

        private void CargarElectores()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT e.IdElector, e.NombreCompleto, e.DUI, e.FechaNacimiento, e.Direccion, d.Nombre AS Distrito, c.Nombre AS Centro, m.NumeroMesa, CASE WHEN e.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS Estado FROM Electores e INNER JOIN Distritos d ON e.IdDistrito = d.IdDistrito INNER JOIN CentrosVotacion c ON e.IdCentro = c.IdCentro INNER JOIN MesasVotacion m ON e.IdMesa = m.IdMesa\r\n";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvElectores.DataSource = dt;
                dgvElectores.Columns["IdElector"].Visible = false;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (FrmElectorEditar frm = new FrmElectorEditar())
            {
                frm.ShowDialog();
                CargarElectores();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvElectores.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un elector para editar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int id = Convert.ToInt32(dgvElectores.CurrentRow.Cells["IdElector"].Value);
            using (FrmElectorEditar frm = new FrmElectorEditar(id))
            {
                frm.ShowDialog();
                CargarElectores();
            }
        }

        private void dgvElectores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvElectores.CurrentRow != null)
            {
                string estado = dgvElectores.CurrentRow.Cells["Estado"].Value.ToString();
                btnEliminar.Text = (estado == "Activo") ? "Eliminar" : "Restaurar";
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvElectores.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvElectores.CurrentRow.Cells["IdElector"].Value);
            string nombre = dgvElectores.CurrentRow.Cells["Nombre Completo"].Value.ToString();
            string estado = dgvElectores.CurrentRow.Cells["Estado"].Value.ToString();

            int nuevoEstado = (estado == "Activo") ? 0 : 1;
            string accion = (estado == "Activo") ? "eliminar" : "restaurar";
            string accionNotificacion = (estado == "Activo") ? "eliminado" : "restaurado";

            if (MessageBox.Show($"¿Está seguro de {accion} el elector '{nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    string query = "UPDATE Electores SET Estado = @nuevoEstado WHERE IdElector = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@nuevoEstado", nuevoEstado);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                Notificaciones.Guardar("Elector", $"El usuario {SesionActual.Usuario} ha {accionNotificacion} el elector {nombre}.");
                CargarElectores();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarElectores();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT e.IdElector, e.NombreCompleto AS 'Nombre Completo', e.DUI, e.FechaNacimiento, e.Direccion, c.Nombre AS Centro, m.NumeroMesa, CASE WHEN e.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS Estado FROM Electores e INNER JOIN CentrosVotacion c ON e.IdCentro = c.IdCentro INNER JOIN MesasVotacion m ON e.IdMesa = m.IdMesa WHERE e.NombreCompleto LIKE @busqueda OR e.DUI LIKE @busqueda";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@busqueda", "%" + txtBusqueda.Text + "%");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvElectores.DataSource = dt;
                }
            }
        }
    }
}
