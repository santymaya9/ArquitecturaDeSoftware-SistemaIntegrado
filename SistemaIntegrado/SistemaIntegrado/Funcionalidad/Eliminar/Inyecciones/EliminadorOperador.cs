using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Eliminar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Eliminar.Inyecciones
{
    public class EliminadorOperador
    {
        // Campo privado para la dependencia inyectada
        private readonly IEliminar<Alerta> eliminarAlerta;

        // Propiedad con accesor lambda
        public IEliminar<Alerta> EliminarAlerta
        {
            get => eliminarAlerta;
        }

        // Constructor para inyección de dependencias
        public EliminadorOperador(IEliminar<Alerta> eliminarAlerta)
        {
            this.eliminarAlerta = eliminarAlerta ?? throw new ArgumentNullException(nameof(eliminarAlerta));
        }
    }
}