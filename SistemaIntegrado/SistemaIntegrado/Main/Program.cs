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
            Console.WriteLine("===== SISTEMA INTEGRADO DE ALERTAS MÉDICAS =====");
            Console.WriteLine("Demostración del Patrón Observer Simplificado\n");
            
            Console.WriteLine("=== DEMOSTRACIÓN DEL PATRÓN OBSERVER ===\n");

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
            Console.WriteLine("Observadores suscritos con éxito.\n");

            // 4. Enviar una notificación (mensaje normal)
            Console.WriteLine("ENVIANDO MENSAJE NORMAL:");
            string mensajeNormal = "Reunión de personal programada para mañana a las 9:00 AM";
            List<string> resultados = mensajePublisher.Notify(mensajeNormal);
            
            // Mostrar los resultados de los observadores
            Console.WriteLine("\nRESULTADOS DE LA NOTIFICACIÓN:");
            foreach (var resultado in resultados)
            {
                Console.WriteLine($"  - {resultado}");
            }
            
            // 5. Enviar una notificación de emergencia
            Console.WriteLine("\nENVIANDO MENSAJE DE EMERGENCIA:");
            string mensajeUrgente = "URGENTE: Se requiere asistencia inmediata en accidente de tráfico";
            resultados = mensajePublisher.Notify(mensajeUrgente);
            
            // Mostrar los resultados
            Console.WriteLine("\nRESULTADOS DE LA NOTIFICACIÓN DE EMERGENCIA:");
            foreach (var resultado in resultados)
            {
                Console.WriteLine($"  - {resultado}");
            }

            // 6. Demostrar desuscripción
            Console.WriteLine("\nDesuscribiendo al paramédico del publisher...");
            mensajePublisher.Unsubscribe(paramedicoObserver);
            Console.WriteLine("Paramédico desuscrito.\n");

            // 7. Enviar otra notificación después de desuscribir
            Console.WriteLine("ENVIANDO MENSAJE DESPUÉS DE DESUSCRIBIR:");
            string otroMensaje = "Actualización de inventario completada";
            resultados = mensajePublisher.Notify(otroMensaje);
            
            // Mostrar resultados (ahora solo del operador)
            Console.WriteLine("\nRESULTADOS DE LA NOTIFICACIÓN (SOLO OPERADOR):");
            foreach (var resultado in resultados)
            {
                Console.WriteLine($"  - {resultado}");
            }

            // 8. DEMO CON ALERTA (utilizando PacienteObserver)
            Console.WriteLine("\n=== DEMOSTRACIÓN CON ALERTAS MÉDICAS ===\n");
            
            // Crear un publisher para alertas
            var alertaPublisher = new Publisher<Alerta>();
            
            // Crear un observador de paciente
            var pacienteObserver = new PacienteObserver();
            
            // Suscribir al observer
            alertaPublisher.Subscribe(pacienteObserver);
            
            // Crear perfiles para la demostración
            var medico = new PerfilDemo("Dr. García", "garcia@hospital.com", 300123456, "CC", 12345678, "clave123");
            var paciente = new PerfilDemo("Juan Pérez", "juan@email.com", 301234567, "CC", 23456789, "clave456");
            
            // Crear una alerta de ejemplo
            var alerta = new Alerta(
                reportante: paciente,
                fecha_creacion: DateTime.Now,
                tipoAlerta: "Consulta médica"
            );
            // Establecer nivel de triaje y equipo asignado
            alerta.Nivel_triaje = 3;
            alerta.Estado = true;
            alerta.Equipo_asignado = medico; // El médico como encargado de la alerta
            
            // Notificar la alerta
            Console.WriteLine("ENVIANDO ALERTA MÉDICA:");
            resultados = alertaPublisher.Notify(alerta);
            
            // Mostrar resultados
            Console.WriteLine("\nRESULTADOS DE LA NOTIFICACIÓN DE ALERTA:");
            foreach (var resultado in resultados)
            {
                Console.WriteLine($"  - {resultado}");
            }

            Console.WriteLine("\n=== FIN DE LA DEMOSTRACIÓN ===");
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