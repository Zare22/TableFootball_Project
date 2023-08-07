using System.Windows;
using System.Windows.Controls;
using TableFootball.DataAccess.Factories;
using TableFootball.Frames;
using TableFootball.Modals;
using TableFootball.Models;
using TableFootball.ViewModels;

namespace TableFootball.Pages
{
    public partial class HomePage : FramedPage
    {
        public HomePage(LeagueViewModel leagueViewModel) : base(leagueViewModel)
        {
            InitializeComponent();
            listBoxLeagues.ItemsSource = leagueViewModel.Leagues;
        }

        private void BtnOpenLeague_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (listBoxLeagues.SelectedItem != null)
            {
                Frame.Navigate(new LeagueTablePage(new TableViewModel(listBoxLeagues.SelectedItem as League)) { Frame = Frame });
            }
        }

        private void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AuthModal modalWindow = new AuthModal();
            modalWindow.ShowDialog();


            string username = modalWindow.Username;
            string password = modalWindow.Password;

            bool isAdmin = RepositoryFactory.GetAuthRepository().VerifyAdminLogin(username, password);

            if (isAdmin)
            {
                Frame.Navigate(new AdminHomePage(new TeamViewModel(), new LeagueViewModel()) { Frame = Frame });
                if (NavigationService.CanGoBack)
                {
                    NavigationService.RemoveBackEntry();
                }
            }
            else
                MessageBox.Show("Wrong credentials", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
