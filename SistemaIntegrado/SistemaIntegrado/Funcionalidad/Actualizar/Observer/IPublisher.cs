using System;
using System.Collections.Generic;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Observer
{
    public interface IPublisher<T>
    {
        void Subscribe(IObserver<T> observer);
        
        void Unsubscribe(IObserver<T> observer);
        
        List<string> Notify(T elemento);
    }
}