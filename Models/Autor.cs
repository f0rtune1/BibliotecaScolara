using System;

namespace BibliotecaScolara.Models
{
    public class Autor
    {
        public int IDAutor { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public DateTime? DataNasterii { get; set; }
        public string Nationalitate { get; set; }
        public string BiografieBrieșă { get; set; }
        public DateTime DataAdaugarii { get; set; }

        public override string ToString()
        {
            return $"{Nume} {Prenume}";
        }
    }
}
