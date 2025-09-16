using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Observer
{
    /// <summary>
    /// Observador para operadores que recibe notificaciones de mensajes
    /// </summary>
    public class OperadorObserver : IObserver<string>
    {
        /// <summary>
        /// Actualiza al observador con un mensaje recibido
        /// </summary>
        /// <param name="mensaje">El mensaje recibido</param>
        /// <returns>Un mensaje descriptivo de la acción realizada</returns>
        public string Update(string mensaje)
        {
            // Crear mensaje de notificación para operadores
            string resultado = $"[OPERADOR] Mensaje recibido: {mensaje}";
            
            // El operador podría tener acciones específicas según el tipo de mensaje
            // Por ejemplo, registrar el mensaje, asignar tareas, etc.
            if (mensaje.Contains("asignar") || mensaje.Contains("equipo"))
            {
                resultado += "\n[OPERADOR] Se requiere asignación de recursos. Iniciando protocolo de asignación.";
                // Aquí se podría implementar lógica para gestión de recursos
            }
            
            return resultado;
        }
    }
}