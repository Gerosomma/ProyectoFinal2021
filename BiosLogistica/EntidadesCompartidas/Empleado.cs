using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            set { _horaInicio = value; }
        }

        public string HoraFin
        {
            get { return _horaFin; }
            set { _horaFin = value; }
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
