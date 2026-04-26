using System;

namespace BibliotecaScolara.Models
{
    public class Elev
    {
        public int IDElev { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Clasa { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public DateTime DataInscrierii { get; set; }
        public string Status { get; set; }
        public DateTime DataAdaugarii { get; set; }

        public override string ToString()
        {
            return $"{Nume} {Prenume} ({Clasa})";
        }
    }
}