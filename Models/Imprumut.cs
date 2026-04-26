using System;

namespace BibliotecaScolara.Models
{
    public class Imprumut
    {
        public int IDImprumut { get; set; }
        public int IDElev { get; set; }
        public int IDExemplar { get; set; }
        public DateTime DataImprumut { get; set; }
        public DateTime DataScadenta { get; set; }
        public DateTime? DataRestituire { get; set; }
        public string Status { get; set; }
        public string Observatii { get; set; }
        public DateTime DataAdaugarii { get; set; }

        // Proprietăți pentru afișare în UI
        public string NumeElev { get; set; }
        public string TitluCarte { get; set; }
        public string CoduInventar { get; set; }
        public int ZileRamase { get; set; }

        public override string ToString()
        {
            return $"{NumeElev} - {TitluCarte}";
        }
    }
}
