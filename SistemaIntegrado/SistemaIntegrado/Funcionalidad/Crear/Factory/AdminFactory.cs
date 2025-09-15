using System;
using SistemaIntegrado.Clases;
using SistemaIntegradoAlertas.Funcionalidad.Crear.Factory;

namespace SistemaIntegrado.Funcionalidad.Crear.Factory
{
    public class AdminFactory : IPerfilFactory
    {
        public Perfil CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, uint celular, string contrasena, params object[] extras)
        {
            // Para Admin: extras[0] = int numAdmin
            int numAdmin = extras.Length > 0 && extras[0] is int ? (int)extras[0] : 1;
            
            return new Admin(nombre, correo, (int)celular, tipoCedula, (int)cedula, contrasena, numAdmin);
        }
    }
}