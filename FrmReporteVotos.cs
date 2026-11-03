using ClosedXML.Excel;
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
using static ClosedXML.Excel.XLPredefinedFormat;

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

        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "PDF Files|*.pdf";
            guardar.FileName = "ReporteGeneral.pdf";

            if (guardar.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using (iTextSharp.text.Document doc = new iTextSharp.text.Document())
            {
                iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new System.IO.FileStream(guardar.FileName, System.IO.FileMode.Create));
                doc.Open();

                //Encabezado
                doc.Add(new iTextSharp.text.Paragraph("Reporte de Votos"));
                doc.Add(new iTextSharp.text.Paragraph("Fecha: " + System.DateTime.Now.ToString("dd/MM/yyyy")));
                doc.Add(new iTextSharp.text.Paragraph(" "));

                //Sección Alcaldes
                doc.Add(new iTextSharp.text.Paragraph("Resultados Alcaldes:"));
                doc.Add(new iTextSharp.text.Paragraph(" "));
                doc.Add(CrearTablaResultados("Alcalde"));
                doc.Add(new iTextSharp.text.Paragraph(" "));

                // Sección Diputados
                doc.Add(new iTextSharp.text.Paragraph("Resultados Diputados:"));
                doc.Add(new iTextSharp.text.Paragraph(" "));
                doc.Add(CrearTablaResultados("Diputados"));
                doc.Add(new iTextSharp.text.Paragraph(" "));

                // Tabla final de todos los votos
                doc.Add(new iTextSharp.text.Paragraph("Detalle de Votos:"));
                doc.Add(new iTextSharp.text.Paragraph(" "));
                doc.Add(CrearTablaVotos());

                doc.Close();
            }

            MessageBox.Show("Reporte PDF generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Notificaciones.Guardar("Reporte", $"El usuario {SesionActual.Usuario} ha generado un reporte PDF.");
        }

        private iTextSharp.text.pdf.PdfPTable CrearTablaResultados(string tipo)
        {
            iTextSharp.text.pdf.PdfPTable tabla = new iTextSharp.text.pdf.PdfPTable(3);
            tabla.AddCell("Candidato");
            tabla.AddCell("Partido");
            tabla.AddCell("Votos");

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"SELECT ca.NombreCompleto, p.Siglas, COUNT(v.IdVoto) AS TotalVotos
                         FROM Candidatos ca
                         INNER JOIN Partidos p ON ca.IdPartido = p.IdPartido
                         LEFT JOIN Votos v ON v.IdCandidato = ca.IdCandidato
                              AND v.TipoEleccion = @tipo AND v.EstadoVoto = 1
                         WHERE ca.TipoEleccion = @tipo
                         GROUP BY ca.NombreCompleto, p.Siglas
                         ORDER BY TotalVotos DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tabla.AddCell(reader["NombreCompleto"].ToString());
                        tabla.AddCell(reader["Siglas"].ToString());
                        tabla.AddCell(reader["TotalVotos"].ToString());
                    }
                }
            }
            return tabla;
        }

        private iTextSharp.text.pdf.PdfPTable CrearTablaVotos()
        {
            iTextSharp.text.pdf.PdfPTable tabla = new iTextSharp.text.pdf.PdfPTable(6);
            tabla.AddCell("Voto");
            tabla.AddCell("Candidato");
            tabla.AddCell("Partido");
            tabla.AddCell("Tipo");
            tabla.AddCell("Estado");
            tabla.AddCell("Mesa");

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"SELECT v.IdVoto, COALESCE(ca.NombreCompleto, 'Voto Nulo') AS Candidato,
                                COALESCE(p.Siglas, '-') AS Partido, v.TipoEleccion,
                                CASE WHEN v.EstadoVoto = 1 THEN 'Valido' ELSE 'Nulo' END AS Estado,
                                m.NumeroMesa
                         FROM Votos v
                         LEFT JOIN Candidatos ca ON v.IdCandidato = ca.IdCandidato
                         LEFT JOIN Partidos p ON ca.IdPartido = p.IdPartido
                         INNER JOIN MesasVotacion m ON v.IdMesa = m.IdMesa
                         ORDER BY v.EstadoVoto DESC, v.IdVoto";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tabla.AddCell(reader["IdVoto"].ToString());
                        tabla.AddCell(reader["Candidato"].ToString());
                        tabla.AddCell(reader["Partido"].ToString());
                        tabla.AddCell(reader["TipoEleccion"].ToString());
                        tabla.AddCell(reader["Estado"].ToString());
                        tabla.AddCell(reader["NumeroMesa"].ToString());
                    }
                }
            }
            return tabla;
        }

        private void btnGenerarExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "Excel Files|*.xlsx";
            guardar.FileName = "ReporteGeneral.xlsx";
            if (guardar.ShowDialog() != DialogResult.OK)
                return;
            
            using (var workbook = new XLWorkbook())
            {
                var hoja = workbook.Worksheets.Add("Reporte de Votos");

                string[] columnas = { "ID de Voto", "Candidato", "Partido", "Tipo", "Estado", "Centro de Votación", "Numero de Mesa" };
                for (int i = 0; i < columnas.Length; i++)
                {
                    hoja.Cell(1, i + 1).Value = columnas[i];
                }

                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    string query = @"SELECT v.IdVoto, COALESCE(ca.NombreCompleto, 'Voto Nulo') AS Candidato,
                                    COALESCE(p.Siglas, '-') AS Partido, v.TipoEleccion,
                                    CASE WHEN v.EstadoVoto = 1 THEN 'Valido' ELSE 'Nulo' END AS Estado,
                                    c.Nombre AS Centro, m.NumeroMesa
                             FROM Votos v
                             LEFT JOIN Candidatos ca ON v.IdCandidato = ca.IdCandidato
                             LEFT JOIN Partidos p ON ca.IdPartido = p.IdPartido
                             INNER JOIN MesasVotacion m ON v.IdMesa = m.IdMesa
                             INNER JOIN CentrosVotacion c ON m.IdCentro = c.IdCentro
                             ORDER BY v.EstadoVoto DESC, v.IdVoto";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int fila = 2;
                        while (reader.Read())
                        {
                            hoja.Cell(fila, 1).Value = reader["IdVoto"].ToString();
                            hoja.Cell(fila, 2).Value = reader["Candidato"].ToString();
                            hoja.Cell(fila, 3).Value = reader["Partido"].ToString();
                            hoja.Cell(fila, 4).Value = reader["TipoEleccion"].ToString();
                            hoja.Cell(fila, 5).Value = reader["Estado"].ToString();
                            hoja.Cell(fila, 6).Value = reader["Centro"].ToString();
                            hoja.Cell(fila, 7).Value = reader["NumeroMesa"].ToString();
                            fila++;
                        }
                    }
                }

                hoja.Row(1).Style.Font.Bold = true;
                hoja.Columns().Width = 20;
                hoja.Column(6).Width = 40;
                hoja.RangeUsed().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                hoja.RangeUsed().SetAutoFilter();

                workbook.SaveAs(guardar.FileName);
            }

            MessageBox.Show("Reporte Excel generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Notificaciones.Guardar("Reporte", $"El usuario {SesionActual.Usuario} ha generado un reporte Excel.");
        }
    }
}
