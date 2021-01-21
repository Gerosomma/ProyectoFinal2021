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
        Usuario BuscarUsuario(string logueo, Usuario usLog);
        void AltaUsuario(Usuario usuario, Usuario usLog);
        void ModificarUsuario(Usuario usuario, Usuario usLog);
        void BajaUsuario(Usuario usuario, Usuario usLog);
    }
}
