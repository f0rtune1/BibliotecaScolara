using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using BibliotecaScolara.Models;
using BibliotecaScolara.Database;

namespace BibliotecaScolara.Managers
{
    public class UtilizatorManager
    {
        /// <summary>
        /// Hashes a password using SHA256
        /// </summary>
        public static string HashParola(string parola)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(parola));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        /// <summary>
        /// Autentifică un utilizator și returnează obiectul dacă reușește
        /// </summary>
        public static Utilizator Autentifica(string numeUtilizator, string parola)
        {
            string parolaHash = HashParola(parola);
            string query = @"
                SELECT * FROM Utilizatori 
                WHERE NumeUtilizator = @Nume AND ParolaHash = @Parola";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@Nume", numeUtilizator ?? ""),
                new SqlParameter("@Parola", parolaHash)
            };

            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            if (dt.Rows.Count > 0)
                return MapToUtilizator(dt.Rows[0]);
            return null;
        }

        /// <summary>
        /// Obține toți utilizatorii
        /// </summary>
        public static List<Utilizator> GetAll()
        {
            List<Utilizator> utilizatori = new List<Utilizator>();
            string query = "SELECT * FROM Utilizatori ORDER BY NumeUtilizator";

            DataTable dt = DatabaseConnection.ExecuteDataTable(query);
            foreach (DataRow row in dt.Rows)
                utilizatori.Add(MapToUtilizator(row));
            return utilizatori;
        }

        /// <summary>
        /// Adaugă un utilizator nou
        /// </summary>
        public static bool Insert(Utilizator utilizator, string parola)
        {
            string query = @"
                INSERT INTO Utilizatori (NumeUtilizator, ParolaHash, Rol, Email)
                VALUES (@Nume, @Parola, @Rol, @Email)";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@Nume", utilizator.NumeUtilizator ?? ""),
                new SqlParameter("@Parola", HashParola(parola)),
                new SqlParameter("@Rol", utilizator.Rol ?? "bibliotecar"),
                new SqlParameter("@Email", utilizator.Email ?? (object)DBNull.Value)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Schimbă parola unui utilizator
        /// </summary>
        public static bool SchimbaParola(int idUtilizator, string parolaNoua)
        {
            string query = "UPDATE Utilizatori SET ParolaHash = @Parola WHERE IDUtilizator = @ID";
            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@ID", idUtilizator),
                new SqlParameter("@Parola", HashParola(parolaNoua))
            };
            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Șterge un utilizator
        /// </summary>
        public static bool Delete(int id)
        {
            string query = "DELETE FROM Utilizatori WHERE IDUtilizator = @ID";
            SqlParameter[] parameters = new[] { new SqlParameter("@ID", id) };
            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Verifică dacă există cel puțin un utilizator admin
        /// </summary>
        public static bool ExistaAdmin()
        {
            string query = "SELECT COUNT(*) FROM Utilizatori WHERE Rol = 'admin'";
            object result = DatabaseConnection.ExecuteScalar(query);
            return result != null && int.TryParse(result.ToString(), out int count) && count > 0;
        }

        /// <summary>
        /// Mapează DataRow la obiect Utilizator
        /// </summary>
        private static Utilizator MapToUtilizator(DataRow row)
        {
            return new Utilizator
            {
                IDUtilizator = (int)row["IDUtilizator"],
                NumeUtilizator = row["NumeUtilizator"].ToString(),
                ParolaHash = row["ParolaHash"].ToString(),
                Rol = row["Rol"].ToString(),
                Email = row["Email"] == DBNull.Value ? "" : row["Email"].ToString(),
                DataAdaugarii = (DateTime)row["DataAdaugarii"]
            };
        }
    }
}
