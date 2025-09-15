using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Mostrar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Mostrar.Servicios
{
    public class MostrarCuentaService : IMostrar<Cuenta>
    {
        public string Mostrar(Cuenta entidad)
        {
            if (entidad != null)
            {
                string estado = entidad.EstadoActivo ? "Activa" : "Inactiva";
                return $"Cuenta: {entidad.Perfil}, Fecha de creaci�n: {entidad.FechaCreacion:dd/MM/yyyy}, Estado: {estado}";
            }
            return "No hay informaci�n de la cuenta";
        }
    }
}