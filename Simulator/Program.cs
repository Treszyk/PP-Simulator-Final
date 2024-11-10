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

    static void Main(string[] args)
    {
        Console.WriteLine("Starting Simulator!\n");
        Lab5a();
    }

}
