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
    public class TeamViewModel : IViewModel
    {
        public ObservableCollection<Team> Teams { get; set; }

        public TeamViewModel()
        {
            Teams = new ObservableCollection<Team>(RepositoryFactory.GetTeamRepository().GetAll());
            Teams.CollectionChanged += CollectionChanged;
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
                            int idTeam = await RepositoryFactory.GetTeamRepository().CreateAsync(Teams[e.NewStartingIndex]);
                            Teams[e.NewStartingIndex].Players.ToList().ForEach(p => AddPlayerToTeam(p.IdPlayer, idTeam));
                            Messenger.Default.Send(new CustomMessage(true ,"Successfully created a team!"));
                        }
                        catch (Exception)
                        {
                            Teams.RemoveAt(e.NewStartingIndex);
                            Messenger.Default.Send(new CustomMessage(false, "An error occurred while creating the team!"));
                        }
                    });
                    break;
                case NotifyCollectionChangedAction.Replace:
                    try
                    {
                        RepositoryFactory.GetTeamRepository().UpdateAsync(e.NewItems.OfType<Team>().ToList()[0]);
                        Messenger.Default.Send(new CustomMessage(true, "Successfully updated the team!"));
                    }
                    catch (Exception)
                    {
                        Messenger.Default.Send(new CustomMessage(false, "An error occurred while updating the team"));
                    }
                    break;
            }
        }

        private void AddPlayerToTeam(int idPlayer, int idTeam) => RepositoryFactory.GetPlayerTeamsRepository().AddAsync(idTeam, idPlayer);

        public void UpdateTeam(Team team) => Teams[Teams.IndexOf(team as Team)] = team as Team;
    }
}
