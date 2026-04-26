using System;
using System.Text.RegularExpressions;

namespace BibliotecaScolara.Utilities
{
    public static class Validari
    {
        /// <summary>
        /// Validează email
        /// </summary>
        public static bool ValidareEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return true; // Email optional

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
        /// Validează telefon (format românesc)
        /// </summary>
        public static bool ValidareTelefon(string telefon)
        {
            if (string.IsNullOrWhiteSpace(telefon))
                return true; // Telefon optional

            // Acceptă: 0700000000, +40700000000, 0xx xxxx xxxx
            string pattern = @"^(\+40|0)[0-9]{9}$";
            return Regex.IsMatch(telefon.Replace(" ", ""), pattern);
        }

        /// <summary>
        /// Validează ISBN (ISBN-10 sau ISBN-13)
        /// </summary>
        public static bool ValidareISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                return true; // ISBN optional

            isbn = isbn.Replace("-", "").Replace(" ", "");

            if (isbn.Length == 10)
                return ValidareISBN10(isbn);
            else if (isbn.Length == 13)
                return ValidareISBN13(isbn);
            else
                return false;
        }

        private static bool ValidareISBN10(string isbn)
        {
            int suma = 0;
            for (int i = 0; i < 10; i++)
            {
                if (!int.TryParse(isbn[i].ToString(), out int digit))
                    return false;
                suma += digit * (10 - i);
            }
            return suma % 11 == 0;
        }

        private static bool ValidareISBN13(string isbn)
        {
            int suma = 0;
            for (int i = 0; i < 13; i++)
            {
                if (!int.TryParse(isbn[i].ToString(), out int digit))
                    return false;
                suma += digit * (i % 2 == 0 ? 1 : 3);
            }
            return suma % 10 == 0;
        }

        /// <summary>
        /// Validează an publicării
        /// </summary>
        public static bool ValidareAnPublicare(int an)
        {
            int anCurent = DateTime.Now.Year;
            return an >= 1000 && an <= anCurent + 1;
        }

        /// <summary>
        /// Validează număr de pagini
        /// </summary>
        public static bool ValidareNrPagini(int pagini)
        {
            return pagini > 0 && pagini < 10000;
        }

        /// <summary>
        /// Validează preț
        /// </summary>
        public static bool ValidarePret(decimal pret)
        {
            return pret >= 0 && pret <= 999999.99m;
        }

        /// <summary>
        /// Verifică dacă string este gol
        /// </summary>
        public static bool EsteGol(string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        /// <summary>
        /// Verifică lungimea string-ului
        /// </summary>
        public static bool ValidareLungime(string text, int min = 1, int max = 500)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;
            return text.Length >= min && text.Length <= max;
        }
    }
}