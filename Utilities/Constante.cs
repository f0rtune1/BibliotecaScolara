using System;

namespace BibliotecaScolara.Utilities
{
    public static class Constante
    {
        // Stări exemplar
        public static class StareExemplar
        {
            public const string BUNA = "Bună";
            public const string DETERIORATA = "Deteriorată";
            public const string PIERDUTA = "Pierdută";
        }

        // Stări împrumut
        public static class StareImprumut
        {
            public const string ACTIV = "Activ";
            public const string INCHEIAT = "Încheiat";
            public const string INTARZIAT = "Întârziat";
        }

        // Stări elev
        public static class StareElev
        {
            public const string ACTIV = "Activ";
            public const string INACTIV = "Inactiv";
            public const string SUSPENDAT = "Suspendat";
        }

        // Configurări
        public const int ZILE_IMPRUMUT_DEFAULT = 30;
        public const int ZILE_AVERTIZARE_INTARZIERE = 3;

        // Mesaje de eroare
        public static class Erori
        {
            public const string AUTOR_ARE_CARTI = "Autorul are cărți asociate și nu poate fi șters!";
            public const string EDITURA_ARE_CARTI = "Editura are cărți asociate și nu poate fi ștearsă!";
            public const string ELEV_ARE_IMPRUMTURI = "Elevul are împrumuturi active și nu poate fi șters!";
            public const string EXEMPLAR_INDISPONIBIL = "Exemplarul nu este disponibil pentru împrumut!";
            public const string ISBN_DUPLICAT = "ISBN-ul există deja în sistem!";
            public const string CAMP_OBLIGATORIU = "Câmp obligatoriu!";
            public const string DATE_INVALIDE = "Date invalide!";
        }

        // Clase disponibile
        public static string[] CLASE = new[]
        {
            "5A", "5B", "5C", "5D",
            "6A", "6B", "6C", "6D",
            "7A", "7B", "7C", "7D",
            "8A", "8B", "8C", "8D",
            "9A", "9B", "9C", "9D",
            "10A", "10B", "10C",
            "11A", "11B", "11C",
            "12A", "12B", "12C"
        };
    }
}