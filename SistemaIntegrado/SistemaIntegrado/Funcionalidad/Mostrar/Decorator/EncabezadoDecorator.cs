using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Mostrar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Mostrar.Decorator
{
    public class EncabezadoDecorator<T> : MostrarDecorator<T>
    {
        private readonly string titulo;
        private readonly string usuario;

        public EncabezadoDecorator(IMostrar<T> mostrarComponent, string titulo = "REPORTE", string usuario = "Sistema") : base(mostrarComponent)
        {
            this.titulo = titulo ?? "REPORTE";
            this.usuario = usuario ?? "Sistema";
        }

        public override string Mostrar(T entidad)
        {
            var contenidoOriginal = base.Mostrar(entidad);
            var tipoEntidad = typeof(T).Name;
            
            return $"SISTEMA INTEGRADO - {titulo} DE {tipoEntidad.ToUpper()}\n" +
                   $"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n" +
                   $"Usuario: {usuario}\n" +
                   $"Tipo: {tipoEntidad}\n" +
                   $"{new string('=', 50)}\n" +
                   $"{contenidoOriginal}\n" +
                   $"{new string('=', 50)}";
        }
    }
}