using System;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Observer
{
    /// <summary>
    /// Interfaz para el patrón Observer que define un observador
    /// </summary>
    /// <typeparam name="T">Tipo de elemento que será notificado al observador</typeparam>
    public interface IObserver<T>
    {
        /// <summary>
        /// Método que será llamado cuando un sujeto quiera notificar a este observador
        /// </summary>
        /// <param name="elemento">El elemento que ha cambiado y motiva la notificación</param>
        /// <returns>Un mensaje descriptivo del resultado de la actualización</returns>
        string Update(T elemento);
    }
}