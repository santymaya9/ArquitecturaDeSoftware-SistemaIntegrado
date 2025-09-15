using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Geolocalizacion.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Geolocalizacion.Servicios
{
    public class GeolocalizacionService : IGeolocalizacion
    {
        public string Ubicacion(float latitud, float longitud)
        {
            if (latitud >= -90 && latitud <= 90 && longitud >= -180 && longitud <= 180)
            {
                return $"Ubicación encontrada - Latitud: {latitud}, Longitud: {longitud}";
            }
            else
            {
                return "Error: Coordenadas inválidas - Latitud debe estar entre -90 y 90, Longitud entre -180 y 180";
            }
        }
    }
}