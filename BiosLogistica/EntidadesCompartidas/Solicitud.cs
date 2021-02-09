﻿using System;
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
            set {
                if (value.Trim().Length > 0 && value.Trim().Length <= 50)
                    _nombreDestinatario = value;
                else
                    throw new Exception("Direccion inválida");
            }
        }

        public string DireccionDestinatario
        {
            get { return _direccionDestinatario; }
            set {
                if (value.Trim().Length > 0 && value.Trim().Length <= 50)
                    _direccionDestinatario = value;
                else
                    throw new Exception("Direccion inválida");
            }
        }

        public string Estado
        {
            get { return _estado; }
            set {
                string[] arr = { "en deposito", "en camino", "entregado" };
                if (arr.All(value.Contains))
                    _estado = value;
                else
                    throw new Exception("Estado inválido");
            }
        }

        public Empleado Empleado
        {
            get { return _empleado; }
            set {
                if (value != null)
                    _empleado = value;
                else
                    throw new Exception("Empleado inválido");
            }
        }

        public List<Paquete> PaquetesSolicitud
        {
            get { return _paquetesSolicitud; }
            set {
                if (value != null && value.Count > 0)
                    _paquetesSolicitud = value;
                else
                    throw new Exception("Solicitud sin paquetes");
            }
        }

        public Solicitud()
        {
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
