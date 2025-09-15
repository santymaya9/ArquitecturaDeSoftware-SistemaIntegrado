using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Actualizar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Memento
{
    public class ActualizarSeguroService<T, TCambio> : IActualizar<T, TCambio>
    {
        private readonly ActualizarConMemento<T, TCambio> actualizadorConMemento;
        private readonly Perfil? usuarioActual;  // Permitir null

        public ActualizarSeguroService(
            IActualizar<T, TCambio> actualizadorBase,
            HistorialActualizaciones historial,
            Perfil? usuarioActual)  // Permitir null
        {
            this.actualizadorConMemento = new ActualizarConMemento<T, TCambio>(
                actualizadorBase, 
                historial, 
                usuarioActual?.Nombre ?? "Sistema"
            );
            this.usuarioActual = usuarioActual;
        }

        public void Actualizar(T entidad, TCambio cambio)
        {
            // Validación básica simple (sin Chain of Responsibility)
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));

            // Validación de permisos simplificada
            if (!TienePermisos(entidad))
                throw new UnauthorizedAccessException("Usuario no tiene permisos para realizar esta actualización");

            // Actualizar con Memento (guarda estado automáticamente)
            actualizadorConMemento.Actualizar(entidad, cambio);
        }

        private bool TienePermisos(T entidad)
        {
            // Si no hay usuario, denegar acceso
            if (usuarioActual == null) return false;

            // Validación simple de permisos
            return usuarioActual switch
            {
                Admin => true,  // Admins pueden actualizar todo
                Operador when entidad is Alerta => true,  // Operadores pueden actualizar alertas
                Paramedico when entidad is Alerta alerta => alerta.Equipo_asignado?.Cedula == usuarioActual.Cedula,  // Paramédicos solo sus alertas
                _ => false
            };
        }

        public bool PuedeDeshacer(T entidad)
        {
            return actualizadorConMemento.PuedeDeshacer(entidad);
        }

        public bool DeshacerUltimoCambio(T entidad)
        {
            return actualizadorConMemento.DeshacerUltimoCambio(entidad);
        }

        public ResultadoActualizacion<T> ActualizarConResultado(T entidad, TCambio cambio)
        {
            try
            {
                Actualizar(entidad, cambio);
                return new ResultadoActualizacion<T>
                {
                    Exitoso = true,
                    Entidad = entidad,
                    Mensaje = "Actualización realizada exitosamente",
                    PuedeDeshacer = PuedeDeshacer(entidad)
                };
            }
            catch (Exception ex)
            {
                return new ResultadoActualizacion<T>
                {
                    Exitoso = false,
                    Entidad = entidad,
                    Mensaje = ex.Message,
                    PuedeDeshacer = false
                };
            }
        }
    }

    public class ResultadoActualizacion<T>
    {
        public bool Exitoso { get; set; }
        public T? Entidad { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public bool PuedeDeshacer { get; set; }
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;
    }
}