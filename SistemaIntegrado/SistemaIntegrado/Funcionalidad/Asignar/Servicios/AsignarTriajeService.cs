using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Servicios.Asignar.Interfaces;

namespace SistemaIntegrado.Servicios.Asignar.Servicios
{
    public class AsignarTriajeService : IAsignar<Alerta, uint>
    {
        public void Asignar(Alerta alerta, uint nivel_triaje)
        {
            if (alerta != null)
            {
                alerta.Nivel_triaje = nivel_triaje;
            }
        }
    }
}