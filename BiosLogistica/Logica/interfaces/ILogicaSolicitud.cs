using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.interfaces
{
    public interface ILogicaSolicitud
    {
        void AltaSolicitud(Solicitud solicitud, Usuario usLog);
        void ModificarEstadoSolicitud(Solicitud solicitud, Usuario usLog);
        string listadoSolicitudes(Usuario usLog);
    }
}
