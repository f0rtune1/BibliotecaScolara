using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BibliotecaScolara.Models;
using BibliotecaScolara.Database;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.Managers
{
    public class AutorManager
    {
        /// <summary>
        /// Obține toți autorii
        /// </summary>
        public static List<Autor> GetAll()
        {
            List<Autor> autori = new List<Autor>();
            string query = "SELECT * FROM Autori ORDER BY Nume, Prenume";
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                autori.Add(MapToAutor(row));
            }
            return autori;
        }

        /// <summary>
        /// Obține un autor după ID
        /// </summary>
        public static Autor GetByID(int id)
        {
            string query = "SELECT * FROM Autori WHERE IDAutor = @ID";
            SqlParameter[] parameters = new[] { new SqlParameter("@ID", id) };
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            if (dt.Rows.Count > 0)
            {
                return MapToAutor(dt.Rows[0]);
            }
            return null;
        }

        /// <summary>
        /// Adaugă un autor nou
        /// </summary>
        public static bool Insert(Autor autor)
        {
            string query = @"
                INSERT INTO Autori (Nume, Prenume, DataNasterii, Nationalitate, BiografieBrieșă)
                VALUES (@Nume, @Prenume, @DataNasterii, @Nationalitate, @Biografie)";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@Nume", autor.Nume ?? ""),
                new SqlParameter("@Prenume", autor.Prenume ?? ""),
                new SqlParameter("@DataNasterii", autor.DataNasterii ?? (object)DBNull.Value),
                new SqlParameter("@Nationalitate", autor.Nationalitate ?? ""),
                new SqlParameter("@Biografie", autor.BiografieBrieșă ?? "")
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Actualizează un autor
        /// </summary>
        public static bool Update(Autor autor)
        {
            string query = @"
                UPDATE Autori 
                SET Nume = @Nume, Prenume = @Prenume, DataNasterii = @DataNasterii, 
                    Nationalitate = @Nationalitate, BiografieBrieșă = @Biografie
                WHERE IDAutor = @ID";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@ID", autor.IDAutor),
                new SqlParameter("@Nume", autor.Nume ?? ""),
                new SqlParameter("@Prenume", autor.Prenume ?? ""),
                new SqlParameter("@DataNasterii", autor.DataNasterii ?? (object)DBNull.Value),
                new SqlParameter("@Nationalitate", autor.Nationalitate ?? ""),
                new SqlParameter("@Biografie", autor.BiografieBrieșă ?? "")
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Șterge un autor (cu verificare)
        /// </summary>
        public static bool Delete(int id)
        {
            // Verifică dacă autorul are cărți
            if (HasBooks(id))
            {
                Mesaje.ImpossibleDelete(Constante.Erori.AUTOR_ARE_CARTI);
                return false;
            }

            string query = "DELETE FROM Autori WHERE IDAutor = @ID";
            SqlParameter[] parameters = new[] { new SqlParameter("@ID", id) };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Caută autori după nume sau prenume
        /// </summary>
        public static List<Autor> Search(string searchTerm)
        {
            List<Autor> autori = new List<Autor>();
            string query = @"
                SELECT * FROM Autori 
                WHERE Nume LIKE @Search OR Prenume LIKE @Search
                ORDER BY Nume, Prenume";

            SqlParameter[] parameters = new[] { new SqlParameter("@Search", "%" + searchTerm + "%") };
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                autori.Add(MapToAutor(row));
            }
            return autori;
        }

        /// <summary>
        /// Verifică dacă autorul are cărți
        /// </summary>
        private static bool HasBooks(int autorId)
        {
            string query = "SELECT COUNT(*) FROM Carti WHERE IDAutor = @ID";
            SqlParameter[] parameters = new[] { new SqlParameter("@ID", autorId) };
            
            object result = DatabaseConnection.ExecuteScalar(query, parameters);
            return result != null && int.TryParse(result.ToString(), out int count) && count > 0;
        }

        /// <summary>
        /// Mapează DataRow la obiect Autor
        /// </summary>
        private static Autor MapToAutor(DataRow row)
        {
            return new Autor
            {
                IDAutor = (int)row["IDAutor"],
                Nume = row["Nume"].ToString(),
                Prenume = row["Prenume"].ToString(),
                DataNasterii = row["DataNasterii"] == DBNull.Value ? null : (DateTime?)row["DataNasterii"],
                Nationalitate = row["Nationalitate"].ToString(),
                BiografieBrieșă = row["BiografieBrieșă"].ToString(),
                DataAdaugarii = (DateTime)row["DataAdaugarii"]
            };
        }
    }
}