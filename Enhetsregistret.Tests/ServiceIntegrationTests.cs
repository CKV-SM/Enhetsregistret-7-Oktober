using Enhetsregistret.Models;
using Enhetsregistret.Repositories;
using Enhetsregistret.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enhetsregistret.Tests;

[TestClass]
[TestCategory("Integration")]
public class ServiceIntegrationTests
{
    private string testDirectory = string.Empty;
    private string equipmentPath = string.Empty;
    private string loanPath = string.Empty;

    [TestInitialize]
    public void SetUp()
    {
        testDirectory = Path.Combine(
            Path.GetTempPath(),
            "EquipmentIntegration-" + Guid.NewGuid().ToString("N"));

        Directory.CreateDirectory(testDirectory);
        equipmentPath = Path.Combine(testDirectory, "equipment.json");
        loanPath = Path.Combine(testDirectory, "loans.json");
    }

    [TestCleanup]
    public void CleanUp()
    {
        if (Directory.Exists(testDirectory))
            Directory.Delete(testDirectory, recursive: true);
    }

    [TestMethod]
    public void Add_Equipment_CanBeFoundByNewService()
    {
        // Arrange
        var service = new EquipmentService(
            new JsonEquipmentRepository(equipmentPath));
        var equipment = new Cellphone(3, "Sony", 10000m, 16);

        // Act
        service.Add(equipment);

        var reopenedService = new EquipmentService(
            new JsonEquipmentRepository(equipmentPath));
        var result = reopenedService.GetById(3);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.Id);
        Assert.AreEqual("Sony", result.Name);
        Assert.AreEqual(10000m, result.Price);
        Assert.IsInstanceOfType<Cellphone>(result);
    }

    [TestMethod]
    public void Loan_RegisteredEquipment_IsShownAfterReload()
    {
        // Arrange
        var equipmentService = new EquipmentService(
            new JsonEquipmentRepository(equipmentPath));
        equipmentService.Add(new Cellphone(3, "Sony", 10000m, 16));

        var loanService = new LoanService(
            new JsonEquipmentRepository(equipmentPath),
            new JsonLoanRepository(loanPath));

        // Act
        loanService.Loan(3);

        var reopenedService = new LoanService(
            new JsonEquipmentRepository(equipmentPath),
            new JsonLoanRepository(loanPath));
        var result = reopenedService.GetLoanedEquipment();

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Sony", result[0].EquipmentName);
        Assert.IsNull(result[0].ReturnDate);

        // Kontrollera också kopplingen i det sparade lånet.
        var savedLoans = new JsonLoanRepository(loanPath).GetAll();
        Assert.AreEqual(1, savedLoans.Count);
        Assert.AreEqual(3, savedLoans[0].EquipmentId);
        Assert.AreEqual(savedLoans[0].StartDate, result[0].BorrowDate);
    }
}
