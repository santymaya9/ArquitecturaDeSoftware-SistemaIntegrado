using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Decorator
{
    // Decorador para comunicaci�n v�a mensaje de radio para param�dicos
    public class RadioDecorator : MedioComunicacionDecorator
    {
        private readonly string canalRadio;
        
        public RadioDecorator(IComunicacion<Perfil> comunicacionBase, string canalRadio = "CANAL-1") 
            : base(comunicacionBase)
        {
            this.canalRadio = canalRadio;
        }

        public override string Comunicacion(Perfil destinatario)
        {
            var mensajeBase = base.Comunicacion(destinatario);
            
            return TransmitirPorRadio(mensajeBase, destinatario);
        }
        
        private string TransmitirPorRadio(string mensaje, Perfil destinatario)
        {
            if (destinatario == null)
            {
                return $"ERROR RADIO: No se puede transmitir - destinatario inv�lido";
            }
            
            // Formato especial para comunicaciones de radio
            var mensajeRadio = mensaje.Replace("\n", " ");
            
            return $"[RADIO] Transmitiendo por {canalRadio}\n" +
                   $"*BEEP* Atenci�n unidades. Mensaje para {destinatario.Nombre}. *BEEP*\n" +
                   $"Mensaje: {mensajeRadio}\n" +
                   $"Cambio y fuera. *BEEP*";
        }
    }
}