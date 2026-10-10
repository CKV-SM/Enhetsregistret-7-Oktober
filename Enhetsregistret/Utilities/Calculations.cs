using System;
using Enhetsregistret.Models;

namespace Enhetsregistret.Utilities;

public static class Calculations
{
    public static decimal CalculateRebatePrice(decimal price, decimal rebatePercent)
    {
        ControlPrice(price);
        if (rebatePercent < 0 || rebatePercent > 100)
            throw new ArgumentOutOfRangeException(nameof(rebatePercent));

        return price - price * (rebatePercent / 100m);
    }

    public static decimal CalculateSalestax(decimal pris)
    {
        ControlPrice(pris);
        return pris * 0.25m;
    }

    public static decimal CalculatePriceIncludingTax(decimal pris)
    {
        return pris + CalculateSalestax(pris);
    }

    private static void ControlPrice(decimal pris)
    {
        if (pris < 0 || pris > Equipment.MaxPrice)
            throw new ArgumentOutOfRangeException(nameof(pris));
    }
}
