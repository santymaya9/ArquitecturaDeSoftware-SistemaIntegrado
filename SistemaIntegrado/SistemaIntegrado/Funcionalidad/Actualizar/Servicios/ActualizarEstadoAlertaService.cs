using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Actualizar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Servicios
{
    public class ActualizarEstadoAlertaService : IActualizar<Alerta, string>
    {
        public void Actualizar(Alerta alerta, string cambio_estado)
        {
            if (alerta != null && !string.IsNullOrWhiteSpace(cambio_estado))
            {
                alerta.Estado = cambio_estado == "activo";
            }
        }
    }
}