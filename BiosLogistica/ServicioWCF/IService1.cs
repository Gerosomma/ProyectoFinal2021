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

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        #region LogicaUsuario

        [OperationContract]
        void validarUsuario(Usuario us);

        [OperationContract]
        void AltaUsuario(Usuario usuario, Usuario usLog);

        [OperationContract]
        void BajaUsuario(Usuario usuario, Usuario usLog);

        [OperationContract]
        Usuario BuscarUsuario(string logueo, Usuario usLog);

        [OperationContract]
        Usuario LogueoUsuario(string logueo, string contraseana);

        [OperationContract]
        void ModificarUsuario(Usuario usuario, Usuario usLog);

        [OperationContract]
        List<Empresa> ListarEmpresas(Usuario usuarioLogueado);

        #endregion

        #region LogicaPaquete

        [OperationContract]
        void validarPaquete(Paquete paquete);

        [OperationContract]
        void AltaPaquete(Paquete paquete, Usuario usLog);

        [OperationContract]
        void BajaPaquete(Paquete paquete, Usuario usLog);

        [OperationContract]
        Paquete BuscarPaquete(int codigo, Usuario usLog);

        [OperationContract]
        void ModificarPaquete(Paquete paquete, Usuario usLog);

        #endregion

        #region LogicaSolicitud

        [OperationContract]
        void validarSolicitud(Solicitud solicitud);

        [OperationContract]
        string generaXmlSolicitudes(List<Solicitud> listaSolicitudes);

        [OperationContract]
        void AltaSolicitud(Solicitud solicitud, Usuario usLog);

        [OperationContract]
        void ModificarEstadoSolicitud(Solicitud solicitud, Usuario usLog);

        [OperationContract]
        string listadoSolicitudes(Usuario usLog);

        #endregion
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
