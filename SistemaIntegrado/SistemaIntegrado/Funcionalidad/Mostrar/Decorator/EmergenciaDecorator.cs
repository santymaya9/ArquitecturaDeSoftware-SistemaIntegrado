using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Mostrar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Mostrar.Decorator
{
    public class EmergenciaDecorator<T> : MostrarDecorator<T>
    {
        public EmergenciaDecorator(IMostrar<T> mostrarComponent) : base(mostrarComponent)
        {
        }

        public override string Mostrar(T entidad)
        {
            var contenidoOriginal = base.Mostrar(entidad);
            
            // Si es una alerta, verificar si es crítica
            if (entidad is Alerta alerta && alerta.Nivel_triaje <= 2)
            {
                return $"*** EMERGENCIA CRÍTICA ***\n" +
                       $"NIVEL DE TRIAJE: {alerta.Nivel_triaje}\n" +
                       $"REQUIERE ATENCIÓN INMEDIATA\n" +
                       $"{new string('*', 40)}\n" +
                       $"{contenidoOriginal}\n" +
                       $"{new string('*', 40)}\n" +
                       $"TIEMPO CRÍTICO: {DateTime.Now:HH:mm:ss}";
            }
            
            return $"FORMATO EMERGENCIA\n" +
                   $"Hora: {DateTime.Now:HH:mm:ss}\n" +
                   $"{contenidoOriginal}";
        }
    }
}