using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.interfaces
{
    public interface ILogicaPaquete
    {
        Paquete BuscarPaquete(int codigo, Empleado usLog);
        void AltaPaquete(Paquete paquete, Empleado usLog);
        List<Paquete> ListadoPaquetesSinSolicitud(Empleado usLog);
    }
}
