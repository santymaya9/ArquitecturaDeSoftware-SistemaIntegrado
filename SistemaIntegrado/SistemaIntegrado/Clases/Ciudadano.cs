using System;

namespace SistemaIntegrado.Clases
{
    public class Ciudadano : Perfil
    {
        
        private Historia_Clinica historia_clinica;
        private float latitud;
        private float longitud;

     
        public Historia_Clinica Historia_clinica
        {
            get => historia_clinica;
            set => historia_clinica = value;
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

      
        public Ciudadano(string nombre, string correo, long celular, string tipo_cedula, int cedula, string contrasena, Historia_Clinica historia_clinica, float latitud, float longitud)
            : base(nombre, correo, celular, tipo_cedula, cedula, contrasena)
        {
            this.historia_clinica = historia_clinica;
            this.latitud = latitud;
            this.longitud = longitud;
        }
    }
}