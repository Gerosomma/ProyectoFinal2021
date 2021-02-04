﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using Persistencia.interfaces;
using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace Persistencia
{
    internal class PersistenciaSolicitud : IPersistenciaSolicitud
    {
        private static PersistenciaSolicitud _instancia = null;

        private PersistenciaSolicitud() { }

        public static PersistenciaSolicitud getInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new PersistenciaSolicitud();
            }
            return _instancia;
        }

        public void AltaSolicitud(Solicitud solicitud, Usuario usLog)
        {
            SqlConnection conexion = null;
            SqlTransaction trn = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(usLog));

                SqlCommand cmdAltaSolicitud = new SqlCommand("AltaSolicitud", conexion);
                cmdAltaSolicitud.CommandType = CommandType.StoredProcedure;

                cmdAltaSolicitud.Parameters.AddWithValue("@fechaEntrega", solicitud.FechaEntrega);
                cmdAltaSolicitud.Parameters.AddWithValue("@nombreDestinatario", solicitud.NombreDestinatario);
                cmdAltaSolicitud.Parameters.AddWithValue("@direccionDestinatario", solicitud.DireccionDestinatario);
                cmdAltaSolicitud.Parameters.AddWithValue("@estado", solicitud.Estado);
                cmdAltaSolicitud.Parameters.AddWithValue("@empleado", solicitud.Empleado.Logueo);

                SqlParameter valorRetorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdAltaSolicitud.Parameters.Add(valorRetorno);
                //tenemos que retornar el idsolicitud generado.
                SqlParameter idSolicitud = new SqlParameter("@@IDENTITY", SqlDbType.Int);
                idSolicitud.Direction = ParameterDirection.ReturnValue;
                cmdAltaSolicitud.Parameters.Add(idSolicitud);

                conexion.Open();

                trn = conexion.BeginTransaction();
                cmdAltaSolicitud.ExecuteNonQuery();

                solicitud.Numero = Convert.ToInt32(idSolicitud.Value);

                switch ((int)valorRetorno.Value)
                {
                    case -1:
                        throw new Exception("error -1");
                        break;
                    case -2:
                        throw new Exception("error -2");
                        break;
                }

                //deberiamos insertar los paquetes de la solicitud.
                foreach (Paquete paquete in solicitud.PaquetesSolicitud)
                {
                    PersistenciaPaquete.AltaPaqueteSolicitud(conexion, solicitud, paquete, usLog); // pasar la transaccion no la conexion.
                }
                
                trn.Commit();

            }
            catch (Exception ex)
            {
                trn.Rollback();
                throw ex;
            }
            finally
            {
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public void BajaSolicitud(Solicitud solicitud, Usuario usLog)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(usLog));

                SqlCommand cmdBajaSolicitud = new SqlCommand("BajaSolicitud", conexion);
                cmdBajaSolicitud.CommandType = CommandType.StoredProcedure;

                cmdBajaSolicitud.Parameters.AddWithValue("@numero", solicitud.Numero);

                SqlParameter valorRetorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdBajaSolicitud.Parameters.Add(valorRetorno);

                conexion.Open();

                cmdBajaSolicitud.ExecuteNonQuery();

                switch ((int)valorRetorno.Value)
                {
                    case -1:
                        throw new Exception("error -1");
                        break;
                    case -2:
                        throw new Exception("error -2");
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public Solicitud BuscarSolicitud(int codigo, Usuario usLog)
        {
            throw new NotImplementedException();
        }

        public void ModificarSolicitud(Solicitud solicitud, Usuario usLog) // es ModificarEstado
        {
            SqlConnection conexion = null;
            SqlTransaction trn = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(usLog));

                SqlCommand cmdModificarSolicitud = new SqlCommand("ModificarEstadoSolicitud", conexion);
                cmdModificarSolicitud.CommandType = CommandType.StoredProcedure;

                cmdModificarSolicitud.Parameters.AddWithValue("@numero", solicitud.Numero);

                SqlParameter valorRetorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdModificarSolicitud.Parameters.Add(valorRetorno);

                conexion.Open();

                trn = conexion.BeginTransaction();
                cmdModificarSolicitud.ExecuteNonQuery();

                switch ((int)valorRetorno.Value)
                {
                    case -1:
                        throw new Exception("error -1");
                        break;
                    case -2:
                        throw new Exception("error -2");
                        break;
                }
                

                trn.Commit();

            }
            catch (Exception ex)
            {
                trn.Rollback();
                throw ex;
            }
            finally
            {
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public string listadoSolicitudes(Usuario usLog)
        {
            XmlDocument doc = new XmlDocument();
            List<Solicitud> listaSolicitudes = new List<Solicitud>();

            try
            {
                listaSolicitudes = solicitudes(usLog);

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

        internal List<Solicitud> solicitudes(Usuario usLog)
        {
            SqlConnection conexion = null;
            Solicitud solicitud = null;
            List<Solicitud> listaSolicitudes = new List<Solicitud>();
            List<Paquete> listaPaquetes = new List<Paquete>();

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(usLog));
                SqlCommand cmdListadoSolicitues = new SqlCommand("listadoSolicitudes", conexion);
                cmdListadoSolicitues.CommandType = CommandType.StoredProcedure;

                //este data table no va, el problema qe tuve en las conexiones se resuelve cambiando la conexion para las busquedas internas.
                conexion.Open();
                DataTable dtSolicitures = new DataTable("Solicitud");
                dtSolicitures.Load(cmdListadoSolicitues.ExecuteReader());

                foreach (DataRow r in dtSolicitures.Rows)
                {
                    listaPaquetes = PersistenciaPaquete.getInstancia().paquetesSolicitud(conexion, Convert.ToInt32(r["numero"]), usLog);
                    solicitud = new Solicitud(Convert.ToInt32(r["numero"]), Convert.ToDateTime(r["fechaEntrega"]), r["nombreDestinatario"].ToString(),
                        r["direccionDestinatario"].ToString(), r["estado"].ToString(), (Empleado)usLog, listaPaquetes);
                    listaSolicitudes.Add(solicitud);
                }

                /*while (drSolicitud.Read())
                { 
                    listaPaquetes = PersistenciaPaquete.getInstancia().paquetesSolicitud(conexion, (int)drSolicitud["numero"], usLog);
                    solicitud = new Solicitud((int)drSolicitud["numero"], (DateTime)drSolicitud["fechaEntrega"], (string)drSolicitud["nombreDestinatario"],
                        (string)drSolicitud["direccionDestinatario"], (string)drSolicitud["estado"], (Empleado)usLog, listaPaquetes);
                    listaSolicitudes.Add(solicitud);
                }*/

                return listaSolicitudes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }
    }
}
