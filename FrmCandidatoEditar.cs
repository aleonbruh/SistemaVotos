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
    public partial class FrmCandidatoEditar : Form
    {
        private int? idCandidato;
        public FrmCandidatoEditar(int? id = null)
        {
            InitializeComponent();
            this.idCandidato = id;
            CargarPartidos();
            CargarTiposEleccion();

            if (idCandidato.HasValue)
            {
                this.Text = "Editar Candidato";
                CargarDatos();
            }
            else
            {
                this.Text = "Nuevo Candidato";
            }
        }

        private void CargarPartidos()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT IdPartido, Siglas FROM Partidos WHERE Estado = 1";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbPartido.DisplayMember = "Siglas";
                cmbPartido.ValueMember = "IdPartido";
                cmbPartido.DataSource = dt;
            }
        }

        private void CargarTiposEleccion()
        {
            cmbTipo.Items.Clear();
            cmbTipo.Items.Add("Alcalde");
            cmbTipo.Items.Add("Diputados");
        }

        private void CargarDatos()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT NombreCompleto, IdPartido, TipoEleccion FROM Candidatos WHERE IdCandidato = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", idCandidato.Value);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtNombre.Text = reader["NombreCompleto"].ToString();
                            cmbPartido.SelectedValue = reader["IdPartido"];
                            cmbTipo.SelectedItem = reader["TipoEleccion"].ToString();
                        }
                    }
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || cmbPartido.SelectedIndex == -1 || cmbTipo.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor complete todos los campos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query;
                if (idCandidato.HasValue)
                {
                    query = "UPDATE Candidatos SET NombreCompleto = @nombre, IdPartido = @partido, TipoEleccion = @tipo WHERE IdCandidato = @id";
                }
                else
                {
                    query = "INSERT INTO Candidatos (NombreCompleto, IdPartido, TipoEleccion, Estado) VALUES (@nombre, @partido, @tipo, 1)";
                }
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@partido", cmbPartido.SelectedValue);
                    cmd.Parameters.AddWithValue("@tipo", cmbTipo.SelectedItem.ToString());
                    if (idCandidato.HasValue)
                        cmd.Parameters.AddWithValue("@id", idCandidato.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Candidato guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


