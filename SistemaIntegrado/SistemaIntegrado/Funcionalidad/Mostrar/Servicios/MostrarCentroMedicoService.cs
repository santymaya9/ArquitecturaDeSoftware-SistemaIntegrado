using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Mostrar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Mostrar.Servicios
{
    public class MostrarCentroMedicoService : IMostrar<CentroMedico>
    {
        public string Mostrar(CentroMedico entidad)
        {
            if (entidad != null)
            {
                return $"Centro Médico: {entidad.Nombre}, Teléfono: {entidad.Telefono}, Complejidad: {entidad.Complejidad}, Ubicación: [{entidad.Latitud}, {entidad.Longitud}]";
            }
            return "No hay información del centro médico";
        }
    }
}