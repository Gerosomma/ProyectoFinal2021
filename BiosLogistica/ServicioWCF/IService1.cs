using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using EntidadesCompartidas;

namespace ServicioWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
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
        void AltaSolicitud(Solicitud solicitud, Empleado usLog);

        [OperationContract]
        void ModificarEstadoSolicitud(Solicitud solicitud, Empleado usLog);

        [OperationContract]
        string listadoSolicitudesEnCamino(Usuario usLog);

        [OperationContract]
        List<Solicitud> listadoSolicitudesEmpresa(Empresa usLog);

        #endregion
    }
}
