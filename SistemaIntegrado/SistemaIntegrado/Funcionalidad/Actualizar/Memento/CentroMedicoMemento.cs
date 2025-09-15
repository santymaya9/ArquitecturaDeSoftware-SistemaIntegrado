using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Memento
{
    public class CentroMedicoMemento : IMemento
    {
        // Estado guardado del centro médico
        private readonly string nombre;
        private readonly int telefono;
        private readonly string complejidad;
        private readonly float latitud;  // Cambiar de uint a float
        private readonly float longitud;
        
        // Metadatos del memento
        public DateTime FechaCreacion { get; private set; }
        public string UsuarioResponsable { get; private set; }

        public CentroMedicoMemento(CentroMedico centro, string usuarioResponsable)
        {
            // Guardar estado actual del centro médico
            this.nombre = centro.Nombre;
            this.telefono = centro.Telefono;
            this.complejidad = centro.Complejidad;
            this.latitud = centro.Latitud;  // Ahora funciona correctamente
            this.longitud = centro.Longitud;
            
            // Metadatos del memento
            this.FechaCreacion = DateTime.Now;
            this.UsuarioResponsable = usuarioResponsable ?? "Sistema";
        }

        public void RestaurarEstado(CentroMedico centro)
        {
            centro.Nombre = this.nombre;
            centro.Telefono = this.telefono;
            centro.Complejidad = this.complejidad;
            centro.Latitud = this.latitud;
            centro.Longitud = this.longitud;
        }

        public string ObtenerDescripcion()
        {
            return $"Estado de Centro Médico: {nombre} - Tel: {telefono} - Complejidad: {complejidad} - Ubicación: [{latitud}, {longitud}]";
        }
    }
}