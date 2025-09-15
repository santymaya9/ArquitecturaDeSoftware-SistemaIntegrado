using System;
using SistemaIntegrado.Clases;
using SistemaIntegrado.Funcionalidad.Crear.Interfaces;

namespace SistemaIntegrado.Funcionalidad.Crear.Servicios
{
    public class CrearCuentaService
    {
        private readonly IPerfilFactory perfilFactory;

        public IPerfilFactory PerfilFactory
        {
            get => perfilFactory;
        }

        public CrearCuentaService(IPerfilFactory factory)
        {
            this.perfilFactory = factory ?? throw new ArgumentNullException(nameof(factory));
        }
    }
}