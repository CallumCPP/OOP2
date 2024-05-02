namespace OOP2;

/// <summary>
/// Abstract game class to be inherited from by games
/// </summary>
/// <param name="name">Name of the game</param>
/// <param name="testing">Whether testing is enabled</param>
public abstract class Game(string name, bool testing) {
    protected bool _multiplayer { get; private set; }
    protected readonly bool _testing = testing;
    protected int[] _scores = [0, 0];
    protected int _player = 0;

    /// <summary>
    /// Plays the game
    /// </summary>
    /// <returns>Name and score of the winner and whether the game ended in a tie</returns>
    public (string name, int score, bool tie) Play() {
        Console.WriteLine($"Playing {name}!\n");
        
        // If testing is enabled this question will not be asked
        _multiplayer = !_testing && Input.YesNo("Would you like to play against another player?");

        (int score, bool tie) = _play();

        // Get a name to save the score
        string playerName = "";
        if (score != 0 && !_testing) {
            Console.WriteLine("Enter a name to save your score: ");
            playerName = Console.ReadLine()!;
        }
        
        if (!_testing)
            Input.WaitForEnter();

        return (playerName, score, tie);
    }
    
    /// <summary>
    /// Internal play function to be overriden by each game
    /// </summary>
    /// <returns>Name and score of the winner and whether the game ended in a tie</returns>
    protected abstract (int score, bool tie) _play();

    protected void _waitForRoll() {
        // If testing is disabled, and it's a players turn, ask the user
        if (!_testing && (_player == 0 || _multiplayer)) {
            Console.Write("Press enter to roll your dice: ");
            Console.ReadLine();
            Console.Write("\b");
        }
        // Otherwise bot should play
        else {
            Console.Write("Bot is rolling");
            for (int i = 0; i < 5; i++) {
                Console.Write(".");
                Thread.Sleep(_testing ? 0 : 100);
            }
            
            Console.WriteLine();
        }
    }
    
    /// <summary>
    /// Shows player scores and clears the screen
    /// </summary>
    protected void _showScores() {
        Console.Clear();
        Console.WriteLine($"P1: {_scores[0]} - P2: {_scores[1]}");
    }
}
