﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;

namespace Persistencia.interfaces
{
    public interface IPersistenciaEmpleado
    {
        Empleado LoguearEmpleado(string logueo, string contrasenia);
        Empleado BuscarEmpleado(string logueo, Usuario usLog);
        void AltaEmpleado(Empleado empleado, Usuario usLog);
        void ModificarEmpleado(Empleado empleado, Usuario usLog);
        void BajaEmpleado(Empleado empleado, Usuario usLog);
    }
}