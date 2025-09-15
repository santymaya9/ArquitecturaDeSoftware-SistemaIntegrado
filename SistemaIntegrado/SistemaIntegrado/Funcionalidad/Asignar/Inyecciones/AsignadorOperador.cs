using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Servicios.Asignar.Interfaces;

namespace SistemaIntegrado.Servicios.Asignar.Inyecciones
{
    public class AsignadorOperador
    {
        // Campos privados para las dependencias inyectadas
        private readonly IAsignar<Alerta, string> asignarNivelTriaje;
        private readonly IAsignar<Alerta, Perfil> asignarAlerta;

        // Propiedades con accesores lambda
        public IAsignar<Alerta, string> AsignarNivelTriaje
        {
            get => asignarNivelTriaje;
        }

        public IAsignar<Alerta, Perfil> AsignarAlerta
        {
            get => asignarAlerta;
        }

        // Constructor para inyección de dependencias
        public AsignadorOperador(
            IAsignar<Alerta, string> asignarNivelTriaje,
            IAsignar<Alerta, Perfil> asignarAlerta)
        {
            this.asignarNivelTriaje = asignarNivelTriaje ?? throw new ArgumentNullException(nameof(asignarNivelTriaje));
            this.asignarAlerta = asignarAlerta ?? throw new ArgumentNullException(nameof(asignarAlerta));
        }
    }
}