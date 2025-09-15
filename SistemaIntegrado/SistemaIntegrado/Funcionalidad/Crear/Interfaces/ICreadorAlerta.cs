using System;
using SistemaIntegrado.Clases;

namespace SistemaIntegrado.Funcionalidad.Crear.Interfaces
{
    public interface ICreadorAlerta
    {
        void Crear(Perfil reportante, DateTime fecha_creacion);
    }
}