using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Eliminar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Eliminar.Inyecciones
{
    public class EliminadorAdmin
    {
        // Campos privados para las dependencias inyectadas
        private readonly IEliminar<Alerta> eliminarAlerta;
        private readonly IEliminar<Cuenta> eliminarCuenta;
        private readonly IEliminar<CentroMedico> eliminarCentroMedico;

        // Propiedades con accesores lambda
        public IEliminar<Alerta> EliminarAlerta
        {
            get => eliminarAlerta;
        }

        public IEliminar<Cuenta> EliminarCuenta
        {
            get => eliminarCuenta;
        }

        public IEliminar<CentroMedico> EliminarCentroMedico
        {
            get => eliminarCentroMedico;
        }

        // Constructor para inyección de dependencias
        public EliminadorAdmin(
            IEliminar<Alerta> eliminarAlerta,
            IEliminar<Cuenta> eliminarCuenta,
            IEliminar<CentroMedico> eliminarCentroMedico)
        {
            this.eliminarAlerta = eliminarAlerta ?? throw new ArgumentNullException(nameof(eliminarAlerta));
            this.eliminarCuenta = eliminarCuenta ?? throw new ArgumentNullException(nameof(eliminarCuenta));
            this.eliminarCentroMedico = eliminarCentroMedico ?? throw new ArgumentNullException(nameof(eliminarCentroMedico));
        }
    }
}