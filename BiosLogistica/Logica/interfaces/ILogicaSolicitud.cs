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
        int AltaSolicitud(Solicitud solicitud, Empleado usLog);
        void ModificarEstadoSolicitud(Solicitud solicitud, Empleado usLog);
        string listadoSolicitudesEnCamino();
        List<Solicitud> listadoSolicitudesEmpresa(Empresa usLog);
        List<Solicitud> listadoSolicitudes(Empleado usLog);
    }
}
