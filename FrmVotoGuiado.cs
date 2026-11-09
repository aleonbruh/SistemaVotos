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
    public partial class FrmVotoGuiado : Form
    {
        private string tipoEleccion;
        private int idElector;
        private int idMesaElector;
        private string nombreElector;
        private string duiElector;

        private string eleccionActual;
        private bool diputadosDespues;

        public FrmVotoGuiado(string tipo, int idElector, int idMesa)
        {
            InitializeComponent();
            this.tipoEleccion = tipo;
            this.idElector = idElector;
            this.idMesaElector = idMesa;
            CargarDatosElector();
            ConfigurarFlujo();
        }

        private void CargarDatosElector()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT NombreCompleto, DUI FROM Electores WHERE idElector = @idElector";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idElector", idElector);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nombreElector = reader["NombreCompleto"].ToString();
                            duiElector = reader["DUI"].ToString();
                            lblNombre.Text = nombreElector;
                            lblDui.Text = $"DUI: {duiElector}";
                        }
                    }
                }
            }
        }

        private void ConfigurarFlujo()
        {
            pnlBievenida.Visible = true;
            pnlVotacion.Visible = false;
            pnlFinal.Visible = false;
            pnlBievenida.BringToFront();
        }

        private void btnEmpezar_Click(object sender, EventArgs e)
        {
            if (tipoEleccion == "Solo Alcaldes")
            {
                eleccionActual = "Alcalde";
                diputadosDespues = false;
            }
            else if (tipoEleccion == "Solo Diputados")
            {
                eleccionActual = "Diputados";
                diputadosDespues = false;
            }
            else if (tipoEleccion == "Ambos")
            {
                eleccionActual = "Alcalde";
                diputadosDespues = true;
            }

            PrepararPanelVoto();
            pnlBievenida.Visible = false;
            pnlVotacion.Visible = true;
            pnlVotacion.BringToFront();
        }

        private void PrepararPanelVoto()
        {
            lblTipoEleccion.Text = $"Eleccion de {eleccionActual} para San Salvador Centro";
            CargarCandidatos(eleccionActual);
            rbVotoValido.Checked = true;
            cmbCandidatos.SelectedIndex = -1;
        }

        private void CargarCandidatos(string tipo)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT c.IdCandidato, c.NombreCompleto + ' (' + p.Siglas + ')' AS NombreCompleto FROM Candidatos c INNER JOIN Partidos p ON c.IdPartido = p.IdPartido WHERE c.TipoEleccion = @tipo AND c.Estado = 1";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@tipo", tipo);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbCandidatos.DataSource = dt;
                cmbCandidatos.DisplayMember = "NombreCompleto";
                cmbCandidatos.ValueMember = "IdCandidato";
            }
        }

        private void rbVotoValido_CheckedChanged(object sender, EventArgs e)
        {
            cmbCandidatos.Enabled = rbVotoValido.Checked;
            if (!rbVotoValido.Checked)
            {
                cmbCandidatos.SelectedIndex = -1;
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            bool votoValido = rbVotoValido.Checked;
            int? idCandidato = null;

            if (votoValido)
            {
                if (cmbCandidatos.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, seleccione un candidato o marque el voto en blanco.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                idCandidato = Convert.ToInt32(cmbCandidatos.SelectedValue);
            }

            GuardarVoto(eleccionActual, idCandidato, votoValido);

            if (diputadosDespues && eleccionActual == "Alcalde")
            {
                eleccionActual = "Diputados";
                diputadosDespues = false;
                PrepararPanelVoto();
            }
            else
            {
                pnlVotacion.Visible = false;
                pnlFinal.Visible = true;
            }
        }

        private void GuardarVoto(string tipoEleccion, int? idCandidato, bool valido)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    string query = "INSERT INTO Votos (IdCandidato, TipoEleccion, FechaVoto, EstadoVoto, IdUsuario, IdMesa) VALUES (@IdCandidato, @TipoEleccion, @FechaVoto, @EstadoVoto, @IdUsuario, @IdMesa)";
                    using (SqlCommand cmd = new SqlCommand(query, con, tran))
                    {
                        cmd.Parameters.AddWithValue("@IdCandidato", idCandidato.HasValue ? (object)idCandidato.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@TipoEleccion", tipoEleccion);
                        cmd.Parameters.AddWithValue("@FechaVoto", DateTime.Now);
                        cmd.Parameters.AddWithValue("@EstadoVoto", valido ? 1 : 0);
                        cmd.Parameters.AddWithValue("@IdUsuario", SesionActual.IdUsuario);
                        cmd.Parameters.AddWithValue("@IdMesa", idMesaElector);
                        cmd.ExecuteNonQuery();
                    }
                    string update = tipoEleccion == "Alcalde" ? "UPDATE Electores SET VotoAlcalde = 1 WHERE IdElector = @IdElector" : "UPDATE Electores SET VotoDiputados = 1 WHERE IdElector = @IdElector";
                    using (SqlCommand cmdUpdate = new SqlCommand(update, con, tran))
                    {
                        cmdUpdate.Parameters.AddWithValue("@IdElector", idElector);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Error al guardar el voto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnaFin_Click(object sender, EventArgs e)
        {
            Notificaciones.Guardar("Voto", $"El usuario {SesionActual.Usuario} ha registrado el voto del elector {nombreElector}.");
            this.Close();
        }
    }
}
