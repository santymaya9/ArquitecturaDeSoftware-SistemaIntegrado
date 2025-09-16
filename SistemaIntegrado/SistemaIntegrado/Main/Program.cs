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
            Console.WriteLine("===== SISTEMA INTEGRADO DE ALERTAS MÉDICAS =====");
            Console.WriteLine("Demostración de patrones de diseño implementados\n");
            
            // Configuración inicial - Crear directorios para descargas
            string directorioBase = Path.Combine(Path.GetTempPath(), "SistemaIntegrado");
            string directorioPDF = Path.Combine(directorioBase, "PDFs");
            string directorioExcel = Path.Combine(directorioBase, "Excel");
            
            Directory.CreateDirectory(directorioBase);
            Directory.CreateDirectory(directorioPDF);
            Directory.CreateDirectory(directorioExcel);
            
            // ======================================================
            // 1. DEMOSTRACIÓN DEL PATRÓN FACTORY
            // ======================================================
            Console.WriteLine("\n=== DEMOSTRACIÓN DEL PATRÓN FACTORY ===");
            
            // Crear perfiles usando fábricas
            var adminFactory = new AdminFactory();
            var pacienteFactory = new PacienteFactory();
            var paramedicoFactory = new ParamedicoFactory();
            var operadorFactory = new OperadorFactory();
            
            // Usando números de celular válidos en Colombia (10 dígitos)
            var admin = adminFactory.CrearPerfil(
                "Admin Sistema", "admin@sistema.com", "CC", 1000001, 3101234567, "admin123");
            
            // Datos extras para crear un paciente (Historia_Clinica)
            var enfermedadesPreexistentes = new List<string> { "Hipertensión", "Diabetes tipo 2" };
            var historiaClinica = new Historia_Clinica("O+", 45, false, "Medellín", enfermedadesPreexistentes);
            var paciente = (Ciudadano)pacienteFactory.CrearPerfil(
                "Juan Pérez", "juan@ejemplo.com", "CC", 2000002, 3209876543, "clave123", 
                historiaClinica, 6.2476f, -75.5658f);
            
            var paramedico = paramedicoFactory.CrearPerfil(
                "Carlos Médico", "carlos@hospital.com", "CC", 3000003, 3507654321, "medic456");
            
            var operador = operadorFactory.CrearPerfil(
                "Laura Operadora", "laura@central.com", "CC", 4000004, 3158765432, "op789");
            
            Console.WriteLine("Perfiles creados usando Factory:");
            Console.WriteLine($" - Admin: {admin.Nombre}, {admin.GetType().Name}, Celular: {admin.Celular}");
            Console.WriteLine($" - Paciente: {paciente.Nombre}, {paciente.GetType().Name}, Celular: {paciente.Celular}");
            Console.WriteLine($" - Paramédico: {paramedico.Nombre}, {paramedico.GetType().Name}, Celular: {paramedico.Celular}");
            Console.WriteLine($" - Operador: {operador.Nombre}, {operador.GetType().Name}, Celular: {operador.Celular}");
            
            // ======================================================
            // 2. DEMOSTRACIÓN DEL PATRÓN DECORATOR
            // ======================================================
            Console.WriteLine("\n=== DEMOSTRACIÓN DEL PATRÓN DECORATOR ===");
            
            // 2.1 Decorator para descargas
            Console.WriteLine("\n* DECORATOR PARA DESCARGAS:");
            // Crear historial médico para descargar
            string historialMedico = FormatearHistorialMedico(paciente);
            
            // Uso básico
            Console.WriteLine("\n1. Componente concreto básico:");
            var descargadorBase = new DescargadorConcreto();
            descargadorBase.Descargar(historialMedico);
            Console.WriteLine("  Descarga básica completada.");
            
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
            Console.WriteLine("  Descarga con múltiples formatos completada.");
            
            // 2.2 Decorator para comunicaciones
            Console.WriteLine("\n* DECORATOR PARA COMUNICACIONES:");
            
            // Componente base
            var comunicacionSimple = new ComunicacionSimple();
            Console.WriteLine("\n1. Comunicación base:");
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
            Console.WriteLine("\n4. Con múltiples decoradores:");
            Console.WriteLine($"  {comunicacionCompleta.Comunicacion(paramedico)}");
            
            // 2.3 Decorator para mostrar
            Console.WriteLine("\n* DECORATOR PARA MOSTRAR:");
            
            // Crear una alerta para mostrar
            var alerta = new Alerta(paciente, DateTime.Now, "Emergencia médica");
            alerta.Nivel_triaje = 2; // Nivel crítico
            alerta.Estado = true;
            alerta.Equipo_asignado = paramedico;
            
            // Componente base
            IMostrar<Alerta> mostrarAlertaBase = new MostrarAlertaService();
            Console.WriteLine("\n1. Mostrar alerta básica:");
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
            // 3. DEMOSTRACIÓN DEL PATRÓN OBSERVER
            // ======================================================
            Console.WriteLine("\n=== DEMOSTRACIÓN DEL PATRÓN OBSERVER ===");
            
            // Demostración simple del patrón Observer
            Console.WriteLine("\nSimulación del patrón Observer:");
            Console.WriteLine("1. En una implementación real, Observer permite notificar a múltiples objetos sobre cambios.");
            Console.WriteLine("2. Por ejemplo, cuando una alerta se actualiza, se notifica a pacientes, paramédicos y operadores.");
            Console.WriteLine("3. Cada observer implementa su propia lógica de procesamiento:");
            
            // Simular manualmente las respuestas de los observers
            Console.WriteLine("\nSimulación de respuestas de observers:");
            var pacienteObs = new PacienteObserver();
            var mensajePaciente = pacienteObs.Update(alerta);
            Console.WriteLine($"  Observer Paciente: {mensajePaciente}");
            
            // Simular con un mensaje de texto para ParamedicoObserver ya que usa string en vez de Alerta
            var paramedicoObs = new ParamedicoObserver();
            var mensajeParamedico = paramedicoObs.Update("Emergencia médica detectada - nivel 2");
            Console.WriteLine($"  Observer Paramédico: {mensajeParamedico}");
            
            // Simular con un mensaje de texto para OperadorObserver
            var operadorObs = new OperadorObserver();
            var mensajeOperador = operadorObs.Update("Alerta: nueva emergencia asignada para gestión");
            Console.WriteLine($"  Observer Operador: {mensajeOperador}");
            
            // ======================================================
            // 4. DEMOSTRACIÓN DE INYECCIÓN DE DEPENDENCIAS
            // ======================================================
            Console.WriteLine("\n=== DEMOSTRACIÓN DE INYECCIÓN DE DEPENDENCIAS ===");
            
            // Inyección de dependencias para descargas
            Console.WriteLine("\n* Inyección en HistorialAdmin:");
            var historialAdmin = new Funcionalidad.Actualizar.Inyecciones.HistorialAdmin(excelDecorator);
            Console.WriteLine("  Se inyectó el decorador Excel en HistorialAdmin");
            Console.WriteLine("  Ejecutando a través de la propiedad Descargas:");
            historialAdmin.Descargas.Descargar(historialMedico);
            
            // Inyección de dependencias para mostrar
            Console.WriteLine("\n* Inyección en MostradorOperador:");
            var mostradorOperador = new Funcionalidad.Mostrar.Inyecciones.MostradorOperador(emergenciaDecorator);
            string resultadoMostrar = mostradorOperador.MostrarAlerta.Mostrar(alerta);
            Console.WriteLine("  Se inyectó el decorador EmergenciaDecorator en MostradorOperador");
            Console.WriteLine("  Resultado obtenido a través de la propiedad MostrarAlerta:");
            Console.WriteLine($"  {resultadoMostrar}");
            
            // Inyección de dependencias para comunicaciones
            Console.WriteLine("\n* Inyección en ComunicacionParamedico:");
            var comunicacionParamedico = new Funcionalidad.Comunicacion.Inyecciones.ComunicacionParamedico(comunicacionSimple);
            string resultadoComunicacion = comunicacionParamedico.Comunicacion.Comunicacion(paramedico);
            Console.WriteLine("  Se inyectaron decoradores de comunicación en ComunicacionParamedico");
            Console.WriteLine("  Resultado obtenido a través de la propiedad Comunicacion:");
            Console.WriteLine($"  {resultadoComunicacion}");

            Console.WriteLine("\n=== FIN DE LA DEMOSTRACIÓN ===");
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
        
        private static string FormatearHistorialMedico(Ciudadano ciudadano)
        {
            var sb = new StringBuilder();
            var historia = ciudadano.Historia_clinica;
            
            sb.AppendLine($"HISTORIAL MÉDICO DE {ciudadano.Nombre.ToUpper()}");
            sb.AppendLine($"Documento: {ciudadano.TipoCedula} {ciudadano.Cedula}");
            sb.AppendLine($"Fecha: {DateTime.Now:yyyy-MM-dd}");
            sb.AppendLine(new string('-', 40));
            
            sb.AppendLine("DATOS PERSONALES:");
            sb.AppendLine($"Edad: {historia.Edad} años");
            sb.AppendLine($"Municipio: {historia.Municipio}");
            sb.AppendLine($"Contacto: {ciudadano.Celular} / {ciudadano.Correo}");
            sb.AppendLine($"Ubicación actual: [{ciudadano.Latitud}, {ciudadano.Longitud}]");
            sb.AppendLine(new string('-', 40));
            
            sb.AppendLine("INFORMACIÓN MÉDICA:");
            sb.AppendLine($"Tipo de sangre: {historia.TipoSangre}");
            sb.AppendLine($"Marcapasos: {(historia.Marcapasos ? "Sí" : "No")}");
            
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
            sb.AppendLine("Este documento es confidencial y está sujeto a normativas de protección de datos de salud.");
            
            return sb.ToString();
        }
    }
}