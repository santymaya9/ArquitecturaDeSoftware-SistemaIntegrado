using System;

namespace SistemaIntegrado.Clases
{
    public class Ruta
    {
        private float latitud;
        private float longitud;
        private int num_nodo;

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
        public int NumNodo
        {
            get => num_nodo == default ? -1 : num_nodo;
            set => num_nodo = value;
        }

        public Ruta(float latitud, float longitud, int num_nodo)
        {
            this.latitud = latitud;
            this.longitud = longitud;
            this.num_nodo = num_nodo;
        }
    }

}