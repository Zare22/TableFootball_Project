using System.Collections.Generic;
using System.ComponentModel;

namespace TableFootball.Models
{
    public class League : INotifyPropertyChanged
    {
        public int IdLeague { get; set; }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public ICollection<Team> Teams { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public override string ToString() => Name;
    }
}