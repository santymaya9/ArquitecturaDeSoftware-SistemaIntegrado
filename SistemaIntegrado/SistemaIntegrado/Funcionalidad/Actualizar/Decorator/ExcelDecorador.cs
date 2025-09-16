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
            
            // Luego a�adimos nuestra funcionalidad de exportaci�n a Excel directamente aqu�
            if (string.IsNullOrEmpty(historial))
                return;
                
            try 
            {
                // En una implementaci�n real, aqu� utilizar�amos una biblioteca 
                // para la generaci�n de archivos Excel como EPPlus, ClosedXML o similar
                string rutaCompleta = Path.Combine(directorio, nombreArchivo);
                
                // Simulamos la escritura del archivo sin usar Console.WriteLine
                File.WriteAllText(rutaCompleta, $"Excel simulado - {historial}");
                
                // Aqu� se aplicar�an formatos, se crear�an hojas, celdas, etc.
                // seg�n los requisitos espec�ficos de la aplicaci�n
            }
            catch (Exception)
            {
                // Manejo silencioso de excepciones o lanzamiento a un nivel superior
                // seg�n la pol�tica de manejo de errores de la aplicaci�n
            }
        }
    }
}