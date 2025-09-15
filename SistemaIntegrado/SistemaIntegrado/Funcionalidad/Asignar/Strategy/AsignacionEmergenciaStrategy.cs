using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Asignar.Strategy;

namespace SistemaIntegradoAlertas.Funcionalidad.Asignar.Strategy
{
    public class AsignacionEmergenciaStrategy : IAsignacionStrategy
    {
        public Perfil AsignarEquipo(Alerta alerta)
        {
            // Estrategia para emergencias críticas
            if (alerta?.Nivel_triaje <= 2)
            {
                // Asignar paramédico más cercano y disponible
                return new Paramedico(1, "Paramédico Emergencia", "emergencia@hospital.com", 123456789, "CC", 12345, "pass123", 1, 10);
            }
            return null;
        }

        public uint CalcularPrioridad(Alerta alerta)
        {
            // Máxima prioridad para emergencias
            return alerta?.Nivel_triaje <= 2 ? 1u : 5u;
        }
    }
}