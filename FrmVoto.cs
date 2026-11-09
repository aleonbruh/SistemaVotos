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
        private int idElectorSeleccionado;
        private int idMesaElector;
        public FrmVoto()
        {
            InitializeComponent();
            cmbTipoEleccion.Enabled = false;
            cmbTipoEleccion.SelectedIndex = -1;
            btnAceptar.Enabled = false;
            CargarTipoEleccion();
        }

        private void CargarTipoEleccion()
        {
            cmbTipoEleccion.Items.Clear();
            cmbTipoEleccion.Items.Add("Solo Alcaldes");
            cmbTipoEleccion.Items.Add("Solo Diputados");
            cmbTipoEleccion.Items.Add("Ambos");
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
                string query = @"SELECT e.IdElector, e.NombreCompleto, c.Nombre AS Centro, m.IdMesa, m.NumeroMesa FROM Electores e INNER JOIN CentrosVotacion c ON e.IdCentro = c.IdCentro INNER JOIN MesasVotacion m ON e.IdMesa = m.IdMesa WHERE e.DUI = @DUI AND e.Estado = 1";
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
                            idMesaElector = Convert.ToInt32(reader["IdMesa"]);
                            txtMesa.Text = "Mesa " + reader["NumeroMesa"].ToString();

                            cmbTipoEleccion.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("No se encontró un elector con el DUI proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
         
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (idElectorSeleccionado <= 0)
            {
                MessageBox.Show("Por favor, busque un elector antes de votar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmbTipoEleccion.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un tipo de elección.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string tipoEleccion = cmbTipoEleccion.SelectedItem.ToString();
            FrmVotoGuiado frm = new FrmVotoGuiado(tipoEleccion, idElectorSeleccionado, idMesaElector);
            frm.Show();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbTipoEleccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAceptar.Enabled = cmbTipoEleccion.SelectedIndex != -1;
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                btnBuscar.PerformClick();
                e.Handled = true;
            }
        }
    }
}