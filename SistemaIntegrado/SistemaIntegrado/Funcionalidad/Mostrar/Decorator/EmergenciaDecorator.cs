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
            
            // Si es una alerta, verificar si es cr�tica
            if (entidad is Alerta alerta && alerta.Nivel_triaje <= 2)
            {
                return $"*** EMERGENCIA CR�TICA ***\n" +
                       $"NIVEL DE TRIAJE: {alerta.Nivel_triaje}\n" +
                       $"REQUIERE ATENCI�N INMEDIATA\n" +
                       $"{new string('*', 40)}\n" +
                       $"{contenidoOriginal}\n" +
                       $"{new string('*', 40)}\n" +
                       $"TIEMPO CR�TICO: {DateTime.Now:HH:mm:ss}";
            }
            
            return $"FORMATO EMERGENCIA\n" +
                   $"Hora: {DateTime.Now:HH:mm:ss}\n" +
                   $"{contenidoOriginal}";
        }
    }
}