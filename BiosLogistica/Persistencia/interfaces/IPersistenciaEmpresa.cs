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
        Empresa BuscarEmpresa(string logueo, Usuario usLog);
        Empresa interBuscarEmpresa(SqlConnection conexion, string logueo);
        void AltaEmpresa(Empresa empresa, Usuario usLog);
        void ModificarEmpresa(Empresa empresa, Usuario usLog);
        void BajaEmpresa(Empresa empresa, Usuario usLog);
        List<Empresa> ListarEmpresas(Usuario usuarioLogueado);
    }
}
