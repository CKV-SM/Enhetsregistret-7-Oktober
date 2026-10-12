using Enhetsregistret.Models;
using Enhetsregistret.Services;
using Enhetsregistret.Utilities;
using System.Linq.Expressions;

namespace Enhetsregistret.UI;

public class ConsoleUI
{
    private readonly IEquipmentService _service;

    public ConsoleUI(IEquipmentService service)
    {
        ArgumentNullException.ThrowIfNull(service);
        _service = service;
    }

    public void Run()
    {
        try
        {
            while (true)
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

                    switch (ReadInteger("Välj alternativ: ", 1, 6))
                    {
                        case 1: ShowAll(); break;
                        case 2: AddEquipment(); break;
                        case 3: UpdatePrice(); break;
                        case 4: PreviewRebate(); break;
                        case 5: UpdateStatus(); break;
                        case 6: return;
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

    private void ShowAll()
    {
        var items = _service.GetAll().ToList();
        if (items.Count == 0)
            Console.WriteLine("Registret är tomt.");

        foreach (Equipment item in items)
        {
            Console.WriteLine($"\n{item.Description()}");
            Console.WriteLine($"Pris exklusive moms: {item.Price:F2} kr");
            Console.WriteLine($"Pris inklusive moms: {Calculations.CalculatePriceIncludingTax(item.Price):F2} kr");
        }
    }
    
    private Equipment? PickEquipment()
    {
        var items = _service.GetAll();

        if (items.Length == 0)
        {
            Console.WriteLine("Registret är tomt.");
            return null;
        }

        for (int i = 0; i < items.Length; i++)
            Console.WriteLine($"{i + 1}. {items[i].Description()}");
        
        Console.WriteLine("0. Avbryt");

        int choice = ReadInteger("Välj enhet: ", 0, items.Length);

        if (choice == 0)
            return null;

        return items[choice - 1];
    }

    private void UpdatePrice()
    {
        try
        {
            Equipment? item = PickEquipment();
            if (item == null) return;

            decimal pris = ReadDecimal("Nytt pris exklusive moms: ", -1000m, Equipment.MaxPrice);
            item.Price = pris;
            _service.UpdateItem(item);
            Console.WriteLine("Priset har ändrats.");
        }
        catch (MissingFieldException e) {
            Console.WriteLine($"Du saknar ett fält: {e.Message}");
        }
        catch (ArgumentOutOfRangeException e) {
            Console.WriteLine($"Du har skrivit ett ogiltigt värde: {e.Message}");
        }
    }

    private void PreviewRebate()
    {
        Equipment? item = PickEquipment();
        if (item == null) return;

        decimal rebate = ReadDecimal("Rabatt i procent: ", 0m, 100m);
        decimal priceWithRebate = Calculations.CalculateWithRebate(item.Price, rebate);
        Console.WriteLine($"Originalpris exklusive moms: {item.Price:F2} kr");
        Console.WriteLine($"Pris efter rabatt exklusive moms: {priceWithRebate:F2} kr");
        Console.WriteLine($"Moms (25 %): {Calculations.CalculateTax(priceWithRebate):F2} kr");
        Console.WriteLine($"Totalpris inklusive moms: {Calculations.CalculatePriceIncludingTax(priceWithRebate):F2} kr");
        Console.WriteLine("Förhandsvisningen ändrar inte enhetens sparade pris.");
    }

    private void UpdateStatus()
    {
        Equipment? item = PickEquipment();
        if (item == null) return;

        Console.WriteLine("1. Aktiv\n2. Inaktiv\n3. Service\n4. Avbryt");
        int val = ReadInteger("Välj status: ", 1, 4);
        switch (val)
        {
            case 1: item.UpdateStatus("Aktiv"); break;
            case 2: item.UpdateStatus("Inaktiv"); break;
            case 3: item.UpdateStatus("Service"); break;
            case 4: return;
        }
        Console.WriteLine("Statusen har ändrats.");
    }

    private int ReadInteger(string fraga, int min, int max)
    {
        while (true)
        {
            Console.Write(fraga);
            if (int.TryParse(ReadLine(), out int tal) && tal >= min && tal <= max)
                return tal;
            Console.WriteLine($"Ange ett heltal mellan {min} och {max}.");
        }
    }

    private decimal ReadDecimal(string fraga, decimal min, decimal max)
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

    private string ReadLine()
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

    private Projector CreateProjector(int id, string namn, decimal pris)
    {
        Console.Write("Ljusstyrka i lumen: ");

        if (!int.TryParse(Console.ReadLine(), out int lumen))
        {
            throw new ArgumentException("Ljusstyrka måste vara ett heltal.");
        }

        return new Projector(id, namn, pris, lumen);
    }

    private Cellphone CreateMobil(int id, string namn, decimal pris)
    {
        Console.Write("Storlek i tum: ");

        if (!double.TryParse(Console.ReadLine(), out double tum))
        {
            throw new ArgumentException("Arbetsminnet måste vara i heltal.");
        }

        return new Cellphone(id, namn, pris, tum);
    }
    
    private void AddEquipment()
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
        
        Equipment? enhet = typ switch
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
            Console.WriteLine($"\nEnhet lades till.");
        }

        Pause();
    }
    
    private void Pause()
    {
        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }
}
