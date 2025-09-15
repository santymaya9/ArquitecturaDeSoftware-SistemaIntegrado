using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Inyecciones
{
    public class ComunicacionPaciente
    {
        // Campo privado para la dependencia inyectada
        private readonly IComunicacion<int> comunicacion;

        // Propiedad con accesor lambda
        public IComunicacion<int> Comunicacion
        {
            get => comunicacion;
        }

        // Constructor para inyección de dependencias
        public ComunicacionPaciente(IComunicacion<int> comunicacion)
        {
            this.comunicacion = comunicacion ?? throw new ArgumentNullException(nameof(comunicacion));
        }
    }
}