using System;
using SistemaIntegrado.Funcionalidad.Login.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Login.Servicios
{
    public class MicrosoftAuthenticationHandler : BaseHandler
    {
        private IHandler next;

        public override string Handle(object request)
        {
            // Implementación básica que solo pasa la solicitud al siguiente manejador
            if (next != null)
            {
                return next.Handle(request);
            }
            
            return "Autenticación con Microsoft completada";
        }
        
        public new void SetNext(IHandler handler)
        {
            next = handler;
            base.SetNext(handler);
        }
    }
}