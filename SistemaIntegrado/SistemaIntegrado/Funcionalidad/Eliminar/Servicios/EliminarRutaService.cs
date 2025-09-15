using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Eliminar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Eliminar.Servicios
{
    public class EliminarRutaService : IEliminar<Ruta>
    {
        public void Eliminar(Ruta entidad)
        {
            if (entidad != null)
            {
                // Lógica para eliminar ruta
                // Por ejemplo, marcar el nodo como -1 para indicar eliminación
                entidad.NumNodo = -1;
            }
        }
    }
}