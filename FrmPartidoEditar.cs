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
    public partial class FrmPartidoEditar : Form
    {
        private int? idPartido;

        public FrmPartidoEditar(int? idPartido = null)
        {
            InitializeComponent();
            this.idPartido = idPartido;
            if (idPartido.HasValue)
            {
                this.Text = "Editar Partido";
                CargarDatos();
            }
            else
            {
                this.Text = "Nuevo Partido";
            }
        }

        private void CargarDatos()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT Nombre, Siglas FROM Partidos WHERE IdPartido = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", idPartido.Value);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtSiglas.Text = reader["Siglas"].ToString();
                        }
                    }
                }
            }
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtSiglas.Text))
            {
                MessageBox.Show("Ingrese nombre y siglas.", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query;
                if (idPartido.HasValue)
                    query = "UPDATE Partidos SET Nombre = @nombre, Siglas = @siglas WHERE IdPartido = @id";
                else
                    query = "INSERT INTO Partidos (Nombre, Siglas, Estado) VALUES (@nombre, @siglas, 1)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                    cmd.Parameters.AddWithValue("@siglas", txtSiglas.Text.Trim());
                    if (idPartido.HasValue)
                        cmd.Parameters.AddWithValue("@id", idPartido.Value);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Parito guardado correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
