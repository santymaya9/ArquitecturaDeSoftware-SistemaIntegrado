using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Mostrar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Mostrar.Inyecciones
{
    public class MostradorAdmin
    {
        // Campos privados para las dependencias inyectadas
        private readonly IMostrar<CentroMedico> mostrarCentroMedico;
        private readonly IMostrar<SistemaIntegradoAlertas.Clases.Singleton.SistemaIntegrado> mostrarSistemaIntegrado;
        private readonly IMostrar<Cuenta> mostrarCuenta;
        private readonly IMostrar<Alerta> mostrarAlerta;

        // Propiedades con accesores lambda
        public IMostrar<CentroMedico> MostrarCentroMedico
        {
            get => mostrarCentroMedico;
        }

        public IMostrar<SistemaIntegradoAlertas.Clases.Singleton.SistemaIntegrado> MostrarSistemaIntegrado
        {
            get => mostrarSistemaIntegrado;
        }

        public IMostrar<Cuenta> MostrarCuenta
        {
            get => mostrarCuenta;
        }

        public IMostrar<Alerta> MostrarAlerta
        {
            get => mostrarAlerta;
        }

        // Constructor para inyecci�n de dependencias
        public MostradorAdmin(
            IMostrar<CentroMedico> mostrarCentroMedico,
            IMostrar<Cuenta> mostrarCuenta,
            IMostrar<SistemaIntegradoAlertas.Clases.Singleton.SistemaIntegrado> mostrarSistemaIntegrado,
            IMostrar<Alerta> mostrarAlerta)
        {
            this.mostrarCentroMedico = mostrarCentroMedico ?? throw new ArgumentNullException(nameof(mostrarCentroMedico));
            this.mostrarCuenta = mostrarCuenta ?? throw new ArgumentNullException(nameof(mostrarCuenta));
            this.mostrarSistemaIntegrado = mostrarSistemaIntegrado ?? throw new ArgumentNullException(nameof(mostrarSistemaIntegrado));
            this.mostrarAlerta = mostrarAlerta ?? throw new ArgumentNullException(nameof(mostrarAlerta));
        }
    }
}