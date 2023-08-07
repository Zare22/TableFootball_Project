using System.Configuration;
using System.Data.SqlClient;
using TableFootball.DataAccess.Interfaces;

namespace TableFootball.DataAccess.Teams
{
    internal class TeamsPlayersRepository : IAddable
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public async void AddAsync(int teamId, int playerId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "sp_AddPlayerToTeam";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("PlayerId", playerId);
                    command.Parameters.AddWithValue("TeamId", teamId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
