using System.Configuration;
using System.Data.SqlClient;
using TableFootball.DataAccess.Interfaces;

namespace TableFootball.DataAccess.Leagues
{
    internal class LeaguesTeamsRepository : IAddable
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public async void AddAsync(int teamId, int leagueId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "sp_AddTeamToLeague";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("TeamId" , teamId);
                    command.Parameters.AddWithValue("LeagueId" , leagueId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
