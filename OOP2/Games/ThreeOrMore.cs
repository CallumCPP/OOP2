namespace OOP2.Games;

/// <summary>
/// Three Or More game
/// </summary>
/// <param name="testingStats">Object to store testing stats in</param>
public class ThreeOrMore(Testing.ThreeOrMoreTesting? testingStats = null) : Game("Three Or More", testingStats != null) {
    private readonly Die[] _dice = [ new Die(), new Die(), new Die(), new Die(), new Die() ];
    private readonly Random _random = new();

    /// <summary>
    /// Plays Three Or More
    /// </summary>
    /// <returns>Name and score of the winner and whether the game ended in a tie</returns>
    protected override (int score, bool tie) _play() {
        while (true) {
            _showScores();
            
            Console.WriteLine($"P{_player+1} GO");
            
            _waitForRoll();
            
            foreach (Die die in _dice)
                die.Roll();
            
            _displayRolls();

            (int longestStreakLen, int longestStreakVal) = _calculateStreak();
            
            switch (longestStreakLen) {
                case 1: // No matches
                    Console.WriteLine("No matches, no points!");
                    break;
                
                case 2: // Two match
                    Console.WriteLine("Two dice match!");
                    if (_player == 1 && !_multiplayer)
                        _rerollDie(longestStreakVal, true);
                    else
                        _rerollDie(longestStreakVal, false);
                    (longestStreakLen) = _calculateStreak().longestStreakLen;
                    break;
            }
            
            // If testing is enabled save the score before augmentation
            if (_testing)
                testingStats!.WinnerLastScores[0] = _scores[_player];
            
            // Split into 2 switch statement since "longestStreakLen" might be changed by a streak length of 2
            int scoreAugment = 0;
            switch (longestStreakLen) {
                case 3: // Three match
                    Console.WriteLine("Three dice match (+3)!");
                    scoreAugment = 3;
                    break;
                
                case 4: // Four match
                    Console.WriteLine("Four dice match (+6)!");
                    scoreAugment = 6;
                    break;
                
                case 5: // Five match
                    Console.WriteLine("Five dice match (+12)!");
                    scoreAugment = 12;
                    break;
            }

            _scores[_player] += scoreAugment;
            
            // If testing is enabled, store the score augmentation
            if (_testing) {
                testingStats!.WinnerLastScores[1] = _scores[_player];
                testingStats!.StreakScores.Add((longestStreakLen, scoreAugment));
            }

            Console.WriteLine($"New score is {_scores[_player]} (+{scoreAugment})\n");
            
            // When a player reaches 20 or more, end the game
            if (_scores[_player] >= 20)
                break;
            
            // If not in testing wait to clear the console
            if (!_testing)
                Input.WaitForEnter();
            
            // Switch player
            _player = (_player + 1) % 2;
        }
        
        Console.WriteLine($"P{_player+1} won!");

        // If bot won or testing is enabled, don't save score
        if ((!_multiplayer && _player == 1) || _testing)
            return (0, false);
        
        return (_scores[_player], false);
    }

    /// <summary>
    /// Finds the longest streak length and value
    /// </summary>
    /// <returns>Longest streak length and value</returns>
    private (int longestStreakLen, int longestStreakVal) _calculateStreak() {
        int longestStreakLen = 0;
        int longestStreakVal = 0;
        
        foreach (Die first in _dice) {
            // Count the dice with the same value
            int curStreakLen = _dice.Count(second => first.Value == second.Value);
            
            // If a new longer streak is found, set the longest to this
            if (curStreakLen > longestStreakLen) {
                longestStreakLen = curStreakLen;
                longestStreakVal = first.Value;
            }
        }

        return (longestStreakLen, longestStreakVal);
    }
    
    /// <summary>
    /// Re rolls the die, called when a streak of 2 is found
    /// </summary>
    /// <param name="longestStreakVal">Value of the longest streak</param>
    /// <param name="bot">Whether it's a bots turn</param>
    private void _rerollDie(int longestStreakVal, bool bot) {
        int choice;
        if (bot || _testing) {
            choice = _random.Next(0, 10);
            
            if (choice < 3) { // 30% chance to re roll all
                choice = 1;
                Console.WriteLine("Bot chose to re roll all");
            }
            else {            // 70% chance to re roll unpaired
                choice = 2;
                Console.WriteLine("Bot chose to re roll unpaired");
            }
        }
        else {
            choice = Input.GetInt("Would you like to re roll all dice (1) or just unpaired (2)?", 1, 2);
        }

        switch (choice) {
            case 1: // Re roll all
                foreach (Die die in _dice)
                    die.Roll();
                break;
                    
            case 2: // Re roll unpaired
                List<Die> unpairedDice = (from die in _dice 
                                          where die.Value != longestStreakVal 
                                          select die).ToList();
                foreach (Die die in unpairedDice)
                    die.Roll();
                
                break;
        }
        
        _displayRolls();
    }
    
    /// <summary>
    /// Displays rolls
    /// </summary>
    private void _displayRolls() {
        List<int> rolls = (from die in _dice select die.Value).ToList();
        Console.WriteLine($"You rolled {string.Join(", ", rolls)}");
    }
}
