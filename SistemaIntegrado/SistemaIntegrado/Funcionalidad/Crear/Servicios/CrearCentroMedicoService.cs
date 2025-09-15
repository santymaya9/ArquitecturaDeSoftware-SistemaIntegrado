using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Crear.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Crear.Servicios
{
    public class CrearCentroMedicoService : ICreadorCentroMedico
    {
        public void Crear(string nombre, uint telefono, uint complejidad, uint latitud, float longitud)
        {
            if (!string.IsNullOrWhiteSpace(nombre) && telefono > 0 && complejidad > 0)
            {
                var centroMedico = new CentroMedico(
                    nombre, 
                    latitud, 
                    longitud, 
                    complejidad.ToString(), 
                    (int)telefono
                );
            }
        }
    }
}