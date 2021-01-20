using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.interfaces;

namespace Logica.clasesDeTrabajo
{
    internal class LogicaSolicitud : ILogicaSolicitud
    {
        private static LogicaSolicitud _instancia = null;
        private LogicaSolicitud() { }
        public static LogicaSolicitud GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new LogicaSolicitud();
            }
            return _instancia;
        }
    }
}
