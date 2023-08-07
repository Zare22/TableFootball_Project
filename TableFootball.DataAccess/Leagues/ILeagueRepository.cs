using TableFootball.DataAccess.Interfaces;
using TableFootball.Models;

namespace TableFootball.DataAccess.Leagues
{
    public interface ILeagueRepository : ICreateable<League>, IReadOnly<League> { }
}
