using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Interfaces
{
    public interface IActualizarCuenta
    {
        void Actualizar(string nombre, string correo, string tipo_cedula, string cedula, uint celular, int contrasena, string tipo_registro, Perfil cuenta);
    }
}