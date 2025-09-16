using System;
using System.Text;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Implementaciones
{
    public class ComunicacionAlertaMedica : IComunicacion<Alerta>
    {
        public string Comunicacion(Alerta alerta)
        {
            if (alerta == null)
            {
                return "Alerta no disponible";
            }
            
            var sb = new StringBuilder();
            sb.AppendLine($"ALERTA MÉDICA: {alerta.TipoAlerta}");
            sb.AppendLine($"Nivel de Triaje: {alerta.Nivel_triaje}");
            sb.AppendLine($"Reportado por: {alerta.Reportante?.Nombre ?? "Desconocido"}");
            sb.AppendLine($"Fecha de Creación: {alerta.Fecha_creacion:dd/MM/yyyy HH:mm:ss}");
            
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
    }
}