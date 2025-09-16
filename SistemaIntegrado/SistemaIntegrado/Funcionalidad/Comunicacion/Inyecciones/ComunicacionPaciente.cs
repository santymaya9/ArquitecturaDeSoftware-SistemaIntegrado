using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces;
using SistemaIntegrado.Funcionalidad.Comunicacion.Decorator;
using SistemaIntegrado.Funcionalidad.Comunicacion.Implementaciones;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Inyecciones
{
    public class ComunicacionPaciente
    {
        // Campo privado para la dependencia inyectada (usando Perfil)
        private readonly IComunicacion<Perfil> comunicacion;

        // Propiedad con accesor lambda
        public IComunicacion<Perfil> Comunicacion => comunicacion;

        // Constructor para inyección de dependencias
        public ComunicacionPaciente(IComunicacion<Perfil> comunicacionBase)
        {
            if (comunicacionBase == null)
                throw new ArgumentNullException(nameof(comunicacionBase));

            // Decoramos la comunicación base con los medios preferidos para pacientes
            // Pacientes: preferencia por comunicaciones sencillas y directas (SMS, notificaciones push)
            this.comunicacion = new SMSDecorator(
                                  new NotificacionPushDecorator(comunicacionBase));
        }
    }
}