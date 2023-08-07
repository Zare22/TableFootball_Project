using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Controls;
using TableFootball.Frames;
using TableFootball.Messages;
using TableFootball.Models;
using TableFootball.ViewModels;

namespace TableFootball.Pages
{
    public partial class GamePage : FramedPage
    {
        private GameViewModel _gameViewModel;

        public GamePage(GameViewModel gameViewModel, League league, Team homeTeam, Team awayTeam) : base(gameViewModel)
        {
            InitializeComponent();
            Messenger.Default.Register<CustomMessage>(this, HandleValidationMessaging);
            Messenger.Default.Register<GameSavedMessage>(this, HandleGameMessaging);
            _gameViewModel = gameViewModel;

            DataContext = _gameViewModel;

            _gameViewModel.Game.League = league;
            _gameViewModel.Game.HomeTeam = homeTeam;
            _gameViewModel.Game.AwayTeam = awayTeam;
        }

        private void HandleGameMessaging(GameSavedMessage message)
        {
            if (message.IsSuccess)
            {
                MessageBox.Show(message.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show(message.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void HandleValidationMessaging(CustomMessage message)
        {
            if (message.IsSuccess)
            {
                MessageBox.Show(message.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show(message.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnAddSet_Click(object sender, RoutedEventArgs e) => _gameViewModel.AddGameSet();

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (_gameViewModel.Game.Winner != null)
            {
                _gameViewModel.SaveTheGame();
            }
            else
                MessageBox.Show("There is no winner yet", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

        }

        private void btnEditPreviousSet_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button editButton && editButton.DataContext is GameSet gameSet)
            {
                _gameViewModel.SelectedGameSet = gameSet;

                _gameViewModel.NewGameSetHomeGoals = gameSet.HomeTeamGoals;
                _gameViewModel.NewGameSetAwayGoals = gameSet.AwayTeamGoals;
            }
        }
    }
}
