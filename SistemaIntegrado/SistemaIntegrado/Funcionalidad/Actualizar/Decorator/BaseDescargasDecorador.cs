using System;
using SistemaIntegradoAlertas.Funcionalidad.Actualizar.Decorator;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Decorator
{
    public abstract class BaseDescargasDecorador : IDescargar
    {
        // Componente decorado (wrappee)
        protected readonly IDescargar descargador;

        protected BaseDescargasDecorador(IDescargar descargador)
        {
            this.descargador = descargador ?? throw new ArgumentNullException(nameof(descargador));
        }

        public virtual void Descargar(string historial)
        {
            descargador.Descargar(historial);
        }
    }
}