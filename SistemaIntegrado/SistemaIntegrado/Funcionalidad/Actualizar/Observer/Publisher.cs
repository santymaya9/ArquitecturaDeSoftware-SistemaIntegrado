using System;
using System.Collections.Generic;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Observer
{
    /// <summary>
    /// Clase que implementa el patrón Publisher para notificar a observadores
    /// </summary>
    /// <typeparam name="T">Tipo de datos que se notificarán</typeparam>
    public class Publisher<T> : IPublisher<T>
    {
        // Lista de observadores suscritos
        private List<IObserver<T>> _observers = new List<IObserver<T>>();
        
        /// <summary>
        /// Suscribe un observador a este publicador
        /// </summary>
        /// <param name="observer">Observador a suscribir</param>
        public void Subscribe(IObserver<T> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }
        
        /// <summary>
        /// Desuscribe un observador de este publicador
        /// </summary>
        /// <param name="observer">Observador a desuscribir</param>
        public void Unsubscribe(IObserver<T> observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }
        
        /// <summary>
        /// Notifica a todos los observadores con los datos proporcionados
        /// </summary>
        /// <param name="data">Datos a notificar</param>
        /// <returns>Lista de mensajes resultantes de cada observador</returns>
        public List<string> Notify(T elemento)
        {
            List<string> resultados = new List<string>();
            
            foreach (var observer in _observers)
            {
                try
                {
                    string resultado = observer.Update(elemento);
                    resultados.Add(resultado);
                }
                catch (Exception ex)
                {
                    // Manejo de errores para evitar que un observador con problemas
                    // afecte a la notificación del resto
                    resultados.Add($"Error al notificar al observador: {ex.Message}");
                }
            }
            
            return resultados;
        }
    }
}