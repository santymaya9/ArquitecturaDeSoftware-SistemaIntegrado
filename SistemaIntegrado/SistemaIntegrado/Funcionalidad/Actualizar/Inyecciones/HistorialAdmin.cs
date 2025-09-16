using System;
using SistemaIntegradoAlertas.Funcionalidad.Actualizar.Decorator;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Inyecciones
{
    public class HistorialAdmin
    {
        // Campo privado para la dependencia inyectada
        private readonly IDescargar descargas;

        // Propiedad para acceder a la dependencia
        public IDescargar Descargas => descargas;

        public HistorialAdmin(IDescargar descargas)
        {
            this.descargas = descargas ?? throw new ArgumentNullException(nameof(descargas));
        }
    }
}