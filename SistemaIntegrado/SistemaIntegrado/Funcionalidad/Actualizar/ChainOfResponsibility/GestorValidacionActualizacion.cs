using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.ChainOfResponsibility
{
    public class GestorValidacionActualizacion
    {
        private readonly ValidadorActualizacionBase primerValidador;

        public GestorValidacionActualizacion(Perfil usuarioActual)
        {
            // Construir la cadena de validadores
            var validadorPermisos = new ValidadorPermisos(usuarioActual);
            var validadorIntegridad = new ValidadorIntegridad();
            var validadorReglas = new ValidadorReglasNegocio();

            // Establecer la cadena: Permisos -> Integridad -> Reglas de Negocio
            validadorPermisos.EstablecerSiguiente(validadorIntegridad);
            validadorIntegridad.EstablecerSiguiente(validadorReglas);

            primerValidador = validadorPermisos;
        }

        public string ValidarActualizacion(object entidad, object cambios = null)
        {
            return primerValidador.ValidarYProcesar(entidad, cambios);
        }

        public bool PuedeActualizar(object entidad, object cambios = null)
        {
            var error = ValidarActualizacion(entidad, cambios);
            return string.IsNullOrEmpty(error);
        }
    }
}