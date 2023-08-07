using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TableFootball.Models;

namespace TableFootball.DataAccess.Leagues
{
    internal class LeagueRepository : ILeagueRepository
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public async Task<int> CreateAsync(League league)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "sp_CreateLeague";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(nameof(League.Name), league.Name);

                    SqlParameter id =
                        new SqlParameter(nameof(League.IdLeague), System.Data.SqlDbType.Int)
                        { Direction = System.Data.ParameterDirection.Output };

                    command.Parameters.Add(id);
                    await command.ExecuteNonQueryAsync();
                    return (int)id.Value;
                }
            }
        }

        public ICollection<League> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var leagueDictionary = new Dictionary<int, League>();
                var result = connection.Query<League, Team, League>(
                    "sp_GetLeaguesAndTeams",
                    (league, team) =>
                    {
                        if (!leagueDictionary.TryGetValue(league.IdLeague, out League leagueEntry))
                        {
                            leagueEntry = league;
                            leagueEntry.Teams = new List<Team>();
                            leagueDictionary.Add(leagueEntry.IdLeague, leagueEntry);
                        }

                        if (team != null)
                        {
                            leagueEntry.Teams.Add(team);
                        }

                        return leagueEntry;
                    },
                    splitOn: "IdTeam",
                    commandType: System.Data.CommandType.StoredProcedure
                );

                return result.Distinct().ToList();
            }
        }
    }
}
