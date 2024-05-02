using System.Text.Json;

namespace OOP2;

/// <summary>
/// Static class holding statistics methods
/// </summary>
public static class Statistics {
    private static GameStats _sevensOutStats = new();
    private static GameStats _threeOrMoreStats = new();
    
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
        // Try 5 times
        int tries = 0;
        while (tries < 5) {
            string dummyJson = JsonSerializer.Serialize(new GameStats());

            if (!File.Exists("Sevens Out Stats.json"))
                File.WriteAllText("Sevens Out Stats.json", dummyJson);

            if (!File.Exists("Three Or More Stats.json"))
                File.WriteAllText("Three Or More Stats.json", dummyJson);
            
            // Create dummy nullable variables in case of null
            GameStats? soDummy = JsonSerializer.Deserialize<GameStats>(File.ReadAllText("Sevens Out Stats.json"));
            GameStats? tomDummy = JsonSerializer.Deserialize<GameStats>(File.ReadAllText("Three Or More Stats.json"));

            if (soDummy == null || tomDummy == null) {
                Console.WriteLine("Statistic files are damaged, deleting and reloading...");
                File.Delete("Sevens Out Stats.json");
                File.Delete("Three Or More Stats.json");
                tries++;
                continue;
            }

            _sevensOutStats = soDummy;
            _threeOrMoreStats = tomDummy;

            return;
        }
        
        // Big problem if code reaches here, program will never run
        Console.WriteLine("Failed to load file, attempted 5 times. Critical error and program will never run.");
        Environment.Exit(1);
    }

    /// <summary>
    /// Add an entry for the Sevens Out game
    /// </summary>
    /// <param name="name">Winner name</param>
    /// <param name="score">Winner score</param>
    /// <param name="tie">Whether the game ended in a tie</param>
    public static void AddSevensOut(string name, int score, bool tie) {
        _sevensOutStats.AddEntry(name, score, tie);
    }
    
    /// <summary>
    /// Add an entry for the Three Or More game
    /// </summary>
    /// <param name="name">Winner name</param>
    /// <param name="score">Winner score</param>
    public static void AddThreeOrMore(string name, int score) {
        _threeOrMoreStats.AddEntry(name, score, false);
    }

    /// <summary>
    /// Gets the high scores for the Sevens Out game
    /// </summary>
    /// <returns>String containing the high scores</returns>
    public static string GetStatsSO() {
        return _sevensOutStats.GetStats();
    }
    
    /// <summary>
    /// Gets the high scores for the Three Or More game
    /// </summary>
    /// <returns>String containing the high scores</returns>
    public static string GetStatsTOM() {
        return _threeOrMoreStats.GetStats();
    }
    
    /// <summary>
    /// Gets the stats for a player in the Sevens Out game
    /// </summary>
    /// <param name="name">Name of the player</param>
    /// <returns>String containing the wins and high scores</returns>
    public static string GetPlayerStatsSO(string name) {
        return _sevensOutStats.GetPlayerStats(name);
    }
    
    /// <summary>
    /// Gets the stats for a player in the Three Or More game
    /// </summary>
    /// <param name="name">Name of the player</param>
    /// <returns>String containing the wins and high scores</returns>
    public static string GetPlayerStatsTOM(string name) {
        return _threeOrMoreStats.GetPlayerStats(name);
    }
}
