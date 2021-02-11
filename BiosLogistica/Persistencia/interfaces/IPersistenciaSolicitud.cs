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
        void AltaSolicitud(Solicitud solicitud, Empleado usLog);
        void ModificarEstadoSolicitud(Solicitud solicitud, Empleado usLog);
        List<Solicitud> listadoSolicitudesEnCamino(Usuario usLog);
        List<Solicitud> listadoSolicitudesEmpresa(Empresa usLog);

    }
}
