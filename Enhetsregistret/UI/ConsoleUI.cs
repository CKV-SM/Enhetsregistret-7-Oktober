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

    private void LaggTillEnhet()
    {
        string namn;
        do
        {
            Console.Write("Namn: ");
            namn = LasaRad().Trim();
            if (namn.Length == 0)
                Console.WriteLine("Ange ett namn.");
        } while (namn.Length == 0);

        decimal pris = LasaDecimal("Pris exklusive moms: ", 0m, Enhet.MaxPris);
        //        Enhet enhet = new Enhet(namn, pris);
        //        _repository.LaggTill(enhet);
        Console.WriteLine("Enheten har lagts till med status Aktiv.");
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

    public void ShowErrorMessage()
    {
        Console.WriteLine($"Ett fel uppstod: {e.Message}");
        Console.ReadLine();
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
}
