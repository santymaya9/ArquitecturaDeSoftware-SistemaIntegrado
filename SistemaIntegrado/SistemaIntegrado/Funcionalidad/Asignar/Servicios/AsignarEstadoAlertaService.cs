using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Servicios.Asignar.Interfaces;

namespace SistemaIntegrado.Servicios.Asignar.Servicios
{
    public class AsignarEstadoAlertaService : IAsignar<Alerta, string>
    {
        public void Asignar(Alerta alerta, string estado)
        {
            if (alerta != null && !string.IsNullOrWhiteSpace(estado))
            {
                alerta.Estado = estado;
            }
        }
    }
}