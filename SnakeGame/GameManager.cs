namespace SnakeGame;

/// <summary>
/// Менеджер игры
/// </summary>
public class GameManager
{
    private Snake _snake;
    private Point? _food;
    private Random _random;
    private Direction _direction;
    private int _width;
    private int _height;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="height"></param>
    public GameManager(int height)
    {
        _random = new Random();
        _direction = Direction.Right;
        _height = height;
        _width = (int)(height * 2.5);
        _snake = new Snake(_width / 2, _height / 2);
        
        Console.WindowHeight = _height + 1;
        Console.WindowWidth = _width + 1;
    }
    
    /// <summary>
    /// Запуск игры
    /// </summary>
    public void StartGame()
    {
        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow when _direction is Direction.Left or Direction.Right:
                        _direction = Direction.Up;
                        break;
                    case ConsoleKey.DownArrow when _direction is Direction.Left or Direction.Right:
                        _direction = Direction.Down;
                        break;
                    case ConsoleKey.LeftArrow when _direction is Direction.Down or Direction.Up:
                        _direction = Direction.Left;
                        break;
                    case ConsoleKey.RightArrow when _direction is Direction.Down or Direction.Up:
                        _direction = Direction.Right;
                        break;
                }
            }

            DrawFiled();
            DrawPoints(_snake.GetPoints());
            _snake.Move(_direction);
            SpawnFood();
            
            Thread.Sleep(_direction is Direction.Down or Direction.Up ? 100 : 40);
        }
    }
    
    /// <summary>
    /// Отрисовка поля
    /// </summary>
    private void DrawFiled()
    {   
        Console.SetCursorPosition(0, 0);
        Console.Write('|');
        for (int i = 0; i < _width - 2; ++i)
        {
            Console.Write('=');
        }
        Console.WriteLine('|');
    
        for (int i = 1; i < _height - 1; ++i)
        {
            Console.Write('|');
            for (int j = 1; j < _width - 1; ++j)
            {
                Console.Write(' ');
            }
            Console.WriteLine('|');
        }
    
        Console.SetCursorPosition(0, _height - 1);
        Console.Write('|');
        for (int i = 0; i < _width - 2; ++i)
        {
            Console.Write('=');
        }
        Console.WriteLine('|');
    }

    /// <summary>
    /// Отрисовка точек на поле
    /// </summary>
    /// <param name="point">Точка для отрисовки на поле</param>
    private void DrawPoint(Point point)
    {
        CheckPosition(point);
        Console.SetCursorPosition(point.X, point.Y);
        Console.Write(point.Symbol);
        Console.SetCursorPosition(0, _height);
    }

    /// <summary>
    /// Отрисовка точек на поле
    /// </summary>
    /// <param name="points">Набор точек для отрисовки</param>
    private void DrawPoints(IEnumerable<Point> points)
    {
        foreach (var point in points)
        {
            DrawPoint(point);
        }
    }

    /// <summary>
    /// Проверка позиции головы на поле
    /// </summary>
    /// <param name="point"></param>
    private void CheckPosition(Point point)
    {
        if (point.X >= _width || point.Y >= _height || point.X <= 0 || point.Y <= 0)
        {
            Console.Clear();
            Console.WriteLine("Game Over...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }

    /// <summary>
    /// Отрисовка еды
    /// </summary>
    private void SpawnFood()
    {
        if (_food == _snake.GetHead)
        {
            _food = null;
            _snake.Eat();
        }
        if (_food is not null)
        {
            DrawPoint(_food.Value);
            return;
        }
        
        _food = (_random.Next() % (_width - 2) + 1, _random.Next() % (_height - 2) + 1, '@');
        DrawPoint(_food.Value);
    }
}