using System;

namespace SistemaIntegrado.Clases
{
    public class Historia_Clinica
    {
        private string tipo_sangre;
        private int edad;
        private bool marcapasos;
        private string municipio;
        private List<string> l_enfermedades_preexistentes;

        public string TipoSangre
        {
            get => string.IsNullOrWhiteSpace(tipo_sangre) ? "Sin tipo de sangre" : tipo_sangre;
            set => tipo_sangre = value;
        }
        public int Edad
        {
            get => edad == default ? -1 : edad;
            set => edad = value;
        }
        public bool Marcapasos
        {
            get => marcapasos;
            set => marcapasos = value;
        }
        public string Municipio
        {
            get => string.IsNullOrWhiteSpace(municipio) ? "Sin municipio" : municipio;
            set => municipio = value;
        }
     
        public List<string> EnfermedadesPreexistentes
        {
            get => l_enfermedades_preexistentes;
            set => l_enfermedades_preexistentes = value;
        }

        public Historia_Clinica(string tipo_sangre, int edad, bool marcapasos, string municipio, List<string> l_enfermedades_preexistentes = null)
        {
            this.tipo_sangre = tipo_sangre;
            this.edad = edad;
            this.marcapasos = marcapasos;
            this.municipio = municipio;
            this.l_enfermedades_preexistentes = l_enfermedades_preexistentes ?? new List<string>();
        }
    }
}