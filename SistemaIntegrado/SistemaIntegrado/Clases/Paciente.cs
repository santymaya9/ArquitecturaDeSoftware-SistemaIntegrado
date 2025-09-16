using System;
using System.Collections.Generic;

namespace SistemaIntegrado.Clases
{
    public class Paciente : Perfil
    {
        private string historialMedico;
        private DateTime fechaNacimiento;
        private int edad;

        public string HistorialMedico
        {
            get => historialMedico;
            set => historialMedico = value;
        }

        public DateTime FechaNacimiento
        {
            get => fechaNacimiento;
            set => fechaNacimiento = value;
        }

        public int Edad
        {
            get => edad;
            set => edad = value;
        }

        public Paciente(string nombre, string cedula, int edad)
            : base(nombre, "paciente@ejemplo.com", 0, "CC", int.Parse(cedula), "password")
        {
            this.historialMedico = "Sin historial";
            this.fechaNacimiento = DateTime.Now.AddYears(-edad);
            this.edad = edad;
        }

        public Paciente(string nombre, string correo, int celular, string tipo_cedula, int cedula, string contrasena, string historialMedico, DateTime fechaNacimiento, int edad)
            : base(nombre, correo, celular, tipo_cedula, cedula, contrasena)
        {
            this.historialMedico = historialMedico;
            this.fechaNacimiento = fechaNacimiento;
            this.edad = edad;
        }
    }
}