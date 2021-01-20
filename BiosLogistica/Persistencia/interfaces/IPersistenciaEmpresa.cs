using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;

namespace Persistencia.interfaces
{
    public interface IPersistenciaEmpresa
    {
        Empresa BuscarEmpresa(string logueo, Usuario usLog);
        void AltaEmpresa(Empleado empresa, Usuario usLog);
        void ModificarEmpresa(Empresa empresa, Usuario usLog);
        void BajaEmpresa(Empresa empresa, Usuario usLog);
        
    }
}
