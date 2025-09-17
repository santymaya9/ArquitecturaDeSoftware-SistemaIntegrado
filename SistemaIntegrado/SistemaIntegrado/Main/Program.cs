using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Actualizar.Decorator;
using SistemaIntegradoAlertas.Funcionalidad.Actualizar.Decorator;
using SistemaIntegrado.Funcionalidad.Actualizar.Observer;
using SistemaIntegrado.Funcionalidad.Actualizar.Inyecciones;
using SistemaIntegrado.Funcionalidad.Comunicacion.Decorator;
using SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces;
using SistemaIntegrado.Funcionalidad.Comunicacion.Implementaciones;
using SistemaIntegrado.Funcionalidad.Comunicacion.Inyecciones;
using SistemaIntegrado.Funcionalidad.Crear.Factory;
using SistemaIntegrado.Funcionalidad.Mostrar.Interfaces;
using SistemaIntegrado.Funcionalidad.Mostrar.Servicios;
using SistemaIntegrado.Funcionalidad.Mostrar.Decorator;
using SistemaIntegrado.Funcionalidad.Mostrar.Inyecciones;
using SistemaIntegrado.Funcionalidad.Login.Interfaces;
using SistemaIntegrado.Funcionalidad.Login.Servicios;
using SistemaIntegrado.Funcionalidad.Login.Inyecciones;

