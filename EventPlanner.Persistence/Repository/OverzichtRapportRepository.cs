using Dapper;
using EventPlanner.Storage.Models;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace EventPlanner.Storage.Repository
{
    public class OverzichtRapportRepository : IOverzichtRapportRepository
    {
        private readonly IConfiguration _configuration;

        public OverzichtRapportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<OverzichtRapport>> GetOverzichtRapportenAsync()
        {
            var connectionString = _configuration.GetConnectionString("LocalMySQL");

            string query = @"SELECT
                            evenement.Id AS EvenementId,
                            evenement.Naam AS EvenementNaam,
                            locatie.Id AS LocatieId,
                            locatie.Naam AS LocatieNaam,

                            (SELECT COUNT(Id) 
                             FROM EventPlanner.Taken 
                                WHERE EvenementId = evenement.Id 
                                AND Belangrijkheid = 1
                                AND Status = 1) AS AantalMustTakenInTodo,
                             (
                             SELECT 
                              CASE 
                               WHEN COUNT(*) = 0 THEN 0
                               ELSE ROUND(SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) * 100.0 / COUNT(*), 2)
                              END
                             FROM EventPlanner.Taken 
                             WHERE EvenementId = evenement.Id
                            ) AS PercentageVoltooideTaken,
                            
                                -- Laatst gewijzigde datum van evenement of zijn taken
                            (
                                SELECT MAX(t.LaatstGewijzigd)
                                FROM EventPlanner.Taken t
                                WHERE t.EvenementId = evenement.Id
                            ) AS LaatsteTaakWijziging,

                            -- De laatstgewijzigde datum: ofwel van het evenement zelf, of van de taken (de recentste van beide)
                            GREATEST(
                                evenement.LaatstGewijzigd,
                                COALESCE(
                                    (
                                        SELECT MAX(t.LaatstGewijzigd)
                                        FROM EventPlanner.Taken t
                                        WHERE t.EvenementId = evenement.Id
                                    ),
                                    evenement.LaatstGewijzigd  -- Fallback als er geen taken zijn
                                )
                            ) AS LaatsteUpdateEvenementOfTaken

                                                    FROM EventPlanner.Evenementen evenement
                                                    INNER JOIN EventPlanner.Locaties locatie ON locatie.Id = evenement.LocatieId";
           
            using var connection = new MySqlConnection(connectionString);
            var result = await connection.QueryAsync<OverzichtRapport>(query);
            return result.ToList();
        }
    }
}
