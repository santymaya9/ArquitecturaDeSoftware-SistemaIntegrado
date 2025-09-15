using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Mostrar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Mostrar.Servicios
{
    public class MostrarAlertaService : IMostrar<Alerta>
    {
        public string Mostrar(Alerta entidad)
        {
            if (entidad != null)
            {
                string estado = entidad.Estado ? "Activa" : "Inactiva";
                string equipoAsignado = entidad.Equipo_asignado?.Nombre ?? "Sin asignar";
                return $"Alerta: {entidad.TipoAlerta}, Reportante: {entidad.Reportante?.Nombre}, Estado: {estado}, Nivel Triaje: {entidad.Nivel_triaje}, Equipo: {equipoAsignado}, Fecha: {entidad.Fecha_creacion:dd/MM/yyyy HH:mm}";
            }
            return "No hay información de la alerta";
        }
    }
}