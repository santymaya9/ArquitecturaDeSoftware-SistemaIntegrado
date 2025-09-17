using System;
using SistemaIntegrado.Funcionalidad.Login.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Login.Servicios
{
    public abstract class BaseHandler : IHandler
    {
        private IHandler next;

        public void SetNext(IHandler handler)
        {
            next = handler;
        }

        public abstract string Handle(object request);
    }
}