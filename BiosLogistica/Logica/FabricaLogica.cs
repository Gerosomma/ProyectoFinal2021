using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.interfaces;
using Logica.clasesDeTrabajo;

namespace Logica
{
    public class FabricaLogica
    {
        public static ILogicaUsuario GetLogicaUsuario()
        {
            return (LogicaUsuario.GetInstancia());
        }

        public static ILogicaPaquete GetLogicaPaquete()
        {
            return (LogicaPaquete.GetInstancia());
        }

        public static ILogicaSolicitud GetLogicaSolicitud()
        {
            return (LogicaSolicitud.GetInstancia());
        }
    }
}
