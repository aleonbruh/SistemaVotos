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
        public FrmElectorEditar(int? id = null)
        {
            InitializeComponent();
            this.idElector = id;
            CargarCentros();

            if (idElector.HasValue)
            {
                this.Text = "Editar Elector";
                CargarDatos();
            }
            else
            {
                this.Text = "Nuevo Elector";
            }
        }

        private void CargarCentros()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT IdCentro, Nombre FROM CentrosVotacion WHERE Estado = 1";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbCentro.DisplayMember = "Nombre";
                cmbCentro.ValueMember = "IdCentro";
                cmbCentro.DataSource = dt;
            }
        }

        private void CargarMesas(int idCentro)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT IdMesa, NumeroMesa FROM MesasVotacion WHERE IdCentro = @idCentro AND Estado = 1";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@idCentro", idCentro);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbMesa.DisplayMember = "NumeroMesa";
                cmbMesa.ValueMember = "IdMesa";
                cmbMesa.DataSource = dt;
            }
        }

        private void cmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCentro.SelectedValue != null && int.TryParse(cmbCentro.SelectedValue.ToString(), out int idCentro))
            {
                CargarMesas(idCentro);
            }
        }

        private void CargarDatos()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT NombreCompleto, DUI, FechaNacimiento, Direccion, IdCentro, IdMesa FROM Electores WHERE IdElector = @id";
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
                            cmbCentro.SelectedValue = reader["IdCentro"];
                            cmbMesa.SelectedValue = reader["IdMesa"];
                        }
                    }
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtDUI.Text) || string.IsNullOrWhiteSpace(txtDireccion.Text) || cmbCentro.SelectedValue == null || cmbMesa.SelectedValue == null || dtpFechaNacimiento.Value == DateTime.MinValue)
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query;
                if (idElector.HasValue)
                {
                    query = "UPDATE Electores SET NombreCompleto = @nombre, DUI = @dui, FechaNacimiento = @fechaNacimiento, Direccion = @direccion, IdCentro = @idCentro, IdMesa = @idMesa WHERE IdElector = @id";
                }
                else
                {
                    query = "INSERT INTO Electores (NombreCompleto, DUI, FechaNacimiento, Direccion, IdCentro, IdMesa, Estado) VALUES (@nombre, @dui, @fechaNacimiento, @direccion, @idCentro, @idMesa, 1)";
                }
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@dui", txtDUI.Text);
                    cmd.Parameters.AddWithValue("@fechaNacimiento", dtpFechaNacimiento.Value);
                    cmd.Parameters.AddWithValue("@direccion", txtDireccion.Text);
                    cmd.Parameters.AddWithValue("@idCentro", cmbCentro.SelectedValue);
                    cmd.Parameters.AddWithValue("@idMesa", cmbMesa.SelectedValue);
                    if (idElector.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@id", idElector.Value);
                    }
                    con.Open();
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
