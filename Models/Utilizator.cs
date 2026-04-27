using System;

namespace BibliotecaScolara.Models
{
    public class Utilizator
    {
        public int IDUtilizator { get; set; }
        public string NumeUtilizator { get; set; }
        public string ParolaHash { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
        public DateTime DataAdaugarii { get; set; }

        public override string ToString()
        {
            return NumeUtilizator;
        }
    }
}
