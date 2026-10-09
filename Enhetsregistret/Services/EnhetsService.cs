using Enhetsregistret.Models;
using Enhetsregistret.Repositories;
using System;
namespace Enhetsregistret.Services;

public class EnhetsService : IEnhetsService
{
    private readonly IEnhetsRepository repo;

    public EnhetsService(IEnhetsRepository repo)
    {
        this.repo = repo;
    }

    public Enhet[] GetAll()
    {
        return repo.HamtaAlla().ToArray();
    }

    public Enhet? GetById(int id)
    {
        var allUnits = repo.HamtaAlla();
        var returnUnit = allUnits
            .FirstOrDefault(searchUnit => searchUnit.Id == id);
        return returnUnit;
    }

    public void Add(Enhet enhet)
    {
        repo.LaggTill(enhet);
    }

    public void SeedData()
    {
        var equipment = repo.HamtaAlla().LastOrDefault();
        if (equipment is not null)
            return;
        
        var lastId = equipment?.Id ?? 1;

        var newEquipment = new Enhet[]{
                new Laptop(lastId++, "Testenhet", 20000, 15.2),
                new Laptop(lastId++, "Sony Vaio X-1", 15000, 14.7),
                new Mobil(lastId++, "Samsung", 12000, 16),
                new Projektor(lastId++, "Testprojekt", 25000, 3000)
            };
        repo.LaggTillAlla(newEquipment);
    }

    public List<Enhet> SearchByName(string name)
    {
        var allUnits = repo.HamtaAlla();
        if (allUnits is null)
            return [];

        var filteredEnheter = allUnits
            .Where(enhet =>
            {
                return enhet.Namn.Contains(name);
            })
            .ToList();

        return filteredEnheter;
    }
}
