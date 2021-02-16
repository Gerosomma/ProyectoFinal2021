using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using EntidadesCompartidas;
using Logica;

namespace ServicioWCF
{
    public class Service1 : IService1
    {
        #region LogicaUsuario
        
        void IService1.AltaUsuario(Usuario usuario, Empleado usLog)
        {
            FabricaLogica.GetLogicaUsuario().AltaUsuario(usuario, usLog);
        }
        
        void IService1.BajaUsuario(Usuario usuario, Empleado usLog)
        {
            FabricaLogica.GetLogicaUsuario().BajaUsuario(usuario, usLog);
        }
        
        Usuario IService1.BuscarUsuario(string logueo, Empleado usLog)
        {
            return FabricaLogica.GetLogicaUsuario().BuscarUsuario(logueo, usLog);
        }
        
        Usuario IService1.LogueoUsuario(string logueo, string contraseana)
        {
            return FabricaLogica.GetLogicaUsuario().LogueoUsuario(logueo, contraseana);
        }
        
        void IService1.ModificarUsuario(Usuario usuario, Empleado usLog)
        {
            FabricaLogica.GetLogicaUsuario().ModificarUsuario(usuario, usLog);
        }
        
        List<Empresa> IService1.ListarEmpresas(Empleado usuarioLogueado)
        {
            return FabricaLogica.GetLogicaUsuario().ListarEmpresas(usuarioLogueado);
        }

        #endregion

        #region LogicaPaquete

        void IService1.AltaPaquete(Paquete paquete, Empleado usLog)
        {
            FabricaLogica.GetLogicaPaquete().AltaPaquete(paquete, usLog);
        }

        Paquete IService1.BuscarPaquete(int codigo, Empleado usLog)
        {
            return FabricaLogica.GetLogicaPaquete().BuscarPaquete(codigo, usLog);
        }

        List<Paquete> IService1.ListadoPaquetesSinSolicitud(Empleado usLog)
        {
            return FabricaLogica.GetLogicaPaquete().ListadoPaquetesSinSolicitud(usLog);
        }

        #endregion

        #region LogicaSolicitud

        void IService1.AltaSolicitud(Solicitud solicitud, Empleado usLog)
        {
            FabricaLogica.GetLogicaSolicitud().AltaSolicitud(solicitud, usLog);
        }

        void IService1.ModificarEstadoSolicitud(Solicitud solicitud, Empleado usLog)
        {
            FabricaLogica.GetLogicaSolicitud().ModificarEstadoSolicitud(solicitud, usLog);
        }

        string IService1.listadoSolicitudesEnCamino(Usuario usLog)
        {
            return FabricaLogica.GetLogicaSolicitud().listadoSolicitudesEnCamino(usLog);
        }
        List<Solicitud> IService1.listadoSolicitudesEmpresa(Empresa usLog)
        {
            return FabricaLogica.GetLogicaSolicitud().listadoSolicitudesEmpresa(usLog);
        }

        #endregion
    }
}
