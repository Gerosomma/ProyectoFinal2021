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
    internal class PersistenciaPaquete : IPersistenciaPaquete
    {
        private static PersistenciaPaquete _instancia = null;

        private PersistenciaPaquete() { }

        public static PersistenciaPaquete getInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new PersistenciaPaquete();
            }
            return _instancia;
        }

        public void AltaPaquete(Paquete paquete, Usuario usLog)
        {
            SqlConnection conexion = null;
            
            try
            {
                conexion = new SqlConnection(Conexion.ObtenerCadenaConexion(usLog.Logueo, usLog.Contrasena));

                SqlCommand cmdAltaPaquete = new SqlCommand("AltaPaquete", conexion);
                cmdAltaPaquete.CommandType = CommandType.StoredProcedure;

                cmdAltaPaquete.Parameters.AddWithValue("@codigo", paquete.Codigo);
                cmdAltaPaquete.Parameters.AddWithValue("@tipo", paquete.Tipo);
                cmdAltaPaquete.Parameters.AddWithValue("@descripcion", paquete.Descripcion);
                cmdAltaPaquete.Parameters.AddWithValue("@peso", paquete.Peso);
                cmdAltaPaquete.Parameters.AddWithValue("@empresa", paquete.EmpresaOrigen.Logueo);

                SqlParameter valorRetorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdAltaPaquete.Parameters.Add(valorRetorno);

                conexion.Open();

                cmdAltaPaquete.ExecuteNonQuery();

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

        public void BajaPaquete(Paquete paquete, Usuario usLog)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.ObtenerCadenaConexion(usLog.Logueo, usLog.Contrasena));

                SqlCommand cmdAltaPaquete = new SqlCommand("BajaPaquete", conexion);
                cmdAltaPaquete.CommandType = CommandType.StoredProcedure;

                cmdAltaPaquete.Parameters.AddWithValue("@codigo", paquete.Codigo);

                SqlParameter valorRetorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdAltaPaquete.Parameters.Add(valorRetorno);

                conexion.Open();

                cmdAltaPaquete.ExecuteNonQuery();

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

        public Paquete BuscarPaquete(int codigo, Usuario usLog)
        {
            SqlConnection conexion = null;
            SqlDataReader drPaquete = null;
            Paquete paquete = null;
            Empresa empresa = null;

            try
            {
                conexion = new SqlConnection(Conexion.ObtenerCadenaConexion(usLog.Logueo, usLog.Contrasena));
                SqlCommand cmdBuscarPaquete = new SqlCommand("BuscarPaquete", conexion);
                cmdBuscarPaquete.CommandType = CommandType.StoredProcedure;

                cmdBuscarPaquete.Parameters.AddWithValue("@codigo", codigo);

                conexion.Open();
                drPaquete = cmdBuscarPaquete.ExecuteReader();
                if (drPaquete.Read())
                {
                    empresa = PersistenciaEmpresa.getInstancia().interBuscarEmpresa(conexion, (string)drPaquete["empresa"]);
                    paquete = new Paquete((int)drPaquete["codigo"], (string)drPaquete["tipo"],
                        (string)drPaquete["descripcion"], (double)drPaquete["peso"], empresa);
                }

                return paquete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (drPaquete != null)
                {
                    drPaquete.Close();
                }
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public Paquete interBuscarPaquete(int codigo, SqlConnection conexion)
        {
            SqlDataReader drPaquete = null;
            Paquete paquete = null;
            Empresa empresa = null;

            try
            {
                SqlCommand cmdBuscarPaquete = new SqlCommand("BuscarPaquete", conexion);
                cmdBuscarPaquete.CommandType = CommandType.StoredProcedure;

                cmdBuscarPaquete.Parameters.AddWithValue("@codigo", codigo);
                
                drPaquete = cmdBuscarPaquete.ExecuteReader();
                if (drPaquete.Read())
                {
                    empresa = PersistenciaEmpresa.getInstancia().interBuscarEmpresa(conexion, (string)drPaquete["empresa"]);
                    paquete = new Paquete((int)drPaquete["codigo"], (string)drPaquete["tipo"],
                        (string)drPaquete["descripcion"], (double)drPaquete["peso"], empresa);
                }

                return paquete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (drPaquete != null)
                {
                    drPaquete.Close();
                }
            }
        }

        public void ModificarPaquete(Paquete paquete, Usuario usLog)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.ObtenerCadenaConexion(usLog.Logueo, usLog.Contrasena));

                SqlCommand cmdAltaPaquete = new SqlCommand("ModificarPaquete", conexion);
                cmdAltaPaquete.CommandType = CommandType.StoredProcedure;

                cmdAltaPaquete.Parameters.AddWithValue("@codigo", paquete.Codigo);
                cmdAltaPaquete.Parameters.AddWithValue("@tipo", paquete.Tipo);
                cmdAltaPaquete.Parameters.AddWithValue("@descripcion", paquete.Descripcion);
                cmdAltaPaquete.Parameters.AddWithValue("@peso", paquete.Peso);
                cmdAltaPaquete.Parameters.AddWithValue("@empresa", paquete.EmpresaOrigen.Logueo);

                SqlParameter valorRetorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdAltaPaquete.Parameters.Add(valorRetorno);

                conexion.Open();

                cmdAltaPaquete.ExecuteNonQuery();

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

        internal static void AltaPaqueteSolicitud(SqlConnection conexion, Solicitud solicitud, Paquete paquete, Usuario usLog)
        {
            try
            {
                SqlCommand cmdAltaPaquete = new SqlCommand("AltaPaqueteSolicitud", conexion);
                cmdAltaPaquete.CommandType = CommandType.StoredProcedure;

                cmdAltaPaquete.Parameters.AddWithValue("@codigoPaquete", paquete.Codigo);
                cmdAltaPaquete.Parameters.AddWithValue("@numeroSolicitud", solicitud.Numero);

                SqlParameter valorRetorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdAltaPaquete.Parameters.Add(valorRetorno);

                conexion.Open();

                cmdAltaPaquete.ExecuteNonQuery();

                switch ((int)valorRetorno.Value)
                {
                    case -1:
                        throw new Exception("error -1");
                        break;
                    case -2:
                        throw new Exception("error -2");
                        break;
                    case -3:
                        throw new Exception("error -3");
                        break;
                    case -4:
                        throw new Exception("error -4");
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

        public List<Paquete> paquetesSolicitud(SqlConnection conexion, int solicitud, Usuario usLog)
        {
            SqlDataReader drPaquetes = null;
            Paquete paquete = null;
            List<Paquete> listaPaquetes = new List<Paquete>();

            try
            {
                SqlCommand cmdListadoSolicitues = new SqlCommand("listadoPaquetesSolicitud", conexion);
                cmdListadoSolicitues.CommandType = CommandType.StoredProcedure;
                cmdListadoSolicitues.Parameters.AddWithValue("@numeroSolicitud", solicitud);
                
                drPaquetes = cmdListadoSolicitues.ExecuteReader();
                while (drPaquetes.Read())
                {

                    paquete = interBuscarPaquete((int)drPaquetes["codigoPaquete"], conexion);
                    //paquete = new Paquete((int)drPaquetes["codigo"], (string)drPaquetes["tipo"], (string)drPaquetes["descripcion"],
                    //    (double)drPaquetes["peso"], (Empresa)usLog);
                    listaPaquetes.Add(paquete);
                }

                return listaPaquetes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (drPaquetes != null)
                {
                    drPaquetes.Close();
                }
            }
        }

    }
}
