using System;
using SistemaIntegradoAlertas.Funcionalidad.Actualizar.Decorator;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Inyecciones
{
    /// <summary>
    /// Clase para la inyección de dependencias relacionadas con la descarga de historial
    /// </summary>
    public class HistorialAdmin
    {
        // Campo privado para la dependencia inyectada
        private readonly IDescargar descargas;

        // Propiedad para acceder a la dependencia
        public IDescargar Descargas => descargas;

        /// <summary>
        /// Constructor para inyección de dependencias
        /// </summary>
        /// <param name="descargas">Servicio de descargas a inyectar</param>
        public HistorialAdmin(IDescargar descargas)
        {
            this.descargas = descargas ?? throw new ArgumentNullException(nameof(descargas));
        }
    }
}