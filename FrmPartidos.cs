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
    public partial class FrmPartidos : Form
    {
        public FrmPartidos()
        {
            InitializeComponent();
        }

        private void FrmPartidos_Load(object sender, EventArgs e)
        {
            CargarPartidos();
        }

        private void CargarPartidos()
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT IdPartido, Nombre, Siglas, CASE WHEN Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS EstadoTexto FROM Partidos";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPartidos.DataSource = dt;
                dgvPartidos.Columns["IdPartido"].Visible = false;
            }
        }
    }
}
