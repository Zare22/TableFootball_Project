using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TableFootball.Models;

namespace TableFootball.DataAccess.Games
{
    internal class GameRepository : IGameRepository
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public async Task<int> CreateAsync(Game game)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "sp_CreateGame";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("HomeTeamId", game.HomeTeam.IdTeam);
                    command.Parameters.AddWithValue("AwayTeamId", game.AwayTeam.IdTeam);
                    command.Parameters.AddWithValue("LeagueId", game.League.IdLeague);
                    command.Parameters.AddWithValue("WinnerId", game.Winner.IdTeam);

                    SqlParameter id =
                        new SqlParameter(nameof(Game.IdGame), System.Data.SqlDbType.Int)
                        { Direction = System.Data.ParameterDirection.Output };

                    command.Parameters.Add(id);
                    await command.ExecuteNonQueryAsync();
                    return (int)id.Value;
                }
            }
        }
    }
}
