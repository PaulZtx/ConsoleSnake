namespace SnakeGame;

/// <summary>
/// Игровая змейка
/// </summary>
public class Snake
{
    private const char Symbol = '*';
    private readonly Queue<Point> _snake;

    // private Point Head => _snake.Dequeue();

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="x">Координата по X</param>
    /// <param name="y">Координата по Y</param>
    /// <param name="length">Длина змейки</param>
    public Snake(int x, int y, int length = 3)
    {
        _snake = new Queue<Point>();
        
        for (int i = x - length; i < x; ++i)
        {
            Point p = (i, y, Symbol);
            _snake.Enqueue(p);
        }
    }

    /// <summary>
    /// Возвращаем все точки для отрисовки
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Point> GetPoints()
        => _snake.ToList();

    /// <summary>
    /// Обработка движения
    /// </summary>
    /// <param name="direction"></param>
    public void Move(Direction direction)
    {
        var head = _snake.Last();
        switch (direction)
        {
            case Direction.Right:
                head.X += 1;
                break;
            
            case Direction.Left:
                head.X -= 1;
                break;
            
            case Direction.Up:
                head.Y -= 1;
                break;
            
            case Direction.Down:
                head.Y += 1;
                break;
        }

        ChangeSnake(head);
    }

    private void ChangeSnake(Point point)
    {
        _snake.Dequeue();
        _snake.Enqueue(point);
    }
}