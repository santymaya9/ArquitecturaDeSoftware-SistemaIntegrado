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
        
        // Propiedad para almacenar el �ltimo error
        public string UltimoError { get; private set; }

        public PDFDecorador(IDescargar descargador, string directorio = "descargas") : base(descargador)
        {
            this.directorio = directorio;
            this.nombreArchivo = $"Historial_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            
            try
            {
                // Asegurar que el directorio exista
                if (!string.IsNullOrEmpty(directorio) && !Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }
            }
            catch (Exception ex)
            {
                UltimoError = $"Error al crear el directorio '{directorio}': {ex.Message}";
                throw new Exception(UltimoError, ex);
            }
        }

        public override void Descargar(string historial)
        {
            try
            {
                // Primero ejecutamos la funcionalidad del componente decorado
                base.Descargar(historial);
            }
            catch (Exception ex)
            {
                UltimoError = $"Error en el decorador base al descargar: {ex.Message}";
                throw new Exception(UltimoError, ex);
            }
            
            // Luego a�adimos nuestra funcionalidad de exportaci�n a PDF directamente aqu�
            if (string.IsNullOrEmpty(historial))
            {
                UltimoError = "Error: No se puede exportar a PDF un historial vac�o o nulo";
                return;
            }
                
            try 
            {
                // En una implementaci�n real, aqu� utilizar�amos una biblioteca 
                // para la generaci�n de PDF como iTextSharp o similar
                string rutaCompleta = Path.Combine(directorio, nombreArchivo);
                
                // Simulamos la escritura del archivo sin usar Console.WriteLine
                File.WriteAllText(rutaCompleta, $"PDF simulado - {historial}");
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otra excepci�n no prevista
                UltimoError = $"Error inesperado al generar el archivo PDF: {ex.Message}";
                throw new Exception(UltimoError, ex);
            }
        }
    }
}