using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService" en el código y en el archivo de configuración a la vez.
[ServiceContract]
public interface IService
{
    #region LogicaUsuario

    [OperationContract]
    void AltaUsuario(Usuario usuario, Empleado usLog);

    [OperationContract]
    void BajaUsuario(Usuario usuario, Empleado usLog);

    [OperationContract]
    Usuario BuscarUsuario(string logueo, Empleado usLog);

    [OperationContract]
    Usuario LogueoUsuario(string logueo, string contraseana);

    [OperationContract]
    void ModificarUsuario(Usuario usuario, Empleado usLog);

    [OperationContract]
    List<Empresa> ListarEmpresas(Empleado usuarioLogueado);

    #endregion

    #region LogicaPaquete

    [OperationContract]
    void AltaPaquete(Paquete paquete, Empleado usLog);

    [OperationContract]
    Paquete BuscarPaquete(int codigo, Empleado usLog);

    [OperationContract]
    List<Paquete> ListadoPaquetesSinSolicitud(Empleado usLog);

    #endregion

    #region LogicaSolicitud

    [OperationContract]
    int AltaSolicitud(Solicitud solicitud, Empleado usLog);

    [OperationContract]
    void ModificarEstadoSolicitud(Solicitud solicitud, Empleado usLog);

    [OperationContract]
    string listadoSolicitudesEnCamino();

    [OperationContract]
    List<Solicitud> listadoSolicitudesEmpresa(Empresa usLog);

    [OperationContract]
    List<Solicitud> listadoSolicitudes(Empleado usLog);

    #endregion
}
