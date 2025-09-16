using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Decorator
{
    // Decorador para comunicaci�n v�a notificaci�n push en app m�vil
    public class NotificacionPushDecorator : MedioComunicacionDecorator
    {
        public NotificacionPushDecorator(IComunicacion<Perfil> comunicacionBase) 
            : base(comunicacionBase)
        {
        }

        public override string Comunicacion(Perfil destinatario)
        {
            var mensajeBase = base.Comunicacion(destinatario);
            
            return EnviarNotificacionPush(mensajeBase, destinatario);
        }
        
        private string EnviarNotificacionPush(string mensaje, Perfil destinatario)
        {
            if (destinatario == null)
            {
                return $"ERROR PUSH: No se puede enviar - destinatario inv�lido";
            }
            
            // Acortar el mensaje para notificaci�n push
            var mensajeCorto = mensaje.Length > 100 
                ? mensaje.Substring(0, 97) + "..." 
                : mensaje;
            
            return $"[PUSH] Notificaci�n enviada al dispositivo de {destinatario.Nombre}\n" +
                   $"T�tulo: Alerta M�dica\n" +
                   $"Cuerpo: {mensajeCorto}\n" +
                   $"Acci�n: Abrir app > Detalles de alerta\n" +
                   $"Prioridad: Alta - Sonido: Activado";
        }
    }
}