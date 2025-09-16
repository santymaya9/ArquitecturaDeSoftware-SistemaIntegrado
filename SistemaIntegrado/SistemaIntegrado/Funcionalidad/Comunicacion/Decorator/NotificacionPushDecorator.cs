using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Decorator
{
    // Decorador para comunicación vía notificación push en app móvil
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
                return $"ERROR PUSH: No se puede enviar - destinatario inválido";
            }
            
            // Acortar el mensaje para notificación push
            var mensajeCorto = mensaje.Length > 100 
                ? mensaje.Substring(0, 97) + "..." 
                : mensaje;
            
            return $"[PUSH] Notificación enviada al dispositivo de {destinatario.Nombre}\n" +
                   $"Título: Alerta Médica\n" +
                   $"Cuerpo: {mensajeCorto}\n" +
                   $"Acción: Abrir app > Detalles de alerta\n" +
                   $"Prioridad: Alta - Sonido: Activado";
        }
    }
}