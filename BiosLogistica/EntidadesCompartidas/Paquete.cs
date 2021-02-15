using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
namespace EntidadesCompartidas
{
    [DataContract]
    public class Paquete
    {
        private int _codigo;
        private string _tipo;
        private string _descripcion;
        private double _peso;
        private Empresa _empresaOrigen;

        [DataMember]
        public int Codigo
        {
            get { return _codigo; }
            set {
                if (value >= 1)
                    _codigo = value;
                else
                    throw new Exception("Codigo paquete inválido");
            }
        }

        [DataMember]
        public string Tipo
        {
            get { return _tipo; }
            set {
                string[] arr = { "fragil", "comun", "bulto"};
                if (arr.All(value.Contains))
                    _tipo = value;
                else
                    throw new Exception("Tipo inválido");
            }
        }

        [DataMember]
        public string Descripcion
        {
            get { return _descripcion;  }
            set {
                if (value.Trim().Length > 0 && value.Trim().Length <= 100)
                    _descripcion = value;
                else
                    throw new Exception("Descripcion inválida");
            }
        }

        [DataMember]
        public double Peso
        {
            get { return _peso; }
            set {
                if (value > 0)
                    _peso = value;
                else
                    throw new Exception("Peso inválido");
            }
        }

        [DataMember]
        public Empresa EmpresaOrigen
        {
            get { return _empresaOrigen; }
            set {
                if (value != null)
                    _empresaOrigen = value;
                else
                    throw new Exception("Empleado inválido para paquete");
            }
        }

        public Paquete()
        {
        }

        public Paquete(int codigo, string tipo, string descripcion, double peso, Empresa empresaOrigen)
        {
            Codigo = codigo;
            Tipo = tipo;
            Descripcion = descripcion;
            Peso = peso;
            EmpresaOrigen = empresaOrigen;
        }
    }
}
