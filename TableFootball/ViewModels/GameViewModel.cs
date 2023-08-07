using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TableFootball.DataAccess.Factories;
using TableFootball.Messages;
using TableFootball.Models;

namespace TableFootball.ViewModels
{
    public class GameViewModel : ViewModelBase, IViewModel
    {

        private Game _game;
        private int _newGameSetHomeGoals;
        private int _newGameSetAwayGoals;

        public GameViewModel()
        {
            Game = new Game
            {
                HomeTeam = new Team(),
                AwayTeam = new Team(),
                Sets = new ObservableCollection<GameSet>()
            };

            NewGameSetHomeGoals = 0;
            NewGameSetAwayGoals = 0;
        }

        public Game Game
        {
            get => _game;
            set => Set(ref _game, value);
        }

        public int NewGameSetHomeGoals
        {
            get => _newGameSetHomeGoals;
            set => Set(ref _newGameSetHomeGoals, value);
        }

        public int NewGameSetAwayGoals
        {
            get => _newGameSetAwayGoals;
            set => Set(ref _newGameSetAwayGoals, value);
        }

        private GameSet _selectedGameSet;
        public GameSet SelectedGameSet
        {
            get => _selectedGameSet;
            set => Set(ref _selectedGameSet, value);
        }

        public ObservableCollection<GameSet> AllSets { get; } = new ObservableCollection<GameSet>();
        public void AddGameSet()
        {
            if (NewGameSetHomeGoals != 0 || NewGameSetAwayGoals != 0)
            {
                // Check if the goals in the set are within the allowed range (0 to 10)
                if (NewGameSetHomeGoals >= 0 && NewGameSetHomeGoals <= 10 &&
                    NewGameSetAwayGoals >= 0 && NewGameSetAwayGoals <= 10 &&
                    NewGameSetHomeGoals + NewGameSetAwayGoals != 20) // This ensures that it's not 10-10 or above
                {
                    if (SelectedGameSet == null && AllSets.Count < 3)
                    {
                        // If no set is selected and the maximum of 3 sets is not reached, add a new one
                        var newGameSet = new GameSet
                        {
                            HomeTeamGoals = NewGameSetHomeGoals,
                            AwayTeamGoals = NewGameSetAwayGoals,
                            Game = Game
                        };

                        AllSets.Add(newGameSet);
                        Game.Sets.Add(newGameSet);

                        // Check if either team has won the game after adding a set
                        CheckGameResult();
                    }
                    else if (SelectedGameSet != null)
                    {
                        // If a set is selected, edit it
                        SelectedGameSet.HomeTeamGoals = NewGameSetHomeGoals;
                        SelectedGameSet.AwayTeamGoals = NewGameSetAwayGoals;

                        // Check if either team has won the game after editing a set
                        CheckGameResult();
                    }

                    // Reset input fields after adding/editing the set
                    NewGameSetHomeGoals = 0;
                    NewGameSetAwayGoals = 0;
                    SelectedGameSet = null;
                }
                else
                {
                    Messenger.Default.Send(new CustomMessage(false, "Invalid score! The set is played to 10!"));
                }
            }
        }


        private void CheckGameResult()
        {
            int homeSetsWon = 0;
            int awaySetsWon = 0;

            foreach (var set in AllSets)
            {
                if (set.HomeTeamGoals > set.AwayTeamGoals)
                    homeSetsWon++;
                else if (set.HomeTeamGoals < set.AwayTeamGoals)
                    awaySetsWon++;
            }

            if (homeSetsWon >= 2 || awaySetsWon >= 2)
            {
                Game.Sets = AllSets;
                if (homeSetsWon > awaySetsWon)
                {
                    Game.Winner = Game.HomeTeam;
                    Messenger.Default.Send(new CustomMessage(true, $"{Game.HomeTeam} team won the game!"));
                }
                else if (homeSetsWon < awaySetsWon)
                {
                    Game.Winner = Game.AwayTeam;
                    Messenger.Default.Send(new CustomMessage(true, $"{Game.AwayTeam} won the game!"));
                }
            }
        }

        public void SaveTheGame()
        {
            Task.Run(async () =>
            {
                try
                {
                    int gameId = await RepositoryFactory.GetGameRepository().CreateAsync(Game);
                    Game.IdGame = gameId;
                    Game.Sets.ToList().ForEach(async set => await RepositoryFactory.GetGameSetRepository().CreateAsync(set));
                    Messenger.Default.Send(new GameSavedMessage(true, "Game was successfully saved!"));
                }
                catch (Exception ex)
                {
                    Messenger.Default.Send(new GameSavedMessage(false, $"An error occurred while saving the game! {ex.Message}"));
                }
            });
        }

    }
}
