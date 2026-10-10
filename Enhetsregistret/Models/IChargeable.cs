namespace Enhetsregistret.Models;

public interface IChargeable
{
    double ChargeLevel { get; set; }
    bool Charge();
}