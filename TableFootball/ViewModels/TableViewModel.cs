using System.Collections.ObjectModel;
using TableFootball.DataAccess.Factories;
using TableFootball.Models;

namespace TableFootball.ViewModels
{
    public class TableViewModel : IViewModel
    {
        public ObservableCollection<LeagueTableEntry> LeagueTable { get; set; }
        public League League { get; set; }

        public TableViewModel(League league)
        {
            League = league;
            LeagueTable = new ObservableCollection<LeagueTableEntry>(RepositoryFactory.GetLeagueTablesRepository().GetAll(league));
        }
    }
}
