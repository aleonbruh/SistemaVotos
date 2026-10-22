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
    public partial class FrmNotificaciones : Form
    {
        public FrmNotificaciones()
        {
            InitializeComponent();
            CargarAlertas();
            CargarNotificaciones();
        }

        private void CargarAlertas()
        {
            rtxAlertas.Clear();
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                con.Open();

                // Electores que no han registrado su voto
                SqlCommand cmd = new SqlCommand("SELECT NombreCompleto FROM Electores WHERE Estado = 1 AND (VotoAlcalde = 0 OR VotoDiputados = 0)", con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        agregarLinea(rtxAlertas, "[Atencion] ", "El elector " + reader["NombreCompleto"] + " no ha registrado su voto.?\n");
                    }
                }

                // Votos nulos registrados
                cmd = new SqlCommand("SELECT COUNT(*) FROM Votos WHERE EstadoVoto = 0", con);
                int nulos = (int)cmd.ExecuteScalar();
                if (nulos > 0)
                {
                    agregarLinea(rtxAlertas, "[Atencion] ", "Se han registrado " + nulos + " votos nulos.\n");
                }
                if (nulos == 0)
                {
                    agregarLinea(rtxAlertas, "[Atencion] ", "No se han registrado votos nulos.?\n");
                }
            }
        }

        private void CargarNotificaciones()
        {
            rtxNotificaciones.Clear();
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT n.Tipo, n.Mensaje, n.FechaHora, u.Usuario FROM Notificaciones n INNER JOIN Usuarios u ON n.IdUsuario = u.IdUsuario ORDER BY n.FechaHora DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string tipo = reader["Tipo"].ToString();
                        string mensaje = reader["Mensaje"].ToString();
                        DateTime fechaHora = Convert.ToDateTime(reader["FechaHora"]);
                        agregarLinea(rtxNotificaciones, $"[{tipo}] ", $"{fechaHora.ToString("HH:mm")} - {mensaje}");
                    }
                }
            }
        }

        private void agregarLinea(RichTextBox rtx, string bold, string normal)
        {
            rtx.SelectionFont = new Font(rtx.Font, FontStyle.Bold);
            rtx.AppendText(bold);
            rtx.SelectionFont = new Font(rtx.Font, FontStyle.Regular);
            rtx.AppendText(normal + Environment.NewLine);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarAlertas();
            CargarNotificaciones();
        }
    }
}
