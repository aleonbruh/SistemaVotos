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
        private int idMesaElector;
        public FrmVoto()
        {
            InitializeComponent();
            grpAlcaldes.Enabled = false;
            grpDiputados.Enabled = false;
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

                // Ingresa los candidatos en las tablas correspondientes según su tipo de elección
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

            CargarCandidatos();

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
                            idMesaElector = Convert.ToInt32(reader["NumeroMesa"]);
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
            cmbCandidatosAlcaldes.SelectedIndex = -1;
            cmbCandidatosDiputados.SelectedIndex = -1;

            if (!idElectorSeleccionado.HasValue) return;

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT VotoAlcalde, VotoDiputados FROM Electores WHERE IdElector = @idElector";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idElector", idElectorSeleccionado.Value);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            alcaldeVotado = Convert.ToBoolean(reader["VotoAlcalde"]);
                            diputadoVotado = Convert.ToBoolean(reader["VotoDiputados"]);

                            if (alcaldeVotado)
                            {
                                grpAlcaldes.Enabled = false;
                                lblEstadoAlcaldes.Text = "Ya ha votado para Alcalde";
                            }

                            if (diputadoVotado)
                            {
                                grpDiputados.Enabled = false;
                                lblEstadoDiputados.Text = "Ya ha votado para Diputados";
                            }
                        }
                    }
                }
            }
        }
         

        private void InsertarVoto(SqlConnection con, SqlTransaction tran, string tipo, int? idCandidato, bool valido)
        {
            string query = "INSERT INTO Votos (idCandidato, TipoEleccion, FechaVoto, EstadoVoto, IdUsuario, IdMesa) VALUES (@candidato, @tipo, @fecha, @estado, @usuario, @mesa)";
            using (SqlCommand cmd = new SqlCommand(query, con, tran))
            {
                cmd.Parameters.AddWithValue("@candidato", idCandidato.HasValue ? (object)idCandidato.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                cmd.Parameters.AddWithValue("@estado", valido ? 1 : 0);
                cmd.Parameters.AddWithValue("@usuario", SesionActual.IdUsuario);
                cmd.Parameters.AddWithValue("@mesa", idMesaElector);
                cmd.ExecuteNonQuery();
            }

            string update = tipo == "Alcalde" ? "UPDATE Electores SET VotoAlcalde = 1 WHERE IdElector = @idElector" : "UPDATE Electores SET VotoDiputados = 1 WHERE IdElector = @idElector";
            using (SqlCommand cmd = new SqlCommand(update, con, tran))
            {
                cmd.Parameters.AddWithValue("@idElector", idElectorSeleccionado.Value);
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

            // Booleanos para determinar si el elector ya voto por cada tipo de eleccion
            bool hayAlcalde = !alcaldeVotado && (rbNuloAlcaldes.Checked || cmbCandidatosAlcaldes.SelectedValue != null);
            bool hayDiputado = !diputadoVotado && (rbNuloDiputados.Checked || cmbCandidatosDiputados.SelectedValue != null);

            if (hayDiputado && rbValidoDiputados.Checked && cmbCandidatosDiputados.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un candidato para Diputados o marque como nulo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (hayAlcalde && rbValidoAlcaldes.Checked && cmbCandidatosAlcaldes.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un candidato para Alcalde o marque como nulo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        InsertarVoto(con, tran, "Alcalde", rbValidoAlcaldes.Checked ? Convert.ToInt32(cmbCandidatosAlcaldes.SelectedValue) : (int?)null, rbValidoAlcaldes.Checked);
                    }
                    if (hayDiputado)
                    {
                        InsertarVoto(con, tran, "Diputados", rbValidoDiputados.Checked ? Convert.ToInt32(cmbCandidatosDiputados.SelectedValue) : (int?)null, rbValidoDiputados.Checked);
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
            // Registrar voto y notificacion
            MessageBox.Show("Voto registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Notificaciones.Guardar("Voto", $"El usuario {SesionActual.Usuario} ha registrado un voto para el elector {txtNombre.Text}.");
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

        private void rbValidoAlcaldes_CheckedChanged(object sender, EventArgs e)
        {
            cmbCandidatosAlcaldes.Enabled = rbValidoAlcaldes.Checked;
            if (!rbValidoAlcaldes.Checked)
                cmbCandidatosAlcaldes.SelectedIndex = -1;
        }

        private void rbValidoDiputados_CheckedChanged(object sender, EventArgs e)
        {
            cmbCandidatosDiputados.Enabled = rbValidoDiputados.Checked;
            if (!rbValidoDiputados.Checked)
                cmbCandidatosDiputados.SelectedIndex = -1;
        }
    }
}
