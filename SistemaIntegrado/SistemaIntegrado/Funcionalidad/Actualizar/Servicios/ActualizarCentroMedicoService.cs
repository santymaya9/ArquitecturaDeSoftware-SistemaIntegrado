using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Actualizar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Servicios
{
    public class ActualizarCentroMedicoService : IActualizarCentroMedico
    {
        public void ActualizarCentroMedico(string nombre, uint telefono, uint complejidad, uint latitud, float longitud, CentroMedico centro_Medico)
        {
            if (centro_Medico != null)
            {
                if (!string.IsNullOrWhiteSpace(nombre))
                    centro_Medico.Nombre = nombre;
                
                if (telefono > 0)
                    centro_Medico.Telefono = (int)telefono;
                
                if (complejidad > 0)
                    centro_Medico.Complejidad = complejidad.ToString();
                
                if (latitud >= 0)
                    centro_Medico.Latitud = latitud;
                
                centro_Medico.Longitud = longitud;
            }
        }
    }
}