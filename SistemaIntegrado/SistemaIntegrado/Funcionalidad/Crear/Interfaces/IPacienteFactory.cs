using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Crear.Interfaces
{
    public interface IPacienteFactory
    {
        Ciudadano CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, uint celular, string contrasena, Historia_Clinica historia, float latitud, float longitud);
    }
}