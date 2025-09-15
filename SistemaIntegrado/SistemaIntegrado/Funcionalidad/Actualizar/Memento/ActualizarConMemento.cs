using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Actualizar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Memento
{
    public class ActualizarConMemento<T, TCambio> : IActualizar<T, TCambio>
    {
        private readonly IActualizar<T, TCambio> actualizadorBase;
        private readonly HistorialActualizaciones historial;
        private readonly string usuarioActual;

        public ActualizarConMemento(
            IActualizar<T, TCambio> actualizadorBase, 
            HistorialActualizaciones historial, 
            string usuarioActual)
        {
            this.actualizadorBase = actualizadorBase ?? throw new ArgumentNullException(nameof(actualizadorBase));
            this.historial = historial ?? throw new ArgumentNullException(nameof(historial));
            this.usuarioActual = usuarioActual ?? "Sistema";
        }

        public void Actualizar(T entidad, TCambio cambio)
        {
            if (entidad == null) return;

            // Guardar estado antes de actualizar
            var memento = CrearMemento(entidad);
            if (memento != null)
            {
                historial.GuardarEstado(entidad, memento);
            }

            // Ejecutar la actualización original
            actualizadorBase.Actualizar(entidad, cambio);
        }

        private IMemento? CrearMemento(T entidad)
        {
            return entidad switch
            {
                Alerta alerta => new AlertaMemento(alerta, usuarioActual),
                CentroMedico centro => new CentroMedicoMemento(centro, usuarioActual),
                _ => null // No hay memento para este tipo
            };
        }

        public bool PuedeDeshacer(T entidad)
        {
            if (entidad == null) return false;  // Validar null
            return historial.PuedeDeshacer(entidad);
        }

        public bool DeshacerUltimoCambio(T entidad)
        {
            if (entidad == null) return false;  // Validar null
            
            var memento = historial.DeshacerUltimoCambio(entidad);
            if (memento == null) return false;

            // Restaurar el estado según el tipo
            switch (entidad)
            {
                case Alerta alerta when memento is AlertaMemento alertaMemento:
                    alertaMemento.RestaurarEstado(alerta);
                    return true;
                    
                case CentroMedico centro when memento is CentroMedicoMemento centroMemento:
                    centroMemento.RestaurarEstado(centro);
                    return true;
                    
                default:
                    return false;
            }
        }
    }
}