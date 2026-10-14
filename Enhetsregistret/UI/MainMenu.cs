using Enhetsregistret.Models;
using Enhetsregistret.Services;
using Enhetsregistret.Utilities;

namespace Enhetsregistret.UI;

public class MainMenu
{
    private readonly EquipmentMenu equipmentMenu;
    private readonly LoanMenu loanMenu;

    public MainMenu(EquipmentMenu equipmentMenu, LoanMenu loanMenu)
    {
        this.equipmentMenu = equipmentMenu;
        this.loanMenu = loanMenu;
    }

    public void Run()
    {
        try
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("\nENHETSREGISTRET");
                    Console.WriteLine("1. Hantera utrustning");
                    Console.WriteLine("2. Hantera utlåningar");
                    Console.WriteLine("3. Avsluta");

                    switch (MenuHelper.ReadInteger("Välj alternativ: ", 1, 3))
                    {
                        case 1: ShowEquipmentMenu(); break;
                        case 2: ShowLoanMenu(); break;
                        case 3: return;
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

    private void ShowEquipmentMenu()
    {
        Console.Clear();
        equipmentMenu.ShowMenu();
    }

    private void ShowLoanMenu()
    {
        Console.Clear();
        loanMenu.ShowMenu();
    }
}
