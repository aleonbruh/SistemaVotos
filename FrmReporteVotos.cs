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
    public partial class FrmReporteVotos : Form
    {
        public FrmReporteVotos()
        {
            InitializeComponent();
            CargarResultados(null);
        }

        private void CargarResultados(int? estado)
        {
            string query = @"SELECT v.IdVoto, COALESCE(ca.NombreCompleto, 'Voto Nulo') AS Candidato, 
                COALESCE(p.Siglas, '-') AS Partido, v.TipoEleccion, v.FechaVoto, v.EstadoVoto, 
                c.Nombre AS Centro, m.NumeroMesa, u.Usuario 
            FROM Votos v 
            LEFT JOIN Candidatos ca ON v.IdCandidato = ca.IdCandidato 
            LEFT JOIN Partidos p ON ca.IdPartido = p.IdPartido 
            INNER JOIN MesasVotacion m ON v.IdMesa = m.IdMesa 
            INNER JOIN CentrosVotacion c ON m.IdCentro = c.IdCentro 
            INNER JOIN Usuarios u ON v.IdUsuario = u.IdUsuario 
            WHERE @Estado IS NULL OR v.EstadoVoto = @Estado
            ORDER BY v.FechaVoto DESC";

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Estado", (object)estado ?? DBNull.Value);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvVotos.DataSource = dt;
                    dgvVotos.Columns["EstadoVoto"].Visible = false;
                    if (dt.Rows.Count > 0)
                    {
                        int votosValidos = dt.AsEnumerable().Count(row => Convert.ToInt32(row["EstadoVoto"]) == 1);
                        int votosNulos = dt.AsEnumerable().Count(row => Convert.ToInt32(row["EstadoVoto"]) == 0);
                        txtValidos.Text = votosValidos.ToString();
                        txtNulos.Text = votosNulos.ToString();
                    }
                    else
                    {
                        txtValidos.Text = "0";
                        txtNulos.Text = "0";
                    }
                }
            }
        }

        private void btnValidos_Click(object sender, EventArgs e)
        {
            CargarResultados(1);
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            CargarResultados(null);
        }

        private void btnNulos_Click(object sender, EventArgs e)
        {
            CargarResultados(0);
        }
    }
}
