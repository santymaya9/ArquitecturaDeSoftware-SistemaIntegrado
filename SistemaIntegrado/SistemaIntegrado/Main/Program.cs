using System;
using System.Collections.Generic;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Actualizar.Observer;

namespace SistemaIntegrado.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("===== SISTEMA INTEGRADO DE ALERTAS M�DICAS =====");
            Console.WriteLine("Demostraci�n del Patr�n Observer Simplificado\n");
            
            Console.WriteLine("=== DEMOSTRACI�N DEL PATR�N OBSERVER ===\n");

            // 1. Crear el publisher (sujeto) para mensajes de texto
            var mensajePublisher = new Publisher<string>();
            
            Console.WriteLine("Publisher para mensajes creado.\n");

            // 2. Crear observadores
            var paramedicoObserver = new ParamedicoObserver();
            var operadorObserver = new OperadorObserver();
            
            Console.WriteLine("Observadores creados: ParamedicoObserver, OperadorObserver\n");

            // 3. Suscribir los observadores al publisher
            Console.WriteLine("Suscribiendo observadores al publisher...");
            mensajePublisher.Subscribe(paramedicoObserver);
            mensajePublisher.Subscribe(operadorObserver);
            Console.WriteLine("Observadores suscritos con �xito.\n");

            // 4. Enviar una notificaci�n (mensaje normal)
            Console.WriteLine("ENVIANDO MENSAJE NORMAL:");
            string mensajeNormal = "Reuni�n de personal programada para ma�ana a las 9:00 AM";
            List<string> resultados = mensajePublisher.Notify(mensajeNormal);
            
            // Mostrar los resultados de los observadores
            Console.WriteLine("\nRESULTADOS DE LA NOTIFICACI�N:");
            foreach (var resultado in resultados)
            {
                Console.WriteLine($"  - {resultado}");
            }
            
            // 5. Enviar una notificaci�n de emergencia
            Console.WriteLine("\nENVIANDO MENSAJE DE EMERGENCIA:");
            string mensajeUrgente = "URGENTE: Se requiere asistencia inmediata en accidente de tr�fico";
            resultados = mensajePublisher.Notify(mensajeUrgente);
            
            // Mostrar los resultados
            Console.WriteLine("\nRESULTADOS DE LA NOTIFICACI�N DE EMERGENCIA:");
            foreach (var resultado in resultados)
            {
                Console.WriteLine($"  - {resultado}");
            }

            // 6. Demostrar desuscripci�n
            Console.WriteLine("\nDesuscribiendo al param�dico del publisher...");
            mensajePublisher.Unsubscribe(paramedicoObserver);
            Console.WriteLine("Param�dico desuscrito.\n");

            // 7. Enviar otra notificaci�n despu�s de desuscribir
            Console.WriteLine("ENVIANDO MENSAJE DESPU�S DE DESUSCRIBIR:");
            string otroMensaje = "Actualizaci�n de inventario completada";
            resultados = mensajePublisher.Notify(otroMensaje);
            
            // Mostrar resultados (ahora solo del operador)
            Console.WriteLine("\nRESULTADOS DE LA NOTIFICACI�N (SOLO OPERADOR):");
            foreach (var resultado in resultados)
            {
                Console.WriteLine($"  - {resultado}");
            }

            // 8. DEMO CON ALERTA (utilizando PacienteObserver)
            Console.WriteLine("\n=== DEMOSTRACI�N CON ALERTAS M�DICAS ===\n");
            
            // Crear un publisher para alertas
            var alertaPublisher = new Publisher<Alerta>();
            
            // Crear un observador de paciente
            var pacienteObserver = new PacienteObserver();
            
            // Suscribir al observer
            alertaPublisher.Subscribe(pacienteObserver);
            
            // Crear perfiles para la demostraci�n
            var medico = new PerfilDemo("Dr. Garc�a", "garcia@hospital.com", 300123456, "CC", 12345678, "clave123");
            var paciente = new PerfilDemo("Juan P�rez", "juan@email.com", 301234567, "CC", 23456789, "clave456");
            
            // Crear una alerta de ejemplo
            var alerta = new Alerta(
                reportante: paciente,
                fecha_creacion: DateTime.Now,
                tipoAlerta: "Consulta m�dica"
            );
            // Establecer nivel de triaje y equipo asignado
            alerta.Nivel_triaje = 3;
            alerta.Estado = true;
            alerta.Equipo_asignado = medico; // El m�dico como encargado de la alerta
            
            // Notificar la alerta
            Console.WriteLine("ENVIANDO ALERTA M�DICA:");
            resultados = alertaPublisher.Notify(alerta);
            
            // Mostrar resultados
            Console.WriteLine("\nRESULTADOS DE LA NOTIFICACI�N DE ALERTA:");
            foreach (var resultado in resultados)
            {
                Console.WriteLine($"  - {resultado}");
            }

            Console.WriteLine("\n=== FIN DE LA DEMOSTRACI�N ===");
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
    
    // Clase simple de perfil para la demo
    public class PerfilDemo : Perfil
    {
        public PerfilDemo(string nombre, string correo, int celular, string tipoCedula, int cedula, string contrasena) 
            : base(nombre, correo, celular, tipoCedula, cedula, contrasena)
        {
        }
    }
}