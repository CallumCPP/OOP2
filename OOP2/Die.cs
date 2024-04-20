namespace OOP2;

/// <summary>
/// Represents a die
/// </summary>
public class Die {
    public int Value { get; private set; }
    private static readonly Random _random = new();
    
    /// <summary>
    /// Rolls the die
    /// </summary>
    /// <returns>Result of the roll</returns>
    public int Roll() {
        // Random number between 1 and 6
        Value = _random.Next(1, 7);
        return Value;
    }
}
