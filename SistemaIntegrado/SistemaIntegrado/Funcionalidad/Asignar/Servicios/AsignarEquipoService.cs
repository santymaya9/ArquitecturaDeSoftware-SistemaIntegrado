using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Servicios.Asignar.Interfaces;

namespace SistemaIntegrado.Servicios.Asignar.Servicios
{
    public class AsignarEquipoService : IAsignarEquipo
    {
        public string Asignar(Perfil perfil)
        {
            if (perfil != null)
            {
                return $"Equipo asignado exitosamente: {perfil.Nombre} (Cédula: {perfil.Cedula})";
            }
            else
            {
                return "Error: No se pudo asignar el equipo. Perfil nulo.";
            }
        }
    }
}