using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class Usuario
    {
        private string _logueo;
        private string _contrasenia;
        private string _nombreCompleto;

        public string Logueo
        {
            get { return _logueo; }
            set { _logueo = value; }
        }
        public string Contrasena
        {
            get { return _contrasenia; }
            set { _contrasenia = value; }
        }
        public string NombreCompleto
        {
            get { return _nombreCompleto; }
            set { _nombreCompleto = value; }
        }

        public Usuario(string logueo, string contrasenia, string nombreCompleto)
        {
            Logueo = logueo;
            Contrasena = contrasenia;
            NombreCompleto = nombreCompleto;
        }
    }
}
