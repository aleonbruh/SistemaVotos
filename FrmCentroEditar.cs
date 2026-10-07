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
    public partial class FrmCentroEditar : Form
    {
        int? idCentro;

        public FrmCentroEditar(int? idCentro = null)
        {
            InitializeComponent();
            this.idCentro = idCentro;
            if (idCentro.HasValue)
            {
                this.Text = "Editar Centro de Votación";
                CargarDatos();
            }
            else
            {
                this.Text = "Nuevo Centro de Votación";
            }
        }

        private void CargarDatos()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT Nombre, Direccion FROM CentrosVotacion WHERE IdCentro = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", idCentro.Value);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtDireccion.Text = reader["Direccion"].ToString();
                        }
                    }
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtDireccion.Text))
            {
                MessageBox.Show("Ingrese nombre y dirección.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query;
                if (idCentro.HasValue)
                    query = "UPDATE CentrosVotacion SET Nombre = @nombre, Direccion = @direccion WHERE IdCentro = @id";
                else
                    query = "INSERT INTO CentrosVotacion (Nombre, Direccion, Estado) VALUES (@nombre, @direccion, 1)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@direccion", txtDireccion.Text);
                    if (idCentro.HasValue)
                        cmd.Parameters.AddWithValue("@id", idCentro.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            this.Close();
            MessageBox.Show("Centro de votación guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
