using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Eliminar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Eliminar.Servicios
{
    public class EliminarCentroMedicoService : IEliminar<CentroMedico>
    {
        public void Eliminar(CentroMedico entidad)
        {
            if (entidad != null)
            {
                // Lógica para eliminar centro médico
                // Por ejemplo, marcarlo como inactivo o removerlo de una lista
            }
        }
    }
}