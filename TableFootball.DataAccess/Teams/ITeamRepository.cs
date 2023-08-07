using TableFootball.DataAccess.Interfaces;
using TableFootball.Models;

namespace TableFootball.DataAccess.Teams
{
    public interface ITeamRepository : ICreateable<Team>, IReadOnly<Team>, IUpdateable<Team> { }
}
