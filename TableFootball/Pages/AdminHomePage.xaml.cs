using System.Windows;
using System.Windows.Controls;
using TableFootball.Frames;
using TableFootball.Models;
using TableFootball.ViewModels;

namespace TableFootball.Pages
{
    public partial class AdminHomePage : FramedPage
    {
        private TeamViewModel _teamViewModel;
        private LeagueViewModel _leagueViewModel;
        public AdminHomePage(TeamViewModel teamViewModel, LeagueViewModel leagueViewModel) : base(teamViewModel)
        {
            InitializeComponent();
            _teamViewModel = teamViewModel;
            _leagueViewModel = leagueViewModel;
            ddlLeagues.DataContext = _leagueViewModel;
            ddlTeams.DataContext = _teamViewModel;
        }

        private void BtnCreatePlayer_Click(object sender, RoutedEventArgs e)
            => Frame.Navigate(new CreatePlayerPage(new PlayerViewModel()) { Frame = Frame });

        private void BtnCreateTeam_Click(object sender, RoutedEventArgs e)
            => Frame.Navigate(new CreateTeamPage(_teamViewModel) { Frame = Frame });

        private void BtnCreateLeague_Click(object sender, RoutedEventArgs e)
            => Frame.Navigate(new CreateLeaguePage(_leagueViewModel) { Frame = Frame });

        private void DdlLeagues_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedLeague = ddlLeagues.SelectedItem as League;
            if (selectedLeague != null)
            {
                ddlHomeTeam.ItemsSource = selectedLeague.Teams;
                ddlAwayTeam.ItemsSource = selectedLeague.Teams;
            }
        }

        private void BtnModifyTeam_Click(object sender, RoutedEventArgs e)
        {
            var selectedTeam = ddlTeams.SelectedItem as Team;
            if (selectedTeam != null)
            {
                Frame.Navigate(new CreateTeamPage(new TeamViewModel(), selectedTeam) { Frame = Frame });
            }
        }

        private void BtnCreateGame_Click(object sender, RoutedEventArgs e)
        {
            var selectedLeague = ddlLeagues.SelectedItem as League;
            var homeTeam = ddlHomeTeam.SelectedItem as Team;
            var awayTeam = ddlAwayTeam.SelectedItem as Team;

            if (selectedLeague != null && homeTeam != null && awayTeam != null && !homeTeam.Equals(awayTeam))
            {
                Frame.Navigate(new GamePage(new GameViewModel(), selectedLeague, homeTeam, awayTeam) { Frame = Frame });
            }
            else
                MessageBox.Show("Please check if you have selected league and 2 different teams", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();
    }
}
