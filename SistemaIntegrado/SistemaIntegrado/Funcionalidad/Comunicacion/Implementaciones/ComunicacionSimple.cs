using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Implementaciones
{
    // Componente concreto básico para la comunicación
    public class ComunicacionSimple : IComunicacion<Perfil>
    {
        private readonly string mensaje;

        public ComunicacionSimple(string mensaje = "Mensaje del sistema")
        {
            this.mensaje = mensaje;
        }

        public string Comunicacion(Perfil destinatario)
        {
            if (destinatario == null)
            {
                return "No se puede enviar mensaje: destinatario no válido";
            }
            
            return $"Mensaje enviado a {destinatario.Nombre}: {mensaje}";
        }
    }
}