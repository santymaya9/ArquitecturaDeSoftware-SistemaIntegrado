using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces;
using SistemaIntegrado.Funcionalidad.Comunicacion.Decorator;
using SistemaIntegrado.Funcionalidad.Comunicacion.Implementaciones;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Inyecciones
{
    public class ComunicacionOperador
    {
        // Cambio para usar Perfil en lugar de Alerta
        private readonly IComunicacion<Perfil> comunicacion;

        // Propiedad con accesor lambda
        public IComunicacion<Perfil> Comunicacion => comunicacion;

        // Constructor para inyección de dependencias
        public ComunicacionOperador(IComunicacion<Perfil> comunicacionBase)
        {
            if (comunicacionBase == null)
                throw new ArgumentNullException(nameof(comunicacionBase));

            // Decoramos la comunicación base con los medios preferidos para operadores
            // Operadores: preferencia por comunicaciones detalladas (email, llamada)
            this.comunicacion = new EmailDecorator(
                                  new LlamadaDecorator(comunicacionBase),
                                  "ALERTA OPERACIONAL: Acción requerida");
        }
    }
}