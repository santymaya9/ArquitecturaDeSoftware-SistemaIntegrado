using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Actualizar.Decorator;
using SistemaIntegradoAlertas.Funcionalidad.Actualizar.Decorator;

namespace SistemaIntegrado.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("===== SISTEMA INTEGRADO DE ALERTAS M�DICAS =====");
            Console.WriteLine("Demostraci�n del Patr�n Decorator para Descargas de Historiales M�dicos\n");
            
            // Crear directorios para las diferentes versiones de descarga
            string directorioBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SistemaIntegrado");
            string directorioPDF = Path.Combine(directorioBase, "PDFs");
            string directorioExcel = Path.Combine(directorioBase, "Excel");
            
            // Crear los directorios si no existen
            Directory.CreateDirectory(directorioBase);
            Directory.CreateDirectory(directorioPDF);
            Directory.CreateDirectory(directorioExcel);
            
            Console.WriteLine("Directorios de descarga:");
            Console.WriteLine($" - Base: {directorioBase}");
            Console.WriteLine($" - PDFs: {directorioPDF}");
            Console.WriteLine($" - Excel: {directorioExcel}");
            
            // Crear un ciudadano de ejemplo con su historia cl�nica
            List<string> enfermedadesPreexistentes = new List<string> 
            {
                "Hipertensi�n arterial", 
                "Diabetes tipo 2",
                "Asma"
            };
            
            var historiaClinica = new Historia_Clinica(
                tipo_sangre: "O+", 
                edad: 45, 
                marcapasos: false, 
                municipio: "Medell�n", 
                l_enfermedades_preexistentes: enfermedadesPreexistentes
            );
            
            var ciudadano = new Ciudadano(
                nombre: "Juan P�rez",
                correo: "juan.perez@ejemplo.com",
                celular: 301234567,
                tipo_cedula: "CC",
                cedula: 12345678,
                contrasena: "clave123",
                historia_clinica: historiaClinica,
                latitud: 6.2476f,
                longitud: -75.5658f
            );
            
            // Convertir la historia cl�nica a un formato textual para ser descargado
            string historialMedico = FormatearHistorialMedico(ciudadano);
            
            Console.WriteLine("\nCiudadano creado con su historial m�dico:");
            Console.WriteLine($"Nombre: {ciudadano.Nombre}");
            Console.WriteLine($"Tipo de sangre: {ciudadano.Historia_clinica.TipoSangre}");
            Console.WriteLine($"Municipio: {ciudadano.Historia_clinica.Municipio}");
            
            // DEMOSTRACI�N DEL PATR�N DECORATOR
            Console.WriteLine("\n=== DEMOSTRACI�N DEL PATR�N DECORATOR ACTUALIZADO ===");
            
            // 1. Componente concreto base
            var descargadorBase = new DescargadorConcreto();
            Console.WriteLine("\n1. Usando componente concreto base:");
            descargadorBase.Descargar(historialMedico);
            Console.WriteLine("   Descarga b�sica completada.");
            
            // 2. Uso del decorador PDF con las propiedades accesoras
            var pdfDecorator = new PDFDecorador(descargadorBase, directorioPDF);
            Console.WriteLine("\n2. Usando decorador PDF:");
            Console.WriteLine($"   Directorio: {pdfDecorator.Directorio}");
            Console.WriteLine($"   Nombre archivo: {pdfDecorator.NombreArchivo}");
            Console.WriteLine($"   Ruta completa: {pdfDecorator.RutaCompleta}");
            pdfDecorator.Descargar(historialMedico);
            Console.WriteLine("   Descarga con PDF completada.");
            
            // 3. Uso del decorador Excel con las propiedades accesoras
            var excelDecorator = new ExcelDecorador(descargadorBase, directorioExcel);
            Console.WriteLine("\n3. Usando decorador Excel:");
            Console.WriteLine($"   Directorio: {excelDecorator.Directorio}");
            Console.WriteLine($"   Nombre archivo: {excelDecorator.NombreArchivo}");
            Console.WriteLine($"   Ruta completa: {excelDecorator.RutaCompleta}");
            excelDecorator.Descargar(historialMedico);
            Console.WriteLine("   Descarga con Excel completada.");
            
            // 4. Uso combinado de decoradores (apilados)
            Console.WriteLine("\n4. Usando decoradores apilados (PDF dentro de Excel):");
            var combinado = new ExcelDecorador(
                new PDFDecorador(descargadorBase, directorioPDF),
                directorioExcel
            );
            
            combinado.Descargar(historialMedico);
            Console.WriteLine("   Descarga combinada completada (ambos formatos generados).");
            
            // 5. Demostraci�n de inyecci�n de dependencias con HistorialAdmin
            Console.WriteLine("\n5. Demostraci�n de inyecci�n de dependencias:");
            var historialAdmin = new Funcionalidad.Actualizar.Inyecciones.HistorialAdmin(excelDecorator);
            Console.WriteLine($"   Se inyect� el decorador Excel en HistorialAdmin");
            Console.WriteLine($"   Accediendo a trav�s de la propiedad Descargas:");
            historialAdmin.Descargas.Descargar(historialMedico);
            Console.WriteLine("   Descarga completada a trav�s de HistorialAdmin.");
            
            // Mostrar informaci�n sobre los archivos generados
            Console.WriteLine("\nARCHIVOS GENERADOS:");
            MostrarArchivosEnDirectorio(directorioBase, "Base");
            MostrarArchivosEnDirectorio(directorioPDF, "PDFs");
            MostrarArchivosEnDirectorio(directorioExcel, "Excel");

            Console.WriteLine("\n=== FIN DE LA DEMOSTRACI�N ===");
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
        
        private static void MostrarArchivosEnDirectorio(string directorio, string tipo)
        {
            Console.WriteLine($"\nArchivos en directorio de {tipo}:");
            if (!Directory.Exists(directorio))
            {
                Console.WriteLine("  No se encontr� el directorio.");
                return;
            }
            
            var archivos = Directory.GetFiles(directorio);
            if (archivos.Length == 0)
            {
                Console.WriteLine("  No hay archivos en el directorio.");
                return;
            }
            
            foreach (var archivo in archivos)
            {
                var info = new FileInfo(archivo);
                Console.WriteLine($"  - {info.Name} ({info.Length} bytes)");
            }
        }
        
        private static string FormatearHistorialMedico(Ciudadano ciudadano)
        {
            var sb = new StringBuilder();
            var historia = ciudadano.Historia_clinica;
            
            sb.AppendLine($"HISTORIAL M�DICO DE {ciudadano.Nombre.ToUpper()}");
            sb.AppendLine($"Documento: {ciudadano.TipoCedula} {ciudadano.Cedula}");
            sb.AppendLine($"Fecha: {DateTime.Now:yyyy-MM-dd}");
            sb.AppendLine(new string('-', 40));
            
            sb.AppendLine("DATOS PERSONALES:");
            sb.AppendLine($"Edad: {historia.Edad} a�os");
            sb.AppendLine($"Municipio: {historia.Municipio}");
            sb.AppendLine($"Contacto: {ciudadano.Celular} / {ciudadano.Correo}");
            sb.AppendLine($"Ubicaci�n actual: [{ciudadano.Latitud}, {ciudadano.Longitud}]");
            sb.AppendLine(new string('-', 40));
            
            sb.AppendLine("INFORMACI�N M�DICA:");
            sb.AppendLine($"Tipo de sangre: {historia.TipoSangre}");
            sb.AppendLine($"Marcapasos: {(historia.Marcapasos ? "S�" : "No")}");
            
            sb.AppendLine("\nENFERMEDADES PREEXISTENTES:");
            if (historia.EnfermedadesPreexistentes != null && historia.EnfermedadesPreexistentes.Count > 0)
            {
                foreach (var enfermedad in historia.EnfermedadesPreexistentes)
                {
                    sb.AppendLine($"  - {enfermedad}");
                }
            }
            else
            {
                sb.AppendLine("  No se registran enfermedades preexistentes");
            }
            
            sb.AppendLine(new string('-', 40));
            sb.AppendLine("Este documento es confidencial y est� sujeto a normativas de protecci�n de datos de salud.");
            
            return sb.ToString();
        }
    }
}