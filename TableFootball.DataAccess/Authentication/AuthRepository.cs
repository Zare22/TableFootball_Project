using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace TableFootball.DataAccess.Authentication
{
    public class AuthRepository : IAuthenticationRepository
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        public bool VerifyAdminLogin(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_VerifyAdminLogin", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Username", username);
                    command.Parameters.AddWithValue("Password", password);

                    int result = (int)command.ExecuteScalar();
                    return result == 1;
                }
            }
        }
    }
}
