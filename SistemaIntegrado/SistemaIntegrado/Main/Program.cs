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
            Console.WriteLine("||             SISTEMA INTEGRADO DE ALERTAS M�DICAS (SIAM)          ||");
            Console.WriteLine("||                 DEMOSTRACI�N DE PATRONES DE DISE�O               ||");
            Console.WriteLine("======================================================================\n");
            
            // Configuraci�n inicial del sistema
            ConfigurarDirectorios();
            
            // Men� principal de la aplicaci�n
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
                Console.WriteLine("||                    MEN� PRINCIPAL DEL SISTEMA                    ||");
                Console.WriteLine("||             SISTEMA INTEGRADO DE ALERTAS M�DICAS (SIAM)          ||");
                Console.WriteLine("======================================================================\n");
                
                Console.WriteLine("1. Patr�n Factory - Creaci�n de perfiles de usuario");
                Console.WriteLine("2. Patr�n Chain of Responsibility - Sistema de autenticaci�n");
                Console.WriteLine("3. Patr�n Decorator - Sistema de comunicaciones");
                Console.WriteLine("4. Patr�n Decorator - Visualizaci�n de informaci�n");
                Console.WriteLine("5. Patr�n Decorator - Exportaci�n de documentos");
                Console.WriteLine("6. Patr�n Observer - Notificaciones de alertas");
                Console.WriteLine("7. Simulaci�n completa de emergencia m�dica");
                Console.WriteLine("8. Salir\n");
                
                Console.Write("Seleccione una opci�n: ");
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
                        Console.WriteLine("\nOpci�n no v�lida. Presione cualquier tecla para continuar...");
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
        
        #region Configuraci�n Inicial
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
                
                // Crear paciente/ciudadano con historia cl�nica
                var enfermedadesPreexistentes = new List<string> { 
                    "Hipertensi�n arterial", 
                    "Diabetes tipo 2",
                    "Asma" 
                };
                
                var historiaClinica = new Historia_Clinica("O+", 45, false, "Medell�n", enfermedadesPreexistentes);
                var paciente = (Ciudadano)pacienteFactory.CrearPerfil(
                    "Juan P�rez", "juan@ejemplo.com", "CC", 2000002, 3209876543, "clave123", 
                    historiaClinica, 6.2476f, -75.5658f);
                perfiles["paciente"] = paciente;
                
                // Crear un segundo paciente para demostrar m�ltiples alertas
                var historiaClinica2 = new Historia_Clinica("A-", 68, true, "Bogot�", 
                    new List<string> { "Cardiopat�a", "Hipertensi�n", "EPOC" });
                var paciente2 = (Ciudadano)pacienteFactory.CrearPerfil(
                    "Mar�a Rodr�guez", "maria@ejemplo.com", "CC", 2000003, 3209876544, "clave456", 
                    historiaClinica2, 4.6097f, -74.0817f);
                perfiles["paciente2"] = paciente2;
                
                // Crear param�dico
                var paramedico = paramedicoFactory.CrearPerfil(
                    "Carlos Rodr�guez", "carlos@hospital.com", "CC", 3000003, 3507654321, "medic456");
                perfiles["paramedico"] = paramedico;
                
                // Crear operador
                var operador = operadorFactory.CrearPerfil(
                    "Laura Ram�rez", "laura@central.com", "CC", 4000004, 3158765432, "op789");
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
            Console.WriteLine("||                        PATR�N FACTORY                            ||");
            Console.WriteLine("||              Creaci�n de diferentes tipos de perfiles            ||");
            Console.WriteLine("======================================================================\n");
            
            Console.WriteLine("El patr�n Factory permite crear objetos de diferentes tipos");
            Console.WriteLine("que heredan de una clase base com�n, sin exponer la l�gica");
            Console.WriteLine("de instanciaci�n al cliente.\n");
            
            Console.WriteLine("PERFILES CREADOS EN EL SISTEMA:\n");
            
            // Mostrar informaci�n del administrador
            Console.WriteLine($"ADMINISTRADOR:");
            Console.WriteLine($" - Nombre: {usuarios["admin"].Nombre}");
            Console.WriteLine($" - Tipo: {usuarios["admin"].GetType().Name}");
            Console.WriteLine($" - ID: {usuarios["admin"].TipoCedula} {usuarios["admin"].Cedula}");
            Console.WriteLine($" - Contacto: {usuarios["admin"].Celular} / {usuarios["admin"].Correo}\n");
            
            // Mostrar informaci�n del ciudadano/paciente
            var paciente = (Ciudadano)usuarios["paciente"];
            Console.WriteLine($"PACIENTE/CIUDADANO:");
            Console.WriteLine($" - Nombre: {paciente.Nombre}");
            Console.WriteLine($" - Tipo: {paciente.GetType().Name}");
            Console.WriteLine($" - ID: {paciente.TipoCedula} {paciente.Cedula}");
            Console.WriteLine($" - Contacto: {paciente.Celular} / {paciente.Correo}");
            Console.WriteLine($" - Tipo de sangre: {paciente.Historia_clinica.TipoSangre}");
            Console.WriteLine($" - Ubicaci�n: [{paciente.Latitud}, {paciente.Longitud}]");
            
            Console.WriteLine($" - Enfermedades preexistentes:");
            foreach (var enfermedad in paciente.Historia_clinica.EnfermedadesPreexistentes)
            {
                Console.WriteLine($"   * {enfermedad}");
            }
            Console.WriteLine();
            
            // Mostrar informaci�n del param�dico
            Console.WriteLine($"PARAM�DICO:");
            Console.WriteLine($" - Nombre: {usuarios["paramedico"].Nombre}");
            Console.WriteLine($" - Tipo: {usuarios["paramedico"].GetType().Name}");
            Console.WriteLine($" - ID: {usuarios["paramedico"].TipoCedula} {usuarios["paramedico"].Cedula}");
            Console.WriteLine($" - Contacto: {usuarios["paramedico"].Celular} / {usuarios["paramedico"].Correo}\n");
            
            // Mostrar informaci�n del operador
            Console.WriteLine($"OPERADOR:");
            Console.WriteLine($" - Nombre: {usuarios["operador"].Nombre}");
            Console.WriteLine($" - Tipo: {usuarios["operador"].GetType().Name}");
            Console.WriteLine($" - ID: {usuarios["operador"].TipoCedula} {usuarios["operador"].Cedula}");
            Console.WriteLine($" - Contacto: {usuarios["operador"].Celular} / {usuarios["operador"].Correo}\n");
            
            Console.WriteLine("Beneficios del patr�n Factory en el sistema:");
            Console.WriteLine(" - Encapsula la l�gica de creaci�n de los diferentes tipos de usuarios");
            Console.WriteLine(" - Permite crear usuarios con configuraciones espec�ficas");
            Console.WriteLine(" - Facilita la extensi�n del sistema para nuevos tipos de usuarios");
            Console.WriteLine(" - Centraliza la creaci�n de objetos en clases especializadas");
            
            Console.WriteLine("\nPresione cualquier tecla para volver al men� principal...");
            Console.ReadKey();
        }
        
        private static void DemostrarPatronChainOfResponsibility()
        {
            Console.Clear();
            Console.WriteLine("======================================================================");
            Console.WriteLine("||             PATR�N CHAIN OF RESPONSIBILITY                       ||");
            Console.WriteLine("||         Sistema de autenticaci�n y verificaci�n de roles         ||");
            Console.WriteLine("======================================================================\n");
            
            Console.WriteLine("El patr�n Chain of Responsibility permite pasar solicitudes a lo largo");
            Console.WriteLine("de una cadena de manejadores, donde cada manejador decide si procesa");
            Console.WriteLine("la solicitud o la pasa al siguiente manejador en la cadena.\n");
            
            // 1. Configurar la cadena de responsabilidad
            Console.WriteLine("1. CONFIGURACI�N DE LA CADENA DE RESPONSABILIDAD:");
            
            var validationHandler = new ValidationRollHandler();
            var localHandler = new LocalAuthenticationHandler();
            var googleHandler = new GoogleAuthenticationHandler();
            var microsoftHandler = new MicrosoftAuthenticationHandler();
            
            validationHandler.SetNext(localHandler);
            localHandler.SetNext(googleHandler);
            googleHandler.SetNext(microsoftHandler);
            
            Console.WriteLine("  ? Handlers creados: ValidationRoll, Local, Google, Microsoft");
            Console.WriteLine("  ? Cadena configurada: ValidationRoll -> Local -> Google -> Microsoft\n");
            
            // 2. Inyecci�n de la cadena en el servicio Login
            Console.WriteLine("2. INYECCI�N DE DEPENDENCIA EN EL SERVICIO LOGIN:");
            
            var loginService = new Login(validationHandler);
            
            Console.WriteLine("  ? Servicio Login creado con inyecci�n del primer handler");
            Console.WriteLine("  ? Se establece el punto de entrada a la cadena de autenticaci�n\n");
            
            // 3. Demostrar solicitudes de autenticaci�n
            Console.WriteLine("3. PROCESAMIENTO DE SOLICITUDES:\n");
            
            // 3.1 Autenticaci�n local
            Console.WriteLine("   3.1 AUTENTICACI�N LOCAL:");
            var solicitudLocal = new Dictionary<string, string>
            {
                { "email", "admin@sistema.com" },
                { "password", "admin123" }
            };
            
            Console.WriteLine($"      Solicitud: email={solicitudLocal["email"]}, password=******");
            string resultadoLocal = loginService.Handler.Handle(solicitudLocal);
            Console.WriteLine($"      Resultado: {resultadoLocal}");
            
            // 3.2 Autenticaci�n con Google
            Console.WriteLine("\n   3.2 AUTENTICACI�N CON GOOGLE:");
            var solicitudGoogle = new Dictionary<string, string>
            {
                { "tipo", "google" },
                { "email", "usuario@gmail.com" },
                { "token", "google_token_12345678" }
            };
            
            Console.WriteLine($"      Solicitud: tipo=google, email={solicitudGoogle["email"]}, token=******");
            string resultadoGoogle = loginService.Handler.Handle(solicitudGoogle);
            Console.WriteLine($"      Resultado: {resultadoGoogle}");
            
            // 3.3 Autenticaci�n con Microsoft
            Console.WriteLine("\n   3.3 AUTENTICACI�N CON MICROSOFT:");
            var solicitudMicrosoft = new Dictionary<string, string>
            {
                { "tipo", "microsoft" },
                { "email", "usuario@outlook.com" },
                { "token", "microsoft_token_87654321" }
            };
            
            Console.WriteLine($"      Solicitud: tipo=microsoft, email={solicitudMicrosoft["email"]}, token=******");
            string resultadoMicrosoft = loginService.Handler.Handle(solicitudMicrosoft);
            Console.WriteLine($"      Resultado: {resultadoMicrosoft}");
            
            // 3.4 Validaci�n de roles y permisos
            Console.WriteLine("\n   3.4 VALIDACI�N DE ROLES Y PERMISOS:");
            var solicitudPermisos = new Dictionary<string, string>
            {
                { "rol", "admin" },
                { "accion", "ver_todas_alertas" }
            };
            
            Console.WriteLine($"      Solicitud: rol={solicitudPermisos["rol"]}, acci�n={solicitudPermisos["accion"]}");
            string resultadoPermisos = loginService.Handler.Handle(solicitudPermisos);
            Console.WriteLine($"      Resultado: {resultadoPermisos}");
            
            Console.WriteLine("\nBeneficios del patr�n Chain of Responsibility en el sistema:");
            Console.WriteLine(" - Desacopla los componentes que env�an solicitudes de los que las procesan");
            Console.WriteLine(" - Permite a�adir o quitar manejadores sin afectar el resto del sistema");
            Console.WriteLine(" - Facilita la implementaci�n de m�ltiples m�todos de autenticaci�n");
            Console.WriteLine(" - Implementa el principio de responsabilidad �nica (SRP)");
            
            Console.WriteLine("\nPresione cualquier tecla para volver al men� principal...");
            Console.ReadKey();
        }
        
        private static void DemostrarPatronDecoratorComunicaciones(Dictionary<string, Perfil> usuarios)
        {
            Console.Clear();
            Console.WriteLine("======================================================================");
            Console.WriteLine("||             PATR�N DECORATOR - COMUNICACIONES                     ||");
            Console.WriteLine("||       Sistema de comunicaci�n por m�ltiples canales               ||");
            Console.WriteLine("======================================================================\n");
            
            Console.WriteLine("El patr�n Decorator permite a�adir funcionalidades a objetos existentes");
            Console.WriteLine("de forma din�mica sin alterar su estructura, envolviendo los objetos con");
            Console.WriteLine("capas adicionales de comportamiento.\n");
            
            // Obtener los perfiles para la demostraci�n
            var paciente = usuarios["paciente"];
            var paramedico = usuarios["paramedico"];
            var operador = usuarios["operador"];
            
            // 1. Componente base de comunicaci�n
            Console.WriteLine("1. COMPONENTE BASE DE COMUNICACI�N:");
            var comunicacionBase = new ComunicacionSimple();
            string mensajeBase = comunicacionBase.Comunicacion(paciente);
            Console.WriteLine($"  Mensaje base: {mensajeBase}\n");
            
            // 2. Decorador SMS
            Console.WriteLine("2. COMUNICACI�N POR SMS (DECORADOR):");
            var smsDecorator = new SMSDecorator(comunicacionBase);
            string mensajeSMS = smsDecorator.Comunicacion(paciente);
            Console.WriteLine($"  {mensajeSMS}\n");
            
            // 3. Decorador Email
            Console.WriteLine("3. COMUNICACI�N POR EMAIL (DECORADOR):");
            var emailDecorator = new EmailDecorator(comunicacionBase);
            string mensajeEmail = emailDecorator.Comunicacion(operador);
            Console.WriteLine($"  {mensajeEmail}\n");
            
            // 4. Decorador Radio (para param�dicos)
            Console.WriteLine("4. COMUNICACI�N POR RADIO (DECORADOR):");
            var radioDecorator = new RadioDecorator(comunicacionBase, "EMERGENCIAS-1");
            string mensajeRadio = radioDecorator.Comunicacion(paramedico);
            Console.WriteLine($"  {mensajeRadio}\n");
            
            // 5. Combinaci�n de decoradores (m�ltiples canales)
            Console.WriteLine("5. COMUNICACI�N MULTICANAL (COMBINACI�N DE DECORADORES):");
            
            var comunicacionMulticanal = new NotificacionPushDecorator(
                                          new SMSDecorator(
                                            new EmailDecorator(comunicacionBase)));
            
            string mensajeCombinado = comunicacionMulticanal.Comunicacion(paramedico);
            Console.WriteLine($"  {mensajeCombinado}\n");
            
            // 6. Inyecci�n del componente de comunicaci�n
            Console.WriteLine("6. INYECCI�N DE DEPENDENCIA DE COMUNICACI�N:");
            var comunicacionParamedico = new ComunicacionParamedico(radioDecorator);
            string resultadoInyeccion = comunicacionParamedico.Comunicacion.Comunicacion(paramedico);
            Console.WriteLine($"  {resultadoInyeccion}\n");
            
            Console.WriteLine("Beneficios del patr�n Decorator en el sistema de comunicaciones:");
            Console.WriteLine(" - Permite a�adir funcionalidades din�micamente a los componentes de comunicaci�n");
            Console.WriteLine(" - Evita subclases innecesarias para cada combinaci�n de funcionalidades");
            Console.WriteLine(" - Posibilita la composici�n de m�ltiples canales de comunicaci�n");
            Console.WriteLine(" - Facilita la extensi�n del sistema con nuevos canales");
            
            Console.WriteLine("\nPresione cualquier tecla para volver al men� principal...");
            Console.ReadKey();
        }
        
        private static void DemostrarPatronDecoratorVisualizacion(Dictionary<string, Perfil> usuarios)
        {
            Console.Clear();
            Console.WriteLine("======================================================================");
            Console.WriteLine("||             PATR�N DECORATOR - VISUALIZACI�N                     ||");
            Console.WriteLine("||         Presentaci�n flexible de datos en varios formatos        ||");
            Console.WriteLine("======================================================================\n");
            
            // Crear una alerta para demostraci�n
            var paciente = (Ciudadano)usuarios["paciente"];
            var alerta = new Alerta(paciente, DateTime.Now, "Emergencia Card�aca");
            alerta.Nivel_triaje = 1; // Nivel cr�tico
            alerta.Estado = true;
            
            // 1. Componente base para mostrar alertas
            Console.WriteLine("1. VISUALIZACI�N B�SICA (COMPONENTE BASE):");
            IMostrar<Alerta> mostrarAlertaBase = new MostrarAlertaService();
            string visualizacionBasica = mostrarAlertaBase.Mostrar(alerta);
            Console.WriteLine(visualizacionBasica);
            Console.WriteLine();
            
            // 2. Visualizaci�n con formato de emergencia
            Console.WriteLine("2. VISUALIZACI�N CON FORMATO DE EMERGENCIA (DECORADOR):");
            var emergenciaDecorator = new EmergenciaDecorator<Alerta>(mostrarAlertaBase);
            string visualizacionEmergencia = emergenciaDecorator.Mostrar(alerta);
            Console.WriteLine(visualizacionEmergencia);
            Console.WriteLine();
            
            // 3. Visualizaci�n en formato JSON
            Console.WriteLine("3. VISUALIZACI�N EN FORMATO JSON (DECORADOR):");
            var jsonDecorator = new FormatoJsonDecorator<Alerta>(mostrarAlertaBase);
            string visualizacionJson = jsonDecorator.Mostrar(alerta);
            Console.WriteLine(visualizacionJson);
            Console.WriteLine();
            
            // 4. Visualizaci�n en formato XML
            Console.WriteLine("4. VISUALIZACI�N EN FORMATO XML (DECORADOR):");
            var xmlDecorator = new FormatoXmlDecorator<Alerta>(mostrarAlertaBase);
            string visualizacionXml = xmlDecorator.Mostrar(alerta);
            Console.WriteLine(visualizacionXml);
            Console.WriteLine();
            
            // 5. Visualizaci�n combinada (emergencia + JSON)
            Console.WriteLine("5. VISUALIZACI�N COMBINADA (EMERGENCIA + JSON):");
            var decoradorCombinado = new FormatoJsonDecorator<Alerta>(
                                      new EmergenciaDecorator<Alerta>(mostrarAlertaBase));
            string visualizacionCombinada = decoradorCombinado.Mostrar(alerta);
            Console.WriteLine(visualizacionCombinada);
            Console.WriteLine();
            
            // 6. Inyecci�n de dependencia para visualizaci�n
            Console.WriteLine("6. INYECCI�N DE DEPENDENCIA PARA VISUALIZACI�N:");
            var mostradorOperador = new MostradorOperador(emergenciaDecorator);
            string resultadoVisualizacion = mostradorOperador.MostrarAlerta.Mostrar(alerta);
            Console.WriteLine("  Resultado de visualizaci�n inyectada para Operador:");
            Console.WriteLine(resultadoVisualizacion);
            
            Console.WriteLine("\nBeneficios del patr�n Decorator en la visualizaci�n:");
            Console.WriteLine(" - Permite formatear datos de m�ltiples maneras sin modificar el componente base");
            Console.WriteLine(" - Facilita la adici�n de nuevos formatos de visualizaci�n");
            Console.WriteLine(" - Permite combinar formatos para diferentes necesidades");
            Console.WriteLine(" - Respeta el principio Open/Closed: abierto para extensi�n, cerrado para modificaci�n");
            
            Console.WriteLine("\nPresione cualquier tecla para volver al men� principal...");
            Console.ReadKey();
        }
        
        private static void DemostrarPatronDecoratorExportacion(Dictionary<string, Perfil> usuarios, string directorioPDF, string directorioExcel)
        {
            Console.Clear();
            Console.WriteLine("======================================================================");
            Console.WriteLine("||             PATR�N DECORATOR - EXPORTACI�N                       ||");
            Console.WriteLine("||       Sistema de exportaci�n de datos en m�ltiples formatos      ||");
            Console.WriteLine("======================================================================\n");
            
            // Crear datos para exportar
            var paciente = (Ciudadano)usuarios["paciente"];
            string historialMedico = FormatearHistorialMedico(paciente);
            
            // 1. Componente base de descarga
            Console.WriteLine("1. DESCARGA B�SICA (COMPONENTE BASE):");
            var descargadorBase = new DescargadorConcreto();
            descargadorBase.Descargar(historialMedico);
            Console.WriteLine("  ? Descarga b�sica realizada (simulaci�n en memoria)\n");
            
            // 2. Exportaci�n a PDF
            Console.WriteLine("2. EXPORTACI�N A PDF (DECORADOR):");
            var pdfDecorator = new PDFDecorador(descargadorBase, directorioPDF);
            pdfDecorator.Descargar(historialMedico);
            Console.WriteLine($"  ? Archivo PDF generado: {pdfDecorator.RutaCompleta}\n");
            
            // 3. Exportaci�n a Excel
            Console.WriteLine("3. EXPORTACI�N A EXCEL (DECORADOR):");
            var excelDecorator = new ExcelDecorador(descargadorBase, directorioExcel);
            excelDecorator.Descargar(historialMedico);
            Console.WriteLine($"  ? Archivo Excel generado: {excelDecorator.RutaCompleta}\n");
            
            // 4. Exportaci�n en m�ltiples formatos (combinaci�n de decoradores)
            Console.WriteLine("4. EXPORTACI�N EN M�LTIPLES FORMATOS (DECORADORES ANIDADOS):");
            var multiFormatoDecorator = new ExcelDecorador(
                                          new PDFDecorador(descargadorBase, directorioPDF),
                                          directorioExcel);
            multiFormatoDecorator.Descargar(historialMedico);
            Console.WriteLine("  ? Datos exportados simult�neamente en PDF y Excel\n");
            
            // 5. Inyecci�n de dependencia para descarga
            Console.WriteLine("5. INYECCI�N DE DEPENDENCIA PARA EXPORTACI�N:");
            var historialAdmin = new HistorialAdmin(pdfDecorator);
            historialAdmin.Descargas.Descargar(historialMedico);
            Console.WriteLine("  ? Datos exportados a trav�s del servicio inyectado\n");
            
            Console.WriteLine("Beneficios del patr�n Decorator en la exportaci�n de documentos:");
            Console.WriteLine(" - Permite a�adir funcionalidades de exportaci�n sin modificar el c�digo existente");
            Console.WriteLine(" - Facilita la combinaci�n de diferentes formatos de exportaci�n");
            Console.WriteLine(" - Permite extender el sistema con nuevos formatos de archivo");
            Console.WriteLine(" - Simplifica la configuraci�n de opciones espec�ficas para cada formato");
            
            Console.WriteLine("\nPresione cualquier tecla para volver al men� principal...");
            Console.ReadKey();
        }
        
        private static void DemostrarPatronObserver(Dictionary<string, Perfil> usuarios)
        {
            Console.Clear();
            Console.WriteLine("======================================================================");
            Console.WriteLine("||                     PATR�N OBSERVER                              ||");
            Console.WriteLine("||         Sistema de notificaciones para m�ltiples actores         ||");
            Console.WriteLine("======================================================================\n");
            
            Console.WriteLine("El patr�n Observer permite definir una dependencia de uno a muchos entre");
            Console.WriteLine("objetos, de forma que cuando un objeto cambia su estado, todos sus");
            Console.WriteLine("dependientes son notificados y actualizados autom�ticamente.\n");
            
            // Crear una alerta para demostraci�n
            var paciente = (Ciudadano)usuarios["paciente"];
            var alerta = new Alerta(paciente, DateTime.Now, "Emergencia Respiratoria");
            alerta.Nivel_triaje = 1; // Nivel cr�tico
            alerta.Estado = true;
            
            // 1. Crear el publicador y los observadores
            Console.WriteLine("1. CONFIGURACI�N DEL SISTEMA DE NOTIFICACIONES:");
            var publisher = new Publisher<Alerta>();
            
            var pacienteObserver = new PacienteObserver();
            var paramedicoObserver = new ParamedicoObserver();
            var operadorObserver = new OperadorObserver();
            
            Console.WriteLine("  ? Publicador creado para difundir alertas");
            Console.WriteLine("  ? Observadores registrados: Paciente, Param�dico, Operador\n");
            
            // 2. Suscribir observadores (simulado)
            Console.WriteLine("2. REGISTRO DE OBSERVADORES EN EL SISTEMA:");
            Console.WriteLine("  ? Paciente suscrito para recibir notificaciones");
            Console.WriteLine("  ? Param�dico suscrito para recibir alertas de emergencia");
            Console.WriteLine("  ? Operador suscrito para gestionar asignaciones\n");
            
            // 3. Notificaci�n a los observadores
            Console.WriteLine("3. EMISI�N DE NOTIFICACIONES A LOS OBSERVADORES:");
            
            // 3.1 Notificaci�n al paciente
            Console.WriteLine("   3.1 NOTIFICACI�N AL PACIENTE:");
            var mensajePaciente = pacienteObserver.Update(alerta);
            Console.WriteLine($"     {mensajePaciente}\n");
            
            // 3.2 Notificaci�n al param�dico
            Console.WriteLine("   3.2 NOTIFICACI�N AL PARAM�DICO:");
            string mensajeAlertaParamedico = $"ALERTA CR�TICA: {alerta.TipoAlerta}. Triaje nivel {alerta.Nivel_triaje}. Ubicaci�n: [{paciente.Latitud}, {paciente.Longitud}]";
            var mensajeParamedico = paramedicoObserver.Update(mensajeAlertaParamedico);
            Console.WriteLine($"     {mensajeParamedico}\n");
            
            // 3.3 Notificaci�n al operador
            Console.WriteLine("   3.3 NOTIFICACI�N AL OPERADOR:");
            string mensajeAlertaOperador = $"Nueva alerta ID-{DateTime.Now.Ticks % 10000}: {alerta.TipoAlerta}. Triaje: {alerta.Nivel_triaje}. Requiere asignaci�n inmediata.";
            var mensajeOperador = operadorObserver.Update(mensajeAlertaOperador);
            Console.WriteLine($"     {mensajeOperador}\n");
            
            // 4. Actualizaci�n del estado y nueva notificaci�n
            Console.WriteLine("4. ACTUALIZACI�N DE ESTADO Y NUEVA NOTIFICACI�N:");
            
            // Asignar un param�dico a la alerta
            alerta.Equipo_asignado = usuarios["paramedico"];
            string notificacionActualizacion = $"ACTUALIZACI�N: Param�dico {alerta.Equipo_asignado.Nombre} asignado a la alerta {alerta.TipoAlerta}.";
            
            Console.WriteLine($"  Estado actualizado: Param�dico asignado a la alerta");
            Console.WriteLine($"  Nueva notificaci�n emitida:");
            Console.WriteLine($"  {notificacionActualizacion}\n");
            
            Console.WriteLine("Beneficios del patr�n Observer en el sistema:");
            Console.WriteLine(" - Permite notificar a m�ltiples actores sobre cambios en tiempo real");
            Console.WriteLine(" - Desacopla los objetos que generan eventos de los que responden a ellos");
            Console.WriteLine(" - Facilita a�adir nuevos tipos de observadores sin modificar el publicador");
            Console.WriteLine(" - Permite implementar un sistema de comunicaci�n as�ncrona");
            
            Console.WriteLine("\nPresione cualquier tecla para volver al men� principal...");
            Console.ReadKey();
        }
        
        private static void SimularFlujoCompletoEmergencia(Dictionary<string, Perfil> usuarios, string directorioPDF, string directorioExcel)
        {
            Console.Clear();
            Console.WriteLine("======================================================================");
            Console.WriteLine("||             SIMULACI�N COMPLETA DE EMERGENCIA M�DICA             ||");
            Console.WriteLine("||           Integraci�n de todos los patrones de dise�o            ||");
            Console.WriteLine("======================================================================\n");
            
            var paciente = (Ciudadano)usuarios["paciente"];
            var paramedico = usuarios["paramedico"];
            var operador = usuarios["operador"];
            var admin = usuarios["admin"];
            
            Console.WriteLine("FASE 1: GENERACI�N DE ALERTA DE EMERGENCIA");
            Console.WriteLine("------------------------------------------\n");
            
            // El ciudadano genera una alerta desde su aplicaci�n
            Console.WriteLine("1.1 Ciudadano activa bot�n de emergencia en la aplicaci�n m�vil");
            var alerta = new Alerta(paciente, DateTime.Now, "Dolor tor�cico agudo");
            alerta.Nivel_triaje = 1; // Cr�tico
            alerta.Estado = true;
            Console.WriteLine("  ? Sistema crea instancia de Alerta (Patr�n Factory)");
            Console.WriteLine("  ? Sistema registra geolocalizaci�n del paciente");
            Console.WriteLine($"  ? Coordenadas: [{paciente.Latitud}, {paciente.Longitud}]\n");
            
            // Notificaci�n mediante Observer
            Console.WriteLine("1.2 Sistema notifica a los actores relevantes (Patr�n Observer)");
            Console.WriteLine("  ? Notificaci�n enviada al Centro de Control (Operador)");
            Console.WriteLine("  ? Confirmaci�n enviada al Paciente");
            Console.WriteLine("  ? Alerta lista para asignaci�n\n");
            
            // Usar el Decorator para la comunicaci�n
            Console.WriteLine("1.3 Sistema env�a comunicaciones por m�ltiples canales (Patr�n Decorator)");
            var comunicacionMulticanal = new SMSDecorator(new EmailDecorator(new ComunicacionSimple()));
            string mensajeNotificacion = comunicacionMulticanal.Comunicacion(operador);
            Console.WriteLine($"  ? {mensajeNotificacion}\n");
            
            Console.WriteLine("Presione cualquier tecla para continuar con la Fase 2...");
            Console.ReadKey();
            
            Console.Clear();
            Console.WriteLine("FASE 2: GESTI�N DE LA ALERTA POR EL OPERADOR");
            Console.WriteLine("-------------------------------------------\n");
            
            // El operador visualiza la alerta con el Decorator
            Console.WriteLine("2.1 Operador recibe y visualiza la alerta (Patr�n Decorator)");
            var visualizadorAlerta = new EmergenciaDecorator<Alerta>(new MostrarAlertaService());
            string visualizacionAlerta = visualizadorAlerta.Mostrar(alerta);
            Console.WriteLine(visualizacionAlerta);
            Console.WriteLine();
            
            // El operador asigna un param�dico
            Console.WriteLine("2.2 Operador asigna un param�dico a la emergencia");
            alerta.Equipo_asignado = paramedico;
            Console.WriteLine($"  ? Param�dico asignado: {paramedico.Nombre}");
            Console.WriteLine("  ? Estado de alerta actualizado\n");
            
            // Notificaci�n al param�dico
            Console.WriteLine("2.3 Sistema notifica al param�dico asignado");
            var radioDecorator = new RadioDecorator(new ComunicacionSimple(), "EMERGENCIAS-1");
            string mensajeParamedico = radioDecorator.Comunicacion(paramedico);
            Console.WriteLine($"  ? {mensajeParamedico}");
            Console.WriteLine($"  ? Datos de la emergencia enviados al dispositivo del param�dico\n");
            
            // Verificaci�n de permisos con Chain of Responsibility
            Console.WriteLine("2.4 Sistema verifica permisos del operador (Patr�n Chain of Responsibility)");
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
            Console.WriteLine("FASE 3: ATENCI�N DE LA EMERGENCIA POR EL PARAM�DICO");
            Console.WriteLine("------------------------------------------------\n");
            
            // El param�dico atiende al paciente
            Console.WriteLine("3.1 Param�dico recibe la alerta y llega al lugar");
            Console.WriteLine("  ? Sistema registra llegada del param�dico a las coordenadas");
            Console.WriteLine($"  ? Hora de llegada: {DateTime.Now.AddMinutes(-15):HH:mm:ss}\n");
            
            // Consulta del historial m�dico
            Console.WriteLine("3.2 Param�dico consulta historial m�dico del paciente");
            string historialMedico = FormatearHistorialMedico(paciente);
            Console.WriteLine(historialMedico);
            Console.WriteLine();
            
            // Actualizaci�n del estado de la alerta
            Console.WriteLine("3.3 Param�dico actualiza el estado de la atenci�n");
            Console.WriteLine("  ? Paciente estabilizado");
            Console.WriteLine("  ? Diagn�stico: Dolor tor�cico de origen muscular, no card�aco");
            Console.WriteLine("  ? Tratamiento administrado: Analg�sicos");
            
            // Cierre de la alerta
            Console.WriteLine("3.4 Param�dico finaliza la atenci�n");
            alerta.Estado = false;
            alerta.Fecha_finalizacion = DateTime.Now;
            Console.WriteLine($"  ? Hora de finalizaci�n: {alerta.Fecha_finalizacion:HH:mm:ss}");
            Console.WriteLine($"  ? Duraci�n total: {(alerta.Fecha_finalizacion - alerta.Fecha_creacion).TotalMinutes:F1} minutos\n");
            
            Console.WriteLine("Presione cualquier tecla para continuar con la Fase 4...");
            Console.ReadKey();
            
            Console.Clear();
            Console.WriteLine("FASE 4: DOCUMENTACI�N Y CIERRE DE LA EMERGENCIA");
            Console.WriteLine("---------------------------------------------\n");
            
            // Generar reporte de atenci�n
            Console.WriteLine("4.1 Sistema genera reporte de la atenci�n m�dica");
            string reporteAtencion = GenerarReporteFinal(alerta, paciente, paramedico);
            Console.WriteLine("  ? Reporte generado con todos los detalles de la emergencia\n");
            
            // Exportaci�n del reporte usando Decorator
            Console.WriteLine("4.2 Sistema exporta el reporte en m�ltiples formatos (Patr�n Decorator)");
            var pdfDecorator = new PDFDecorador(new DescargadorConcreto(), directorioPDF);
            pdfDecorator.Descargar(reporteAtencion);
            Console.WriteLine($"  ? Reporte exportado en PDF: {pdfDecorator.RutaCompleta}");
            
            var excelDecorator = new ExcelDecorador(new DescargadorConcreto(), directorioExcel);
            excelDecorator.Descargar(reporteAtencion);
            Console.WriteLine($"  ? Reporte exportado en Excel: {excelDecorator.RutaCompleta}\n");
            
            // Notificaci�n de finalizaci�n
            Console.WriteLine("4.3 Sistema notifica la finalizaci�n de la emergencia");
            Console.WriteLine("  ? Notificaci�n enviada al paciente sobre cierre de su alerta");
            Console.WriteLine("  ? Notificaci�n enviada al operador para actualizar estado en sistema");
            Console.WriteLine("  ? Notificaci�n enviada al administrador con resumen de la atenci�n\n");
            
            Console.WriteLine("4.4 Sistema actualiza estad�sticas e indicadores");
            Console.WriteLine("  ? Tiempo de respuesta registrado: 8 minutos");
            Console.WriteLine("  ? Satisfacci�n del paciente: Pendiente de evaluaci�n");
            Console.WriteLine("  ? Recurso liberado para nuevas emergencias\n");
            
            Console.WriteLine("RESUMEN DE PATRONES UTILIZADOS EN LA SIMULACI�N:");
            Console.WriteLine(" - Factory: Creaci�n de perfiles y objetos del sistema");
            Console.WriteLine(" - Chain of Responsibility: Autenticaci�n y verificaci�n de permisos");
            Console.WriteLine(" - Observer: Notificaciones a los diferentes actores del sistema");
            Console.WriteLine(" - Decorator: Comunicaciones, visualizaci�n y exportaci�n de datos");
            Console.WriteLine(" - Inyecci�n de Dependencias: Acoplamiento flexible entre componentes\n");
            
            Console.WriteLine("Presione cualquier tecla para volver al men� principal...");
            Console.ReadKey();
        }
        #endregion
        
        #region Utilidades
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
        
        private static string GenerarReporteFinal(Alerta alerta, Ciudadano paciente, Perfil paramedico)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine($"REPORTE DE ATENCI�N DE EMERGENCIA");
            sb.AppendLine($"FECHA: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine(new string('=', 60));
            
            sb.AppendLine($"INFORMACI�N DE LA ALERTA:");
            sb.AppendLine($"Tipo: {alerta.TipoAlerta}");
            sb.AppendLine($"Nivel de triaje: {alerta.Nivel_triaje}");
            sb.AppendLine($"Fecha de creaci�n: {alerta.Fecha_creacion:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"Fecha de cierre: {alerta.Fecha_finalizacion:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"Estado: {(alerta.Estado ? "Activa" : "Cerrada")}");
            sb.AppendLine($"Duraci�n: {(alerta.Fecha_finalizacion - alerta.Fecha_creacion).TotalMinutes:F2} minutos");
            sb.AppendLine(new string('-', 60));
            
            sb.AppendLine($"DATOS DEL PACIENTE:");
            sb.AppendLine($"Nombre: {paciente.Nombre}");
            sb.AppendLine($"Documento: {paciente.TipoCedula} {paciente.Cedula}");
            sb.AppendLine($"Contacto: {paciente.Celular} / {paciente.Correo}");
            sb.AppendLine($"Tipo de sangre: {paciente.Historia_clinica.TipoSangre}");
            sb.AppendLine($"Edad: {paciente.Historia_clinica.Edad} a�os");
            sb.AppendLine($"Ubicaci�n: [{paciente.Latitud}, {paciente.Longitud}]");
            sb.AppendLine(new string('-', 60));
            
            sb.AppendLine($"PERSONAL QUE ATENDI�:");
            sb.AppendLine($"Nombre: {paramedico.Nombre}");
            sb.AppendLine($"Cargo: {paramedico.GetType().Name}");
            sb.AppendLine($"Contacto: {paramedico.Celular} / {paramedico.Correo}");
            sb.AppendLine(new string('-', 60));
            
            sb.AppendLine($"DIAGN�STICO Y TRATAMIENTO:");
            sb.AppendLine($"Diagn�stico: Dolor tor�cico de origen muscular, no card�aco");
            sb.AppendLine($"Tratamiento administrado: Analg�sicos");
            sb.AppendLine($"Recomendaciones: Reposo por 48 horas. Evitar esfuerzos f�sicos.");
            sb.AppendLine($"Seguimiento: No requerido");
            sb.AppendLine(new string('-', 60));
            
            sb.AppendLine($"OBSERVACIONES:");
            sb.AppendLine($"El paciente presenta mejor�a tras la administraci�n de analg�sicos.");
            sb.AppendLine($"No se requiere traslado a centro hospitalario.");
            sb.AppendLine(new string('=', 60));
            
            sb.AppendLine($"Este reporte fue generado autom�ticamente por el Sistema Integrado de Alertas M�dicas.");
            sb.AppendLine($"Documento confidencial. Prohibida su reproducci�n sin autorizaci�n.");
            
            return sb.ToString();
        }
        #endregion
    }
}