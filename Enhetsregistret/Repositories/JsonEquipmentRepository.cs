using Enhetsregistret.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Enhetsregistret.Repositories;

public class JsonEquipmentRepository : IEquipmentRepository
{
    private readonly string _filePath;

    public JsonEquipmentRepository(string filePath)
    {
        _filePath = filePath;
    }

    public void Add(Equipment equipment)
    {
        var currentItems = GetAll();
        currentItems.Add(equipment);
        var itemsAsJson = JsonSerializer.Serialize(currentItems);
        File.WriteAllText(_filePath, itemsAsJson);
    }

    public List<Equipment> GetAll()
    {
        try
        {
            var json = File.ReadAllText(_filePath);
            var equipmentList = JsonSerializer.Deserialize<List<Equipment>>(json);
            if (equipmentList is null)
            {
                throw new Exception("Den här listan innehåller fel värden. Vänligen kontrollera.");
            }
            else return equipmentList;
        }
        catch
        {
            return [];
        }
    }

    public void ReplaceAll(Equipment[] enheter)
    {
        var currentItems = GetAll();
        currentItems.Clear();
        currentItems.AddRange(enheter);
        var itemsAsJson = JsonSerializer.Serialize(currentItems);
        File.WriteAllText(_filePath, itemsAsJson);
    }

    public void Update(Equipment newState)
    {
        var currentItems = GetAll();
        var itemToChangeID = newState.Id;
        var indexInList = currentItems.FindIndex(i => i.Id == itemToChangeID);
        currentItems[indexInList] = newState;
        var itemsAsJson = JsonSerializer.Serialize(currentItems);
        File.WriteAllText(_filePath, itemsAsJson);
    }
}
