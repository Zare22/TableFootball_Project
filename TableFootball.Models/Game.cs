using System.Collections.Generic;
using System.Linq;

namespace TableFootball.Models
{
    public class Game
    {
        public int IdGame { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public League League { get; set; }
        public Team Winner { get; set; }    
        public ICollection<GameSet> Sets { get; set; }
    }
}
