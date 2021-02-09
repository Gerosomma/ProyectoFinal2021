using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class Empleado : Usuario
    {
        private string _horaInicio;
        private string _horaFin;

        public string HoraInicio
        {
            get { return _horaInicio; }
            set
            {
                Regex _expresion = new Regex("[0-2][0-9][:][0-5][0-9][:][0-5][0-9]");
                if (!_expresion.IsMatch(value))
                    throw new Exception("Formato hora inicio inválido.");
                else
                    _horaInicio = value;
                
            }
        }

        public string HoraFin
        {
            get { return _horaFin; }
            set
            {
                if (!new Regex("[0-2][0-9][:][0-5][0-9][:][0-5][0-9]").IsMatch(value))
                    throw new Exception("Formato hora fin inválido.");
                else
                    _horaFin = value;
            }
        }

        public Empleado()
        {

        }

        public Empleado(string logueo, string contrasenia, string nombreCompleto,
            string horaInicio, string horaFin)
            :base(logueo, contrasenia, nombreCompleto)
        {
            HoraInicio = horaInicio;
            HoraFin = horaFin;
        }
    }
}
