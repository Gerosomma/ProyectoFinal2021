using System;
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

        public void AltaSolicitud(Solicitud solicitud, Empleado usLog)
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

                SqlParameter idSolicitud = new SqlParameter("@@IDENTITY", SqlDbType.Int);
                idSolicitud.Direction = ParameterDirection.ReturnValue;
                cmdAltaSolicitud.Parameters.Add(idSolicitud);

                conexion.Open();
                trn = conexion.BeginTransaction();
                cmdAltaSolicitud.Transaction = trn;
                cmdAltaSolicitud.ExecuteNonQuery();

                solicitud.Numero = Convert.ToInt32(idSolicitud.Value);

                switch ((int)valorRetorno.Value)
                {
                    case -1:
                        throw new Exception("El empleado no esta activo.");
                    case -2:
                        throw new Exception("Error al insertar solicitud.");
                }
                
                foreach (Paquete paquete in solicitud.PaquetesSolicitud)
                {
                    PersistenciaPaquete.getInstancia().AltaPaqueteSolicitud(trn, solicitud, paquete); // pasar la transaccion no la conexion.
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
        

        public void ModificarEstadoSolicitud(Solicitud solicitud, Empleado usLog) 
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(usLog));

                SqlCommand cmdModificarSolicitud = new SqlCommand("ModificarEstadoSolicitud", conexion);
                cmdModificarSolicitud.CommandType = CommandType.StoredProcedure;

                cmdModificarSolicitud.Parameters.AddWithValue("@numero", solicitud.Numero);
                cmdModificarSolicitud.Parameters.AddWithValue("@empleado", solicitud.Empleado.Logueo);
                
                SqlParameter valorRetorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdModificarSolicitud.Parameters.Add(valorRetorno);

                conexion.Open();
                cmdModificarSolicitud.ExecuteNonQuery();

                switch ((int)valorRetorno.Value)
                {
                    case -1:
                        throw new Exception("El empleado no exíste o no está activo.");
                    case -2:
                        throw new Exception("Solicitud inexistente.");
                    case -3:
                        throw new Exception("Error al modificar estado.");
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

        public List<Solicitud> listadoSolicitudesEnCamino()
        {
            SqlConnection conexion = null;
            SqlDataReader drSolicitud = null;
            Solicitud solicitud = null;
            List<Solicitud> listaSolicitudes = new List<Solicitud>();
            List<Paquete> listaPaquetes = new List<Paquete>();
            Empleado empleado = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn());
                SqlCommand cmdListadoSolicitues = new SqlCommand("listadoSolicitudesEnCamino", conexion);
                cmdListadoSolicitues.CommandType = CommandType.StoredProcedure;
                
                conexion.Open();
                drSolicitud = cmdListadoSolicitues.ExecuteReader();
                
                while (drSolicitud.Read())
                { 
                    listaPaquetes = PersistenciaPaquete.getInstancia().listadoPaquetesSolicitud((int)drSolicitud["numero"]);
                    empleado = PersistenciaEmpleado.getInstancia().interBuscarEmpleado((string)drSolicitud["empleado"]);
                    solicitud = new Solicitud((int)drSolicitud["numero"], (DateTime)drSolicitud["fechaEntrega"], (string)drSolicitud["nombreDestinatario"],
                        (string)drSolicitud["direccionDestinatario"], (string)drSolicitud["estado"], empleado, listaPaquetes);
                    listaSolicitudes.Add(solicitud);
                }

                return listaSolicitudes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (drSolicitud != null)
                {
                    drSolicitud.Close();
                }
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public List<Solicitud> listadoSolicitudesEmpresa(Empresa usLog)
        {
            SqlConnection conexion = null;
            SqlDataReader drSolicitud = null;
            Solicitud solicitud = null;
            List<Solicitud> listaSolicitudes = new List<Solicitud>();
            List<Paquete> listaPaquetes = new List<Paquete>();
            Empleado empleado = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(usLog));
                SqlCommand cmdListadoSolicitues = new SqlCommand("ListadoSolicitudesEmpresa", conexion);
                cmdListadoSolicitues.CommandType = CommandType.StoredProcedure;

                conexion.Open();
                drSolicitud = cmdListadoSolicitues.ExecuteReader();

                while (drSolicitud.Read())
                {
                    listaPaquetes = PersistenciaPaquete.getInstancia().listadoPaquetesSolicitud((int)drSolicitud["numero"]);
                    empleado = PersistenciaEmpleado.getInstancia().interBuscarEmpleado((string)drSolicitud["empleado"]);
                    solicitud = new Solicitud((int)drSolicitud["numero"], (DateTime)drSolicitud["fechaEntrega"], (string)drSolicitud["nombreDestinatario"],
                        (string)drSolicitud["direccionDestinatario"], (string)drSolicitud["estado"], empleado, listaPaquetes);
                    listaSolicitudes.Add(solicitud);
                }

                return listaSolicitudes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (drSolicitud != null)
                {
                    drSolicitud.Close();
                }
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }
    }
}
