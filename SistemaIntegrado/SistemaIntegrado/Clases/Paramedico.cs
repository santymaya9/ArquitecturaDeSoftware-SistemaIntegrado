using System;
using System.Collections.Generic;

namespace SistemaIntegrado.Clases
{
    public class Paramedico : Perfil
    {
        private int numParamedico;
        private List<Alerta> alertasAsignadas;
        private int limiteAlertas;

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

        public Paramedico(uint id, string nombre, string correo, int celular, string tipo_cedula, int cedula, string contrasena, int numParamedico, int limiteAlertas, List<Alerta>? alertasAsignadas = null)
            : base(nombre, correo, celular, tipo_cedula, cedula, contrasena)
        {
            this.numParamedico = numParamedico;
            this.limiteAlertas = limiteAlertas;
            this.alertasAsignadas = alertasAsignadas ?? new List<Alerta>();
        }
    }
}