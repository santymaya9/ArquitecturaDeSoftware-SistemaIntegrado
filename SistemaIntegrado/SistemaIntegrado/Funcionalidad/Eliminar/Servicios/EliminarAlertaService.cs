using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Eliminar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Eliminar.Servicios
{
    public class EliminarAlertaService : IEliminar<Alerta>
    {
        public void Eliminar(Alerta entidad)
        {
            if (entidad != null)
            {
                // Lógica para eliminar alerta
                entidad.Estado = false;
            }
        }
    }
}