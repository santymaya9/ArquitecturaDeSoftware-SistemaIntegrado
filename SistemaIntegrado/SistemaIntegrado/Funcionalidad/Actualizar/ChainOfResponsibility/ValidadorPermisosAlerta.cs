using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.ChainOfResponsibility
{
    public class ValidadorPermisosAlerta : ValidadorActualizacion<Alerta>
    {
        private readonly Perfil usuarioActual;

        public ValidadorPermisosAlerta(Perfil usuarioActual)
        {
            this.usuarioActual = usuarioActual ?? throw new ArgumentNullException(nameof(usuarioActual));
        }

        protected override string Validar(Alerta alerta, object cambios)
        {
            if (usuarioActual is Admin)
            {
                return null; // Admins pueden actualizar todo
            }

            if (usuarioActual is Operador)
            {
                return null; // Operadores pueden actualizar alertas
            }

            if (usuarioActual is Paramedico)
            {
                // Paramédicos solo pueden actualizar alertas asignadas a ellos
                if (alerta.Equipo_asignado?.Cedula == usuarioActual.Cedula)
                {
                    return null;
                }
                return "Error: Paramédico no tiene permisos para actualizar esta alerta";
            }

            return $"Error: Usuario tipo {usuarioActual.GetType().Name} no tiene permisos para actualizar alertas";
        }
    }
}