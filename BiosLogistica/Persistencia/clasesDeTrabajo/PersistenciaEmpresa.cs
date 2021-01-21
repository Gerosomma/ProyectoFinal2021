﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia.interfaces;
using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class PersistenciaEmpresa : IPersistenciaEmpresa
    {
        private static PersistenciaEmpresa _instancia = null;

        private PersistenciaEmpresa() { }

        public static PersistenciaEmpresa getInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new PersistenciaEmpresa();
            }
            return _instancia;
        }

        public Empresa LoguearEmpresa(string logueo, string contrasenia)
        {
            SqlConnection conexion = null;
            SqlDataReader drEmpresa = null;
            Empresa empresa = null;

            try
            {
                conexion = new SqlConnection(Conexion.ObtenerCadenaConexion(logueo, contrasenia));
                SqlCommand cmdBuscarEpresa = new SqlCommand("BuscarEmpresa", conexion);
                cmdBuscarEpresa.CommandType = CommandType.StoredProcedure;

                cmdBuscarEpresa.Parameters.AddWithValue("@logueo", logueo);

                conexion.Open();
                drEmpresa = cmdBuscarEpresa.ExecuteReader();

                if (drEmpresa.Read())
                {
                    empresa = new Empresa((string)drEmpresa["logueo"], (string)drEmpresa["contrasena"], (string)drEmpresa["nombreCompleto"], (string)drEmpresa["telefono"], (string)drEmpresa["direccion"], (string)drEmpresa["email"]);
                }
                return empresa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (drEmpresa != null)
                {
                    drEmpresa.Close();
                }
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public Empresa BuscarEmpresa(string logueo, Usuario usLog)
        {
            SqlConnection conexion = null;
            SqlDataReader drEmpresa = null;
            Empresa empresa = null;

            try
            {
                conexion = new SqlConnection(Conexion.ObtenerCadenaConexion(usLog.Logueo, usLog.Contrasena));
                SqlCommand cmdBuscarEpresa = new SqlCommand("BuscarEmpresa", conexion);
                cmdBuscarEpresa.CommandType = CommandType.StoredProcedure;

                cmdBuscarEpresa.Parameters.AddWithValue("@logueo", logueo);

                conexion.Open();
                drEmpresa = cmdBuscarEpresa.ExecuteReader();

                if (drEmpresa.Read())
                {
                    empresa = new Empresa((string)drEmpresa["logueo"], (string)drEmpresa["contrasena"], (string)drEmpresa["nombreCompleto"], (string)drEmpresa["telefono"], (string)drEmpresa["direccion"], (string)drEmpresa["email"]);
                }
                return empresa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (drEmpresa != null)
                {
                    drEmpresa.Close();
                }
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public Empresa interBuscarEmpresa(SqlConnection conexion, string logueo)
        {
            SqlDataReader drEmpresa = null;
            Empresa empresa = null;

            try
            {
                SqlCommand cmdBuscarEpresa = new SqlCommand("BuscarEmpresa", conexion);
                cmdBuscarEpresa.CommandType = CommandType.StoredProcedure;

                cmdBuscarEpresa.Parameters.AddWithValue("@logueo", logueo);

                conexion.Open();
                drEmpresa = cmdBuscarEpresa.ExecuteReader();

                if (drEmpresa.Read())
                {
                    empresa = new Empresa((string)drEmpresa["logueo"], (string)drEmpresa["contrasena"], (string)drEmpresa["nombreCompleto"], (string)drEmpresa["telefono"], (string)drEmpresa["direccion"], (string)drEmpresa["email"]);
                }
                return empresa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (drEmpresa != null)
                {
                    drEmpresa.Close();
                }
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public void AltaEmpresa(Empresa empresa, Usuario usLog)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.ObtenerCadenaConexion(usLog.Logueo, usLog.Contrasena));

                SqlCommand cmdAltaEmpresa = new SqlCommand("AltaEmpresa", conexion);
                cmdAltaEmpresa.CommandType = CommandType.StoredProcedure;

                cmdAltaEmpresa.Parameters.AddWithValue("@logueo", empresa.Logueo);
                cmdAltaEmpresa.Parameters.AddWithValue("@contrasena", empresa.Contrasena);
                cmdAltaEmpresa.Parameters.AddWithValue("@nombreCompleto", empresa.NombreCompleto);
                cmdAltaEmpresa.Parameters.AddWithValue("@telefono", empresa.Telefono);
                cmdAltaEmpresa.Parameters.AddWithValue("@direccion", empresa.Direccion);
                cmdAltaEmpresa.Parameters.AddWithValue("@email", empresa.Email);

                SqlParameter valorRetorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdAltaEmpresa.Parameters.Add(valorRetorno);

                conexion.Open();
                cmdAltaEmpresa.ExecuteNonQuery();

                switch ((int)valorRetorno.Value)
                {

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
        public void ModificarEmpresa(Empresa empresa, Usuario empLog)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.ObtenerCadenaConexion(empLog.Logueo, empLog.Contrasena));

                SqlCommand cmdModificarEmpresa = new SqlCommand("ModificarEmpresa", conexion);
                cmdModificarEmpresa.CommandType = CommandType.StoredProcedure;

                cmdModificarEmpresa.Parameters.AddWithValue("@logueo", empresa.Logueo);
                cmdModificarEmpresa.Parameters.AddWithValue("@contrasena", empresa.Contrasena);
                cmdModificarEmpresa.Parameters.AddWithValue("@nombreCompleto", empresa.NombreCompleto);
                cmdModificarEmpresa.Parameters.AddWithValue("@telefono", empresa.Telefono);
                cmdModificarEmpresa.Parameters.AddWithValue("@direccion", empresa.Direccion);
                cmdModificarEmpresa.Parameters.AddWithValue("@email", empresa.Email);

                SqlParameter valorRetorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdModificarEmpresa.Parameters.Add(valorRetorno);

                conexion.Open();
                cmdModificarEmpresa.ExecuteNonQuery();

                switch ((int)valorRetorno.Value)
                {
                    case -1:
                        throw new Exception("El empleado no existe.");
                        break;
                    case -2:
                        throw new Exception("Ocurrió un error al modificar el usuario.");
                        break;
                    case -3:
                        throw new Exception("Ocurrió un error al modificar el empleado.");
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

        public void BajaEmpresa(Empresa empresa, Usuario empLog)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.ObtenerCadenaConexion(empLog.Logueo, empLog.Contrasena));

                SqlCommand cmdBajaEmpresa = new SqlCommand("BajaEmpresa", conexion);
                cmdBajaEmpresa.CommandType = CommandType.StoredProcedure;

                cmdBajaEmpresa.Parameters.AddWithValue("@logueo", empresa.Logueo);

                SqlParameter valorRetorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdBajaEmpresa.Parameters.Add(valorRetorno);

                conexion.Open();
                cmdBajaEmpresa.ExecuteNonQuery();

                switch ((int)valorRetorno.Value)
                {
                    case -1:
                        throw new Exception("El empleado no existe.");
                        break;
                    case -2:
                        throw new Exception("Ocurrió un error al eliminar el empleado.");
                        break;
                    case -3:
                        throw new Exception("Ocurrió un error al eliminar el usuario.");
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