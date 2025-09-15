using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Servicios.Asignar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Asignar.Servicios
{
    public class AsignarEstadoAlertaService : IAsignar<Alerta, string>
    {
        public void Asignar(Alerta alerta, string estado)
        {
            if (alerta != null && !string.IsNullOrWhiteSpace(estado))
            {
                // Convertir string a bool
                alerta.Estado = estado.ToLower() == "activo" || estado.ToLower() == "true" || estado == "1";
            }
        }
    }
}