using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;

namespace Persistencia.interfaces
{
    public interface IPersistenciaPaquete
    {
        Paquete BuscarPaquete(int codigo, Empleado usLog);
        void AltaPaquete(Paquete paquete, Empleado usLog);
        List<Paquete> ListadoPaquetesSinSolicitud(Empleado usLog);

    }
}
