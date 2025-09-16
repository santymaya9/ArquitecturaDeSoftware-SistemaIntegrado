using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Actualizar.Decorator;
using SistemaIntegradoAlertas.Funcionalidad.Actualizar.Decorator;
using SistemaIntegrado.Funcionalidad.Actualizar.Observer;
using SistemaIntegrado.Funcionalidad.Comunicacion.Decorator;
using SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces;
using SistemaIntegrado.Funcionalidad.Comunicacion.Implementaciones;
using SistemaIntegrado.Funcionalidad.Crear.Factory;
using SistemaIntegrado.Funcionalidad.Mostrar.Interfaces;
using SistemaIntegrado.Funcionalidad.Mostrar.Servicios;
using SistemaIntegrado.Funcionalidad.Mostrar.Decorator;

namespace SistemaIntegrado.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("===== SISTEMA INTEGRADO DE ALERTAS M�DICAS =====");
            Console.WriteLine("Demostraci�n de patrones de dise�o implementados\n");
            
            // Configuraci�n inicial - Crear directorios para descargas
            string directorioBase = Path.Combine(Path.GetTempPath(), "SistemaIntegrado");
            string directorioPDF = Path.Combine(directorioBase, "PDFs");
            string directorioExcel = Path.Combine(directorioBase, "Excel");
            
            Directory.CreateDirectory(directorioBase);
            Directory.CreateDirectory(directorioPDF);
            Directory.CreateDirectory(directorioExcel);
            
            // ======================================================
            // 1. DEMOSTRACI�N DEL PATR�N FACTORY
            // ======================================================
            Console.WriteLine("\n=== DEMOSTRACI�N DEL PATR�N FACTORY ===");
            
            // Crear perfiles usando f�bricas
            var adminFactory = new AdminFactory();
            var pacienteFactory = new PacienteFactory();
            var paramedicoFactory = new ParamedicoFactory();
            var operadorFactory = new OperadorFactory();
            
            // Usando n�meros de celular v�lidos en Colombia (10 d�gitos)
            var admin = adminFactory.CrearPerfil(
                "Admin Sistema", "admin@sistema.com", "CC", 1000001, 3101234567, "admin123");
            
            // Datos extras para crear un paciente (Historia_Clinica)
            var enfermedadesPreexistentes = new List<string> { "Hipertensi�n", "Diabetes tipo 2" };
            var historiaClinica = new Historia_Clinica("O+", 45, false, "Medell�n", enfermedadesPreexistentes);
            var paciente = (Ciudadano)pacienteFactory.CrearPerfil(
                "Juan P�rez", "juan@ejemplo.com", "CC", 2000002, 3209876543, "clave123", 
                historiaClinica, 6.2476f, -75.5658f);
            
            var paramedico = paramedicoFactory.CrearPerfil(
                "Carlos M�dico", "carlos@hospital.com", "CC", 3000003, 3507654321, "medic456");
            
            var operador = operadorFactory.CrearPerfil(
                "Laura Operadora", "laura@central.com", "CC", 4000004, 3158765432, "op789");
            
            Console.WriteLine("Perfiles creados usando Factory:");
            Console.WriteLine($" - Admin: {admin.Nombre}, {admin.GetType().Name}, Celular: {admin.Celular}");
            Console.WriteLine($" - Paciente: {paciente.Nombre}, {paciente.GetType().Name}, Celular: {paciente.Celular}");
            Console.WriteLine($" - Param�dico: {paramedico.Nombre}, {paramedico.GetType().Name}, Celular: {paramedico.Celular}");
            Console.WriteLine($" - Operador: {operador.Nombre}, {operador.GetType().Name}, Celular: {operador.Celular}");
            
            // ======================================================
            // 2. DEMOSTRACI�N DEL PATR�N DECORATOR
            // ======================================================
            Console.WriteLine("\n=== DEMOSTRACI�N DEL PATR�N DECORATOR ===");
            
            // 2.1 Decorator para descargas
            Console.WriteLine("\n* DECORATOR PARA DESCARGAS:");
            // Crear historial m�dico para descargar
            string historialMedico = FormatearHistorialMedico(paciente);
            
            // Uso b�sico
            Console.WriteLine("\n1. Componente concreto b�sico:");
            var descargadorBase = new DescargadorConcreto();
            descargadorBase.Descargar(historialMedico);
            Console.WriteLine("  Descarga b�sica completada.");
            
            // Decorador PDF
            Console.WriteLine("\n2. Con decorador PDF:");
            var pdfDecorator = new PDFDecorador(descargadorBase, directorioPDF);
            pdfDecorator.Descargar(historialMedico);
            Console.WriteLine($"  Documento PDF generado: {pdfDecorator.RutaCompleta}");
            
            // Decorador Excel
            Console.WriteLine("\n3. Con decorador Excel:");
            var excelDecorator = new ExcelDecorador(descargadorBase, directorioExcel);
            excelDecorator.Descargar(historialMedico);
            Console.WriteLine($"  Documento Excel generado: {excelDecorator.RutaCompleta}");
            
            // Decoradores apilados
            Console.WriteLine("\n4. Con decoradores apilados:");
            var decoradorCompleto = new ExcelDecorador(
                new PDFDecorador(descargadorBase, directorioPDF), 
                directorioExcel);
            decoradorCompleto.Descargar(historialMedico);
            Console.WriteLine("  Descarga con m�ltiples formatos completada.");
            
            // 2.2 Decorator para comunicaciones
            Console.WriteLine("\n* DECORATOR PARA COMUNICACIONES:");
            
            // Componente base
            var comunicacionSimple = new ComunicacionSimple();
            Console.WriteLine("\n1. Comunicaci�n base:");
            Console.WriteLine($"  {comunicacionSimple.Comunicacion(paciente)}");
            
            // Decorar con SMS
            var smsDecorator = new SMSDecorator(comunicacionSimple);
            Console.WriteLine("\n2. Con decorador SMS:");
            Console.WriteLine($"  {smsDecorator.Comunicacion(paciente)}");
            
            // Decorar con Email
            var emailDecorator = new EmailDecorator(comunicacionSimple);
            Console.WriteLine("\n3. Con decorador Email:");
            Console.WriteLine($"  {emailDecorator.Comunicacion(admin)}");
            
            // Decoradores apilados
            var comunicacionCompleta = new NotificacionPushDecorator(
                                        new SMSDecorator(
                                          new EmailDecorator(comunicacionSimple)));
            Console.WriteLine("\n4. Con m�ltiples decoradores:");
            Console.WriteLine($"  {comunicacionCompleta.Comunicacion(paramedico)}");
            
            // 2.3 Decorator para mostrar
            Console.WriteLine("\n* DECORATOR PARA MOSTRAR:");
            
            // Crear una alerta para mostrar
            var alerta = new Alerta(paciente, DateTime.Now, "Emergencia m�dica");
            alerta.Nivel_triaje = 2; // Nivel cr�tico
            alerta.Estado = true;
            alerta.Equipo_asignado = paramedico;
            
            // Componente base
            IMostrar<Alerta> mostrarAlertaBase = new MostrarAlertaService();
            Console.WriteLine("\n1. Mostrar alerta b�sica:");
            Console.WriteLine($"  {mostrarAlertaBase.Mostrar(alerta)}");
            
            // Decorar con formato de emergencia
            var emergenciaDecorator = new EmergenciaDecorator<Alerta>(mostrarAlertaBase);
            Console.WriteLine("\n2. Con decorador de emergencia:");
            Console.WriteLine($"  {emergenciaDecorator.Mostrar(alerta)}");
            
            // Decorar con formato JSON
            var jsonDecorator = new FormatoJsonDecorator<Alerta>(mostrarAlertaBase);
            Console.WriteLine("\n3. Con decorador JSON:");
            Console.WriteLine($"  {jsonDecorator.Mostrar(alerta)}");
            
            // ======================================================
            // 3. DEMOSTRACI�N DEL PATR�N OBSERVER
            // ======================================================
            Console.WriteLine("\n=== DEMOSTRACI�N DEL PATR�N OBSERVER ===");
            
            // Demostraci�n simple del patr�n Observer
            Console.WriteLine("\nSimulaci�n del patr�n Observer:");
            Console.WriteLine("1. En una implementaci�n real, Observer permite notificar a m�ltiples objetos sobre cambios.");
            Console.WriteLine("2. Por ejemplo, cuando una alerta se actualiza, se notifica a pacientes, param�dicos y operadores.");
            Console.WriteLine("3. Cada observer implementa su propia l�gica de procesamiento:");
            
            // Simular manualmente las respuestas de los observers
            Console.WriteLine("\nSimulaci�n de respuestas de observers:");
            var pacienteObs = new PacienteObserver();
            var mensajePaciente = pacienteObs.Update(alerta);
            Console.WriteLine($"  Observer Paciente: {mensajePaciente}");
            
            // Simular con un mensaje de texto para ParamedicoObserver ya que usa string en vez de Alerta
            var paramedicoObs = new ParamedicoObserver();
            var mensajeParamedico = paramedicoObs.Update("Emergencia m�dica detectada - nivel 2");
            Console.WriteLine($"  Observer Param�dico: {mensajeParamedico}");
            
            // Simular con un mensaje de texto para OperadorObserver
            var operadorObs = new OperadorObserver();
            var mensajeOperador = operadorObs.Update("Alerta: nueva emergencia asignada para gesti�n");
            Console.WriteLine($"  Observer Operador: {mensajeOperador}");
            
            // ======================================================
            // 4. DEMOSTRACI�N DE INYECCI�N DE DEPENDENCIAS
            // ======================================================
            Console.WriteLine("\n=== DEMOSTRACI�N DE INYECCI�N DE DEPENDENCIAS ===");
            
            // Inyecci�n de dependencias para descargas
            Console.WriteLine("\n* Inyecci�n en HistorialAdmin:");
            var historialAdmin = new Funcionalidad.Actualizar.Inyecciones.HistorialAdmin(excelDecorator);
            Console.WriteLine("  Se inyect� el decorador Excel en HistorialAdmin");
            Console.WriteLine("  Ejecutando a trav�s de la propiedad Descargas:");
            historialAdmin.Descargas.Descargar(historialMedico);
            
            // Inyecci�n de dependencias para mostrar
            Console.WriteLine("\n* Inyecci�n en MostradorOperador:");
            var mostradorOperador = new Funcionalidad.Mostrar.Inyecciones.MostradorOperador(emergenciaDecorator);
            string resultadoMostrar = mostradorOperador.MostrarAlerta.Mostrar(alerta);
            Console.WriteLine("  Se inyect� el decorador EmergenciaDecorator en MostradorOperador");
            Console.WriteLine("  Resultado obtenido a trav�s de la propiedad MostrarAlerta:");
            Console.WriteLine($"  {resultadoMostrar}");
            
            // Inyecci�n de dependencias para comunicaciones
            Console.WriteLine("\n* Inyecci�n en ComunicacionParamedico:");
            var comunicacionParamedico = new Funcionalidad.Comunicacion.Inyecciones.ComunicacionParamedico(comunicacionSimple);
            string resultadoComunicacion = comunicacionParamedico.Comunicacion.Comunicacion(paramedico);
            Console.WriteLine("  Se inyectaron decoradores de comunicaci�n en ComunicacionParamedico");
            Console.WriteLine("  Resultado obtenido a trav�s de la propiedad Comunicacion:");
            Console.WriteLine($"  {resultadoComunicacion}");

            Console.WriteLine("\n=== FIN DE LA DEMOSTRACI�N ===");
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
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