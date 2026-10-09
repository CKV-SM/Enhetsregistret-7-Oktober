using Enhetsregistret.Models;
using Enhetsregistret.Services;
using Enhetsregistret.Utilities;
using System.Linq.Expressions;

namespace Enhetsregistret.UI;

public class ConsoleUI
{
    private readonly IEnhetsService _service;

    public ConsoleUI(IEnhetsService service)
    {
        ArgumentNullException.ThrowIfNull(service);
        _service = service;
    }

    public void Kor()
    {
        try
        {
            bool korProgrammet = true;
            while (korProgrammet)
            {
                try
                {

                    Console.WriteLine("\nENHETSREGISTRET");
                    Console.WriteLine("1. Visa alla enheter");
                    Console.WriteLine("2. Lägg till enhet");
                    Console.WriteLine("3. Ändra pris");
                    Console.WriteLine("4. Förhandsvisa rabatt");
                    Console.WriteLine("5. Ändra status");
                    Console.WriteLine("6. Avsluta");

                    switch (LasaHeltal("Välj alternativ: ", 1, 6))
                    {
                        case 1: VisaAlla(); break;
                        case 2: LaggTillEnhet(); break;
                        case 3: AndraPris(); break;
                        case 4: ForhandsvisaRabatt(); break;
                        case 5: AndraStatus(); break;
                        case 6: korProgrammet = false; break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ett fel uppstod: {e.Message}");
                }
            }
        }
        catch (EndOfStreamException)
        {
            Console.WriteLine("\nInmatningen avslutades.");
        }
        Console.WriteLine("Enhetsregistret avslutas.");
    }

    private void VisaAlla()
    {
        var enheter = _service.GetAll().ToList();
        if (enheter.Count == 0)
            Console.WriteLine("Registret är tomt.");

        foreach (Enhet enhet in enheter)
        {
            Console.WriteLine($"\n{enhet.Beskrivning()}");
            Console.WriteLine($"Pris exklusive moms: {enhet.Pris:F2} kr");
            Console.WriteLine($"Pris inklusive moms: {Prisberakningar.BeraknaPrisMedMoms(enhet.Pris):F2} kr");
        }
    }
    
    private Enhet? ValjEnhet()
    {
        var enheter = _service.GetAll();

        if (enheter.Length == 0)
        {
            Console.WriteLine("Registret är tomt.");
            return null;
        }

        for (int i = 0; i < enheter.Length; i++)
            Console.WriteLine($"{i + 1}. {enheter[i].Beskrivning()}");
        Console.WriteLine("0. Avbryt");

        int val = LasaHeltal("Välj enhet: ", 0, enheter.Length);

        if (val == 0)
            return null;

        return enheter[val - 1];
    }

    private void AndraPris()
    {
        try
        {
            Enhet? enhet = ValjEnhet();
            if (enhet == null) return;

            decimal pris = LasaDecimal("Nytt pris exklusive moms: ", -1000m, Enhet.MaxPris);
            enhet.Pris = pris;
            Console.WriteLine("Priset har ändrats.");
        }
        catch (MissingFieldException e) {
            Console.WriteLine($"Du saknar ett fält: {e.Message}");
        }
        catch (ArgumentOutOfRangeException e) {
            Console.WriteLine($"Du har skrivit ett ogiltigt värde: {e.Message}");
        }
    }

    private void ForhandsvisaRabatt()
    {
        Enhet? enhet = ValjEnhet();
        if (enhet == null) return;

        decimal rabatt = LasaDecimal("Rabatt i procent: ", 0m, 100m);
        decimal rabatteratPris = Prisberakningar.BeraknaRabatteratPris(enhet.Pris, rabatt);
        Console.WriteLine($"Originalpris exklusive moms: {enhet.Pris:F2} kr");
        Console.WriteLine($"Pris efter rabatt exklusive moms: {rabatteratPris:F2} kr");
        Console.WriteLine($"Moms (25 %): {Prisberakningar.BeraknaMomsbelopp(rabatteratPris):F2} kr");
        Console.WriteLine($"Totalpris inklusive moms: {Prisberakningar.BeraknaPrisMedMoms(rabatteratPris):F2} kr");
        Console.WriteLine("Förhandsvisningen ändrar inte enhetens sparade pris.");
    }

    private void AndraStatus()
    {
        Enhet? enhet = ValjEnhet();
        if (enhet == null) return;

        Console.WriteLine("1. Aktiv\n2. Inaktiv\n3. Service\n4. Avbryt");
        int val = LasaHeltal("Välj status: ", 1, 4);
        switch (val)
        {
            case 1: enhet.AndraStatus("Aktiv"); break;
            case 2: enhet.AndraStatus("Inaktiv"); break;
            case 3: enhet.AndraStatus("Service"); break;
            case 4: return;
        }
        Console.WriteLine("Statusen har ändrats.");
    }

    private int LasaHeltal(string fraga, int min, int max)
    {
        while (true)
        {
            Console.Write(fraga);
            if (int.TryParse(LasaRad(), out int tal) && tal >= min && tal <= max)
                return tal;
            Console.WriteLine($"Ange ett heltal mellan {min} och {max}.");
        }
    }

    private decimal LasaDecimal(string fraga, decimal min, decimal max)
    {
        while (true)
        {
            Console.Write(fraga);
            if (decimal.TryParse(LasaRad(),
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

    private string LasaRad()
    {
        string? rad = Console.ReadLine();
        if (rad == null) throw new EndOfStreamException();
        return rad;
    }

    private Laptop CreateLaptop(int id, string namn, decimal pris)
    {
        Console.Write("RAM i GB: ");

        if (!int.TryParse(Console.ReadLine(), out int ramGb))
        {
            throw new ArgumentException("RAM måste vara ett heltal.");
        }

        return new Laptop(id, namn, pris, ramGb);
    }

    private Projektor CreateProjector(int id, string namn, decimal pris)
    {
        Console.Write("Ljusstyrka i lumen: ");

        if (!int.TryParse(Console.ReadLine(), out int lumen))
        {
            throw new ArgumentException("Ljusstyrka måste vara ett heltal.");
        }

        return new Projektor(id, namn, pris, lumen);
    }

    private Mobil CreateMobil(int id, string namn, decimal pris)
    {
        Console.Write("Storlek i tum: ");

        if (!double.TryParse(Console.ReadLine(), out double tum))
        {
            throw new ArgumentException("Arbetsminnet måste vara i heltal.");
        }

        return new Mobil(id, namn, pris, tum);
    }
    
    private void LaggTillEnhet()
    {
        Console.Clear();
        Console.WriteLine("=== LÄGG TILL ENHET ===\n");
        Console.WriteLine("1. Laptop");
        Console.WriteLine("2. Projektor");
        Console.WriteLine("3. Skärm");
        Console.Write("\nTyp: ");

        string? typ = Console.ReadLine();

        Console.Write("Id: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Id måste vara ett heltal.");
            Pause();
            return;
        }

        Console.Write("Namn: ");
        string namn = Console.ReadLine() ?? "";

        Console.Write("Pris: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal pris))
        {
            Console.WriteLine("Pris måste vara ett tal.");
            Pause();
            return;
        }
        
        Enhet? enhet = typ switch
        {
            "1" => CreateLaptop(id, namn, pris),
            "2" => CreateProjector(id, namn, pris),
            "3" => CreateMobil(id, namn, pris),
            _ => null
        };

        if (enhet == null)
        {
            Console.WriteLine("Ogiltig typ.");
        }
        else
        {
            _service.Add(enhet);
            Console.WriteLine($"\n{enhet.Typ} lades till.");
        }

        Pause();
    }
    
    private void Pause()
    {
        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }
}
