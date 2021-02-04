using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class Paquete
    {
        private int _codigo;
        private string _tipo;
        private string _descripcion;
        private double _peso;
        private Empresa _empresaOrigen;

        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public string Descripcion
        {
            get { return _descripcion;  }
            set { _descripcion = value; }
        }

        public double Peso
        {
            get { return _peso; }
            set { _peso = value; }
        }

        public Empresa EmpresaOrigen
        {
            get { return _empresaOrigen; }
            set { _empresaOrigen = value; }
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
