using Simulator.Maps;

namespace Simulator;

internal class Program
{
    static void Lab5a()
    {
        Console.WriteLine("Testowanie tworzenia prostokątów:");

        try
        {
            // Test 1: Tworzenie prostokąta z współrzędnych które są w złej kolejności
            Rectangle rect1 = new Rectangle(10, 5, 5, 10);
            Console.WriteLine($"Rect1: {rect1}"); // Oczekiwane: (5, 5):(10, 10)

            // Test 2: Tworzenie prostokąta z użyciem punktów, współrzędne w złej kolejności
            Point p1 = new Point(20, 25);
            Point p2 = new Point(15, 30);
            Rectangle rect2 = new Rectangle(p1, p2);
            Console.WriteLine($"Rect2: {rect2}"); // Oczekiwane: (15, 25):(20, 30)

            // Test 3: Próba utworzenia prostokąta z punktów współliniowych
            Rectangle rect3 = new Rectangle(10, 10, 10, 20); // Współliniowe współrzędne
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Wyjątek: {ex.Message}");
        }

        // Testowanie zawierania punktów
        Rectangle rect4 = new Rectangle(0, 0, 10, 10);
        Point insidePoint = new Point(5, 5);
        Point outsidePoint = new Point(15, 5);

        Console.WriteLine($"Czy prostokąt zawiera punkt (5, 5): {rect4.Contains(insidePoint)}"); // Oczekiwane: true
        Console.WriteLine($"Czy prostokąt zawiera punkt (15, 5): {rect4.Contains(outsidePoint)}"); // Oczekiwane: false

        // Testowanie struktury Point i jej metod
        Point start = new Point(10, 25);
        Console.WriteLine($"Punkt startowy: {start}"); // Oczekiwane: (10, 25)

        // Test metody Next
        Console.WriteLine($"Next (Right): {start.Next(Direction.Right)}");    // Oczekiwane: (11, 25)
        Console.WriteLine($"Next (Up): {start.Next(Direction.Up)}");          // Oczekiwane: (10, 26)
        Console.WriteLine($"Next (Left): {start.Next(Direction.Left)}");      // Oczekiwane: (9, 25)
        Console.WriteLine($"Next (Down): {start.Next(Direction.Down)}");      // Oczekiwane: (10, 24)

        // Test metody NextDiagonal
        Console.WriteLine($"NextDiagonal (Right): {start.NextDiagonal(Direction.Right)}");   // Oczekiwane: (11, 24)
        Console.WriteLine($"NextDiagonal (Up): {start.NextDiagonal(Direction.Up)}");         // Oczekiwane: (11, 26)
        Console.WriteLine($"NextDiagonal (Left): {start.NextDiagonal(Direction.Left)}");     // Oczekiwane: (9, 26)
        Console.WriteLine($"NextDiagonal (Down): {start.NextDiagonal(Direction.Down)}");     // Oczekiwane: (9, 24)
    }
    static void Lab5b()
    {
        Console.WriteLine("Testowanie klasy SmallSquareMap:");

        // Test 1: Tworzenie mapy z poprawnym rozmiarem
        SmallSquareMap map = new SmallSquareMap(10);
        Console.WriteLine($"Mapa o rozmiarze: {map.Size}"); // Oczekiwane: 10

        // Test 2: Próba stworzenia mapy z niepoprawnym rozmiarem
        try
        {
            SmallSquareMap invalidMap = new SmallSquareMap(25);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Złapano wyjątek: {ex.Message}"); // Oczekiwane: błąd o niepoprawnym rozmiarze
        }

        // Test 3: Sprawdzanie, czy punkt istnieje na mapie
        Point insidePoint = new Point(5, 5);
        Point outsidePoint = new Point(15, 5);
        Console.WriteLine($"Czy punkt (5, 5) istnieje na mapie: {map.Exist(insidePoint)}"); // Oczekiwane: true
        Console.WriteLine($"Czy punkt (15, 5) istnieje na mapie: {map.Exist(outsidePoint)}"); // Oczekiwane: false

        // Test 4: Poruszanie się po mapie w różnych kierunkach
        Point start = new Point(0, 0);

        // Próba ruchu poza granice
        Console.WriteLine($"Next (Left) z (0,0): {map.Next(start, Direction.Left)}");   // Oczekiwane: (0, 0)
        Console.WriteLine($"Next (Down) z (0,0): {map.Next(start, Direction.Down)}");   // Oczekiwane: (0, 0)

        // Próba ruchu wewnątrz granic
        Console.WriteLine($"Next (Right) z (0,0): {map.Next(start, Direction.Right)}"); // Oczekiwane: (1, 0)
        Console.WriteLine($"Next (Up) z (0,0): {map.Next(start, Direction.Up)}");       // Oczekiwane: (0, 1)

        // Próba ruchu diagonalnego
        Point nearBorder = new Point(9, 9);
        Console.WriteLine($"NextDiagonal (Up) z (9,9): {map.NextDiagonal(nearBorder, Direction.Up)}");    // Oczekiwane: (9, 9) - poza granicą
        Console.WriteLine($"NextDiagonal (Left) z (0,0): {map.NextDiagonal(start, Direction.Left)}");     // Oczekiwane: (0, 0) - poza granicą
        Console.WriteLine($"NextDiagonal (Up) z (0,0): {map.NextDiagonal(start, Direction.Up)}");     // Oczekiwane: (1, 1) - w granicy
        Console.WriteLine($"NextDiagonal (Down) z (9,9): {map.NextDiagonal(nearBorder, Direction.Down)}");    // Oczekiwane: (8, 8) - w granicy
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Starting Simulator!\n");
        Lab5a();
        Console.Write("\n\n");
        Lab5b();
    }
}
