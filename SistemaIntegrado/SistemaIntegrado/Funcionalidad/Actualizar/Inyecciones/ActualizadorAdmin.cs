using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Actualizar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Inyecciones
{
    public class ActualizadorAdmin
    {
        // Campos privados para las dependencias inyectadas
        private readonly IActualizarCentroMedico actualizarCentroMedico;
        private readonly IActualizarCuenta actualizarCuenta;
        private readonly IActualizar<Perfil, Alerta> actualizarAlerta;
        private readonly IActualizar<Alerta, string> actualizarEstadoAlerta;
        private readonly IActualizar<Alerta, string> actualizarNivelTriaje;
        private readonly IActualizar<Cuenta, bool> actualizarEstadoCuenta;

        // Propiedades con accesores lambda
        public IActualizarCentroMedico ActualizarCentroMedico
        {
            get => actualizarCentroMedico;
        }

        public IActualizarCuenta ActualizarCuenta
        {
            get => actualizarCuenta;
        }

        public IActualizar<Perfil, Alerta> ActualizarAlerta
        {
            get => actualizarAlerta;
        }

        public IActualizar<Alerta, string> ActualizarEstadoAlerta
        {
            get => actualizarEstadoAlerta;
        }

        public IActualizar<Alerta, string> ActualizarNivelTriaje
        {
            get => actualizarNivelTriaje;
        }

        public IActualizar<Cuenta, bool> ActualizarEstadoCuenta
        {
            get => actualizarEstadoCuenta;
        }

        // Constructor para inyección de dependencias
        public ActualizadorAdmin(
            IActualizarCentroMedico actualizarCentroMedico,
            IActualizarCuenta actualizarCuenta,
            IActualizar<Perfil, Alerta> actualizarAlerta,
            IActualizar<Alerta, string> actualizarEstadoAlerta,
            IActualizar<Alerta, string> actualizarNivelTriaje,
            IActualizar<Cuenta, bool> actualizarEstadoCuenta)
        {
            this.actualizarCentroMedico = actualizarCentroMedico ?? throw new ArgumentNullException(nameof(actualizarCentroMedico));
            this.actualizarCuenta = actualizarCuenta ?? throw new ArgumentNullException(nameof(actualizarCuenta));
            this.actualizarAlerta = actualizarAlerta ?? throw new ArgumentNullException(nameof(actualizarAlerta));
            this.actualizarEstadoAlerta = actualizarEstadoAlerta ?? throw new ArgumentNullException(nameof(actualizarEstadoAlerta));
            this.actualizarNivelTriaje = actualizarNivelTriaje ?? throw new ArgumentNullException(nameof(actualizarNivelTriaje));
            this.actualizarEstadoCuenta = actualizarEstadoCuenta ?? throw new ArgumentNullException(nameof(actualizarEstadoCuenta));
        }
    }
}