using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Modelos.Demo
{
    /// <summary>
    /// Clases de modelo para demos del sistema
    /// </summary>
    
    // Clase para operadores de demostración
    public class OperadorDemo : Perfil
    {
        public int NumOperador { get; set; }
        
        public OperadorDemo(string nombre, string correo, int celular, string tipoCedula, int cedula, string contrasena, int numOperador) 
            : base(nombre, correo, celular, tipoCedula, cedula, contrasena)
        {
            NumOperador = numOperador;
        }
    }
    
    // Clase para paramédicos de demostración
    public class ParamedicoDemo : Perfil
    {
        public int NumParamedico { get; set; }
        public int LimiteAlertas { get; set; }
        
        public ParamedicoDemo(string nombre, string correo, int celular, string tipoCedula, int cedula, string contrasena, int numParamedico, int limiteAlertas) 
            : base(nombre, correo, celular, tipoCedula, cedula, contrasena)
        {
            NumParamedico = numParamedico;
            LimiteAlertas = limiteAlertas;
        }
    }
    
    // Clase para pacientes de demostración
    public class PacienteDemo : Perfil
    {
        public string GrupoSanguineo { get; set; }
        
        public PacienteDemo(string nombre, string correo, int celular, string tipoCedula, int cedula, string contrasena, string grupoSanguineo) 
            : base(nombre, correo, celular, tipoCedula, cedula, contrasena)
        {
            GrupoSanguineo = grupoSanguineo;
        }
    }
}