using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Observer
{
    public class ParamedicoObserver : IObserver<string>
    {
        public string Update(string mensaje)
        {
            // Crear mensaje de notificaci�n para param�dicos
            string resultado = $"[PARAM�DICO] Mensaje recibido: {mensaje}";
            
            // Verificar si es un mensaje de emergencia
            if (mensaje.Contains("urgencia") || mensaje.Contains("emergencia") || mensaje.Contains("urgente"))
            {
                resultado += "\n[PARAM�DICO] �ALERTA! Mensaje de emergencia detectado. Enviando notificaci�n prioritaria.";
                // Aqu� se podr�a implementar l�gica adicional para manejar emergencias
            }
            
            return resultado;
        }
    }
}