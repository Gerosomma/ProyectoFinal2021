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
    internal class PersistenciaEmpleado : IPersistenciaEmpleado
    {
        private static PersistenciaEmpleado _instancia = null;

        private PersistenciaEmpleado() { }

        public static PersistenciaEmpleado getInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new PersistenciaEmpleado();
            }
            return _instancia;
        }

        public Empleado LoguearEmpleado(string logueo, string contrasena)
        {
            SqlConnection conexion = null;
            SqlDataReader drEmpleado = null;
            Empleado empleado = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(null));
                SqlCommand cmdBuscarEmpresa = new SqlCommand("LogueoEmpleado", conexion);
                cmdBuscarEmpresa.CommandType = CommandType.StoredProcedure;

                cmdBuscarEmpresa.Parameters.AddWithValue("@logueo", logueo);
                cmdBuscarEmpresa.Parameters.AddWithValue("@contrasena", contrasena);

                conexion.Open();
                drEmpleado = cmdBuscarEmpresa.ExecuteReader();

                if (drEmpleado.Read())
                {
                    empleado = new Empleado((string)drEmpleado["logueo"], (string)drEmpleado["contrasena"], (string)drEmpleado["nombreCompleto"], Convert.ToString((TimeSpan)drEmpleado["horaInicio"]), Convert.ToString((TimeSpan)drEmpleado["horaFin"]));
                }
                return empleado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (drEmpleado != null)
                {
                    drEmpleado.Close();
                }
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public Empleado BuscarEmpleado(string logueo, Usuario usLog)
        {
            SqlConnection conexion = null;
            SqlDataReader drEmpleado = null;
            Empleado empleado = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(usLog));
                SqlCommand cmdBuscarEmpresa = new SqlCommand("BuscarEmpleado", conexion);
                cmdBuscarEmpresa.CommandType = CommandType.StoredProcedure;

                cmdBuscarEmpresa.Parameters.AddWithValue("@logueo", logueo);

                conexion.Open();
                drEmpleado = cmdBuscarEmpresa.ExecuteReader();

                if (drEmpleado.Read())
                {
                    empleado = new Empleado((string)drEmpleado["logueo"], (string)drEmpleado["contrasena"], (string)drEmpleado["nombreCompleto"], Convert.ToString((TimeSpan)drEmpleado["horaInicio"]), Convert.ToString((TimeSpan)drEmpleado["horaFin"]));
                }
                return empleado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (drEmpleado != null)
                {
                    drEmpleado.Close();
                }
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        internal Empleado interBuscarEmpleado(string logueo)
        {
            SqlConnection conexion = null;
            SqlDataReader drEmpleado = null;
            Empleado empleado = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(null));
                SqlCommand cmdBuscarEmpresa = new SqlCommand("interBuscarEmpleado", conexion);
                cmdBuscarEmpresa.CommandType = CommandType.StoredProcedure;

                cmdBuscarEmpresa.Parameters.AddWithValue("@logueo", logueo);

                conexion.Open();
                drEmpleado = cmdBuscarEmpresa.ExecuteReader();

                if (drEmpleado.Read())
                {
                    empleado = new Empleado((string)drEmpleado["logueo"], (string)drEmpleado["contrasena"], (string)drEmpleado["nombreCompleto"], Convert.ToString((TimeSpan)drEmpleado["horaInicio"]), Convert.ToString((TimeSpan)drEmpleado["horaFin"]));
                }
                return empleado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (drEmpleado != null)
                {
                    drEmpleado.Close();
                }
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public void AltaEmpleado(Empleado empleado, Usuario usLog)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(usLog));
                //conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdAltaEmpleado = new SqlCommand("AltaEmpleado", conexion);
                cmdAltaEmpleado.CommandType = CommandType.StoredProcedure;

                cmdAltaEmpleado.Parameters.AddWithValue("@logueo", empleado.Logueo);
                cmdAltaEmpleado.Parameters.AddWithValue("@contrasena", empleado.Contrasena);
                cmdAltaEmpleado.Parameters.AddWithValue("@nombreCompleto", empleado.NombreCompleto);
                cmdAltaEmpleado.Parameters.AddWithValue("@horaInicio", empleado.HoraInicio);
                cmdAltaEmpleado.Parameters.AddWithValue("@horaFin", empleado.HoraFin);

                SqlParameter valorRetorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdAltaEmpleado.Parameters.Add(valorRetorno);

                conexion.Open();
                cmdAltaEmpleado.ExecuteNonQuery();

                switch ((int)valorRetorno.Value)
                {
                    case -1:
                        throw new Exception("Nombre de usuario existente y activo");
                    case -2:
                        throw new Exception("Ocurrió un error al insertar usuario.");
                    case -3:
                        throw new Exception("Ocurrió un error al insertar empleado.");
                    case -4:
                        throw new Exception("Ocurrió un error al crear usuario servidor.");
                    case -5:
                        throw new Exception("Ocurrió un error al crear usuario base de datos.");
                    case -6:
                        throw new Exception("Ocurrió un error al asignar permisos usuario.");
                    case -7:
                        throw new Exception("Ocurrió un error al asignar permisos usuario.");
                    case -8:
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
        public void ModificarEmpleado(Empleado empleado, Usuario usLog)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(usLog));

                SqlCommand cmdModificarEmpleado = new SqlCommand("ModificarEmpleado", conexion);
                cmdModificarEmpleado.CommandType = CommandType.StoredProcedure;

                cmdModificarEmpleado.Parameters.AddWithValue("@usLog", usLog.Logueo); //esta logica se podria validar aquí
                cmdModificarEmpleado.Parameters.AddWithValue("@logueo", empleado.Logueo);
                cmdModificarEmpleado.Parameters.AddWithValue("@contrasena", empleado.Contrasena);
                cmdModificarEmpleado.Parameters.AddWithValue("@nombreCompleto", empleado.NombreCompleto);
                cmdModificarEmpleado.Parameters.AddWithValue("@horaInicio", empleado.HoraInicio);
                cmdModificarEmpleado.Parameters.AddWithValue("@horaFin", empleado.HoraFin);

                SqlParameter valorRetorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdModificarEmpleado.Parameters.Add(valorRetorno);

                conexion.Open();
                cmdModificarEmpleado.ExecuteNonQuery();

                switch ((int)valorRetorno.Value)
                {
                    case -1:
                        throw new Exception("Debe loguearse como este empleado para modificar su contraseña.");
                    case -2:
                        throw new Exception("No se encontro empleado activo para modificar.");
                    case -3:
                        throw new Exception("Ocurrió un error al modificar usuario.");
                    case -4:
                        throw new Exception("Ocurrió un error al modificar empleado.");
                    case -5:
                        throw new Exception("Ocurrió un error al modificar contraseña de acceso.");
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

        public void BajaEmpleado(Empleado empleado, Usuario usLog)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn(usLog));

                SqlCommand cmdBajaEmpleado = new SqlCommand("BajaEmpleado", conexion);
                cmdBajaEmpleado.CommandType = CommandType.StoredProcedure;

                cmdBajaEmpleado.Parameters.AddWithValue("@logueo", empleado.Logueo);

                SqlParameter valorRetorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdBajaEmpleado.Parameters.Add(valorRetorno);

                conexion.Open();
                cmdBajaEmpleado.ExecuteNonQuery();

                switch ((int)valorRetorno.Value)
                {
                    case -1:
                        throw new Exception("El empleado no existe.");
                    case -2:
                        throw new Exception("Ocurrió un error al eliminar el empleado.");
                    case -3:
                        throw new Exception("Ocurrió un error al eliminar el usuario.");
                    case -4:
                        throw new Exception("Ocurrió un error al eliminar usuario base de datos.");
                    case -5:
                        throw new Exception("Ocurrió un error al eliminar el usuario servidor.");
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
    }
}
