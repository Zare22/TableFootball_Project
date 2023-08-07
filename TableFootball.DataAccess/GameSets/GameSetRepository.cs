using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TableFootball.Models;

namespace TableFootball.DataAccess.GameSets
{
    internal class GameSetRepository : IGameSetRepository
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public async Task<int> CreateAsync(GameSet gameSet)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "sp_CreateGameSet";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(nameof(GameSet.HomeTeamGoals), gameSet.HomeTeamGoals);
                    command.Parameters.AddWithValue(nameof(GameSet.AwayTeamGoals), gameSet.AwayTeamGoals);
                    command.Parameters.AddWithValue(nameof(GameSet.Game.IdGame), gameSet.Game.IdGame);

                    SqlParameter id =
                        new SqlParameter(nameof(GameSet.IdGameSet), System.Data.SqlDbType.Int)
                        { Direction = System.Data.ParameterDirection.Output };

                    command.Parameters.Add(id);
                    await command.ExecuteNonQueryAsync();
                    return (int)id.Value;
                }
            }
        }
    }
}
