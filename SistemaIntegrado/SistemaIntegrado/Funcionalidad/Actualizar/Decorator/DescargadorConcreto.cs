using System;
using System.IO;
using SistemaIntegradoAlertas.Funcionalidad.Actualizar.Decorator;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Decorator
{
    public class DescargadorConcreto : IDescargar
    {
        // Propiedad para almacenar el último mensaje de error
        public string UltimoError { get; private set; }

        public void Descargar(string historial)
        {
            if (string.IsNullOrEmpty(historial))
            {
                UltimoError = "Error: El historial está vacío o es nulo";
                return;
            }

            try
            {
                // Aquí implementamos la descarga básica del historial
                string tempPath = Path.GetTempPath();
                string filePath = Path.Combine(tempPath, $"Historial_{DateTime.Now:yyyyMMddHHmmss}.txt");
                File.WriteAllText(filePath, historial);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier excepción
                UltimoError = $"Error inesperado al descargar el historial: {ex.Message}";
                throw new Exception(UltimoError, ex);
            }
        }
    }
}