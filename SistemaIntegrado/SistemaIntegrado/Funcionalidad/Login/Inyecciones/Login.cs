using System;
using SistemaIntegrado.Funcionalidad.Login.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Login.Inyecciones
{
    public class Login
    {
        // Campo privado para la dependencia inyectada
        private readonly IHandler handler;

        // Propiedad p�blica para acceder a la dependencia
        public IHandler Handler => handler;

        // Constructor que recibe la dependencia por inyecci�n
        public Login(IHandler handler)
        {
            this.handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }
    }
}