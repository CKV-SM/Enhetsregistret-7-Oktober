using System;
using System.Collections.Generic;
using System.Text;

namespace Enhetsregistret.Utilities;

public static class MenuHelper
{
    public static void Pause()
    {
        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }


    public static int ReadInteger(string fraga, int min, int max)
    {
        while (true)
        {
            Console.Write(fraga);
            if (int.TryParse(ReadLine(), out int tal) && tal >= min && tal <= max)
                return tal;
            Console.WriteLine($"Ange ett heltal mellan {min} och {max}.");
        }
    }

    public static decimal ReadDecimal(string fraga, decimal min, decimal max)
    {
        while (true)
        {
            Console.Write(fraga);
            if (decimal.TryParse(ReadLine(),
                System.Globalization.NumberStyles.AllowLeadingWhite |
                System.Globalization.NumberStyles.AllowTrailingWhite |
                System.Globalization.NumberStyles.AllowLeadingSign |
                System.Globalization.NumberStyles.AllowDecimalPoint,
                System.Globalization.CultureInfo.CurrentCulture, out decimal tal)
                && tal >= min && tal <= max)
                return tal;
            Console.WriteLine($"Ange ett tal mellan {min} och {max}. Använd komma för decimaler.");
        }
    }

    public static string ReadLine()
    {
        string? rad = Console.ReadLine();
        if (rad == null) throw new EndOfStreamException();
        return rad;
    }
}
