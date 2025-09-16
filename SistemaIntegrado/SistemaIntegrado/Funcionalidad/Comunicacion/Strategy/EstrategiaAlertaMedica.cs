using System;
using System.Text;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Strategy
{
    /// <summary>
    /// Estrategia específica para comunicaciones de alertas médicas
    /// </summary>
    public class EstrategiaAlertaMedica
    {
        /// <summary>
        /// Formatea una alerta médica como texto plano
        /// </summary>
        public string FormatearAlertaTextoPlano(Alerta alerta)
        {
            if (alerta == null)
            {
                return "Alerta no disponible";
            }
            
            var sb = new StringBuilder();
            sb.AppendLine($"ALERTA MÉDICA: {alerta.TipoAlerta}");
            sb.AppendLine($"Nivel de Triaje: {alerta.Nivel_triaje}");
            sb.AppendLine($"Reportado por: {alerta.Reportante?.Nombre ?? "Desconocido"}");
            sb.AppendLine($"Fecha: {alerta.Fecha_creacion:dd/MM/yyyy HH:mm:ss}");
            
            if (alerta.Equipo_asignado != null)
            {
                sb.AppendLine($"Asignado a: {alerta.Equipo_asignado.Nombre}");
            }
            else
            {
                sb.AppendLine("No asignado a ningún equipo");
            }
            
            sb.AppendLine($"Estado: {(alerta.Estado ? "Activo" : "Inactivo")}");
            
            if (alerta.Rutas != null && alerta.Rutas.Count > 0)
            {
                sb.AppendLine("\nPuntos de Ruta:");
                foreach (var ruta in alerta.Rutas)
                {
                    sb.AppendLine($"  - Nodo {ruta.NumNodo}: ({ruta.Latitud}, {ruta.Longitud})");
                }
            }
            
            return sb.ToString();
        }
        
        /// <summary>
        /// Formatea una alerta médica como SMS (versión resumida)
        /// </summary>
        public string FormatearAlertaSMS(Alerta alerta)
        {
            if (alerta == null)
            {
                return "Alerta no disponible";
            }
            
            // Formato resumido para SMS
            var prioridad = alerta.Nivel_triaje <= 2 ? "CRÍTICA" : "Normal";
            return $"ALERTA {prioridad}: {alerta.TipoAlerta}. " +
                   $"Nivel: {alerta.Nivel_triaje}. " +
                   $"Reportado: {alerta.Fecha_creacion:HH:mm}. " +
                   $"Estado: {(alerta.Estado ? "Activa" : "Inactiva")}";
        }
        
        /// <summary>
        /// Formatea una alerta médica como email
        /// </summary>
        public string FormatearAlertaEmail(Alerta alerta)
        {
            if (alerta == null)
            {
                return "Alerta no disponible";
            }
            
            var textoBase = FormatearAlertaTextoPlano(alerta);
            
            var sb = new StringBuilder();
            sb.AppendLine("SISTEMA INTEGRADO DE ALERTAS MÉDICAS");
            sb.AppendLine("===================================");
            sb.AppendLine();
            
            // Agregar encabezado según nivel de triaje
            if (alerta.Nivel_triaje <= 2)
            {
                sb.AppendLine("*** ALERTA CRÍTICA - ACCIÓN INMEDIATA REQUERIDA ***");
                sb.AppendLine();
            }
            
            sb.AppendLine(textoBase);
            
            sb.AppendLine();
            sb.AppendLine("---");
            sb.AppendLine("Este correo ha sido generado automáticamente.");
            sb.AppendLine("Por favor no responda a este mensaje.");
            
            return sb.ToString();
        }
        
        /// <summary>
        /// Método principal que envía una alerta por el medio especificado
        /// </summary>
        public string EnviarAlerta(Alerta alerta, Perfil destinatario, string medio)
        {
            if (alerta == null)
            {
                return "Error: Alerta no válida";
            }
            
            if (destinatario == null)
            {
                return "Error: Destinatario no válido";
            }
            
            // Formatear según el medio
            string mensaje;
            switch (medio.ToLower())
            {
                case "sms":
                    if (destinatario.Celular <= 0)
                    {
                        return "ERROR: Número de celular inválido";
                    }
                    mensaje = FormatearAlertaSMS(alerta);
                    return $"[SMS] Enviado a {destinatario.Celular}:\n{mensaje}";
                
                case "email":
                    if (string.IsNullOrEmpty(destinatario.Correo))
                    {
                        return "ERROR: Correo electrónico inválido";
                    }
                    mensaje = FormatearAlertaEmail(alerta);
                    return $"[EMAIL] Enviado a {destinatario.Correo}:\n{mensaje}";
                
                case "radio":
                    mensaje = FormatearAlertaSMS(alerta); // Formato compacto para radio
                    return $"[RADIO] Transmitido a unidad de {destinatario.Nombre}:\n*BEEP* {mensaje} *BEEP*";
                
                default:
                    mensaje = FormatearAlertaTextoPlano(alerta);
                    return $"[MENSAJE] Enviado a {destinatario.Nombre}:\n{mensaje}";
            }
        }
        
        /// <summary>
        /// Envía una alerta usando el medio más adecuado según el nivel de triaje y el destinatario
        /// </summary>
        public string EnviarAlertaInteligente(Alerta alerta, Perfil destinatario)
        {
            if (alerta == null || destinatario == null)
            {
                return "Error: Alerta o destinatario no válidos";
            }
            
            // Alerta crítica: usar todos los medios disponibles
            if (alerta.Nivel_triaje <= 2)
            {
                var sb = new StringBuilder();
                sb.AppendLine("=== ALERTA CRÍTICA - NOTIFICACIÓN MULTICANAL ===");
                
                // Determinar canales según tipo de destinatario
                if (destinatario is Paramedico)
                {
                    sb.AppendLine(EnviarAlerta(alerta, destinatario, "radio"));
                    sb.AppendLine();
                    sb.AppendLine(EnviarAlerta(alerta, destinatario, "sms"));
                }
                else if (destinatario is Operador)
                {
                    sb.AppendLine(EnviarAlerta(alerta, destinatario, "email"));
                    sb.AppendLine();
                    sb.AppendLine(EnviarAlerta(alerta, destinatario, "sms"));
                }
                else
                {
                    sb.AppendLine(EnviarAlerta(alerta, destinatario, "sms"));
                }
                
                return sb.ToString();
            }
            
            // Alerta normal: usar canal principal según tipo de destinatario
            if (destinatario is Paramedico)
            {
                return EnviarAlerta(alerta, destinatario, "radio");
            }
            else if (destinatario is Operador)
            {
                return EnviarAlerta(alerta, destinatario, "email");
            }
            
            // Por defecto
            return EnviarAlerta(alerta, destinatario, "sms");
        }
    }
}