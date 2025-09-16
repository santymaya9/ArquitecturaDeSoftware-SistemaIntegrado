using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Decorator
{
    // Decorador para comunicación vía email
    public class EmailDecorator : MedioComunicacionDecorator
    {
        private readonly string asunto;
        
        public EmailDecorator(IComunicacion<Perfil> comunicacionBase, string asunto = "Notificación del Sistema Médico") 
            : base(comunicacionBase)
        {
            this.asunto = asunto;
        }

        public override string Comunicacion(Perfil destinatario)
        {
            var mensajeBase = base.Comunicacion(destinatario);
            
            return EnviarEmail(mensajeBase, destinatario);
        }
        
        private string EnviarEmail(string mensaje, Perfil destinatario)
        {
            if (destinatario == null || string.IsNullOrEmpty(destinatario.Correo))
            {
                return $"ERROR EMAIL: No se puede enviar - dirección de correo inválida";
            }
            
            return $"[EMAIL] Enviado a {destinatario.Correo}\n" +
                   $"Asunto: {asunto}\n" +
                   $"---------------------------\n" +
                   $"{mensaje}\n" +
                   $"---------------------------\n" +
                   $"Este correo es generado automáticamente por el Sistema Integrado de Alertas Médicas.\n" +
                   $"No responda a este correo. Si necesita asistencia, comuníquese con soporte.";
        }
    }
}