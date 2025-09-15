using System;
using System.Collections.Generic;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Crear.Interfaces
{
    public interface IOperadorFactory
    {
        Operador CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, uint celular, string contrasena, int numOperador, List<Alerta> alertas);
    }
}