using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Observer
{
    public class LogComunicacionObserver : INotificacionObserver
    {
        public string OnNotificacionEnviada(string mensaje, Perfil destinatario)
        {
            return $"LOG: Comunicación enviada a {destinatario?.Nombre} - Mensaje: {mensaje} - Fecha: {DateTime.Now:HH:mm:ss}";
        }
    }
}