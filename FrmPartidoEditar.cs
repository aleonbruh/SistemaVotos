using System;
using System.Data.SqlClient;
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
                con.Open();

                //Validacion
                string queryValidacion = "SELECT COUNT(*) FROM Partidos WHERE LOWER(Nombre) = LOWER(@nombre) AND LOWER(Siglas) = LOWER(@siglas) AND IdPartido <> ISNULL(@id, -1)";
                using (SqlCommand cmdValidacion = new SqlCommand(queryValidacion, con))
                {
                    cmdValidacion.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmdValidacion.Parameters.AddWithValue("@siglas", txtSiglas.Text);
                    cmdValidacion.Parameters.AddWithValue("@id", idPartido.HasValue ? (object)idPartido.Value : DBNull.Value);
                    int count = Convert.ToInt32(cmdValidacion.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("Ya existe un partido con el mismo nombre y siglas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

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

                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show($"Partido {txtSiglas.Text.Trim()} guardado correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Notificaciones.Guardar("Partido", $"El usuario {SesionActual.Usuario} ha {(idPartido.HasValue ? "editado" : "creado")} el partido {txtSiglas.Text.Trim()}.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}