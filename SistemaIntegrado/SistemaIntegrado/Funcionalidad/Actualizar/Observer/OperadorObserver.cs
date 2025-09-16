using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Observer
{
    public class OperadorObserver : IObserver<string>
    {
        public string Update(string mensaje)
        {
            // Crear mensaje de notificaci�n para operadores
            string resultado = $"[OPERADOR] Mensaje recibido: {mensaje}";
            
            // El operador podr�a tener acciones espec�ficas seg�n el tipo de mensaje
            // Por ejemplo, registrar el mensaje, asignar tareas, etc.
            if (mensaje.Contains("asignar") || mensaje.Contains("equipo"))
            {
                resultado += "\n[OPERADOR] Se requiere asignaci�n de recursos. Iniciando protocolo de asignaci�n.";
                // Aqu� se podr�a implementar l�gica para gesti�n de recursos
            }
            
            return resultado;
        }
    }
}