using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using Logica.interfaces;
using Persistencia;

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

        internal static void validarSolicitud(Solicitud solicitud)
        {
            if (solicitud.FechaEntrega < DateTime.Today.Date)
            {
                throw new Exception("Fecha de entrega invalida.");
            }
        }

        public void AltaSolicitud(Solicitud solicitud, Usuario usLog)
        {
            validarSolicitud(solicitud);
            FabricaPersistencia.GetPersistenciaSolicitud().AltaSolicitud(solicitud, usLog);
        }

        public void BajaSolicitud(Solicitud solicitud, Usuario usLog)
        {
            validarSolicitud(solicitud);
            FabricaPersistencia.GetPersistenciaSolicitud().BajaSolicitud(solicitud, usLog);
        }

        public Solicitud BuscarSolicitud(int codigo, Usuario usLog)
        {
            return FabricaPersistencia.GetPersistenciaSolicitud().BuscarSolicitud(codigo, usLog);
        }

        public string listadoSolicitudes(Usuario usLog)
        {
            return FabricaPersistencia.GetPersistenciaSolicitud().listadoSolicitudes(usLog);
        }

        public void ModificarSolicitud(Solicitud solicitud, Usuario usLog)
        {
            validarSolicitud(solicitud);
            FabricaPersistencia.GetPersistenciaSolicitud().ModificarSolicitud(solicitud, usLog);
        }
    }
}
