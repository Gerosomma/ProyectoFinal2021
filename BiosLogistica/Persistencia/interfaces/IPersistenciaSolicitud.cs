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
        Solicitud BuscarSolicitud(int codigo, Usuario usLog);
        void AltaSolicitud(Solicitud solicitud, Usuario usLog);
        void ModificarSolicitud(Solicitud solicitud, Usuario usLog);
        void BajaSolicitud(Solicitud solicitud, Usuario usLog);
    }
}
