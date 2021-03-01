using System;
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

        public Empresa LoguearEmpresa(string logueo, string contrasena)
        {
            SqlConnection conexion = null;
            SqlDataReader drEmpresa = null;
            Empresa empresa = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn());
                SqlCommand cmdBuscarEpresa = new SqlCommand("LogueoEmpresa", conexion);
                cmdBuscarEpresa.CommandType = CommandType.StoredProcedure;

                cmdBuscarEpresa.Parameters.AddWithValue("@logueo", logueo);
                cmdBuscarEpresa.Parameters.AddWithValue("@contrasena", contrasena);

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
                throw ex;
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

        public Empresa BuscarEmpresa(string logueo, Empleado usLog)
        {
            SqlConnection conexion = null;
            SqlDataReader drEmpresa = null;
            Empresa empresa = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(usLog));
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
                throw ex;
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

        internal Empresa interBuscarEmpresa(string logueo)
        {
            SqlConnection conexion = null;
            SqlDataReader drEmpresa = null;
            Empresa empresa = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn());
                SqlCommand cmdBuscarEpresa = new SqlCommand("interBuscarEmpresa", conexion);
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
                throw ex;
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

        public void AltaEmpresa(Empresa empresa, Empleado usLog)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(usLog));

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
                    case -1:
                        throw new Exception("Nombre de usuario existente y activo.");
                    case -2:
                        throw new Exception("Ocurrió un error al insertar usuario.");
                    case -3:
                        throw new Exception("Ocurrió un error al insertar empresa.");
                    case -4:
                        throw new Exception("Ocurrio un error al crear usuario servidor.");
                    case -5:
                        throw new Exception("Ocurrió un error al crear usuario base de datos.");
                    case -6:
                        throw new Exception("Ocurrió un error al asignar permisos usuario.");

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
        public void ModificarEmpresa(Empresa empresa, Empleado usLog)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(usLog));

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
                    case -2:
                        throw new Exception("No se encontro empresa activa.");
                    case -3:
                        throw new Exception("Ocurrió un error al modificar el usuario.");
                    case -4:
                        throw new Exception("Ocurrió un error al modificar el empresa.");
                    case -5:
                        throw new Exception("Ocurrió un error al modificar contraseña de usuario.");
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

        public void BajaEmpresa(Empresa empresa, Empleado usLog)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(usLog));

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
                        throw new Exception("No se encontro empresa activa.");
                    case -2:
                        throw new Exception("Ocurrió un error al eliminar el usuario.");
                    case -3:
                        throw new Exception("Ocurrió un error al eliminar la empresa.");
                    case -4:
                        throw new Exception("Ocurrió un error al eliminar el usuario de base de datos.");
                    case -5:
                        throw new Exception("Ocurrió un error al eliminar la usuario del servidor.");
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

        public List<Empresa> ListarEmpresas(Empleado usLog)
        {
            SqlConnection conexion = null;
            SqlDataReader drEmpresas = null;
            List<Empresa> empresas = new List<Empresa>();
            Empresa empresa = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(usLog));

                SqlCommand cmdListarEmpresas = new SqlCommand("ListarEmpresas", conexion);
                cmdListarEmpresas.CommandType = CommandType.StoredProcedure;

                conexion.Open();
                drEmpresas = cmdListarEmpresas.ExecuteReader();

                while (drEmpresas.Read())
                {
                    empresa = new Empresa((string)drEmpresas["logueo"], (string)drEmpresas["contrasena"], (string)drEmpresas["nombreCompleto"], (string)drEmpresas["telefono"],
                        (string)drEmpresas["direccion"], (string)drEmpresas["email"]);
                    empresas.Add(empresa);
                }

                return empresas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (drEmpresas != null)
                {
                    drEmpresas.Close();
                }
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }
    }
}
