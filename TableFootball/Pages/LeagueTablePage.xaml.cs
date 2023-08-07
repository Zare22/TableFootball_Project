using System.Windows;
using TableFootball.Frames;
using TableFootball.ViewModels;

namespace TableFootball.Pages
{
    public partial class LeagueTablePage : FramedPage
    {
        public LeagueTablePage(TableViewModel tableViewModel) : base(tableViewModel)
        {
            InitializeComponent();
            DataContext = tableViewModel;
            dataGridLeagueTable.ItemsSource = tableViewModel.LeagueTable;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();
    }
}
