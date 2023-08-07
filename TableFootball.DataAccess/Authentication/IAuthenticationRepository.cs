using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableFootball.DataAccess.Authentication
{
    public interface IAuthenticationRepository
    {
        bool VerifyAdminLogin(string username, string password);
    }
}
