using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Actualizar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Servicios
{
    public class ActualizarAlertaService : IActualizar<Perfil, Alerta>
    {
        public void Actualizar(Perfil reportante, Alerta alerta)
        {
            if (reportante != null && alerta != null)
            {
                alerta.Reportante = reportante;
            }
        }
    }
}