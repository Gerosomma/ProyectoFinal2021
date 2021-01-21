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

        }

        public void AltaPaquete(Paquete paquete, Usuario usLog)
        {
            validarPaquete(paquete);
            FabricaPersistencia.GetPersistenciaPaquete().AltaPaquete(paquete, usLog);   
        }

        public void BajaPaquete(Paquete paquete, Usuario usLog)
        {
            validarPaquete(paquete);
            FabricaPersistencia.GetPersistenciaPaquete().BajaPaquete(paquete, usLog);
        }

        public Paquete BuscarPaquete(int codigo, Usuario usLog)
        {
            return FabricaPersistencia.GetPersistenciaPaquete().BuscarPaquete(codigo, usLog);
        }

        public void ModificarPaquete(Paquete paquete, Usuario usLog)
        {
            validarPaquete(paquete);
            FabricaPersistencia.GetPersistenciaPaquete().ModificarPaquete(paquete, usLog);
        }
    }
}
