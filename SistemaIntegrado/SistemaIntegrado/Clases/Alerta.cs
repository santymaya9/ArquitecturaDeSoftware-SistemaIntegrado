using System;

namespace SistemaIntegrado.Clases
{
    public class Alerta
    {
        private string estado;
        private Ciudadano paciente;
        private float latitud;
        private float longitud;
        private DateTime fecha;
        private DateTime fecha_finalizacion;
        private Ruta rutas;
        private Paramedico paramedicoAsignado;

        public string Estado
        {
            get => string.IsNullOrWhiteSpace(estado) ? "Sin estado" : estado;
            set => estado = value;
        }
        public Ciudadano Paciente
        {
            get => paciente;
            set => paciente = value;
        }
        public float Latitud
        {
            get => latitud;
            set => latitud = value;
        }
        public float Longitud
        {
            get => longitud;
            set => longitud = value;
        }
        public DateTime Fecha
        {
            get => fecha == default ? DateTime.MinValue : fecha;
            set => fecha = value;
        }
        public DateTime FechaFinalizacion
        {
            get => fecha_finalizacion == default ? DateTime.MinValue : fecha_finalizacion;
            set => fecha_finalizacion = value;
        }
        public Ruta Rutas
        {
            get => rutas;
            set => rutas = value;
        }
        public Paramedico ParamedicoAsignado
        {
            get => paramedicoAsignado;
            set => paramedicoAsignado = value;
        }

        public Alerta(string estado, Ciudadano paciente, float latitud, float longitud, DateTime fecha, DateTime fecha_finalizacion, Ruta rutas, Paramedico paramedicoAsignado)
        {
            this.estado = estado;
            this.paciente = paciente;
            this.latitud = latitud;
            this.longitud = longitud;
            this.fecha = fecha;
            this.fecha_finalizacion = fecha_finalizacion;
            this.rutas = rutas;
            this.paramedicoAsignado = paramedicoAsignado;
        }
    }

}