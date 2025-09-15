using System;
using SistemaIntegrado.Clases;
using SistemaIntegradoAlertas.Funcionalidad.Crear.Factory;

namespace SistemaIntegradoAlertas.Funcionalidad.Crear.Inyecciones
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
            perfilFactory = factory ?? throw new ArgumentNullException(nameof(factory));
        }
    }
}