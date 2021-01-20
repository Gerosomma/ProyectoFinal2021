using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia.interfaces;
using Persistencia;

namespace Persistencia
{
    public class FabricaPersistencia
    {
        public static IPersistenciaEmpleado GetPersistenciaEmpleado()
        {
            return (PersistenciaEmpleado.getInstancia());
        }

        public static IPersistenciaEmpresa GetPersistenciaEmpresa()
        {
            return (PersistenciaEmpresa.getInstancia());
        }

        public static IPersistenciaPaquete GetPersistenciaPaquete()
        {
            return (PersistenciaPaquete.getInstancia());
        }
        public static IPersistenciaSolicitud GetPersistenciaSolicitud()
        {
            return (PersistenciaSolicitud.getInstancia());

        }
    }
}
