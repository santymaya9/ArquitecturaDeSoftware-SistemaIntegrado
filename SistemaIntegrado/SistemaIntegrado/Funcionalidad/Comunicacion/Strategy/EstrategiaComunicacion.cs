using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Strategy
{
    /// <summary>
    /// Clase que implementa el patrón Strategy simplificado para comunicaciones
    /// </summary>
    public class EstrategiaComunicacion
    {
        private readonly string mensaje;

        public EstrategiaComunicacion(string mensaje)
        {
            this.mensaje = mensaje ?? "Mensaje del sistema";
        }

        /// <summary>
        /// Envía un mensaje básico al destinatario
        /// </summary>
        public string EnviarMensajeBasico(Perfil destinatario)
        {
            if (destinatario == null)
            {
                return "No se puede enviar mensaje: destinatario no válido";
            }
            
            return $"Mensaje enviado a {destinatario.Nombre}: {mensaje}";
        }

        /// <summary>
        /// Envía un mensaje vía SMS
        /// </summary>
        public string EnviarSMS(Perfil destinatario)
        {
            if (destinatario == null || destinatario.Celular <= 0)
            {
                return $"ERROR SMS: No se puede enviar - número de celular inválido";
            }
            
            // Truncar mensaje si es demasiado largo (límite SMS: 160 caracteres)
            string mensajeSMS = mensaje.Length > 160 ? mensaje.Substring(0, 157) + "..." : mensaje;
            
            return $"[SMS] Enviado al {destinatario.Celular}: {mensajeSMS}";
        }

        /// <summary>
        /// Envía un mensaje vía correo electrónico
        /// </summary>
        public string EnviarEmail(Perfil destinatario, string asunto = "Notificación del Sistema Médico")
        {
            if (destinatario == null || string.IsNullOrEmpty(destinatario.Correo))
            {
                return $"ERROR EMAIL: No se puede enviar - dirección de correo inválida";
            }
            
            return $"[EMAIL] Enviado a {destinatario.Correo}\n" +
                   $"Asunto: {asunto}\n" +
                   $"---------------------------\n" +
                   $"{mensaje}\n" +
                   $"---------------------------\n" +
                   $"Este correo es generado automáticamente por el Sistema Integrado de Alertas Médicas.\n" +
                   $"No responda a este correo. Si necesita asistencia, comuníquese con soporte.";
        }

        /// <summary>
        /// Realiza una llamada telefónica
        /// </summary>
        public string RealizarLlamada(Perfil destinatario)
        {
            if (destinatario == null || destinatario.Celular <= 0)
            {
                return $"ERROR LLAMADA: No se puede conectar - número de teléfono inválido";
            }
            
            return $"[LLAMADA] Conectando con {destinatario.Nombre} al {destinatario.Celular}\n" +
                   $"Estado: Conectado - Duración: 00:01:32\n" +
                   $"Mensaje transmitido verbalmente: \"{mensaje}\"\n" +
                   $"Llamada finalizada correctamente.";
        }

        /// <summary>
        /// Envía una notificación push al dispositivo móvil
        /// </summary>
        public string EnviarNotificacionPush(Perfil destinatario)
        {
            if (destinatario == null)
            {
                return $"ERROR PUSH: No se puede enviar - destinatario inválido";
            }
            
            // Acortar el mensaje para notificación push
            var mensajeCorto = mensaje.Length > 100 
                ? mensaje.Substring(0, 97) + "..." 
                : mensaje;
            
            return $"[PUSH] Notificación enviada al dispositivo de {destinatario.Nombre}\n" +
                   $"Título: Alerta Médica\n" +
                   $"Cuerpo: {mensajeCorto}\n" +
                   $"Acción: Abrir app > Detalles de alerta\n" +
                   $"Prioridad: Alta - Sonido: Activado";
        }

        /// <summary>
        /// Transmite un mensaje por radio
        /// </summary>
        public string TransmitirPorRadio(Perfil destinatario, string canalRadio = "CANAL-1")
        {
            if (destinatario == null)
            {
                return $"ERROR RADIO: No se puede transmitir - destinatario inválido";
            }
            
            // Formato especial para comunicaciones de radio
            var mensajeRadio = mensaje.Replace("\n", " ");
            
            return $"[RADIO] Transmitiendo por {canalRadio}\n" +
                   $"*BEEP* Atención unidades. Mensaje para {destinatario.Nombre}. *BEEP*\n" +
                   $"Mensaje: {mensajeRadio}\n" +
                   $"Cambio y fuera. *BEEP*";
        }

        /// <summary>
        /// Método principal que selecciona la estrategia adecuada según el medio especificado
        /// </summary>
        public string Comunicar(Perfil destinatario, string medio)
        {
            return medio.ToLower() switch
            {
                "sms" => EnviarSMS(destinatario),
                "email" => EnviarEmail(destinatario),
                "llamada" => RealizarLlamada(destinatario),
                "push" => EnviarNotificacionPush(destinatario),
                "radio" => TransmitirPorRadio(destinatario),
                _ => EnviarMensajeBasico(destinatario)
            };
        }

        /// <summary>
        /// Envía la comunicación por los medios preferidos según el tipo de destinatario
        /// </summary>
        public string ComunicarInteligente(Perfil destinatario)
        {
            // Determinar automáticamente los medios preferidos según el tipo de destinatario
            if (destinatario is Paramedico)
            {
                return $"COMUNICACIÓN PARA PARAMÉDICO:\n{TransmitirPorRadio(destinatario)}\n\nRespaldo por SMS:\n{EnviarSMS(destinatario)}";
            }
            else if (destinatario is Operador)
            {
                return $"COMUNICACIÓN PARA OPERADOR:\n{EnviarEmail(destinatario, "ALERTA OPERATIVA")}\n\nLlamada de confirmación:\n{RealizarLlamada(destinatario)}";
            }
            else if (destinatario is Paciente)
            {
                return $"COMUNICACIÓN PARA PACIENTE:\n{EnviarSMS(destinatario)}\n\nNotificación secundaria:\n{EnviarNotificacionPush(destinatario)}";
            }
            
            // Por defecto, enviar SMS
            return EnviarSMS(destinatario);
        }
    }
}