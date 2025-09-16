using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Observer
{
    /// <summary>
    /// Observador para pacientes que recibe notificaciones de alertas m�dicas
    /// </summary>
    public class PacienteObserver : IObserver<Alerta>
    {
        /// <summary>
        /// Actualiza al observador con una alerta recibida
        /// </summary>
        /// <param name="elemento">La alerta m�dica recibida</param>
        /// <returns>Un mensaje descriptivo de la acci�n realizada</returns>
        public string Update(Alerta elemento)
        {
            // Manejar caso de alerta nula
            if (elemento == null)
            {
                return "[PACIENTE] Alerta recibida pero sin informaci�n.";
            }
            
            // Crear mensaje b�sico de notificaci�n
            string resultado = $"[PACIENTE] Alerta recibida: {elemento.TipoAlerta}";
            
            // Crear mensaje personalizado para el paciente
            string mensajePaciente = $"Estimado paciente, se le informa que tiene una alerta de tipo {elemento.TipoAlerta} ";
            
            if (elemento.Equipo_asignado != null)
            {
                mensajePaciente += $"y ser� atendido por el equipo {elemento.Equipo_asignado.Nombre}.";
            }
            else
            {
                mensajePaciente += "y pronto se le asignar� un equipo m�dico.";
            }
            
            resultado += $"\n[PACIENTE] Mensaje para paciente: {mensajePaciente}";
            
            return resultado;
        }
    }
}