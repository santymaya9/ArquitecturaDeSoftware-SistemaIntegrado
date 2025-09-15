using System;
using System.Collections.Generic;
using System.Linq;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Memento
{
    public class HistorialActualizaciones
    {
        private readonly Dictionary<object, Stack<IMemento>> historialPorEntidad;
        private readonly List<IMemento> historialGlobal;
        private readonly int limiteHistorial;

        public HistorialActualizaciones(int limiteHistorial = 50)
        {
            this.historialPorEntidad = new Dictionary<object, Stack<IMemento>>();
            this.historialGlobal = new List<IMemento>();
            this.limiteHistorial = limiteHistorial;
        }

        public void GuardarEstado(object entidad, IMemento memento)
        {
            if (entidad == null || memento == null) return;

            // Agregar al historial específico de la entidad
            if (!historialPorEntidad.ContainsKey(entidad))
            {
                historialPorEntidad[entidad] = new Stack<IMemento>();
            }

            var historialEntidad = historialPorEntidad[entidad];
            historialEntidad.Push(memento);

            // Limitar el historial por entidad
            if (historialEntidad.Count > limiteHistorial / 2)
            {
                // Convertir a lista, remover los más antiguos, convertir de vuelta
                var lista = historialEntidad.ToArray().Reverse().Take(limiteHistorial / 2).Reverse().ToArray();
                historialEntidad.Clear();
                foreach (var item in lista)
                {
                    historialEntidad.Push(item);
                }
            }

            // Agregar al historial global
            historialGlobal.Add(memento);

            // Limitar el historial global
            if (historialGlobal.Count > limiteHistorial)
            {
                historialGlobal.RemoveAt(0);
            }
        }

        public IMemento? ObtenerUltimoEstado(object entidad)
        {
            if (entidad == null || !historialPorEntidad.ContainsKey(entidad))
                return null;

            var historial = historialPorEntidad[entidad];
            return historial.Count > 0 ? historial.Peek() : null;
        }

        public bool PuedeDeshacer(object entidad)
        {
            return ObtenerUltimoEstado(entidad) != null;
        }

        public IMemento? DeshacerUltimoCambio(object entidad)
        {
            if (entidad == null || !historialPorEntidad.ContainsKey(entidad))
                return null;

            var historial = historialPorEntidad[entidad];
            return historial.Count > 0 ? historial.Pop() : null;
        }

        public List<IMemento> ObtenerHistorialCompleto()
        {
            return new List<IMemento>(historialGlobal);
        }

        public List<IMemento> ObtenerHistorialEntidad(object entidad)
        {
            if (entidad == null || !historialPorEntidad.ContainsKey(entidad))
                return new List<IMemento>();

            return historialPorEntidad[entidad].ToList();
        }

        public void LimpiarHistorial()
        {
            historialPorEntidad.Clear();
            historialGlobal.Clear();
        }

        public void LimpiarHistorialEntidad(object entidad)
        {
            if (entidad != null && historialPorEntidad.ContainsKey(entidad))
            {
                historialPorEntidad[entidad].Clear();
            }
        }
    }
}