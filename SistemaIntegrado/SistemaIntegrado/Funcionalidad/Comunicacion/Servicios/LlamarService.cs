using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Comunicacion.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Comunicacion.Servicios
{
    public class LlamarService : IComunicacion<int>
    {
        public string Comunicacion(int telefono)
        {
            if (telefono > 0)
            {
                return $"Llamada realizada exitosamente al número: {telefono}";
            }
            else
            {
                return "Error: Número de teléfono inválido";
            }
        }
    }
}}