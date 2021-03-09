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
    [KnownType(typeof(Empresa))]
    [KnownType(typeof(Empleado))]
    [DataContract]
    public abstract class Usuario
    {
        private string _logueo;
        private string _contrasenia;
        private string _nombreCompleto;

        [DataMember]
        public string Logueo
        {
            get { return _logueo; }
            set
            {
                if (value.Trim().Length > 12 || value.Trim().Length <= 0)
                    throw new Exception("Usuario debe tener 12 caracteres");
                else
                    _logueo = value;
            }
        }

        [DataMember]
        public string Contrasena
        {
            get { return _contrasenia; }
            set {
                Regex _expresion = new Regex("[a-zA-ZñÑ]{3}[0-9]{2}[|°¬¡!#$%&/=()¿?'_´{},;.:`+*~^<>@]");
                if (value.Length != 6)
                {
                    throw new Exception("La contraseña debe contener 6 caracteres");
                }
                else if (!_expresion.IsMatch(value))
                {
                    throw new Exception("El formato de la contraseña no es válido.");
                }
                else
                {
                    _contrasenia = value;
                }
            }
        }

        [DataMember]
        public string NombreCompleto
        {
            get { return _nombreCompleto; }
            set
            {
                if ((value.Trim().Length > 50) || (value.Trim().Length <= 0))
                    throw new Exception("Nombre cliente inválido");
                else
                    _nombreCompleto = value;
            }
        }
        
        public Usuario()
        { }

        public  Usuario(string logueo, string contrasenia, string nombreCompleto)
        {
            Logueo = logueo;
            Contrasena = contrasenia;
            NombreCompleto = nombreCompleto;
        }
    }
}
