using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TableFootball.DataAccess.Factories;
using TableFootball.Frames;
using TableFootball.Messages;
using TableFootball.Models;
using TableFootball.ViewModels;

namespace TableFootball.Pages
{
    public partial class CreateLeaguePage : FramedPage
    {
        public CreateLeaguePage(LeagueViewModel leagueViewModel) : base(leagueViewModel)
        {
            InitializeComponent();
            DataContext = new League() { Teams = new List<Team>() };
            FillListBox();
            Messenger.Default.Register<CustomMessage>(this, HandleLeagueCreatedResult);
        }

        private void FillListBox()
        {
            listBoxTeamsInLeague.Items.Clear();
            listBoxTeamsNotInLeague.Items.Clear();
            var teams = RepositoryFactory.GetTeamRepository().GetAll();
            teams.ToList().ForEach(t => listBoxTeamsNotInLeague.Items.Add(t));
        }

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is League league)
            {
                if (!string.IsNullOrEmpty(league.Name))
                {
                    if (ViewModel is LeagueViewModel leagueViewModel)
                    {
                        leagueViewModel.Leagues.Add(league);
                    }
                }
                else
                    MessageBox.Show("Your form isn't valid, check it and try again", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void HandleLeagueCreatedResult(CustomMessage message)
        {
            if (message.IsSuccess)
            {
                MessageBox.Show("You have added your league successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    DataContext = new Team() { Players = new List<Player>() };
                    FillListBox();
                });
            }
            else
                MessageBox.Show(message.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnRemoveFromLeague_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxTeamsInLeague.SelectedItem is Team team)
            {
                listBoxTeamsInLeague.Items.Remove(team);
                listBoxTeamsNotInLeague.Items.Add(team);
                (DataContext as League)?.Teams.Remove(team);
            }
        }

        private void BtnMoveToLeague_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxTeamsNotInLeague.SelectedItem is Team team)
            {
                listBoxTeamsNotInLeague.Items.Remove(team);
                listBoxTeamsInLeague.Items.Add(team);
                (DataContext as League)?.Teams.Add(team);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new HomePage(new LeagueViewModel()) { Frame = Frame });
        }
    }
}
