using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using TableFootball.Frames;
using TableFootball.Messages;
using TableFootball.Models;
using TableFootball.ViewModels;

namespace TableFootball.Pages
{
    public partial class CreatePlayerPage : FramedPage
    {
        public CreatePlayerPage(PlayerViewModel playerViewModel) : base(playerViewModel)
        {
            InitializeComponent();
            DataContext = new Player();
            Messenger.Default.Register<CustomMessage>(this, HandlePlayerCreatedResultMessage);
        }

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Player player)
            {
                if (FormIsValid(player))
                {
                    if (ViewModel is PlayerViewModel playerViewModel)
                    {
                        playerViewModel.Players.Add(player);
                    }
                }
                else
                    MessageBox.Show("Your form isn't valid, check it and try again", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool FormIsValid(Player player)
        {
            const string emailRegexPattern = @"^\S+@\S+\.\S+$";
            return !string.IsNullOrEmpty(player.FirstName) &&
                   !string.IsNullOrEmpty(player.LastName) &&
                   System.Text.RegularExpressions.Regex.IsMatch(player.Email, emailRegexPattern);
        }

        private void HandlePlayerCreatedResultMessage(CustomMessage message)
        {
            if (message.IsSuccess)
            {
                MessageBox.Show("You have added your player successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    DataContext = null;
                    DataContext = new Player();
                });
            }
            else
                MessageBox.Show(message.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();
    }
}
