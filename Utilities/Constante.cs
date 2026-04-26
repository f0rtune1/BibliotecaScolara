namespace BibliotecaScolara.Utilities
{
    public static class Constante
    {
        // Stări pentru exemplare
        public static class StareExemplar
        {
            public const string BUNA = "Bună";
            public const string DETERIORATA = "Deteriorată";
            public const string PIERDUTA = "Pierdută";

            public static string[] GetAll()
            {
                return new[] { BUNA, DETERIORATA, PIERDUTA };
            }
        }

        // Stări pentru împrumuturi
        public static class StareImprumut
        {
            public const string ACTIV = "Activ";
            public const string INCHEIAT = "Încheiat";
            public const string INTARZIAT = "Întârziat";

            public static string[] GetAll()
            {
                return new[] { ACTIV, INCHEIAT, INTARZIAT };
            }
        }

        // Stări pentru elevi
        public static class StareElev
        {
            public const string ACTIV = "Activ";
            public const string INACTIV = "Inactiv";
            public const string SUSPENDAT = "Suspendat";

            public static string[] GetAll()
            {
                return new[] { ACTIV, INACTIV, SUSPENDAT };
            }
        }

        // Lungimi maxime pentru text
        public static class LungimeMaxima
        {
            public const int NUME = 100;
            public const int PRENUME = 100;
            public const int TITLU_CARTE = 200;
            public const int ISBN = 20;
            public const int COD_INVENTAR = 50;
            public const int EMAIL = 100;
            public const int TELEFON = 20;
            public const int ADRESA = 200;
            public const int DESCRIERE = 500;
            public const int OBSERVATII = 300;
        }

        // Zile pentru împrumut
        public const int ZILE_IMPRUMUT_DEFAULT = 30;

        // Mesaje de eroare
        public static class Erori
        {
            public const string CONEXIUNE_ESUATA = "Conexiunea la baza de date a eșuat!";
            public const string OPERATIE_ESUATA = "Operația nu a putut fi completată!";
            public const string AUTOR_ARE_CARTI = "Autorul are cărți asociate și nu poate fi șters!";
            public const string EDITURA_ARE_CARTI = "Editura are cărți asociate și nu poate fi ștearsă!";
            public const string ELEV_ARE_IMPRUMTURI = "Elevul are împrumuturi active și nu poate fi șters!";
            public const string EXEMPLAR_INDISPONIBIL = "Exemplarul nu este disponibil pentru împrumut!";
        }

        // Titluri de ferestre
        public static class TitluriFormelar
        {
            public const string ADAUGARE_AUTOR = "Adăugare Autor";
            public const string EDITARE_AUTOR = "Editare Autor";
            public const string ADAUGARE_EDITURA = "Adăugare Editura";
            public const string EDITARE_EDITURA = "Editare Editura";
            public const string ADAUGARE_CARTE = "Adăugare Carte";
            public const string EDITARE_CARTE = "Editare Carte";
            public const string ADAUGARE_EXEMPLAR = "Adăugare Exemplar";
            public const string EDITARE_EXEMPLAR = "Editare Exemplar";
            public const string ADAUGARE_ELEV = "Adăugare Elev";
            public const string EDITARE_ELEV = "Editare Elev";
            public const string INREGISTRARE_IMPRUMUT = "Înregistrare Împrumut";
            public const string RESTITUIRE_CARTE = "Restituire Carte";
        }
    }
}
