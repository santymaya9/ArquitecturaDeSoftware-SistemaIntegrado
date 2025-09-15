using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Servicios.Asignar.Interfaces;

namespace SistemaIntegrado.Servicios.Asignar.Servicios
{
    public class AsignarAlertaService : IAsignar<Alerta, Alerta>
    {
        public void Asignar(Alerta alerta, Alerta alertaDestino)
        {
            if (alerta != null && alertaDestino != null)
            {
                alertaDestino.TipoAlerta = alerta.TipoAlerta;
                alertaDestino.Reportante = alerta.Reportante;
                alertaDestino.Estado = alerta.Estado;
                alertaDestino.Nivel_triaje = alerta.Nivel_triaje;
                alertaDestino.Fecha_creacion = alerta.Fecha_creacion;
                alertaDestino.Fecha_finalizacion = alerta.Fecha_finalizacion;
                alertaDestino.Equipo_asignado = alerta.Equipo_asignado;
                alertaDestino.Rutas = alerta.Rutas;
            }
        }
    }
}