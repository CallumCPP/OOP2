namespace OOP2;

public class GameStats {
    public int Plays { get; set; }
    public List<Player> Players { get; init; } = [];

    /// <summary>
    /// Class to store player stats
    /// </summary>
    public class Player {
        public string Name { get; init; } = "";
        public List<int> Scores { get; init; } = [];
        public int Wins { get; set; }
    }

    /// <summary>
    /// Adds an entry to the game
    /// </summary>
    /// <param name="name">Name of winner</param>
    /// <param name="score">Score of winner</param>
    /// <param name="tie">Whether the game ended in a tie</param>
    public void AddEntry(string name, int score, bool tie) {
        // Add a play to the game
        Plays++;

        // Don't save the score if the score is 0
        if (score == 0)
            return;

        // Find player with the name
        Player? player = (from entry in Players
                          where entry.Name == name
                          select entry).SingleOrDefault();

        // Player was found, add to their entry
        if (player != null) {
            player.Scores.Add(score);
            player.Scores.Sort();
            player.Wins += tie ? 0 : 1;
        }
        // Player was not found, create new entry
        else {
            Player newPlayer = new() {
                Name = name,
                Scores = [score],
                Wins = tie ? 0 : 1
            };

            Players.Add(newPlayer);
        }
    }

    /// <summary>
    /// Gets the statistics of a game
    /// </summary>
    /// <returns>String containing the high scores</returns>
    public string GetStats() {
        // Order all scores and get the name, number of wins, and score from each score
        var highScores = (from entry in Players
                          from score in entry.Scores
                          orderby score descending, entry.Name
                          select new { entry.Name, entry.Wins, Score = score }).ToList();

        // Add the top 10 scores to a string
        string statsStr = $"{Plays} plays\n";
        int scoresToShow = Math.Min(highScores.Count, 10);
        foreach (var entry in highScores[..scoresToShow]) {
            statsStr += $"{entry.Name} ({entry.Wins} wins) {entry.Score}\n";
        }

        return statsStr;
    }

    /// <summary>
    /// Gets a players stats in a game
    /// </summary>
    /// <param name="name">Name of the player</param>
    /// <returns>String containing the wins and high scores</returns>
    public string GetPlayerStats(string name) {
        // Get the entry with the right name, null if not found
        Player? playerStats = (from entry in Players
                               where entry.Name == name
                               select entry).SingleOrDefault();

        if (playerStats == null)
            return $"Did not find player {name}!\n";

        // Format high scores and wins
        string playerStatsStr = $"{name} has {playerStats.Wins} wins\n";

        playerStatsStr += "High scores:\n";
        int scoresToShow = Math.Min(playerStats.Scores.Count, 10);
        foreach (int score in playerStats.Scores[..scoresToShow])
            playerStatsStr += $"\t{score}\n";

        return playerStatsStr;
    }
}
