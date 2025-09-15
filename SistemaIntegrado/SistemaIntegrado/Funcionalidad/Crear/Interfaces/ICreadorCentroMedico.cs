using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Crear.Interfaces
{
    public interface ICreadorCentroMedico
    {
        void Crear(string nombre, uint telefono, uint complejidad, uint latitud, float longitud);
    }
}