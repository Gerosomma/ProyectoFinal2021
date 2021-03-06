using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.interfaces
{
    public interface ILogicaUsuario
    {
        Usuario LogueoUsuario(string logueo, string contraseana);
        Usuario BuscarUsuario(string logueo, Empleado usLog);
        void AltaUsuario(Usuario usuario, Empleado usLog);
        void ModificarUsuario(Usuario usuario, Empleado usLog);
        void BajaUsuario(Usuario usuario, Empleado usLog);
        void ModificarContrasenaUsuario(Usuario usuario, Usuario usLog);
        List<Empresa> ListarEmpresas(Empleado usuarioLogueado);
    }
}
