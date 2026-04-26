using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BibliotecaScolara.Models;
using BibliotecaScolara.Database;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.Managers
{
    public class EdituraManager
    {
        /// <summary>
        /// Obține toate editurile
        /// </summary>
        public static List<Editura> GetAll()
        {
            List<Editura> edituri = new List<Editura>();
            string query = "SELECT * FROM Edituri ORDER BY NumeEditura";
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                edituri.Add(MapToEditura(row));
            }
            return edituri;
        }

        /// <summary>
        /// Obține o editură după ID
        /// </summary>
        public static Editura GetByID(int id)
        {
            string query = "SELECT * FROM Edituri WHERE IDEditura = @ID";
            SqlParameter[] parameters = new[] { new SqlParameter("@ID", id) };
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            if (dt.Rows.Count > 0)
            {
                return MapToEditura(dt.Rows[0]);
            }
            return null;
        }

        /// <summary>
        /// Adaugă o editură nouă
        /// </summary>
        public static bool Insert(Editura editura)
        {
            string query = @"
                INSERT INTO Edituri (NumeEditura, Adresa, Telefon, Email)
                VALUES (@Nume, @Adresa, @Telefon, @Email)";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@Nume", editura.NumeEditura ?? ""),
                new SqlParameter("@Adresa", editura.Adresa ?? ""),
                new SqlParameter("@Telefon", editura.Telefon ?? ""),
                new SqlParameter("@Email", editura.Email ?? "")
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Actualizează o editură
        /// </summary>
        public static bool Update(Editura editura)
        {
            string query = @"
                UPDATE Edituri 
                SET NumeEditura = @Nume, Adresa = @Adresa, Telefon = @Telefon, Email = @Email
                WHERE IDEditura = @ID";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@ID", editura.IDEditura),
                new SqlParameter("@Nume", editura.NumeEditura ?? ""),
                new SqlParameter("@Adresa", editura.Adresa ?? ""),
                new SqlParameter("@Telefon", editura.Telefon ?? ""),
                new SqlParameter("@Email", editura.Email ?? "")
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Șterge o editură (cu verificare)
        /// </summary>
        public static bool Delete(int id)
        {
            // Verifică dacă editura are cărți
            if (HasBooks(id))
            {
                Mesaje.ImpossibleDelete(Constante.Erori.EDITURA_ARE_CARTI);
                return false;
            }

            string query = "DELETE FROM Edituri WHERE IDEditura = @ID";
            SqlParameter[] parameters = new[] { new SqlParameter("@ID", id) };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Caută edituri după nume
        /// </summary>
        public static List<Editura> Search(string searchTerm)
        {
            List<Editura> edituri = new List<Editura>();
            string query = "SELECT * FROM Edituri WHERE NumeEditura LIKE @Search ORDER BY NumeEditura";

            SqlParameter[] parameters = new[] { new SqlParameter("@Search", "%" + searchTerm + "%") };
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                edituri.Add(MapToEditura(row));
            }
            return edituri;
        }

        /// <summary>
        /// Verifică dacă editura are cărți asociate
        /// </summary>
        private static bool HasBooks(int editulaId)
        {
            string query = "SELECT COUNT(*) FROM Carti WHERE IDEditura = @ID";
            SqlParameter[] parameters = new[] { new SqlParameter("@ID", editulaId) };
            
            object result = DatabaseConnection.ExecuteScalar(query, parameters);
            return result != null && int.TryParse(result.ToString(), out int count) && count > 0;
        }

        /// <summary>
        /// Mapează DataRow la obiect Editura
        /// </summary>
        private static Editura MapToEditura(DataRow row)
        {
            return new Editura
            {
                IDEditura = (int)row["IDEditura"],
                NumeEditura = row["NumeEditura"].ToString(),
                Adresa = row["Adresa"].ToString(),
                Telefon = row["Telefon"].ToString(),
                Email = row["Email"].ToString(),
                DataAdaugarii = (DateTime)row["DataAdaugarii"]
            };
        }
    }
}