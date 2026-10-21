using Enhetsregistret.Models;
using Enhetsregistret.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enhetsregistret.Tests;

[TestClass]
public class JsonEquipmentServiceTests
{
    private string testDirectory = "";
    private string filePath = string.Empty;

    [TestInitialize]
    public void SetUp() {
        testDirectory = Path.Combine(
            Path.GetTempPath(),
            "EnhetsRegisterTests-" + Guid.NewGuid().ToString("N")
            );
        Directory.CreateDirectory(testDirectory);

        filePath = Path.Combine(testDirectory, "equipment.json");

    }

    [TestCleanup]
    public void CleanUp() {
    
        if (Directory.Exists(testDirectory))
        {
            Directory.Delete(testDirectory, recursive: true);
        }
    }


    [TestMethod]
    public void AddedEquipment_CanBeRead_ByNewRepository() {

        // ARRANGE
        IEquipmentRepository repository = new JsonEquipmentRepository(filePath);
        var equipment = new Cellphone(1, "Sony", 10000m, 16);

        // ACT
        repository.Add(equipment);
        var reader = new JsonEquipmentRepository(filePath);
        var saved = reader.GetAll();

        // ASSERT
        Assert.HasCount(1, saved);
        Assert.AreEqual(1, saved[0].Id);
        Assert.AreEqual("Sony", saved[0].Name);
        Assert.AreEqual(10000m, saved[0].Price);
    }

    [TestMethod]
    public void ReplaceAll_Should_Remove_Old_Equipment() {
        // ARRANGE
        IEquipmentRepository repository = new JsonEquipmentRepository(filePath);
        var equipment = new Cellphone(1, "Sony", 10000m, 16);
        var equipment2 = new Laptop(2, "Lenovo", 10000m, 16);
        var toReplace = new Projector(3, "Testprojektor", 16000, 3000);

        // ACT
        repository.Add(equipment);
        repository.Add(equipment2);
        repository.ReplaceAll([toReplace]);

        var items = repository.GetAll();

        // ASSERT
        Assert.HasCount(1, items);
    }
}
