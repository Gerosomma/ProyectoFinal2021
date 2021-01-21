using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using Persistencia.interfaces;
using System.Data.SqlClient;
using System.Data;

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
                conexion = new SqlConnection(Conexion.ObtenerCadenaConexion(usLog.Logueo, usLog.Contrasena));

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

                conexion.Open();

                trn = conexion.BeginTransaction();
                cmdAltaSolicitud.ExecuteNonQuery();

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

                trn.Commit();

            }
            catch (Exception ex)
            {
                trn.Rollback();
                throw new Exception(ex.Message);
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
                conexion = new SqlConnection(Conexion.ObtenerCadenaConexion(usLog.Logueo, usLog.Contrasena));

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
                throw new Exception(ex.Message);
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

        public void ModificarSolicitud(Solicitud solicitud, Usuario usLog)
        {
            SqlConnection conexion = null;
            SqlTransaction trn = null;

            try
            {
                conexion = new SqlConnection(Conexion.ObtenerCadenaConexion(usLog.Logueo, usLog.Contrasena));

                SqlCommand cmdModificarSolicitud = new SqlCommand("ModificarSolicitud", conexion);
                cmdModificarSolicitud.CommandType = CommandType.StoredProcedure;

                cmdModificarSolicitud.Parameters.AddWithValue("@numero", solicitud.Numero);
                cmdModificarSolicitud.Parameters.AddWithValue("@fechaEntrega", solicitud.FechaEntrega);
                cmdModificarSolicitud.Parameters.AddWithValue("@nombreDestinatario", solicitud.NombreDestinatario);
                cmdModificarSolicitud.Parameters.AddWithValue("@direccionDestinatario", solicitud.DireccionDestinatario);
                cmdModificarSolicitud.Parameters.AddWithValue("@estado", solicitud.Estado);
                cmdModificarSolicitud.Parameters.AddWithValue("@empleado", solicitud.Empleado.Logueo);

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

                //deberiamos modificar los paquetes de la solicitud.

                trn.Commit();

            }
            catch (Exception ex)
            {
                trn.Rollback();
                throw new Exception(ex.Message);
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
