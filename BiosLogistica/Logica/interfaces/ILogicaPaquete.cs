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
        Paquete BuscarPaquete(int codigo, Usuario usLog);
        void AltaPaquete(Paquete paquete, Usuario usLog);
        void ModificarPaquete(Paquete paquete, Usuario usLog);
        void BajaPaquete(Paquete paquete, Usuario usLog);
    }
}
