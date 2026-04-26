using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BibliotecaScolara.Models;
using BibliotecaScolara.Database;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.Managers
{
    public class CarteManager
    {
        /// <summary>
        /// Obține toate cărțile cu detalii autor și editură
        /// </summary>
        public static List<Carte> GetAll()
        {
            List<Carte> carti = new List<Carte>();
            string query = @"
                SELECT c.*, a.Nume AS NumeAutor, a.Prenume AS PrenumeAutor, 
                       e.NumeEditura, cat.NumeCategorie
                FROM Carti c
                JOIN Autori a ON c.IDAutor = a.IDAutor
                JOIN Edituri e ON c.IDEditura = e.IDEditura
                JOIN Categorii cat ON c.IDCategorie = cat.IDCategorie
                ORDER BY c.Titlu";
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                carti.Add(MapToCarte(row));
            }
            return carti;
        }

        /// <summary>
        /// Obține o carte după ID
        /// </summary>
        public static Carte GetByID(int id)
        {
            string query = @"
                SELECT c.*, a.Nume AS NumeAutor, a.Prenume AS PrenumeAutor, 
                       e.NumeEditura, cat.NumeCategorie
                FROM Carti c
                JOIN Autori a ON c.IDAutor = a.IDAutor
                JOIN Edituri e ON c.IDEditura = e.IDEditura
                JOIN Categorii cat ON c.IDCategorie = cat.IDCategorie
                WHERE c.IDCarte = @ID";
            
            SqlParameter[] parameters = new[] { new SqlParameter("@ID", id) };
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            
            if (dt.Rows.Count > 0)
            {
                return MapToCarte(dt.Rows[0]);
            }
            return null;
        }

        /// <summary>
        /// Adaugă o carte nouă
        /// </summary>
        public static bool Insert(Carte carte)
        {
            // Validare ISBN unic
            if (ISBNExists(carte.ISBN))
            {
                Mesaje.Eroare("ISBN-ul există deja în sistem!", "ISBN Duplicat");
                return false;
            }

            string query = @"
                INSERT INTO Carti (Titlu, IDAutor, IDEditura, IDCategorie, AnPublicarii, ISBN, NrPagini)
                VALUES (@Titlu, @IDAutor, @IDEditura, @IDCategorie, @AnPublicarii, @ISBN, @NrPagini)";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@Titlu", carte.Titlu ?? ""),
                new SqlParameter("@IDAutor", carte.IDAutor),
                new SqlParameter("@IDEditura", carte.IDEditura),
                new SqlParameter("@IDCategorie", carte.IDCategorie),
                new SqlParameter("@AnPublicarii", carte.AnPublicarii ?? (object)DBNull.Value),
                new SqlParameter("@ISBN", carte.ISBN ?? ""),
                new SqlParameter("@NrPagini", carte.NrPagini ?? (object)DBNull.Value)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Actualizează o carte
        /// </summary>
        public static bool Update(Carte carte)
        {
            string query = @"
                UPDATE Carti 
                SET Titlu = @Titlu, IDAutor = @IDAutor, IDEditura = @IDEditura, 
                    IDCategorie = @IDCategorie, AnPublicarii = @AnPublicarii, 
                    ISBN = @ISBN, NrPagini = @NrPagini
                WHERE IDCarte = @ID";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@ID", carte.IDCarte),
                new SqlParameter("@Titlu", carte.Titlu ?? ""),
                new SqlParameter("@IDAutor", carte.IDAutor),
                new SqlParameter("@IDEditura", carte.IDEditura),
                new SqlParameter("@IDCategorie", carte.IDCategorie),
                new SqlParameter("@AnPublicarii", carte.AnPublicarii ?? (object)DBNull.Value),
                new SqlParameter("@ISBN", carte.ISBN ?? ""),
                new SqlParameter("@NrPagini", carte.NrPagini ?? (object)DBNull.Value)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Șterge o carte
        /// </summary>
        public static bool Delete(int id)
        {
            string query = "DELETE FROM Carti WHERE IDCarte = @ID";
            SqlParameter[] parameters = new[] { new SqlParameter("@ID", id) };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Caută cărți după titlu, autor sau ISBN
        /// </summary>
        public static List<Carte> Search(string searchTerm)
        {
            List<Carte> carti = new List<Carte>();
            string query = @"
                SELECT c.*, a.Nume AS NumeAutor, a.Prenume AS PrenumeAutor, 
                       e.NumeEditura, cat.NumeCategorie
                FROM Carti c
                JOIN Autori a ON c.IDAutor = a.IDAutor
                JOIN Edituri e ON c.IDEditura = e.IDEditura
                JOIN Categorii cat ON c.IDCategorie = cat.IDCategorie
                WHERE c.Titlu LIKE @Search OR a.Nume LIKE @Search 
                   OR a.Prenume LIKE @Search OR c.ISBN LIKE @Search
                ORDER BY c.Titlu";

            SqlParameter[] parameters = new[] { new SqlParameter("@Search", "%" + searchTerm + "%") };
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                carti.Add(MapToCarte(row));
            }
            return carti;
        }

        /// <summary>
        /// Obține cărți după categorie
        /// </summary>
        public static List<Carte> GetByCategorie(int categorieId)
        {
            List<Carte> carti = new List<Carte>();
            string query = @"
                SELECT c.*, a.Nume AS NumeAutor, a.Prenume AS PrenumeAutor, 
                       e.NumeEditura, cat.NumeCategorie
                FROM Carti c
                JOIN Autori a ON c.IDAutor = a.IDAutor
                JOIN Edituri e ON c.IDEditura = e.IDEditura
                JOIN Categorii cat ON c.IDCategorie = cat.IDCategorie
                WHERE c.IDCategorie = @IDCategorie
                ORDER BY c.Titlu";

            SqlParameter[] parameters = new[] { new SqlParameter("@IDCategorie", categorieId) };
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                carti.Add(MapToCarte(row));
            }
            return carti;
        }

        /// <summary>
        /// Verifică dacă ISBN-ul există
        /// </summary>
        private static bool ISBNExists(string isbn)
        {
            string query = "SELECT COUNT(*) FROM Carti WHERE ISBN = @ISBN";
            SqlParameter[] parameters = new[] { new SqlParameter("@ISBN", isbn) };
            
            object result = DatabaseConnection.ExecuteScalar(query, parameters);
            return result != null && int.TryParse(result.ToString(), out int count) && count > 0;
        }

        /// <summary>
        /// Mapează DataRow la obiect Carte
        /// </summary>
        private static Carte MapToCarte(DataRow row)
        {
            return new Carte
            {
                IDCarte = (int)row["IDCarte"],
                Titlu = row["Titlu"].ToString(),
                IDAutor = (int)row["IDAutor"],
                IDEditura = (int)row["IDEditura"],
                IDCategorie = (int)row["IDCategorie"],
                AnPublicarii = row["AnPublicarii"] == DBNull.Value ? null : (int?)row["AnPublicarii"],
                ISBN = row["ISBN"].ToString(),
                NrPagini = row["NrPagini"] == DBNull.Value ? null : (int?)row["NrPagini"],
                DataAdaugarii = (DateTime)row["DataAdaugarii"],
                NumeAutor = row["NumeAutor"].ToString() + " " + row["PrenumeAutor"].ToString(),
                NumeEditura = row["NumeEditura"].ToString(),
                NumeCategorie = row["NumeCategorie"].ToString()
            };
        }
    }
}
