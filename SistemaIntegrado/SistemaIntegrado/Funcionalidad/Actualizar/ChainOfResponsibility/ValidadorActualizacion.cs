using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.ChainOfResponsibility
{
    public abstract class ValidadorActualizacion<T>
    {
        protected ValidadorActualizacion<T> siguienteValidador;

        public void EstablecerSiguiente(ValidadorActualizacion<T> siguiente)
        {
            this.siguienteValidador = siguiente;
        }

        public virtual string ValidarYProcesar(T entidad, object cambios = null)
        {
            var resultado = Validar(entidad, cambios);
            
            if (!string.IsNullOrEmpty(resultado))
            {
                return resultado; // Error encontrado, detener cadena
            }

            // Si no hay error y hay siguiente validador, continuar cadena
            if (siguienteValidador != null)
            {
                return siguienteValidador.ValidarYProcesar(entidad, cambios);
            }

            return null; // Todo OK, cadena completada exitosamente
        }

        protected abstract string Validar(T entidad, object cambios);
    }
}