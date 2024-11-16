using Simulator;
namespace TestSimulator;

public class ValidatorTests
{
    // Testowanie metody Limiter
    [Theory]
    [InlineData(10, 5, 15, 10)] // Wartość w przedziale
    [InlineData(20, 5, 15, 15)] // Wartość powyżej max
    [InlineData(0, 5, 15, 5)]   // Wartość poniżej min
    public void Limiter_ShouldReturnCorrectValue(int value, int min, int max, int expected)
    {
        // Act
        var result = Validator.Limiter(value, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    // Testowanie metody Shortener
    [Theory]
    // Testy zbyt długie ciągi (usuwanie nadmiaru, usuwanie białych znaków, dodanie placeholderów)
    [InlineData("d    h", 3, 5, '#', "D##")] // Zbyt długi ciąg, z białymi znakami i placeholderami
    [InlineData("This is a long string that will be shortened", 5, 25, '#', "This is a long string tha")]
    // Testy zbyt krótkie ciągi (dodawanie placeholderów)
    [InlineData("Short", 8, 15, '*', "Short***")]
    // Testy poprawnego ciągu (zmiana pierwszej litery na wielką)
    [InlineData("hello", 3, 10, '#', "Hello")]
    // Testy, które są w obrębie zakresu i nie wymagają żadnych zmian
    [InlineData("Valid", 3, 10, '-', "Valid")]
    // Testowanie przypadku, gdzie tekst ma mniej niż min
    [InlineData("Fine", 5, 10, '@', "Fine@")]
    [InlineData("Hi", 5, 10, '.', "Hi...")]
    // Testowanie przypadków, gdzie ciąg nie wymaga żadnych zmian
    [InlineData("Okay", 3, 10, '*', "Okay")]
    [InlineData("Fine", 4, 10, '@', "Fine")]
    public void Shortener_ShouldReturnCorrectString(string value, int min, int max, char placeholder, string expected)
    {
        // Act
        var result = Validator.Shortener(value, min, max, placeholder);

        // Assert
        Assert.Equal(expected, result);
    }
}
