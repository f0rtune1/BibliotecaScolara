using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.Database
{
    public static class DatabaseConnection
    {
        private static string _connectionString;

        static DatabaseConnection()
        {
            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["BibliotecaDB"].ConnectionString;
            }
            catch
            {
                _connectionString = @"Server=localhost\SQLEXPRESS;Database=BibliotecaScolara;Trusted_Connection=true;";
            }
        }

        /// <summary>
        /// Obține conexiunea la baza de date
        /// </summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Testează conexiunea la baza de date
        /// </summary>
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Mesaje.Eroare($"Eroare conexiune: {ex.Message}", "Conexiune BD");
                return false;
            }
        }

        /// <summary>
        /// Execută o comandă SQL fără a returna rezultate
        /// </summary>
        public static bool ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandTimeout = 30;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Violation of primary key
                    Mesaje.Eroare("Înregistrare duplicată! Valoarea există deja.", "Eroare");
                else if (ex.Number == 547) // Violation of foreign key
                    Mesaje.Eroare("Nu se poate efectua operația! Înregistrarea are dependențe.", "Eroare");
                else
                    Mesaje.Eroare($"Eroare bază de date: {ex.Message}", "Eroare");
                return false;
            }
            catch (Exception ex)
            {
                Mesaje.Eroare($"Eroare: {ex.Message}", "Eroare");
                return false;
            }
        }

        /// <summary>
        /// Execută o comandă SQL și returnează un DataTable
        /// </summary>
        public static DataTable ExecuteDataTable(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandTimeout = 30;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                Mesaje.Eroare($"Eroare încărcare date: {ex.Message}", "Eroare");
                return dt;
            }
        }

        /// <summary>
        /// Execută o comandă SQL și returnează o valoare scalară
        /// </summary>
        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandTimeout = 30;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Mesaje.Eroare($"Eroare: {ex.Message}", "Eroare");
                return null;
            }
        }

        /// <summary>
        /// Execută o procedură stocată
        /// </summary>
        public static DataTable ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 30;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                Mesaje.Eroare($"Eroare procedură: {ex.Message}", "Eroare");
                return dt;
            }
        }

        /// <summary>
        /// Verific dacă o valoare există în baza de date
        /// </summary>
        public static bool Exists(string query, SqlParameter[] parameters = null)
        {
            try
            {
                object result = ExecuteScalar(query, parameters);
                return result != null && int.TryParse(result.ToString(), out int count) && count > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
