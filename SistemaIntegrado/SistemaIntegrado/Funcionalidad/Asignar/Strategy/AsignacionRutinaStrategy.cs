using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Asignar.Strategy
{
    public class AsignacionRutinaStrategy : IAsignacionStrategy
    {
        public Perfil AsignarEquipo(Alerta alerta)
        {
            // Estrategia para casos rutinarios
            if (alerta?.Nivel_triaje >= 3)
            {
                // Asignar operador disponible
                return new Operador("Operador Rutina", "rutina@hospital.com", 987654321, "CC", 54321, "pass456", 2, new List<Alerta>());
            }
            return null;
        }

        public uint CalcularPrioridad(Alerta alerta)
        {
            // Prioridad normal para casos rutinarios
            return alerta?.Nivel_triaje >= 3 ? 3u : 1u;
        }
    }
}