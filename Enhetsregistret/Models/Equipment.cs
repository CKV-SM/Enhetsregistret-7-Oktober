using System.Text.Json.Serialization;

namespace Enhetsregistret.Models;


[JsonPolymorphic]
[JsonDerivedType(typeof(Laptop), "laptop")]
[JsonDerivedType(typeof(Cellphone), "cellphone")]
[JsonDerivedType(typeof(Projector), "projector")]
public abstract class Equipment
{
    public const decimal MaxPrice = 1_000_000_000m;
    public int Id { get; private set; }
    private string _name { get; set; }
    public string Name { 
        get {
            return _name;
        }
        set {
            if (string.IsNullOrEmpty(value))
            {
                throw new MissingFieldException("You can't set a empty name. Please provide a name.");
            }
            else _name = value;
        }
    }
    private decimal _price { get; set; }
    public decimal Price { 
        get { 
            return _price;
        }
        set {
            if (value < 0 || value > MaxPrice)
                throw new ArgumentOutOfRangeException(nameof(value), "Invalid price. Please provide a valid price.");
            else _price = value;
        } 
    }
    public string Status { get; set; }
    public Equipment(int id, string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("The unit must have a name.", nameof(name));

        Name = name.Trim();
        Price = price;
        Status = "Active";
        Id = id;
    }
    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0 || newPrice > MaxPrice)
            throw new ArgumentOutOfRangeException(nameof(newPrice));

        Price = newPrice;
    }

    public void UpdateStatus(string newStatus)
    {
        if (newStatus != "Active" && newStatus != "Inactive" && newStatus != "Service")
            throw new ArgumentException("Invalid status", nameof(newStatus));

        Status = newStatus;
    }

    public virtual string Description()
    {
        return $"{Name} – {Status}";
    }
}