namespace SistemaIntegrado.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("======================================================================");
            Console.WriteLine("||             SISTEMA INTEGRADO DE ALERTAS MÉDICAS (SIAM)          ||");
            Console.WriteLine("||                 DEMOSTRACIÓN DE PATRONES DE DISEÑO               ||");
            Console.WriteLine("======================================================================\n");
            
            // Configuración inicial del sistema
            ConfigurarDirectorios();
            
            // Menú principal de la aplicación
            MostrarMenuPrincipal();
        }
        
        private static void MostrarMenuPrincipal()
        {
            bool salir = false;
            string directorioBase = Path.Combine(Path.GetTempPath(), "SistemaIntegrado");
            string directorioPDF = Path.Combine(directorioBase, "PDFs");
            string directorioExcel = Path.Combine(directorioBase, "Excel");
            
            // Crear objetos principales para las demostraciones
            var usuarios = CrearPerfiles();
            
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("======================================================================");
                Console.WriteLine("||                    MENÚ PRINCIPAL DEL SISTEMA                    ||");
                Console.WriteLine("||             SISTEMA INTEGRADO DE ALERTAS MÉDICAS (SIAM)          ||");
                Console.WriteLine("======================================================================\n");
                
                Console.WriteLine("1. Patrón Factory - Creación de perfiles de usuario");
                Console.WriteLine("2. Patrón Chain of Responsibility - Sistema de autenticación");
                Console.WriteLine("3. Patrón Decorator - Sistema de comunicaciones");
                Console.WriteLine("4. Patrón Decorator - Visualización de información");
                Console.WriteLine("5. Patrón Decorator - Exportación de documentos");
                Console.WriteLine("6. Patrón Observer - Notificaciones de alertas");
                Console.WriteLine("7. Simulación completa de emergencia médica");
                Console.WriteLine("8. Salir\n");
                
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();
                
                switch (opcion)
                {
                    case "1":
                        DemostrarPatronFactory(usuarios);
                        break;
                    case "2":
                        DemostrarPatronChainOfResponsibility();
                        break;
                    case "3":
                        DemostrarPatronDecoratorComunicaciones(usuarios);
                        break;
                    case "4":
                        DemostrarPatronDecoratorVisualizacion(usuarios);
                        break;
                    case "5":
                        DemostrarPatronDecoratorExportacion(usuarios, directorioPDF, directorioExcel);
                        break;
                    case "6":
                        DemostrarPatronObserver(usuarios);
                        break;
                    case "7":
                        SimularFlujoCompletoEmergencia(usuarios, directorioPDF, directorioExcel);
                        break;
                    case "8":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
            
            Console.WriteLine("\n======================================================================");
            Console.WriteLine("||                  GRACIAS POR USAR EL SISTEMA SIAM                ||");
            Console.WriteLine("======================================================================");
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
        
        #region Configuración Inicial
        private static void ConfigurarDirectorios()
        {
            try
            {
                string directorioBase = Path.Combine(Path.GetTempPath(), "SistemaIntegrado");
                string directorioPDF = Path.Combine(directorioBase, "PDFs");
                string directorioExcel = Path.Combine(directorioBase, "Excel");
                
                // Crear los directorios si no existen
                Directory.CreateDirectory(directorioBase);
                Directory.CreateDirectory(directorioPDF);
                Directory.CreateDirectory(directorioExcel);
                
                Console.WriteLine("Directorios del sistema inicializados correctamente:");
                Console.WriteLine($" - Base: {directorioBase}");
                Console.WriteLine($" - PDFs: {directorioPDF}");
                Console.WriteLine($" - Excel: {directorioExcel}");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al configurar directorios: {ex.Message}");
            }
        }
        
        private static Dictionary<string, Perfil> CrearPerfiles()
        {
            var perfiles = new Dictionary<string, Perfil>();
            
            try
            {
                // Crear factories para cada tipo de perfil
                var adminFactory = new AdminFactory();
                var pacienteFactory = new PacienteFactory();
                var paramedicoFactory = new ParamedicoFactory();
                var operadorFactory = new OperadorFactory();
                
                // Crear administrador
                var admin = adminFactory.CrearPerfil(
                    "Administrador Sistema", "admin@sistema.com", "CC", 1000001, 3101234567, "admin123");
                perfiles["admin"] = admin;
                
                // Crear paciente/ciudadano con historia clínica
                var enfermedadesPreexistentes = new List<string> { 
                    "Hipertensión arterial", 
                    "Diabetes tipo 2",
                    "Asma" 
                };
                
                var historiaClinica = new Historia_Clinica("O+", 45, false, "Medellín", enfermedadesPreexistentes);
                var paciente = (Ciudadano)pacienteFactory.CrearPerfil(
                    "Juan Pérez", "juan@ejemplo.com", "CC", 2000002, 3209876543, "clave123", 
                    historiaClinica, 6.2476f, -75.5658f);
                perfiles["paciente"] = paciente;
                
                // Crear un segundo paciente para demostrar múltiples alertas
                var historiaClinica2 = new Historia_Clinica("A-", 68, true, "Bogotá", 
                    new List<string> { "Cardiopatía", "Hipertensión", "EPOC" });
                var paciente2 = (Ciudadano)pacienteFactory.CrearPerfil(
                    "María Rodríguez", "maria@ejemplo.com", "CC", 2000003, 3209876544, "clave456", 
                    historiaClinica2, 4.6097f, -74.0817f);
                perfiles["paciente2"] = paciente2;
                
                // Crear paramédico
                var paramedico = paramedicoFactory.CrearPerfil(
                    "Carlos Rodríguez", "carlos@hospital.com", "CC", 3000003, 3507654321, "medic456");
                perfiles["paramedico"] = paramedico;
                
                // Crear operador
                var operador = operadorFactory.CrearPerfil(
                    "Laura Ramírez", "laura@central.com", "CC", 4000004, 3158765432, "op789");
                perfiles["operador"] = operador;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear perfiles: {ex.Message}");
            }
            
            return perfiles;
        }
        #endregion
        
        #region Demostraciones de Patrones
        private static void DemostrarPatronFactory(Dictionary<string, Perfil> usuarios)
        {
            Console.Clear();
            Console.WriteLine("======================================================================");
            Console.WriteLine("||                        PATRÓN FACTORY                            ||");
            Console.WriteLine("||              Creación de diferentes tipos de perfiles            ||");
            Console.WriteLine("======================================================================\n");
            
            Console.WriteLine("El patrón Factory permite crear objetos de diferentes tipos");
            Console.WriteLine("que heredan de una clase base común, sin exponer la lógica");
            Console.WriteLine("de instanciación al cliente.\n");
            
            Console.WriteLine("PERFILES CREADOS EN EL SISTEMA:\n");
            
            // Mostrar información del administrador
            Console.WriteLine($"ADMINISTRADOR:");
            Console.WriteLine($" - Nombre: {usuarios["admin"].Nombre}");
            Console.WriteLine($" - Tipo: {usuarios["admin"].GetType().Name}");
            Console.WriteLine($" - ID: {usuarios["admin"].TipoCedula} {usuarios["admin"].Cedula}");
            Console.WriteLine($" - Contacto: {usuarios["admin"].Celular} / {usuarios["admin"].Correo}\n");
            
            // Mostrar información del ciudadano/paciente
            var paciente = (Ciudadano)usuarios["paciente"];
            Console.WriteLine($"PACIENTE/CIUDADANO:");
            Console.WriteLine($" - Nombre: {paciente.Nombre}");
            Console.WriteLine($" - Tipo: {paciente.GetType().Name}");
            Console.WriteLine($" - ID: {paciente.TipoCedula} {paciente.Cedula}");
            Console.WriteLine($" - Contacto: {paciente.Celular} / {paciente.Correo}");
            Console.WriteLine($" - Tipo de sangre: {paciente.Historia_clinica.TipoSangre}");
            Console.WriteLine($" - Ubicación: [{paciente.Latitud}, {paciente.Longitud}]");
            
            Console.WriteLine($" - Enfermedades preexistentes:");
            foreach (var enfermedad in paciente.Historia_clinica.EnfermedadesPreexistentes)
            {
                Console.WriteLine($"   * {enfermedad}");
            }
            Console.WriteLine();
            
            // Mostrar información del paramédico
            Console.WriteLine($"PARAMÉDICO:");
            Console.WriteLine($" - Nombre: {usuarios["paramedico"].Nombre}");
            Console.WriteLine($" - Tipo: {usuarios["paramedico"].GetType().Name}");
            Console.WriteLine($" - ID: {usuarios["paramedico"].TipoCedula} {usuarios["paramedico"].Cedula}");
            Console.WriteLine($" - Contacto: {usuarios["paramedico"].Celular} / {usuarios["paramedico"].Correo}\n");
            
            // Mostrar información del operador
            Console.WriteLine($"OPERADOR:");
            Console.WriteLine($" - Nombre: {usuarios["operador"].Nombre}");
            Console.WriteLine($" - Tipo: {usuarios["operador"].GetType().Name}");
            Console.WriteLine($" - ID: {usuarios["operador"].TipoCedula} {usuarios["operador"].Cedula}");
            Console.WriteLine($" - Contacto: {usuarios["operador"].Celular} / {usuarios["operador"].Correo}\n");
            
            Console.WriteLine("Beneficios del patrón Factory en el sistema:");
            Console.WriteLine(" - Encapsula la lógica de creación de los diferentes tipos de usuarios");
            Console.WriteLine(" - Permite crear usuarios con configuraciones específicas");
            Console.WriteLine(" - Facilita la extensión del sistema para nuevos tipos de usuarios");
            Console.WriteLine(" - Centraliza la creación de objetos en clases especializadas");
            
            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }
        
        private static void DemostrarPatronChainOfResponsibility()
        {
            Console.Clear();
            Console.WriteLine("======================================================================");
            Console.WriteLine("||             PATRÓN CHAIN OF RESPONSIBILITY                       ||");
            Console.WriteLine("||         Sistema de autenticación y verificación de roles         ||");
            Console.WriteLine("======================================================================\n");
            
            Console.WriteLine("El patrón Chain of Responsibility permite pasar solicitudes a lo largo");
            Console.WriteLine("de una cadena de manejadores, donde cada manejador decide si procesa");
            Console.WriteLine("la solicitud o la pasa al siguiente manejador en la cadena.\n");
            
            // 1. Configurar la cadena de responsabilidad
            Console.WriteLine("1. CONFIGURACIÓN DE LA CADENA DE RESPONSABILIDAD:");
            
            var validationHandler = new ValidationRollHandler();
            var localHandler = new LocalAuthenticationHandler();
            var googleHandler = new GoogleAuthenticationHandler();
            var microsoftHandler = new MicrosoftAuthenticationHandler();
            
            validationHandler.SetNext(localHandler);
            localHandler.SetNext(googleHandler);
            googleHandler.SetNext(microsoftHandler);
            
            Console.WriteLine("  ? Handlers creados: ValidationRoll, Local, Google, Microsoft");
            Console.WriteLine("  ? Cadena configurada: ValidationRoll -> Local -> Google -> Microsoft\n");
            
            // 2. Inyección de la cadena en el servicio Login
            Console.WriteLine("2. INYECCIÓN DE DEPENDENCIA EN EL SERVICIO LOGIN:");
            
            var loginService = new Login(validationHandler);
            
            Console.WriteLine("  ? Servicio Login creado con inyección del primer handler");
            Console.WriteLine("  ? Se establece el punto de entrada a la cadena de autenticación\n");
            
            // 3. Demostrar solicitudes de autenticación
            Console.WriteLine("3. PROCESAMIENTO DE SOLICITUDES:\n");
            
            // 3.1 Autenticación local
            Console.WriteLine("   3.1 AUTENTICACIÓN LOCAL:");
            var solicitudLocal = new Dictionary<string, string>
            {
                { "email", "admin@sistema.com" },
                { "password", "admin123" }
            };
            
            Console.WriteLine($"      Solicitud: email={solicitudLocal["email"]}, password=******");
            string resultadoLocal = loginService.Handler.Handle(solicitudLocal);
            Console.WriteLine($"      Resultado: {resultadoLocal}");
            
            // 3.2 Autenticación con Google
            Console.WriteLine("\n   3.2 AUTENTICACIÓN CON GOOGLE:");
            var solicitudGoogle = new Dictionary<string, string>
            {
                { "tipo", "google" },
                { "email", "usuario@gmail.com" },
                { "token", "google_token_12345678" }
            };
            
            Console.WriteLine($"      Solicitud: tipo=google, email={solicitudGoogle["email"]}, token=******");
            string resultadoGoogle = loginService.Handler.Handle(solicitudGoogle);
            Console.WriteLine($"      Resultado: {resultadoGoogle}");
            
            // 3.3 Autenticación con Microsoft
            Console.WriteLine("\n   3.3 AUTENTICACIÓN CON MICROSOFT:");
            var solicitudMicrosoft = new Dictionary<string, string>
            {
                { "tipo", "microsoft" },
                { "email", "usuario@outlook.com" },
                { "token", "microsoft_token_87654321" }
            };
            
            Console.WriteLine($"      Solicitud: tipo=microsoft, email={solicitudMicrosoft["email"]}, token=******");
            string resultadoMicrosoft = loginService.Handler.Handle(solicitudMicrosoft);
            Console.WriteLine($"      Resultado: {resultadoMicrosoft}");
            
            // 3.4 Validación de roles y permisos
            Console.WriteLine("\n   3.4 VALIDACIÓN DE ROLES Y PERMISOS:");
            var solicitudPermisos = new Dictionary<string, string>
            {
                { "rol", "admin" },
                { "accion", "ver_todas_alertas" }
            };
            
            Console.WriteLine($"      Solicitud: rol={solicitudPermisos["rol"]}, acción={solicitudPermisos["accion"]}");
            string resultadoPermisos = loginService.Handler.Handle(solicitudPermisos);
            Console.WriteLine($"      Resultado: {resultadoPermisos}");
            
            Console.WriteLine("\nBeneficios del patrón Chain of Responsibility en el sistema:");
            Console.WriteLine(" - Desacopla los componentes que envían solicitudes de los que las procesan");
            Console.WriteLine(" - Permite añadir o quitar manejadores sin afectar el resto del sistema");
            Console.WriteLine(" - Facilita la implementación de múltiples métodos de autenticación");
            Console.WriteLine(" - Implementa el principio de responsabilidad única (SRP)");
            
            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }
        
        private static void DemostrarPatronDecoratorComunicaciones(Dictionary<string, Perfil> usuarios)
        {
            Console.Clear();
            Console.WriteLine("======================================================================");
            Console.WriteLine("||             PATRÓN DECORATOR - COMUNICACIONES                     ||");
            Console.WriteLine("||       Sistema de comunicación por múltiples canales               ||");
            Console.WriteLine("======================================================================\n");
            
            Console.WriteLine("El patrón Decorator permite añadir funcionalidades a objetos existentes");
            Console.WriteLine("de forma dinámica sin alterar su estructura, envolviendo los objetos con");
            Console.WriteLine("capas adicionales de comportamiento.\n");
            
            // Obtener los perfiles para la demostración
            var paciente = usuarios["paciente"];
            var paramedico = usuarios["paramedico"];
            var operador = usuarios["operador"];
            
            // 1. Componente base de comunicación
            Console.WriteLine("1. COMPONENTE BASE DE COMUNICACIÓN:");
            var comunicacionBase = new ComunicacionSimple();
            string mensajeBase = comunicacionBase.Comunicacion(paciente);
            Console.WriteLine($"  Mensaje base: {mensajeBase}\n");
            
            // 2. Decorador SMS
            Console.WriteLine("2. COMUNICACIÓN POR SMS (DECORADOR):");
            var smsDecorator = new SMSDecorator(comunicacionBase);
            string mensajeSMS = smsDecorator.Comunicacion(paciente);
            Console.WriteLine($"  {mensajeSMS}\n");
            
            // 3. Decorador Email
            Console.WriteLine("3. COMUNICACIÓN POR EMAIL (DECORADOR):");
            var emailDecorator = new EmailDecorator(comunicacionBase);
            string mensajeEmail = emailDecorator.Comunicacion(operador);
            Console.WriteLine($"  {mensajeEmail}\n");
            
            // 4. Decorador Radio (para paramédicos)
            Console.WriteLine("4. COMUNICACIÓN POR RADIO (DECORADOR):");
            var radioDecorator = new RadioDecorator(comunicacionBase, "EMERGENCIAS-1");
            string mensajeRadio = radioDecorator.Comunicacion(paramedico);
            Console.WriteLine($"  {mensajeRadio}\n");
            
            // 5. Combinación de decoradores (múltiples canales)
            Console.WriteLine("5. COMUNICACIÓN MULTICANAL (COMBINACIÓN DE DECORADORES):");
            
            var comunicacionMulticanal = new NotificacionPushDecorator(
                                          new SMSDecorator(
                                            new EmailDecorator(comunicacionBase)));
            
            string mensajeCombinado = comunicacionMulticanal.Comunicacion(paramedico);
            Console.WriteLine($"  {mensajeCombinado}\n");
            
            // 6. Inyección del componente de comunicación
            Console.WriteLine("6. INYECCIÓN DE DEPENDENCIA DE COMUNICACIÓN:");
            var comunicacionParamedico = new ComunicacionParamedico(radioDecorator);
            string resultadoInyeccion = comunicacionParamedico.Comunicacion.Comunicacion(paramedico);
            Console.WriteLine($"  {resultadoInyeccion}\n");
            
            Console.WriteLine("Beneficios del patrón Decorator en el sistema de comunicaciones:");
            Console.WriteLine(" - Permite añadir funcionalidades dinámicamente a los componentes de comunicación");
            Console.WriteLine(" - Evita subclases innecesarias para cada combinación de funcionalidades");
            Console.WriteLine(" - Posibilita la composición de múltiples canales de comunicación");
            Console.WriteLine(" - Facilita la extensión del sistema con nuevos canales");
            
            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }
        
        private static void DemostrarPatronDecoratorVisualizacion(Dictionary<string, Perfil> usuarios)
        {
            Console.Clear();
            Console.WriteLine("======================================================================");
            Console.WriteLine("||             PATRÓN DECORATOR - VISUALIZACIÓN                     ||");
            Console.WriteLine("||         Presentación flexible de datos en varios formatos        ||");
            Console.WriteLine("======================================================================\n");
            
            // Crear una alerta para demostración
            var paciente = (Ciudadano)usuarios["paciente"];
            var alerta = new Alerta(paciente, DateTime.Now, "Emergencia Cardíaca");
            alerta.Nivel_triaje = 1; // Nivel crítico
            alerta.Estado = true;
            
            // 1. Componente base para mostrar alertas
            Console.WriteLine("1. VISUALIZACIÓN BÁSICA (COMPONENTE BASE):");
            IMostrar<Alerta> mostrarAlertaBase = new MostrarAlertaService();
            string visualizacionBasica = mostrarAlertaBase.Mostrar(alerta);
            Console.WriteLine(visualizacionBasica);
            Console.WriteLine();
            
            // 2. Visualización con formato de emergencia
            Console.WriteLine("2. VISUALIZACIÓN CON FORMATO DE EMERGENCIA (DECORADOR):");
            var emergenciaDecorator = new EmergenciaDecorator<Alerta>(mostrarAlertaBase);
            string visualizacionEmergencia = emergenciaDecorator.Mostrar(alerta);
            Console.WriteLine(visualizacionEmergencia);
            Console.WriteLine();
            
            // 3. Visualización en formato JSON
            Console.WriteLine("3. VISUALIZACIÓN EN FORMATO JSON (DECORADOR):");
            var jsonDecorator = new FormatoJsonDecorator<Alerta>(mostrarAlertaBase);
            string visualizacionJson = jsonDecorator.Mostrar(alerta);
            Console.WriteLine(visualizacionJson);
            Console.WriteLine();
            
            // 4. Visualización en formato XML
            Console.WriteLine("4. VISUALIZACIÓN EN FORMATO XML (DECORADOR):");
            var xmlDecorator = new FormatoXmlDecorator<Alerta>(mostrarAlertaBase);
            string visualizacionXml = xmlDecorator.Mostrar(alerta);
            Console.WriteLine(visualizacionXml);
            Console.WriteLine();
            
            // 5. Visualización combinada (emergencia + JSON)
            Console.WriteLine("5. VISUALIZACIÓN COMBINADA (EMERGENCIA + JSON):");
            var decoradorCombinado = new FormatoJsonDecorator<Alerta>(
                                      new EmergenciaDecorator<Alerta>(mostrarAlertaBase));
            string visualizacionCombinada = decoradorCombinado.Mostrar(alerta);
            Console.WriteLine(visualizacionCombinada);
            Console.WriteLine();
            
            // 6. Inyección de dependencia para visualización
            Console.WriteLine("6. INYECCIÓN DE DEPENDENCIA PARA VISUALIZACIÓN:");
            var mostradorOperador = new MostradorOperador(emergenciaDecorator);
            string resultadoVisualizacion = mostradorOperador.MostrarAlerta.Mostrar(alerta);
            Console.WriteLine("  Resultado de visualización inyectada para Operador:");
            Console.WriteLine(resultadoVisualizacion);
            
            Console.WriteLine("\nBeneficios del patrón Decorator en la visualización:");
            Console.WriteLine(" - Permite formatear datos de múltiples maneras sin modificar el componente base");
            Console.WriteLine(" - Facilita la adición de nuevos formatos de visualización");
            Console.WriteLine(" - Permite combinar formatos para diferentes necesidades");
            Console.WriteLine(" - Respeta el principio Open/Closed: abierto para extensión, cerrado para modificación");
            
            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }
        
        private static void DemostrarPatronDecoratorExportacion(Dictionary<string, Perfil> usuarios, string directorioPDF, string directorioExcel)
        {
            Console.Clear();
            Console.WriteLine("======================================================================");
            Console.WriteLine("||             PATRÓN DECORATOR - EXPORTACIÓN                       ||");
            Console.WriteLine("||       Sistema de exportación de datos en múltiples formatos      ||");
            Console.WriteLine("======================================================================\n");
            
            // Crear datos para exportar
            var paciente = (Ciudadano)usuarios["paciente"];
            string historialMedico = FormatearHistorialMedico(paciente);
            
            // 1. Componente base de descarga
            Console.WriteLine("1. DESCARGA BÁSICA (COMPONENTE BASE):");
            var descargadorBase = new DescargadorConcreto();
            descargadorBase.Descargar(historialMedico);
            Console.WriteLine("  ? Descarga básica realizada (simulación en memoria)\n");
            
            // 2. Exportación a PDF
            Console.WriteLine("2. EXPORTACIÓN A PDF (DECORADOR):");
            var pdfDecorator = new PDFDecorador(descargadorBase, directorioPDF);
            pdfDecorator.Descargar(historialMedico);
            Console.WriteLine($"  ? Archivo PDF generado: {pdfDecorator.RutaCompleta}\n");
            
            // 3. Exportación a Excel
            Console.WriteLine("3. EXPORTACIÓN A EXCEL (DECORADOR):");
            var excelDecorator = new ExcelDecorador(descargadorBase, directorioExcel);
            excelDecorator.Descargar(historialMedico);
            Console.WriteLine($"  ? Archivo Excel generado: {excelDecorator.RutaCompleta}\n");
            
            // 4. Exportación en múltiples formatos (combinación de decoradores)
            Console.WriteLine("4. EXPORTACIÓN EN MÚLTIPLES FORMATOS (DECORADORES ANIDADOS):");
            var multiFormatoDecorator = new ExcelDecorador(
                                          new PDFDecorador(descargadorBase, directorioPDF),
                                          directorioExcel);
            multiFormatoDecorator.Descargar(historialMedico);
            Console.WriteLine("  ? Datos exportados simultáneamente en PDF y Excel\n");
            
            // 5. Inyección de dependencia para descarga
            Console.WriteLine("5. INYECCIÓN DE DEPENDENCIA PARA EXPORTACIÓN:");
            var historialAdmin = new HistorialAdmin(pdfDecorator);
            historialAdmin.Descargas.Descargar(historialMedico);
            Console.WriteLine("  ? Datos exportados a través del servicio inyectado\n");
            
            Console.WriteLine("Beneficios del patrón Decorator en la exportación de documentos:");
            Console.WriteLine(" - Permite añadir funcionalidades de exportación sin modificar el código existente");
            Console.WriteLine(" - Facilita la combinación de diferentes formatos de exportación");
            Console.WriteLine(" - Permite extender el sistema con nuevos formatos de archivo");
            Console.WriteLine(" - Simplifica la configuración de opciones específicas para cada formato");
            
            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }
        
        private static void DemostrarPatronObserver(Dictionary<string, Perfil> usuarios)
        {
            Console.Clear();
            Console.WriteLine("======================================================================");
            Console.WriteLine("||                     PATRÓN OBSERVER                              ||");
            Console.WriteLine("||         Sistema de notificaciones para múltiples actores         ||");
            Console.WriteLine("======================================================================\n");
            
            Console.WriteLine("El patrón Observer permite definir una dependencia de uno a muchos entre");
            Console.WriteLine("objetos, de forma que cuando un objeto cambia su estado, todos sus");
            Console.WriteLine("dependientes son notificados y actualizados automáticamente.\n");
            
            // Crear una alerta para demostración
            var paciente = (Ciudadano)usuarios["paciente"];
            var alerta = new Alerta(paciente, DateTime.Now, "Emergencia Respiratoria");
            alerta.Nivel_triaje = 1; // Nivel crítico
            alerta.Estado = true;
            
            // 1. Crear el publicador y los observadores
            Console.WriteLine("1. CONFIGURACIÓN DEL SISTEMA DE NOTIFICACIONES:");
            var publisher = new Publisher<Alerta>();
            
            var pacienteObserver = new PacienteObserver();
            var paramedicoObserver = new ParamedicoObserver();
            var operadorObserver = new OperadorObserver();
            
            Console.WriteLine("  ? Publicador creado para difundir alertas");
            Console.WriteLine("  ? Observadores registrados: Paciente, Paramédico, Operador\n");
            
            // 2. Suscribir observadores (simulado)
            Console.WriteLine("2. REGISTRO DE OBSERVADORES EN EL SISTEMA:");
            Console.WriteLine("  ? Paciente suscrito para recibir notificaciones");
            Console.WriteLine("  ? Paramédico suscrito para recibir alertas de emergencia");
            Console.WriteLine("  ? Operador suscrito para gestionar asignaciones\n");
            
            // 3. Notificación a los observadores
            Console.WriteLine("3. EMISIÓN DE NOTIFICACIONES A LOS OBSERVADORES:");
            
            // 3.1 Notificación al paciente
            Console.WriteLine("   3.1 NOTIFICACIÓN AL PACIENTE:");
            var mensajePaciente = pacienteObserver.Update(alerta);
            Console.WriteLine($"     {mensajePaciente}\n");
            
            // 3.2 Notificación al paramédico
            Console.WriteLine("   3.2 NOTIFICACIÓN AL PARAMÉDICO:");
            string mensajeAlertaParamedico = $"ALERTA CRÍTICA: {alerta.TipoAlerta}. Triaje nivel {alerta.Nivel_triaje}. Ubicación: [{paciente.Latitud}, {paciente.Longitud}]";
            var mensajeParamedico = paramedicoObserver.Update(mensajeAlertaParamedico);
            Console.WriteLine($"     {mensajeParamedico}\n");
            
            // 3.3 Notificación al operador
            Console.WriteLine("   3.3 NOTIFICACIÓN AL OPERADOR:");
            string mensajeAlertaOperador = $"Nueva alerta ID-{DateTime.Now.Ticks % 10000}: {alerta.TipoAlerta}. Triaje: {alerta.Nivel_triaje}. Requiere asignación inmediata.";
            var mensajeOperador = operadorObserver.Update(mensajeAlertaOperador);
            Console.WriteLine($"     {mensajeOperador}\n");
            
            // 4. Actualización del estado y nueva notificación
            Console.WriteLine("4. ACTUALIZACIÓN DE ESTADO Y NUEVA NOTIFICACIÓN:");
            
            // Asignar un paramédico a la alerta
            alerta.Equipo_asignado = usuarios["paramedico"];
            string notificacionActualizacion = $"ACTUALIZACIÓN: Paramédico {alerta.Equipo_asignado.Nombre} asignado a la alerta {alerta.TipoAlerta}.";
            
            Console.WriteLine($"  Estado actualizado: Paramédico asignado a la alerta");
            Console.WriteLine($"  Nueva notificación emitida:");
            Console.WriteLine($"  {notificacionActualizacion}\n");
            
            Console.WriteLine("Beneficios del patrón Observer en el sistema:");
            Console.WriteLine(" - Permite notificar a múltiples actores sobre cambios en tiempo real");
            Console.WriteLine(" - Desacopla los objetos que generan eventos de los que responden a ellos");
            Console.WriteLine(" - Facilita añadir nuevos tipos de observadores sin modificar el publicador");
            Console.WriteLine(" - Permite implementar un sistema de comunicación asíncrona");
            
            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }
        
        private static void SimularFlujoCompletoEmergencia(Dictionary<string, Perfil> usuarios, string directorioPDF, string directorioExcel)
        {
            Console.Clear();
            Console.WriteLine("======================================================================");
            Console.WriteLine("||             SIMULACIÓN COMPLETA DE EMERGENCIA MÉDICA             ||");
            Console.WriteLine("||           Integración de todos los patrones de diseño            ||");
            Console.WriteLine("======================================================================\n");
            
            var paciente = (Ciudadano)usuarios["paciente"];
            var paramedico = usuarios["paramedico"];
            var operador = usuarios["operador"];
            var admin = usuarios["admin"];
            
            Console.WriteLine("FASE 1: GENERACIÓN DE ALERTA DE EMERGENCIA");
            Console.WriteLine("------------------------------------------\n");
            
            // El ciudadano genera una alerta desde su aplicación
            Console.WriteLine("1.1 Ciudadano activa botón de emergencia en la aplicación móvil");
            var alerta = new Alerta(paciente, DateTime.Now, "Dolor torácico agudo");
            alerta.Nivel_triaje = 1; // Crítico
            alerta.Estado = true;
            Console.WriteLine("  ? Sistema crea instancia de Alerta (Patrón Factory)");
            Console.WriteLine("  ? Sistema registra geolocalización del paciente");
            Console.WriteLine($"  ? Coordenadas: [{paciente.Latitud}, {paciente.Longitud}]\n");
            
            // Notificación mediante Observer
            Console.WriteLine("1.2 Sistema notifica a los actores relevantes (Patrón Observer)");
            Console.WriteLine("  ? Notificación enviada al Centro de Control (Operador)");
            Console.WriteLine("  ? Confirmación enviada al Paciente");
            Console.WriteLine("  ? Alerta lista para asignación\n");
            
            // Usar el Decorator para la comunicación
            Console.WriteLine("1.3 Sistema envía comunicaciones por múltiples canales (Patrón Decorator)");
            var comunicacionMulticanal = new SMSDecorator(new EmailDecorator(new ComunicacionSimple()));
            string mensajeNotificacion = comunicacionMulticanal.Comunicacion(operador);
            Console.WriteLine($"  ? {mensajeNotificacion}\n");
            
            Console.WriteLine("Presione cualquier tecla para continuar con la Fase 2...");
            Console.ReadKey();
            
            Console.Clear();
            Console.WriteLine("FASE 2: GESTIÓN DE LA ALERTA POR EL OPERADOR");
            Console.WriteLine("-------------------------------------------\n");
            
            // El operador visualiza la alerta con el Decorator
            Console.WriteLine("2.1 Operador recibe y visualiza la alerta (Patrón Decorator)");
            var visualizadorAlerta = new EmergenciaDecorator<Alerta>(new MostrarAlertaService());
            string visualizacionAlerta = visualizadorAlerta.Mostrar(alerta);
            Console.WriteLine(visualizacionAlerta);
            Console.WriteLine();
            
            // El operador asigna un paramédico
            Console.WriteLine("2.2 Operador asigna un paramédico a la emergencia");
            alerta.Equipo_asignado = paramedico;
            Console.WriteLine($"  ? Paramédico asignado: {paramedico.Nombre}");
            Console.WriteLine("  ? Estado de alerta actualizado\n");
            
            // Notificación al paramédico
            Console.WriteLine("2.3 Sistema notifica al paramédico asignado");
            var radioDecorator = new RadioDecorator(new ComunicacionSimple(), "EMERGENCIAS-1");
            string mensajeParamedico = radioDecorator.Comunicacion(paramedico);
            Console.WriteLine($"  ? {mensajeParamedico}");
            Console.WriteLine($"  ? Datos de la emergencia enviados al dispositivo del paramédico\n");
            
            // Verificación de permisos con Chain of Responsibility
            Console.WriteLine("2.4 Sistema verifica permisos del operador (Patrón Chain of Responsibility)");
            var validationHandler = new ValidationRollHandler();
            var loginService = new Login(validationHandler);
            
            var solicitudPermiso = new Dictionary<string, string>
            {
                { "rol", "operador" },
                { "accion", "asignar_equipos" }
            };
            
            string resultadoPermiso = loginService.Handler.Handle(solicitudPermiso);
            Console.WriteLine($"  ? {resultadoPermiso}\n");
            
            Console.WriteLine("Presione cualquier tecla para continuar con la Fase 3...");
            Console.ReadKey();
            
            Console.Clear();
            Console.WriteLine("FASE 3: ATENCIÓN DE LA EMERGENCIA POR EL PARAMÉDICO");
            Console.WriteLine("------------------------------------------------\n");
            
            // El paramédico atiende al paciente
            Console.WriteLine("3.1 Paramédico recibe la alerta y llega al lugar");
            Console.WriteLine("  ? Sistema registra llegada del paramédico a las coordenadas");
            Console.WriteLine($"  ? Hora de llegada: {DateTime.Now.AddMinutes(-15):HH:mm:ss}\n");
            
            // Consulta del historial médico
            Console.WriteLine("3.2 Paramédico consulta historial médico del paciente");
            string historialMedico = FormatearHistorialMedico(paciente);
            Console.WriteLine(historialMedico);
            Console.WriteLine();
            
            // Actualización del estado de la alerta
            Console.WriteLine("3.3 Paramédico actualiza el estado de la atención");
            Console.WriteLine("  ? Paciente estabilizado");
            Console.WriteLine("  ? Diagnóstico: Dolor torácico de origen muscular, no cardíaco");
            Console.WriteLine("  ? Tratamiento administrado: Analgésicos");
            
            // Cierre de la alerta
            Console.WriteLine("3.4 Paramédico finaliza la atención");
            alerta.Estado = false;
            alerta.Fecha_finalizacion = DateTime.Now;
            Console.WriteLine($"  ? Hora de finalización: {alerta.Fecha_finalizacion:HH:mm:ss}");
            Console.WriteLine($"  ? Duración total: {(alerta.Fecha_finalizacion - alerta.Fecha_creacion).TotalMinutes:F1} minutos\n");
            
            Console.WriteLine("Presione cualquier tecla para continuar con la Fase 4...");
            Console.ReadKey();
            
            Console.Clear();
            Console.WriteLine("FASE 4: DOCUMENTACIÓN Y CIERRE DE LA EMERGENCIA");
            Console.WriteLine("---------------------------------------------\n");
            
            // Generar reporte de atención
            Console.WriteLine("4.1 Sistema genera reporte de la atención médica");
            string reporteAtencion = GenerarReporteFinal(alerta, paciente, paramedico);
            Console.WriteLine("  ? Reporte generado con todos los detalles de la emergencia\n");
            
            // Exportación del reporte usando Decorator
            Console.WriteLine("4.2 Sistema exporta el reporte en múltiples formatos (Patrón Decorator)");
            var pdfDecorator = new PDFDecorador(new DescargadorConcreto(), directorioPDF);
            pdfDecorator.Descargar(reporteAtencion);
            Console.WriteLine($"  ? Reporte exportado en PDF: {pdfDecorator.RutaCompleta}");
            
            var excelDecorator = new ExcelDecorador(new DescargadorConcreto(), directorioExcel);
            excelDecorator.Descargar(reporteAtencion);
            Console.WriteLine($"  ? Reporte exportado en Excel: {excelDecorator.RutaCompleta}\n");
            
            // Notificación de finalización
            Console.WriteLine("4.3 Sistema notifica la finalización de la emergencia");
            Console.WriteLine("  ? Notificación enviada al paciente sobre cierre de su alerta");
            Console.WriteLine("  ? Notificación enviada al operador para actualizar estado en sistema");
            Console.WriteLine("  ? Notificación enviada al administrador con resumen de la atención\n");
            
            Console.WriteLine("4.4 Sistema actualiza estadísticas e indicadores");
            Console.WriteLine("  ? Tiempo de respuesta registrado: 8 minutos");
            Console.WriteLine("  ? Satisfacción del paciente: Pendiente de evaluación");
            Console.WriteLine("  ? Recurso liberado para nuevas emergencias\n");
            
            Console.WriteLine("RESUMEN DE PATRONES UTILIZADOS EN LA SIMULACIÓN:");
            Console.WriteLine(" - Factory: Creación de perfiles y objetos del sistema");
            Console.WriteLine(" - Chain of Responsibility: Autenticación y verificación de permisos");
            Console.WriteLine(" - Observer: Notificaciones a los diferentes actores del sistema");
            Console.WriteLine(" - Decorator: Comunicaciones, visualización y exportación de datos");
            Console.WriteLine(" - Inyección de Dependencias: Acoplamiento flexible entre componentes\n");
            
            Console.WriteLine("Presione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }
        #endregion
        
        #region Utilidades
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
        
        private static string GenerarReporteFinal(Alerta alerta, Ciudadano paciente, Perfil paramedico)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine($"REPORTE DE ATENCIÓN DE EMERGENCIA");
            sb.AppendLine($"FECHA: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine(new string('=', 60));
            
            sb.AppendLine($"INFORMACIÓN DE LA ALERTA:");
            sb.AppendLine($"Tipo: {alerta.TipoAlerta}");
            sb.AppendLine($"Nivel de triaje: {alerta.Nivel_triaje}");
            sb.AppendLine($"Fecha de creación: {alerta.Fecha_creacion:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"Fecha de cierre: {alerta.Fecha_finalizacion:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"Estado: {(alerta.Estado ? "Activa" : "Cerrada")}");
            sb.AppendLine($"Duración: {(alerta.Fecha_finalizacion - alerta.Fecha_creacion).TotalMinutes:F2} minutos");
            sb.AppendLine(new string('-', 60));
            
            sb.AppendLine($"DATOS DEL PACIENTE:");
            sb.AppendLine($"Nombre: {paciente.Nombre}");
            sb.AppendLine($"Documento: {paciente.TipoCedula} {paciente.Cedula}");
            sb.AppendLine($"Contacto: {paciente.Celular} / {paciente.Correo}");
            sb.AppendLine($"Tipo de sangre: {paciente.Historia_clinica.TipoSangre}");
            sb.AppendLine($"Edad: {paciente.Historia_clinica.Edad} años");
            sb.AppendLine($"Ubicación: [{paciente.Latitud}, {paciente.Longitud}]");
            sb.AppendLine(new string('-', 60));
            
            sb.AppendLine($"PERSONAL QUE ATENDIÓ:");
            sb.AppendLine($"Nombre: {paramedico.Nombre}");
            sb.AppendLine($"Cargo: {paramedico.GetType().Name}");
            sb.AppendLine($"Contacto: {paramedico.Celular} / {paramedico.Correo}");
            sb.AppendLine(new string('-', 60));
            
            sb.AppendLine($"DIAGNÓSTICO Y TRATAMIENTO:");
            sb.AppendLine($"Diagnóstico: Dolor torácico de origen muscular, no cardíaco");
            sb.AppendLine($"Tratamiento administrado: Analgésicos");
            sb.AppendLine($"Recomendaciones: Reposo por 48 horas. Evitar esfuerzos físicos.");
            sb.AppendLine($"Seguimiento: No requerido");
            sb.AppendLine(new string('-', 60));
            
            sb.AppendLine($"OBSERVACIONES:");
            sb.AppendLine($"El paciente presenta mejoría tras la administración de analgésicos.");
            sb.AppendLine($"No se requiere traslado a centro hospitalario.");
            sb.AppendLine(new string('=', 60));
            
            sb.AppendLine($"Este reporte fue generado automáticamente por el Sistema Integrado de Alertas Médicas.");
            sb.AppendLine($"Documento confidencial. Prohibida su reproducción sin autorización.");
            
            return sb.ToString();
        }
        #endregion
    }
}