using System;

namespace SistemaIntegrado.Clases
{
    public class Paramedico : Perfil
    {
        private int numParamedico;
        private List<AlertaEmergencia> alertasAsignadas;
        private int limiteAlertas;

        public int NumParamedico
        {
            get => numParamedico;
            set => numParamedico = value;
        }
        public List<AlertaEmergencia> AlertasAsignadas
        {
            get => alertasAsignadas;
            set => alertasAsignadas = value;
        }
     
        public int LimiteAlertas
        {
            get => limiteAlertas;
            set => limiteAlertas = value;
        }

        public Paramedico(uint id, string nombre, string correo, int celular, string tipo_cedula, int cedula, string contrasena, int numParamedico, int limiteAlertas, List<AlertaEmergencia> alertasAsignadas = null)
            : base(nombre, correo, celular, tipo_cedula, cedula, contrasena)
        {
            this.numParamedico = numParamedico;
            this.limiteAlertas = limiteAlertas;
            this.alertasAsignadas = alertasAsignadas ?? new List<AlertaEmergencia>();
           
        }
    }
}