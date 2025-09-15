using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.ChainOfResponsibility
{
    public class ValidadorIntegridadAlerta : ValidadorActualizacion<Alerta>
    {
        protected override string Validar(Alerta alerta, object cambios)
        {
            if (alerta == null)
            {
                return "Error: La alerta a actualizar no puede ser nula";
            }

            if (alerta.Nivel_triaje < 1 || alerta.Nivel_triaje > 5)
            {
                return "Error: Nivel de triaje debe estar entre 1 y 5";
            }

            if (string.IsNullOrWhiteSpace(alerta.TipoAlerta))
            {
                return "Error: Tipo de alerta no puede estar vacío";
            }

            return null;
        }
    }
}