using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Geolocalizacion.Interfaces
{
    public interface IGeolocalizacion
    {
        string Ubicacion(float latitud, float longitud);
    }
}