using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BibliotecaScolara.Models;
using BibliotecaScolara.Database;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.Managers
{
    public class CategorieManager
    {
        /// <summary>
        /// Obține toate categoriile
        /// </summary>
        public static List<Categorie> GetAll()
        {
            List<Categorie> categorii = new List<Categorie>();
            string query = "SELECT * FROM Categorii ORDER BY NumeCategorie";
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                categorii.Add(MapToCategorie(row));
            }
            return categorii;
        }

        /// <summary>
        /// Obține o categorie după ID
        /// </summary>
        public static Categorie GetByID(int id)
        {
            string query = "SELECT * FROM Categorii WHERE IDCategorie = @ID";
            SqlParameter[] parameters = new[] { new SqlParameter("@ID", id) };
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            if (dt.Rows.Count > 0)
            {
                return MapToCategorie(dt.Rows[0]);
            }
            return null;
        }

        /// <summary>
        /// Adaugă o categorie nouă
        /// </summary>
        public static bool Insert(Categorie categorie)
        {
            string query = @"
                INSERT INTO Categorii (NumeCategorie, Descriere)
                VALUES (@Nume, @Descriere)";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@Nume", categorie.NumeCategorie ?? ""),
                new SqlParameter("@Descriere", categorie.Descriere ?? "")
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Actualizează o categorie
        /// </summary>
        public static bool Update(Categorie categorie)
        {
            string query = @"
                UPDATE Categorii 
                SET NumeCategorie = @Nume, Descriere = @Descriere
                WHERE IDCategorie = @ID";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@ID", categorie.IDCategorie),
                new SqlParameter("@Nume", categorie.NumeCategorie ?? ""),
                new SqlParameter("@Descriere", categorie.Descriere ?? "")
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Șterge o categorie
        /// </summary>
        public static bool Delete(int id)
        {
            string query = "DELETE FROM Categorii WHERE IDCategorie = @ID";
            SqlParameter[] parameters = new[] { new SqlParameter("@ID", id) };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Caută categorii după nume
        /// </summary>
        public static List<Categorie> Search(string searchTerm)
        {
            List<Categorie> categorii = new List<Categorie>();
            string query = "SELECT * FROM Categorii WHERE NumeCategorie LIKE @Search ORDER BY NumeCategorie";

            SqlParameter[] parameters = new[] { new SqlParameter("@Search", "%" + searchTerm + "%") };
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                categorii.Add(MapToCategorie(row));
            }
            return categorii;
        }

        /// <summary>
        /// Mapează DataRow la obiect Categorie
        /// </summary>
        private static Categorie MapToCategorie(DataRow row)
        {
            return new Categorie
            {
                IDCategorie = (int)row["IDCategorie"],
                NumeCategorie = row["NumeCategorie"].ToString(),
                Descriere = row["Descriere"].ToString()
            };
        }
    }
}