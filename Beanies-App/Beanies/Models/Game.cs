using System;
using System.Collections.Generic;

namespace Beanies.Models
{
    public class Game
    {
        public Game()
        {

        }

        public Game(string title, List<User> players, int totalRounds = 13)
        {
            Id = Guid.NewGuid().ToString();
            Name = title;
            Players = players;
            TotalRounds = totalRounds;
            CurrentRound = 0;
            foreach (var player in Players)
            {
                ScoreBoard.Add(player, 0);
            }
        }

        public string Id;
        public string Name { get; set; }
        public List<User> Players { get; set; }
        public int CurrentRound { get; set; }
        public int TotalRounds { get; set; }
        public Dictionary<User, int> ScoreBoard { get; set; }

        public string PlayersDisplayText
        {
            get
            {
                return $"Players: {Players.Count}";
            }
        }

        public string RoundsDisplayText
        {
            get
            {
                return $"Rounds Left: {TotalRounds - CurrentRound}";
            }
        }
    }
}
