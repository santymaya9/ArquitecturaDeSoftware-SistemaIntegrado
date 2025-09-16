using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegradoAlertas.Funcionalidad.Crear.Factory
{
    public interface IPerfilFactory
    {
        Perfil CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, long celular, string contrasena, params object[] extras);
    }
}