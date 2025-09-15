using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Crear.Observer
{
    public class AlertaCreacionObserver : ICreacionObserver<Alerta>
    {
        public string OnElementoCreado(Alerta elemento)
        {
            if (elemento != null)
            {
                return $"🚨 NUEVA ALERTA CREADA: {elemento.TipoAlerta} - Reportante: {elemento.Reportante?.Nombre} - Fecha: {elemento.Fecha_creacion:HH:mm:ss}";
            }
            return "Error: No se pudo crear la alerta";
        }
    }
}