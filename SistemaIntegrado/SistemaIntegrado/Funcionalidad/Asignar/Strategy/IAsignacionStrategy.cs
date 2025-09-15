using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Asignar.Strategy
{
    public interface IAsignacionStrategy
    {
        Perfil AsignarEquipo(Alerta alerta);
        uint CalcularPrioridad(Alerta alerta);
    }
}