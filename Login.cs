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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MessageBox.Show("Ingrese usuario y contrasena.", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            string hashIngresado = ObtenerHashSHA256(txtContrasena.Text);

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT IdUsuario, NombreCompleto, Usuario, Rol FROM Usuarios WHERE Usuario = @usuario AND Contrasena = @hash AND Estado = 1";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text.Trim());
                    cmd.Parameters.AddWithValue("@hash", hashIngresado);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            SesionActual.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                            SesionActual.NombreCompleto = reader["NombreCompleto"].ToString();
                            SesionActual.Usuario = reader["Usuario"].ToString();
                            SesionActual.Rol = reader["Rol"].ToString();

                            MainMenu menu = new MainMenu();
                            menu.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contrasena incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private string ObtenerHashSHA256(string hash)
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hash));
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
