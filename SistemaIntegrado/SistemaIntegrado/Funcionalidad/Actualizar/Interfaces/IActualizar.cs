using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Interfaces
{
    public interface IActualizar<T, T2>
    {
        void Actualizar(T entidad, T2 entidad2);
    }
}