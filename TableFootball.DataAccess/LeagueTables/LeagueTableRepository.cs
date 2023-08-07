using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using TableFootball.Models;
using Dapper;

namespace TableFootball.DataAccess.Leagues
{
    public class LeagueTableRepository
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        public ICollection<LeagueTableEntry> GetAll(League league)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var leagueTableDictionary = new Dictionary<int, LeagueTableEntry>();
                var result = connection.Query<LeagueTableEntry>(
                    "sp_GetLeagueTable",
                    new { LeagueId = league.IdLeague },
                    commandType: CommandType.StoredProcedure
                );

                return result.ToList();
            }
        }
    }
}
