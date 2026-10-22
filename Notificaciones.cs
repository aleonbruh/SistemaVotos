using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVotos
{
    internal class Notificaciones
    {
        public static void Guardar(string tipo, string mensaje)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                con.Open();
                string query = "INSERT INTO Notificaciones (Tipo, Mensaje, FechaHora, IdUsuario) VALUES (@Tipo, @Mensaje, @FechaHora, @IdUsuario)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Tipo", tipo);
                    cmd.Parameters.AddWithValue("@Mensaje", mensaje);
                    cmd.Parameters.AddWithValue("@FechaHora", DateTime.Now);
                    cmd.Parameters.AddWithValue("@IdUsuario", SesionActual.IdUsuario);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
