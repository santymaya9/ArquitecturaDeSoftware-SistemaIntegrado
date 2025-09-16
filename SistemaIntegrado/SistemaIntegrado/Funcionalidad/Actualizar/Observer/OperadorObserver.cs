using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Observer
{
    public class OperadorObserver : IObserver<string>
    {
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