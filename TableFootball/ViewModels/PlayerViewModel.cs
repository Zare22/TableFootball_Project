using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using TableFootball.DataAccess.Factories;
using TableFootball.Messages;
using TableFootball.Models;

namespace TableFootball.ViewModels
{
    public class PlayerViewModel : IViewModel
    {
        public ObservableCollection<Player> Players { get; set; }
        public PlayerViewModel()
        {
            Players = new ObservableCollection<Player>(RepositoryFactory.GetPlayerRepository().GetAll());
            Players.CollectionChanged += CollectionChanged;
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
                            await RepositoryFactory.GetPlayerRepository().CreateAsync(Players[e.NewStartingIndex]);
                            Messenger.Default.Send(new CustomMessage(true));
                        }
                        catch (Exception)
                        {
                            Players.RemoveAt(e.NewStartingIndex);
                            Messenger.Default.Send(new CustomMessage(false, "An error occurred while creating the player!"));
                        }
                    });
                    break;
            }
        }
    }
}
