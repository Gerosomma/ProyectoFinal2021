using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using Logica.interfaces;
using Persistencia;

namespace Logica.clasesDeTrabajo
{
    internal class LogicaUsuario : ILogicaUsuario
    {
        private static LogicaUsuario _instancia = null;
        private LogicaUsuario() { }
        public static LogicaUsuario GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new LogicaUsuario();
            }
            return _instancia;
        }

        internal static void validarUsuario(Usuario us)
        {
            
        }

        public void AltaUsuario(Usuario usuario, Empleado usLog)
        {
            validarUsuario(usuario);
            if (usuario is Empleado)
            {
                FabricaPersistencia.GetPersistenciaEmpleado().AltaEmpleado((Empleado)usuario, usLog);
            }
            else if (usuario is Empresa)
            {
                FabricaPersistencia.GetPersistenciaEmpresa().AltaEmpresa((Empresa)usuario, usLog);
            }
        }

        public void BajaUsuario(Usuario usuario, Empleado usLog)
        {
            validarUsuario(usuario);
            if (usuario is Empleado)
            {
                FabricaPersistencia.GetPersistenciaEmpleado().BajaEmpleado((Empleado)usuario, usLog);
            }
            else if (usuario is Empresa)
            {
                FabricaPersistencia.GetPersistenciaEmpresa().BajaEmpresa((Empresa)usuario, usLog);
            }
        }

        public Usuario BuscarUsuario(string logueo, Empleado usLog)
        {
            Usuario usuario = FabricaPersistencia.GetPersistenciaEmpleado().BuscarEmpleado(logueo, usLog);

            if (usuario == null)
            {
                usuario = FabricaPersistencia.GetPersistenciaEmpresa().BuscarEmpresa(logueo, usLog);
            }

            return usuario;
        }

        public Usuario LogueoUsuario(string logueo, string contraseana)
        {
            try
            {
                Usuario usuario = FabricaPersistencia.GetPersistenciaEmpleado().LoguearEmpleado(logueo, contraseana);

                if (usuario == null)
                {
                    usuario = FabricaPersistencia.GetPersistenciaEmpresa().LoguearEmpresa(logueo, contraseana);
                }

                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarUsuario(Usuario usuario, Empleado usLog)
        {
            validarUsuario(usuario);
            if (usuario is Empleado)
            {
                FabricaPersistencia.GetPersistenciaEmpleado().ModificarEmpleado((Empleado)usuario, usLog);
            }
            else if (usuario is Empresa)
            {
                FabricaPersistencia.GetPersistenciaEmpresa().ModificarEmpresa((Empresa)usuario, usLog);
            }
        }

        public List<Empresa> ListarEmpresas(Empleado usLog)
        {
            return FabricaPersistencia.GetPersistenciaEmpresa().ListarEmpresas(usLog);
        }
    }
}
