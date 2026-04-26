using System.Windows.Forms;

namespace BibliotecaScolara.Utilities
{
    public static class Mesaje
    {
        public static void Succes(string mesaj, string titlu = "Succes")
        {
            MessageBox.Show(mesaj, titlu, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Eroare(string mesaj, string titlu = "Eroare")
        {
            MessageBox.Show(mesaj, titlu, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Avertisment(string mesaj, string titlu = "Avertisment")
        {
            MessageBox.Show(mesaj, titlu, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult Intrebare(string mesaj, string titlu = "Confirmare")
        {
            return MessageBox.Show(mesaj, titlu, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult IntrebareStergere(string numElement)
        {
            string mesaj = $"Sigur doriți să ștergeți {numElement}?\n\nAceastă acțiune nu poate fi anulată!";
            return Intrebare(mesaj, "Ștergere Confirmare");
        }

        public static DialogResult IntrebareStergereRelationat(string mesaj)
        {
            string mesajComplet = $"{mesaj}\n\nAceastă acțiune va șterge și datele asociate și nu poate fi anulată!";
            return Intrebare(mesajComplet, "Ștergere - Avertisment");
        }

        // Mesaje predefinite pentru validări
        public static void CampulEsteObligatoriu(string numCamp)
        {
            Eroare($"Câmpul '{numCamp}' este obligatoriu!", "Validare");
        }

        public static void FormatInvalid(string numCamp)
        {
            Eroare($"Format invalid pentru câmpul '{numCamp}'!", "Validare");
        }

        public static void ValoareDuplicata(string numCamp)
        {
            Eroare($"Valoarea în câmpul '{numCamp}' există deja în sistem!", "Validare");
        }

        public static void ImpossibleDelete(string motiv)
        {
            Eroare($"Nu se poate șterge înregistrarea!\n\n{motiv}", "Ștergere Imposibilă");
        }
    }
}
