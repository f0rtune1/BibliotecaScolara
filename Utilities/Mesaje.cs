using System;
using System.Windows.Forms;

namespace BibliotecaScolara.Utilities
{
    public static class Mesaje
    {
        /// <summary>
        /// Afișează mesaj de informare
        /// </summary>
        public static void Informare(string mesaj, string titlu = "Informare")
        {
            MessageBox.Show(mesaj, titlu, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Afișează mesaj de eroare
        /// </summary>
        public static void Eroare(string mesaj, string titlu = "Eroare")
        {
            MessageBox.Show(mesaj, titlu, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Afișează mesaj de avertisment
        /// </summary>
        public static void Avertisment(string mesaj, string titlu = "Avertisment")
        {
            MessageBox.Show(mesaj, titlu, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Afișează mesaj de confirmare
        /// </summary>
        public static DialogResult Confirmare(string mesaj, string titlu = "Confirmare")
        {
            return MessageBox.Show(mesaj, titlu, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Afișează mesaj de ștergere cu confirmare
        /// </summary>
        public static DialogResult ConfirmareSt()
        {
            return MessageBox.Show(
                "Sunteți sigur că doriți să ștergeți această înregistrare?",
                "Confirmare ștergere",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
        }

        /// <summary>
        /// Afișează mesaj de ștergere imposibil
        /// </summary>
        public static void ImpossibleDelete(string motiv)
        {
            MessageBox.Show(
                motiv,
                "Ștergere imposibilă",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        /// <summary>
        /// Afișează mesaj de succes
        /// </summary>
        public static void Succes(string mesaj, string titlu = "Succes")
        {
            MessageBox.Show(mesaj, titlu, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Afișează mesaj de validare
        /// </summary>
        public static void Validare(string mesaj)
        {
            MessageBox.Show(
                mesaj,
                "Validare date",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        /// <summary>
        /// Afișează mesaj pentru operație nereușită
        /// </summary>
        public static void OperatieFailed(string operatie = "Operația")
        {
            MessageBox.Show(
                $"{operatie} a eșuat! Vă rugăm verificați datele și încercați din nou.",
                "Eroare",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}