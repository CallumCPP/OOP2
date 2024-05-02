namespace OOP2;

/// <summary>
/// Static class holding input methods
/// </summary>
public static class Input {
    /// <summary>
    /// Gets an integer from the user
    /// </summary>
    /// <param name="message">Message to display</param>
    /// <param name="min">Minimum value, inclusive</param>
    /// <param name="max">Maximum value, inclusive</param>
    /// <returns>Users choice of integer</returns>
    public static int GetInt(string message, int min = int.MinValue, int max = int.MaxValue) {
        string? input = null;
        int choice;

        Console.Write(message);
        
        // While the input is not a valid integer or is not in range keep asking the user
        while (input == null || !int.TryParse(input, out choice) || choice < min || choice > max) {
            Console.Write(": ");
            input = Console.ReadLine();
        }

        return choice;
    }
    
    /// <summary>
    /// Gets a string from the user
    /// </summary>
    /// <param name="message">Message to display</param>
    /// <returns>Users entered string</returns>
    public static string GetString(string message) {
        string? input = null;
        
        Console.Write(message);

        while (input == null) {
            Console.Write(": ");
            input = Console.ReadLine();
        }

        return input;
    }
    
    /// <summary>
    /// Asks the user a yes or no question
    /// </summary>
    /// <param name="message">Message to display</param>
    /// <returns>True if y, false if n</returns>
    public static bool YesNo(string message) {
        string? input = null;
        
        Console.Write(message + " ");
        
        // While the input is not either y/n keep asking the user
        while (input == null || input != "y" && input != "n") {
            Console.Write("(y/n): ");
            input = Console.ReadLine();
        }

        return input == "y";
    }
    
    /// <summary>
    /// Waits for the user to press enter
    /// </summary>
    public static void WaitForEnter() {
        Console.Write("Press enter to continue");
        Console.ReadLine();
    }
}
