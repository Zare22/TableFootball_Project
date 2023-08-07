using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TableFootball.Models;

namespace TableFootball.DataAccess.Teams
{
    internal class TeamRepository : ITeamRepository
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public async Task<int> CreateAsync(Team team)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "sp_CreateTeam";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(nameof(Team.Name), team.Name);
                    command.Parameters.AddWithValue(nameof(Team.CreatedAt), team.CreatedAt);

                    //Register output
                    SqlParameter id =
                        new SqlParameter(nameof(Team.IdTeam), SqlDbType.Int)
                        { Direction = ParameterDirection.Output };

                    command.Parameters.Add(id);
                    await command.ExecuteNonQueryAsync();
                    return (int)id.Value;
                }
            }
        }

        public ICollection<Team> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var teamsDictionary = new Dictionary<int, Team>();

                var teams = connection.Query<Team, Player, Team>(
                    "sp_GetAllTeamsWithPlayers",
                    (team, player) =>
                    {
                        if (!teamsDictionary.TryGetValue(team.IdTeam, out Team existingTeam))
                        {
                            existingTeam = team;
                            existingTeam.Players = new List<Player>();
                            teamsDictionary.Add(existingTeam.IdTeam, existingTeam);
                        }

                        if (player != null)
                            existingTeam.Players.Add(player);

                        return existingTeam;
                    },
                    splitOn: "IdPlayer",
                    commandType: CommandType.StoredProcedure
                );

                return teamsDictionary.Values.ToList();
            }
        }


        public async void UpdateAsync(Team team)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "sp_UpdateTeam";
                    command.CommandType = CommandType.StoredProcedure;

                    var playerOne = team.Players.ElementAt(0);
                    var playerTwo = team.Players.ElementAt(1);
                    command.Parameters.AddWithValue(nameof(Team.IdTeam), team.IdTeam);
                    command.Parameters.AddWithValue(nameof(Team.Name), team.Name);
                    command.Parameters.AddWithValue(nameof(Team.CreatedAt), team.CreatedAt);
                    command.Parameters.AddWithValue("PlayerOneId", playerOne.IdPlayer);
                    command.Parameters.AddWithValue("PlayerTwoId", playerTwo.IdPlayer);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
