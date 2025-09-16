using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Decorator
{
    // Clase base decorador que ser� heredada por los diferentes medios de comunicaci�n
    public abstract class MedioComunicacionDecorator : IComunicacion<Perfil>
    {
        protected readonly IComunicacion<Perfil> comunicacionBase;

        protected MedioComunicacionDecorator(IComunicacion<Perfil> comunicacionBase)
        {
            this.comunicacionBase = comunicacionBase ?? throw new ArgumentNullException(nameof(comunicacionBase));
        }

        public virtual string Comunicacion(Perfil destinatario)
        {
            return comunicacionBase.Comunicacion(destinatario);
        }
    }
}