
Console.Write("Enter patient weight (kg): ");

string? raw = Console.ReadLine();

try
{
    double weight = double.Parse(raw!);   // throws if not a number

    if (weight <= 0)
        throw new ArgumentException("Weight must be positive.");

    Console.WriteLine($"Recorded weight: {weight} kg");
}
catch (FormatException)
{
    Console.WriteLine("Input was not a valid number.");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Invalid value: {ex.Message}");
}
finally
{
    Console.WriteLine("Weight entry step complete.");
}
