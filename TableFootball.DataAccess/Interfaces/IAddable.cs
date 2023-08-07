namespace TableFootball.DataAccess.Interfaces
{
    public interface IAddable
    {
        void AddAsync(int teamId, int playerOrLeagueId);
    }
}
