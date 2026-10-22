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
    public partial class FrmMesaNueva : Form
    {
        public FrmMesaNueva()
        {
            InitializeComponent();
            CargarCentros();
        }

        private void CargarCentros()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT IdCentro, Nombre FROM CentrosVotacion WHERE Estado = 1";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbCentros.DisplayMember = "Nombre";
                cmbCentros.ValueMember = "IdCentro";
                cmbCentros.DataSource = dt;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (cmbCentros.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un centro de votación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                int num = SiguenteMesa();
                if (MessageBox.Show($"¿Está seguro de agregar la mesa número '{num}' en el centro de votación seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
                string query = "INSERT INTO MesasVotacion (IdCentro, NumeroMesa, Estado) VALUES (@idCentro, @numero, 1)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idCentro", cmbCentros.SelectedValue);
                cmd.Parameters.AddWithValue("@numero", num);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Mesa creada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Notificaciones.Guardar("Mesa de Votación", $"El usuario {SesionActual.Usuario} ha creado la mesa número '{SiguenteMesa() - 1}' en el centro de votación '{cmbCentros.Text}'.");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int SiguenteMesa()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT ISNULL(Max(NumeroMesa), 0) + 1 FROM MesasVotacion WHERE IdCentro = @idCentro";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idCentro", cmbCentros.SelectedValue);
                    con.Open();
                    //ExecuteScalar: Ejecuta la consulta y devuelve la primera columna de la primera fila del conjunto de resultados devuelto por la consulta. Las demas columnas o filas no se tienen en cuenta.
                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
