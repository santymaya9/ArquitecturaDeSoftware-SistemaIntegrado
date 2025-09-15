using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Crear.Interfaces
{
    public interface IParamedicoFactory
    {
        Paramedico CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, uint celular, string contrasena, uint id, int numParamedico, int limiteAlertas);
    }
}