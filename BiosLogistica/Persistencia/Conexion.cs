﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    internal class Conexion
    {
        internal static string Cnn(EntidadesCompartidas.Usuario u = null)
        {
            if (u == null)
                return "Data Source =.; Initial Catalog = ProyectoSegundo2020; Integrated Security = true";
            else
                return "Data Source =.; Initial Catalog = ProyectoSegundo2020; User=" + u.Logueo + "; Password='" + u.Contrasena + "'";
        }
    }
}
