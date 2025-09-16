using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Decorator
{
    // Decorador para comunicaci�n v�a llamada telef�nica
    public class LlamadaDecorator : MedioComunicacionDecorator
    {
        public LlamadaDecorator(IComunicacion<Perfil> comunicacionBase) 
            : base(comunicacionBase)
        {
        }

        public override string Comunicacion(Perfil destinatario)
        {
            var mensajeBase = base.Comunicacion(destinatario);
            
            return RealizarLlamada(mensajeBase, destinatario);
        }
        
        private string RealizarLlamada(string mensaje, Perfil destinatario)
        {
            if (destinatario == null || destinatario.Celular <= 0)
            {
                return $"ERROR LLAMADA: No se puede conectar - n�mero de tel�fono inv�lido";
            }
            
            return $"[LLAMADA] Conectando con {destinatario.Nombre} al {destinatario.Celular}\n" +
                   $"Estado: Conectado - Duraci�n: 00:01:32\n" +
                   $"Mensaje transmitido verbalmente: \"{mensaje}\"\n" +
                   $"Llamada finalizada correctamente.";
        }
    }
}