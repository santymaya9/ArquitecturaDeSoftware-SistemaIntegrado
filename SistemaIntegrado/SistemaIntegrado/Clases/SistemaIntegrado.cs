using System;

namespace SistemaIntegrado.Clases
{
    public class SistemaIntegrado
    {
        // Instancia estática privada (Singleton)
        private static SistemaIntegrado? instancia;

        // Campos privados
        private string nombre;
        private int telefono;
        private List<Cuenta> l_cuentas;
        private List<CentroMedico> l_centroMedico;

        // Propiedades públicas
        public string Nombre
        {
            get => string.IsNullOrWhiteSpace(nombre) ? "Sin nombre" : nombre;
            set => nombre = value;
        }

        public int Telefono
        {
            get => telefono;
            set => telefono = value;
        }

        public List<Cuenta> L_cuentas
        {
            get => l_cuentas;
            set => l_cuentas = value;
        }

        public List<CentroMedico> L_centroMedico
        {
            get => l_centroMedico;
            set => l_centroMedico = value;
        }

        // Constructor privado para implementar Singleton
        private SistemaIntegrado(string nombre, int telefono, List<Cuenta> l_cuentas, List<CentroMedico> l_centroMedico)
        {
            this.nombre = nombre;
            this.telefono = telefono;
            this.l_cuentas = l_cuentas ?? new List<Cuenta>();
            this.l_centroMedico = l_centroMedico ?? new List<CentroMedico>();
        }

        // Método estático para obtener la instancia (GetInstance)
        public static SistemaIntegrado GetInstance()
        {
            if (instancia == null)
            {
                instancia = new SistemaIntegrado(
                    "Sistema Clínica de Emergencias",
                    0,
                    new List<Cuenta>(),
                    new List<CentroMedico>()
                );
            }
            return instancia;
        }
    }
}