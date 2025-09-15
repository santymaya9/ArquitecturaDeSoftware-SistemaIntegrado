using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.ChainOfResponsibility
{
    public class ValidadorPermisos : ValidadorActualizacionBase
    {
        private readonly Perfil usuarioActual;

        public ValidadorPermisos(Perfil usuarioActual)
        {
            this.usuarioActual = usuarioActual ?? throw new ArgumentNullException(nameof(usuarioActual));
        }

        protected override string Validar(object entidad, object cambios)
        {
            // Validar permisos seg�n el tipo de usuario
            if (usuarioActual is Admin)
            {
                return null; // Admins pueden actualizar todo
            }

            if (usuarioActual is Operador && entidad is Alerta)
            {
                return null; // Operadores pueden actualizar alertas
            }

            if (usuarioActual is Paramedico && entidad is Alerta alerta)
            {
                // Param�dicos solo pueden actualizar alertas asignadas a ellos
                if (alerta.Equipo_asignado?.Cedula == usuarioActual.Cedula)
                {
                    return null;
                }
                return "Error: Param�dico no tiene permisos para actualizar esta alerta";
            }

            return $"Error: Usuario tipo {usuarioActual.GetType().Name} no tiene permisos para actualizar {entidad?.GetType().Name}";
        }
    }
}