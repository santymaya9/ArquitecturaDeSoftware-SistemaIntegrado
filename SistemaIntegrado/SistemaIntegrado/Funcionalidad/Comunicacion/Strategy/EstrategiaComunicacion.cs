using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Strategy
{
    /// <summary>
    /// Clase que implementa el patr�n Strategy simplificado para comunicaciones
    /// </summary>
    public class EstrategiaComunicacion
    {
        private readonly string mensaje;

        public EstrategiaComunicacion(string mensaje)
        {
            this.mensaje = mensaje ?? "Mensaje del sistema";
        }

        /// <summary>
        /// Env�a un mensaje b�sico al destinatario
        /// </summary>
        public string EnviarMensajeBasico(Perfil destinatario)
        {
            if (destinatario == null)
            {
                return "No se puede enviar mensaje: destinatario no v�lido";
            }
            
            return $"Mensaje enviado a {destinatario.Nombre}: {mensaje}";
        }

        /// <summary>
        /// Env�a un mensaje v�a SMS
        /// </summary>
        public string EnviarSMS(Perfil destinatario)
        {
            if (destinatario == null || destinatario.Celular <= 0)
            {
                return $"ERROR SMS: No se puede enviar - n�mero de celular inv�lido";
            }
            
            // Truncar mensaje si es demasiado largo (l�mite SMS: 160 caracteres)
            string mensajeSMS = mensaje.Length > 160 ? mensaje.Substring(0, 157) + "..." : mensaje;
            
            return $"[SMS] Enviado al {destinatario.Celular}: {mensajeSMS}";
        }

        /// <summary>
        /// Env�a un mensaje v�a correo electr�nico
        /// </summary>
        public string EnviarEmail(Perfil destinatario, string asunto = "Notificaci�n del Sistema M�dico")
        {
            if (destinatario == null || string.IsNullOrEmpty(destinatario.Correo))
            {
                return $"ERROR EMAIL: No se puede enviar - direcci�n de correo inv�lida";
            }
            
            return $"[EMAIL] Enviado a {destinatario.Correo}\n" +
                   $"Asunto: {asunto}\n" +
                   $"---------------------------\n" +
                   $"{mensaje}\n" +
                   $"---------------------------\n" +
                   $"Este correo es generado autom�ticamente por el Sistema Integrado de Alertas M�dicas.\n" +
                   $"No responda a este correo. Si necesita asistencia, comun�quese con soporte.";
        }

        /// <summary>
        /// Realiza una llamada telef�nica
        /// </summary>
        public string RealizarLlamada(Perfil destinatario)
        {
            if (destinatario == null || destinatario.Celular <= 0)
            {
                return $"ERROR LLAMADA: No se puede conectar - n�mero de tel�fono inv�lido";
            }
            
            return $"[LLAMADA] Conectando con {destinatario.Nombre} al {destinatario.Celular}\n" +
                   $"Estado: Conectado - Duraci�n: 00:01:32\n" +
                   $"Mensaje transmitido verbalmente: \"{mensaje}\"\n" +
                   $"Llamada finalizada correctamente.";
        }

        /// <summary>
        /// Env�a una notificaci�n push al dispositivo m�vil
        /// </summary>
        public string EnviarNotificacionPush(Perfil destinatario)
        {
            if (destinatario == null)
            {
                return $"ERROR PUSH: No se puede enviar - destinatario inv�lido";
            }
            
            // Acortar el mensaje para notificaci�n push
            var mensajeCorto = mensaje.Length > 100 
                ? mensaje.Substring(0, 97) + "..." 
                : mensaje;
            
            return $"[PUSH] Notificaci�n enviada al dispositivo de {destinatario.Nombre}\n" +
                   $"T�tulo: Alerta M�dica\n" +
                   $"Cuerpo: {mensajeCorto}\n" +
                   $"Acci�n: Abrir app > Detalles de alerta\n" +
                   $"Prioridad: Alta - Sonido: Activado";
        }

        /// <summary>
        /// Transmite un mensaje por radio
        /// </summary>
        public string TransmitirPorRadio(Perfil destinatario, string canalRadio = "CANAL-1")
        {
            if (destinatario == null)
            {
                return $"ERROR RADIO: No se puede transmitir - destinatario inv�lido";
            }
            
            // Formato especial para comunicaciones de radio
            var mensajeRadio = mensaje.Replace("\n", " ");
            
            return $"[RADIO] Transmitiendo por {canalRadio}\n" +
                   $"*BEEP* Atenci�n unidades. Mensaje para {destinatario.Nombre}. *BEEP*\n" +
                   $"Mensaje: {mensajeRadio}\n" +
                   $"Cambio y fuera. *BEEP*";
        }

        /// <summary>
        /// M�todo principal que selecciona la estrategia adecuada seg�n el medio especificado
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
        /// Env�a la comunicaci�n por los medios preferidos seg�n el tipo de destinatario
        /// </summary>
        public string ComunicarInteligente(Perfil destinatario)
        {
            // Determinar autom�ticamente los medios preferidos seg�n el tipo de destinatario
            if (destinatario is Paramedico)
            {
                return $"COMUNICACI�N PARA PARAM�DICO:\n{TransmitirPorRadio(destinatario)}\n\nRespaldo por SMS:\n{EnviarSMS(destinatario)}";
            }
            else if (destinatario is Operador)
            {
                return $"COMUNICACI�N PARA OPERADOR:\n{EnviarEmail(destinatario, "ALERTA OPERATIVA")}\n\nLlamada de confirmaci�n:\n{RealizarLlamada(destinatario)}";
            }
            else if (destinatario is Paciente)
            {
                return $"COMUNICACI�N PARA PACIENTE:\n{EnviarSMS(destinatario)}\n\nNotificaci�n secundaria:\n{EnviarNotificacionPush(destinatario)}";
            }
            
            // Por defecto, enviar SMS
            return EnviarSMS(destinatario);
        }
    }
}