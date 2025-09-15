using System;

namespace SistemaIntegrado.Clases
{
    public abstract class Perfil
    {
     
        private string nombre;
        private string correo;
        private int celular;
        private string tipo_cedula;
        private int cedula;
        private string contrasena;

  
        public string Nombre
        {
            get => string.IsNullOrWhiteSpace(nombre) ? "Sin nombre" : nombre;
            set => nombre = value;
        }
        public string Correo
        {
            get => string.IsNullOrWhiteSpace(correo) ? "Sin correo" : correo;
            set => correo = value;
        }
        public int Celular
        {
            get => celular;
            set => celular = value;
        }
        public string TipoCedula
        {
            get => tipo_cedula;
            set => tipo_cedula = value;
        }
        public int Cedula
        {
            get => cedula;
            set => cedula = value;
        }
        public string Contrasena
        {
            get => string.IsNullOrWhiteSpace(contrasena) ? "Sin contraseña" : contrasena;
            set => contrasena = value;
        }

        public Perfil(string nombre, string correo, int celular, string tipo_cedula, int cedula, string contrasena)
        {
            
            this.nombre = nombre;
            this.correo = correo;
            this.celular = celular;
            this.tipo_cedula = tipo_cedula;
            this.cedula = cedula;
            this.contrasena = contrasena;
        }
    }
}