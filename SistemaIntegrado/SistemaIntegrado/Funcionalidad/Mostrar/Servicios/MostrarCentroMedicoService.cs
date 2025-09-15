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
                return $"Centro M�dico: {entidad.Nombre}, Tel�fono: {entidad.Telefono}, Complejidad: {entidad.Complejidad}, Ubicaci�n: [{entidad.Latitud}, {entidad.Longitud}]";
            }
            return "No hay informaci�n del centro m�dico";
        }
    }
}