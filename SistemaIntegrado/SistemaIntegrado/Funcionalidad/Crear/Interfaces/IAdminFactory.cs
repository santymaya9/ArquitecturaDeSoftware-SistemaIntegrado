using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Crear.Interfaces
{
    public interface IAdminFactory
    {
        Admin CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, uint celular, string contrasena, int numAdmin);
    }
}