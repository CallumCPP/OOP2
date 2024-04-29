namespace OOP2;

public static class Program {
    private static void Main() {
        // If Ctrl+C is pressed, save statistics
        Console.CancelKeyPress += delegate {
            Statistics.Save();
        };
        
        Statistics.Load();
        Console.WriteLine("OOP 2 Assessment.");

        bool shouldRun = true;
        while (shouldRun) {
            int choice = Input.GetInt(
                "Pick an option:\n" +
                "\t1) Play Sevens Out\n" +
                "\t2) Play Three Or More\n" +
                "\t3) View stats\n" +
                "\t4) View players stats\n" +
                "\t5) Start testing\n" +
                "\t6) Exit\n",
                1, 6);

            switch (choice) {
                case 1: // Sevens out
                    _playSevensOut();
                    break;

                case 2: // Three or more
                    _playThreeOrMore();
                    break;

                case 3: // View stats
                    _viewStats();
                    break;
                
                case 4: // View players stats
                    _viewPlayersStats();
                    break;

                case 5: // Start testing
                    Testing.Test();
                    break;

                case 6: // Exit
                    shouldRun = false;
                    break;
            }
            
            Console.WriteLine();
        }
        
        Statistics.Save();
        Console.WriteLine("Thanks for playing!");
    }
    
    /// <summary>
    /// Plays Sevens Out
    /// </summary>
    private static void _playSevensOut() {
        Game game = new Games.SevensOut();
        (string name, int score, bool tie) = game.Play();
        Statistics.AddSevensOut(name, score, tie);
    }
    
    /// <summary>
    /// Plays Three Or More
    /// </summary>
    private static void _playThreeOrMore() {
        Game game = new Games.ThreeOrMore();
        (string name, int score, bool _) = game.Play();
        Statistics.AddThreeOrMore(name, score);
    }
    
    /// <summary>
    /// Views a games statistics
    /// </summary>
    private static void _viewStats() {
        int choice = 1;
        while (choice != 3) {
            choice = Input.GetInt(
                "Which game would you like statistics of?\n" +
                "\t1) Sevens Out\n" +
                "\t2) Three Or More\n" +
                "\t3) Exit to main menu\n"
            );

            switch (choice) {
                case 1: // Sevens Out
                    Console.WriteLine(Statistics.GetStatsSO());
                    break;
                
                case 2: // Three Or More
                    Console.WriteLine(Statistics.GetStatsTOM());
                    break;
            }
        }
        
        Console.WriteLine("Returning to main menu...");
    }
    
    /// <summary>
    /// Views a players statistics
    /// </summary>
    private static void _viewPlayersStats() {
        Console.WriteLine("Enter the name of the player: ");
        string playerName = Console.ReadLine()!;
        
        int choice = 1;
        while (choice != 3) {
            choice = Input.GetInt(
                "\nWhich game would you like statistics of?\n" +
                "\t1) Sevens Out\n" +
                "\t2) Three Or More\n" +
                "\t3) Exit to main menu\n"
            );
            
            Console.WriteLine();
            
            switch (choice) {
                case 1: // Sevens Out
                    Console.WriteLine(Statistics.GetPlayerStatsSO(playerName));
                    break;
                
                case 2: // Three Or More
                    Console.WriteLine(Statistics.GetPlayerStatsTOM(playerName));
                    break;
            }
        }
    }
}
