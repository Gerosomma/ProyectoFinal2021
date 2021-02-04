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
                conexion = new SqlConnection(Conexion.Cnn(usLog));

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
                    case -2:
                        throw new Exception("error -2");
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
        
        public Paquete BuscarPaquete(int codigo, Usuario usLog)
        {
            SqlConnection conexion = null;
            SqlDataReader drPaquete = null;
            Paquete paquete = null;
            Empresa empresa = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(usLog));
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

        internal Paquete interBuscarPaquete(int codigo, SqlConnection conexion)
        {
            Paquete paquete = null;
            Empresa empresa = null;

            try
            {
                SqlCommand cmdBuscarPaquete = new SqlCommand("BuscarPaquete", conexion);
                cmdBuscarPaquete.CommandType = CommandType.StoredProcedure;

                cmdBuscarPaquete.Parameters.AddWithValue("@codigo", codigo);
                
                DataTable dtPaquete = new DataTable("Paquete");
                dtPaquete.Load(cmdBuscarPaquete.ExecuteReader());

                if (dtPaquete.Rows.Count > 0)
                {
                    empresa = PersistenciaEmpresa.getInstancia().interBuscarEmpresa(conexion, dtPaquete.Rows[0]["empresa"].ToString());
                    paquete = new Paquete(Convert.ToInt32(dtPaquete.Rows[0]["codigo"]), dtPaquete.Rows[0]["tipo"].ToString(),
                        dtPaquete.Rows[0]["descripcion"].ToString(), Convert.ToDouble(dtPaquete.Rows[0]["peso"]), empresa);
                }
                /*
                if (drPaquete.Read())
                {
                    empresa = PersistenciaEmpresa.getInstancia().interBuscarEmpresa(conexion, (string)drPaquete["empresa"]);
                    paquete = new Paquete((int)drPaquete["codigo"], (string)drPaquete["tipo"],
                        (string)drPaquete["descripcion"], (double)drPaquete["peso"], empresa);
                }*/

                return paquete;
            }
            catch (Exception ex)
            {
                throw ex;
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

        public List<Paquete> paquetesSolicitud(SqlConnection conexion, int solicitud, Usuario usLog)
        {
            Paquete paquete = null;
            List<Paquete> listaPaquetes = new List<Paquete>();

            try
            {
                SqlCommand cmdListadoSolicitues = new SqlCommand("listadoPaquetesSolicitud", conexion);
                cmdListadoSolicitues.CommandType = CommandType.StoredProcedure;
                cmdListadoSolicitues.Parameters.AddWithValue("@numeroSolicitud", solicitud);
                
                DataTable dtPaquetesSolicitud = new DataTable("PaquetesSolicitud");
                dtPaquetesSolicitud.Load(cmdListadoSolicitues.ExecuteReader());

                foreach (DataRow r in dtPaquetesSolicitud.Rows)
                {
                    paquete = interBuscarPaquete(Convert.ToInt32(r["codigoPaquete"]), conexion);
                    listaPaquetes.Add(paquete);
                }

                /*while (drPaquetes.Read())
                {

                    paquete = interBuscarPaquete((int)drPaquetes["codigoPaquete"], conexion);
                    //paquete = new Paquete((int)drPaquetes["codigo"], (string)drPaquetes["tipo"], (string)drPaquetes["descripcion"],
                    //    (double)drPaquetes["peso"], (Empresa)usLog);
                    listaPaquetes.Add(paquete);
                }*/

                return listaPaquetes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
