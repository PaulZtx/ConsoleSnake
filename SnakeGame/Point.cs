namespace SnakeGame;

/// <summary>
/// Структура точки на поле
/// </summary>
public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public char Symbol { get; init; }

    public static implicit operator Point((int, int, char) value) =>
        new Point() { X = value.Item1, Y = value.Item2, Symbol = value.Item3 };
    
    public static bool operator ==(Point a, Point b) =>
        (a.X == b.X && a.Y == b.Y);
    
    public static bool operator !=(Point a, Point b) =>
        (a.X != b.X || a.Y != b.Y);
    
}