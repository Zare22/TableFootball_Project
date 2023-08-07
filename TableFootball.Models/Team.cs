using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TableFootball.Models
{
    public class Team : INotifyPropertyChanged
    {
        public int IdTeam { get; set; }

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

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Player> Players { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public override string ToString() => Name;

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Team))
                return false;

            Team otherTeam = (Team)obj;

            return this.IdTeam == otherTeam.IdTeam;
        }

        public override int GetHashCode()
        {
            return this.IdTeam.GetHashCode();
        }
    }
}