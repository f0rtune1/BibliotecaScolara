using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BibliotecaScolara.Models;
using BibliotecaScolara.Database;
using BibliotecaScolara.Utilities;

namespace BibliotecaScolara.Managers
{
    public class ImprumutManager
    {
        /// <summary>
        /// Obține toate împrumuturile cu detalii
        /// </summary>
        public static List<Imprumut> GetAll()
        {
            List<Imprumut> imprumturi = new List<Imprumut>();
            string query = @"
                SELECT i.*, e.Nume AS NumeElev, e.Prenume AS PrenumeElev,
                       c.Titlu AS TitluCarte, ex.CoduInventar,
                       DATEDIFF(DAY, GETDATE(), i.DataScadenta) AS ZileRamase
                FROM Imprumturi i
                JOIN Elevi e ON i.IDElev = e.IDElev
                JOIN Exemplare ex ON i.IDExemplar = ex.IDExemplar
                JOIN Carti c ON ex.IDCarte = c.IDCarte
                ORDER BY i.DataImprumut DESC";
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                imprumturi.Add(MapToImprumut(row));
            }
            return imprumturi;
        }

        /// <summary>
        /// Obține un împrumut după ID
        /// </summary>
        public static Imprumut GetByID(int id)
        {
            string query = @"
                SELECT i.*, e.Nume AS NumeElev, e.Prenume AS PrenumeElev,
                       c.Titlu AS TitluCarte, ex.CoduInventar,
                       DATEDIFF(DAY, GETDATE(), i.DataScadenta) AS ZileRamase
                FROM Imprumturi i
                JOIN Elevi e ON i.IDElev = e.IDElev
                JOIN Exemplare ex ON i.IDExemplar = ex.IDExemplar
                JOIN Carti c ON ex.IDCarte = c.IDCarte
                WHERE i.IDImprumut = @ID";
            
            SqlParameter[] parameters = new[] { new SqlParameter("@ID", id) };
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            
            if (dt.Rows.Count > 0)
            {
                return MapToImprumut(dt.Rows[0]);
            }
            return null;
        }

