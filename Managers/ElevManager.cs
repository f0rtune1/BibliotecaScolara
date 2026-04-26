using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BibliotecaScolara.Models;
using BibliotecaScolara.Database;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.Managers
{
    public class ElevManager
    {
        /// <summary>
        /// Obține toți elevii
        /// </summary>
        public static List<Elev> GetAll()
        {
            List<Elev> elevi = new List<Elev>();
            string query = "SELECT * FROM Elevi ORDER BY Nume, Prenume";
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                elevi.Add(MapToElev(row));
            }
            return elevi;
        }

        /// <summary>
        /// Obține un elev după ID
        /// </summary>
        public static Elev GetByID(int id)
        {
            string query = "SELECT * FROM Elevi WHERE IDElev = @ID";
            SqlParameter[] parameters = new[] { new SqlParameter("@ID", id) };
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            if (dt.Rows.Count > 0)
            {
                return MapToElev(dt.Rows[0]);
            }
            return null;
        }

        /// <summary>
        /// Adaugă un elev nou
        /// </summary>
        public static bool Insert(Elev elev)
        {
            string query = @"
                INSERT INTO Elevi (Nume, Prenume, Clasa, Email, Telefon, Status)
                VALUES (@Nume, @Prenume, @Clasa, @Email, @Telefon, @Status)";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@Nume", elev.Nume ?? ""),
                new SqlParameter("@Prenume", elev.Prenume ?? ""),
                new SqlParameter("@Clasa", elev.Clasa ?? ""),
                new SqlParameter("@Email", elev.Email ?? ""),
                new SqlParameter("@Telefon", elev.Telefon ?? ""),
                new SqlParameter("@Status", elev.Status ?? Constante.StareElev.ACTIV)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Actualizează un elev
        /// </summary>
        public static bool Update(Elev elev)
        {
            string query = @"
                UPDATE Elevi 
                SET Nume = @Nume, Prenume = @Prenume, Clasa = @Clasa, 
                    Email = @Email, Telefon = @Telefon, Status = @Status
                WHERE IDElev = @ID";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@ID", elev.IDElev),
                new SqlParameter("@Nume", elev.Nume ?? ""),
                new SqlParameter("@Prenume", elev.Prenume ?? ""),
                new SqlParameter("@Clasa", elev.Clasa ?? ""),
                new SqlParameter("@Email", elev.Email ?? ""),
                new SqlParameter("@Telefon", elev.Telefon ?? ""),
                new SqlParameter("@Status", elev.Status ?? Constante.StareElev.ACTIV)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Șterge un elev (cu verificare)
        /// </summary>
        public static bool Delete(int id)
        {
            // Verifică dacă elevul are împrumuturi active
            if (HasActiveLoans(id))
            {
                Mesaje.ImpossibleDelete(Constante.Erori.ELEV_ARE_IMPRUMTURI);
                return false;
            }

            string query = "DELETE FROM Elevi WHERE IDElev = @ID";
            SqlParameter[] parameters = new[] { new SqlParameter("@ID", id) };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Caută elevi după nume, prenume sau clasă
        /// </summary>
        public static List<Elev> Search(string searchTerm)
        {
            List<Elev> elevi = new List<Elev>();
            string query = @"
                SELECT * FROM Elevi 
                WHERE Nume LIKE @Search OR Prenume LIKE @Search OR Clasa LIKE @Search
                ORDER BY Nume, Prenume";

            SqlParameter[] parameters = new[] { new SqlParameter("@Search", "%" + searchTerm + "%") };
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                elevi.Add(MapToElev(row));
            }
            return elevi;
        }

        /// <summary>
        /// Obține elevi după clasă
        /// </summary>
        public static List<Elev> GetByClasa(string clasa)
        {
            List<Elev> elevi = new List<Elev>();
            string query = "SELECT * FROM Elevi WHERE Clasa = @Clasa ORDER BY Nume, Prenume";
            
            SqlParameter[] parameters = new[] { new SqlParameter("@Clasa", clasa) };
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                elevi.Add(MapToElev(row));
            }
            return elevi;
        }

        /// <summary>
        /// Obține elevi activi
        /// </summary>
        public static List<Elev> GetActive()
        {
            List<Elev> elevi = new List<Elev>();
            string query = "SELECT * FROM Elevi WHERE Status = @Status ORDER BY Nume, Prenume";
            
            SqlParameter[] parameters = new[] { new SqlParameter("@Status", Constante.StareElev.ACTIV) };
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                elevi.Add(MapToElev(row));
            }
            return elevi;
        }

        /// <summary>
        /// Verifică dacă elevul are împrumuturi active
        /// </summary>
        private static bool HasActiveLoans(int elevId)
        {
            string query = @"
                SELECT COUNT(*) FROM Imprumturi 
                WHERE IDElev = @ID AND DataRestituire IS NULL";
            
            SqlParameter[] parameters = new[] { new SqlParameter("@ID", elevId) };
            
            object result = DatabaseConnection.ExecuteScalar(query, parameters);
            return result != null && int.TryParse(result.ToString(), out int count) && count > 0;
        }

        /// <summary>
        /// Mapează DataRow la obiect Elev
        /// </summary>
        private static Elev MapToElev(DataRow row)
        {
            return new Elev
            {
                IDElev = (int)row["IDElev"],
                Nume = row["Nume"].ToString(),
                Prenume = row["Prenume"].ToString(),
                Clasa = row["Clasa"].ToString(),
                Email = row["Email"].ToString(),
                Telefon = row["Telefon"].ToString(),
                DataInscrierii = (DateTime)row["DataInscrierii"],
                Status = row["Status"].ToString(),
                DataAdaugarii = (DateTime)row["DataAdaugarii"]
            };
        }
    }
}
