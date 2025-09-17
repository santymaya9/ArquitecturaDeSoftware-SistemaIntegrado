using System;
using SistemaIntegrado.Funcionalidad.Login.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Login.Servicios
{
    public class ValidationRollHandler : BaseHandler
    {
        private IHandler next;

        public override string Handle(object request)
        {
            // Implementaci�n b�sica que solo pasa la solicitud al siguiente manejador
            if (next != null)
            {
                return next.Handle(request);
            }
            
            return "Validaci�n de roles completada";
        }
        
        public new void SetNext(IHandler handler)
        {
            next = handler;
            base.SetNext(handler);
        }
    }
}