using System;
using System.Collections.Generic;
using SistemaIntegrado.Clases;
using SistemaIntegradoAlertas.Funcionalidad.Crear.Factory;

namespace SistemaIntegrado.Funcionalidad.Crear.Factory
{
    public class OperadorFactory : IPerfilFactory
    {
        public Perfil CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, long celular, string contrasena, params object[] extras)
        {
            // Para Operador: extras[0] = int numOperador, extras[1] = List<Alerta> alertas
            int numOperador = extras.Length > 0 && extras[0] is int ? (int)extras[0] : 1;
            List<Alerta> alertas = extras.Length > 1 && extras[1] is List<Alerta> ? (List<Alerta>)extras[1] : new List<Alerta>();
            
            return new Operador(nombre, correo, celular, tipoCedula, (int)cedula, contrasena, numOperador, alertas);
        }
    }
}