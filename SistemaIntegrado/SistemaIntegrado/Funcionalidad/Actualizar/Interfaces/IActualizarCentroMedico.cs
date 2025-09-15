using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Interfaces
{
    public interface IActualizarCentroMedico
    {
        void ActualizarCentroMedico(string nombre, uint telefono, uint complejidad, uint latitud, float longitud, CentroMedico centro_Medico);
    }
}