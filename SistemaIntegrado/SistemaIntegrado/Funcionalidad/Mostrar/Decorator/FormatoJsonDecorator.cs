using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Mostrar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Mostrar.Decorator
{
    public class FormatoJsonDecorator<T> : MostrarDecorator<T>
    {
        public FormatoJsonDecorator(IMostrar<T> mostrarComponent) : base(mostrarComponent)
        {
        }

        public override string Mostrar(T entidad)
        {
            var contenidoOriginal = base.Mostrar(entidad);
            
            if (entidad == null)
            {
                return "{ \"error\": \"Entidad nula\", \"data\": null }";
            }

            return $"{{ \"format\": \"JSON\", \"timestamp\": \"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\", \"data\": \"{contenidoOriginal.Replace("\"", "\\\"")}\" }}";
        }
    }
}