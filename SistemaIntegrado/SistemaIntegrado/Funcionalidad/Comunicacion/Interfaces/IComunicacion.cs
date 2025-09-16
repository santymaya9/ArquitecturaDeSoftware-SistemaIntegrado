using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces
{
    // Mantenemos la interfaz base para la comunicación
    public interface IComunicacion<T>
    {
        string Comunicacion(T entidad);
    }
}