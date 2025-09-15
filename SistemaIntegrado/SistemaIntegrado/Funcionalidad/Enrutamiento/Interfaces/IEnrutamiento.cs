using System;
using System.Collections.Generic;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Enrutamiento.Interfaces
{
    public interface IEnrutamiento
    {
        string Trazar_ruta(Alerta alerta, List<CentroMedico> centroMedicos);
    }
}