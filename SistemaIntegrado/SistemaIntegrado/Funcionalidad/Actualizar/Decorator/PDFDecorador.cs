using System;
using System.IO;
using SistemaIntegradoAlertas.Funcionalidad.Actualizar.Decorator;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Decorator
{
    public class PDFDecorador : BaseDescargasDecorador
    {
        private readonly string directorio;
        private readonly string nombreArchivo;

        public string Directorio => directorio;
        public string NombreArchivo => nombreArchivo;
        public string RutaCompleta => Path.Combine(directorio, nombreArchivo);

        public PDFDecorador(IDescargar descargador, string directorio = "descargas") : base(descargador)
        {
            this.directorio = directorio;
            this.nombreArchivo = $"Historial_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            
            // Asegurar que el directorio exista
            if (!string.IsNullOrEmpty(directorio) && !Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }
        }

        public override void Descargar(string historial)
        {
            // Primero ejecutamos la funcionalidad del componente decorado
            base.Descargar(historial);
            
            // Luego a�adimos nuestra funcionalidad de exportaci�n a PDF directamente aqu�
            if (string.IsNullOrEmpty(historial))
                return;
                
            try 
            {
                // En una implementaci�n real, aqu� utilizar�amos una biblioteca 
                // para la generaci�n de PDF como iTextSharp o similar
                string rutaCompleta = Path.Combine(directorio, nombreArchivo);
                
                // Simulamos la escritura del archivo sin usar Console.WriteLine
                File.WriteAllText(rutaCompleta, $"PDF simulado - {historial}");
                
                // Podr�amos retornar el nombre del archivo generado o alguna informaci�n
                // de estado de la exportaci�n para que sea utilizada por quien llame a este m�todo
            }
            catch (Exception)
            {
                // Manejo silencioso de excepciones o lanzamiento a un nivel superior
                // seg�n la pol�tica de manejo de errores de la aplicaci�n
            }
        }
    }
}