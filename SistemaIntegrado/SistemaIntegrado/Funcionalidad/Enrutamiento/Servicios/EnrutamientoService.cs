using System;
using System.Collections.Generic;
using System.Linq;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Enrutamiento.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Enrutamiento.Servicios
{
    public class EnrutamientoService : IEnrutamiento
    {
        public string Trazar_ruta(Alerta alerta, List<CentroMedico> centroMedicos)
        {
            if (alerta == null)
            {
                return "Error: La alerta no puede ser nula";
            }

            if (centroMedicos == null || !centroMedicos.Any())
            {
                return "Error: No hay centros médicos disponibles para trazar la ruta";
            }

            var centroMasCercano = centroMedicos.First();
            
            return $"Ruta trazada exitosamente desde alerta '{alerta.TipoAlerta}' hacia centro médico '{centroMasCercano.Nombre}'";
        }
    }
}