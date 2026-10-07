namespace Enhetsregistret.Models;

public class Laptop : Enhet, ILaddningsbar
{
    public double SkärmStorlek { get; set; }
    public double LaddningNiva { get; set; }

    public Laptop(int id, string namn, decimal pris, double skärmStorlek) : base(id, namn, pris)
    {
        SkärmStorlek = skärmStorlek;
    }

    public override string Beskrivning()
    {
        return $"{Namn} – {Status} - Skärmstorlek (Tum): {SkärmStorlek}";
    }

    public bool Ladda()
    {
        Console.WriteLine("Jag är en bärbar dator och jag laddar just nu...");
        LaddningNiva = 100;
        return true;
    }
}
