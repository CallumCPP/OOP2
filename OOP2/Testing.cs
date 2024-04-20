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
    }
    
    /// <summary>
    /// Test Sevens Out
    /// </summary>
    private static void _testSevensOut() {
        Console.WriteLine("Testing Sevens Out...");
        
        // List "rolls" stores data to test sevens out
        List<(int roll1, int roll2, int total)> rolls = [];
        SevensOut game = new(rolls);
        game.Play();
        
        // Check rolls were added correctly
        foreach ((int roll1, int roll2, int total) roll in rolls) {
            Debug.Assert(roll.roll1 + roll.roll2 == roll.total,
                         $"[Sevens Out] Rolls were not added correctly, total should be {roll.roll1 + roll.roll2} but was {roll.total}");
        }
        
        // Ensure last total was 7
        Debug.Assert(rolls.Last().total == 7,
                     $"[Sevens Out] Last roll should always be 7, was {rolls.Last().total}");
        
        Console.WriteLine("Completed testing Sevens Out");
    }
    
    /// <summary>
    /// Used to store data to test Three Or More
    /// </summary>
    public class ThreeOrMoreTesting {
        public readonly int[] WinnerLastScores = [0, 0];
        public readonly List<(int streakLen, int scoreAugment)> StreakScores = [];
    }
    
    /// <summary>
    /// Test Three Or More
    /// </summary>
    private static void _testThreeOrMore() {
        Console.WriteLine("Testing Three Or More...");
        
        ThreeOrMoreTesting stats = new();
        ThreeOrMore game = new(stats);
        game.Play();
        
        // Ensure the game recognises when the player has got at least 20
        Debug.Assert(stats.WinnerLastScores[0] < 20 && stats.WinnerLastScores[1] >= 20,
                     "[Three Or More] Second to last score should be < 20 and last score should be > 20");

        // Ensure correct number of points is added for each streak length
        foreach ((int streakLen, int scoreAugment) score in stats.StreakScores) {
            switch (score.streakLen) {
                case 3:
                    Debug.Assert(score.scoreAugment == 3,
                                 $"[Three Or More] With a streak of 3 score should increase by 3, instead increased by {score.scoreAugment}");
                    break;
                
                case 4:
                    Debug.Assert(score.scoreAugment == 6,
                                 $"[Three Or More] With a streak of 4 score should increase by 6, instead increased by {score.scoreAugment}");
                    break;
                
                case 5:
                    Debug.Assert(score.scoreAugment == 12,
                                 $"[Three Or More] With a streak of 5 score should increase by 12, instead increased by {score.scoreAugment}");
                    break;
            }
        }
        
        Console.WriteLine("Completed testing Three Or More");
    }
}
