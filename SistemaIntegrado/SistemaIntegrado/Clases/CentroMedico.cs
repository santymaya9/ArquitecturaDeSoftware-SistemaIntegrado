using System;

namespace SistemaIntegrado.Clases
{
    public class CentroMedico
    {
        private float latitud;
        private string nombre;
        private float longitud;
        private string complejidad;
        private int telefono;

        public float Latitud
        {
            get => latitud;
            set => latitud = value;
        }
        public string Nombre
        {
            get => string.IsNullOrWhiteSpace(nombre) ? "Sin nombre" : nombre;
            set => nombre = value;
        }
        public float Longitud
        {
            get => longitud;
            set => longitud = value;
        }
        public string Complejidad
        {
            get => string.IsNullOrWhiteSpace(complejidad) ? "Sin complejidad" : complejidad;
            set => complejidad = value;
        }
        public int Telefono
        {
            get => telefono;
            set => telefono = value;
        }

        public CentroMedico(string nombre, float latitud, float longitud, string complejidad, int telefono)
        {
            this.nombre = nombre;
            this.latitud = latitud;
            this.longitud = longitud;
            this.complejidad = complejidad;
            this.telefono = telefono;
        }
    }
}