using Enhetsregistret.Services;
using Enhetsregistret.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enhetsregistret.UI
{
    public class LoanMenu
    {
        private readonly ILoanService loanService;

        public LoanMenu(ILoanService loanService)
        {
            this.loanService = loanService;
        }

        public void ShowMenu()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("\nUtlåningar");
                        Console.WriteLine("1. Visa alla utlånade enheter");
                        Console.WriteLine("2. Låna ut enhet");
                        Console.WriteLine("3. Gå tillbaka");

                        switch (MenuHelper.ReadInteger("Välj alternativ: ", 1, 3))
                        {
                            case 1: ShowLoanedEquipment(); break;
                            case 2: LoanEquipment(); break;
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

        private void LoanEquipment()
        {
            Console.WriteLine("Vänligen skriv in ett enhets-id");
            bool activeLoop = true;
            while(activeLoop)
            {
                try
                {
                    var success = int.TryParse(Console.ReadLine(), out var value);
                    if (!success){
                            Console.WriteLine("Vänligen skriv in ett giltigt värde");
                    }
                    loanService.Loan(value);
                    Console.WriteLine("Utlåning gick bra.");
                    activeLoop = false;
                } catch(Exception e)
                {
                    Console.WriteLine("Utlåning gick inte :(");
                    Console.WriteLine(e.Message);
                    activeLoop = false;
                }
            }
        }

        private void ShowLoanedEquipment()
        {
            var loanedItems = loanService.GetLoanedEquipment();
            if(loanedItems.Count == 0)
            {
                Console.WriteLine("Det finns inga utlånade föremål");
            } else
            {
                foreach(var item in loanedItems)
                {
                    Console.WriteLine($"Namn: {item.EquipmentName}. Utlåningsdatum: {item.BorrowDate.ToShortDateString()}");
                }
            }
        }
    }
}
