using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BibliotecaScolara.Models;
using BibliotecaScolara.Database;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.Managers
{
    public class ExemplarManager
    {
        /// <summary>
        /// Obține toate exemplarele cu detalii carte
        /// </summary>
        public static List<Exemplar> GetAll()
        {
            List<Exemplar> exemplare = new List<Exemplar>();
            string query = @"
                SELECT e.*, c.Titlu AS TitluCarte
                FROM Exemplare e
                JOIN Carti c ON e.IDCarte = c.IDCarte
                ORDER BY e.CoduInventar";
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                exemplare.Add(MapToExemplar(row));
            }
            return exemplare;
        }

        /// <summary>
        /// Obține un exemplar după ID
        /// </summary>
        public static Exemplar GetByID(int id)
        {
            string query = @"
                SELECT e.*, c.Titlu AS TitluCarte
                FROM Exemplare e
                JOIN Carti c ON e.IDCarte = c.IDCarte
                WHERE e.IDExemplar = @ID";
            
            SqlParameter[] parameters = new[] { new SqlParameter("@ID", id) };
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            
            if (dt.Rows.Count > 0)
            {
                return MapToExemplar(dt.Rows[0]);
            }
            return null;
        }

        /// <summary>
        /// Obține exemplare după carte
        /// </summary>
        public static List<Exemplar> GetByCarte(int carteId)
        {
            List<Exemplar> exemplare = new List<Exemplar>();
            string query = @"
                SELECT e.*, c.Titlu AS TitluCarte
                FROM Exemplare e
                JOIN Carti c ON e.IDCarte = c.IDCarte
                WHERE e.IDCarte = @IDCarte
                ORDER BY e.CoduInventar";

            SqlParameter[] parameters = new[] { new SqlParameter("@IDCarte", carteId) };
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                exemplare.Add(MapToExemplar(row));
            }
            return exemplare;
        }

        /// <summary>
        /// Adaugă un exemplar nou
        /// </summary>
        public static bool Insert(Exemplar exemplar)
        {
            string query = @"
                INSERT INTO Exemplare (IDCarte, CoduInventar, StareExemplar, DataAchizitiei, Pret)
                VALUES (@IDCarte, @Cod, @Stare, @DataAchizitie, @Pret)";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@IDCarte", exemplar.IDCarte),
                new SqlParameter("@Cod", exemplar.CoduInventar ?? ""),
                new SqlParameter("@Stare", exemplar.StareExemplar ?? Constante.StareExemplar.BUNA),
                new SqlParameter("@DataAchizitie", exemplar.DataAchizitiei),
                new SqlParameter("@Pret", exemplar.Pret ?? (object)DBNull.Value)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Actualizează un exemplar
        /// </summary>
        public static bool Update(Exemplar exemplar)
        {
            string query = @"
                UPDATE Exemplare 
                SET IDCarte = @IDCarte, CoduInventar = @Cod, StareExemplar = @Stare, 
                    DataAchizitiei = @DataAchizitie, Pret = @Pret
                WHERE IDExemplar = @ID";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@ID", exemplar.IDExemplar),
                new SqlParameter("@IDCarte", exemplar.IDCarte),
                new SqlParameter("@Cod", exemplar.CoduInventar ?? ""),
                new SqlParameter("@Stare", exemplar.StareExemplar ?? Constante.StareExemplar.BUNA),
                new SqlParameter("@DataAchizitie", exemplar.DataAchizitiei),
                new SqlParameter("@Pret", exemplar.Pret ?? (object)DBNull.Value)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Șterge un exemplar
        /// </summary>
        public static bool Delete(int id)
        {
            string query = "DELETE FROM Exemplare WHERE IDExemplar = @ID";
            SqlParameter[] parameters = new[] { new SqlParameter("@ID", id) };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Caută exemplare după cod inventar sau titlu carte
        /// </summary>
        public static List<Exemplar> Search(string searchTerm)
        {
            List<Exemplar> exemplare = new List<Exemplar>();
            string query = @"
                SELECT e.*, c.Titlu AS TitluCarte
                FROM Exemplare e
                JOIN Carti c ON e.IDCarte = c.IDCarte
                WHERE e.CoduInventar LIKE @Search OR c.Titlu LIKE @Search
                ORDER BY e.CoduInventar";

            SqlParameter[] parameters = new[] { new SqlParameter("@Search", "%" + searchTerm + "%") };
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                exemplare.Add(MapToExemplar(row));
            }
            return exemplare;
        }

        /// <summary>
        /// Obține exemplare disponibile pentru o carte
        /// </summary>
        public static List<Exemplar> GetAvailableForCarte(int carteId)
        {
            List<Exemplar> exemplare = new List<Exemplar>();
            string query = @"
                SELECT e.*, c.Titlu AS TitluCarte
                FROM Exemplare e
                JOIN Carti c ON e.IDCarte = c.IDCarte
                WHERE e.IDCarte = @IDCarte 
                  AND e.StareExemplar = @Stare
                  AND e.IDExemplar NOT IN (
                      SELECT IDExemplar FROM Imprumturi 
                      WHERE Status = @StatusActiv AND DataRestituire IS NULL
                  )
                ORDER BY e.CoduInventar";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@IDCarte", carteId),
                new SqlParameter("@Stare", Constante.StareExemplar.BUNA),
                new SqlParameter("@StatusActiv", Constante.StareImprumut.ACTIV)
            };
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                exemplare.Add(MapToExemplar(row));
            }
            return exemplare;
        }

        /// <summary>
        /// Mapează DataRow la obiect Exemplar
        /// </summary>
        private static Exemplar MapToExemplar(DataRow row)
        {
            return new Exemplar
            {
                IDExemplar = (int)row["IDExemplar"],
                IDCarte = (int)row["IDCarte"],
                CoduInventar = row["CoduInventar"].ToString(),
                StareExemplar = row["StareExemplar"].ToString(),
                DataAchizitiei = (DateTime)row["DataAchizitiei"],
                Pret = row["Pret"] == DBNull.Value ? null : (decimal?)row["Pret"],
                DataAdaugarii = (DateTime)row["DataAdaugarii"],
                TitluCarte = row["TitluCarte"].ToString()
            };
        }
    }
}
