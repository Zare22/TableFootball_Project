using System;
using TableFootball.DataAccess.Authentication;
using TableFootball.DataAccess.Games;
using TableFootball.DataAccess.GameSets;
using TableFootball.DataAccess.Interfaces;
using TableFootball.DataAccess.Leagues;
using TableFootball.DataAccess.Players;
using TableFootball.DataAccess.Teams;

namespace TableFootball.DataAccess.Factories
{
    public static class RepositoryFactory
    {
        private static readonly Lazy<IGameRepository> gameRepository = new Lazy<IGameRepository>(() => new GameRepository());
        private static readonly Lazy<IPlayerRepository> playerRepository = new Lazy<IPlayerRepository>(() => new PlayerRepository());
        private static readonly Lazy<ILeagueRepository> leagueRepository = new Lazy<ILeagueRepository>(() => new LeagueRepository());
        private static readonly Lazy<IGameSetRepository> gameSetRepository = new Lazy<IGameSetRepository>(() => new GameSetRepository());
        private static readonly Lazy<ITeamRepository> teamRepository = new Lazy<ITeamRepository>(() => new TeamRepository());

        private static readonly Lazy<IAddable> playerTeamsRepository = new Lazy<IAddable>(() => new TeamsPlayersRepository());
        private static readonly Lazy<IAddable> teamsLeaguesRepository = new Lazy<IAddable>(() => new LeaguesTeamsRepository());
        private static readonly Lazy<LeagueTableRepository> tablesRepository = new Lazy<LeagueTableRepository>(() => new LeagueTableRepository());
        private static readonly Lazy<AuthRepository> authRepository = new Lazy<AuthRepository>(() => new AuthRepository());

        public static IGameRepository GetGameRepository() => gameRepository.Value;
        public static IPlayerRepository GetPlayerRepository() => playerRepository.Value;
        public static ILeagueRepository GetLeagueRepository() => leagueRepository.Value;
        public static IGameSetRepository GetGameSetRepository() => gameSetRepository.Value;
        public static ITeamRepository GetTeamRepository() => teamRepository.Value;
        public static IAddable GetPlayerTeamsRepository() => playerTeamsRepository.Value;
        public static IAddable GetTeamsLeaguesRepository() => teamsLeaguesRepository.Value;
        public static AuthRepository GetAuthRepository() => authRepository.Value;

        public static LeagueTableRepository GetLeagueTablesRepository() => tablesRepository.Value;



       
    }
}
