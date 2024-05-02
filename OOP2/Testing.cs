using System.Diagnostics;
using OOP2.Games;

namespace OOP2;

/// <summary>
/// Static class holding testing methods
/// </summary>
public static class Testing {
    /// <summary>
    /// Initiate testing
    /// </summary>
    public static void Test() {
        _testSevensOut();
        _testThreeOrMore();
        
        Console.Clear();
        Console.WriteLine("Tests passed and logged!");
        Input.WaitForEnter();
    }

    /// <summary>
    /// Implemented by testing data classes
    /// </summary>
    private interface ITestData {
        public string GetName();
    }

    /// <summary>
    /// Used to store data to test Sevens Out
    /// </summary>
    public class SevensOutData : ITestData {
        public readonly List<(int roll1, int roll2, int total)> Rolls = [];
        
        public string GetName() {
            return "Sevens Out";
        }
    }
    
    /// <summary>
    /// Used to store data to test Three Or More
    /// </summary>
    public class ThreeOrMoreData : ITestData {
        public readonly int[] WinnerLastScores = [0, 0];
        public readonly List<(int streakLen, int scoreAugment)> StreakScores = [];
        
        public string GetName() {
            return "Three Or More";
        }
    }
    
    /// <summary>
    /// Test Sevens Out
    /// </summary>
    private static void _testSevensOut() {
        Console.WriteLine("Testing Sevens Out...");
        
        // List "rolls" stores data to test sevens out
        SevensOutData data = new();
        SevensOut game = new(data);
        game.Play();
        
        // Check rolls were added correctly
        foreach ((int roll1, int roll2, int total) roll in data.Rolls) {
            _assertAndLogSO(roll.roll1 + roll.roll2 == roll.total,
                            $"Rolls were not added correctly, total should be {roll.roll1 + roll.roll2} but was {roll.total}");
        }
        
        // Ensure last total was 7
        _assertAndLogSO(data.Rolls.Last().total == 7,
                        $"Last roll should always be 7, was {data.Rolls.Last().total}");
        
        // If this code is reached, the test passed
        File.AppendAllText("SO Test Log.txt", $"{DateTime.Now}: Test passed");

        Console.WriteLine($"Completed testing {data.GetName()}");
    }

    /// <summary>
    /// Same as Debug.Assert, but logs to Sevens Out log file
    /// </summary>
    /// <param name="condition">Condition to assert</param>
    /// <param name="message">Message on failure</param>
    private static void _assertAndLogSO(bool condition, string message) {
        if (!condition)
            File.AppendAllText("SO Test Log.txt", $"{DateTime.Now}: Failed with message - {message}");
        
        Debug.Assert(condition, $"[Sevens Out] {message}");
    }

    /// <summary>
    /// Test Three Or More
    /// </summary>
    private static void _testThreeOrMore() {
        Console.WriteLine("Testing Three Or More...");
        
        ThreeOrMoreData data = new();
        ThreeOrMore game = new(data);
        game.Play();
        
        // Ensure the game recognises when the player has got at least 20
        _assertAndLogTOM(data.WinnerLastScores[0] < 20 && data.WinnerLastScores[1] >= 20,
                         "Second to last score should be < 20 and last score should be > 20");

        // Ensure correct number of points is added for each streak length
        foreach ((int streakLen, int scoreAugment) score in data.StreakScores) {
            switch (score.streakLen) {
                case 3:
                    _assertAndLogTOM(score.scoreAugment == 3,
                                     $"With a streak of 3 score should increase by 3, instead increased by {score.scoreAugment}");
                    break;
                
                case 4:
                    _assertAndLogTOM(score.scoreAugment == 6,
                                     $"With a streak of 4 score should increase by 6, instead increased by {score.scoreAugment}");
                    break;
                
                case 5:
                    _assertAndLogTOM(score.scoreAugment == 12,
                                     $"With a streak of 5 score should increase by 12, instead increased by {score.scoreAugment}");
                    break;
            }
        }
        
        // If this code is reached, the test passed
        File.AppendAllText("TOM Test Log.txt", $"{DateTime.Now}: Test passed");
        
        Console.WriteLine($"Completed testing {data.GetName()}");
    }
    
    /// <summary>
    /// Same as Debug.Assert, but logs to Three Or More log file
    /// </summary>
    /// <param name="condition">Condition to assert</param>
    /// <param name="message">Message on failure</param>
    private static void _assertAndLogTOM(bool condition, string message) {
        if (!condition)
            File.AppendAllText("TOM Test Log.txt", $"{DateTime.Now}: Failed with message - {message}");
        
        Debug.Assert(condition, $"[Three Or More] {message}");
    }
}
