using System;
using System.Text.RegularExpressions;

namespace BibliotecaScolara.Utilities
{
    public static class Validari
    {
        /// <summary>
        /// Validează dacă un string nu este gol sau null
        /// </summary>
        public static bool ValidareRequired(string valoare)
        {
            return !string.IsNullOrWhiteSpace(valoare);
        }

        /// <summary>
        /// Validează formatul unui email
        /// </summary>
        public static bool ValidareEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return true; // Email opțional

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validează formatul unui telefon (doar cifre, -, +)
        /// </summary>
        public static bool ValidareTelefon(string telefon)
        {
            if (string.IsNullOrWhiteSpace(telefon))
                return true; // Telefon opțional

            return Regex.IsMatch(telefon, @"^[\d\-\+\s()]+$");
        }

        /// <summary>
        /// Validează formatul ISBN (10 sau 13 caractere)
        /// </summary>
        public static bool ValidareISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                return false;

            isbn = isbn.Replace("-", "").Replace(" ", "");
            return (isbn.Length == 10 || isbn.Length == 13) && Regex.IsMatch(isbn, @"^\d+$");
        }

        /// <summary>
        /// Validează o dată (nu poate fi în viitor)
        /// </summary>
        public static bool ValidareData(DateTime? data)
        {
            if (data == null)
                return true; // Data opțională

            return data.Value.Date <= DateTime.Now.Date;
        }

        /// <summary>
        /// Validează un an de publicare
        /// </summary>
        public static bool ValidareAn(int? an)
        {
            if (an == null)
                return true; // An opțional

            return an >= 1000 && an <= DateTime.Now.Year;
        }

        /// <summary>
        /// Validează numărul de pagini
        /// </summary>
        public static bool ValidareNrPagini(int? nrPagini)
        {
            if (nrPagini == null)
                return true; // Opțional

            return nrPagini > 0;
        }

        /// <summary>
        /// Validează un preț
        /// </summary>
        public static bool ValidarePret(decimal? pret)
        {
            if (pret == null)
                return true; // Opțional

            return pret >= 0;
        }

        /// <summary>
        /// Validează lungimea unui text
        /// </summary>
        public static bool ValidareLungime(string text, int maxLungime)
        {
            if (string.IsNullOrWhiteSpace(text))
                return true;

            return text.Length <= maxLungime;
        }

        /// <summary>
        /// Validează o clasă școlară (format: 5A, 9B, 12C etc)
        /// </summary>
        public static bool ValidareClasa(string clasa)
        {
            if (string.IsNullOrWhiteSpace(clasa))
                return false;

            return Regex.IsMatch(clasa.Trim(), @"^[5-9]|1[0-2])\s*[A-Z]?$");
        }
    }
}
