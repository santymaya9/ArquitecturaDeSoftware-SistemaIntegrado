using System;
using System.Collections.Generic;

namespace SistemaIntegrado.Clases
{
    public class Paramedico : Perfil
    {
        private int numParamedico;
        private List<Alerta> alertasAsignadas;
        private int limiteAlertas;
        private string unidad;

        public int NumParamedico
        {
            get => numParamedico;
            set => numParamedico = value;
        }
        
        public List<Alerta> AlertasAsignadas
        {
            get => alertasAsignadas;
            set => alertasAsignadas = value;
        }
     
        public int LimiteAlertas
        {
            get => limiteAlertas;
            set => limiteAlertas = value;
        }
        
        public string Unidad
        {
            get => unidad;
            set => unidad = value;
        }

        // Constructor simplificado para el ejemplo
        public Paramedico(string nombre, string cedula, int edad, string unidad)
            : base(nombre, "paramedico@ejemplo.com", 0, "CC", int.Parse(cedula), "password")
        {
            this.numParamedico = 1;
            this.limiteAlertas = 5;
            this.alertasAsignadas = new List<Alerta>();
            this.unidad = unidad;
        }

        // Constructor completo original
        public Paramedico(uint id, string nombre, string correo, int celular, string tipo_cedula, int cedula, string contrasena, int numParamedico, int limiteAlertas, List<Alerta>? alertasAsignadas = null)
            : base(nombre, correo, celular, tipo_cedula, cedula, contrasena)
        {
            this.numParamedico = numParamedico;
            this.limiteAlertas = limiteAlertas;
            this.alertasAsignadas = alertasAsignadas ?? new List<Alerta>();
            this.unidad = "Sin asignar";
        }
    }
}