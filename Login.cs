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
using static ClosedXML.Excel.XLPredefinedFormat;

namespace SistemaVotos
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            txtUsuario.Focus();
            txtContrasena.UseSystemPasswordChar = true;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MessageBox.Show("Ingrese usuario y contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hashIngresado = ObtenerHashSHA256(txtContrasena.Text);

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT IdUsuario, NombreCompleto, Usuario, Rol, Estado FROM Usuarios WHERE Usuario = @usuario AND Contrasena = @hash";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text.Trim());
                    cmd.Parameters.AddWithValue("@hash", hashIngresado);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int estado = Convert.ToInt32(reader["Estado"]);
                            if (estado == 0)
                            {
                                MessageBox.Show("Su cuenta está inactiva. Contacte con administración.", "Cuenta Inactiva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Notificaciones.Guardar("Inicio de Sesion", $"El usuario inactivo {txtUsuario.Text.Trim()} ha intentado iniciar sesión.");
                            }
                            else
                            {
                                SesionActual.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                                SesionActual.NombreCompleto = reader["NombreCompleto"].ToString();
                                SesionActual.Usuario = reader["Usuario"].ToString();
                                SesionActual.Rol = reader["Rol"].ToString();

                                Notificaciones.Guardar("Inicio de Sesion", $"El usuario {SesionActual.Usuario} ha iniciado sesión.");
                                MainMenu menu = new MainMenu();
                                menu.Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txtContrasena_TextChanged(object sender, EventArgs e)
        {
            btnMostrarOcultar.Visible = true;
        }

        private void btnMostrarOcultar_Click(object sender, EventArgs e)
        {
            if (txtContrasena.UseSystemPasswordChar)
            {
                txtContrasena.UseSystemPasswordChar = false;
                btnMostrarOcultar.Text = "Ocultar Contraseña";
            }
            else
            {
                txtContrasena.UseSystemPasswordChar = true;
                btnMostrarOcultar.Text = "Mostrar Contraseña";
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                txtContrasena.Focus();
                e.Handled = true;
            }
        }

        private void txtContrasena_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                btnIngresar.PerformClick();
                e.Handled = true;
            }
        }
    }
}