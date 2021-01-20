using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia.interfaces;

namespace Persistencia
{
    internal class PersistenciaPaquete : IPersistenciaPaquete
    {
        private static PersistenciaPaquete _instancia = null;

        private PersistenciaPaquete() { }

        public static PersistenciaPaquete getInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new PersistenciaPaquete();
            }
            return _instancia;
        }


    }
}
