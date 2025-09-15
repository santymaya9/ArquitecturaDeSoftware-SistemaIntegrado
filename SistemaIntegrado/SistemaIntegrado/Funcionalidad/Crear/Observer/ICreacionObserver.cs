using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Crear.Observer
{
    public interface ICreacionObserver<T>
    {
        string OnElementoCreado(T elemento);
    }
}