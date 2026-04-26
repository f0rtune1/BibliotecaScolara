using System;
using System.Windows.Forms;
using BibliotecaScolara.Database;
using BibliotecaScolara.UI;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Test database connection
            if (!DatabaseConnection.TestConnection())
            {
                Mesaje.Eroare(
                    "Eroare de conexiune la baza de date!\n\n" +
                    "Asigură-te că:\n" +
                    "1. SQL Server este pornit\n" +
                    "2. Connection string din App.config este corect\n" +
                    "3. Baza de date 'BibliotecaScolara' există",
                    "Eroare Bază de Date"
                );
                return;
            }

            Application.Run(new FrmMain());
        }
    }
}