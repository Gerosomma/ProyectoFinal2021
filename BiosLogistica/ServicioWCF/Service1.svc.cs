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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        #region LogicaUsuario
        void IService1.validarUsuario(Usuario us)
        {
            //FabricaLogica.GetLogicaUsuario().
        } 
        
        void IService1.AltaUsuario(Usuario usuario, Usuario usLog)
        {
            FabricaLogica.GetLogicaUsuario().AltaUsuario(usuario, usLog);
        }
        
        void IService1.BajaUsuario(Usuario usuario, Usuario usLog)
        {
            FabricaLogica.GetLogicaUsuario().BajaUsuario(usuario, usLog);
        }
        
        Usuario IService1.BuscarUsuario(string logueo, Usuario usLog)
        {
            return FabricaLogica.GetLogicaUsuario().BuscarUsuario(logueo, usLog);
        }
        
        Usuario IService1.LogueoUsuario(string logueo, string contraseana)
        {
            return FabricaLogica.GetLogicaUsuario().LogueoUsuario(logueo, contraseana);
        }
        
        void IService1.ModificarUsuario(Usuario usuario, Usuario usLog)
        {
            FabricaLogica.GetLogicaUsuario().ModificarUsuario(usuario, usLog);
        }
        
        List<Empresa> IService1.ListarEmpresas(Usuario usuarioLogueado)
        {
            return FabricaLogica.GetLogicaUsuario().ListarEmpresas(usuarioLogueado);
        }

        #endregion

        #region LogicaPaquete

        void IService1.validarPaquete(Paquete paquete)
        {
            //FabricaLogica.GetLogicaPaquete().
        }

        void IService1.AltaPaquete(Paquete paquete, Usuario usLog)
        {
            FabricaLogica.GetLogicaPaquete().AltaPaquete(paquete, usLog);
        }

        void IService1.BajaPaquete(Paquete paquete, Usuario usLog)
        {
            FabricaLogica.GetLogicaPaquete().BajaPaquete(paquete, usLog);
        }

        Paquete IService1.BuscarPaquete(int codigo, Usuario usLog)
        {
            return FabricaLogica.GetLogicaPaquete().BuscarPaquete(codigo, usLog);
        }

        void IService1.ModificarPaquete(Paquete paquete, Usuario usLog)
        {
            FabricaLogica.GetLogicaPaquete().ModificarPaquete(paquete, usLog);
        }

        #endregion

        #region LogicaSolicitud

        void IService1.validarSolicitud(Solicitud solicitud)
        {
            //FabricaLogica.GetLogicaSolicitud().
        }

        string IService1.generaXmlSolicitudes(List<Solicitud> listaSolicitudes)
        {
            //return FabricaLogica.GetLogicaSolicitud().
        } 

        void IService1.AltaSolicitud(Solicitud solicitud, Usuario usLog)
        {
            FabricaLogica.GetLogicaSolicitud().AltaSolicitud(solicitud, usLog);
        }

        void IService1.ModificarEstadoSolicitud(Solicitud solicitud, Usuario usLog)
        {
            FabricaLogica.GetLogicaSolicitud().ModificarEstadoSolicitud(solicitud, usLog);
        }

        string IService1.listadoSolicitudes(Usuario usLog)
        {
            return FabricaLogica.GetLogicaSolicitud().listadoSolicitudes(usLog);
        }

        #endregion
    }
}
