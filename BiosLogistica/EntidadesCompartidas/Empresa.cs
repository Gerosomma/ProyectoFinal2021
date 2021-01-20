using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class Empresa : Usuario
    {
        private string _telefono;
        private string _direccion;
        private string _email;

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public Empresa(string logueo, string contrasenia, string nombreCompleto, 
            string telefono, string direccion, string email)
            :base(logueo, contrasenia, nombreCompleto)
        {
            Telefono = telefono;
            Direccion = direccion;
            Email = email;
        }
    }
}
