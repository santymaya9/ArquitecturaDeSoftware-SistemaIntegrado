using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Observer
{
    public interface INotificacionObserver
    {
        string OnNotificacionEnviada(string mensaje, Perfil destinatario);
    }
}