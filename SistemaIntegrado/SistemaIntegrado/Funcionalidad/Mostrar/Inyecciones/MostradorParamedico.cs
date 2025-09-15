using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Mostrar.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Mostrar.Inyecciones
{
    public class MostradorParamedico
    {
        // Campo privado para la dependencia inyectada
        private readonly IMostrar<Alerta> mostrarAlerta;

        // Propiedad con accesor lambda
        public IMostrar<Alerta> MostrarAlerta
        {
            get => mostrarAlerta;
        }

        // Constructor para inyecci�n de dependencias
        public MostradorParamedico(IMostrar<Alerta> mostrarAlerta)
        {
            this.mostrarAlerta = mostrarAlerta ?? throw new ArgumentNullException(nameof(mostrarAlerta));
        }
    }
}