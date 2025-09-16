using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces;
using SistemaIntegrado.Funcionalidad.Comunicacion.Decorator;
using SistemaIntegrado.Funcionalidad.Comunicacion.Implementaciones;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Inyecciones
{
    public class ComunicacionParamedico
    {
        // Cambio para usar Perfil en lugar de Alerta
        private readonly IComunicacion<Perfil> comunicacion;

        // Propiedad con accesor lambda
        public IComunicacion<Perfil> Comunicacion => comunicacion;

        // Constructor para inyección de dependencias
        public ComunicacionParamedico(IComunicacion<Perfil> comunicacionBase)
        {
            if (comunicacionBase == null)
                throw new ArgumentNullException(nameof(comunicacionBase));

            // Decoramos la comunicación base con los medios preferidos para paramédicos
            // Paramédicos: preferencia por comunicaciones en movilidad (radio, SMS)
            this.comunicacion = new RadioDecorator(
                                  new SMSDecorator(comunicacionBase),
                                  "EMERGENCIAS-7");
        }
    }
}