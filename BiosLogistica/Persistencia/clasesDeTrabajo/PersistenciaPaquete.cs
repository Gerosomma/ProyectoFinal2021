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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
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
    }
}
