namespace Enhetsregistret.Models;

public class Laptop : Equipment, IChargeable
{
    public double ScreenSize { get; set; }
    public double ChargeLevel { get; set; }
    public Laptop(int id, string name, decimal price, double screenSize) : base(id, name, price)
    {
        ScreenSize = screenSize;
    }

    public override string Description()
    {
        return $"{Name} – {Status} - Screensize (Inches): {ScreenSize}";
    }

    public bool Charge()
    {
        Console.WriteLine("I am a laptop and I am currently charging...");
        ChargeLevel = 100;
        return true;
    }
}
