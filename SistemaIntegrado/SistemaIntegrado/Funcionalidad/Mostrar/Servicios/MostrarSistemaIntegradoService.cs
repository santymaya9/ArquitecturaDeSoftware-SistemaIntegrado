using System;
using SistemaIntegrado.Funcionalidad.Mostrar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Mostrar.Servicios
{
    public class MostrarSistemaIntegradoService : IMostrar<SistemaIntegradoAlertas.Clases.Singleton.SistemaIntegrado>
    {
        public string Mostrar(SistemaIntegradoAlertas.Clases.Singleton.SistemaIntegrado entidad)
        {
            if (entidad != null)
            {
                return $"Sistema: {entidad.Nombre}, Tel�fono: {entidad.Telefono}, Cuentas registradas: {entidad.L_cuentas?.Count ?? 0}, Centros m�dicos: {entidad.L_centroMedico?.Count ?? 0}";
            }
            return "No hay informaci�n del sistema";
        }
    }
}