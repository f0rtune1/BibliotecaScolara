using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BibliotecaScolara.Database
{
    public static class DatabaseConnection
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["BibliotecaDB"].ConnectionString;
        }

        /// <summary>
        /// Execută query și returnează DataTable
        /// </summary>
        public static DataTable ExecuteDataTable(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                            command.Parameters.AddRange(parameters);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Execută query non-query (INSERT, UPDATE, DELETE)
        /// </summary>
        public static bool ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                            command.Parameters.AddRange(parameters);

                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Execută query și returnează scalar value
        /// </summary>
        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                            command.Parameters.AddRange(parameters);

                        connection.Open();
                        return command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Testează conexiunea la baza de date
        /// </summary>
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    return connection.State == ConnectionState.Open;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}