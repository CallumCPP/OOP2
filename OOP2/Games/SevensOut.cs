namespace OOP2.Games;

/// <summary>
/// Sevens Out game
/// </summary>
/// <param name="testingRolls">Optional, list to store rolls in for testing. If null testing will be disabled</param>
public class SevensOut(List<(int, int, int)>? testingRolls = null) : Game("Sevens Out", testingRolls != null) {
    private readonly Die[] _dice = [ new Die(), new Die() ];
    
    /// <summary>
    /// Plays Sevens Out
    /// </summary>
    /// <param name="multiplayer">Whether multiplayer is enabled or not</param>
    /// <returns>Name and score of the winner and whether the game ended in a tie</returns>
    protected override (string name, int score, bool tie) _play(bool multiplayer) {
        bool testing = testingRolls != null;
        int[] scores = [ 0, 0 ];
        int player = 0;

        while (true) {
            Console.WriteLine($"Player {player+1}'s turn with a score of {scores[player]}");
            
            // If testing is disabled, and it's a players turn, ask the user
            if (!testing && (player == 0 || multiplayer)) {
                Console.Write("Press enter to roll your dice: ");
                Console.ReadLine();
            }
            // Otherwise bot should play
            else {
                Console.Write("Bot is rolling");
                for (int i = 0; i < 5; i++) {
                    Console.Write(".");
                    Thread.Sleep(testing ? 0 : 100);
                }
                
                Console.WriteLine();
            }
            
            Console.Write($"You rolled a {_dice[0].Roll()} and a {_dice[1].Roll()} ");
            int total = _dice[0].Value + _dice[1].Value;
            Console.WriteLine($"totalling {total}");

            testingRolls?.Add((_dice[0].Value, _dice[1].Value, total)); // Only runs when "testingStats" != null
            
            // If the total is 7, the game should end
            if (total == 7)
                break;
            
            // If the player rolled a double, add twice the total to their score, otherwise jut the total
            if (_dice[0].Value == _dice[1].Value)
                scores[player] += 2 * total;
            else
                scores[player] += total;
            
            Console.WriteLine($"Your new score is {scores[player]}\n");
            
            // Switch to other player
            player = (player + 1) % 2;
        }

        Console.WriteLine("\nRolled a 7! End of game\n");
        
        string name = "";
        int winnersTotal;
        bool tie = false;
        
        if (scores[0] > scores[1]) {        // Player 1 won
            Console.WriteLine($"Player 1 won with a score of {scores[0]}!");
            winnersTotal = scores[0];
        }
        else if (scores[1] > scores[0]) {   // Player 2 won
            Console.WriteLine($"Player 2 won with a score of {scores[1]}!");
            winnersTotal = scores[1];
            
            // If the bot won, don't save score
            if (!multiplayer)
                return ("", 0, false);
        }
        else {                              // Tie
            Console.WriteLine($"It's a tie! Both players got {scores[0]}");
            winnersTotal = scores[0];
            tie = true;
        }
        
        // Ask the user for their name if testing is disabled
        if (!testing) {
            Console.WriteLine("Enter a name to save the score: ");
            name = Console.ReadLine()!;
        }

        return (name, winnersTotal, tie);
    }
}
