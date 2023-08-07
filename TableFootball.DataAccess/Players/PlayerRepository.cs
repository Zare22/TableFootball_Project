using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TableFootball.Models;

namespace TableFootball.DataAccess.Players
{
    internal class PlayerRepository : IPlayerRepository
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public async Task<int> CreateAsync(Player player)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "sp_CreatePlayer";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(nameof(Player.FirstName), player.FirstName);
                    command.Parameters.AddWithValue(nameof(Player.LastName), player.LastName);
                    command.Parameters.AddWithValue(nameof(Player.Email), player.Email);
                    command.Parameters.AddWithValue(nameof(Player.CreatedAt), player.CreatedAt);

                    //Register output
                    SqlParameter id =
                        new SqlParameter(nameof(Player.IdPlayer), System.Data.SqlDbType.Int)
                        { Direction = System.Data.ParameterDirection.Output };

                    command.Parameters.Add(id);
                    await command.ExecuteNonQueryAsync();
                    return (int)id.Value;
                }
            }
        }

        public ICollection<Player> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var players = connection.Query<Player>("sp_GetAllPlayers", commandType: CommandType.StoredProcedure);
                return players.ToList();
            }
        }
    }
}
