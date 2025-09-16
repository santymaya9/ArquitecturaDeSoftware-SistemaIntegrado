using System;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Observer
{
    /// <summary>
    /// Interfaz para el patr�n Observer que define un observador
    /// </summary>
    /// <typeparam name="T">Tipo de elemento que ser� notificado al observador</typeparam>
    public interface IObserver<T>
    {
        /// <summary>
        /// M�todo que ser� llamado cuando un sujeto quiera notificar a este observador
        /// </summary>
        /// <param name="elemento">El elemento que ha cambiado y motiva la notificaci�n</param>
        /// <returns>Un mensaje descriptivo del resultado de la actualizaci�n</returns>
        string Update(T elemento);
    }
}