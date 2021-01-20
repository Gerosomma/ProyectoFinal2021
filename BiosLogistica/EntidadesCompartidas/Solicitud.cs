using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class Solicitud
    {
        private int _numero;
        private DateTime _fechaEntrega;
        private string _nombreDestinatario;
        private string _direccionDestinatario;
        private string _estado;
        private Empleado _empleado;
        private List<Paquete> _paquetesSolicitud = new List<Paquete>();

        public int Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        public DateTime FechaEntrega
        {
            get { return _fechaEntrega; }
            set { _fechaEntrega = value; }
        }

        public string NombreDestinatario
        {
            get { return _nombreDestinatario; }
            set { _nombreDestinatario = value; }
        }

        public string DireccionDestinatario
        {
            get { return _direccionDestinatario; }
            set { _direccionDestinatario = value; }
        }

        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public Empleado Empleado
        {
            get { return _empleado; }
            set { _empleado = value; }
        }

        public List<Paquete> PaquetesSolicitud
        {
            get { return _paquetesSolicitud; }
            set { _paquetesSolicitud = value; }
        }

        public Solicitud(int numero, DateTime fechaEntrega, string nombreDestinatario, 
            string direccionDestinatario, string estado, Empleado empleado, List<Paquete> paquetesSolicitud)
        {
            Numero = numero;
            FechaEntrega = fechaEntrega;
            NombreDestinatario = nombreDestinatario;
            DireccionDestinatario = direccionDestinatario;
            Estado = estado;
            Empleado = empleado;
            PaquetesSolicitud = paquetesSolicitud;
        }
        
    }
}
