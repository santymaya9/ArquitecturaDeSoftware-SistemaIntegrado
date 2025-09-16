using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Implementaciones
{
    // Componente concreto b�sico para la comunicaci�n
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
                return "No se puede enviar mensaje: destinatario no v�lido";
            }
            
            return $"Mensaje enviado a {destinatario.Nombre}: {mensaje}";
        }
    }
}