using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Mostrar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Mostrar.Servicios
{
    public class MostrarSistemaIntegradoService : IMostrar<SistemaIntegrado.Clases.SistemaIntegrado>
    {
        public string Mostrar(SistemaIntegrado.Clases.SistemaIntegrado entidad)
        {
            if (entidad != null)
            {
                return $"Sistema: {entidad.Nombre}, Tel�fono: {entidad.Telefono}, Cuentas registradas: {entidad.L_cuentas?.Count ?? 0}, Centros m�dicos: {entidad.L_centroMedico?.Count ?? 0}";
            }
            return "No hay informaci�n del sistema";
        }
    }
}