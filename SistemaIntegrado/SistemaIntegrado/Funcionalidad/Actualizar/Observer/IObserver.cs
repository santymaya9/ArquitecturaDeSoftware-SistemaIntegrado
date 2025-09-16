using System;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Observer
{
    public interface IObserver<T>
    {
        string Update(T elemento);
    }
}