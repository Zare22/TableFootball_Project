using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using TableFootball.DataAccess.Factories;
using TableFootball.Messages;
using TableFootball.Models;

namespace TableFootball.ViewModels
{
    public class LeagueViewModel : IViewModel
    {
        public ObservableCollection<League> Leagues { get; set; }

        public LeagueViewModel()
        {
            Leagues = new ObservableCollection<League>(RepositoryFactory.GetLeagueRepository().GetAll());
            Leagues.CollectionChanged += CollectionChanged;
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Task.Run(async () =>
                    {
                        try
                        {
                            int idLeague = await RepositoryFactory.GetLeagueRepository().CreateAsync(Leagues[e.NewStartingIndex]);
                            Leagues[e.NewStartingIndex].Teams.ToList().ForEach(t => AddTeamToLeague(t.IdTeam, idLeague));
                            Messenger.Default.Send(new CustomMessage(true));
                        }
                        catch (Exception)
                        {
                            Leagues.RemoveAt(e.NewStartingIndex);
                            Messenger.Default.Send(new CustomMessage(false, "An error occurred while creating the league!"));
                        }
                    });
                    break;
            }
        }

        private void AddTeamToLeague(int idTeam, int idLeague) => RepositoryFactory.GetTeamsLeaguesRepository().AddAsync(idTeam, idLeague);
    }
}
