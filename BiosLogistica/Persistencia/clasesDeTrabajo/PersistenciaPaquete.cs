﻿using System;
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
                        throw new Exception("La empresa origen no esta activa o no existe.");
                    case -2:
                        throw new Exception("Se encontro otro paquete con el mismo codigo de paquete.");
                    case -3:
                        throw new Exception("Ocurrio un error al insertar paquete.");
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
                    empresa = PersistenciaEmpresa.getInstancia().interBuscarEmpresa((string)drPaquete["empresa"]);
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
                    case -2:
                        throw new Exception("error -2");
                    case -3:
                        throw new Exception("error -3");
                    case -4:
                        throw new Exception("error -4");
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

        public List<Paquete> paquetesSolicitud(int solicitud)
        {
            Paquete paquete = null;
            Empresa empresa = null; 
            List<Paquete> listaPaquetes = new List<Paquete>();
            SqlConnection conexion = null;
            SqlDataReader drPaquete = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(null));
                SqlCommand cmdListadoSolicitues = new SqlCommand("ListarPaquetesSolicitud", conexion);
                cmdListadoSolicitues.CommandType = CommandType.StoredProcedure;
                cmdListadoSolicitues.Parameters.AddWithValue("@numeroSolicitud", solicitud);

                /*DataTable dtPaquetesSolicitud = new DataTable("PaquetesSolicitud");
                dtPaquetesSolicitud.Load(cmdListadoSolicitues.ExecuteReader());*/

                conexion.Open();
                drPaquete = cmdListadoSolicitues.ExecuteReader();
                while (drPaquete.Read())
                {
                    empresa = PersistenciaEmpresa.getInstancia().interBuscarEmpresa((string)drPaquete["empresa"]);
                    paquete = new Paquete((int)drPaquete["codigo"], (string)drPaquete["tipo"], (string)drPaquete["descripcion"], (double)drPaquete["peso"], empresa);
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
