using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.ChainOfResponsibility
{
    public class GestorValidacionAlerta
    {
        private readonly ValidadorActualizacion<Alerta> primerValidador;

        public GestorValidacionAlerta(Perfil usuarioActual)
        {
            // Construir la cadena específica para alertas
            var validadorPermisos = new ValidadorPermisosAlerta(usuarioActual);
            var validadorIntegridad = new ValidadorIntegridadAlerta();
            var validadorReglas = new ValidadorReglasNegocioAlerta();

            // Establecer la cadena: Permisos -> Integridad -> Reglas de Negocio
            validadorPermisos.EstablecerSiguiente(validadorIntegridad);
            validadorIntegridad.EstablecerSiguiente(validadorReglas);

            primerValidador = validadorPermisos;
        }

        public string ValidarActualizacion(Alerta alerta, object cambios = null)
        {
            return primerValidador.ValidarYProcesar(alerta, cambios);
        }

        public bool PuedeActualizar(Alerta alerta, object cambios = null)
        {
            var error = ValidarActualizacion(alerta, cambios);
            return string.IsNullOrEmpty(error);
        }
    }
}