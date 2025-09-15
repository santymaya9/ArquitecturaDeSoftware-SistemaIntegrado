using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Asignar.Strategy;

namespace SistemaIntegradoAlertas.Funcionalidad.Asignar.Strategy
{
    public class AsignacionEmergenciaStrategy : IAsignacionStrategy
    {
        public Perfil AsignarEquipo(Alerta alerta)
        {
            // Estrategia para emergencias cr�ticas
            if (alerta?.Nivel_triaje <= 2)
            {
                // Asignar param�dico m�s cercano y disponible
                return new Paramedico(1, "Param�dico Emergencia", "emergencia@hospital.com", 123456789, "CC", 12345, "pass123", 1, 10);
            }
            return null;
        }

        public uint CalcularPrioridad(Alerta alerta)
        {
            // M�xima prioridad para emergencias
            return alerta?.Nivel_triaje <= 2 ? 1u : 5u;
        }
    }
}