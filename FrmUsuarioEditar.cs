using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVotos
{
    public partial class FrmUsuarioEditar : Form
    {
        private int? idUsuario;
        private bool cambiandoContrasena = false;
        private bool contrasenaVisible = false;

        public FrmUsuarioEditar(int? id = null)
        {
            InitializeComponent();
            this.idUsuario = id;
            
            CargarRoles();

            if (idUsuario.HasValue)
            {
                this.Text = "Editar Usuario";
                CargarDatos();
                txtContrasena.Enabled = false;
                txtContrasena.UseSystemPasswordChar = true;
                lblAccionContrasena.Text = "Cambiar Contraseña";
                lblAccionContrasena.Click += LblAccionContraseña_Click;
            }
            else
            {
                this.Text = "Nuevo Usuario";
                txtContrasena.Enabled = true;
                lblAccionContrasena.Visible = false;
            }
        }

        private void CargarRoles()
        {
            cmbRol.Items.Clear();
            cmbRol.Items.Add("Administrador");
            cmbRol.Items.Add("Operador");
            cmbRol.SelectedIndex = 0;
        }

        private void CargarDatos()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                con.Open();
                string query = "SELECT NombreCompleto, Usuario, Rol FROM Usuarios WHERE IdUsuario = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", idUsuario.Value);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtNombre.Text = reader["NombreCompleto"].ToString();
                    txtUsuario.Text = reader["Usuario"].ToString();
                    string rolActual = reader["Rol"].ToString();
                    cmbRol.SelectedItem = rolActual;
                    txtContrasena.Text = string.Empty;
                }
                con.Close();
            }
        }

        private void LblAccionContraseña_Click(object sender, EventArgs e)
        {
            if (!cambiandoContrasena)
            {
                cambiandoContrasena = true;
                txtContrasena.Enabled = true;
                txtContrasena.Text = string.Empty;
                txtContrasena.UseSystemPasswordChar = true;
                txtContrasena.Focus();
                lblContrasena.Text = "Ingrese una nueva contraseña:";
                lblAccionContrasena.Text = "Mostrar";
            }
            else
            {
                contrasenaVisible = !contrasenaVisible;
                txtContrasena.UseSystemPasswordChar = !contrasenaVisible;
                lblAccionContrasena.Text = contrasenaVisible ? "Ocultar" : "Mostrar";
            }
        }

        private string EncriptarSHA256(string texto)
{
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(texto));
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        private void GuardarUsuario()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtUsuario.Text) || cmbRol.SelectedItem == null)
            {
                MessageBox.Show("Complete todos los campos requeridos", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                con.Open();
                string query;
                SqlCommand cmd;
                bool esNuevoUsuario = !idUsuario.HasValue;

                if (idUsuario.HasValue)
                {
                    // Editar usuario existente
                    if (cambiandoContrasena && !string.IsNullOrWhiteSpace(txtContrasena.Text))
                    {
                        string contrasenaEncriptada = EncriptarSHA256(txtContrasena.Text);
                        query = "UPDATE Usuarios SET NombreCompleto = @nombre, Usuario = @usuario, Rol = @rol, Contrasena = @contrasena WHERE IdUsuario = @id";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@contrasena", contrasenaEncriptada);
                    }
                    else
                    {
                        query = "UPDATE Usuarios SET NombreCompleto = @nombre, Usuario = @usuario, Rol = @rol WHERE IdUsuario = @id";
                        cmd = new SqlCommand(query, con);
                    }
                    cmd.Parameters.AddWithValue("@id", idUsuario.Value);
                }
                else
                {
                    // Crear nuevo usuario
                    if (string.IsNullOrWhiteSpace(txtContrasena.Text))
                    {
                        MessageBox.Show("Ingrese una contraseña", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string contrasenaEncriptada = EncriptarSHA256(txtContrasena.Text);
                    query = "INSERT INTO Usuarios (NombreCompleto, Usuario, Contrasena, Rol, Estado) VALUES (@nombre, @usuario, @contrasena, @rol, @estado)";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@contrasena", contrasenaEncriptada);
                    cmd.Parameters.AddWithValue("@estado", 1);
                }

                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@rol", cmbRol.SelectedItem.ToString());

                cmd.ExecuteNonQuery();
                con.Close();
                
                // Notificar cambios
                if (esNuevoUsuario)
                {
                    Notificaciones.Guardar("Usuario", $"El usuario {SesionActual.Usuario} ha creado un nuevo usuario: {txtUsuario.Text}.");
                }
                else
                {
                    Notificaciones.Guardar("Usuario", $"El usuario {SesionActual.Usuario} ha modificado el usuario: {txtUsuario.Text}.");
                }

                MessageBox.Show("Usuario guardado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GuardarUsuario();
        }
    }
}