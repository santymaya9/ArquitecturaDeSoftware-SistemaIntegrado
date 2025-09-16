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

        public ExcelDecorador(IDescargar descargador, string directorio = "descargas") : base(descargador)
        {
            this.directorio = directorio;
            this.nombreArchivo = $"Historial_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            
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
            
            // Luego añadimos nuestra funcionalidad de exportación a Excel directamente aquí
            if (string.IsNullOrEmpty(historial))
                return;
                
            try 
            {
                // En una implementación real, aquí utilizaríamos una biblioteca 
                // para la generación de archivos Excel como EPPlus, ClosedXML o similar
                string rutaCompleta = Path.Combine(directorio, nombreArchivo);
                
                // Simulamos la escritura del archivo sin usar Console.WriteLine
                File.WriteAllText(rutaCompleta, $"Excel simulado - {historial}");
                
                // Aquí se aplicarían formatos, se crearían hojas, celdas, etc.
                // según los requisitos específicos de la aplicación
            }
            catch (Exception)
            {
                // Manejo silencioso de excepciones o lanzamiento a un nivel superior
                // según la política de manejo de errores de la aplicación
            }
        }
    }
}