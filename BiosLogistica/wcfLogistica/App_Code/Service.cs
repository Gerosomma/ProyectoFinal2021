using EntidadesCompartidas;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
public class Service : IService
{
    #region LogicaUsuario

    void IService.AltaUsuario(Usuario usuario, Empleado usLog)
    {
        FabricaLogica.GetLogicaUsuario().AltaUsuario(usuario, usLog);
    }

    void IService.BajaUsuario(Usuario usuario, Empleado usLog)
    {
        FabricaLogica.GetLogicaUsuario().BajaUsuario(usuario, usLog);
    }

    Usuario IService.BuscarUsuario(string logueo, Empleado usLog)
    {
        return FabricaLogica.GetLogicaUsuario().BuscarUsuario(logueo, usLog);
    }

    Usuario IService.LogueoUsuario(string logueo, string contraseana)
    {
        return FabricaLogica.GetLogicaUsuario().LogueoUsuario(logueo, contraseana);
    }

    void IService.ModificarUsuario(Usuario usuario, Empleado usLog)
    {
        FabricaLogica.GetLogicaUsuario().ModificarUsuario(usuario, usLog);
    }

    List<Empresa> IService.ListarEmpresas(Empleado usuarioLogueado)
    {
        return FabricaLogica.GetLogicaUsuario().ListarEmpresas(usuarioLogueado);
    }

    #endregion

    #region LogicaPaquete

    void IService.AltaPaquete(Paquete paquete, Empleado usLog)
    {
        FabricaLogica.GetLogicaPaquete().AltaPaquete(paquete, usLog);
    }

    Paquete IService.BuscarPaquete(int codigo, Empleado usLog)
    {
        return FabricaLogica.GetLogicaPaquete().BuscarPaquete(codigo, usLog);
    }

    List<Paquete> IService.ListadoPaquetesSinSolicitud(Empleado usLog)
    {
        return FabricaLogica.GetLogicaPaquete().ListadoPaquetesSinSolicitud(usLog);
    }

    #endregion

    #region LogicaSolicitud

    void IService.AltaSolicitud(Solicitud solicitud, Empleado usLog)
    {
        FabricaLogica.GetLogicaSolicitud().AltaSolicitud(solicitud, usLog);
    }

    void IService.ModificarEstadoSolicitud(Solicitud solicitud, Empleado usLog)
    {
        FabricaLogica.GetLogicaSolicitud().ModificarEstadoSolicitud(solicitud, usLog);
    }

    string IService.listadoSolicitudesEnCamino()
    {
        return FabricaLogica.GetLogicaSolicitud().listadoSolicitudesEnCamino();
    }
    List<Solicitud> IService.listadoSolicitudesEmpresa(Empresa usLog)
    {
        return FabricaLogica.GetLogicaSolicitud().listadoSolicitudesEmpresa(usLog);
    }

    #endregion
}
