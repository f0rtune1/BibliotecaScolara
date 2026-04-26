using System;

namespace BibliotecaScolara.Models
{
    public class Exemplar
    {
        public int IDExemplar { get; set; }
        public int IDCarte { get; set; }
        public string CoduInventar { get; set; }
        public string StareExemplar { get; set; }
        public DateTime DataAchizitiei { get; set; }
        public decimal? Pret { get; set; }
        public DateTime DataAdaugarii { get; set; }

        // Proprietăți pentru afișare în UI
        public string TitluCarte { get; set; }
        public bool Disponibil { get; set; }

        public override string ToString()
        {
            return $"{CoduInventar} - {TitluCarte}";
        }
    }
}
