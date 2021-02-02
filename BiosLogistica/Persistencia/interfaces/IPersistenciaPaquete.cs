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
        Paquete BuscarPaquete(int codigo, Usuario usLog);
        void AltaPaquete(Paquete paquete, Usuario usLog);
    }
}
