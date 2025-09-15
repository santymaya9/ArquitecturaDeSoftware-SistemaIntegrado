using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Mostrar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Mostrar.Decorator
{
    public class FormatoXmlDecorator<T> : MostrarDecorator<T>
    {
        public FormatoXmlDecorator(IMostrar<T> mostrarComponent) : base(mostrarComponent)
        {
        }

        public override string Mostrar(T entidad)
        {
            var contenidoOriginal = base.Mostrar(entidad);
            var tipoEntidad = typeof(T).Name;
            
            if (entidad == null)
            {
                return $"<{tipoEntidad}><error>Entidad nula</error></{tipoEntidad}>";
            }

            return $"<{tipoEntidad}>" +
                   $"<timestamp>{DateTime.Now:yyyy-MM-dd HH:mm:ss}</timestamp>" +
                   $"<data>{System.Security.SecurityElement.Escape(contenidoOriginal)}</data>" +
                   $"</{tipoEntidad}>";
        }
    }
}