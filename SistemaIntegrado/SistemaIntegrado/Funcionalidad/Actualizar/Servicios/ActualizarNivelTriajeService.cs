using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Actualizar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Servicios
{
    public class ActualizarNivelTriajeService : IActualizar<Alerta, string>
    {
        public void Actualizar(Alerta alerta, string cambio_nivelTriaje)
        {
            if (alerta != null && !string.IsNullOrWhiteSpace(cambio_nivelTriaje))
            {
                if (uint.TryParse(cambio_nivelTriaje, out uint nivel))
                {
                    alerta.Nivel_triaje = nivel;
                }
            }
        }
    }
}