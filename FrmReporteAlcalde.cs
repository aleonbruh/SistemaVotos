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
    public partial class FrmReporteAlcalde : Form
    {
        public FrmReporteAlcalde()
        {
            InitializeComponent();
            CargarCentros();
            CargarResultados();
        }

        private void CargarCentros()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT IdCentro, Nombre FROM CentrosVotacion WHERE Estado = 1", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbFiltroCentro.DisplayMember = "Nombre";
                cmbFiltroCentro.ValueMember = "IdCentro";
                cmbFiltroCentro.DataSource = dt;
            }
            cmbFiltroCentro.SelectedIndex = -1;
        }

        private void CargarMesas(int idCentro)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT IdMesa, NumeroMesa FROM MesasVotacion WHERE IdCentro = @IdCentro AND Estado = 1";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@IdCentro", idCentro);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbFiltroMesa.DisplayMember = "NumeroMesa";
                cmbFiltroMesa.ValueMember = "IdMesa";
                cmbFiltroMesa.DataSource = dt;
            }
        }

        private void cmbFiltroCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFiltroCentro.SelectedValue != null && cmbFiltroCentro.SelectedValue is int)
            {
                int idCentro = (int)cmbFiltroCentro.SelectedValue;
                CargarMesas(idCentro);
                cmbFiltroMesa.SelectedIndex = -1;
            }
        }

        private void CargarResultados()
        {
            string query = @"SELECT ca.NombreCompleto, p.Siglas, COUNT(v.IdVoto) AS TotalVotos
                            FROM Candidatos ca
                            INNER JOIN Partidos p ON ca.IdPartido = p.IdPartido
                            LEFT JOIN Votos v ON v.IdCandidato = ca.IdCandidato
                                AND v.TipoEleccion = 'Alcalde' AND v.EstadoVoto = 1
                                AND (@Centro IS NULL OR v.IdMesa IN (SELECT IdMesa FROM MesasVotacion WHERE IdCentro = @Centro))
                                AND (@Mesa IS NULL OR v.IdMesa = @Mesa)
                            WHERE ca.TipoEleccion = 'Alcalde'
                            GROUP BY ca.NombreCompleto, p.Siglas
                            ORDER BY TotalVotos DESC";
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@Centro", cmbFiltroCentro.SelectedValue ?? (object)DBNull.Value);
                da.SelectCommand.Parameters.AddWithValue("@Mesa", cmbFiltroMesa.SelectedValue ?? (object)DBNull.Value);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvResultados.DataSource = dt;
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarResultados();
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            cmbFiltroCentro.SelectedIndex = -1;
            cmbFiltroMesa.DataSource = null;
            CargarResultados();
        }
    }
}