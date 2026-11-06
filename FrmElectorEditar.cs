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
    public partial class FrmElectorEditar : Form
    {
        private int? idElector;
        private int? idCentroAsignado;
        private int? idMesaAsignada;
        public FrmElectorEditar(int? id = null)
        {
            InitializeComponent();
            this.idElector = id;
            CargarDistritos();

            if (idElector.HasValue)
            {
                this.Text = "Editar Elector";
                CargarDatos();
            }
            else
            {
                this.Text = "Nuevo Elector";
                lblMensajeCentro.Text = "Se elegira automaticamente según el distrito seleccionado.";
                lblMensajeMesa.Text = "Se elegira automaticamente según el distrito seleccionado.";
            }
        }

        private void CargarDistritos()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT IdDistrito, Nombre FROM Distritos";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbDistrito.DisplayMember = "Nombre";
                cmbDistrito.ValueMember = "IdDistrito";
                cmbDistrito.DataSource = dt;
            }
        }

        // Asigar centro y mesa de votación según el distrito seleccionado
        private void cmbDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDistrito.SelectedValue == null) return;

            int idDistrito = Convert.ToInt32(cmbDistrito.SelectedValue);

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                con.Open();

                SqlCommand cmdCentro = new SqlCommand("SELECT TOP 1 IdCentro, Nombre FROM CentrosVotacion WHERE IdDistrito = @idDistrito AND Estado = 1 ORDER BY NEWID()", con);
                cmdCentro.Parameters.AddWithValue("@idDistrito", idDistrito);
                using (SqlDataReader readerCentro = cmdCentro.ExecuteReader())
                {
                    if (readerCentro.Read())
                    {
                        idCentroAsignado = Convert.ToInt32(readerCentro["IdCentro"]);
                        txtCentro.Text = readerCentro["Nombre"].ToString();
                    }
                    else
                    {
                        idCentroAsignado = null;
                        lblMensajeCentro.Text = "No hay centros de votación activos en este distrito.";
                    }
                }

                if (idCentroAsignado.HasValue)
                {
                    SqlCommand cmdMesa = new SqlCommand("SELECT TOP 1 IdMesa, NumeroMesa FROM MesasVotacion WHERE IdCentro = @idCentro AND Estado = 1 ORDER BY NEWID()", con);
                    cmdMesa.Parameters.AddWithValue("@idCentro", idCentroAsignado.Value);
                    using (SqlDataReader readerMesa = cmdMesa.ExecuteReader())
                    {
                        if (readerMesa.Read())
                        {
                            idMesaAsignada = Convert.ToInt32(readerMesa["IdMesa"]);
                            txtMesa.Text = "Mesa " + readerMesa["NumeroMesa"].ToString();
                        }
                        else
                        {
                            idMesaAsignada = null;
                            lblMensajeMesa.Text = "No hay mesas de votación activas en este centro.";
                        }
                    }
                }
                else
                {
                    idMesaAsignada = null;
                    lblMensajeMesa.Text = "No se puede asignar una mesa sin un centro de votación.";
                }
            }
        }

        private void CargarDatos()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT NombreCompleto, DUI, FechaNacimiento, Direccion, IdDistrito, IdCentro, IdMesa FROM Electores WHERE IdElector = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", idElector.Value);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtNombre.Text = reader["NombreCompleto"].ToString();
                            txtDUI.Text = reader["DUI"].ToString();
                            dtpFechaNacimiento.Value = Convert.ToDateTime(reader["FechaNacimiento"]);
                            txtDireccion.Text = reader["Direccion"].ToString();
                            cmbDistrito.SelectedValue = Convert.ToInt32(reader["IdDistrito"]);

                            int idCentro = Convert.ToInt32(reader["IdCentro"]);
                            int idMesa = Convert.ToInt32(reader["IdMesa"]);
                            idCentroAsignado = idCentro;
                            idMesaAsignada = idMesa;
                            CentroYMesa(idCentro, idMesa);
                        }
                    }
                }
            }
        }

        private void CentroYMesa(int idCentro, int idMesa)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"SELECT c.Nombre AS Centro, m.NumeroMesa
                                 FROM CentrosVotacion c
                                 INNER JOIN MesasVotacion m ON c.IdCentro = m.IdCentro
                                 WHERE c.IdCentro = @idCentro AND m.IdMesa = @idMesa";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idCentro", idCentro);
                    cmd.Parameters.AddWithValue("@idMesa", idMesa);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtCentro.Text = reader["Centro"].ToString();
                            txtMesa.Text = "Mesa " + reader["NumeroMesa"].ToString();
                        }
                    }
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtDUI.Text) || string.IsNullOrWhiteSpace(txtDireccion.Text) || cmbDistrito.SelectedValue == null)
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!idElector.HasValue && (!idCentroAsignado.HasValue || !idMesaAsignada.HasValue))
            {
                MessageBox.Show("No se puede asignar un centro o mesa de votación. Por favor, seleccione un distrito con centros y mesas activas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                con.Open();

                //Validacion
                string queryValidacion = "SELECT COUNT(*) FROM Electores WHERE LOWER(NombreCompleto) = LOWER(@nombre) AND LOWER(DUI) = LOWER(@dui) AND IdElector <> ISNULL(@id, -1)";
                using (SqlCommand cmdValidacion = new SqlCommand(queryValidacion, con))
                {
                    cmdValidacion.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmdValidacion.Parameters.AddWithValue("@dui", txtDUI.Text);
                    cmdValidacion.Parameters.AddWithValue("@id", idElector.HasValue ? (object)idElector.Value : DBNull.Value);
                    int count = Convert.ToInt32(cmdValidacion.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("Ya existe un elector con el mismo nombre y DUI.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string query;
                if (idElector.HasValue)
                {
                    query = "UPDATE Electores SET NombreCompleto = @nombre, DUI = @dui, FechaNacimiento = @fechaNacimiento, Direccion = @direccion, IdDistrito = @idDistrito, IdCentro = @idCentro, IdMesa = @idMesa WHERE IdElector = @id";
                }
                else
                {
                    query = "INSERT INTO Electores (NombreCompleto, DUI, FechaNacimiento, Direccion, IdDistrito, IdCentro, IdMesa, Estado) VALUES (@nombre, @dui, @fechaNacimiento, @direccion, @idDistrito, @idCentro, @idMesa, 1)";
                }
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@dui", txtDUI.Text);
                    cmd.Parameters.AddWithValue("@fechaNacimiento", dtpFechaNacimiento.Value);
                    cmd.Parameters.AddWithValue("@direccion", txtDireccion.Text);
                    cmd.Parameters.AddWithValue("@idDistrito", cmbDistrito.SelectedValue);
                    cmd.Parameters.AddWithValue("@idCentro", idCentroAsignado);
                    cmd.Parameters.AddWithValue("@idMesa", idMesaAsignada);
                    if (idElector.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@id", idElector.Value);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Elector guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Notificaciones.Guardar("Elector", $"El usuario {SesionActual.Usuario} ha {(idElector.HasValue ? "editado" : "creado")} el elector {txtNombre.Text}.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}