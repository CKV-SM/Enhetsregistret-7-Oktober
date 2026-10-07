namespace Enhetsregistret.Models;

public abstract class Enhet
{
    public const decimal MaxPris = 1_000_000_000m;

    public int Id { get; private set; }
    private string _namn { get; set; }
    public string Namn { 
        get {
            return _namn;
        }
        set {
            if (string.IsNullOrEmpty(value))
            {
                throw new MissingFieldException("Du kan inte sätta ett tomt namn. Vänligen ange ett namn.");
            }
            else _namn = value;
        }
    }
    private decimal _pris { get; set; }
    public decimal Pris { 
        get { 
            return _pris;
        }
        set {
            if (value < 0 || value > MaxPris)
                throw new ArgumentOutOfRangeException(nameof(value), "Ogiltigt pris");
            else _pris = value;
        } 
    }
    public string Status { get; set; }
    public Enhet(int id, string namn, decimal pris)
    {
        if (string.IsNullOrWhiteSpace(namn))
            throw new ArgumentException("Enheten måste ha ett namn.", nameof(namn));

        Namn = namn.Trim();
        Pris = pris;
        Status = "Aktiv";
        Id = id;
    }

    public void AndraPris(decimal nyttPris)
    {
        if (nyttPris < 0 || nyttPris > MaxPris)
            throw new ArgumentOutOfRangeException(nameof(nyttPris));

        Pris = nyttPris;
    }

    public void AndraStatus(string nyStatus)
    {
        if (nyStatus != "Aktiv" && nyStatus != "Inaktiv" && nyStatus != "Service")
            throw new ArgumentException("Ogiltig status.", nameof(nyStatus));

        Status = nyStatus;
    }

    public virtual string Beskrivning()
    {
        return $"{Namn} – {Status}";
    }
}
