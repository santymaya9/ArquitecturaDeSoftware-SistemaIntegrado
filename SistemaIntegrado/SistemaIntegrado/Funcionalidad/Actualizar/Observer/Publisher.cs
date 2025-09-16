using System;
using System.Collections.Generic;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Observer
{
    public class Publisher<T> : IPublisher<T>
    {
        // Lista de observadores suscritos
        private List<IObserver<T>> _observers = new List<IObserver<T>>();
        
        public void Subscribe(IObserver<T> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }
        
        public void Unsubscribe(IObserver<T> observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }
        
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