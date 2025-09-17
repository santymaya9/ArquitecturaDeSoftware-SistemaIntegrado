namespace SistemaIntegrado.Funcionalidad.Login.Interfaces
{
    public interface IHandler
    {
        void SetNext(IHandler handler);
        string Handle(object request);
    }
}