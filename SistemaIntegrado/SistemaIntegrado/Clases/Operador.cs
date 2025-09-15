using System;

namespace SistemaIntegrado.Clases
{
    public class Operador : Perfil
    {
        private int num_operador;
        private List<Alerta> l_alertasEmergencia;

        public int NumOperador
        {
            get => num_operador;
            set => num_operador = value;
        }
        public List<Alerta> AlertasEmergencia
        {
            get => l_alertasEmergencia;
            set => l_alertasEmergencia = value;
        }

        public Operador(string nombre, string correo, int celular, string tipo_cedula, int cedula, string contrasena, int num_operador, List<Alerta> l_alertasEmergencia)
            : base(nombre, correo, celular, tipo_cedula, cedula, contrasena)
        {
            this.num_operador = num_operador;
            this.l_alertasEmergencia = l_alertasEmergencia ?? new List<Alerta>();
        }
    }
}