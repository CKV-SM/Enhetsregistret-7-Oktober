using System;
using System.Collections.Generic;
using Enhetsregistret.Models;

namespace Enhetsregistret.Repositories;

public class EnhetsRepository : IEnhetsRepository
{
    private readonly List<Enhet> _enheter = new List<Enhet>();

    public void LaggTill(Enhet enhet)
    {
        ArgumentNullException.ThrowIfNull(enhet);
        _enheter.Add(enhet);
    }

    public List<Enhet> HamtaAlla()
    {
        return new List<Enhet>(_enheter);
    }

    public void LaggTillAlla(Enhet[] enheter)
    {
        _enheter.AddRange(enheter);
    }
}
