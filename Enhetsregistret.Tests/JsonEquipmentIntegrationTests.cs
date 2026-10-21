using Enhetsregistret.Models;
using Enhetsregistret.Repositories;
using Enhetsregistret.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enhetsregistret.Tests;

[TestClass]
public class JsonEquipmentIntegrationTests
{
    private string testDirectory = string.Empty;
    private string filePath = string.Empty;

    [TestInitialize]
    public void SetUp()
    {
        testDirectory = Path.Combine(
            Path.GetTempPath(),
            "EnhetsregistretTests-" + Guid.NewGuid().ToString("N")
        );
        Directory.CreateDirectory(testDirectory);
        filePath = Path.Combine(testDirectory, "equipment.json");
    }

    [TestCleanup]
    public void CleanUp()
    {
        if (Directory.Exists(testDirectory))
            Directory.Delete(testDirectory, recursive: true);
    }

    [TestMethod]
    public void AddedEquipment_CanBeRead_ByNewRepository()
    {
        // Arrange
        IEquipmentRepository repository = new JsonEquipmentRepository(filePath);
        var equipment = new Cellphone(3, "Sony", 10000m, 16);

        // Act
        repository.Add(equipment);
        var reader = new JsonEquipmentRepository(filePath);
        var saved = reader.GetAll();

        // Assert
        Assert.HasCount(1, saved);
        Assert.AreEqual(3, saved[0].Id);
        Assert.AreEqual("Sony", saved[0].Name);
        Assert.AreEqual(10000m, saved[0].Price);
        Assert.IsInstanceOfType<Cellphone>(saved[0]);
    }

    [TestMethod]
    public void ValidId_Returns_Equipment_FromJsonFile()
    {
        // Arrange
        var writer = new JsonEquipmentRepository(filePath);
        writer.Add(new Cellphone(3, "Sony", 10000m, 16));
        IEquipmentRepository repository =
            new JsonEquipmentRepository(filePath);
        IEquipmentService sut = new EquipmentService(repository);

        // Act
        Equipment? foundProduct = sut.GetById(3);

        // Assert
        Assert.IsNotNull(foundProduct);
        Assert.AreEqual(3, foundProduct.Id);
        Assert.AreEqual("Sony", foundProduct.Name);
        Assert.AreEqual(10000m, foundProduct.Price);
    }

    [TestMethod]
    public void UnknownId_Returns_Null_FromJsonFile()
    {
        // Arrange
        var writer = new JsonEquipmentRepository(filePath);
        writer.Add(new Cellphone(3, "Sony", 10000m, 16));
        var sut = new EquipmentService(
            new JsonEquipmentRepository(filePath));

        // Act
        var foundProduct = sut.GetById(999);

        // Assert
        Assert.IsNull(foundProduct);
    }

    [TestMethod]
    public void ReplaceAll_Removes_PreviousEquipment()
    {
        // Arrange
        var repository = new JsonEquipmentRepository(filePath);
        repository.Add(new Cellphone(3, "Sony", 10000m, 16));
        Equipment[] replacement =
        {
        new Laptop(7, "Dell", 15000m, 14)
    };

        // Act
        repository.ReplaceAll(replacement);

        // Assert
        var saved = new JsonEquipmentRepository(filePath).GetAll();
        Assert.AreEqual(1, saved.Count);
        Assert.AreEqual(7, saved[0].Id);
        Assert.AreEqual("Dell", saved[0].Name);
    }
}
