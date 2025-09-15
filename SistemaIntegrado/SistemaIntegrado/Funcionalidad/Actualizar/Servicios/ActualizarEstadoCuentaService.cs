using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Actualizar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Servicios
{
    public class ActualizarEstadoCuentaService : IActualizar<Cuenta, bool>
    {
        public void Actualizar(Cuenta cuenta, bool cambio_estado)
        {
            if (cuenta != null)
            {
                cuenta.EstadoActivo = cambio_estado;
            }
        }
    }
}