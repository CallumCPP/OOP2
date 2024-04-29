using System.Text.Json;

namespace OOP2;

/// <summary>
/// Static class holding statistics methods
/// </summary>
public static class Statistics {
    /// <summary>
    /// Class to store player stats
    /// </summary>
    private class Player {
        public string Name { get; init; } = "";
        public List<int> Scores { get; init; } = [];
        public int Wins { get; set; }
    }

    private static List<Player> _sevensOutStats = [];
    private static List<Player> _threeOrMoreStats = [];
    
    /// <summary>
    /// Saves player data
    /// </summary>
    public static void Save() {
        string soJson = JsonSerializer.Serialize(_sevensOutStats);
        File.WriteAllText("Sevens Out Stats.json", soJson);

        string tomJson = JsonSerializer.Serialize(_threeOrMoreStats);
        File.WriteAllText("Three Or More Stats.json", tomJson);
    }
    
    /// <summary>
    /// Loads player data
    /// </summary>
    public static void Load() {
        if (!File.Exists("Three Or More Stats.json"))
            File.WriteAllText("Three Or More Stats.json", "[]");
        
        if (!File.Exists("Sevens Out Stats.json"))
            File.WriteAllText("Sevens Out Stats.json", "[]");
        
        _threeOrMoreStats = JsonSerializer.Deserialize<List<Player>>(File.ReadAllText("Three Or More Stats.json"))!;
        _sevensOutStats = JsonSerializer.Deserialize<List<Player>>(File.ReadAllText("Sevens Out Stats.json"))!;
    }
    
    /// <summary>
    /// Add an entry for the Sevens Out game
    /// </summary>
    /// <param name="name">Winner name</param>
    /// <param name="score">Winner score</param>
    /// <param name="tie">Whether the game ended in a tie</param>
    public static void AddSevensOut(string name, int score, bool tie) {
        _addEntry(_sevensOutStats, name, score, tie);
    }
    
    /// <summary>
    /// Add an entry for the Three Or More game
    /// </summary>
    /// <param name="name">Winner name</param>
    /// <param name="score">Winner score</param>
    public static void AddThreeOrMore(string name, int score) {
        _addEntry(_threeOrMoreStats, name, score, false);
    }

    /// <summary>
    /// Gets the high scores for the Sevens Out game
    /// </summary>
    /// <returns>String containing the high scores</returns>
    public static string GetStatsSO() {
        return _getStats(_sevensOutStats);
    }
    
    /// <summary>
    /// Gets the high scores for the Three Or More game
    /// </summary>
    /// <returns>String containing the high scores</returns>
    public static string GetStatsTOM() {
        return _getStats(_threeOrMoreStats);
    }
    
    /// <summary>
    /// Gets the stats for a player in the Sevens Out game
    /// </summary>
    /// <param name="name">Name of the player</param>
    /// <returns>String containing the wins and high scores</returns>
    public static string GetPlayerStatsSO(string name) {
        return _getPlayerStats(_sevensOutStats, name);
    }
    
    /// <summary>
    /// Gets the stats for a player in the Three Or More game
    /// </summary>
    /// <param name="name">Name of the player</param>
    /// <returns>String containing the wins and high scores</returns>
    public static string GetPlayerStatsTOM(string name) {
        return _getPlayerStats(_threeOrMoreStats, name);
    }
    
    /// <summary>
    /// Adds an entry to the game
    /// </summary>
    /// <param name="game">Game to add to</param>
    /// <param name="name">Name of winner</param>
    /// <param name="score">Score of winner</param>
    /// <param name="tie">Whether the game ended in a tie</param>
    private static void _addEntry(List<Player> game, string name, int score, bool tie) {
        // Don't save the score if the score is 0
        if (score == 0)
            return;
        
        // Find player with the name
        Player? player = (from entry in game
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

            game.Add(newPlayer);
        }
    }
    
    /// <summary>
    /// Gets the statistics of a game
    /// </summary>
    /// <param name="game">Game to get the statistics of</param>
    /// <returns>String containing the high scores</returns>
    private static string _getStats(List<Player> game) {
        // Order all scores and get the name, number of wins, and score from each score
        var highScores = (from entry in game
                          from score in entry.Scores
                          orderby score descending, entry.Name
                          select new { entry.Name, entry.Wins, Score = score }).ToList();
        
        // Add the top 10 scores to a string
        string highScoresStr = "";
        int scoresToShow = Math.Min(highScores.Count, 10);
        foreach (var entry in highScores[..scoresToShow]) {
            highScoresStr += $"{entry.Name} ({entry.Wins} wins) {entry.Score}\n";
        }

        return highScoresStr;
    }
    
    /// <summary>
    /// Gets a players stats in a game
    /// </summary>
    /// <param name="game">Game to get the statistics of</param>
    /// <param name="name">Name of the player</param>
    /// <returns>String containing the wins and high scores</returns>
    private static string _getPlayerStats(List<Player> game, string name) {
        // Get the entry with the right name, null if not found
        Player? playerStats = (from entry in game
                               where entry.Name == name
                               select entry).SingleOrDefault();

        if (playerStats == null)
            return $"Did not find player {name}!\n";
        
        // Format high scores and wins
        string playerStatsStr = $"{name} has {playerStats.Wins} wins\n";
        playerStatsStr += "High scores:\n";
        
        foreach (int score in playerStats.Scores)
            playerStatsStr += $"\t{score}\n";
        
        return playerStatsStr;
    }
}
