using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Eliminar.Interfaces
{
    public interface IEliminar<T>
    {
        void Eliminar(T entidad);
    }
}