using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Observer
{
    public class PacienteObserver : IObserver<Alerta>
    {
        public string Update(Alerta elemento)
        {
            // Manejar caso de alerta nula
            if (elemento == null)
            {
                return "[PACIENTE] Alerta recibida pero sin información.";
            }
            
            // Crear mensaje básico de notificación
            string resultado = $"[PACIENTE] Alerta recibida: {elemento.TipoAlerta}";
            
            // Crear mensaje personalizado para el paciente
            string mensajePaciente = $"Estimado paciente, se le informa que tiene una alerta de tipo {elemento.TipoAlerta} ";
            
            if (elemento.Equipo_asignado != null)
            {
                mensajePaciente += $"y será atendido por el equipo {elemento.Equipo_asignado.Nombre}.";
            }
            else
            {
                mensajePaciente += "y pronto se le asignará un equipo médico.";
            }
            
            resultado += $"\n[PACIENTE] Mensaje para paciente: {mensajePaciente}";
            
            return resultado;
        }
    }
}