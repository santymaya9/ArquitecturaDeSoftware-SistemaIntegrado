using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Servicios.Asignar.Interfaces;

namespace SistemaIntegrado.Servicios.Asignar.Inyecciones
{
    public class AsignadorParamedico
    {
        // Campos privados para las dependencias inyectadas
        private readonly IAsignar<Alerta, uint> asignarNivelTriaje;
        private readonly IAsignar<Alerta, string> asignarEstadoAlerta;

        // Propiedades con accesores lambda
        public IAsignar<Alerta, uint> AsignarNivelTriaje
        {
            get => asignarNivelTriaje;
        }

        public IAsignar<Alerta, string> AsignarEstadoAlerta
        {
            get => asignarEstadoAlerta;
        }

        // Constructor para inyección de dependencias
        public AsignadorParamedico(
            IAsignar<Alerta, uint> asignarNivelTriaje,
            IAsignar<Alerta, string> asignarEstadoAlerta)
        {
            this.asignarNivelTriaje = asignarNivelTriaje ?? throw new ArgumentNullException(nameof(asignarNivelTriaje));
            this.asignarEstadoAlerta = asignarEstadoAlerta ?? throw new ArgumentNullException(nameof(asignarEstadoAlerta));
        }
    }
}