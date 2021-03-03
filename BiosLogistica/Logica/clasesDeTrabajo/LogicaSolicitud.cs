using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using Logica.interfaces;
using Persistencia;
using System.Xml;

namespace Logica.clasesDeTrabajo
{
    internal class LogicaSolicitud : ILogicaSolicitud
    {
        private static LogicaSolicitud _instancia = null;
        private LogicaSolicitud() { }
        public static LogicaSolicitud GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new LogicaSolicitud();
            }
            return _instancia;
        }
        

        public int AltaSolicitud(Solicitud solicitud, Empleado usLog)
        {
            if (solicitud.FechaEntrega < DateTime.Today)
                throw new Exception("Fecha de entrega invalida.");

            return FabricaPersistencia.GetPersistenciaSolicitud().AltaSolicitud(solicitud, usLog);
        }

        public void ModificarEstadoSolicitud(Solicitud solicitud, Empleado usLog)
        {
            FabricaPersistencia.GetPersistenciaSolicitud().ModificarEstadoSolicitud(solicitud, usLog);
        }

        public string listadoSolicitudesEnCamino()
        {
            return generaXmlSolicitudes(FabricaPersistencia.GetPersistenciaSolicitud().listadoSolicitudesEnCamino());
        }

        public List<Solicitud> listadoSolicitudesEmpresa(Empresa usLog)
        {
            return FabricaPersistencia.GetPersistenciaSolicitud().listadoSolicitudesEmpresa(usLog);
        }

        public List<Solicitud> listadoSolicitudes(Empleado usLog)
        {
            return FabricaPersistencia.GetPersistenciaSolicitud().listadoSolicitudes(usLog);
        }

        internal static string generaXmlSolicitudes(List<Solicitud> listaSolicitudes)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                XmlElement raiz = doc.CreateElement(string.Empty, "solicitudes", string.Empty);
                doc.AppendChild(raiz);

                foreach (Solicitud s in listaSolicitudes)
                {
                    XmlElement eSolicitud = doc.CreateElement(string.Empty, "solicitud", string.Empty);
                    raiz.AppendChild(eSolicitud);

                    XmlElement nodNumero = doc.CreateElement(string.Empty, "numero", string.Empty);
                    XmlText numeroSolicitud = doc.CreateTextNode(s.Numero.ToString());
                    nodNumero.AppendChild(numeroSolicitud);
                    eSolicitud.AppendChild(nodNumero);

                    XmlElement nodFechaEntrega = doc.CreateElement(string.Empty, "fechaEntrega", string.Empty);
                    XmlText fechaEntrega = doc.CreateTextNode(s.FechaEntrega.ToShortDateString());
                    nodFechaEntrega.AppendChild(fechaEntrega);
                    eSolicitud.AppendChild(nodFechaEntrega);

                    XmlElement nodNombreDestinatario = doc.CreateElement(string.Empty, "nombreDestinatario", string.Empty);
                    XmlText nombreDestinatario = doc.CreateTextNode(s.NombreDestinatario);
                    nodNombreDestinatario.AppendChild(nombreDestinatario);
                    eSolicitud.AppendChild(nodNombreDestinatario);

                    XmlElement nodDireccionDestinatario = doc.CreateElement(string.Empty, "direccionDestinatario", string.Empty);
                    XmlText direccionDestinatario = doc.CreateTextNode(s.DireccionDestinatario);
                    nodDireccionDestinatario.AppendChild(direccionDestinatario);
                    eSolicitud.AppendChild(nodDireccionDestinatario);

                    XmlElement nodEstado = doc.CreateElement(string.Empty, "estado", string.Empty);
                    XmlText estado = doc.CreateTextNode(s.Estado);
                    nodEstado.AppendChild(estado);
                    eSolicitud.AppendChild(nodEstado);

                    XmlElement ePaquetesSolicitud = doc.CreateElement(string.Empty, "paquetesSolicitud", string.Empty);
                    eSolicitud.AppendChild(ePaquetesSolicitud);

                    foreach (Paquete p in s.PaquetesSolicitud)
                    {
                        XmlElement ePaquete = doc.CreateElement(string.Empty, "paquete", string.Empty);
                        eSolicitud.AppendChild(ePaquete);

                        XmlElement nodCodigo = doc.CreateElement(string.Empty, "codigo", string.Empty);
                        XmlText codDoc = doc.CreateTextNode(p.Codigo.ToString());
                        nodCodigo.AppendChild(codDoc);
                        ePaquete.AppendChild(nodCodigo);

                        XmlElement nodTipo = doc.CreateElement(string.Empty, "tipo", string.Empty);
                        XmlText tipo = doc.CreateTextNode(p.Tipo);
                        nodTipo.AppendChild(tipo);
                        ePaquete.AppendChild(nodTipo);

                        XmlElement nodDescripcion = doc.CreateElement(string.Empty, "descripcion", string.Empty);
                        XmlText descripcion = doc.CreateTextNode(p.Descripcion);
                        nodDescripcion.AppendChild(descripcion);
                        ePaquete.AppendChild(nodDescripcion);

                        XmlElement nodPeso = doc.CreateElement(string.Empty, "peso", string.Empty);
                        XmlText peso = doc.CreateTextNode(p.Descripcion);
                        nodPeso.AppendChild(peso);
                        ePaquete.AppendChild(nodPeso);

                        // nodo empresa
                        XmlElement eEmpresa = doc.CreateElement(string.Empty, "empresa", string.Empty);
                        ePaquete.AppendChild(eEmpresa);

                        XmlElement nodLogueo = doc.CreateElement(string.Empty, "logueo", string.Empty);
                        XmlText logueo = doc.CreateTextNode(p.EmpresaOrigen.Logueo);
                        nodLogueo.AppendChild(logueo);
                        eEmpresa.AppendChild(nodLogueo);

                        XmlElement nodNombreCompleto = doc.CreateElement(string.Empty, "nombreCompleto", string.Empty);
                        XmlText nombreCompleto = doc.CreateTextNode(p.EmpresaOrigen.NombreCompleto);
                        nodNombreCompleto.AppendChild(nombreCompleto);
                        eEmpresa.AppendChild(nodNombreCompleto);

                        XmlElement nodTelefono = doc.CreateElement(string.Empty, "telefono", string.Empty);
                        XmlText telefono = doc.CreateTextNode(p.EmpresaOrigen.Telefono);
                        nodTelefono.AppendChild(telefono);
                        eEmpresa.AppendChild(nodTelefono);

                        XmlElement nodDireccion = doc.CreateElement(string.Empty, "direccion", string.Empty);
                        XmlText direccion = doc.CreateTextNode(p.EmpresaOrigen.Direccion);
                        nodDireccion.AppendChild(direccion);
                        eEmpresa.AppendChild(nodDireccion);

                        XmlElement nodEmail = doc.CreateElement(string.Empty, "email", string.Empty);
                        XmlText email = doc.CreateTextNode(p.EmpresaOrigen.Email);
                        nodEmail.AppendChild(email);
                        eEmpresa.AppendChild(nodEmail);

                    }
                }
                return doc.InnerXml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
