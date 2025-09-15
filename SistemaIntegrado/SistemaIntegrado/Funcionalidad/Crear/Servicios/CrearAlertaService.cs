using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Crear.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Crear.Servicios
{
    public class CrearAlertaService : ICreadorAlerta
    {
        public void Crear(Perfil reportante, DateTime fecha_creacion)
        {
            if (reportante != null && fecha_creacion != default)
            {
                var alerta = new Alerta(reportante, fecha_creacion, "Emergencia");
            }
        }
    }
}