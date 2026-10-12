using System;
using System.Collections.Generic;
using Enhetsregistret.Models;

namespace Enhetsregistret.Repositories;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly List<Equipment> _enheter = new List<Equipment>();

    public void Add(Equipment equipment)
    {
        ArgumentNullException.ThrowIfNull(equipment);
        _enheter.Add(equipment);
    }

    public List<Equipment> GetAll()
    {
        return new List<Equipment>(_enheter);
    }

    public void ReplaceAll(Equipment[] enheter)
    {
        _enheter.AddRange(enheter);
    }

    public void Update(Equipment equipment)
    {
        throw new NotImplementedException();
    }
}
