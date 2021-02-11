using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using System.Data.SqlClient;

namespace Persistencia.interfaces
{
    public interface IPersistenciaEmpresa
    {
        Empresa LoguearEmpresa(string logueo, string contrasenia);
        Empresa BuscarEmpresa(string logueo, Empleado usLog);
        void AltaEmpresa(Empresa empresa, Empleado usLog);
        void ModificarEmpresa(Empresa empresa, Empleado usLog);
        void BajaEmpresa(Empresa empresa, Empleado usLog);
        List<Empresa> ListarEmpresas(Empleado usuarioLogueado);
    }
}
