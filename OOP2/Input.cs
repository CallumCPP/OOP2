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
        string input = "a";
        int choice;

        // While the input is not a valid integer or is not in range keep asking the user
        while (!int.TryParse(input, out choice) || choice < min || choice > max) {
            Console.Write(message + ": ");
            input = Console.ReadLine()!; // During regular use of this application this should never be null
        }

        return choice;
    }
    
    /// <summary>
    /// Asks the user a yes or no question
    /// </summary>
    /// <param name="message">Message to display</param>
    /// <returns>True if y, false if n</returns>
    public static bool YesNo(string message) {
        string input = "a";
        // While the input is not either y/n keep asking the user
        while (input != "y" && input != "n") {
            Console.Write(message + " (y/n): ");
            input = Console.ReadLine()!;
        }

        return input == "y";
    }
}
