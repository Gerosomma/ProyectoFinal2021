using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;
using Logica.interfaces;
using Persistencia;

namespace Logica.clasesDeTrabajo
{
    internal class LogicaPaquete : ILogicaPaquete
    {
        private static LogicaPaquete _instancia = null;
        private LogicaPaquete() { }
        public static LogicaPaquete GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new LogicaPaquete();
            }
            return _instancia;
        }

        internal static void validarPaquete(Paquete paquete)
        {
            if (paquete.Peso <= 0)
                throw new Exception("Peso paquete inválido");
        }

        public void AltaPaquete(Paquete paquete, Empleado usLog)
        {
            validarPaquete(paquete);
            FabricaPersistencia.GetPersistenciaPaquete().AltaPaquete(paquete, usLog);   
        }

        public Paquete BuscarPaquete(int codigo, Empleado usLog)
        {
            return FabricaPersistencia.GetPersistenciaPaquete().BuscarPaquete(codigo, usLog);
        }

        public List<Paquete> ListadoPaquetesSinSolicitud(Empleado usLog)
        {
            return FabricaPersistencia.GetPersistenciaPaquete().ListadoPaquetesSinSolicitud(usLog);
        }
    }
}
