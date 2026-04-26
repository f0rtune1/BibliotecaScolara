using System;

namespace BibliotecaScolara.Models
{
    public class Carte
    {
        public int IDCarte { get; set; }
        public string Titlu { get; set; }
        public int IDAutor { get; set; }
        public int IDEditura { get; set; }
        public int IDCategorie { get; set; }
        public int? AnPublicarii { get; set; }
        public string ISBN { get; set; }
        public int? NrPagini { get; set; }
        public DateTime DataAdaugarii { get; set; }

        // Proprietăți pentru afișare în UI
        public string NumeAutor { get; set; }
        public string NumeEditura { get; set; }
        public string NumeCategorie { get; set; }

        public override string ToString()
        {
            return $"{Titlu} - {NumeAutor}";
        }
    }
}
