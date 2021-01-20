using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    internal class Conexion
    {
        private static string autenticacion = "data source = .; initial catalog = ProyectoSegundo2019;";

        public static string ObtenerCadenaConexion(string us, string pw)
        {
            return autenticacion + String.Format(" User Id={0}; Password={1};", us, pw);
        }
        
    }
}
