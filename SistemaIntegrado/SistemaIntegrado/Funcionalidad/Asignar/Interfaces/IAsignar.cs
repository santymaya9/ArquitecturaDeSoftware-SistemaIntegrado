using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Servicios.Asignar.Interfaces
{
    public interface IAsignar<T, T2>
    {
        void Asignar(T entidad, T2 cambio);
    }
}