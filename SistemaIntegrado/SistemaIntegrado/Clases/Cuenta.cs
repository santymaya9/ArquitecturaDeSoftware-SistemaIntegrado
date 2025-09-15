using System;

namespace SistemaIntegrado.Clases
{
    public class Cuenta
    {
        private string perfil;
        private DateTime fecha_creacion;
        private bool estadoActivo;

        public string Perfil
        {
            get => string.IsNullOrWhiteSpace(perfil) ? "Sin perfil" : perfil;
            set => perfil = value;
        }
        public DateTime FechaCreacion
        {
            get => fecha_creacion == default ? DateTime.MinValue : fecha_creacion;
            set => fecha_creacion = value;
        }

        public bool EstadoActivo
        {
            get => estadoActivo;
            set => estadoActivo = value;
        }

        public Cuenta(string perfil, DateTime fecha_creacion)
        {
            this.perfil = perfil;
            this.fecha_creacion = fecha_creacion;
            this.estadoActivo = true; // Por defecto activo
        }
    }
}