        /// <summary>
        /// Crează un nou împrumut
        /// </summary>
        public static bool Insert(Imprumut imprumut)
        {
            // Verifică dacă exemplarul este disponibil
            if (!IsExemplarAvailable(imprumut.IDExemplar))
            {
                Mesaje.Eroare(Constante.Erori.EXEMPLAR_INDISPONIBIL, "Exemplar Indisponibil");
                return false;
            }

            DateTime datascadenta = imprumut.DataImprumut.AddDays(Constante.ZILE_IMPRUMUT_DEFAULT);
            
            string query = @"
                INSERT INTO Imprumturi (IDElev, IDExemplar, DataImprumut, DataScadenta, Status)
                VALUES (@IDElev, @IDExemplar, @DataImprumut, @DataScadenta, @Status)";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@IDElev", imprumut.IDElev),
                new SqlParameter("@IDExemplar", imprumut.IDExemplar),
                new SqlParameter("@DataImprumut", imprumut.DataImprumut),
                new SqlParameter("@DataScadenta", datascadenta),
                new SqlParameter("@Status", Constante.StareImprumut.ACTIV)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Returnează o carte (marcheaza împrumutul ca încheiat)
        /// </summary>
        public static bool ReturnBook(int imprumutId, string observatii = "")
        {
            string query = @"
                UPDATE Imprumturi 
                SET DataRestituire = GETDATE(), Status = @Status, Observatii = @Observatii
                WHERE IDImprumut = @ID";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@ID", imprumutId),
                new SqlParameter("@Status", Constante.StareImprumut.INCHEIAT),
                new SqlParameter("@Observatii", observatii ?? "")
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Prelungește termenul unui împrumut
        /// </summary>
        public static bool ExtendLoan(int imprumutId)
        {
            string query = @"
                UPDATE Imprumturi 
                SET DataScadenta = DATEADD(DAY, @Zile, DataScadenta)
                WHERE IDImprumut = @ID AND DataRestituire IS NULL";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@ID", imprumutId),
                new SqlParameter("@Zile", Constante.ZILE_IMPRUMUT_DEFAULT)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Obține împrumuturile active ale unui elev
        /// </summary>
        public static List<Imprumut> GetActiveByElev(int elevId)
        {
            List<Imprumut> imprumturi = new List<Imprumut>();
            string query = @"
                SELECT i.*, e.Nume AS NumeElev, e.Prenume AS PrenumeElev,
                       c.Titlu AS TitluCarte, ex.CoduInventar,
                       DATEDIFF(DAY, GETDATE(), i.DataScadenta) AS ZileRamase
                FROM Imprumturi i
                JOIN Elevi e ON i.IDElev = e.IDElev
                JOIN Exemplare ex ON i.IDExemplar = ex.IDExemplar
                JOIN Carti c ON ex.IDCarte = c.IDCarte
                WHERE i.IDElev = @IDElev AND i.DataRestituire IS NULL
                ORDER BY i.DataScadenta";

            SqlParameter[] parameters = new[] { new SqlParameter("@IDElev", elevId) };
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                imprumturi.Add(MapToImprumut(row));
            }
            return imprumturi;
        }

        /// <summary>
        /// Obține împrumuturile întârziate
        /// </summary>
        public static List<Imprumut> GetOverdueLoans()
        {
            List<Imprumut> imprumturi = new List<Imprumut>();
            string query = @"
                SELECT i.*, e.Nume AS NumeElev, e.Prenume AS PrenumeElev,
                       c.Titlu AS TitluCarte, ex.CoduInventar,
                       DATEDIFF(DAY, GETDATE(), i.DataScadenta) AS ZileRamase
                FROM Imprumturi i
                JOIN Elevi e ON i.IDElev = e.IDElev
                JOIN Exemplare ex ON i.IDExemplar = ex.IDExemplar
                JOIN Carti c ON ex.IDCarte = c.IDCarte
                WHERE i.DataRestituire IS NULL AND i.DataScadenta < GETDATE()
                ORDER BY i.DataScadenta";
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                imprumturi.Add(MapToImprumut(row));
            }
            return imprumturi;
        }

        /// <summary>
        /// Caută împrumuturi după nume elev sau titlu carte
        /// </summary>
        public static List<Imprumut> Search(string searchTerm)
        {
            List<Imprumut> imprumturi = new List<Imprumut>();
            string query = @"
                SELECT i.*, e.Nume AS NumeElev, e.Prenume AS PrenumeElev,
                       c.Titlu AS TitluCarte, ex.CoduInventar,
                       DATEDIFF(DAY, GETDATE(), i.DataScadenta) AS ZileRamase
                FROM Imprumturi i
                JOIN Elevi e ON i.IDElev = e.IDElev
                JOIN Exemplare ex ON i.IDExemplar = ex.IDExemplar
                JOIN Carti c ON ex.IDCarte = c.IDCarte
                WHERE e.Nume LIKE @Search OR e.Prenume LIKE @Search 
                   OR c.Titlu LIKE @Search OR ex.CoduInventar LIKE @Search
                ORDER BY i.DataImprumut DESC";

            SqlParameter[] parameters = new[] { new SqlParameter("@Search", "%" + searchTerm + "%") };
            
            DataTable dt = DatabaseConnection.ExecuteDataTable(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                imprumturi.Add(MapToImprumut(row));
            }
            return imprumturi;
        }

        /// <summary>
        /// Verifică dacă un exemplar este disponibil pentru împrumut
        /// </summary>
        private static bool IsExemplarAvailable(int exemplarId)
        {
            string query = @"
                SELECT COUNT(*) FROM Imprumturi 
                WHERE IDExemplar = @ID AND DataRestituire IS NULL";
            
            SqlParameter[] parameters = new[] { new SqlParameter("@ID", exemplarId) };
            
            object result = DatabaseConnection.ExecuteScalar(query, parameters);
            return result != null && int.TryParse(result.ToString(), out int count) && count == 0;
        }

        /// <summary>
        /// Obține statistici împrumuturi
        /// </summary>
        public static DataTable GetStatistics()
        {
            string query = @"
                SELECT 
                    COUNT(*) AS TotalImprumturi,
                    SUM(CASE WHEN DataRestituire IS NULL THEN 1 ELSE 0 END) AS ImprumturiActive,
                    SUM(CASE WHEN DataRestituire IS NULL AND DataScadenta < GETDATE() THEN 1 ELSE 0 END) AS ImprumturiIntarziate,
                    SUM(CASE WHEN DataRestituire IS NOT NULL THEN 1 ELSE 0 END) AS ImprumturiIncheiata
                FROM Imprumturi";
            
            return DatabaseConnection.ExecuteDataTable(query);
        }

        /// <summary>
        /// Mapează DataRow la obiect Imprumut
        /// </summary>
        private static Imprumut MapToImprumut(DataRow row)
        {
            return new Imprumut
            {
                IDImprumut = (int)row["IDImprumut"],
                IDElev = (int)row["IDElev"],
                IDExemplar = (int)row["IDExemplar"],
                DataImprumut = (DateTime)row["DataImprumut"],
                DataScadenta = (DateTime)row["DataScadenta"],
                DataRestituire = row["DataRestituire"] == DBNull.Value ? null : (DateTime?)row["DataRestituire"],
                Status = row["Status"].ToString(),
                Observatii = row["Observatii"].ToString(),
                DataAdaugarii = (DateTime)row["DataAdaugarii"],
                NumeElev = row["NumeElev"].ToString() + " " + row["PrenumeElev"].ToString(),
                TitluCarte = row["TitluCarte"].ToString(),
                CoduInventar = row["CoduInventar"].ToString(),
                ZileRamase = row["ZileRamase"] == DBNull.Value ? 0 : (int)row["ZileRamase"]
            };
        }
    }
}
