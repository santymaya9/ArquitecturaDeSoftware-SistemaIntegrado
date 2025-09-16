using System;
using SistemaIntegrado.Clases;
using SistemaIntegradoAlertas.Funcionalidad.Crear.Factory;

namespace SistemaIntegrado.Funcionalidad.Crear.Factory
{
    public class ParamedicoFactory : IPerfilFactory
    {
        public Perfil CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, long celular, string contrasena, params object[] extras)
        {
            // Para Paramedico: extras[0] = uint id, extras[1] = int numParamedico, extras[2] = int limiteAlertas
            uint id = extras.Length > 0 && extras[0] is uint ? (uint)extras[0] : 1;
            int numParamedico = extras.Length > 1 && extras[1] is int ? (int)extras[1] : 1;
            int limiteAlertas = extras.Length > 2 && extras[2] is int ? (int)extras[2] : 5;
            
            return new Paramedico(id, nombre, correo, celular, tipoCedula, (int)cedula, contrasena, numParamedico, limiteAlertas);
        }
    }
}