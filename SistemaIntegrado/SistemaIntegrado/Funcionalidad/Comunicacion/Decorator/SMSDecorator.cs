using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Decorator
{
    // Decorador para comunicaci�n v�a SMS
    public class SMSDecorator : MedioComunicacionDecorator
    {
        public SMSDecorator(IComunicacion<Perfil> comunicacionBase) 
            : base(comunicacionBase)
        {
        }

        public override string Comunicacion(Perfil destinatario)
        {
            var mensajeBase = base.Comunicacion(destinatario);
            
            return FormatearSMS(mensajeBase, destinatario);
        }
        
        private string FormatearSMS(string mensaje, Perfil destinatario)
        {
            if (destinatario == null || destinatario.Celular <= 0)
            {
                return $"ERROR SMS: No se puede enviar - n�mero de celular inv�lido";
            }
            
            return $"[SMS] Enviado al {destinatario.Celular}: {TruncateMensaje(mensaje, 160)}";
        }
        
        // Los SMS tienen un l�mite de 160 caracteres
        private string TruncateMensaje(string mensaje, int maxLength)
        {
            if (mensaje.Length <= maxLength)
                return mensaje;
                
            return mensaje.Substring(0, maxLength - 3) + "...";
        }
    }
}