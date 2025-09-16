using System;
using System.Collections.Generic;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Observer
{
    /// <summary>
    /// Interfaz para el patrón Observer que define un publicador
    /// </summary>
    /// <typeparam name="T">Tipo de elemento sobre el que se notificarán cambios</typeparam>
    public interface IPublisher<T>
    {
        /// <summary>
        /// Suscribe un observador a este publicador
        /// </summary>
        /// <param name="observer">Observador a suscribir</param>
        void Subscribe(IObserver<T> observer);
        
        /// <summary>
        /// Desuscribe un observador de este publicador
        /// </summary>
        /// <param name="observer">Observador a desuscribir</param>
        void Unsubscribe(IObserver<T> observer);
        
        /// <summary>
        /// Notifica a todos los observadores suscritos sobre un cambio
        /// </summary>
        /// <param name="elemento">El elemento que ha cambiado</param>
        /// <returns>Lista de mensajes resultantes de cada observador</returns>
        List<string> Notify(T elemento);
    }
}