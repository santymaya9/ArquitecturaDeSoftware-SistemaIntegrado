using System;
using SistemaIntegrado.Clases;
using SistemaIntegradoAlertas.Funcionalidad.Crear.Factory;

namespace SistemaIntegrado.Funcionalidad.Crear.Factory
{
    public class PacienteFactory : IPerfilFactory
    {
        public Perfil CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, uint celular, string contrasena, params object[] extras)
        {
            // Para Paciente: extras[0] = Historia_Clinica, extras[1] = float latitud, extras[2] = float longitud
            Historia_Clinica historia = extras.Length > 0 && extras[0] is Historia_Clinica ? (Historia_Clinica)extras[0] : 
                new Historia_Clinica("O+", 25, false, "Bogotá");
            float latitud = extras.Length > 1 && extras[1] is float ? (float)extras[1] : 0.0f;
            float longitud = extras.Length > 2 && extras[2] is float ? (float)extras[2] : 0.0f;
            
            return new Ciudadano(nombre, correo, (int)celular, tipoCedula, (int)cedula, contrasena, historia, latitud, longitud);
        }
    }
}