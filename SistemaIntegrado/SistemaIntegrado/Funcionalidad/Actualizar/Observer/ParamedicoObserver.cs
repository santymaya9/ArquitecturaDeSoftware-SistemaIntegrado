using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Observer
{
    /// <summary>
    /// Observador para paramédicos que recibe notificaciones de mensajes
    /// </summary>
    public class ParamedicoObserver : IObserver<string>
    {
        /// <summary>
        /// Actualiza al observador con un mensaje recibido
        /// </summary>
        /// <param name="mensaje">El mensaje recibido</param>
        /// <returns>Un mensaje descriptivo de la acción realizada</returns>
        public string Update(string mensaje)
        {
            // Crear mensaje de notificación para paramédicos
            string resultado = $"[PARAMÉDICO] Mensaje recibido: {mensaje}";
            
            // Verificar si es un mensaje de emergencia
            if (mensaje.Contains("urgencia") || mensaje.Contains("emergencia") || mensaje.Contains("urgente"))
            {
                resultado += "\n[PARAMÉDICO] ¡ALERTA! Mensaje de emergencia detectado. Enviando notificación prioritaria.";
                // Aquí se podría implementar lógica adicional para manejar emergencias
            }
            
            return resultado;
        }
    }
}