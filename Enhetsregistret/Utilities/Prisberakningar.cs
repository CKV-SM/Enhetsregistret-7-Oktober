using System;
using Enhetsregistret.Models;

namespace Enhetsregistret.Utilities;

public static class Prisberakningar
{
    public static decimal BeraknaRabatteratPris(decimal pris, decimal rabattProcent)
    {
        KontrolleraPris(pris);
        if (rabattProcent < 0 || rabattProcent > 100)
            throw new ArgumentOutOfRangeException(nameof(rabattProcent));

        return pris - pris * (rabattProcent / 100m);
    }

    public static decimal BeraknaMomsbelopp(decimal pris)
    {
        KontrolleraPris(pris);
        return pris * 0.25m;
    }

    public static decimal BeraknaPrisMedMoms(decimal pris)
    {
        return pris + BeraknaMomsbelopp(pris);
    }

    private static void KontrolleraPris(decimal pris)
    {
        if (pris < 0 || pris > Enhet.MaxPris)
            throw new ArgumentOutOfRangeException(nameof(pris));
    }
}
