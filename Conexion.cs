using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVotos
{
    internal class Conexion
    {
        public static SqlConnection ObtenerConexion()
        {
            // Se usa local para que sea ejecutable en cualquier dispositivo donde la base de datos este almacenada
            string cadena = @"Server=localhost\SQLEXPRESS;Database=SistemaVotos;Trusted_Connection=True;";
            SqlConnection conexion = new SqlConnection(cadena);
            return conexion;
        }
    }
}
