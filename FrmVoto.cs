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
    public partial class FrmVoto : Form
    {
        private int? idElectorSeleccionado;
        private bool alcaldeVotado;
        private bool diputadoVotado;
        public FrmVoto()
        {
            InitializeComponent();
            CargarCandidatos();
        }

        private void CargarCandidatos()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"SELECT c.IdCandidato, c.NombreCompleto + ' (' + p.Siglas + ')' AS NombreCompleto, c.TipoEleccion FROM Candidatos c INNER JOIN Partidos p ON c.IdPartido = p.IdPartido WHERE c.Estado = 1";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                DataTable dtAlcaldes = dt.Clone();
                DataTable dtDiputados = dt.Clone();

                foreach (DataRow row in dt.Rows)
                {
                    if (row["TipoEleccion"].ToString() == "Alcalde")
                    {
                        dtAlcaldes.ImportRow(row);
                    }
                    else if (row["TipoEleccion"].ToString() == "Diputados")
                    {
                        dtDiputados.ImportRow(row);
                    }
                }

                cmbCandidatosAlcaldes.DataSource = dtAlcaldes;
                cmbCandidatosAlcaldes.DisplayMember = "NombreCompleto";
                cmbCandidatosAlcaldes.ValueMember = "IdCandidato";

                cmbCandidatosDiputados.DataSource = dtDiputados;
                cmbCandidatosDiputados.DisplayMember = "NombreCompleto";
                cmbCandidatosDiputados.ValueMember = "IdCandidato";
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBusqueda.Text.Trim()))
            {
                MessageBox.Show("Por favor, ingrese un DUI válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"SELECT e.IdElector, e.NombreCompleto, c.Nombre AS Centro, m.NumeroMesa FROM Electores e INNER JOIN CentrosVotacion c ON e.IdCentro = c.IdCentro INNER JOIN MesasVotacion m ON e.IdMesa = m.IdMesa WHERE e.DUI = @DUI AND e.Estado = 1";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@DUI", txtBusqueda.Text.Trim());
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idElectorSeleccionado = Convert.ToInt32(reader["IdElector"]);
                            txtNombre.Text = reader["NombreCompleto"].ToString();
                            txtCentro.Text = reader["Centro"].ToString();
                            txtMesa.Text = "Mesa " + reader["NumeroMesa"].ToString();
                            VerificarVotos();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró un elector con el DUI proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            LimpiarElector();
                        }
                    }
                }
            }
        }

        private void VerificarVotos()
        {
            grpAlcaldes.Enabled = true;
            grpDiputados.Enabled = true;
            lblEstadoAlcaldes.Text = "";
            lblEstadoDiputados.Text = "";

            if (!idElectorSeleccionado.HasValue) return;

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT TipoEleccion FROM Votos WHERE idElector = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", idElectorSeleccionado.Value);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        alcaldeVotado = false;
                        diputadoVotado = false;
                        while (reader.Read())
                        {
                            if (reader["TipoEleccion"].ToString() == "Alcalde") alcaldeVotado = true;
                            else if (reader["TipoEleccion"].ToString() == "Diputados") diputadoVotado = true;
                        }

                        if (alcaldeVotado)
                        {
                            grpAlcaldes.Enabled = false;
                            lblEstadoAlcaldes.Text = "Ya ha votado por Alcalde";
                        }
                        if (diputadoVotado)
                        {
                            grpDiputados.Enabled = false;
                            lblEstadoDiputados.Text = "Ya ha votado por Diputados";
                        }
                    }
                }
            }
        }

        private void InsertarVoto(SqlConnection con, SqlTransaction tran, string tipo, int idCandidato, bool valido)
        {
            string query = "INSERT INTO Votos (idElector, idCandidato, TipoEleccion, FechaVoto, EstadoVoto, IdUsuario) VALUES (@elector, @candidato, @tipo, @fecha, @estado, @usuario)";
            using (SqlCommand cmd = new SqlCommand(query, con, tran))
            {
                cmd.Parameters.AddWithValue("@elector", idElectorSeleccionado.Value);
                cmd.Parameters.AddWithValue("@candidato", idCandidato);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                cmd.Parameters.AddWithValue("@estado", valido ? "Valido" : "Nulo");
                cmd.Parameters.AddWithValue("@usuario", SesionActual.IdUsuario);
                cmd.ExecuteNonQuery();
            }
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!idElectorSeleccionado.HasValue)
            {
                MessageBox.Show("Por favor, busque un elector antes de votar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool hayAlcalde = !alcaldeVotado && cmbCandidatosAlcaldes.SelectedValue != null;
            bool hayDiputado = !diputadoVotado && cmbCandidatosDiputados.SelectedValue != null;

            if (!hayAlcalde && !hayDiputado)
            {
                MessageBox.Show("No hay votos para registrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    if (hayAlcalde)
                    {
                        InsertarVoto(con, tran, "Alcalde", Convert.ToInt32(cmbCandidatosAlcaldes.SelectedValue), rbValidoAlcaldes.Checked);
                    }
                    if (hayDiputado)
                    {
                        InsertarVoto(con, tran, "Diputados", Convert.ToInt32(cmbCandidatosDiputados.SelectedValue), rbValidoDiputados.Checked);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Error al registrar el voto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            MessageBox.Show("Voto registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarTodo();
        }
        private void LimpiarElector()
        {
            idElectorSeleccionado = null;
            txtNombre.Clear();
            txtCentro.Clear();
            txtMesa.Clear();
            grpAlcaldes.Enabled = true;
            grpDiputados.Enabled = true;
            lblEstadoAlcaldes.Text = "";
            lblEstadoDiputados.Text = "";
        }

        private void LimpiarTodo()
        {
            LimpiarElector();
            txtBusqueda.Clear();
            cmbCandidatosAlcaldes.SelectedIndex = -1;
            cmbCandidatosDiputados.SelectedIndex = -1;
            rbValidoAlcaldes.Checked = true;
            rbValidoDiputados.Checked = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
