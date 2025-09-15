using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Mostrar.Interfaces
{
    public interface IMostrar<T>
    {
        string Mostrar(T entidad);
    }
}