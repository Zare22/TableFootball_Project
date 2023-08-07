using TableFootball.DataAccess.Interfaces;
using TableFootball.Models;

namespace TableFootball.DataAccess.Players
{
    public interface IPlayerRepository : ICreateable<Player>, IReadOnly<Player> { }
}
