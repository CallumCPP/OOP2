namespace OOP2;

/// <summary>
/// Abstract game class to be inherited from by games
/// </summary>
/// <param name="name">Name of the game</param>
/// <param name="testing">Whether testing is enabled</param>
public abstract class Game(string name, bool testing) {
    /// <summary>
    /// Plays the game
    /// </summary>
    /// <returns>Name and score of the winner and whether the game ended in a tie</returns>
    public (string name, int score, bool tie) Play() {
        Console.WriteLine($"Playing {name}!\n");
        
        // If testing is enabled this question will not be asked
        bool multiplayer = !testing && Input.YesNo("Would you like to play against another player?");

        return _play(multiplayer);
    }
    
    /// <summary>
    /// Internal play function to be overriden by each game
    /// </summary>
    /// <param name="multiplayer">Whether or not to play with another player</param>
    /// <returns>Name and score of the winner and whether the game ended in a tie</returns>
    protected abstract (string name, int score, bool tie) _play(bool multiplayer);
}
