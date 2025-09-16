using System;
using System.IO;
using SistemaIntegradoAlertas.Funcionalidad.Actualizar.Decorator;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Decorator
{
    public class DescargadorConcreto : IDescargar
    {
        public void Descargar(string historial)
        {
            if (string.IsNullOrEmpty(historial))
                return;

            try
            {
                // Aqu� implementamos la descarga b�sica del historial
                // No usamos Console.WriteLine siguiendo instrucciones previas
                // Podr�amos guardar en un archivo o hacer otra operaci�n seg�n requisitos
                
                // Ejemplo: guardar en un archivo temporal
                string tempPath = Path.GetTempPath();
                string filePath = Path.Combine(tempPath, $"Historial_{DateTime.Now:yyyyMMddHHmmss}.txt");
                File.WriteAllText(filePath, historial);
            }
            catch (Exception)
            {
                // Manejo silencioso de excepciones
            }
        }
    }
}