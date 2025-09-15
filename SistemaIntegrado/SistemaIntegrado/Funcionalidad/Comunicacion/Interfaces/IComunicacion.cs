using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces
{
    public interface IComunicacion<T>
    {
        string Comunicacion(T entidad);
    }
}