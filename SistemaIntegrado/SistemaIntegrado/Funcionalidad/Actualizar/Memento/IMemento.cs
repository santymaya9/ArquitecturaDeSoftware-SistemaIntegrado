using System;

namespace SistemaIntegrado.Funcionalidad.Actualizar.Memento
{
    public interface IMemento
    {
        string ObtenerDescripcion();
        DateTime FechaCreacion { get; }
        string UsuarioResponsable { get; }
    }
}