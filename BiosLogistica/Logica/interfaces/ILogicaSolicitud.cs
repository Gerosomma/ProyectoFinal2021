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
        Solicitud BuscarSolicitud(int codigo, Usuario usLog);
        void AltaSolicitud(Solicitud solicitud, Usuario usLog);
        void ModificarSolicitud(Solicitud solicitud, Usuario usLog);
        void BajaSolicitud(Solicitud solicitud, Usuario usLog);
        string listadoSolicitudes(Usuario usLog);
    }
}
