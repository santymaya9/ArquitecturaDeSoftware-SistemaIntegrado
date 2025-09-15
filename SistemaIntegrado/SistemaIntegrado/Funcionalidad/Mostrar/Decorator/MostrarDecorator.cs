using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Mostrar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Mostrar.Decorator
{
    public abstract class MostrarDecorator<T> : IMostrar<T>
    {
        protected readonly IMostrar<T> mostrarComponente;

        protected MostrarDecorator(IMostrar<T> mostrarComponente)
        {
            this.mostrarComponente = mostrarComponente ?? throw new ArgumentNullException(nameof(mostrarComponente));
        }

        public virtual string Mostrar(T entidad)
        {
            return mostrarComponente.Mostrar(entidad);
        }
    }
}