using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;

namespace Persistencia.interfaces
{
    public interface IPersistenciaSolicitud
    {
        void AltaSolicitud(Solicitud solicitud, Usuario usLog);
        void ModificarEstadoSolicitud(Solicitud solicitud, Usuario usLog);
        List<Solicitud> listadoSolicitudesEnCamino(Usuario usLog);
    }
}
