using Enhetsregistret.Models;
using Enhetsregistret.Repositories;
using System;
namespace Enhetsregistret.Services;

public class EquipmentService : IEquipmentService
{
    private readonly IEquipmentRepository repo;

    public EquipmentService(IEquipmentRepository repo)
    {
        this.repo = repo;
    }

    public Equipment[] GetAll()
    {
        return repo.GetAll().ToArray();
    }

    public Equipment? GetById(int id)
    {
        var allUnits = repo.GetAll();
        var returnUnit = allUnits
            .FirstOrDefault(searchUnit => searchUnit.Id == id);
        return returnUnit;
    }

    public void Add(Equipment equipment)
    {
        repo.Add(equipment);
    }

    public void SeedData()
    {
        var equipment = repo.GetAll().LastOrDefault();
        if (equipment is not null)
            return;
        
        var lastId = equipment?.Id ?? 1;

        var newEquipment = new Equipment[]{
                new Laptop(lastId++, "Testenhet", 20000, 15.2),
                new Laptop(lastId++, "Sony Vaio X-1", 15000, 14.7),
                new Cellphone(lastId++, "Samsung", 12000, 16),
                new Projector(lastId++, "Testprojekt", 25000, 3000)
            };
        repo.ReplaceAll(newEquipment);
    }

    public List<Equipment> SearchByName(string name)
    {
        var allUnits = repo.GetAll();
        if (allUnits.Count == 0) return [];

        return allUnits
            .Where(units => units.Name.Contains(name))
            .ToList();
    }
}
