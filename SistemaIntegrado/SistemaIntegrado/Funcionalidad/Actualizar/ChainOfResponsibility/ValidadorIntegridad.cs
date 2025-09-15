using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.ChainOfResponsibility
{
    public class ValidadorIntegridad : ValidadorActualizacionBase
    {
        protected override string Validar(object entidad, object cambios)
        {
            if (entidad == null)
            {
                return "Error: La entidad a actualizar no puede ser nula";
            }

            // Validaciones espec�ficas por tipo de entidad
            if (entidad is Alerta alerta)
            {
                return ValidarAlerta(alerta, cambios);
            }

            if (entidad is CentroMedico centro)
            {
                return ValidarCentroMedico(centro, cambios);
            }

            if (entidad is Perfil perfil)
            {
                return ValidarPerfil(perfil, cambios);
            }

            return null; // No hay validaciones espec�ficas para este tipo
        }

        private string ValidarAlerta(Alerta alerta, object cambios)
        {
            if (alerta.Nivel_triaje < 1 || alerta.Nivel_triaje > 5)
            {
                return "Error: Nivel de triaje debe estar entre 1 y 5";
            }

            if (string.IsNullOrWhiteSpace(alerta.TipoAlerta))
            {
                return "Error: Tipo de alerta no puede estar vac�o";
            }

            return null;
        }

        private string ValidarCentroMedico(CentroMedico centro, object cambios)
        {
            if (string.IsNullOrWhiteSpace(centro.Nombre))
            {
                return "Error: Nombre del centro m�dico no puede estar vac�o";
            }

            if (centro.Telefono <= 0)
            {
                return "Error: Tel�fono del centro m�dico debe ser v�lido";
            }

            return null;
        }

        private string ValidarPerfil(Perfil perfil, object cambios)
        {
            if (string.IsNullOrWhiteSpace(perfil.Nombre))
            {
                return "Error: Nombre del perfil no puede estar vac�o";
            }

            if (string.IsNullOrWhiteSpace(perfil.Correo) || !perfil.Correo.Contains("@"))
            {
                return "Error: Correo electr�nico debe ser v�lido";
            }

            if (perfil.Celular <= 0)
            {
                return "Error: N�mero de celular debe ser v�lido";
            }

            return null;
        }
    }
}