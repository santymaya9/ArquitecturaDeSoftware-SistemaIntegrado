using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Servicios.Asignar.Interfaces;

namespace SistemaIntegrado.Servicios.Asignar.Servicios
{
    public class AsignarEstadoCuentaService : IAsignar<Cuenta, bool>
    {
        public void Asignar(Cuenta cuenta, bool estadoCuenta)
        {
            if (cuenta != null)
            {
                cuenta.EstadoActivo = estadoCuenta;
            }
        }
    }
}