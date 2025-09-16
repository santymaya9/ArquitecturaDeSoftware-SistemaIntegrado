using System;
using System.IO;
using SistemaIntegradoAlertas.Funcionalidad.Actualizar.Decorator;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Decorator
{
    public class ExcelDecorador : BaseDescargasDecorador
    {
        private readonly string directorio;
        private readonly string nombreArchivo;

        public string Directorio => directorio;
        public string NombreArchivo => nombreArchivo;
        public string RutaCompleta => Path.Combine(directorio, nombreArchivo);
        
        // Propiedad para almacenar el último error
        public string UltimoError { get; private set; }

        public ExcelDecorador(IDescargar descargador, string directorio = "descargas") : base(descargador)
        {
            this.directorio = directorio;
            this.nombreArchivo = $"Historial_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            
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
            
            // Luego añadimos nuestra funcionalidad de exportación a Excel directamente aquí
            if (string.IsNullOrEmpty(historial))
            {
                UltimoError = "Error: No se puede exportar a Excel un historial vacío o nulo";
                return;
            }
                
            try 
            {
                // En una implementación real, aquí utilizaríamos una biblioteca 
                // para la generación de archivos Excel como EPPlus, ClosedXML o similar
                string rutaCompleta = Path.Combine(directorio, nombreArchivo);
                
                // Simulamos la escritura del archivo sin usar Console.WriteLine
                File.WriteAllText(rutaCompleta, $"Excel simulado - {historial}");
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otra excepción no prevista
                UltimoError = $"Error inesperado al generar el archivo Excel: {ex.Message}";
                throw new Exception(UltimoError, ex);
            }
        }
    }
}