using System;

namespace BibliotecaScolara.Models
{
    public class Editura
    {
        public int IDEditura { get; set; }
        public string NumeEditura { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public DateTime DataAdaugarii { get; set; }

        public override string ToString()
        {
            return NumeEditura;
        }
    }
}
