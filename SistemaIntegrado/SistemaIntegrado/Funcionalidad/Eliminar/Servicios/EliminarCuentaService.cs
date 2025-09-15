using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Eliminar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Eliminar.Servicios
{
    public class EliminarCuentaService : IEliminar<Cuenta>
    {
        public void Eliminar(Cuenta entidad)
        {
            if (entidad != null)
            {
                // Lógica para eliminar cuenta
                entidad.EstadoActivo = false;
            }
        }
    }
}