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
                // L�gica para eliminar centro m�dico
                // Por ejemplo, marcarlo como inactivo o removerlo de una lista
            }
        }
    }
}