using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVotos
{
    internal class SesionActual
    {
        public static int IdUsuario {  get; set; }
        public static string NombreCompleto { get; set; }
        public static string Usuario {  get; set; }
        public static string Rol {  get; set; }

        static SesionActual()
        {
            // Valores por defecto al iniciar
            IdUsuario = 0;
            NombreCompleto = string.Empty;
            Usuario = string.Empty;
            Rol = string.Empty;
        }
    }
}
