using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Windows;
using TableFootball.DataAccess.Factories;
using TableFootball.Frames;
using TableFootball.Messages;
using TableFootball.Models;
using TableFootball.ViewModels;

namespace TableFootball.Pages
{
    public partial class CreateTeamPage : FramedPage
    {
        private readonly Team _team;
        public CreateTeamPage(TeamViewModel teamViewModel, Team team = null) : base(teamViewModel)
        {
            InitializeComponent();
            _team = team ?? new Team() { Players = new List<Player>() };
            DataContext = _team;
            FillDdlsWithPlayers();
            Messenger.Default.Register<CustomMessage>(this, HandleTeamCreatedResult);
        }

        private void FillDdlsWithPlayers()
        {
            ICollection<Player> players = RepositoryFactory.GetPlayerRepository().GetAll();
            ddlPlayerOne.ItemsSource = players;
            ddlPlayerTwo.ItemsSource = players;
        }

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Team team)
            {
                if (FormIsValid(team))
                {
                    if (ViewModel is TeamViewModel teamViewModel)
                    {
                        if (_team.IdTeam == 0)
                        {
                            team.Players.Add(ddlPlayerOne.SelectedItem as Player);
                            team.Players.Add(ddlPlayerTwo.SelectedItem as Player);
                            teamViewModel.Teams.Add(team);
                        }
                        else
                        {
                            teamViewModel.UpdateTeam(team);
                        }
                    }
                }
                else
                    MessageBox.Show("Your form isn't valid, check it and try again", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool FormIsValid(Team team)
        {
            if (string.IsNullOrEmpty(team.Name) || ddlPlayerOne.SelectedItem == null || ddlPlayerTwo.SelectedItem == null)
            {
                return false;
            }

            if (ddlPlayerOne.SelectedItem is Player playerOne && ddlPlayerTwo.SelectedItem is Player playerTwo)
            {
                return !playerOne.Equals(playerTwo);
            }

            return false;
        }


        private void HandleTeamCreatedResult(CustomMessage message)
        {
            if (message.IsSuccess)
            {
                MessageBox.Show(message.Message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    DataContext = null;
                    DataContext = new Team() { Players = new List<Player>() };
                    ddlPlayerOne.SelectedItem = null;
                    ddlPlayerTwo.SelectedItem = null;
                });
            }
            else
                MessageBox.Show(message.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();
    }
}
