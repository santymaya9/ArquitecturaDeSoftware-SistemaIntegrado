using System;
using System.Collections.Generic;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Memento
{
    public class AlertaMemento : IMemento
    {
        // Estado guardado de la alerta
        private readonly string tipoAlerta;
        private readonly Perfil reportante;
        private readonly bool estado;
        private readonly uint nivelTriaje;
        private readonly DateTime fechaCreacion;
        private readonly DateTime fechaFinalizacion;
        private readonly Perfil? equipoAsignado;  // Permitir null
        private readonly List<Ruta> rutas;
        
        // Metadatos del memento
        public DateTime FechaCreacion { get; private set; }
        public string UsuarioResponsable { get; private set; }

        public AlertaMemento(Alerta alerta, string usuarioResponsable)
        {
            // Guardar estado actual de la alerta
            this.tipoAlerta = alerta.TipoAlerta;
            this.reportante = alerta.Reportante;
            this.estado = alerta.Estado;
            this.nivelTriaje = alerta.Nivel_triaje;
            this.fechaCreacion = alerta.Fecha_creacion;
            this.fechaFinalizacion = alerta.Fecha_finalizacion;
            this.equipoAsignado = alerta.Equipo_asignado;  // Puede ser null
            this.rutas = new List<Ruta>(alerta.Rutas ?? new List<Ruta>());
            
            // Metadatos del memento
            this.FechaCreacion = DateTime.Now;
            this.UsuarioResponsable = usuarioResponsable ?? "Sistema";
        }

        public void RestaurarEstado(Alerta alerta)
        {
            alerta.TipoAlerta = this.tipoAlerta;
            alerta.Reportante = this.reportante;
            alerta.Estado = this.estado;
            alerta.Nivel_triaje = this.nivelTriaje;
            alerta.Fecha_creacion = this.fechaCreacion;
            alerta.Fecha_finalizacion = this.fechaFinalizacion;
            alerta.Equipo_asignado = this.equipoAsignado;  // Puede ser null
            alerta.Rutas = new List<Ruta>(this.rutas);
        }

        public string ObtenerDescripcion()
        {
            return $"Estado de Alerta: {tipoAlerta} - Triaje: {nivelTriaje} - Estado: {(estado ? "Activa" : "Inactiva")} - Equipo: {equipoAsignado?.Nombre ?? "Sin asignar"}";
        }
    }
}