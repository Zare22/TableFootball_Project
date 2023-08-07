using System.ComponentModel;

namespace TableFootball.Models
{
    public class GameSet : INotifyPropertyChanged
    {
        public int IdGameSet { get; set; }

        private int _homeTeamGoals;
        public int HomeTeamGoals
        {
            get => _homeTeamGoals;
            set
            {
                if (_homeTeamGoals != value)
                {
                    _homeTeamGoals = value;
                    RaisePropertyChanged(nameof(HomeTeamGoals));
                }
            }
        }

        private int _awayTeamGoals;
        public int AwayTeamGoals
        {
            get => _awayTeamGoals;
            set
            {
                if (_awayTeamGoals != value)
                {
                    _awayTeamGoals = value;
                    RaisePropertyChanged(nameof(AwayTeamGoals));
                }
            }
        }

        public Game Game { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}