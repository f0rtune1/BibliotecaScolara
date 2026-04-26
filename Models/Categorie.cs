namespace BibliotecaScolara.Models
{
    public class Categorie
    {
        public int IDCategorie { get; set; }
        public string NumeCategorie { get; set; }
        public string Descriere { get; set; }

        public override string ToString()
        {
            return NumeCategorie;
        }
    }
}
