using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Actualizar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Servicios
{
    public class ActualizarCuentaService : IActualizarCuenta
    {
        public void Actualizar(string nombre, string correo, string tipo_cedula, string cedula, uint celular, int contrasena, string tipo_registro, Perfil cuenta)
        {
            if (cuenta != null)
            {
                if (!string.IsNullOrWhiteSpace(nombre))
                    cuenta.Nombre = nombre;
                
                if (!string.IsNullOrWhiteSpace(correo))
                    cuenta.Correo = correo;
                
                if (!string.IsNullOrWhiteSpace(tipo_cedula))
                    cuenta.TipoCedula = tipo_cedula;
                
                if (!string.IsNullOrWhiteSpace(cedula) && int.TryParse(cedula, out int cedulaNum))
                    cuenta.Cedula = cedulaNum;
                
                if (celular > 0)
                    cuenta.Celular = (int)celular;
                
                if (contrasena > 0)
                    cuenta.Contrasena = contrasena.ToString();
            }
        }
    }
}