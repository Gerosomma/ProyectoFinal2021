using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
    [DataContract]
    public class Empresa : Usuario
    {
        private string _telefono;
        private string _direccion;
        private string _email;

        [DataMember]
        public string Telefono
        {
            get { return _telefono; }
            set {
                int i = 0;
                if (value.Trim().Length == 9 && int.TryParse(value, out i))
                    _telefono = value;
                else
                    throw new Exception("Telefono inválido");
            }
        }

        [DataMember]
        public string Direccion
        {
            get { return _direccion; }
            set
            {
                if (value.Trim().Length > 0 && value.Trim().Length <= 50)
                    _direccion = value;
                else
                    throw new Exception("Direccion inválida");
            }
        }

        [DataMember]
        public string Email
        {
            get { return _email; }
            set {
                if (Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
                    _email = value;
                else
                    throw new Exception("Email inválido");
            }
        }

        public Empresa()
        { }

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
