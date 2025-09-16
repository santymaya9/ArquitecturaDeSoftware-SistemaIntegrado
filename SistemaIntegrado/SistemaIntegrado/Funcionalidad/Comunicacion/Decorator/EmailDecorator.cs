using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Decorator
{
    // Decorador para comunicaci�n v�a email
    public class EmailDecorator : MedioComunicacionDecorator
    {
        private readonly string asunto;
        
        public EmailDecorator(IComunicacion<Perfil> comunicacionBase, string asunto = "Notificaci�n del Sistema M�dico") 
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
                return $"ERROR EMAIL: No se puede enviar - direcci�n de correo inv�lida";
            }
            
            return $"[EMAIL] Enviado a {destinatario.Correo}\n" +
                   $"Asunto: {asunto}\n" +
                   $"---------------------------\n" +
                   $"{mensaje}\n" +
                   $"---------------------------\n" +
                   $"Este correo es generado autom�ticamente por el Sistema Integrado de Alertas M�dicas.\n" +
                   $"No responda a este correo. Si necesita asistencia, comun�quese con soporte.";
        }
    }
}