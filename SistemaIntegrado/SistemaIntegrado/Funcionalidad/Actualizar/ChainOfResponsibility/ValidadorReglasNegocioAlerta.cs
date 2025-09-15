using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.ChainOfResponsibility
{
    public class ValidadorReglasNegocioAlerta : ValidadorActualizacion<Alerta>
    {
        protected override string Validar(Alerta alerta, object cambios)
        {
            // Regla: No se puede cambiar una alerta ya finalizada
            if (alerta.Fecha_finalizacion != default && alerta.Fecha_finalizacion < DateTime.Now)
            {
                return "Error: No se puede actualizar una alerta ya finalizada";
            }

            // Regla: Alertas críticas (nivel 1-2) deben tener equipo asignado
            if (alerta.Nivel_triaje <= 2 && alerta.Equipo_asignado == null)
            {
                return "Error: Alertas críticas deben tener un equipo asignado";
            }

            // Regla: No se puede desactivar una alerta crítica sin finalizar
            if (alerta.Nivel_triaje <= 2 && !alerta.Estado && alerta.Fecha_finalizacion == default)
            {
                return "Error: No se puede desactivar una alerta crítica sin finalizarla";
            }

            return null;
        }
    }
}