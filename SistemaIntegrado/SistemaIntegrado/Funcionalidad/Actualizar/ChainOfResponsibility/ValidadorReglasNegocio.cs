using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.ChainOfResponsibility
{
    public class ValidadorReglasNegocio : ValidadorActualizacionBase
    {
        protected override string Validar(object entidad, object cambios)
        {
            if (entidad is Alerta alerta)
            {
                return ValidarReglasAlerta(alerta);
            }

            if (entidad is CentroMedico centro)
            {
                return ValidarReglasCentroMedico(centro);
            }

            return null;
        }

        private string ValidarReglasAlerta(Alerta alerta)
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

        private string ValidarReglasCentroMedico(CentroMedico centro)
        {
            // Regla: Centros de alta complejidad deben tener más de 7 dígitos en teléfono
            if (centro.Complejidad == "Alta" && centro.Telefono.ToString().Length < 7)
            {
                return "Error: Centros de alta complejidad requieren teléfono con al menos 7 dígitos";
            }

            // Regla: Coordenadas deben estar en rangos válidos
            if (centro.Latitud < -90 || centro.Latitud > 90)
            {
                return "Error: Latitud debe estar entre -90 y 90 grados";
            }

            if (centro.Longitud < -180 || centro.Longitud > 180)
            {
                return "Error: Longitud debe estar entre -180 y 180 grados";
            }

            return null;
        }
    }
}