using System;
using System.IO;
using SistemaIntegradoAlertas.Funcionalidad.Actualizar.Decorator;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Servicios
{
    /// <summary>
    /// Implementación concreta del componente descargador
    /// </summary>
    public class DescargadorConcreto : IDescargar
    {
        private readonly string directorio;

        public DescargadorConcreto(string directorio = "descargas")
        {
            this.directorio = directorio;
            
            // Asegurar que el directorio exista
            if (!string.IsNullOrEmpty(directorio) && !Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }
        }

        public void Descargar(string historial)
        {
            if (string.IsNullOrEmpty(historial))
                return;

            try
            {
                string nombreArchivo = $"Historial_Base_{DateTime.Now:yyyyMMddHHmmss}.txt";
                string rutaCompleta = Path.Combine(directorio, nombreArchivo);
                
                // Guardar el historial en un archivo de texto simple
                File.WriteAllText(rutaCompleta, historial);
            }
            catch (Exception)
            {
                // Manejo silencioso de excepciones o propagación según política de la aplicación
            }
        }
    }
}