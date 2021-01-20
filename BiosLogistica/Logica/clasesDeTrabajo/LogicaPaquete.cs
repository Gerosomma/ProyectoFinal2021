using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.interfaces;

namespace Logica.clasesDeTrabajo
{
    internal class LogicaPaquete : ILogicaPaquete
    {
        private static LogicaPaquete _instancia = null;
        private LogicaPaquete() { }
        public static LogicaPaquete GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new LogicaPaquete();
            }
            return _instancia;
        }
    }
}
