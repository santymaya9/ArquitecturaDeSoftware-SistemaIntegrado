using System;
using System.Collections.Generic;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Clases
{
    public class Alerta
    {
        // Campos privados
        private string tipoAlerta;
        private Perfil reportante;
        private bool estado;
        private uint nivel_triaje;
        private DateTime fecha_creacion;
        private DateTime fecha_finalizacion;
        private Perfil? equipo_asignado;  // Permitir null
        private List<Ruta> rutas;

        // Propiedades con accesores lambda
        public string TipoAlerta
        {
            get => tipoAlerta;
            set => tipoAlerta = value;
        }

        public Perfil Reportante
        {
            get => reportante;
            set => reportante = value;
        }

        public bool Estado
        {
            get => estado;
            set => estado = value;
        }

        public uint Nivel_triaje
        {
            get => nivel_triaje;
            set => nivel_triaje = value;
        }

        public DateTime Fecha_creacion
        {
            get => fecha_creacion;
            set => fecha_creacion = value;
        }

        public DateTime Fecha_finalizacion
        {
            get => fecha_finalizacion;
            set => fecha_finalizacion = value;
        }

        public Perfil? Equipo_asignado  // Permitir null
        {
            get => equipo_asignado;
            set => equipo_asignado = value;
        }

        public List<Ruta> Rutas
        {
            get => rutas;
            set => rutas = value;
        }

        // Constructor según el diagrama
        public Alerta(Perfil reportante, DateTime fecha_creacion, string tipoAlerta)
        {
            this.reportante = reportante;
            this.fecha_creacion = fecha_creacion;
            this.tipoAlerta = tipoAlerta;
            this.rutas = new List<Ruta>();
            this.estado = true;
            this.equipo_asignado = null; // Inicializar explícitamente
        }
    }
}