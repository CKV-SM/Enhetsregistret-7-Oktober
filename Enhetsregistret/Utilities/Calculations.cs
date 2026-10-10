using Enhetsregistret.Models;

namespace Enhetsregistret.Utilities;

public static class Calculations
{
    public static decimal CalculateWithRebate(decimal price, decimal rebatePercent)
    {
        CheckPrice(price);
        if (rebatePercent < 0 || rebatePercent > 100)
            throw new ArgumentOutOfRangeException(nameof(rebatePercent));

        return price - price * (rebatePercent / 100m);
    }

    public static decimal CalculateTax(decimal pris)
    {
        CheckPrice(pris);
        return pris * 0.25m;
    }

    public static decimal CalculatePriceIncludingTax(decimal pris)
    {
        return pris + CalculateTax(pris);
    }

    private static void CheckPrice(decimal pris)
    {
        if (pris < 0 || pris > Equipment.MaxPrice)
            throw new ArgumentOutOfRangeException(nameof(pris));
    }
}
